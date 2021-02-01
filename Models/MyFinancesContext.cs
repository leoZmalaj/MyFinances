using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace MyFinances.Models
{
    public partial class MyFinancesContext : DbContext
    {
        public MyFinancesContext()
        {
        }
        public MyFinancesContext(DbContextOptions<MyFinancesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Budget> Budgets { get; set; }
        public virtual DbSet<Login> Logins { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-HHDM1CG\\MSSQLSERVER01;Initial Catalog=MyFinances;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Budget>(entity =>
            {
                entity.Property(e => e.AmountLeft).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Amounts).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.CameFrom).HasMaxLength(50);

                entity.Property(e => e.DateInsert)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateTake).HasColumnType("date");

                entity.Property(e => e.SpentOn).HasMaxLength(100);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
                entity.Property(e => e.AmountSpent).HasColumnType("decimal(18, 4)");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Budgets)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Budgets_Login");
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.ToTable("Login");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
