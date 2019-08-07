using ContaBancaria.Dominio.Entidades;
using ContaBancaria.Service.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ContaBancaria.Test
{
    public class CheckContaBancaria
    {
        [Theory]
        [InlineData(1000, 100, 1)]
        [InlineData(300, 1000, 1)]
        [InlineData(-100, 300, 1)]
        public void CanCreateAndSave(decimal saldo, decimal limiteCredito, int idCorrentista)
        {
            TestService<ContaCorrente> service = new TestService<ContaCorrente>();

            var conta = new ContaCorrente();
            Assert.NotNull(conta);
            conta.saldo = saldo;
            conta.limiteCredito = limiteCredito;
            conta.IdCorrentista = idCorrentista;

            // Se houver erro em validações, deve dar erro ao adicionar no banco.
            if (saldo < 0)
            {
                Assert.Throws<FluentValidation.ValidationException>(() => service.Post<ContaCorrenteValidator>(conta));
                return;
            }
            // Agora criando, resgatando, comparando e deletando.
            service.Post<ContaCorrenteValidator>(conta);
            Assert.True(conta.Id > -1);
            var conta2 = service.Get(conta.Id);

            Assert.Equal(conta2.saldo, conta.saldo);
            Assert.Equal(conta2.limiteCredito, conta.limiteCredito);
            Assert.Equal(conta2.IdCorrentista, conta.IdCorrentista);

            var id = conta.Id;
            service.Delete(conta.Id);

            // Tentando deletar novamente e verificando se Exception é gerada.
            Assert.Throws<System.ArgumentNullException>(() => service.Delete(id));
        }
    }
}
