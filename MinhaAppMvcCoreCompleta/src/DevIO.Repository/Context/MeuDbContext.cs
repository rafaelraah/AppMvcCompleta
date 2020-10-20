using DevIO.Business.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevIO.Data.Context
{
    public class MeuDbContext : DbContext
    {
        public MeuDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Produto> Produtos { get; set; }

        public DbSet<Endereco> Enderecos{ get; set; }

        public DbSet<Fornecedor> Fornecedores { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Se esquecer de mapear, ele entra como nvarchar(max), o que não é legal. Solução:
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e=>e.GetProperties()
                .Where(p => p.ClrType == typeof(string))))
            {
                //Versão antiga:
                property.Relational().ColumnType = "varchar(100)";

                //Versao nova:
                //property.SetColumnType("varchar(100)");
            }

            //Irá buscar cada class que implementa IEntityTypeConfiguration, utilizando reflection (apesar de não recomendado usar, aqui irá usar uma vez só, não comprometendo a aplicação)
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeuDbContext).Assembly);

            //Desabilitar o Cascade Delete
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }

            base.OnModelCreating(modelBuilder);
        }

    }
}
