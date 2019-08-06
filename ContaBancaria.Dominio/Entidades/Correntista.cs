using System;
using System.Collections.Generic;
using System.Text;

namespace ContaBancaria.Dominio.Entidades
{
    public class Correntista : BaseEntity
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
    }
}
