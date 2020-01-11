using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SimpleMarketAPI.Models
{
    public partial class SIMPLEMARKETContext : DbContext
    {
        public SIMPLEMARKETContext()
        {
        }

        public SIMPLEMARKETContext(DbContextOptions<SIMPLEMARKETContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CartDetail> CartDetail { get; set; }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=ANKIIMATION;Initial Catalog=SIMPLEMARKET;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartDetail>(entity =>
            {
                entity.ToTable("CART_DETAIL");

                entity.Property(e => e.CartdetailId).HasColumnName("CARTDETAIL_ID");

                entity.Property(e => e.CartdetailCount)
                    .HasColumnName("CARTDETAIL_COUNT")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ProductId)
                    .IsRequired()
                    .HasColumnName("PRODUCT_ID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("USERNAME")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CartDetail)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CART_DETA__PRODU__239E4DCF");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.CartDetail)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CART_DETA__USERN__22AA2996");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("ORDER_DETAIL");

                entity.Property(e => e.OrderdetailId).HasColumnName("ORDERDETAIL_ID");

                entity.Property(e => e.OrderId).HasColumnName("ORDER_ID");

                entity.Property(e => e.OrderdetailCount)
                    .HasColumnName("ORDERDETAIL_COUNT")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ProductId)
                    .IsRequired()
                    .HasColumnName("PRODUCT_ID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetail)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__ORDER_DET__ORDER__2C3393D0");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetail)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ORDER_DET__PRODU__2D27B809");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__ORDERS__460A946403B464E6");

                entity.ToTable("ORDERS");

                entity.Property(e => e.OrderId).HasColumnName("ORDER_ID");

                entity.Property(e => e.OrderCreatedtime)
                    .HasColumnName("ORDER_CREATEDTIME")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.OrderPaymethod)
                    .IsRequired()
                    .HasColumnName("ORDER_PAYMETHOD")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('COD')");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("USERNAME")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ORDERS__USERNAME__276EDEB3");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK__PRODUCTS__52B41763350170CF");

                entity.ToTable("PRODUCTS");

                entity.Property(e => e.ProductId)
                    .HasColumnName("PRODUCT_ID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ProdcutPrice).HasColumnName("PRODCUT_PRICE");

                entity.Property(e => e.ProductDescription)
                    .IsRequired()
                    .HasColumnName("PRODUCT_DESCRIPTION")
                    .HasDefaultValueSql("('Chua có miêu t?')");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasColumnName("PRODUCT_NAME")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK__USERS__B15BE12FE4C16B61");

                entity.ToTable("USERS");

                entity.Property(e => e.Username)
                    .HasColumnName("USERNAME")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("PASSWORD")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Roles)
                    .IsRequired()
                    .HasColumnName("ROLES")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('EMPLOYEE')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
