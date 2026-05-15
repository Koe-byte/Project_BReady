# 🚨 Project B-Ready: Digital Disaster Relief & Shelter Management

![Project Logo](Frontend/Assets/logo.jpg)

**Project B-Ready** is a professional desktop-based management system designed to assist barangay officials and disaster response teams in managing evacuation centers and relief operations. Built with modern .NET technologies, it provides a seamless transition from manual tracking to a robust, real-time digital solution.

---

## 🛠️ Tech Stack & Architecture

- **Framework:** .NET 8.0 (Windows Desktop)
- **UI Technology:** Windows Presentation Foundation (WPF) with XAML
- **Language:** C# 12
- **Database:** PostgreSQL (Hosted on Supabase)
- **Pattern:** N-Tier Architecture with Dependency Injection (DI)
- **Real-time:** Custom PostgreSQL Notification Listener

---

## ⚙️ Key Features

### 🏢 Evacuation Shelter Management
- Real-time monitoring of shelter capacities and occupancy.
- Dynamic status tracking: **Open**, **Full**, **Closed**, or **Under Maintenance**.
- Centralized database synchronization across all terminal instances.

### 📦 Relief Goods & Inventory
- Specialized management for stock-in and dispatch of essential supplies.
- Automated logs for distribution to ensure transparency in relief operations.
- Inventory trend tracking to predict supply needs.

### 🖥️ Kiosk Mode & Secure Admin Access
- **Resident View:** A read-only dashboard for public display in evacuation centers.
- **Hidden Admin Panel:** Authorized personnel can access management tools via a secret shortcut: `Ctrl + Shift + O`.
- **PIN Verification:** Secondary security layer for administrative actions.

---

## 🚀 Getting Started

Follow these instructions to set up the project on your local machine for development and testing.

### Prerequisites
- **Visual Studio 2022** (Community, Professional, or Enterprise)
- **.NET 8 SDK**
- **Desktop Development with .NET** workload (Install via VS Installer)

### Installation & Setup

1. **Clone the Repository**
   ```bash
   git clone https://github.com/Koe-byte/Project_BReady.git
   cd ProjectBReadyWPF
   ```

2. **Restore NuGet Packages**
   Open the solution (`ProjectBReadyWPF.sln`) in Visual Studio. NuGet packages should restore automatically. If not, go to:
   `Tools > NuGet Package Manager > Manage NuGet Packages for Solution > Restore`

3. **Configure Database**
   The project uses a remote PostgreSQL database. To protect sensitive credentials, `appsettings.json` is excluded from the repository.
   - Rename `appsettings.Example.json` to `appsettings.json`.
   - Update the connection string and Admin PIN hash with your local/development values.
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "YOUR_POSTGRES_CONNECTION_STRING"
     },
     "Security": {
       "AdminPinHash": "YOUR_SHA256_PIN_HASH"
     }
   }
   ```
   > [!IMPORTANT]
   > Never commit your real `appsettings.json` to a public repository. Always use the example file as a template.


4. **Build and Run**
   Press `F5` or click **Start** in Visual Studio to launch the application in **Resident View**.

---

## 🏗️ Project Structure

- **`Frontend/`**: Contains XAML Views, ViewModels, and UI Components.
- **`Backend/`**: Core logic, Service implementations, and Interfaces.
- **`Database/`**: Data access layer and database helper classes.
- **`Models/`**: Domain entities (Shelters, Inventory, Persons).

---

## 🎓 Object-Oriented Programming (OOP) Principles

This project serves as a showcase for advanced OOP concepts:
- **Abstraction:** Use of interfaces (`IShelterService`, `IAuthService`) to decouple logic from implementation.
- **Encapsulation:** Data protection through private fields and robust property validation.
- **Inheritance:** Hierarchical model structures for different facility and user types.
- **Polymorphism:** Flexible service implementations and UI event handling.

---

## 👥 Meet the Team (BSU Alangilan Devs)

- **John Danver Manalo** - Project Manager / Lead Backend Developer
- **Tristan Allen Cabral** - Logic Developer / Database Architect
- **Nash Ibon** - Logic Developer / Quality Assurance
- **Janna Alexis Raras** - Lead GUI Designer / UX Specialist

---

## 📜 License
This project is licensed under the **MIT License**. See the [LICENSE](LICENSE) file for details.
