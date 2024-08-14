## TourEase
TourEase is a website specifically designed to connect tour guides with travel agencies and travelers, making the process of finding and selecting tours easy and stress-free for all parties involved.

## Features

### Phase 1:
- **Authentication & Authorization:** Login, Logout
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
- **Frontend:** ReactJS, Next.js, Redux Toolkit, Tailwind CSS
- **Database:** PostgreSQL, MongoDB, SQL Server, Redis
- **Deployment:** Docker, Kubernetes, CI/CD workflows using GitHub Actions
- **Unit & Integration Testing:** XUnit, Moq