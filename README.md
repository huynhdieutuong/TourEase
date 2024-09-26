# TourEase
TourEase is a website specifically designed to connect tour guides with travel agencies and travelers, making the process of finding and selecting tours easy and stress-free for all parties involved.

# Features

## Phase 1:
![usecases](./resources/usecases.png "Use cases")
- **Authentication & Authorization:** Login, Logout, Register
- **Admin:** Manage destinations (Create, Read, Update, Delete)
- **Travel Agency:** Add, update, and delete tour jobs; select tour guides
- **Tour Guide:** Search for and apply to tour jobs
- **Notifications:** When a tour guide applies for a tour job, the travel agency receives a notification. The agency can then select the tour guide who best matches their qualifications, and that tour guide will receive a notification.

## Phase 2:
- **Tour Guide Networking:** Tour guides can connect with each other by adding friends.
- **Reviews:** Both tour guides and travel agencies can leave reviews for each other.
- **Tour Management for Guides:** Helps tour guides easily organize their calendars and manage their tours.
- **Tour Advertising:** Agencies can promote their tours by pushing notifications to selected tour guides.

## Phase 3:
- **Chat Feature:** Allows communication between users.
- **New Role: Traveler:** Introduce a traveler role to the platform.
- **Expanded Tour Properties:** Add more details to tours, such as types (Adventure, History, Buddhism, Culture, etc.), locations (US, UK, Vietnam, etc.), and languages (English, Vietnamese, etc.).
- **Tour Design & Booking:** Tour guides and travel agencies can design tours. Travelers can book individual guides or purchase tours from agencies.
- **Payment Integration:** Allow payments through the platform.
- **Multilingual Support:** Support multiple languages for the website.

# Technologies

## Phase 1:
- **Backend:** Microservices with .NET 8, Identity Server, Entity Framework, RabbitMQ, gRPC, SignalR, YARP, Serilog, Elasticsearch, AutoMapper
- **Frontend:** ReactJS, Next.js, Zustand, Tailwind CSS
- **Database:** PostgreSQL, MongoDB, SQL Server, Redis
- **Deployment:** Docker, Kubernetes, CI/CD workflows using GitHub Actions
- **Unit & Integration Testing:** XUnit, Moq

### Architecture:
![diagram](./resources/tourease-diagram.png "Diagram")
#### 1. Tour Service (SQL Server + Entity Framework):
- Handles write operations (like creating, updating and deleting tour jobs).
- We use SQL Server to ensure data consistency with strong transactions, which is important for Travel Agencies posting new tours.

#### 2. TourSearch Service (MongoDB + MongoDB.Driver):
- Focuses on read operations, as many users will search for tour jobs.
- MongoDB allows faster searches by avoiding complex joins, and it can easily scale out to handle large numbers of users.

#### 3. TourApply Service (MySQL + Dapper):

#### 4. Notification Service (SignalR):

#### 5. Identity Service (Duende IdentityServer):

#### 6. Api Gateway (Yarp.ReverseProxy):


### Main Functionalities:
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

#### 3. CRUD TourJobs and Destinations UI:
- Create and Update TourJob validation:
	- For VND Currency, salary must not have decimal places.
	- Expired date must be in the future (at least 2 hours).
	- Start date must be after the expired date.
	- End date must be after the start date.
- Create Root Destination: User can select a Parent Destination
- Create Child Destination: The Parent Dropdown is disabled
- Update Destiantion: The Parent Dropdown is hidden

- Public pages:
	- /tourjobs: Displays tour jobs
	- /tourjobs/[slug]: Displays tour job details that TourGuide Role can apply for (login required)
- Pages only TravelAgency Role: 
	- /tourjobs/list : Displays the tour jobs of the agency (My tour jobs)
	- /tourjobs/create: Allows the agency to create a tour job
	- /tourjobs/update/[id]: Allows the agency to update a tour job
- Pages only Admin Role:
	- /destinations/list: Displays the destination list, including options create, update, delete destinations


## Useful commands
- Migration commands for Tour API:
```
cd src\Services\Tour
dotnet ef migrations add "Int_TourDB" -p Tour.Infrastructure --startup-project Tour.API --output-dir Persistence/Migrations
dotnet ef migrations remove -p Tour.Infrastructure --startup-project Tour.API
dotnet ef database update -p Tour.Infrastructure --startup-project Tour.API
```