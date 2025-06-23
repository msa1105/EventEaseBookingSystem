## 📘 **Reflective Technical Report**

**EventEase Booking System**
**Student: Muhammed Saif Alexander**
**Web App URL**: (cldvprojectpart3-cvckadf8a9dpc5h5.southafricanorth-01.azurewebsites.net)

---

### 🔧 1. Full Feature List

The *EventEase* system is a cloud-enabled event booking platform that enables staff to manage venues, events, and client bookings. Key features include:

* **Venue Management**: Create, edit, view, and delete venues, including uploading images to Azure Blob Storage.
* **Event Management**: CRUD operations on events, linked to venues, with image support.
* **Booking System**: Users can make bookings for events, with validation to prevent double-booking.
* **Event Type Categorisation**: Events can be filtered by predefined event types.
* **Advanced Filtering**: Users can filter events by date range, event type, and venue availability.
* **Azure Blob Storage Integration**: Upload and serve images through a secure, scalable storage service.
* **Azure SQL Database**: Used to persist all system data including users, venues, events, and bookings.
* **Responsive, Dark-Themed UI**: Mobile-friendly and styled with Razor + CSS.
* **Deployment to Azure Web App**: Live web app accessible on the internet.

---

### 🧩 2. Component & Azure Services Discussion

#### ⚙ Azure Web App (Hosting)

Used to deploy and host the ASP.NET Core MVC web application. This service was selected for its seamless GitHub integration, auto-scaling support, and built-in diagnostics.

**Alternative Considered**: I briefly considered Azure Static Web Apps, but due to the need for server-side logic and database integration, Azure Web App was a better fit.

#### 🗃 Azure SQL Database (Persistent Data Store)

Used to store relational data for all entities: `Venue`, `Event`, `Booking`, and `EventType`. It ensured ACID-compliant transactions and easy integration with Entity Framework Core.

**Alternative**: Azure Cosmos DB was considered for its global distribution and NoSQL capabilities, but since the data was relational, SQL Database was the better choice.

#### 📂 Azure Blob Storage (Media Handling)

Used to upload and serve venue/event images via public URLs. A custom service (`BlobService.cs`) manages file uploads.

**Alternative**: Filesystem storage on the server was a non-scalable option. Blob Storage offers durability and is production-ready for media files.

#### 🔨 GitHub Actions (CI/CD)

Used for continuous deployment to Azure Web App directly from GitHub repository. It reduced manual deployment effort and ensured up-to-date production environments.

---

### 💻 3. Technologies Used and Justification

* **ASP.NET Core MVC**: Ideal for rapid web development with clear separation of concerns.
* **Entity Framework Core**: Simplified database interaction using code-first approach and migrations.
* **Azure SQL**: Hosted relational database with minimal configuration needed.
* **Azure Blob Storage**: Used for managing media files externally, improving performance.
* **HTML/CSS**: Used to craft a sleek, dark-themed, responsive UI.
* **Visual Studio / GitHub**: IDE and version control platform used throughout the project.
* **JavaScript (Lightweight)**: Used in views for validation and dynamic UI elements.

---

### 📖 4. Full Project Reflection (Part 1 → Part 3)

From the initial phase of setting up basic CRUD functionality to fully deploying the EventEase system on Azure, this project has been a transformational learning experience.

In **Part 1**, I laid the foundation with system analysis and design tasks like ERD design, use cases, and interface mockups. This phase taught me the value of planning and stakeholder-based thinking.

In **Part 2**, I translated those designs into a working web application. This was the first time I integrated image uploading using Azure Blob Storage, implemented custom business logic like booking validations, and began deploying to the cloud. Debugging real-time deployment issues pushed me to understand cloud environments much better.

In **Part 3**, I enhanced the system’s usability with advanced filtering, added an `EventType` table, and polished the application for final deployment. I also dealt with data relationships more deeply and encountered issues like double foreign key mappings, which helped me strengthen my Entity Framework and SQL skills.

Overall, I gained practical, hands-on experience in:

* Designing and modeling real-world systems
* Developing full-stack web applications
* Deploying to a cloud platform with service integration
* Debugging and managing a production app
* Applying Agile-style incremental development

---

### 🧾 5. Conclusion

The EventEase project gave me a real taste of professional development, far beyond the classroom. It taught me that building modern applications means much more than writing code — it’s about architecting reliable systems, understanding cloud platforms, working with databases, and delivering a product that users can trust.

Every Azure service used was not just chosen out of necessity, but for its scalability, reliability, and alignment with enterprise standards. With each service — Web App, SQL Database, Blob Storage — I learned how to balance configuration with performance and security.

This project was not just a university assignment — it was a complete development experience that will shape how I build software in the future.

---

### 🔗 6. Useful Links

* **Web App**: [EventEase Live Web App](https://ggyygygygygyg-fkded3eka0apcea6.brazilsouth-01.azurewebsites.net)
* **GitHub Repository**: *(Add your GitHub repo URL here)*
* **Blob Storage Container**: `images` (Azure Portal access)
* **Database Name**: *(Add your Azure SQL DB name here)*

---

### 📝 7. References

* Microsoft Learn. (2024). *Deploy an ASP.NET Core app to Azure*. [https://learn.microsoft.com](https://learn.microsoft.com)
* Pluralsight. (2024). *Working with Azure Blob Storage in .NET Core*
* Freeman, A. (2021). *Pro ASP.NET Core MVC 5*. Apress.
* IIE Prescribed Material (2024). *Software Development Frameworks and Azure Integration*.

---

Would you like this exported as a PDF or Markdown for GitHub (`README.md`)?
