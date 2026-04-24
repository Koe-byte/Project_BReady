# 🚨 Project B-Ready: Digital Disaster Relief & Shelter Management

[cite_start]Project B-Ready is a desktop-based management system developed using C# Windows Forms Application[cite: 152]. [cite_start]It is designed to assist barangay officials in managing disaster relief operations and evacuation shelter monitoring at a localized scale[cite: 153]. 

[cite_start]To ensure efficiency and maintainability, the system runs on a "Kiosk Mode" architecture[cite: 154]. [cite_start]It starts with a default read-only "Resident View" dashboard and restricts administrative controls to authorized officials via a secret shortcut and PIN[cite: 155].

---

## ⚙️ Core Features
* [cite_start]**Evacuation Shelter Management:** Monitors shelter capacity, current occupancy, and operational status (Open/Full/Closed)[cite: 156].
* [cite_start]**Relief Goods Inventory:** Manages the stock-in and dispatch of essential supplies without tight resident-level tracking to prevent system lag[cite: 157].
* [cite_start]**Reports & Summary Dashboard:** Utilizes an on-demand refresh system to fetch the latest data from the database and generate quick situational reports[cite: 158].

---

## 🛠️ Tech Stack
* [cite_start]**Language & Framework:** C# / .NET [cite: 159]
* [cite_start]**Interface:** Windows Forms (WinForms) [cite: 159]
* [cite_start]**Database:** SQL Server (LocalDB via Table-Per-Hierarchy approach) [cite: 159]
* [cite_start]**Design Tool:** Figma [cite: 159]

---

## 💻 Object-Oriented Programming (OOP) Application
The system strictly adheres to the four pillars of Object-Oriented Programming:
* [cite_start]**Encapsulation:** Protects data integrity by managing shelter capacities through private fields and public methods[cite: 159].
* [cite_start]**Inheritance:** Utilizes base classes for users (Person) and inventory (InventoryItem) to create specialized subclasses[cite: 160].
* [cite_start]**Polymorphism:** Implements method overriding to handle distinct relief distribution computations based on item types[cite: 161].
* [cite_start]**Abstraction:** Leverages `ITrackable` and `IReportable` interfaces to ensure consistent data generation across isolated modules[cite: 162].

---

## 🚀 Setup Instructions (For Development)
1. **Clone the repository.**
2. [cite_start]Ensure you have **Visual Studio** installed with the `.NET desktop development` workload[cite: 37].
3. [cite_start]**Database Setup:** The project uses **SQL Server Express (LocalDB)** which is built-in to Visual Studio[cite: 54]. [cite_start]The `.mdf` database file is automatically included in the repository and will dynamically connect using the `|DataDirectory|` connection string[cite: 114].
4. Build and Run the solution.

> [cite_start]**Note:** A `.gitignore` file specifically for Visual Studio has been applied to this repository to prevent temporary background files (like `bin/` and `obj/` folders) from causing code conflicts[cite: 167, 168]. [cite_start]Pure C# code lang ang ma-u-upload[cite: 169].

---

## 👥 Meet the Team
* [cite_start]**Tristan Allen Cabral** - Logic Developer/Tester [cite: 320]
* [cite_start]**Nash Ibon** - Logic Developer/Tester [cite: 320]
* [cite_start]**John Danver Manalo** - Project Manager/Lead Developer [cite: 320]
* [cite_start]**Janna Alexis Raras** - GUI Designer [cite: 321]

---

## 📜 License
[cite_start]This project is licensed under the **MIT License**[cite: 178]. 
* [cite_start]You can copy, study, or use the code[cite: 179].
* [cite_start]Please give credit to the original creators (Team B-Ready) if you use the code[cite: 180]. 
* [cite_start]The creators hold no liability for any issues or errors that may occur upon using the system[cite: 181].
