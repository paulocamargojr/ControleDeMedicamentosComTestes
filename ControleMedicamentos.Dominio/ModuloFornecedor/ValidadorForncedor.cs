using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Dominio.ModuloFornecedor
{
    public class ValidadorFornecedor : AbstractValidator<Fornecedor>
    {

        public ValidadorFornecedor()
        {
            RuleFor(x => x.Nome)
                .NotNull().WithMessage("O campo nome não pode ficar vazio")
                .NotEmpty().WithMessage("O campo nome não pode ficar vazio");

            RuleFor(x => x.Telefone)
                .NotNull().WithMessage("O campo telefone não pode ficar vazio")
                .NotEmpty().WithMessage("O campo telefone não pode ficar vazio")
                .MinimumLength(9).WithMessage("O telefone precisa ter pelo menos 9 digitos");

            RuleFor(x => x.Email)
                .NotNull().WithMessage("O campo email não pode ficar vazio")
                .NotEmpty().WithMessage("O campo email não pode ficar vazio")
                .EmailAddress().WithMessage("Formato inválido");

            RuleFor(x => x.Cidade)
                .NotNull().WithMessage("O campo cidade não pode ficar vazio")
                .NotEmpty().WithMessage("O campo cidade não pode ficar vazio");

            RuleFor(x => x.Estado)
                .NotNull().WithMessage("O campo estado não pode ficar vazio")
                .NotEmpty().WithMessage("O campo estado não pode ficar vazio");
        }

    }
}
