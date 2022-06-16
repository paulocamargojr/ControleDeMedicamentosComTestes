using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFuncionario;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloFuncionario
{
    [TestClass]
    public class RepositorioFuncionarioEmBancoDeDadosTests
    {

        public RepositorioFuncionarioEmBancoDeDadosTests()
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
        public void Deve_Inserir_Funcionario()
        {

            Funcionario funcionario = new Funcionario("Paulo", "Paulo@login.com", "123");

            var repositorio = new RepositorioFuncionarioEmBancoDeDados();

            repositorio.Inserir(funcionario);

            Funcionario funcionarioEncontrado = repositorio.SelecionarPorNumero(funcionario.Id);

            Assert.IsNotNull(funcionarioEncontrado);
            Assert.AreEqual(funcionario.Id, funcionarioEncontrado.Id);
            Assert.AreEqual(funcionario.Nome, funcionarioEncontrado.Nome);
            Assert.AreEqual(funcionario.Login, funcionarioEncontrado.Login);
            Assert.AreEqual(funcionario.Senha, funcionarioEncontrado.Senha);

        }

        [TestMethod]
        public void Deve_Editar_Funcionario()
        {

            Funcionario funcionario = new Funcionario("Paulo", "Paulo@login.com", "123");

            var repositorio = new RepositorioFuncionarioEmBancoDeDados();

            repositorio.Inserir(funcionario);

            Funcionario funcionarioAtualizado = repositorio.SelecionarPorNumero(funcionario.Id);
            funcionarioAtualizado.Nome = "Pedro";
            funcionarioAtualizado.Login = "Pedro@login.com";
            funcionarioAtualizado.Senha = "321";

            repositorio.Editar(funcionarioAtualizado);

            Funcionario funcionarioEncontrado = repositorio.SelecionarPorNumero(funcionario.Id);

            Assert.IsNotNull(funcionarioEncontrado);
            Assert.AreEqual(funcionarioAtualizado.Id, funcionarioEncontrado.Id);
            Assert.AreEqual(funcionarioAtualizado.Nome, funcionarioEncontrado.Nome);
            Assert.AreEqual(funcionarioAtualizado.Login, funcionarioEncontrado.Login);
            Assert.AreEqual(funcionarioAtualizado.Senha, funcionarioEncontrado.Senha);

        }

        [TestMethod]
        public void Deve_Excluir_Funcionario()
        {

            Funcionario funcionario = new Funcionario("Paulo", "Paulo@login.com", "123");

            var repositorio = new RepositorioFuncionarioEmBancoDeDados();

            repositorio.Inserir(funcionario);

            repositorio.Excluir(funcionario);

            Funcionario funcionarioEncontrado = repositorio.SelecionarPorNumero(funcionario.Id);

            Assert.IsNull(funcionarioEncontrado);

        }

        [TestMethod]
        public void Deve_Selecionar_Um_Funcionario()
        {

            Funcionario funcionario = new Funcionario("Paulo", "Paulo@login.com", "123");

            var repositorio = new RepositorioFuncionarioEmBancoDeDados();

            repositorio.Inserir(funcionario);

            repositorio.SelecionarPorNumero(funcionario.Id);

            Assert.AreEqual("Paulo", funcionario.Nome);

        }

        [TestMethod]
        public void Deve_Selecionar_Todos_Funcionarios()
        {

            Funcionario funcionario = new Funcionario("Paulo", "Paulo@login.com", "123");

            Funcionario funcionario2 = new Funcionario("Joao", "Joao@login.com", "321");

            Funcionario funcionario3 = new Funcionario("Maria", "Marria@login.com", "132");

            var repositorio = new RepositorioFuncionarioEmBancoDeDados();

            repositorio.Inserir(funcionario);
            repositorio.Inserir(funcionario2);
            repositorio.Inserir(funcionario3);

            var funcionarios = repositorio.SelecionarTodos();

            Assert.AreEqual(3, funcionarios.Count);

            Assert.AreEqual("Paulo", funcionarios[0].Nome);
            Assert.AreEqual("Joao", funcionarios[1].Nome);
            Assert.AreEqual("Maria", funcionarios[2].Nome);

        }
    }
}
