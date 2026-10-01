# Cairo Metro E-Subscription Management System

An enterprise-oriented web application designed to automate and streamline public transit subscription operations for the Cairo Metro network. The platform replaces traditional, manual paper workflows with an end-to-end digital ecosystem, enabling commuters to register, verify identity, and generate verified digital passes, while providing transit authority administrators with automated audit and decision-making tools.

---

## System Overview & Problem Statement

Public transit subscription services frequently encounter significant operational bottlenecks:
* Extreme in-station crowding and processing delays at ticketing offices during peak academic and administrative cycles.
* Vulnerability to manual processing errors, physical document loss, and prolonged verification turnaround times.
* Substantial administrative overhead and lack of centralized tracking for subsidized ticket allocations.

This solution provides a centralized web platform that decentralizes application intake, enforces strict data validation standards, and establishes digital verification mechanisms for field transit personnel.

---

## Core Capabilities

* **Role-Based Access Control (RBAC):** Distinct permission tiers separating commuter self-service interfaces from administrative and auditing dashboards.
* **Document Ingestion & Validation:** Secure handling and storage of high-resolution identity credentials, proof-of-enrollment certificates, and formal employer documentation.
* **Administrative Audit Logging:** Rigorous accountability mechanisms recording administrative actions, exact timestamps, and reviewer IDs for all subscription status decisions.
* **Contactless Transit Pass Generation:** Algorithmic generation of scannable, cryptographically compliant 2D barcodes (QR Codes) encoding user identity, validity periods, and designated line stations.
* **Database Constraint Optimization:** Custom entity relationship mappings configured via Fluent API to eliminate circular dependencies and multiple cascade deletion paths.
* **Automated Data Initialization:** Programmatic database seeding on startup ensuring system roles, station reference matrices, and default officer profiles exist upon deployment.

---

## Technical Architecture & Dependencies

* **Core Platform:** ASP.NET Core MVC
* **Programming Language:** C# (.NET)
* **Object-Relational Mapping (ORM):** Entity Framework Core
* **Database Engine:** Microsoft SQL Server
* **Security & Authentication:** ASP.NET Core Identity
* **Data Presentation:** Razor Views, Bootstrap 5, Modern JavaScript
* **Supporting Libraries:**
  * `QRCoder` (Dynamic QR generation engine)
  * `Microsoft.EntityFrameworkCore.SqlServer`
  * `Microsoft.EntityFrameworkCore.Tools`

---

## Database Schema Design

The relational schema implements clean separation between authentication primitives and domain-specific operations:

* **AspNetUsers:** Custom-extended identity table capturing core identity attributes, National Identification numbers, and profile assets.
* **EmployeeProfiles:** One-to-one extension managing administrative staff, departmental affiliations, employee codes, and duty locations.
* **Subscriptions:** Central transactional entity tracking trip parameters (origin/destination stations), user demographic categories, uploaded verification assets, approval states, and foreign keys for reviewing officers.
* **Stations:** Reference data representing operational metro stops and zone boundaries across transit lines.

---

## Deployment & Local Setup

### System Requirements
* .NET SDK (Version 8.0 or later)
* Microsoft SQL Server (LocalDB, Express, or Enterprise Edition)
* Visual Studio 2022 or Visual Studio Code with the C# Dev Kit extension
### Installation Guide

1. Clone the repository:
   git clone (https://github.com/Mgemyy/Metro.git)
   cd Metro

2. Configure the database connection string in appsettings.json:
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MetroDb;Trusted_Connection=True;MultipleActiveResultSets=true"
   }

3. Execute migration scripts to construct the database schema:
   Update-Database
   # Or using .NET CLI:
   dotnet ef database update

4. Launch the application:
   dotnet run

### Default System Accounts (Seeded)
* Administrator: (for local development only)
  * Email: admin@metro.com
  * Password: Admin@123
* Standard Commuter:
  * Email: user@metro.com
  * Password: User@123

---

## Technical Enhancements Roadmap

* Integration of external PCI-compliant payment gateways (Fawry / Paymob) for automated transaction reconciliation.
* Development of an inspector-facing web terminal with integrated camera scanning for gate-side pass validation.
* Implementation of distributed background schedulers (Hangfire) to manage automated expiry alerts and subscription cycle renewals.

---

## Developer Information

Mohamed Gamal  
Backend Software Developer  
* LinkedIn: (https://www.linkedin.com/in/mohamed-gamal-763354288/)  
* GitHub: (https://github.com/Mgemyy)

   
