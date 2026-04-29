using Microsoft.EntityFrameworkCore;
using ProjectBReady.Models.Facilities;
using ProjectBReady.Models.Inventory;
using ProjectBReady.Models.Users;

namespace ProjectBReady.Data
{
    public class AppDbContext : DbContext
    {
        // ── Tables ──────────────────────────────────────────────────────────
        public DbSet<Shelter>          Shelters          { get; set; }
        public DbSet<InventoryItem>    InventoryItems    { get; set; }
        public DbSet<FoodItem>         FoodItems         { get; set; }
        public DbSet<MedicalSupply>    MedicalSupplies   { get; set; }
        public DbSet<Person>           Persons           { get; set; }
        public DbSet<Resident>         Residents         { get; set; }
        public DbSet<BarangayOfficial> BarangayOfficials { get; set; }
        public DbSet<DispatchLog>      DispatchLogs      { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // LocalDB — palitan ng full SQL Server connection string kung kailangan
            options.UseSqlServer(
                @"Server=(localdb)\MSSQLLocalDB;Database=ProjectBReadyDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ── TPH (Table-Per-Hierarchy) para sa InventoryItem ─────────────
            modelBuilder.Entity<InventoryItem>()
                .HasDiscriminator<string>("ItemType")
                .HasValue<FoodItem>("Food")
                .HasValue<MedicalSupply>("Medical");

            modelBuilder.Entity<InventoryItem>(e =>
            {
                e.HasKey(i => i.ItemID);
                e.Property(i => i.ItemName).IsRequired().HasMaxLength(100);
                e.Property(i => i.Quantity).HasDefaultValue(0);
            });

            modelBuilder.Entity<FoodItem>(e =>
            {
                e.Property(f => f.ExpirationDate).IsRequired();
            });

            modelBuilder.Entity<MedicalSupply>(e =>
            {
                e.Property(m => m.Dosage).HasMaxLength(50);
            });

            // ── TPH para sa Person ──────────────────────────────────────────
            modelBuilder.Entity<Person>()
                .HasDiscriminator<string>("PersonType")
                .HasValue<Resident>("Resident")
                .HasValue<BarangayOfficial>("Official");

            modelBuilder.Entity<Person>(e =>
            {
                e.HasKey(p => p.UserID);
                e.Property(p => p.Role).HasMaxLength(50);
            });

            // ── Shelter ─────────────────────────────────────────────────────
            modelBuilder.Entity<Shelter>(e =>
            {
                e.HasKey(s => s.ShelterID);
                e.Property(s => s.ShelterName).IsRequired().HasMaxLength(100);
                e.Property(s => s.Status).HasMaxLength(50);
            });

            // ── DispatchLog ─────────────────────────────────────────────────
            modelBuilder.Entity<DispatchLog>(e =>
            {
                e.HasKey(d => d.LogID);
                e.HasOne(d => d.Item)
                 .WithMany()
                 .HasForeignKey(d => d.ItemID)
                 .OnDelete(DeleteBehavior.Restrict);
                e.HasOne(d => d.Shelter)
                 .WithMany()
                 .HasForeignKey(d => d.ShelterID)
                 .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
