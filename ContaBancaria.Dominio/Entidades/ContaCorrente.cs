using System;
using System.Collections.Generic;
using System.Text;

namespace ContaBancaria.Dominio.Entidades
{
    public class ContaCorrente : BaseEntity
    {
        public decimal saldo { get; set; }
        public decimal limiteCredito { get; set; }
        public int IdCorrentista { get; set; }
        public virtual Correntista correntista { get; set; }

        public void Creditar(decimal? valor)
        {
            if (valor == null || valor == 0)
            {
                throw new Exception("Favor informar um valor para creditar.");
            }
            else
            {
                this.saldo = this.saldo + valor.Value;
            }
        }

        public void Debitar(decimal? valor)
        {
            if (valor == null || valor == 0)
            {
                throw new Exception("Favor informar um valor para debitar.");
            }
            else if (valor <= this.saldo + this.limiteCredito)
            {
                this.saldo = this.saldo - valor.Value;
            }
            else
            {
                throw new Exception($"Você não possui saldo o suficiente. \nSaldo: {this.saldo}.");
            }
        }
    }
}
