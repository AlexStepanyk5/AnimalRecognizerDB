using Microsoft.EntityFrameworkCore;
using AnimalRecognizer.Model;
using System.Reflection.Metadata;

namespace AnimalRecognizer.Data
{

    public class AnimalRecognizerDBContext : DbContext
    {
        public AnimalRecognizerDBContext(DbContextOptions<AnimalRecognizerDBContext> options) : base(options)
        {
        }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<Shelter> Shelters { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=AnimalRecognizerDB;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pet>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Name)
                .HasColumnType("varchar")
                .HasMaxLength(150)
                .IsUnicode(false);

                entity.Property(p => p.Colour)
                .HasColumnType("varchar")
                .HasMaxLength(150)
                .IsUnicode(false)
                .IsRequired();

                entity.Property(c => c.Type)
                .HasConversion<string>()
                .HasMaxLength(150)
                .IsUnicode(false);

                entity.Property(p => p.Sterilized)
                .HasColumnType("bit")
                .IsRequired();

                entity.Property(p => p.Passport)
                .HasColumnType("bit")
                .IsRequired();

                entity.HasOne(p => p.Image)
                .WithOne()
                .IsRequired();

                entity.HasOne(p => p.CurrentShelter)
                .WithMany(p => p.Pets)
                .HasForeignKey(p => p.CurrentShelterId)
                .IsRequired();

            });

            modelBuilder.Entity<Shelter>(entity =>
            {
                entity.HasKey(s => s.Id);

                entity.Property(s => s.Name)
                .HasColumnType("varchar")
                .HasMaxLength(150)
                .IsUnicode(false);

                entity.Property(s => s.Address)
                .HasColumnType("varchar")
                .HasMaxLength(150)
                .IsUnicode(false);

                entity.Property(s => s.Phone)
                .HasColumnType("varchar")
                .HasMaxLength(150)
                .IsUnicode(false);

                entity.Property(s => s.QuantityOfPets)
                .HasColumnType("int")
                .HasMaxLength(12);
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.HasKey(i => i.Id);

                entity.Property(i => i.Src)
               .HasColumnType("varchar")
               .HasMaxLength(150)
               .IsUnicode(false)
               .IsRequired();

                entity.Property(i => i.Alt)
               .HasColumnType("varchar")
               .HasMaxLength(150)
               .IsUnicode(false);

            });

        }

    }
}
