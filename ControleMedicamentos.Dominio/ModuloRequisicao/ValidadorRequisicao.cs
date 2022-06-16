using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Dominio.ModuloRequisicao
{
    public class ValidadorRequisicao : AbstractValidator<Requisicao>
    {

        public ValidadorRequisicao()
        {
            RuleFor(x => x.Medicamento)
                .NotNull().WithMessage("O campo medicamento não pode ficar vazio")
                .NotEmpty().WithMessage("O campo medicamento não pode ficar vazio");

            RuleFor(x => x.Paciente.Nome)
                .NotNull().WithMessage("O campo paciente não pode ficar vazio")
                .NotEmpty().WithMessage("O campo paciente não pode ficar vazio");

            RuleFor(x => x.Paciente.CartaoSUS)
                .NotNull().WithMessage("O campo cartao não pode ficar vazio")
                .NotEmpty().WithMessage("O campo cartao não pode ficar vazio")
                .MinimumLength(10).WithMessage("O cartão deve possuir pelo menos 10 digitos");

            RuleFor(x => x.QtdMedicamento)
                .NotNull().WithMessage("O Campo quantidade não pode ficar vazio")
                .NotEmpty().WithMessage("O campo quantidade não pode ficar vazio");
                //.GreaterThan(-1).WithMessage("A quantidade não pode ser negativa");

            RuleFor(x => x.Funcionario.Nome)
                .NotNull().WithMessage("O campo nome não pode ficar vazio")
                .NotEmpty().WithMessage("O campo nome não pode ficar vazio");

        }
    }
}
