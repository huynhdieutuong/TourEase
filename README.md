## TourEase
TourEase is a website specifically designed to connect tour guides with travel agencies and travelers, making the process of finding and selecting tours easy and stress-free for all parties involved.

## Features

### Phase 1:
- **Authentication & Authorization:** Login, Logout, Register
- **Admin:** CRUD Destinations
- **Travel Agency:** Add, update, and delete tours; select tour guides
- **Tour Guide:** Search for and bid on tours
- **Notifications:** When a tour guide bids on a tour, the travel agency receives a notification. The agency can then select the tour guide who best matches their qualifications.

### Phase 2:
- **Tour Guide Networking:** Tour guides can add friends and connect with each other.
- **Reviews:** Both tour guides and travel agencies can leave reviews for each other.
- **Tour Management for Guides:** Helps tour guides easily arrange their calendars and manage their tours.
- **Tour Advertising:** Agencies can advertise their tours by pushing notifications to selected tour guides.

### Phase 3:
- **Chat Feature:** Enables communication between users.
- **New Role: Traveler:** Add a traveler role to the platform.
- **Expanded Tour Properties:** Add more details to tours, such as types (Adventure, History, Buddhism, Culture, etc.), locations (US, UK, Vietnam, etc.), and languages (English, Vietnamese, etc.).
- **Tour Design & Booking:** Tour guides and travel agencies can design tours. Travelers can book individual guides or purchase tours from agencies.
- **Payment Integration:** Allow payments through the platform.
- **Multilingual Support:** Enable multiple languages for the website.

## Technologies

### Phase 1:
- **Backend:** Microservices with .NET 8, Identity Server, Entity Framework, RabbitMQ, gRPC, SignalR, YARP, Serilog, Elasticsearch, AutoMapper
- **Frontend:** ReactJS, Next.js, Zustand, Tailwind CSS
- **Database:** PostgreSQL, MongoDB, SQL Server, Redis
- **Deployment:** Docker, Kubernetes, CI/CD workflows using GitHub Actions
- **Unit & Integration Testing:** XUnit, Moq

#### 1. Sync Data Between Tour Service (SQL Server) and TourSearch Service (MongoDB):
- Seeding:
	- During seeding, all data first be seeded in SQL Server. The TourSearch Service will then call the Tour Service to retrieve data and insert it into MongoDB
	- If the Tour Service is unavailable, the TourSearch Service will retry until the data is successfully retrieved
- Create, Update, Delete:
	- Data synchronization is handled via RabbitMQ, with the Outbox Pattern implemented to manage scenarios when RabbitMQ is down
	- The TourJobCreatedConsumer will retry 5 times if MongoDB is down
	- When a Destination is Deleted, all its child destinations will also be Deleted. Any TourJobs containing these Destinations will be Updated in both SQL Server and MongoDB

#### 2. Search TourJobs Behaviors:
- Search by Title or Itinerary
- Filter by Country, City, Duration, Currency, and Include Finished
- Order by End date (default), Recently added, Ascending salary, or Descending salary
- Pagination with the option to select Page Size
- If User wants to select a City, they must select a Country first (all Cities of that Country will be available in the City Options)
- When User inputs a Search Term, Filters will be reset
- When User selects a Country, Search Term will be reset
- Clicking the Logo will reset all Search Term and Filters

## Useful commands
- Migration commands for Tour API:
```
cd src\Services\Tour
dotnet ef migrations add "Int_TourDB" -p Tour.Infrastructure --startup-project Tour.API --output-dir Persistence/Migrations
dotnet ef migrations remove -p Tour.Infrastructure --startup-project Tour.API
dotnet ef database update -p Tour.Infrastructure --startup-project Tour.API
```