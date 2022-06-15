using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Dominio.ModuloFuncionario
{
    public  class ValidadorFuncionario : AbstractValidator<Funcionario>
    {

        public ValidadorFuncionario()
        {
            RuleFor(x => x.Nome)
                .NotNull().WithMessage("O campo nome não pode ficar vazio")
                .NotEmpty().WithMessage("O campo nome não pode ficar vazio");

            RuleFor(x => x.Login)
                .NotNull().WithMessage("O campo login não pode ficar vazio")
                .NotEmpty().WithMessage("O campo login não pode ficar vazio");

            RuleFor(x => x.Senha)
                .NotNull().WithMessage("O campo senha não pode ficar vazio")
                .NotEmpty().WithMessage("O campo senha não pode ficar vazio");
        }

    }
}
