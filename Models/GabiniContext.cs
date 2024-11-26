using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace crudd.Models;

public partial class GabiniContext : DbContext
{
#pragma warning disable CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere adicionar o modificador "obrigatório" ou declarar como anulável.
    public GabiniContext()
#pragma warning restore CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere adicionar o modificador "obrigatório" ou declarar como anulável.
    {
    }

#pragma warning disable CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere adicionar o modificador "obrigatório" ou declarar como anulável.
    public GabiniContext(DbContextOptions<GabiniContext> options)
#pragma warning restore CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere adicionar o modificador "obrigatório" ou declarar como anulável.
        : base(options)
    {
    }

    public virtual DbSet<Endereco> Enderecos { get; set; }

    public virtual DbSet<Produto> Produtos { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=.;Database=Gabini;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Endereco>(entity =>
        {
            entity.HasKey(e => e.IdEndereco).HasName("PK__endereco__324B959E43154A40");

            entity.ToTable("endereco");

            entity.Property(e => e.IdEndereco).HasColumnName("id_endereco");
            entity.Property(e => e.Bairro)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("bairro");
            entity.Property(e => e.Cep)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("cep");
            entity.Property(e => e.Cidade)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cidade");
            entity.Property(e => e.Complemento)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("complemento");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.Notas)
                .IsUnicode(false)
                .HasColumnName("notas");
            entity.Property(e => e.Numero)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("numero");
            entity.Property(e => e.Pais)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("pais");
            entity.Property(e => e.Rua)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("rua");
            entity.Property(e => e.TipoResidencia)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo_residencia");

            entity.HasOne(d => d.User).WithMany(p => p.Enderecos)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Endereco_User");
        });

        modelBuilder.Entity<Produto>(entity =>
        {
            entity.HasKey(e => e.ProdutoId).HasName("PK__produtos__9C8800E3FBF083B6");

            entity.ToTable("produtos");

            entity.Property(e => e.DataCadastro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Descricao).HasMaxLength(255);
            entity.Property(e => e.Estoque).HasDefaultValue(0);
            entity.Property(e => e.ImagemUrl).HasMaxLength(255);
            entity.Property(e => e.Marca).HasMaxLength(50);
            entity.Property(e => e.Nome_produto)
                .HasMaxLength(100)
                .HasColumnName("Nome_produto");
            entity.Property(e => e.Preco).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3213E83F4A3E27B5");

            entity.ToTable("users");

            entity.HasIndex(e => e.Senha, "UQ__users__D8D98E82EFBC0932").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(100)
                .HasColumnName("firstname");
            entity.Property(e => e.Gender)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.Nacionalidade)
                .HasMaxLength(100)
                .HasColumnName("nacionalidade");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.RegisterDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("register_date");
            entity.Property(e => e.SecurityNumber)
                .HasMaxLength(30)
                .HasColumnName("security_number");
            entity.Property(e => e.Senha)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("senha");
            entity.Property(e => e.Surname)
                .HasMaxLength(100)
                .HasColumnName("surname");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
