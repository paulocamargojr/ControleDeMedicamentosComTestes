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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloRequisicao
{

    [TestClass]
    public class RepositorioRequisicaoEmBancoDeDadosTests
    {

        public RepositorioRequisicaoEmBancoDeDadosTests()
        {
            //string sql =
            //    @"DELETE FROM CONTROLE DE MEDICAMENTOS";

            //DB.ExecutarSql(sql);
        }

        //[TestMethod]
        //public void Deve_Inserir_Requisicao()
        //{

        //    Paciente paciente = new Paciente("Paulo", "1234567890");

        //    var repositorioPaciente = new RepositorioPacienteEmBancoDeDados();

        //    repositorioPaciente.Inserir(paciente);

        //    Funcionario funcionario = new Funcionario("Paulo", "Paulo@login.com", "123");

        //    var repositorioFuncionario = new RepositorioFuncionarioEmBancoDeDados();

        //    repositorioFuncionario.Inserir(funcionario);

        //    Fornecedor fornecedor = new Fornecedor("Paulo", "123456789", "paulo@fornecedor.com", "Lages", "SC");

        //    var repositorioFornecedor = new RepositorioFornecedorEmBancoDeDados();

        //    repositorioFornecedor.Inserir(fornecedor);

        //    Medicamento medicamento = new Medicamento("Dipirona", "Remédio para dor de cabeça", "500f", DateTime.Now.Date, 100, fornecedor);

        //    var repositorioMedicamento = new RepositorioMedicamentoEmBancoDados();

        //    repositorioMedicamento.Inserir(medicamento);

        //    Requisicao requisicao = new Requisicao(medicamento, paciente, 10, DateTime.Now, funcionario);

        //    var repositorio = new RepositorioRequisicaoEmBancoDeDados();

        //    repositorio.Inserir(requisicao);

        //    Requisicao requisicaoEncontrada = repositorio.SelecionarPorNumero(requisicao.Id);

        //    Assert.IsNotNull(requisicaoEncontrada);
        //    Assert.AreEqual(requisicao.Id, requisicaoEncontrada.Id);
        //    Assert.AreEqual(requisicao.Medicamento.Id, requisicaoEncontrada.Medicamento.Id);
        //    Assert.AreEqual(requisicao.Paciente.Id, requisicaoEncontrada.Paciente.Id);
        //    Assert.AreEqual(requisicao.QtdMedicamento, requisicaoEncontrada.QtdMedicamento);
        //    Assert.AreEqual(requisicao.Data, requisicaoEncontrada.Data);
        //    Assert.AreEqual(requisicao.Funcionario.Id, requisicaoEncontrada.Funcionario.Id);

        //}

        //[TestMethod]
        //public void Deve_Editar_Requisicao()
        //{

        //    Fornecedor fornecedor = new Fornecedor("Paulo", "123456789", "Paulo@fornecedor.com", "Lages", "SC");

        //    var repositorio = new RepositorioFornecedorEmBancoDeDados();

        //    repositorio.Inserir(fornecedor);

        //    Fornecedor fornecedorAtualizado = repositorio.SelecionarPorNumero(fornecedor.Id);
        //    fornecedorAtualizado.Nome = "Pedro";
        //    fornecedorAtualizado.Telefone = "987654321";
        //    fornecedorAtualizado.Email = "Pedro@fornecedor.com";
        //    fornecedorAtualizado.Cidade = "Curitiba";
        //    fornecedorAtualizado.Estado = "PR";

        //    repositorio.Editar(fornecedorAtualizado);

        //    Fornecedor fornecedorEncontrado = repositorio.SelecionarPorNumero(fornecedor.Id);

        //    Assert.IsNotNull(fornecedorEncontrado);
        //    Assert.AreEqual(fornecedorAtualizado.Id, fornecedorEncontrado.Id);
        //    Assert.AreEqual(fornecedorAtualizado.Nome, fornecedorEncontrado.Nome);
        //    Assert.AreEqual(fornecedorAtualizado.Telefone, fornecedorEncontrado.Telefone);
        //    Assert.AreEqual(fornecedorAtualizado.Email, fornecedorEncontrado.Email);
        //    Assert.AreEqual(fornecedorAtualizado.Cidade, fornecedorEncontrado.Cidade);
        //    Assert.AreEqual(fornecedorAtualizado.Estado, fornecedorEncontrado.Estado);

        //}

    }
}
