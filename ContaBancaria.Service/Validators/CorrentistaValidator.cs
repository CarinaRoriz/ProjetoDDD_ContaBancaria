using ContaBancaria.Dominio.Entidades;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContaBancaria.Service.Validators
{
    public class CorrentistaValidator : AbstractValidator<Correntista>
    {
        public CorrentistaValidator()
        {
            RuleFor(c => c)
                    .NotNull()
                    .OnAnyFailure(x =>
                    {
                        throw new ArgumentNullException("Can't found the object.");
                    });

            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("É necessário informar o nome.")
                .NotNull().WithMessage("É necessário informar o nome.");

            RuleFor(c => c.CPF)
                .NotEmpty().WithMessage("É necessário informar o CPF.")
                .NotNull().WithMessage("É necessário informar o CPF.");

            RuleFor(c => c.Telefone)
                .NotEmpty().WithMessage("É necessário informar o telefone.")
                .NotNull().WithMessage("É necessário informar o telefone.");

            RuleFor(c => c.Endereco)
                .NotEmpty().WithMessage("É necessário informar o endereço.")
                .NotNull().WithMessage("É necessário informar o endereço.");
        }
    }
}
