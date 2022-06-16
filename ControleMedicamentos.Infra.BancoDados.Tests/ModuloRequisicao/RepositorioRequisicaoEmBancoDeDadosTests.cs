using ControleMedicamento.Infra.BancoDados.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using ControleMedicamentos.Infra.BancoDados.ModuloFuncionario;
using ControleMedicamentos.Infra.BancoDados.ModuloPaciente;
using ControleMedicamentos.Infra.BancoDados.ModuloRequisicao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloRequisicao
{

    [TestClass]
    public class RepositorioRequisicaoEmBancoDeDadosTests
    {

        public RepositorioRequisicaoEmBancoDeDadosTests()
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
        public void Deve_Inserir_Requisicao()
        {

            Paciente paciente = new Paciente("Paulo", "1234567890");

            var repositorioPaciente = new RepositorioPacienteEmBancoDeDados();

            repositorioPaciente.Inserir(paciente);

            Funcionario funcionario = new Funcionario("Paulo", "Paulo@login.com", "123");

            var repositorioFuncionario = new RepositorioFuncionarioEmBancoDeDados();

            repositorioFuncionario.Inserir(funcionario);

            Fornecedor fornecedor = new Fornecedor("Paulo", "123456789", "paulo@fornecedor.com", "Lages", "SC");

            var repositorioFornecedor = new RepositorioFornecedorEmBancoDeDados();

            repositorioFornecedor.Inserir(fornecedor);

            Medicamento medicamento = new Medicamento("Dipirona", "Remédio para dor de cabeça", "500f", DateTime.Now.Date, 100, fornecedor);

            var repositorioMedicamento = new RepositorioMedicamentoEmBancoDados();

            repositorioMedicamento.Inserir(medicamento);

            Requisicao requisicao = new Requisicao(medicamento, paciente, 10, DateTime.Now.Date, funcionario);

            var repositorio = new RepositorioRequisicaoEmBancoDeDados();

            repositorio.Inserir(requisicao);

            Requisicao requisicaoEncontrada = repositorio.SelecionarPorNumero(requisicao.Id);

            Assert.IsNotNull(requisicaoEncontrada);
            Assert.AreEqual(requisicao.Id, requisicaoEncontrada.Id);
            Assert.AreEqual(requisicao.Medicamento.Id, requisicaoEncontrada.Medicamento.Id);
            Assert.AreEqual(requisicao.Paciente.Id, requisicaoEncontrada.Paciente.Id);
            Assert.AreEqual(requisicao.QtdMedicamento, requisicaoEncontrada.QtdMedicamento);
            Assert.AreEqual(requisicao.Data, requisicaoEncontrada.Data);
            Assert.AreEqual(requisicao.Funcionario.Id, requisicaoEncontrada.Funcionario.Id);

        }

        [TestMethod]
        public void Deve_Editar_Requisicao()
        {

            Paciente paciente = new Paciente("Paulo", "1234567890");

            var repositorioPaciente = new RepositorioPacienteEmBancoDeDados();

            repositorioPaciente.Inserir(paciente);

            Funcionario funcionario = new Funcionario("Paulo", "Paulo@login.com", "123");

            var repositorioFuncionario = new RepositorioFuncionarioEmBancoDeDados();

            repositorioFuncionario.Inserir(funcionario);

            Fornecedor fornecedor = new Fornecedor("Paulo", "123456789", "paulo@fornecedor.com", "Lages", "SC");

            var repositorioFornecedor = new RepositorioFornecedorEmBancoDeDados();

            repositorioFornecedor.Inserir(fornecedor);

            Medicamento medicamento = new Medicamento("Dipirona", "Remédio para dor de cabeça", "500f", DateTime.Now.Date, 100, fornecedor);

            var repositorioMedicamento = new RepositorioMedicamentoEmBancoDados();

            repositorioMedicamento.Inserir(medicamento);

            Requisicao requisicao = new Requisicao(medicamento, paciente, 10, DateTime.Now.Date, funcionario);

            var repositorio = new RepositorioRequisicaoEmBancoDeDados();

            repositorio.Inserir(requisicao);

            Requisicao requisicaoAtualizada = repositorio.SelecionarPorNumero(requisicao.Id);

            Paciente novoPaciente = obterPaciente();
            Medicamento novoMedicamento = obterMeedicamento();
            Funcionario novoFuncionario = obterFuncionario();

            requisicaoAtualizada.Medicamento = novoMedicamento;
            requisicaoAtualizada.Paciente = novoPaciente;
            requisicaoAtualizada.QtdMedicamento = 15;
            requisicaoAtualizada.Funcionario = novoFuncionario;

            repositorio.Editar(requisicaoAtualizada);

            Requisicao requisicaoEncontrada = repositorio.SelecionarPorNumero(requisicao.Id);

            Assert.IsNotNull(requisicaoEncontrada);
            Assert.AreEqual(requisicaoAtualizada.Id, requisicaoEncontrada.Id);
            Assert.AreEqual(requisicaoAtualizada.Medicamento.Nome, requisicaoEncontrada.Medicamento.Nome);
            Assert.AreEqual(requisicaoAtualizada.Paciente.Nome, requisicaoEncontrada.Paciente.Nome);
            Assert.AreEqual(requisicaoAtualizada.QtdMedicamento, requisicaoEncontrada.QtdMedicamento);
            Assert.AreEqual(requisicaoAtualizada.Funcionario.Nome, requisicaoEncontrada.Funcionario.Nome);

        }

        private Funcionario obterFuncionario()
        {
            Funcionario funcionario = new Funcionario("Teste", "Paulo@login.com", "123");

            var repositorioFuncionario = new RepositorioFuncionarioEmBancoDeDados();

            repositorioFuncionario.Inserir(funcionario);

            return funcionario;
        }

        private Medicamento obterMeedicamento()
        {
            Fornecedor fornecedor = new Fornecedor("Paulo", "123456789", "paulo@fornecedor.com", "Lages", "SC");

            var repositorioFornecedor = new RepositorioFornecedorEmBancoDeDados();

            repositorioFornecedor.Inserir(fornecedor);

            Medicamento medicamento = new Medicamento("teste", "Remédio para dor de cabeça", "500f", DateTime.Now.Date, 100, fornecedor);

            var repositorioMedicamento = new RepositorioMedicamentoEmBancoDados();

            repositorioMedicamento.Inserir(medicamento);

            return medicamento;
        }

        private Paciente obterPaciente()
        {

            Paciente paciente = new Paciente("Peedro", "1237567890");

            var repositorioPaciente = new RepositorioPacienteEmBancoDeDados();

            repositorioPaciente.Inserir(paciente);

            return paciente;

        }

        [TestMethod]
        public void Deve_Excluir_Requisicao()
        {

            Paciente paciente = new Paciente("Paulo", "1234567890");

            var repositorioPaciente = new RepositorioPacienteEmBancoDeDados();

            repositorioPaciente.Inserir(paciente);

            Funcionario funcionario = new Funcionario("Paulo", "Paulo@login.com", "123");

            var repositorioFuncionario = new RepositorioFuncionarioEmBancoDeDados();

            repositorioFuncionario.Inserir(funcionario);

            Fornecedor fornecedor = new Fornecedor("Paulo", "123456789", "paulo@fornecedor.com", "Lages", "SC");

            var repositorioFornecedor = new RepositorioFornecedorEmBancoDeDados();

            repositorioFornecedor.Inserir(fornecedor);

            Medicamento medicamento = new Medicamento("Dipirona", "Remédio para dor de cabeça", "500f", DateTime.Now.Date, 100, fornecedor);

            var repositorioMedicamento = new RepositorioMedicamentoEmBancoDados();

            repositorioMedicamento.Inserir(medicamento);

            Requisicao requisicao = new Requisicao(medicamento, paciente, 10, DateTime.Now.Date, funcionario);

            var repositorio = new RepositorioRequisicaoEmBancoDeDados();

            repositorio.Inserir(requisicao);

            repositorio.Excluir(requisicao);

            Requisicao requisicaoEncontrada = repositorio.SelecionarPorNumero(requisicao.Id);

            Assert.IsNull(requisicaoEncontrada);

        }

        [TestMethod]
        public void Deve_selecionar_uma_requisicao()
        {

            Paciente paciente = new Paciente("Paulo", "1234567890");

            var repositorioPaciente = new RepositorioPacienteEmBancoDeDados();

            repositorioPaciente.Inserir(paciente);

            Funcionario funcionario = new Funcionario("Paulo", "Paulo@login.com", "123");

            var repositorioFuncionario = new RepositorioFuncionarioEmBancoDeDados();

            repositorioFuncionario.Inserir(funcionario);

            Fornecedor fornecedor = new Fornecedor("Paulo", "123456789", "paulo@fornecedor.com", "Lages", "SC");

            var repositorioFornecedor = new RepositorioFornecedorEmBancoDeDados();

            repositorioFornecedor.Inserir(fornecedor);

            Medicamento medicamento = new Medicamento("Dipirona", "Remédio para dor de cabeça", "500f", DateTime.Now.Date, 100, fornecedor);

            var repositorioMedicamento = new RepositorioMedicamentoEmBancoDados();

            repositorioMedicamento.Inserir(medicamento);

            Requisicao requisicao = new Requisicao(medicamento, paciente, 10, DateTime.Now.Date, funcionario);

            var repositorio = new RepositorioRequisicaoEmBancoDeDados();

            repositorio.Inserir(requisicao);

            Requisicao requisicaoEncontrada = repositorio.SelecionarPorNumero(requisicao.Id);

            Assert.AreEqual(requisicao.Id, requisicaoEncontrada.Id);
            Assert.AreEqual(10, requisicaoEncontrada.QtdMedicamento);

        }

    }
}
