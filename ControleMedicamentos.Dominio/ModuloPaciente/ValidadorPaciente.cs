using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Dominio.ModuloPaciente
{
    public class ValidadorPaciente : AbstractValidator<Paciente>
    {

        public ValidadorPaciente()
        {
            RuleFor(x => x.Nome)
                .NotNull().WithMessage("O campo nome não pode ficar vazio")
                .NotEmpty().WithMessage("O campo nome não pode ficar vazio");

            RuleFor(x => x.CartaoSUS)
                .NotNull().WithMessage("O campo cartão não pode ficar vazio")
                .NotEmpty().WithMessage("O campo cartão não pode ficar vazio")
                .MinimumLength(10).WithMessage("O cartão deve possuir pelo menos 10 digitos");
        }
    }
}
