## ASP.NET Web API Wrapper for Turkish National Id Check Service
The Turkish government provides a SOAP service for us to validate the national Id number of a person. This little application provides a simple Web API wrapper around this SOAP service. Main idea behind this application is to show how to consume a SOAP service from an ASP.NET Web API application.

## Endpoint
GET /api/nationalid?nationalid={your-turkish-national-id}&name={url-encoded-first-name-as-is}&surname={url-encoded-first-lastname-as-is}&year={birth-year}