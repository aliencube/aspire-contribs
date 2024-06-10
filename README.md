# aspire-contribs
This is a collection of community contributed libraries for .NET Aspire

## Prerequisites

- [JDK 17+](https://learn.microsoft.com/java/openjdk/download)
- [Springboot CLI](https://docs.spring.io/spring-boot/installing.html#getting-started.installing.cli)
- [OpenTelemetry Agent for Java](https://github.com/open-telemetry/opentelemetry-java-instrumentation/releases/latest/download/opentelemetry-javaagent.jar)


## Getting Started



### Spring Boot App with Maven

```bash
spring init \
    --build=maven \
    --format=project \
    --java-version=17 \
    --packaging=jar \
    --dependencies=web \
    --group-id org.aliencube.aspire.contribs \
    --artifact-id=spring-maven \
    --name=spring-maven \
    aspire-contribs-spring-maven
```