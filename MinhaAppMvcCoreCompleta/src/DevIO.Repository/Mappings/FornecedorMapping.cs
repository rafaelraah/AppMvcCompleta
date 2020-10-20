using DevIO.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevIO.Data.Mappings
{
    public class FornecedorMapping : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Documento)
                .IsRequired()
                .HasColumnType("varchar(14)");

            /** 1 : 1 => Fornecedor : Endereco   
             * Teoricamente, o Entity já resolveria isso sozinho, mas é interessante fazer para garantir, 
             * dependendo de fatores externos que podem acabar complicando a geração automatica do entity para estes casos
             * (como, versão de banco, por exemplo, segundo o Eduardo Pires) */
            builder.HasOne(f => f.Endereco) //Fornecedor tem 1 endereço
                .WithOne(e => e.Fornecedor); //.. e o endereço tem 1 fornecedor

            builder.HasMany(f => f.Produtos).
                WithOne(p => p.Fornecedor).
                HasForeignKey(p => p.FornecedorId);

            builder.ToTable("Fornecedores");

        }
    }

}
