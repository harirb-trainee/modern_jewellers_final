using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MJ;

public partial class MJDbContext : DbContext
{
    public MJDbContext()
    {
    }

    public MJDbContext(DbContextOptions<MJDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Color> Colors { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Karigar> Karigars { get; set; }

    public virtual DbSet<LoginLog> LoginLogs { get; set; }

    public virtual DbSet<PolishType> PolishTypes { get; set; }

    public virtual DbSet<ProductHead> ProductHeads { get; set; }

    public virtual DbSet<ProductHeadType> ProductHeadTypes { get; set; }

    public virtual DbSet<StoneType> StoneTypes { get; set; }

    public virtual DbSet<FittingType> FittingTypes { get; set; }

    public virtual DbSet<Pattern> Patterns { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Connection string moved to appsettings.json
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Color>(entity =>
        {
            entity.HasKey(e => e.ColorId).HasName("color_pkey");

            entity.ToTable("color");

            entity.Property(e => e.ColorId).HasColumnName("color_id");
            entity.Property(e => e.HexCode)
                .HasMaxLength(7)
                .HasColumnName("hex_code");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("item_pkey");

            entity.ToTable("item");

            entity.Property(e => e.ItemId)
                .HasMaxLength(50)
                .HasColumnName("item_id");
            entity.Property(e => e.Color).HasColumnName("color");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.ItemCategory).HasColumnName("item_category");
            entity.Property(e => e.ItemName)
                .HasColumnType("character varying")
                .HasColumnName("item_name");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("description");
            entity.Property(e => e.ItemPolish).HasColumnName("item_polish");
            entity.Property(e => e.ItemPrice).HasColumnName("item_price");
            entity.Property(e => e.ItemQuantity).HasColumnName("item_quantity");
            entity.Property(e => e.Threshold).HasColumnName("threshold");
            entity.Property(e => e.ItemSubCategory).HasColumnName("item_subCategory");
            entity.Property(e => e.PhotoUrl).HasColumnName("photo_url");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.StoneId).HasColumnName("stone_id");
            entity.Property(e => e.FittingId).HasColumnName("fitting_id");
            entity.Property(e => e.PatternId).HasColumnName("pattern_id");

            entity.HasOne(d => d.ColorNavigation).WithMany(p => p.Items)
                .HasForeignKey(d => d.Color)
                .HasConstraintName("color_fkey");

            entity.HasOne(d => d.ItemCategoryNavigation).WithMany(p => p.Items)
                .HasForeignKey(d => d.ItemCategory)
                .HasConstraintName("item_categoryfkey");

            entity.HasOne(d => d.ItemPolishNavigation).WithMany(p => p.Items)
                .HasForeignKey(d => d.ItemPolish)
                .HasConstraintName("polish_fkey");

            entity.HasOne(d => d.ItemSubCategoryNavigation).WithMany(p => p.Items)
                .HasForeignKey(d => d.ItemSubCategory)
                .HasConstraintName("item_subCategoryfKey");

            entity.HasOne(d => d.StoneNavigation).WithMany(p => p.Items)
                .HasForeignKey(d => d.StoneId)
                .HasConstraintName("stone_fkey");

            entity.HasOne(d => d.FittingNavigation).WithMany(p => p.Items)
                .HasForeignKey(d => d.FittingId)
                .HasConstraintName("fitting_fkey");

            entity.HasOne(d => d.PatternNavigation).WithMany(p => p.Items)
                .HasForeignKey(d => d.PatternId)
                .HasConstraintName("pattern_fkey");
        });

        modelBuilder.Entity<Karigar>(entity =>
        {
            entity.HasKey(e => e.KarigarId).HasName("karigar_pkey");

            entity.ToTable("karigar");

            entity.Property(e => e.KarigarId).HasColumnName("karigar_id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.ContactInfo)
                .HasMaxLength(200)
                .HasColumnName("contact_info");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
        });

        modelBuilder.Entity<LoginLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("loginLog_pkey");

            entity.ToTable("loginLog");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.LoginAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("login_at");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.LoginLogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("userId");
        });

        modelBuilder.Entity<PolishType>(entity =>
        {
            entity.HasKey(e => e.PolishTypeId).HasName("polish_type_pkey");

            entity.ToTable("polish_type");

            entity.Property(e => e.PolishTypeId).HasColumnName("polish_type_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<ProductHead>(entity =>
        {
            entity.HasKey(e => e.ProductHeadId).HasName("product_head_pkey");

            entity.ToTable("product_head");

            entity.Property(e => e.ProductHeadId).HasColumnName("product_head_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<ProductHeadType>(entity =>
        {
            entity.HasKey(e => e.ProductHeadTypeId).HasName("product_head_type_pkey");

            entity.ToTable("product_head_type");

            entity.Property(e => e.ProductHeadTypeId).HasColumnName("product_head_type_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.ProductHeadId).HasColumnName("product_head_id");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.ProductHead).WithMany(p => p.ProductHeadTypes)
                .HasForeignKey(d => d.ProductHeadId)
                .HasConstraintName("fk_category");
        });

        modelBuilder.Entity<StoneType>(entity =>
        {
            entity.HasKey(e => e.StoneTypeId).HasName("stone_type_pkey");
            entity.ToTable("stone_type");
            entity.Property(e => e.StoneTypeId).HasColumnName("stone_type_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name).HasMaxLength(100).HasColumnName("name");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<FittingType>(entity =>
        {
            entity.HasKey(e => e.FittingTypeId).HasName("fitting_type_pkey");
            entity.ToTable("fitting_type");
            entity.Property(e => e.FittingTypeId).HasColumnName("fitting_type_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name).HasMaxLength(100).HasColumnName("name");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<Pattern>(entity =>
        {
            entity.HasKey(e => e.PatternId).HasName("pattern_pkey");
            entity.ToTable("pattern");
            entity.Property(e => e.PatternId).HasColumnName("pattern_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name).HasMaxLength(100).HasColumnName("name");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_pkey");

            entity.ToTable("user");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasColumnType("character varying")
                .HasColumnName("password");
            entity.Property(e => e.Phone).HasColumnName("phone");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
