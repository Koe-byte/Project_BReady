# 🚀 Project B-Ready: Setup Guide

Para sa mga teammates (**Danver, Nash, Janna**), sundin ang mga steps na ito para mapagana ang project sa inyong local machines pagkatapos i-clone mula sa GitHub.

---

## 🛠️ 1. Required Software
Bago niyo i-open ang project, siguraduhing installed ang mga ito:
* **Visual Studio 2022** (Community Edition).
* **Desktop Development with .NET** (Workload) - I-check ito sa Visual Studio Installer.
* **SQL Server Express LocalDB** - Kasama dapat ito sa VS installation para mabasa ang ating `.mdf` file.

---

## 📦 2. Installing Dependencies (NuGet Packages)
Kapag na-open niyo na ang project, baka may makita kayong red errors sa `Microsoft.Data.SqlClient`. Heto ang fix:

1. Pumunta sa **Tools** > **NuGet Package Manager** > **Manage NuGet Packages for Solution**.
2. I-click ang **Restore** button sa yellow bar sa taas (kung lalabas).
3. Kung wala, pumunta sa **Browse** tab, i-search ang `Microsoft.Data.SqlClient`, at i-install sa project.

---

## 🗄️ 3. Database Setup (Crucial!)
Para gumana ang Monitoring Dashboard nang hindi nag-e-error:
1. Sa **Solution Explorer**, hanapin ang folder na `Data`.
2. I-click ang **`BReadyDB.mdf`**.
3. Sa **Properties Window** (sa ilalim), hanapin ang **Copy to Output Directory**.
4. Siguraduhin na naka-set ito sa **`Copy if newer`**.

---

## ⚠️ 4. Troubleshooting (File Lock Error)
Kung lumabas ang error na *"The process cannot access the file because it is being used by another process"*:

1. Pumunta sa **Server Explorer**.
2. Right-click ang **`BReadyDB.mdf`**.
3. Piliin ang **Close Connection** (Dapat mawala ang green plug icon).
4. Subukan ulit i-run (**F5**).

---

## 🤝 5. Workflow Reminder
* **Janna:** Focus sa `DashboardForm.cs [Design]` para sa GUI.
* **Tristan/Nash/Danver:** Focus sa Business Logic at Database queries.
* **Laging mag-`git pull`** bago mag-umpisang mag-code!

---
Good luck, BSU Alangilan Devs! 🦇
