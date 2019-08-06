using System;
using System.Collections.Generic;
using System.Text;

namespace ContaBancaria.Dominio.Entidades
{
    public class ContaCorrente : BaseEntity
    {
        public decimal saldo { get; set; }
        public decimal limiteCredito { get; set; }
        public Correntista correntista { get; set; }

        public string Creditar(decimal? valor)
        {
            if (valor == null || valor == 0)
            {
                return "Favor informar um valor para creditar.";
            }
            else
            {
                this.saldo = this.saldo + valor.Value;

                var mensagemRetorno = "Crédito realizado com sucesso!";

                var retorno = NotificarCOAF(valor.Value);

                if (retorno)
                {
                    mensagemRetorno = mensagemRetorno + $"\nNotificação COAF: crédito realizado no valor de {valor.Value}";
                }

                return mensagemRetorno;
            }
        }

        private bool NotificarCOAF(decimal valor)
        {
            if (valor > 50000)
                return true;
            else 
                return false;
        }

        public string Debitar(decimal? valor)
        {
            if (valor == null || valor == 0) {
                return "Favor informar um valor para debitar.";
            }
            else if (valor <= this.saldo + this.limiteCredito)
            {
                this.saldo = this.saldo - valor.Value;
                return $"Débito realizado com sucesso! Saldo atual {this.saldo}";
            }
            else
            {
                return $"Você não possui saldo o suficiente. \nSaldo: {this.saldo}.";
            }
        }
    }
}
