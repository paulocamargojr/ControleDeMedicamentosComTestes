using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ControleMedicamentos.Dominio.Tests.ModuloMedicamento
{
    [TestClass]
    public class MedicamentoTest
    {
        [TestMethod]
        public void Deve_validar_o_nome()
        {
            Fornecedor fornecedor = new Fornecedor("Paulo", "123456789", "Paulo@fornecedor.com", "Lages", "SC");

            Medicamento medicamento = new Medicamento("", "Remédio para dor de cabeça", "500f", DateTime.Now.Date, 100, fornecedor);

            ValidadorMedicamento validador = new ValidadorMedicamento();

            var resultadoValidacao = validador.Validate(medicamento);

            Assert.AreEqual("O campo nome não pode ficar vazio", resultadoValidacao.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Deve_validar_descricao()
        {
            Fornecedor fornecedor = new Fornecedor("Paulo", "123456789", "Paulo@fornecedor.com", "Lages", "SC");

            Medicamento medicamento = new Medicamento("Dipirona", "", "500f", DateTime.Now.Date, 100, fornecedor);

            ValidadorMedicamento validador = new ValidadorMedicamento();

            var resultadoValidacao = validador.Validate(medicamento);

            Assert.AreEqual("O campo descrição não pode ficar vazio", resultadoValidacao.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Deve_validar_Lote()
        {
            Fornecedor fornecedor = new Fornecedor("Paulo", "123456789", "Paulo@fornecedor.com", "Lages", "SC");

            Medicamento medicamento = new Medicamento("Dipirona", "Remédio para dor de cabeça", "", DateTime.Now.Date, 100, fornecedor);

            ValidadorMedicamento validador = new ValidadorMedicamento();

            var resultadoValidacao = validador.Validate(medicamento);

            Assert.AreEqual("O campo Lote não pode ficar vazio", resultadoValidacao.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Deve_validar_Quantidade()
        {
            Fornecedor fornecedor = new Fornecedor("Paulo", "123456789", "Paulo@fornecedor.com", "Lages", "SC");

            Medicamento medicamento = new Medicamento("Dipirona", "Remédio para dor de cabeça", "500f", DateTime.Now.Date, -1, fornecedor);

            ValidadorMedicamento validador = new ValidadorMedicamento();

            var resultadoValidacao = validador.Validate(medicamento);

            Assert.AreEqual("Não é possível ter quantidade negativa", resultadoValidacao.Errors[0].ErrorMessage);
        }

    }
}
