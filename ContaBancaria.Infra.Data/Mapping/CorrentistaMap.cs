using ContaBancaria.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContaBancaria.Infra.Data.Mapping
{
    public class CorrentistaMap : IEntityTypeConfiguration<Correntista>
    {
        public void Configure(EntityTypeBuilder<Correntista> builder)
        {
            builder.ToTable("ContaCorrente");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
              .IsRequired()
              .HasColumnName("Nome");

            builder.Property(c => c.CPF)
              .IsRequired()
              .HasColumnName("Cpf");

            builder.Property(c => c.Telefone)
              .IsRequired()
              .HasColumnName("Telefone");

            builder.Property(c => c.Endereco)
              .IsRequired()
              .HasColumnName("Endereco");
        }
    }
}
