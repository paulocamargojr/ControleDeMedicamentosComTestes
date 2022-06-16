using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControleMedicamento.Infra.BancoDados.ModuloMedicamento
{
    public class RepositorioMedicamentoEmBancoDados : IRepositorioMedicamento
    {

        private const string enderecoBanco =
              "Data Source=(LocalDB)\\MSSqlLocalDB;" +
              "Initial Catalog=Controle de Medicamentos;" +
              "Integrated Security=True;" +
              "Pooling=False";

        private const string sqlInserir =
         @"INSERT INTO [TBMEDICAMENTO]
                (
                    [NOME],                    
                    [DESCRICAO],
                    [LOTE],
                    [VALIDADE],
                    [QUANTIDADEDISPONIVEL],
                    [FORNECEDOR_ID]
	            )
	            VALUES
                (
                    @NOME,                    
                    @DESCRICAO,
                    @LOTE,
                    @VALIDADE,
                    @QUANTIDADEDISPONIVEL,
                    @FORNECEDOR_ID

                );SELECT SCOPE_IDENTITY();";

        private const string sqlEditar =
            @"UPDATE [TBMEDICAMENTO]	
		        SET
                    [NOME] = @NOME,
                    [DESCRICAO] = @DESCRICAO,
                    [LOTE] = @LOTE,
                    [VALIDADE] = @VALIDADE,
                    [QUANTIDADEDISPONIVEL] = @QUANTIDADEDISPONIVEL,
                    [FORNECEDOR_ID] = FORNECEDOR_ID
		        WHERE
			        [ID] = @ID";

        private const string sqlExcluir =
           @"
            DELETE FROM [TBMEDICAMENTO]
		            WHERE
			            [ID] = @ID";

        private const string sqlSelecionarPorNumero =
            @"SELECT        
	                M.ID,
	                M.NOME, 
	                M.DESCRICAO, 
	                M.LOTE, 
                    M.VALIDADE,
 	                M.QUANTIDADEDISPONIVEL,
	
	                F.ID FORNECEDOR_ID	
                FROM  
	                TBMEDICAMENTO M INNER JOIN TBFORNECEDOR F
                ON 
	                M.FORNECEDOR_ID = F.ID
                WHERE 
	                M.[ID] = @ID";

        private const string sqlSelecionarTodos =
            @"SELECT        
	                M.ID,
	                M.NOME, 
	                M.DESCRICAO, 
	                M.LOTE, 
                    M.VALIDADE,
 	                M.QUANTIDADEDISPONIVEL,
	
	                F.ID FORNECEDOR_ID	
                FROM  
	                TBMEDICAMENTO M INNER JOIN TBFORNECEDOR F
                ON 
	                M.FORNECEDOR_ID = F.ID";



        public ValidationResult Editar(Medicamento medicamento)
        {
            var validador = new ValidadorMedicamento();

            var resultadoValidacao = validador.Validate(medicamento);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosMedicamento(medicamento, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Medicamento medicamento)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("ID", medicamento.Id);

            conexaoComBanco.Open();
            int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            var resultadoValidacao = new ValidationResult();

            if (numeroRegistrosExcluidos == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Inserir(Medicamento medicamento)
        {

            ValidadorMedicamento validador = new ValidadorMedicamento();

            var resultadoValidacao = validador.Validate(medicamento);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosMedicamento(medicamento, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            medicamento.Id = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return resultadoValidacao;

        }

        public Medicamento SelecionarPorNumero(int ID)
        {

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorNumero, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("ID", ID);

            conexaoComBanco.Open();
            SqlDataReader leitorMedicamento = comandoSelecao.ExecuteReader();

            Medicamento medicamento = null;

            if (leitorMedicamento.Read())
            {
                medicamento = ConverterParaMedicamento(leitorMedicamento);
            }

            conexaoComBanco.Close();

            return medicamento;

        }

        private Medicamento ConverterParaMedicamento(SqlDataReader leitorMedicamento)
        {

            int id = Convert.ToInt32(leitorMedicamento["ID"]);
            string nomeMedicamento = Convert.ToString(leitorMedicamento["NOME"]);
            string descricao = Convert.ToString(leitorMedicamento["DESCRICAO"]);
            string lote = Convert.ToString(leitorMedicamento["LOTE"]);
            DateTime validade = Convert.ToDateTime(leitorMedicamento["VALIDADE"]);
            int qtdDisponivel = Convert.ToInt32(leitorMedicamento["QUANTIDADEDISPONIVEL"]);
            int fornecedorId = Convert.ToInt32(leitorMedicamento["FORNECEDOR_ID"]);

            var medicamento = new Medicamento
            {
                Id = id,
                Nome = nomeMedicamento,
                Descricao = descricao,
                Lote = lote,
                Validade = validade,
                QuantidadeDisponivel = qtdDisponivel,
                Fornecedor = new Fornecedor()
                {
                    Id = fornecedorId
                }
            };

            return medicamento;

        }

        public List<Medicamento> SelecionarTodos()
        {

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitorMedicamento = comandoSelecao.ExecuteReader();

            List<Medicamento> medicamentos = new List<Medicamento>();

            while (leitorMedicamento.Read())
            {
                Medicamento medicamento = ConverterParaMedicamento(leitorMedicamento);

                medicamentos.Add(medicamento);
            }

            conexaoComBanco.Close();

            return medicamentos;

        }

        private void ConfigurarParametrosMedicamento(Medicamento medicamento, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", medicamento.Id);
            comando.Parameters.AddWithValue("NOME", medicamento.Nome);
            comando.Parameters.AddWithValue("DESCRICAO", medicamento.Descricao);
            comando.Parameters.AddWithValue("LOTE", medicamento.Lote);
            comando.Parameters.AddWithValue("VALIDADE", medicamento.Validade);
            comando.Parameters.AddWithValue("QUANTIDADEDISPONIVEL", medicamento.QuantidadeDisponivel);
            comando.Parameters.AddWithValue("FORNECEDOR_ID", medicamento.Fornecedor.Id);

        }
    }
}
