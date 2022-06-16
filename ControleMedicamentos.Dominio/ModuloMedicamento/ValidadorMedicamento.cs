using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Dominio.ModuloMedicamento
{
    public class ValidadorMedicamento : AbstractValidator<Medicamento>
    {

        public ValidadorMedicamento()
        {
            RuleFor(x => x.Nome)
                .NotNull().WithMessage("O campo nome não pode ficar vazio")
                .NotEmpty().WithMessage("O campo nome não pode ficar vazio");

            RuleFor(x => x.Descricao)
                .NotNull().WithMessage("O campo descrição não pode ficar vazio")
                .NotEmpty().WithMessage("O campo descrição não pode ficar vazio");

            RuleFor(x => x.Lote)
                .NotNull().WithMessage("O campo Lote não pode ficar vazio")
                .NotEmpty().WithMessage("O campo Lote não pode ficar vazio");

            RuleFor(x => x.Validade)
                .NotNull().WithMessage("O campo validade não pode ficar vazio")
                .NotEmpty().WithMessage("O campo validade não pode ficar vazio");

            RuleFor(x => x.QuantidadeDisponivel)
                .NotNull().WithMessage("O campo quantidade não pode ficar vazio")
                .NotEmpty().WithMessage("O campo quantidade não pode ficar vazio")
                .GreaterThan(-1).WithMessage("Não é possível ter quantidade negativa");

        }
    }
}
