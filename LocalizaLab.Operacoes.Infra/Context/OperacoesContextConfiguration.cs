using LocalizaLab.Operacoes.Domain.Entities;
using LocalizaLab.Operacoes.Domain.Entities.Clientes;
using LocalizaLab.Operacoes.Domain.Entities.Contratos;
using LocalizaLab.Operacoes.Domain.Entities.Usuarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizaLab.Operacoes.Infra.Context
{
    public class ContratoMapping : IEntityTypeConfiguration<Contrato>
    {
        public void Configure(EntityTypeBuilder<Contrato> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Codigo).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Agencia).HasMaxLength(10).HasColumnType("varchar(10)").IsRequired();
            builder.Property(x => x.ValorTotal).HasColumnType("decimal(9,2)").IsRequired();
            builder.Property(x => x.DataAberturaContreato).HasColumnType("datetime").IsRequired();

            builder.HasOne(c => c.Cliente).WithMany(p => p.Contratos)
               .HasForeignKey(p => p.ClienteId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.DadosReserva).WithOne(p => p.Contrato)
                .HasForeignKey<DadosReserva>(p => p.ContratoId).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.DadosPagamentos).WithOne(p => p.Contrato)
                .HasForeignKey<DadosPagamentos>(p => p.ContratoId).OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.DadosItemContrato).WithOne(p => p.Contrato)
                .HasForeignKey(p => p.ContratoId).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.DadosDevolucao).WithOne(p => p.Contrato)
                .HasForeignKey<DadosDevolucao>(p => p.ContratoId).OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("tbContrato");
        }
    }
    public class ReservaMapping : IEntityTypeConfiguration<Reserva>
    {
        public void Configure(EntityTypeBuilder<Reserva> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Agencia).HasMaxLength(10).HasColumnType("varchar(10)").IsRequired();
            builder.Property(x => x.CodigoReserva).HasMaxLength(100).HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.Grupo).HasColumnType("int").IsRequired();
            builder.Property(x => x.DataInicioReserva).HasColumnType("datetime").IsRequired();
            builder.Property(x => x.DataFimReserva).HasColumnType("datetime").IsRequired();
            builder.Property(x => x.Diarias).HasColumnType("int").IsRequired();
            builder.Property(x => x.ValorSimulado).HasColumnType("decimal(10,2)").IsRequired();
            builder.Property(x => x.ValorAdicionarGrupo).HasColumnType("decimal(10,2)").IsRequired();

            builder.HasOne(c => c.Cliente).WithMany(p => p.Reservas)
               .HasForeignKey(p => p.ClienteId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.Veiculos).WithOne(p => p.Reserva)
              .HasForeignKey<Reserva>(p => p.VeiculosId).OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("tbReserva");
        }
    }
    public class DadosReservaMapping : IEntityTypeConfiguration<DadosReserva>
    {
        public void Configure(EntityTypeBuilder<DadosReserva> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.CodigoReserva).HasMaxLength(100).HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.Grupo).HasColumnType("int").IsRequired();
            builder.Property(x => x.DataInicioReserva).HasColumnType("datetime").IsRequired();
            builder.Property(x => x.DataFinalReserva).HasColumnType("datetime").IsRequired();
            builder.Property(x => x.DiariasEmHoras).HasColumnType("int").IsRequired();
            builder.Property(x => x.Diarias).HasColumnType("int").IsRequired();
            builder.Property(x => x.ValorReserva).HasColumnType("decimal(10,2)").IsRequired();
            builder.Property(x => x.ValorPorHora).HasColumnType("decimal(10,2)").IsRequired();

            builder.HasOne(c => c.Contrato).WithOne(p => p.DadosReserva)
              .HasForeignKey<DadosReserva>(p => p.ContratoId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.Veiculo).WithOne(p => p.DadosReserva)
                .HasForeignKey<DadosReserva>(p => p.VeiculosId).OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("tbDadosReserva");
        }
    }
    public class DadosPagamentosMapping : IEntityTypeConfiguration<DadosPagamentos>
    {
        public void Configure(EntityTypeBuilder<DadosPagamentos> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Bandeira).HasMaxLength(100).HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.Status);
            builder.Property(x => x.NumeroCartao).HasMaxLength(100).HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.DataExpiracao).HasMaxLength(5).HasColumnType("varchar(5)").IsRequired();
            builder.Property(x => x.CVV).HasMaxLength(3).HasColumnType("varchar(3)").IsRequired();
            builder.Property(x => x.DataPagamento).HasColumnType("datetime").IsRequired();
            builder.Property(x => x.Valor).HasColumnType("decimal(10,2)").IsRequired();

            builder.HasOne(c => c.Contrato).WithOne(p => p.DadosPagamentos)
              .HasForeignKey<DadosPagamentos>(p => p.ContratoId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.Pagamento).WithOne(p => p.DadosPagamentos)
                .HasForeignKey<DadosPagamentos>(p => p.PagamentoId).OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("tbDadosPagamento");
        }
    }
    public class DadosItemContratoMapping : IEntityTypeConfiguration<DadosItemContrato>
    {
        public void Configure(EntityTypeBuilder<DadosItemContrato> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Item).HasColumnType("int").IsRequired();
            builder.Property(x => x.ValorItem).HasColumnType("decimal(10,2)").IsRequired();

            builder.ToTable("tbDadosItemContrato");
        }
    }
    public class DadosDevolucaoMapping : IEntityTypeConfiguration<DadosDevolucao>
    {
        public void Configure(EntityTypeBuilder<DadosDevolucao> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.CarroLimpo).HasColumnType("bit");
            builder.Property(x => x.TanqueCheio).HasColumnType("bit");
            builder.Property(x => x.Amassado).HasColumnType("bit");
            builder.Property(x => x.Arranhado).HasColumnType("bit");
            builder.Property(x => x.CarroLimpo).HasColumnType("bit");
            builder.Property(x => x.PorcentagemTotalAdiconada).HasColumnType("decimal(10,2)").IsRequired();
            builder.Property(x => x.ValorContrato).HasColumnType("decimal(10,2)").IsRequired();

            builder.ToTable("tbDadosDevolucao");
        }
    }
    public class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {

            builder.HasKey(c => c.Id);
            builder.Property(x => x.Nome).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Identidade).HasMaxLength(25).HasColumnType("varchar(25)").IsRequired();
            builder.Property(x => x.Email).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.DataNascimento).HasColumnType("datetime").IsRequired();
            builder.Property(x => x.CPF.Numero).HasMaxLength(50).HasColumnType("varchar(50)").IsRequired();
            builder.Property(x => x.CPF.Estado).HasMaxLength(50).HasColumnType("varchar(50)").IsRequired();
            builder.OwnsOne(x => x.CPF);

            builder.HasOne(c => c.Endereco).WithOne(p => p.Cliente)
             .HasForeignKey<Endereco>(p => p.ClienteId).OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("tbCliente");
        }
    }

    public class EnderecoMapping : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {

            builder.HasKey(c => c.Id);
            builder.Property(x => x.CEP).HasMaxLength(15).HasColumnType("varchar(15)").IsRequired();
            builder.Property(x => x.Cidade).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Complemento).HasMaxLength(10).HasColumnType("varchar(10)").IsRequired();
            builder.Property(x => x.Estado).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Logradouro).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Numero).HasMaxLength(50).HasColumnType("varchar(50)").IsRequired();

            builder.ToTable("tbEndereco");
        }
    }

    public class VeiculosMapping : IEntityTypeConfiguration<Veiculos>
    {
        public void Configure(EntityTypeBuilder<Veiculos> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Placa).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Ano).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.ValorHora).HasColumnType("decimal(10,2)").IsRequired();
            builder.Property(x => x.Combustivel).HasColumnType("int").IsRequired();
            builder.Property(x => x.LimitePortaMalas).HasMaxLength(50).HasColumnType("varchar(50)").IsRequired();
            builder.Property(x => x.Categoria).HasColumnType("int").IsRequired();

            builder.HasOne(c => c.Modelo).WithMany(p => p.Veiculos)
             .HasForeignKey(p => p.ModeloId).OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("tbVeiculos");
        }
    }

    public class ModeloMapping : IEntityTypeConfiguration<Modelo>
    {
        public void Configure(EntityTypeBuilder<Modelo> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Nome).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();

            builder.ToTable("tbModelo");
        }
    }

    public class MarcaMapping : IEntityTypeConfiguration<Marca>
    {
        public void Configure(EntityTypeBuilder<Marca> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Nome).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Pais).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();

            builder.HasMany(c => c.Modelos).WithOne(p => p.Marca)
                .HasForeignKey(p => p.MarcaId).OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("tbMarca");
        }
    }
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Login).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Nome).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Senha).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.CPF).HasMaxLength(25).HasColumnType("varchar(25)").IsRequired();
            builder.Property(x => x.Perfil).HasColumnType("int").IsRequired();
            builder.OwnsOne(x => x.CPF);

            builder.HasOne(c => c.Cliente).WithOne(p => p.Usuario).HasConstraintName("FK_Cliente_Usuario_Cliente")
                .HasForeignKey<Usuario>(p => p.ClienteId).IsRequired(false).OnDelete(DeleteBehavior.NoAction);


            builder.ToTable("tbUsuarios");
        }
    }
    public class OperadorMapping : IEntityTypeConfiguration<Operador>
    {
        public void Configure(EntityTypeBuilder<Operador> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Matricula).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Nome).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Senha).HasMaxLength(250).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Perfil).HasColumnType("int").IsRequired();

            builder.ToTable("tbOperador");
        }
    }
}
