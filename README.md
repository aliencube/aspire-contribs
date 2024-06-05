# aspire-contribs
This is a collection of community contributed libraries for .NET Aspire

## Prerequisites

- JDK 17 and 21
- Springboot CLI

## Getting Started

### Spring Boot App with Maven

```bash
spring init \
    --build=maven \
    --format=project \
    --java-version=17 \
    --packaging=jar \
    --dependencies=web \
    --group-id com.aliencube.aspire.contribs \
    --artifact-id=spring-maven \
    --name=spring-maven \
    aspire-contribs-spring-maven
```