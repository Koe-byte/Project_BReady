using System;
using System.Linq;
using ProjectBReady.Models.Facilities;
using ProjectBReady.Models.Inventory;
using ProjectBReady.Models.Users;

namespace ProjectBReady.Data
{
    /// <summary>
    /// Seeds the database with default data on first run.
    /// Call DbInitializer.Initialize(context) from Program.cs.
    /// </summary>
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            // Ensure DB is created (applies any pending migrations too)
            context.Database.EnsureCreated();

            // ── Seed Shelters ────────────────────────────────────────────
            if (!context.Shelters.Any())
            {
                context.Shelters.AddRange(
                    new Shelter
                    {
                        ShelterID    = "SH-001",
                        ShelterName  = "Barangay Hall Evacuation Center",
                        MaxCapacity  = 200,
                        Status       = "Active"
                    },
                    new Shelter
                    {
                        ShelterID    = "SH-002",
                        ShelterName  = "Elementary School Gym",
                        MaxCapacity  = 350,
                        Status       = "Active"
                    },
                    new Shelter
                    {
                        ShelterID    = "SH-003",
                        ShelterName  = "Community Center",
                        MaxCapacity  = 150,
                        Status       = "Standby"
                    }
                );
            }

            // ── Seed Inventory ───────────────────────────────────────────
            if (!context.InventoryItems.Any())
            {
                context.InventoryItems.AddRange(
                    new FoodItem
                    {
                        ItemID         = "FOOD-001",
                        ItemName       = "Canned Goods (Sardines)",
                        Quantity       = 500,
                        ExpirationDate = new DateTime(2026, 12, 31)
                    },
                    new FoodItem
                    {
                        ItemID         = "FOOD-002",
                        ItemName       = "Rice (50kg sacks)",
                        Quantity       = 100,
                        ExpirationDate = new DateTime(2025, 6, 30)
                    },
                    new MedicalSupply
                    {
                        ItemID                  = "MED-001",
                        ItemName                = "Paracetamol 500mg",
                        Quantity                = 1000,
                        Dosage                  = "500mg",
                        IsPrescriptionRequired  = false
                    },
                    new MedicalSupply
                    {
                        ItemID                  = "MED-002",
                        ItemName                = "First Aid Kit",
                        Quantity                = 50,
                        Dosage                  = "N/A",
                        IsPrescriptionRequired  = false
                    }
                );
            }

            // ── Seed Users ───────────────────────────────────────────────
            if (!context.Persons.Any())
            {
                context.Persons.AddRange(
                    new BarangayOfficial
                    {
                        UserID = "OFF-001",
                        Role   = "Barangay Captain"
                    },
                    new BarangayOfficial
                    {
                        UserID = "OFF-002",
                        Role   = "Relief Coordinator"
                    },
                    new Resident
                    {
                        UserID     = "RES-001",
                        Role       = "Resident",
                        IsReadOnly = true
                    }
                );
            }

            context.SaveChanges();
        }
    }
}
