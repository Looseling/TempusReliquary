using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TimeCapsuleBackend.Data.Models
{
    public partial class TImeCapsuleDBContext : DbContext
    {
        public TImeCapsuleDBContext()
        {
        }

        public TImeCapsuleDBContext(DbContextOptions<TImeCapsuleDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Collaborator> Collaborators { get; set; }
        public virtual DbSet<TimeCapsule> TimeCapsules { get; set; }
        public virtual DbSet<TimeCapsuleContent> TimeCapsuleContents { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-FGBPURC;Initial Catalog=TImeCapsuleDB;User ID=admin;Password=123;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Collaborator>(entity =>
            {
                entity.Property(e => e.TimeCapsuleId).HasColumnName("time_capsule_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.TimeCapsule)
                    .WithMany(p => p.Collaborators)
                    .HasForeignKey(d => d.TimeCapsuleId)
                    .HasConstraintName("FK_Collaborators_Time_Capsule");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Collaborators)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Collaborators_Users");
            });

            modelBuilder.Entity<TimeCapsule>(entity =>
            {
                entity.ToTable("Time_Capsule");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.OpeningDate)
                    .HasColumnType("datetime")
                    .HasColumnName("opening_date");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("title");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<TimeCapsuleContent>(entity =>
            {
                entity.ToTable("Time_Capsule_Content");

                entity.Property(e => e.ContentType)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("content_type");

                entity.Property(e => e.Files)
                    .HasMaxLength(100)
                    .HasColumnName("files");

                entity.Property(e => e.Text)
                    .HasColumnType("text")
                    .HasColumnName("text");

                entity.Property(e => e.TimeCapsuleId).HasColumnName("time_capsule_id");

                entity.HasOne(d => d.TimeCapsule)
                    .WithMany(p => p.TimeCapsuleContents)
                    .HasForeignKey(d => d.TimeCapsuleId)
                    .HasConstraintName("FK_Time_Capsule_Content_Time_Capsule");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("password");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
