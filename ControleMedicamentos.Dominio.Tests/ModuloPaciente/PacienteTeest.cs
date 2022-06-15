using ControleMedicamentos.Dominio.ModuloPaciente;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Dominio.Tests.ModuloPaciente
{

    [TestClass]
    public class PacienteTeest
    {

        [TestMethod]
        public void Deve_validar_o_nome()
        {
            Paciente paciente = new Paciente("", "123456789");

            ValidadorPaciente validador = new ValidadorPaciente();

            var resultadoValidacao = validador.Validate(paciente);

            Assert.AreEqual("O campo nome não pode ficar vazio", resultadoValidacao.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Deve_validar_cartaoSus()
        {
            Paciente paciente = new Paciente("Paulo", "");

            Paciente paciente1 = new Paciente("Paulo", "3872");

            ValidadorPaciente validador = new ValidadorPaciente();

            var resultadoValidacao = validador.Validate(paciente);

            Assert.AreEqual("O campo cartão não pode ficar vazio", resultadoValidacao.Errors[0].ErrorMessage);

            resultadoValidacao = validador.Validate(paciente1);

            Assert.AreEqual("O cartão deve possuir pelo menos 10 digitos", resultadoValidacao.Errors[0].ErrorMessage);
        }
    }
}
