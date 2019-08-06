using System;
using System.Collections.Generic;
using System.Text;

namespace ContaBancaria.Dominio.Entidades
{
    public abstract class BaseEntity
    {
        public virtual int Id { get; set; }
    }
}
