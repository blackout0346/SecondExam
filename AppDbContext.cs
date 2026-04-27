
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.Data.Sqlite;
using secondExam.Module;
namespace secondExam
{
    class AppDbContext : DbContext
    {
        public DbSet<Products> Products { get; set; }
        public DbSet<Partner> Partner { get; set; }
        public DbSet<TypeProduct> TypeProduct { get; set; }

        public DbSet<PartnerProducts> PartnerProducts { get; set; }
        public DbSet<TypeMaterial> TypeMaterial { get; set; }
        public DbSet<TypePartner> TypePartner { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = SecondExamdb.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(k => k.Id);
                entity.HasOne(p => p.typeProduct)
                .WithMany(p => p.Products)
                .HasForeignKey(f => f.TypeProductsId);
            });

            modelBuilder.Entity<Partner>(entity =>
            {
                entity.HasKey(k => k.Id);
                entity.HasOne(p => p.typePartners)
               .WithMany(p => p.Partners)
               .HasForeignKey(f => f.TypeNamePartnerId);
            });
            modelBuilder.Entity<TypeMaterial>(entity =>
            {
                entity.HasKey(k => k.Id);

            });
            modelBuilder.Entity<PartnerProducts>(entity =>
            {
                entity.HasKey(k => k.Id);
                entity.HasOne(p => p.Partner)
                   .WithMany(p => p.PartnerProducts)
                   .HasForeignKey(f => f.PartnersId);
            });


            modelBuilder.Entity<PartnerProducts>(entity =>
            {
                entity.HasKey(k => k.Id);
                entity.HasOne(p => p.Product)
                  .WithMany(p => p.PartnerProducts)
                  .HasForeignKey(f => f.ProductsId);
            });



        }

    }

}