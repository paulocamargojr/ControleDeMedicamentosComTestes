using ControleMedicamentos.Dominio.ModuloFuncionario;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Dominio.Tests.ModuloFuncionario
{

    [TestClass]
    public class FuncionarioTest
    {

        [TestMethod]
        public void Deve_validar_o_nome()
        {
            Funcionario funcionario = new Funcionario("", "Paulo@funcionario.com", "123");

            ValidadorFuncionario validador = new ValidadorFuncionario();

            var resultadoValidacao = validador.Validate(funcionario);

            Assert.AreEqual("O campo nome não pode ficar vazio", resultadoValidacao.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Deve_validar_o_login()
        {
            Funcionario funcionario = new Funcionario("Paulo", "", "123");

            ValidadorFuncionario validador = new ValidadorFuncionario();

            var resultadoValidacao = validador.Validate(funcionario);

            Assert.AreEqual("O campo login não pode ficar vazio", resultadoValidacao.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Deve_validar_senha()
        {
            Funcionario funcionario = new Funcionario("Paulo", "paulo.c", "");

            ValidadorFuncionario validador = new ValidadorFuncionario();

            var resultadoValidacao = validador.Validate(funcionario);

            Assert.AreEqual("O campo senha não pode ficar vazio", resultadoValidacao.Errors[0].ErrorMessage);
        }

    }
}
