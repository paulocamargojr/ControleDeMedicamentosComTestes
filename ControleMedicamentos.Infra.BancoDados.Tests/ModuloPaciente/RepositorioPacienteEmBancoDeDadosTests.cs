using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloPaciente;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloPaciente
{

    [TestClass]
    public class RepositorioPacienteEmBancoDeDadosTests
    {

        public RepositorioPacienteEmBancoDeDadosTests()
        {
            string sql =
               @"DELETE FROM TBPACIENTE;
                  DBCC CHECKIDENT (TBPACIENTE, RESEED, 0)";

            DB.ExecutarSql(sql);
        }

        [TestMethod]
        public void Deve_Inserir_Paciente()
        {

            Paciente paciente = new Paciente("Paulo", "1234567890");

            var repositorio = new RepositorioPacienteEmBancoDeDados();

            repositorio.Inserir(paciente);

            Paciente pacienteEncontrado = repositorio.SelecionarPorNumero(paciente.Id);

            Assert.IsNotNull(pacienteEncontrado);
            Assert.AreEqual(paciente.Id, pacienteEncontrado.Id);
            Assert.AreEqual(paciente.Nome, pacienteEncontrado.Nome);
            Assert.AreEqual(paciente.CartaoSUS, pacienteEncontrado.CartaoSUS);

        }

        [TestMethod]
        public void Deve_Editar_Paciente()
        {

            Paciente paciente = new Paciente("Paulo", "1234567890");

            var repositorio = new RepositorioPacienteEmBancoDeDados();

            repositorio.Inserir(paciente);

            Paciente pacienteAtualizado = repositorio.SelecionarPorNumero(paciente.Id);

            pacienteAtualizado.Nome = "Pedro";
            pacienteAtualizado.CartaoSUS = "9876543210";

            repositorio.Editar(pacienteAtualizado);

            Paciente pacienteEncontrado = repositorio.SelecionarPorNumero(paciente.Id);

            Assert.IsNotNull(pacienteEncontrado);
            Assert.AreEqual(pacienteAtualizado.Id, pacienteEncontrado.Id);
            Assert.AreEqual(pacienteAtualizado.Nome, pacienteEncontrado.Nome);
            Assert.AreEqual(pacienteAtualizado.CartaoSUS, pacienteEncontrado.CartaoSUS);

        }

        [TestMethod]
        public void Deve_Excluir_Paciente()
        {

            Paciente paciente = new Paciente("Paulo", "1234567890");

            var repositorio = new RepositorioPacienteEmBancoDeDados();

            repositorio.Inserir(paciente);

            repositorio.Excluir(paciente);

            Paciente pacienteEncontrado = repositorio.SelecionarPorNumero(paciente.Id);

            Assert.IsNull(pacienteEncontrado);

        }

        [TestMethod]
        public void Deve_Selecionar_Um_Paciente()
        {

            Paciente paciente = new Paciente("Paulo", "1234567890");

            var repositorio = new RepositorioPacienteEmBancoDeDados();

            repositorio.Inserir(paciente);

            repositorio.SelecionarPorNumero(paciente.Id);

            Assert.AreEqual("Paulo", paciente.Nome);
            Assert.AreEqual("1234567890", paciente.CartaoSUS);

        }

        [TestMethod]
        public void Deve_Selecionar_Todos_Pacientes()
        {

            Paciente paciente = new Paciente("Paulo", "1234567890");

            Paciente paciente2 = new Paciente("Joao", "09876543211");

            Paciente paciente3 = new Paciente("Maria", "1234509876");

            var repositorio = new RepositorioPacienteEmBancoDeDados();

            repositorio.Inserir(paciente);
            repositorio.Inserir(paciente2);
            repositorio.Inserir(paciente3);

            var pacientes = repositorio.SelecionarTodos();

            Assert.AreEqual(3, pacientes.Count);

            Assert.AreEqual("Paulo", pacientes[0].Nome);
            Assert.AreEqual("1234567890", pacientes[0].CartaoSUS);
            Assert.AreEqual("Joao", pacientes[1].Nome);
            Assert.AreEqual("09876543211", pacientes[1].CartaoSUS);
            Assert.AreEqual("Maria", pacientes[2].Nome);
            Assert.AreEqual("1234509876", pacientes[2].CartaoSUS);

        }
    }
}
