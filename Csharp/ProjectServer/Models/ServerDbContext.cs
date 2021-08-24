using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ProjectServer.Models
{
    public partial class ServerDbContext : DbContext
    {
        public ServerDbContext()
        {
        }

        public ServerDbContext(DbContextOptions<ServerDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Connect> Connects { get; set; }
        public virtual DbSet<DirectMessage> DirectMessages { get; set; }
        public virtual DbSet<Topic> Topics { get; set; }
        public virtual DbSet<TopicMessage> TopicMessages { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=adm;Password=adm");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "en_US.utf8");

            modelBuilder.Entity<Connect>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.TopicsId })
                    .HasName("PK_connect");

                entity.HasOne(d => d.Topics)
                    .WithMany(p => p.Connects)
                    .HasForeignKey(d => d.TopicsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_18");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Connects)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_14");
            });

            modelBuilder.Entity<DirectMessage>(entity =>
            {
                entity.HasOne(d => d.ReceiverNavigation)
                    .WithMany(p => p.DirectMessageReceiverNavigations)
                    .HasForeignKey(d => d.Receiver)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_40");

                entity.HasOne(d => d.SenderNavigation)
                    .WithMany(p => p.DirectMessageSenderNavigations)
                    .HasForeignKey(d => d.Sender)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_37");
            });

            modelBuilder.Entity<Topic>(entity =>
            {
                entity.HasKey(e => e.TopicsId)
                    .HasName("PK_topics");
            });

            modelBuilder.Entity<TopicMessage>(entity =>
            {
                entity.HasOne(d => d.Topics)
                    .WithMany(p => p.TopicMessages)
                    .HasForeignKey(d => d.TopicsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_29");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TopicMessages)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_26");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
