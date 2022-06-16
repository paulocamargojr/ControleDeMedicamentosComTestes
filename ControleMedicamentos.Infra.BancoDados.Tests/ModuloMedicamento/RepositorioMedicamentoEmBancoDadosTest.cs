using ControleMedicamento.Infra.BancoDados.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloMedicamento
{
    [TestClass]
    public class RepositorioMedicamentoEmBancoDadosTest
    {

        public RepositorioMedicamentoEmBancoDadosTest()
        {
            string sql =
               @"DELETE FROM TBFORNECEDOR;
                  DBCC CHECKIDENT (TBFORNECEDOR, RESEED, 0)
                DELETE FROM TBMEDICAMENTO;
                  DBCC CHECKIDENT (TBMEDICAMENTO, RESEED, 0)";

            DB.ExecutarSql(sql);
        }

        [TestMethod]
        public void Deve_inserir_medicamento()
        {

            Fornecedor fornecedor = new Fornecedor("Paulo", "123456789", "paulo@fornecedor.com", "Lages", "SC");

            var repositorioFornecedor = new RepositorioFornecedorEmBancoDeDados();

            repositorioFornecedor.Inserir(fornecedor);

            Medicamento medicamento = new Medicamento("Dipirona", "Remédio para dor de cabeça", "500f", DateTime.Now.Date, 100, fornecedor);

            var repositorio = new RepositorioMedicamentoEmBancoDados();

            repositorio.Inserir(medicamento);

            Medicamento medicamentoEncontrado = repositorio.SelecionarPorNumero(medicamento.Id);

            Assert.IsNotNull(medicamentoEncontrado);
            Assert.AreEqual(medicamento.Id, medicamentoEncontrado.Id);
            Assert.AreEqual(medicamento.Nome, medicamentoEncontrado.Nome);
            Assert.AreEqual(medicamento.Descricao, medicamentoEncontrado.Descricao);
            Assert.AreEqual(medicamento.Lote, medicamentoEncontrado.Lote);
            Assert.AreEqual(medicamento.Validade, medicamentoEncontrado.Validade);
            Assert.AreEqual(medicamento.QuantidadeDisponivel, medicamentoEncontrado.QuantidadeDisponivel);
            Assert.AreEqual(medicamento.Fornecedor.Id, medicamentoEncontrado.Fornecedor.Id);

        }

        [TestMethod]
        public void Deve_editar_medicamento()
        {

            Fornecedor fornecedor = new Fornecedor("Paulo", "123456789", "paulo@fornecedor.com", "Lages", "SC");

            var repositorioFornecedor = new RepositorioFornecedorEmBancoDeDados();

            repositorioFornecedor.Inserir(fornecedor);

            Medicamento medicamento = new Medicamento("Dipirona", "Remédio para dor de cabeça", "500f", DateTime.Now.Date.AddMonths(8), 100, fornecedor);

            var repositorio = new RepositorioMedicamentoEmBancoDados();

            repositorio.Inserir(medicamento);

            Medicamento medicamentoAtualizado = repositorio.SelecionarPorNumero(medicamento.Id);

            medicamentoAtualizado.Nome = "Comprimido";
            medicamentoAtualizado.Descricao = "Comprimido para dor muscular";
            medicamentoAtualizado.Lote = "501e";
            medicamentoAtualizado.Validade = DateTime.Now.Date.AddMonths(6);
            medicamentoAtualizado.QuantidadeDisponivel = 300;
            medicamentoAtualizado.Fornecedor = fornecedor;

            repositorio.Editar(medicamentoAtualizado);

            Medicamento medicamentoEncontrado = repositorio.SelecionarPorNumero(medicamento.Id);

            Assert.IsNotNull(medicamentoEncontrado);
            Assert.AreEqual(medicamentoAtualizado.Id, medicamentoEncontrado.Id);
            Assert.AreEqual(medicamentoAtualizado.Nome, medicamentoEncontrado.Nome);
            Assert.AreEqual(medicamentoAtualizado.Descricao, medicamentoEncontrado.Descricao);
            Assert.AreEqual(medicamentoAtualizado.Lote, medicamentoEncontrado.Lote);
            Assert.AreEqual(medicamentoAtualizado.Validade, medicamentoEncontrado.Validade);
            Assert.AreEqual(medicamentoAtualizado.QuantidadeDisponivel, medicamentoEncontrado.QuantidadeDisponivel);
            Assert.AreEqual(medicamentoAtualizado.Fornecedor.Id, medicamentoEncontrado.Fornecedor.Id);

        }

        [TestMethod]
        public void Deve_Excluir_Medicamento()
        {

            Fornecedor fornecedor = new Fornecedor("Paulo", "123456789", "paulo@fornecedor.com", "Lages", "SC");

            var repositorioFornecedor = new RepositorioFornecedorEmBancoDeDados();

            repositorioFornecedor.Inserir(fornecedor);

            Medicamento medicamento = new Medicamento("Dipirona", "Remédio para dor de cabeça", "500f", DateTime.Now.Date, 100, fornecedor);

            var repositorio = new RepositorioMedicamentoEmBancoDados();

            repositorio.Inserir(medicamento);

            repositorio.Excluir(medicamento);

            Medicamento medicamentoEncontrado = repositorio.SelecionarPorNumero(medicamento.Id);

            Assert.IsNull(medicamentoEncontrado);

        }

        [TestMethod]
        public void Deve_selecionar_um_Medicamento()
        {

            Fornecedor fornecedor = new Fornecedor("Paulo", "123456789", "paulo@fornecedor.com", "Lages", "SC");

            var repositorioFornecedor = new RepositorioFornecedorEmBancoDeDados();

            repositorioFornecedor.Inserir(fornecedor);

            Medicamento medicamento = new Medicamento("Dipirona", "Remédio para dor de cabeça", "500f", DateTime.Now.Date, 100, fornecedor);

            var repositorio = new RepositorioMedicamentoEmBancoDados();

            repositorio.Inserir(medicamento);

            Medicamento medicamentoEncontrado = repositorio.SelecionarPorNumero(medicamento.Id);

            Assert.AreEqual("Dipirona", medicamentoEncontrado.Nome);

        }

        [TestMethod]
        public void Deve_selecionar_todos_Medicamento()
        {

            Fornecedor fornecedor = new Fornecedor("Paulo", "123456789", "paulo@fornecedor.com", "Lages", "SC");

            var repositorioFornecedor = new RepositorioFornecedorEmBancoDeDados();

            repositorioFornecedor.Inserir(fornecedor);

            Medicamento medicamento = new Medicamento("Dipirona", "Remédio para dor de cabeça", "500f", DateTime.Now.Date, 100, fornecedor);

            Medicamento medicamento2 = new Medicamento("Dipirona2", "Remédio para dor de cabeça2", "500f2", DateTime.Now.Date, 2100, fornecedor);

            var repositorio = new RepositorioMedicamentoEmBancoDados();

            repositorio.Inserir(medicamento);
            repositorio.Inserir(medicamento2);

            var medicamentos = repositorio.SelecionarTodos();

            Assert.AreEqual(2, medicamentos.Count);

            Assert.AreEqual("Dipirona", medicamentos[0].Nome);
            Assert.AreEqual("500f", medicamentos[0].Lote);
            Assert.AreEqual("Dipirona2", medicamentos[1].Nome);
            Assert.AreEqual("500f2", medicamentos[1].Lote);

        }

    }
}
