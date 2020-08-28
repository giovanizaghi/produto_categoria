using Dominio.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Context
{
    public partial class DataModel : DbContext
    {
        public DataModel()
        {
        }

        public DataModel(DbContextOptions<DataModel> options)
          : base(options)
        {
        }

        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Produto> Produto { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data source=sql5063.site4now.net;initial catalog=DB_A37A16_citel;persist security info=True;user id=DB_A37A16_citel_admin;password=Canada2020;MultipleActiveResultSets=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__Categori__3214EC07AA1BD0DC");
            });

            modelBuilder.Entity<Produto>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__Produto__3214EC0758977B74");

                entity.HasOne(e => e.Categoria)
                    .WithMany(e => e.Produto)
                    .HasForeignKey(e => e.IdCategoria);

            });
        }
    }
}
