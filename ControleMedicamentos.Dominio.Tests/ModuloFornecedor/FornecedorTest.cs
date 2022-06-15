using ControleMedicamentos.Dominio.ModuloFornecedor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Dominio.Tests.ModuloFornecedor
{
    [TestClass]
    public class FornecedorTest
    {

        [TestMethod]
        public void Deve_validar_o_nome()
        {
            Fornecedor fornecedor = new Fornecedor("", "123456789", "Paulo@fornecedor.com", "Lages", "SC");

            ValidadorFornecedor validador = new ValidadorFornecedor();

            var resultadoValidacao = validador.Validate(fornecedor);

            Assert.AreEqual("O campo nome não pode ficar vazio", resultadoValidacao.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Deve_validar_o_telefone()
        {
            Fornecedor fornecedor = new Fornecedor("Paulo", "", "Paulo@fornecedor.com", "Lages", "SC");

            Fornecedor fornecedor1 = new Fornecedor("Paulo", "123", "Paulo@fornecedor.com", "Lages", "SC");

            ValidadorFornecedor validador = new ValidadorFornecedor();

            var resultadoValidacao = validador.Validate(fornecedor);

            Assert.AreEqual("O campo telefone não pode ficar vazio", resultadoValidacao.Errors[0].ErrorMessage);

            resultadoValidacao = validador.Validate(fornecedor1);

            Assert.AreEqual("O telefone precisa ter pelo menos 9 digitos", resultadoValidacao.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Deve_validar_o_email()
        {
            Fornecedor fornecedor = new Fornecedor("Paulo", "123456789", "", "Lages", "SC");

            Fornecedor fornecedor1 = new Fornecedor("Paulo", "123456789", "pauloteste", "Lages", "SC");

            ValidadorFornecedor validador = new ValidadorFornecedor();

            var resultadoValidacao = validador.Validate(fornecedor);

            Assert.AreEqual("O campo email não pode ficar vazio", resultadoValidacao.Errors[0].ErrorMessage);

            resultadoValidacao = validador.Validate(fornecedor1);

            Assert.AreEqual("Formato inválido", resultadoValidacao.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Deve_validar_a_cidade()
        {
            Fornecedor fornecedor = new Fornecedor("Paulo", "123456789", "Paulo@fornecedor.com", "", "SC");

            ValidadorFornecedor validador = new ValidadorFornecedor();

            var resultadoValidacao = validador.Validate(fornecedor);

            Assert.AreEqual("O campo cidade não pode ficar vazio", resultadoValidacao.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Deve_validar_o_estado()
        {
            Fornecedor fornecedor = new Fornecedor("Paulo", "123456789", "Paulo@fornecedor.com", "Lages", "");

            ValidadorFornecedor validador = new ValidadorFornecedor();

            var resultadoValidacao = validador.Validate(fornecedor);

            Assert.AreEqual("O campo estado não pode ficar vazio", resultadoValidacao.Errors[0].ErrorMessage);
        }
    }
}
