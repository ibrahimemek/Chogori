# Distributed E-Commerce Microservices Platform

A production-ready, resilient e-commerce architecture engineered using modern .NET, Clean Architecture principles, Domain-Driven Design (DDD), and an event-driven messaging topology.

---

## Architectural Topology

The following diagram illustrates the structural decoupling of the ecosystem, demonstrating the "Database-per-Service" pattern and the centralized infrastructure backbone.

```mermaid
graph TD
    subgraph Applications [Application Layer]
        Catalog[Catalog API]
        Inventory[Inventory API]
        Order[Order API]
        Payment[Payment API]
        Shipping[Shipping API]
        Notification[Notification API]
    end

    subgraph Infrastructure [Infrastructure Backbone]
        CatDB[(Catalog Postgres)]
        InvDB[(Inventory Postgres)]
        OrdDB[(Order Postgres)]
        PayDB[(Payment Postgres)]
        RMQ[RabbitMQ Message Broker]
        Redis[(Redis Distributed Cache)]
        ES[(Elasticsearch Engine)]
    end

    Catalog --> CatDB
    Inventory --> InvDB
    Order --> OrdDB
    Order --> RMQ
    Payment --> PayDB
    Payment --> RMQ
    Shipping --> RMQ
    Notification --> RMQ