# Inventory Application (.NET Core)

## Description

The **Inventory Application** is a .NET Core-based solution comprising a **Web API** and a **Web Application**. It uses **SQL Server** as the database and is containerized using **Docker**.

---

## Prerequisites

* .NET Core SDK
* Docker
* Docker Compose (optional)
* SQL Server Docker Image

---

## Environment Variables

* `ASPNETCORE_ENVIRONMENT`: Specifies the environment (Development/Production)
* `INVENTORY_CONNECTION_STRING`: Connection string to the SQL Server database

---

## Building Docker Images

### 1. Inventory API

To build the Docker image for the API, ensure you are in the **root directory** of the project:

```bash
docker build -t inventory-api -f InventoryApplication.Api/Dockerfile .
```

### 2. Inventory Web Application

To build the Docker image for the web application, also from the root directory:

```bash
docker build -t inventory-web-app -f Dockerfile .
```

---

## Setting Up the Network

To ensure that the API and database can communicate, create a dedicated Docker network:

```bash
docker network create inventory-network
```

---

## Running the SQL Server Container

Start the SQL Server container with the following command:

```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=P@ssword@veryStrong" -p 1433:1433 --name inventory-sqlserver --network inventory-network -d mcr.microsoft.com/mssql/server:2022-latest
```

### Access the SQL Server

To connect to the database, use the following credentials:

* **Server:** `localhost,1433`
* **Username:** `sa`
* **Password:** `P@ssword@veryStrong`

---

## Running the Inventory API Container

Start the API container using the previously created network:

```bash
docker run -d --name inventory-api -p 8080:8080 --network inventory-network -e "ASPNETCORE_ENVIRONMENT=Development" -e "INVENTORY_CONNECTION_STRING=Server=inventory-sqlserver;Database=InventoryDb;User Id=sa;Password=P@ssword@veryStrong;TrustServerCertificate=True;" inventory-api
```

---

## Accessing the API

Once the container is running, the API will be available at:

```
http://localhost:8080
```

### API Endpoints

* **GET /api/equipment**: Retrieves all equipment records.
* **POST /api/equipment**: Adds new equipment.
* **PUT /api/equipment/{id}**: Updates existing equipment.
* **DELETE /api/equipment/{id}**: Deletes an equipment record.

---

## Stopping and Removing Containers

To stop and remove the API and SQL Server containers:

```bash
docker stop inventory-api inventory-sqlserver
docker rm inventory-api inventory-sqlserver
```

To remove the Docker network:

```bash
docker network rm inventory-network
```

---

## Troubleshooting

* **Database Connection Issues:**

  * Ensure the SQL Server container is running.
  * Check if the network is correctly configured.
  * Validate the connection string.

* **Port Conflicts:**

  * Make sure ports `8080` and `1433` are not in use by other applications.

---

## License

This project is licensed under the MIT License.
