**📘 Microservices Kafka Demo (.NET)**
🚀 Overview

This project demonstrates an event-driven microservices architecture built with .NET 8 and Apache Kafka, showcasing publish/subscribe communication between independent services.

The system simulates a simple order workflow where an order is created and multiple services react to the same event independently.

**🏗️ Architecture**

<pre>
OrderService (Publisher)
        │
        ▼
   Kafka Topic: order-created
        │
 ┌──────┴───────────────┐
 ▼                      ▼
InventoryService   NotificationService
(Consumer)         (Consumer)
</pre>

**⚙️ Tech Stack**

- .NET 8 Web API  
- Apache Kafka  
- Docker  
- SQL Server (not implemented in this demo)  
- Confluent.Kafka (.NET client)

**🧩 Microservices**

**1. OrderService**
- Exposes REST API  
- Publishes order events to Kafka topic `order-created`

**2. InventoryService**
- Consumes messages from Kafka  
- Simulates inventory update logic  

**3. NotificationService**
- Consumes messages from Kafka  
- Simulates sending notifications (email/SMS)
  
**🐳 Prerequisites**
- Docker Desktop installed and running
- .NET 8 SDK
-  Visual Studio 2022+


**▶️ How to Run the Project**
1. Start Kafka and Services via Docker
`docker-compose up -d`
2. Run Microservices
    - Open the solution in Visual Studio:
    - Set Multiple Startup Projects:

      . OrderService  
      . InventoryService  
      . NotificationService  


    - Click Start (F5) to run all services.

3. Test the Flow
     - Open Swagger UI for OrderService
     Call:
          POST /api/orders

Example request:

                {
                  "id": 1,
                  "productName": "Laptop",
                  "quantity": 2
                }

                
**🔄 Event Flow**

1. Order is created via API  
2. OrderService publishes an event to Kafka  
3. Kafka distributes the message to:
   - InventoryService  
   - NotificationService  
4. Each service processes the event independently  
    
**📌 Kafka Topic**
    - order-created
