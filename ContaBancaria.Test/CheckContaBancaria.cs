using ContaBancaria.Dominio.Entidades;
using ContaBancaria.Service.Services;
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
            ContaCorrenteTesteService service = new ContaCorrenteTesteService();

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

        [Theory]
        [InlineData(0)]
        [InlineData(50000)]
        [InlineData(200)]
        public void Debitar(decimal valor)
        {
            ContaCorrenteTesteService service = new ContaCorrenteTesteService();
            TestService<Correntista> serviceCorrentista = new TestService<Correntista>();

            var correntista = new Correntista();
            correntista.Nome = "Teste";
            correntista.CPF = "11111";
            correntista.Telefone = "33333";
            correntista.Endereco = "Teste";
            serviceCorrentista.Post<CorrentistaValidator>(correntista);

            var novaConta = new ContaCorrente();
            Assert.NotNull(novaConta);
            novaConta.saldo = 1000;
            novaConta.limiteCredito = 500;
            novaConta.IdCorrentista = correntista.Id;
            service.Post<ContaCorrenteValidator>(novaConta);

            var conta = service.Get(novaConta.Id);

            // Se houver erro em validações, deve dar erro ao adicionar no banco.
            if (valor == 0 || valor > (conta.saldo + conta.limiteCredito))
            {
                Assert.Throws<System.Exception>(() => service.Debitar(novaConta.Id, valor));
                return;
            }

            var contaAtualizada = service.Debitar(novaConta.Id, valor);

            var conta2 = service.Get(novaConta.Id);

            Assert.Equal(contaAtualizada.saldo, (conta.saldo - valor));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(50000)]
        [InlineData(200)]
        public void Creditar(decimal valor)
        {
            ContaCorrenteTesteService service = new ContaCorrenteTesteService();
            TestService<Correntista> serviceCorrentista = new TestService<Correntista>();

            var correntista = new Correntista();
            correntista.Nome = "Teste";
            correntista.CPF = "11111";
            correntista.Telefone = "33333";
            correntista.Endereco = "Teste";
            serviceCorrentista.Post<CorrentistaValidator>(correntista);

            var novaConta = new ContaCorrente();
            Assert.NotNull(novaConta);
            novaConta.saldo = 1000;
            novaConta.limiteCredito = 500;
            novaConta.IdCorrentista = correntista.Id;
            service.Post<ContaCorrenteValidator>(novaConta);

            var conta = service.Get(novaConta.Id);

            // Se houver erro em validações, deve dar erro ao adicionar no banco.
            if (valor == 0)
            {
                Assert.Throws<System.Exception>(() => service.Debitar(novaConta.Id, valor));
                return;
            }

            var contaAtualizada = service.Creditar(novaConta.Id, valor);

            var conta2 = service.Get(novaConta.Id);

            Assert.Equal(contaAtualizada.saldo, (conta.saldo + valor));

        }
    }
}
