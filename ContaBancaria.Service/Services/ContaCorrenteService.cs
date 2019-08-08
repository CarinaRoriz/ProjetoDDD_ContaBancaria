using ContaBancaria.Dominio.Entidades;
using ContaBancaria.Infra.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContaBancaria.Service.Services
{
    public class ContaCorrenteService : BaseService<ContaCorrente>
    {
        private BaseRepository<ContaCorrente> repository = new BaseRepository<ContaCorrente>();

        public ContaCorrente Debitar(int id, decimal valor)
        {
            if (valor == 0)
                throw new Exception("Informe um valor para ser debitado.");

            var contaCorrente = repository.Select(id);

            if(contaCorrente == null)
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
