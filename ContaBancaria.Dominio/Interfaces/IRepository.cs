using ContaBancaria.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContaBancaria.Dominio.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        void Insert(T obj);

        void Update(T obj);

        void Delete(int id);

        T Select(int id);

        IList<T> Select();
    }
}
