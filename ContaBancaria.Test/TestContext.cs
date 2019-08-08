using ContaBancaria.Dominio.Entidades;
using ContaBancaria.Dominio.Interfaces;
using ContaBancaria.Infra.Data.Mapping;
using ContaBancaria.Infra.Data.Repository;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContaBancaria.Test
{
    public class TestContext : DbContext
    {
        public DbSet<ContaCorrente> ContaCorrente { get; set; }
        public DbSet<Correntista> Correntista { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Utilizando um servidor SQLite local. Aqui poderíamos configurar qualquer outro banco de dados.
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseInMemoryDatabase(databaseName: "test1");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ContaCorrente>(new ContaCorrenteMap().Configure);
            modelBuilder.Entity<Correntista>(new CorrentistaMap().Configure);
        }

    }

    public class TestRepository<T> : BaseRepository<T> where T : BaseEntity
    {
        private TestContext context = new TestContext();
        new public void Insert(T obj)
        {
            context.Set<T>().Add(obj);
            context.SaveChanges();
        }

        new public void Update(T obj)
        {
            context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }

        new public void Delete(int id)
        {
            context.Set<T>().Remove(Select(id));
            context.SaveChanges();
        }

        new public IList<T> Select()
        {
            return context.Set<T>().ToList();
        }

        new public T Select(int id)
        {
            return context.Set<T>().Find(id);
        }

    }
    public class TestService<T> : IService<T> where T : BaseEntity
    {
        private TestRepository<T> repository = new TestRepository<T>();

        public T Post<V>(T obj) where V : AbstractValidator<T>
        {
            Validate(obj, Activator.CreateInstance<V>());

            repository.Insert(obj);
            return obj;
        }

        public T Put<V>(T obj) where V : AbstractValidator<T>
        {
            Validate(obj, Activator.CreateInstance<V>());

            repository.Update(obj);
            return obj;
        }

        public void Delete(int id)
        {
            if (id == 0)
                throw new ArgumentException("The id can't be zero.");

            repository.Delete(id);
        }

        public IList<T> Get() => repository.Select();

        public T Get(int id)
        {
            if (id == 0)
                throw new ArgumentException("The id can't be zero.");

            return repository.Select(id);
        }

        private void Validate(T obj, AbstractValidator<T> validator)
        {
            if (obj == null)
                throw new Exception("Registros não detectados!");

            validator.ValidateAndThrow(obj);
        }


    }

    public class ContaCorrenteTesteService : TestService<ContaCorrente>
    {
        private TestRepository<ContaCorrente> repository = new TestRepository<ContaCorrente>();

        public ContaCorrente Debitar(int id, decimal valor)
        {
            if (valor == 0)
                throw new Exception("Informe um valor para ser debitado.");

            var contaCorrente = repository.Select(id);

            if (contaCorrente == null)
                throw new Exception("Conta Corrente não encontrada.");

            contaCorrente.Debitar(valor);

            repository.Update(contaCorrente);

            return contaCorrente;
        }

        public ContaCorrente Creditar(int id, decimal valor)
        {
            if (valor == 0)
                throw new Exception("Informe um valor para ser creditado.");

            var contaCorrente = repository.Select(id);

            if (contaCorrente == null)
                throw new Exception("Conta Corrente não encontrada.");

            contaCorrente.Creditar(valor);

            repository.Update(contaCorrente);

            NotificarCOAF(valor);

            return contaCorrente;
        }

        private void NotificarCOAF(decimal valor)
        {
            if (valor > 50000)
                Console.WriteLine("\n\nNotificação COAF: Operação de crédito acima de R$50.000,00\n\n");
        }
    }
}
