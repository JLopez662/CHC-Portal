# Client Manager for Accounting
![Project Status: Active](https://img.shields.io/badge/Project%20Status-Active-brightgreen)

A comprehensive Software as a Service (SaaS) platform for accountants to manage their customers' accounting and tax information efficiently.

## Project Objective

ClientManager offers accountants an efficient platform to manage customer information, simplifying the input, access, and updating of records across various categories like demographics, tax, confidential data, and much more.

## Methods

- Insert Customer's Data:

<p align="center">
  <img src="Images/ManualCreate.png" alt="Manual Insert" width="45%">
  <img src="Images/ImportCreate.png" alt="Import Insert" width="45%">
</p>
<p align="center" style="margin-top: 20px;">
  <span style="display:inline-block; width: 45%;">Manual Insert &nbsp;
    Import Insert</span>
</p>

- Update Customer's Data:

<p align="center">
  <img src="Images/ManualUpdate.png" alt="Manual Update" width="45%">
  <img src="Images/ImportUpdate.png" alt="Import Update" width="45%">
</p>
<p align="center" style="margin-top: 20px;">
  <span style="display:inline-block; width: 45%;">Manual Update &nbsp;
    Import Update</span>
</p>

- Export Customer's Data:

<p align="center">
  <img src="Images/ExportPDF.png" alt="Export PDF" width="45%">
  <img src="Images/ExportExcel.png" alt="Export Excel" width="45%">
</p>
<p align="center" style="margin-top: 20px;">
  <span style="display:inline-block; width: 45%;">Export to PDF &nbsp;
    Export to Excel</span>
</p>

- Search Customers and Users:

<p align="center">
  <img src="Images/Search.png" alt="Search" width="45%">
  <img src="Images/UserManagement.png" alt="UserManagement" width="45%">
</p>
<p align="center" style="margin-top: 20px;">
  <span style="display:inline-block; width: 45%;">Search Customer &nbsp;
    Search User</span>
</p>

## Technologies
- `ASP.NET Core 8 MVC`
- `C#`, `Javascript`, `Razor`, `SQL` , `HTML` , `CSS` 
- `Entity Framework`
- `SQL Server Management`

## Architecture and Component Overview

- **Models**: Used for defining data structures for users and customer details.

- **Views**: Razor views are used for rendering the user interface, dynamically presenting data with structured content. The views include dynamic implementations with JavaScript and AJAX, tightly integrated with controllers to ensure responsive and interactive user experiences.

- **Controllers**: The application includes both MVC and API controllers. MVC controllers manage HTTP requests and return views, while API controllers handle RESTful endpoints for external interactions.

- **Business Layer Logic (BLL)**: Implements business rules using interfaces and services. Interfaces ensure loose coupling, and services handle complex operations, providing clean interaction points for controllers.

- **Data Access Layers (DAL)**: Manages database interactions with models, repositories, and migrations. Repositories abstract data access, promoting code reusability and centralized logic.

- **API**: A comprehensive API layer uses controllers and services to expose functionalities to external clients, ensuring scalability and easy integration with other systems.

## Conclusion
ClientManager provides an efficient and user-friendly approach for accountants to manage their customers' data. By automating various aspects of data management and providing a comprehensive set of features, it enhances productivity and ensures accurate record-keeping.

## Contact

- [LinkedIn](https://www.linkedin.com/in/jlopezgonzalez/)
- [GitHub](https://github.com/JLopez662)
- [GitLab](https://gitlab.com/jorge.lopez19)

<p align="center">
  <a href="https://www.linkedin.com/in/jlopezgonzalez/">
    <img src="https://upload.wikimedia.org/wikipedia/commons/0/01/LinkedIn_Logo.svg" alt="LinkedIn" width="150" height="50">
  </a>
  <a href="https://github.com/JLopez662">
    <img src="https://upload.wikimedia.org/wikipedia/commons/9/91/Octicons-mark-github.svg" alt="GitHub" width="50" height="50">
  </a>
  <a href="https://gitlab.com/jorge.lopez19">
    <img src="https://upload.wikimedia.org/wikipedia/commons/e/e1/GitLab_logo.svg" alt="GitLab" width="150" height="50">
  </a>
</p>

