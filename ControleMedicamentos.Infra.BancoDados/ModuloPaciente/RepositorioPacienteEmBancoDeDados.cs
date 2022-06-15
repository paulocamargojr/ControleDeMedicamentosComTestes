using ControleMedicamentos.Dominio.ModuloPaciente;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.ModuloPaciente
{
    public class RepositorioPacienteEmBancoDeDados : IRepositorioPaciente
    {

        #region sqls  
        private const string enderecoBanco =
              "Data Source=(LocalDB)\\MSSqlLocalDB;" +
              "Initial Catalog=Controle de Medicamentos;" +
              "Integrated Security=True;" +
              "Pooling=False";

        private const string sqlInserir =
            @"INSERT INTO [TBPACIENTE]
                   (
                        [NOME],
                        [CARTAOSUS]
                   )
                VALUES
                   (
                        @NOME,
                        @CARTAOSUS
                    ); 
                SELECT SCOPE_IDENTITY()";

        private const string sqlEditar =
            @"UPDATE [TBPACIENTE]	
		        SET
                    [NOME] = @NOME,
                    [CARTAOSUS] = @CARTAOSUS
		        WHERE
			        [ID] = @ID";

        private const string sqlExcluir =
            @"DELETE FROM [TBPACIENTE]
		        WHERE
			        [ID] = @ID";

        private const string sqlSelecionarPorNumero =
            @"SELECT 
		            [ID], 
		            [NOME],
                    [CARTAOSUS]
	            FROM 
		            [TBPACIENTE]
		        WHERE
                    [ID] = @ID";

        private const string sqlSelecionarTodos =
            @"SELECT 
		            [ID], 
		            [NOME],
                    [CARTAOSUS]
	            FROM 
		            [TBPACIENTE]";

        #endregion

        public ValidationResult Editar(Paciente paciente)
        {

            var validador = new ValidadorPaciente();

            var resultadoValidacao = validador.Validate(paciente);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosPaciente(paciente, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;

        }

        public ValidationResult Excluir(Paciente paciente)
        {

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("ID", paciente.Id);

            conexaoComBanco.Open();
            int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            var resultadoValidacao = new ValidationResult();

            if (numeroRegistrosExcluidos == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            conexaoComBanco.Close();

            return resultadoValidacao;

        }

        public ValidationResult Inserir(Paciente paciente)
        {

            var validador = new ValidadorPaciente();

            var resultadoValidacao = validador.Validate(paciente);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosPaciente(paciente, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            paciente.Id = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return resultadoValidacao;

        }

        public Paciente SelecionarPorNumero(int ID)
        {

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorNumero, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("ID", ID);

            conexaoComBanco.Open();
            SqlDataReader leitorPaciente = comandoSelecao.ExecuteReader();

            Paciente paciente = null;
            if (leitorPaciente.Read())
                paciente = ConverterParaPaciente(leitorPaciente);

            conexaoComBanco.Close();

            return paciente;

        }

        public List<Paciente> SelecionarTodos()
        {

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();

            SqlDataReader leitorPaciente = comandoSelecao.ExecuteReader();

            List<Paciente> pacientes = new List<Paciente>();

            while (leitorPaciente.Read())
            {
                Paciente paciente = ConverterParaPaciente(leitorPaciente);

                pacientes.Add(paciente);
            }

            conexaoComBanco.Close();

            return pacientes;

        }

        private Paciente ConverterParaPaciente(SqlDataReader leitorPaciente)
        {
            int id = Convert.ToInt32(leitorPaciente["ID"]);
            string nome = Convert.ToString(leitorPaciente["NOME"]);
            string cartao = Convert.ToString(leitorPaciente["CARTAOSUS"]);

            var paciente = new Paciente()
            {
                Id = id,
                Nome = nome,
                CartaoSUS = cartao
            };

            return paciente;
        }

        private void ConfigurarParametrosPaciente(Paciente paciente, SqlCommand comandoInsercao)
        {

            comandoInsercao.Parameters.AddWithValue("ID", paciente.Id);
            comandoInsercao.Parameters.AddWithValue("NOME", paciente.Nome);
            comandoInsercao.Parameters.AddWithValue("CARTAOSUS", paciente.CartaoSUS);

        }

    }
}
