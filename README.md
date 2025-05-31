# Incidents Service
## Description

This microservice manages incidents and provides the following API endpoints:

| Operation          | HTTP Method | Endpoint                 | Description               |
|--------------------|-------------|--------------------------|---------------------------|
| CreateIncident     | POST        | `/v1/incidents`          | Create a new incident     |
| GetAllIncidents    | GET         | `/v1/incidents`          | Retrieve all incidents    |
| GetIncidentById    | GET         | `/v1/incidents/{id}`     | Retrieve incident by ID   |

---

## Architecture and Design

The service has been designed adhering to **SOLID principles** to ensure maintainability, extensibility, and testability.

Furthermore, the **CQRS (Command Query Responsibility Segregation)** pattern is applied to separate commands (create/update operations) from queries (read operations). 
This separation promotes clearer code organization and can facilitate future scalability, such as implementing a repository pattern for data storage.

---

## Running the Project

1. **Prerequisites:**
   - [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
   - Supported IDE (e.g., Visual Studio 2022+, VS Code)

2. **Build and run:**

```bash
dotnet build
dotnet run --project .\Incidents.Service.Host
```

3. **Testing:**
   - Postman: Create a new collection pointing to https://localhost:8082 and create new endpoints to tests the API endpoints while the service is running.
   - Swagger: Test the methods straight away from the swagger landing page.
   - NUnit: Run the unit tests using the test explorer in your IDE or via command line: ```bash dotnet test Incidents-service.sln```

4. **Improvements:**
   - I would complete the integration tests to make sure the whole execution of each endpoint covers all the expected error codes (happy and unhappy paths).
   - Creating a Database to properly store and manage Incidents would be ideal, then working with a Cache if it's highly demanded.
   - Add CodeQL file to validate build and testing within the service when creating PRs and branches.
   - Create a pipeline mechanism to deploy the service (Azure pipeline yaml and/or AWS CloudFormation stacks or deploying through Jenkins/TeamCity).
   - Extend GetAllIncidents to support pagination and filtering through query parameters in the URL.
