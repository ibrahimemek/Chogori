# ADR 001: Architectural Decision for Repository Structure

## Status
Accepted

## Context & Problem Statement
Our e-commerce platform architecture requires the development of 6 core microservices: `catalog`, `inventory`, `order`, `payment`, `shipping`, and `notification`. In addition, we need to manage shared infrastructure configurations (PostgreSQL, RabbitMQ, Redis, Elasticsearch) and common architectural abstractions. 

We need to decide on the structural organization of our source code repositories to ensure development efficiency, robust dependency management, and smooth CI/CD processes during the early phases of the project.

## Alternatives Considered

### Alternative 1: Polyrepo (Multi-Repo) Topology
Dedicating a separate Git repository for each individual microservice and the shared library.
* **Pros:** Strict boundary isolation; independent versioning; smaller, decoupled individual pull requests.
* **Cons:** High operational overhead for local setup; complex orchestration for multi-service cross-cutting changes; requires maintaining a private NuGet registry for sharing core abstractions.

### Alternative 2: Monorepo Topology
Housing all microservices, shared architectural kernel, and environment deployment scripts inside a single, unified Git repository.
* **Pros:** Single-command local environment initialization; atomic cross-service commits; simplified shared project references.
* **Cons:** Larger repository size over time; risk of accidental tight coupling if layer rules are violated; requires sophisticated CI/CD pipelines to prevent redundant deployments.

## Decision Outcome
We chosen **Alternative 2: Monorepo Topology**. All microservices are located in the `services/` directory, global infrastructure manifests in the `infrastructure/` directory, and the shared enterprise primitives in `shared/SharedKernel`.

---

## Consequences

### Positive Impacts (Pros):
1. **Unified Developer Experience (DX):** A new team member can clone a single repository and spin up the entire application mesh and infrastructure with one execution of `docker-compose up`.
2. **Simplified Shared Kernel:** Code sharing for critical DDD primitives (`BaseEntity`, `AggregateRoot`, `DomainEvent`) is managed via direct compiler-enforced local project references instead of continuous NuGet package publishing.
3. **Atomic Changes:** Architectural changes that span across multiple services or deployment environments can be reviewed, verified, and merged within a single Pull Request.

### Negative Impacts (Cons) & Mitigation:
1. **Monolithic CI/CD Risks:** A naive CI/CD workflow would trigger build pipelines for all six services simultaneously upon any commit. 
   * *Mitigation:* We will configure path-based triggers (`paths` filter in GitHub Actions) to run tests and builds exclusively for modified directories.
2. **Directory Growth:** The repository size will increase rapidly as end-to-end integration tests are introduced.
   * *Mitigation:* Regular maintenance of `.gitignore` configurations and utilizing Git sparse-checkout if needed in later phases.