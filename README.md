# Meraki Ping Api Interface

---

MerakiPingApiInterface is a simple API designed to ping functionality for devices. It offers endpoints to verify the API health and to perform ping operations on devices using their serial numbers and target addresses.

## API Version

**Version**: 1.0

## Endpoints

### Health Check

#### `GET /`

Returns the health status of the API.

- **Tags**: Health
- **Responses**:
  - **200 OK**
    - **Content Types**:
      - `text/plain`
      - `application/json`
      - `text/json`
    - **Schema**:
      - `$ref: #/components/schemas/Health`

### Ping Device

#### `GET /Ping`

Performs a ping operation on a device using its serial number and target address.

- **Tags**: Ping
- **Parameters**:
  - **deviceSerial** (query, string): The serial number of the device to be pinged.
  - **target** (query, string): The target address to ping.
- **Responses**:
  - **200 OK**

## Schemas

### Health

Represents the health status of the API.

- **Type**: Object
- **Properties**:
  - **data**: 
    - **$ref: #/components/schemas/HealthData**
- **Additional Properties**: False

### HealthData

Contains the health information.

- **Type**: Object
- **Properties**:
  - **health**: 
    - **Type**: Boolean
- **Additional Properties**: False


---

### Pre built container

>https://hub.docker.com/r/zimbres/meraki-ping-api-interface

---

A pre-compiled package is available for Windows and Linux. It requires ASP.NET Core Runtime 8.x.

[Download .NET 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
