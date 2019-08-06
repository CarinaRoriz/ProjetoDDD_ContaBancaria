using ContaBancaria.Dominio.Entidades;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContaBancaria.Service.Validators
{
    public class ContaCorrenteValidator : AbstractValidator<ContaCorrente>
    {
        public ContaCorrenteValidator()
        {
            RuleFor(c => c)
                    .NotNull()
                    .OnAnyFailure(x =>
                    {
                        throw new ArgumentNullException("Can't found the object.");
                    });

            RuleFor(c => c.saldo)
                .NotEmpty().WithMessage("É necessário informar o saldo.")
                .NotNull().WithMessage("É necessário informar o saldo.");

            RuleFor(c => c.limiteCredito)
                .NotEmpty().WithMessage("É necessário informar o limite de crédito.")
                .NotNull().WithMessage("É necessário informar o limite de crédito.");

            RuleFor(c => c.correntista)
                .NotEmpty().WithMessage("É necessário informar o correntista.")
                .NotNull().WithMessage("É necessário informar o correntista.");
        }
    }
}
