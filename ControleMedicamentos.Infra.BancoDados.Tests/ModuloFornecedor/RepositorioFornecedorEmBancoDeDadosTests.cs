using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloFornecedor
{

    [TestClass]
    public class RepositorioFornecedorEmBancoDeDadosTests
    {
        public RepositorioFornecedorEmBancoDeDadosTests()
        {
            string sql =
               @"DELETE FROM TBREQUISICAO;
                  DBCC CHECKIDENT (TBREQUISICAO, RESEED, 0)
                DELETE FROM TBMEDICAMENTO;
                  DBCC CHECKIDENT (TBMEDICAMENTO, RESEED, 0)
                DELETE FROM TBFORNECEDOR;
                  DBCC CHECKIDENT (TBFORNECEDOR, RESEED, 0)
                DELETE FROM TBPACIENTE;
                  DBCC CHECKIDENT (TBPACIENTE, RESEED, 0)
                DELETE FROM TBFUNCIONARIO;
                  DBCC CHECKIDENT (TBFUNCIONARIO, RESEED, 0)
                ";

            DB.ExecutarSql(sql);
        }

        [TestMethod]
        public void Deve_Inserir_Fornecedor()
        {

            Fornecedor fornecedor = new Fornecedor("Paulo", "123456789", "paulo@fornecedor.com", "Lages", "SC");

            var repositorio = new RepositorioFornecedorEmBancoDeDados();

            repositorio.Inserir(fornecedor);

            Fornecedor fornecedorEncontrado = repositorio.SelecionarPorNumero(fornecedor.Id);

            Assert.IsNotNull(fornecedorEncontrado);
            Assert.AreEqual(fornecedor.Id, fornecedorEncontrado.Id);
            Assert.AreEqual(fornecedor.Nome, fornecedorEncontrado.Nome);
            Assert.AreEqual(fornecedor.Telefone, fornecedorEncontrado.Telefone);
            Assert.AreEqual(fornecedor.Email, fornecedor.Email);
            Assert.AreEqual(fornecedor.Cidade, fornecedor.Cidade);
            Assert.AreEqual(fornecedor.Estado, fornecedor.Estado);

        }

        [TestMethod]
        public void Deve_Editar_Fornecedor()
        {

            Fornecedor fornecedor = new Fornecedor("Paulo","123456789", "Paulo@fornecedor.com", "Lages", "SC");

            var repositorio = new RepositorioFornecedorEmBancoDeDados();

            repositorio.Inserir(fornecedor);

            Fornecedor fornecedorAtualizado = repositorio.SelecionarPorNumero(fornecedor.Id);
            fornecedorAtualizado.Nome = "Pedro";
            fornecedorAtualizado.Telefone = "987654321";
            fornecedorAtualizado.Email = "Pedro@fornecedor.com";
            fornecedorAtualizado.Cidade = "Curitiba";
            fornecedorAtualizado.Estado = "PR";

            repositorio.Editar(fornecedorAtualizado);

            Fornecedor fornecedorEncontrado = repositorio.SelecionarPorNumero(fornecedor.Id);

            Assert.IsNotNull(fornecedorEncontrado);
            Assert.AreEqual(fornecedorAtualizado.Id, fornecedorEncontrado.Id);
            Assert.AreEqual(fornecedorAtualizado.Nome, fornecedorEncontrado.Nome);
            Assert.AreEqual(fornecedorAtualizado.Telefone, fornecedorEncontrado.Telefone);
            Assert.AreEqual(fornecedorAtualizado.Email, fornecedorEncontrado.Email);
            Assert.AreEqual(fornecedorAtualizado.Cidade, fornecedorEncontrado.Cidade);
            Assert.AreEqual(fornecedorAtualizado.Estado, fornecedorEncontrado.Estado);

        }

        [TestMethod]
        public void Deve_Excluir_Fornecedor()
        {

            Fornecedor fornecedor = new Fornecedor("Paulo", "123456789", "Paulo@fornecedor.com", "Lages", "SC");

            var repositorio = new RepositorioFornecedorEmBancoDeDados();

            repositorio.Inserir(fornecedor);

            repositorio.Excluir(fornecedor);

            Fornecedor funcionarioEncontrado = repositorio.SelecionarPorNumero(fornecedor.Id);

            Assert.IsNull(funcionarioEncontrado);

        }

        [TestMethod]
        public void Deve_Selecionar_Um_Fornecedor()
        {

            Fornecedor fornecedor = new Fornecedor("Paulo", "123456789", "Paulo@fornecedor.com", "Lages", "SC");

            var repositorio = new RepositorioFornecedorEmBancoDeDados();

            repositorio.Inserir(fornecedor);

            repositorio.SelecionarPorNumero(fornecedor.Id);

            Assert.AreEqual("Paulo", fornecedor.Nome);

        }

        [TestMethod]
        public void Deve_Selecionar_Todos_Fornecedores()
        {

            Fornecedor fornecedor = new Fornecedor("Paulo", "123456789", "Paulo@fornecedor.com", "Lages", "SC");

            Fornecedor fornecedor2 = new Fornecedor("Joao", "123987654", "Joao@fornecedor.com", "Lages", "SC");

            Fornecedor fornecedor3 = new Fornecedor("Maria", "987654321", "Maria@fornecedor.com", "Lages", "SC");

            var repositorio = new RepositorioFornecedorEmBancoDeDados();

            repositorio.Inserir(fornecedor);
            repositorio.Inserir(fornecedor2);
            repositorio.Inserir(fornecedor3);

            var funcionarios = repositorio.SelecionarTodos();

            Assert.AreEqual(3, funcionarios.Count);

            Assert.AreEqual("Paulo", funcionarios[0].Nome);
            Assert.AreEqual("Joao", funcionarios[1].Nome);
            Assert.AreEqual("Maria", funcionarios[2].Nome);

        }
    }
}
