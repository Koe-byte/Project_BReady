using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectBReady.Data;
using ProjectBReady.Models.Inventory;

namespace ProjectBReady.Services
{
    public class InventoryService
    {
        private readonly AppDbContext _db;

        public InventoryService(AppDbContext db)
        {
            _db = db;
        }

        // ── READ ────────────────────────────────────────────────────────────

        /// <summary>All inventory items (food + medical) as a flat list.</summary>
        public List<InventoryItem> GetAll()
        {
            return _db.InventoryItems.ToList();
        }

        /// <summary>Food items only.</summary>
        public List<FoodItem> GetFoodItems()
        {
            return _db.FoodItems.ToList();
        }

        /// <summary>Medical supplies only.</summary>
        public List<MedicalSupply> GetMedicalSupplies()
        {
            return _db.MedicalSupplies.ToList();
        }

        /// <summary>Returns FoodItems whose ExpirationDate is already past.</summary>
        public List<FoodItem> GetExpiredItems()
        {
            return _db.FoodItems
                      .Where(f => f.ExpirationDate < DateTime.Now)
                      .ToList();
        }

        /// <summary>Find a single item by ID. Returns null if not found.</summary>
        public InventoryItem GetByID(string itemID)
        {
            return _db.InventoryItems.FirstOrDefault(i => i.ItemID == itemID);
        }

        // ── STOCK IN ────────────────────────────────────────────────────────

        /// <summary>Adds stock to an existing item's quantity.</summary>
        public void StockIn(string itemID, int amount)
        {
            var item = GetByID(itemID);
            if (item == null)
                throw new Exception($"Item '{itemID}' not found.");
            if (amount <= 0)
                throw new Exception("Amount must be greater than zero.");

            item.StockIn(amount);   // calls InventoryItem.StockIn() (virtual)
            _db.SaveChanges();
        }

        // ── DISPATCH ────────────────────────────────────────────────────────

        /// <summary>
        /// Deducts <paramref name="qty"/> from the item's stock and writes a DispatchLog.
        /// Throws if item/shelter not found or insufficient stock.
        /// </summary>
        public void Dispatch(string itemID, string shelterID, int qty, string dispatchedByUserID)
        {
            var item = GetByID(itemID);
            if (item == null)
                throw new Exception($"Item '{itemID}' not found.");

            var shelter = _db.Shelters.FirstOrDefault(s => s.ShelterID == shelterID);
            if (shelter == null)
                throw new Exception($"Shelter '{shelterID}' not found.");

            if (qty <= 0)
                throw new Exception("Dispatch quantity must be greater than zero.");

            if (item.Quantity < qty)
                throw new Exception($"Insufficient stock. Available: {item.Quantity}");

            // Deduct stock using the model method (Polymorphism - overrideable)
            item.Dispatch(qty, shelter);

            // Write dispatch log
            _db.DispatchLogs.Add(new DispatchLog
            {
                ItemID = itemID,
                ShelterID = shelterID,
                Quantity = qty,
                DispatchedAt = DateTime.Now,
                DispatchedBy = dispatchedByUserID
            });

            _db.SaveChanges();
        }

        // ── LOGS ────────────────────────────────────────────────────────────

        /// <summary>
        /// Returns all dispatch logs, newest first, including related Item and Shelter info.
        /// </summary>
        public List<DispatchLogView> GetDispatchLogs()
        {
            return _db.DispatchLogs
                      .Include(d => d.Item)
                      .Include(d => d.Shelter)
                      .OrderByDescending(d => d.DispatchedAt)
                      .Select(d => new DispatchLogView
                      {
                          LogID = d.LogID,
                          ItemName = d.Item.ItemName,
                          ShelterName = d.Shelter.ShelterName,
                          Quantity = d.Quantity,
                          DispatchedAt = d.DispatchedAt,
                          DispatchedBy = d.DispatchedBy
                      })
                      .ToList();
        }

        // ── ADD NEW ITEMS ───────────────────────────────────────────────────

        public void AddFoodItem(string itemID, string name, int qty, DateTime expiration)
        {
            if (_db.InventoryItems.Any(i => i.ItemID == itemID))
                throw new Exception($"Item ID '{itemID}' already exists.");

            _db.FoodItems.Add(new FoodItem
            {
                ItemID = itemID,
                ItemName = name,
                Quantity = qty,
                ExpirationDate = expiration
            });
            _db.SaveChanges();
        }

        public void AddMedicalSupply(string itemID, string name, int qty, string dosage, bool requiresPrescription)
        {
            if (_db.InventoryItems.Any(i => i.ItemID == itemID))
                throw new Exception($"Item ID '{itemID}' already exists.");

            _db.MedicalSupplies.Add(new MedicalSupply
            {
                ItemID = itemID,
                ItemName = name,
                Quantity = qty,
                Dosage = dosage,
                IsPrescriptionRequired = requiresPrescription
            });
            _db.SaveChanges();
        }

        // ── DELETE ──────────────────────────────────────────────────────────

        public void DeleteItem(string itemID)
        {
            var item = GetByID(itemID);
            if (item == null)
                throw new Exception($"Item '{itemID}' not found.");

            _db.InventoryItems.Remove(item);
            _db.SaveChanges();
        }
    }

    // ── Flat DTO used by DataGridView (avoids EF navigation property noise) ──

    public class DispatchLogView
    {
        public int LogID { get; set; }
        public string ItemName { get; set; }
        public string ShelterName { get; set; }
        public int Quantity { get; set; }
        public DateTime DispatchedAt { get; set; }
        public string DispatchedBy { get; set; }
    }
}