using ControleMedicamentos.Dominio.ModuloFornecedor;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.ModuloFornecedor
{
    public class RepositorioFornecedorEmBancoDeDados : IRepositorioFornecedor
    {

#region sqls  
        private const string enderecoBanco =
              "Data Source=(LocalDB)\\MSSqlLocalDB;" +
              "Initial Catalog=Controle de Medicamentos;" +
              "Integrated Security=True;" +
              "Pooling=False";

        private const string sqlInserir =
            @"INSERT INTO [TBFORNECEDOR]
                   (
                        [NOME],
                        [TELEFONE],
                        [EMAIL],
                        [CIDADE],
                        [ESTADO]
                   )
                VALUES
                   (
                        @NOME,
                        @TELEFONE,
                        @EMAIL,
                        @CIDADE,
                        @ESTADO
                    ); 
                SELECT SCOPE_IDENTITY()";

        private const string sqlEditar =
            @"UPDATE [TBFORNECEDOR]	
		        SET
                    [NOME] = @NOME,
                    [TELEFONE] = @TELEFONE,
                    [EMAIL] = @EMAIL,
                    [CIDADE] = @CIDADE,
                    [ESTADO] = @ESTADO
		        WHERE
			        [ID] = @ID";

        private const string sqlExcluir =
            @"DELETE FROM [TBFORNECEDOR]
		        WHERE
			        [ID] = @ID";

        private const string sqlSelecionarPorNumero =
            @"SELECT 
		            [ID], 
		            [NOME],
                    [TELEFONE],
                    [EMAIL],
                    [CIDADE],
                    [ESTADO]
	            FROM 
		            [TBFORNECEDOR]
		        WHERE
                    [ID] = @ID";

        private const string sqlSelecionarTodos =
            @"SELECT 
		            [ID], 
		            [NOME],
                    [TELEFONE],
                    [EMAIL],
                    [CIDADE],
                    [ESTADO]
	            FROM 
		            [TBFORNECEDOR]";

#endregion

        public ValidationResult Editar(Fornecedor fornecedor)
        {

            var validador = new ValidadorFornecedor();

            var resultadoValidacao = validador.Validate(fornecedor);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosFornecedor(fornecedor, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;

        }

        public ValidationResult Excluir(Fornecedor fornecedor)
        {

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("ID", fornecedor.Id);

            conexaoComBanco.Open();
            int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            var resultadoValidacao = new ValidationResult();

            if (numeroRegistrosExcluidos == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            conexaoComBanco.Close();

            return resultadoValidacao;

        }

        public ValidationResult Inserir(Fornecedor fornecedor)
        {

            var validador = new ValidadorFornecedor();

            var resultadoValidacao = validador.Validate(fornecedor);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosFornecedor(fornecedor, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            fornecedor.Id = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return resultadoValidacao;

        }

        public Fornecedor SelecionarPorNumero(int ID)
        {

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorNumero, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("ID", ID);

            conexaoComBanco.Open();
            SqlDataReader leitorFornecedor = comandoSelecao.ExecuteReader();

            Fornecedor fornecedor = null;
            if (leitorFornecedor.Read())
                fornecedor = ConverterParaFornecedor(leitorFornecedor);

            conexaoComBanco.Close();

            return fornecedor;

        }

        public List<Fornecedor> SelecionarTodos()
        {

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();

            SqlDataReader leitorFornecedor = comandoSelecao.ExecuteReader();

            List<Fornecedor> fornecedores = new List<Fornecedor>();

            while (leitorFornecedor.Read())
            {
                Fornecedor fornecedor = ConverterParaFornecedor(leitorFornecedor);

                fornecedores.Add(fornecedor);
            }

            conexaoComBanco.Close();

            return fornecedores;

        }

        private Fornecedor ConverterParaFornecedor(SqlDataReader leitorFornecedor)
        {
            int id = Convert.ToInt32(leitorFornecedor["ID"]);
            string nome = Convert.ToString(leitorFornecedor["NOME"]);
            string telefone = Convert.ToString(leitorFornecedor["TELEFONE"]);
            string email = Convert.ToString(leitorFornecedor["EMAIL"]);
            string cidade = Convert.ToString(leitorFornecedor["CIDADE"]);
            string estado = Convert.ToString(leitorFornecedor["ESTADO"]);

            var fornecedor = new Fornecedor()
            {
                Id = id,
                Nome = nome,
                Telefone = telefone,
                Email = email,
                Cidade = cidade,
                Estado = estado
            };

            return fornecedor;
        }

        private void ConfigurarParametrosFornecedor(Fornecedor fornecedor, SqlCommand comandoInsercao)
        {

            comandoInsercao.Parameters.AddWithValue("ID", fornecedor.Id);
            comandoInsercao.Parameters.AddWithValue("NOME", fornecedor.Nome);
            comandoInsercao.Parameters.AddWithValue("TELEFONE", fornecedor.Telefone);
            comandoInsercao.Parameters.AddWithValue("EMAIL", fornecedor.Email);
            comandoInsercao.Parameters.AddWithValue("CIDADE", fornecedor.Cidade);
            comandoInsercao.Parameters.AddWithValue("ESTADO", fornecedor.Estado);

        }
    }
}
