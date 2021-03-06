using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControleMedicamentos.Infra.BancoDados.ModuloRequisicao
{
    public class RepositorioRequisicaoEmBancoDeDados : IRepositorioRequisicao
    {

        #region sqls  
        private const string enderecoBanco =
              "Data Source=(LocalDB)\\MSSqlLocalDB;" +
              "Initial Catalog=Controle de Medicamentos;" +
              "Integrated Security=True;" +
              "Pooling=False";

        private const string sqlInserir =
            @"INSERT INTO [TBREQUISICAO]
                   (
                        [MEDICAMENTO_ID],
                        [PACIENTE_ID],
                        [QUANTIDADEMEDICAMENTO],
                        [DATA],
                        [FUNCIONARIO_ID]
                   )
                VALUES
                   (
                        @MEDICAMENTO_ID,
                        @PACIENTE_ID,
                        @QUANTIDADEMEDICAMENTO,
                        @DATA,
                        @FUNCIONARIO_ID
                    ); 
                SELECT SCOPE_IDENTITY()";

        private const string sqlEditar =
            @"UPDATE [TBREQUISICAO]	
		        SET
                    [MEDICAMENTO_ID] = @MEDICAMENTO_ID,
                    [PACIENTE_ID] = @PACIENTE_ID,
                    [QUANTIDADEMEDICAMENTO] = @QUANTIDADEMEDICAMENTO,
                    [DATA] = @DATA,
                    [FUNCIONARIO_ID] = @FUNCIONARIO_ID
		        WHERE
			        [ID] = @ID";

        private const string sqlExcluir =
            @"DELETE FROM [TBREQUISICAO]
		        WHERE
			        [ID] = @ID";

        private const string sqlSelecionarPorNumero =
            @"SELECT 
                    R.ID,
                    R.QuantidadeMedicamento,
                    R.DATA,

                    M.NOME AS NOME_MEDICAMENTO,
                    M.ID AS ID_MEDICAMENTO,
                    P.NOME AS NOME_PACIENTE,
                    P.ID AS ID_PACIENTE,
                    F.NOME AS NOME_FUNCIONARIO,
                    F.ID AS ID_FUNCIONARIO
                    
                    
	            FROM TBREQUISICAO AS R
		             INNER JOIN TBMEDICAMENTO AS M ON
	            R.MEDICAMENTO_ID = M.ID
                     INNER JOIN TBPACIENTE AS P ON 
                R.PACIENTE_ID = P.ID
                     INNER JOIN TBFUNCIONARIO AS F ON
                R.FUNCIONARIO_ID = F.ID

		        WHERE
                    R.ID = @ID";



        private const string sqlSelecionarTodos =
            @"SELECT 
                    R.ID,
                    R.QTDMEDICAMENTO,
                    R.DATA,

                    M.NOME AS NOME_MEDICAMENTO,
                    M.ID AS ID_MEDICAMENTO,
                    P.NOME AS NOME_PACIENTE,
                    P.ID AS ID_PACIENTE,
                    F.NOME AS NOME_FUNCIONARIO,
                    F.ID AS ID_FUNCIONARIO
                    
	            FROM 
		            TBREQUISICAO AS R INNER JOIN TBMEDICAMENTO AS M ON
	            R.MEDICAMENTO_ID = M.ID
                    TBREQUISICAO AS R INNER JOIN TBPACIENTE AS P ON 
                R.PACIENTE_ID = P.ID
                    TBREQUISICAO AS R INNER JOIN TBFUNCIONARIO AS F ON
                R.FUNCIONARIO_ID = F.ID";

        #endregion

        public ValidationResult Editar(Requisicao requisicao)
        {

            var validador = new ValidadorRequisicao();

            var resultadoValidacao = validador.Validate(requisicao);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosRequisicao(requisicao, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;

        }

        public ValidationResult Excluir(Requisicao requisicao)
        {

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("ID", requisicao.Id);

            conexaoComBanco.Open();
            int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            var resultadoValidacao = new ValidationResult();

            if (numeroRegistrosExcluidos == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            conexaoComBanco.Close();

            return resultadoValidacao;

        }

        public ValidationResult Inserir(Requisicao requisicao)
        {

            var validador = new ValidadorRequisicao();

            var resultadoValidacao = validador.Validate(requisicao);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosRequisicao(requisicao, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            requisicao.Id = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return resultadoValidacao;

        }

        public Requisicao SelecionarPorNumero(int ID)
        {

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorNumero, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("ID", ID);

            conexaoComBanco.Open();
            SqlDataReader leitorRequisicao = comandoSelecao.ExecuteReader();

            Requisicao requisicao = null;
            if (leitorRequisicao.Read())
                requisicao = ConverterParaRequisicao(leitorRequisicao);

            conexaoComBanco.Close();

            return requisicao;

        }

        public List<Requisicao> SelecionarTodos()
        {

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitorRequisicao = comandoSelecao.ExecuteReader();

            List<Requisicao> requisicoes = new List<Requisicao>();

            while (leitorRequisicao.Read())
            {
                Requisicao requisicao = ConverterParaRequisicao(leitorRequisicao);

                requisicoes.Add(requisicao);
            }

            conexaoComBanco.Close();

            return requisicoes;

        }

        private static void ConfigurarParametrosRequisicao(Requisicao requisicao, SqlCommand comandoInsercao)
        {
            comandoInsercao.Parameters.AddWithValue("ID", requisicao.Id);
            comandoInsercao.Parameters.AddWithValue("MEDICAMENTO_ID", requisicao.Medicamento.Id);
            comandoInsercao.Parameters.AddWithValue("PACIENTE_ID", requisicao.Paciente.Id);
            comandoInsercao.Parameters.AddWithValue("QuantidadeMedicamento", requisicao.QtdMedicamento);
            comandoInsercao.Parameters.AddWithValue("DATA", requisicao.Data);
            comandoInsercao.Parameters.AddWithValue("FUNCIONARIO_ID", requisicao.Funcionario.Id);
        }

        private Requisicao ConverterParaRequisicao(SqlDataReader leitorRequisicao)
        {
            int id = Convert.ToInt32(leitorRequisicao["ID"]);
            int qtdMedicamento = Convert.ToInt32(leitorRequisicao["QuantidadeMedicamento"]);
            DateTime data = Convert.ToDateTime(leitorRequisicao["DATA"]);
            int idPaciente = Convert.ToInt32(leitorRequisicao["ID_PACIENTE"]);
            string nomePaciente = Convert.ToString(leitorRequisicao["NOME_PACIENTE"]);
            int idMedicamento = Convert.ToInt32(leitorRequisicao["ID_MEDICAMENTO"]);
            string nomeMedicamento = Convert.ToString(leitorRequisicao["NOME_MEDICAMENTO"]);
            int idFuncionario = Convert.ToInt32(leitorRequisicao["ID_FUNCIONARIO"]);
            string nomeFuncionario = Convert.ToString(leitorRequisicao["NOME_FUNCIONARIO"]);


            var requisicao = new Requisicao
            {
                Id = id,
                QtdMedicamento = qtdMedicamento,
                Data = data,
                Paciente = new Paciente
                {
                    Id = idPaciente,
                    Nome = nomePaciente
                },
                Medicamento = new Medicamento
                {
                    Id = idMedicamento,
                    Nome = nomeMedicamento
                },
                Funcionario = new Funcionario
                {
                    Id = idFuncionario,
                    Nome = nomeFuncionario
                }
            };

            return requisicao;
        }
    }
}
