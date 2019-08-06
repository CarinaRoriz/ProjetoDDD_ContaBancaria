using ContaBancaria.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContaBancaria.Infra.Data.Mapping
{
    public class ContaCorrenteMap : IEntityTypeConfiguration<ContaCorrente>
    {
        public void Configure(EntityTypeBuilder<ContaCorrente> builder)
        {
            builder.ToTable("ContaCorrente");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.saldo)
              .IsRequired()
              .HasColumnName("Saldo");

            builder.Property(c => c.limiteCredito)
              .IsRequired()
              .HasColumnName("LimiteCredito");

            builder.Property(c => c.IdCorrentista)
              .IsRequired()
              .HasColumnName("IdCorrentista");

            builder.HasOne(c => c.correntista)
            .WithMany(c => c.ContasCorrentes)
            .HasForeignKey(x => x.IdCorrentista)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
