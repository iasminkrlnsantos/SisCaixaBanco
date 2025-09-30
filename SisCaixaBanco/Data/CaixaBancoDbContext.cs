using System.Drawing;
using Microsoft.EntityFrameworkCore;
using SisCaixaBanco.Common.Enums;
using SisCaixaBanco.Models;

namespace SisCaixaBanco.Data
{
    public class CaixaBancoDbContext : DbContext
    {
        public CaixaBancoDbContext(DbContextOptions<CaixaBancoDbContext> options) : base(options) { }

        public DbSet<Conta> Conta { get; set; }
        public DbSet<Transferencia> Transferencia { get; set; }
        public DbSet<ContaLog> ContaLog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Conta>()
                .HasIndex(c => c.Documento)
                .IsUnique();
            modelBuilder.Entity<Conta>()
                 .Property(c => c.Status)
                 .HasConversion(
                     v => v == StatusConta.Ativa, 
                     v => v ? StatusConta.Ativa : StatusConta.Inativa 
                 );


            modelBuilder.Entity<Transferencia>()
                .HasOne(t => t.ContaOrigem)
                .WithMany(c=> c.TransferenciasOrigem)
                .HasForeignKey(t => t.IdContaOrigem)
                .HasConstraintName("FK_Transferencia_ContaOrigem")
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transferencia>()
                .HasOne(t => t.ContaDestino)
                .WithMany(c => c.TransferenciasDestino)
                .HasForeignKey(t => t.IdContaDestino)
                .HasConstraintName("FK_Transferencia_ContaDestino")
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ContaLog>()
                .HasOne(cl => cl.ContaBancaria)
                .WithMany()
                .HasForeignKey(cl => cl.IdContaBancaria);

        }


    }
}
