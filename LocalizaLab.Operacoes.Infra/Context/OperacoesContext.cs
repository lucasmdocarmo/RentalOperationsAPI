using Flunt.Notifications;
using LocalizaLab.Operacoes.Domain.Entities;
using LocalizaLab.Operacoes.Domain.Entities.Clientes;
using LocalizaLab.Operacoes.Domain.Entities.Contratos;
using LocalizaLab.Operacoes.Domain.Entities.Usuarios;
using LocalizaLab.Operacoes.Domain.Shared.Contracts;
using LocalizaLab.Operacoes.Domain.Shared.Entities;
using LocalizaLab.Operacoes.Domain.ValueObjects.Clientes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LocalizaLab.Operacoes.Infra.Context
{
    public class OperacoesContext : DbContext, IUnitOfWork
    {
        public OperacoesContext(DbContextOptions<OperacoesContext> options) : base(options) { }
        public virtual DbSet<Contrato> Contrato { get; set; }
        public virtual DbSet<Reserva> Reserva { get; set; }
        public virtual DbSet<DadosReserva> DadosReserva { get; set; }
        public virtual DbSet<DadosPagamentos> DadosPagamentos { get; set; }
        public virtual DbSet<DadosItemContrato> DadosItemContrato { get; set; }
        public virtual DbSet<DadosDevolucao> DadosDevolucao { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Endereco> Endereco { get; set; }
        public virtual DbSet<Veiculos> Veiculos { get; set; }
        public virtual DbSet<Modelo> Modelo { get; set; }
        public virtual DbSet<Marca> Marca { get; set; } 
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Operador> Operador { get; set; }

        public void Rollback() => base.Database.RollbackTransaction();
        public void Begin() => base.Database.BeginTransaction();
        public async Task<bool> Commit() => await base.SaveChangesAsync() > 0;

        public bool CheckDatabaseStatus()
        {
            if (!base.Database.CanConnect())
            {
                return false;
            }
            return true;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Notification>();
            modelBuilder.Entity<Cliente>().OwnsOne(p => p.CPF);
            modelBuilder.Entity<Usuario>().OwnsOne(p => p.CPF);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbContext).Assembly);
        }
    }
}
