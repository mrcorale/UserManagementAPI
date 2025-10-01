# User Management API

A robust ASP.NET Core Web API for managing users with comprehensive authentication, authorization, and security features.

## 🚀 Features

- **CRUD Operations** - Full Create, Read, Update, Delete functionality for user management
- **JWT-style Authentication** - Token-based security middleware
- **Request Logging** - Comprehensive request/response logging
- **Error Handling** - Global exception handling with consistent error responses
- **Input Validation** - Data validation using Data Annotations
- **Swagger Documentation** - Interactive API documentation
- **HTTPS Enforcement** - Secure communication enforcement

## 🔐 Authentication & Authorization

### 📋 Authentication Requirements

**ALL API ENDPOINTS REQUIRE AUTHENTICATION** except:
- Swagger UI (`/swagger`)

### 🔑 How to Authenticate

Include the Authorization header in all requests:

```http
Authorization: Bearer valid-token-123
```

### 🛡️ Valid Token
The current valid token for testing is:
```
valid-token-123
```

### 🔒 Authentication Flow

1. **Request Interception**: Authentication middleware checks every incoming request
2. **Header Validation**: Verifies `Authorization` header exists and follows `Bearer {token}` format
3. **Token Validation**: Compares token against valid tokens
4. **Access Control**:
   - Valid token → Request proceeds to endpoint
   - Invalid/missing token → `401 Unauthorized` response

### 🚫 Unauthorized Responses

You'll receive `401 Unauthorized` with descriptive messages for:
- Missing Authorization header: `"Authorization header is required"`
- Invalid format: `"Invalid authorization format. Use 'Bearer {token}'"`
- Invalid token: `"Invalid or expired token"`

## 🏗️ Project Structure

```
UserManagementAPI/
├── Controllers/
│   └── UserManagementController.cs
├── Models/
│   └── User.cs
├── Services/
│   ├── IUserService.cs
│   └── UserService.cs
├── Middleware/
│   ├── AuthenticationMiddleware.cs
│   ├── ErrorHandlingMiddleware.cs
│   └── LoggingMiddleware.cs
├── Program.cs
└── README.md
```

## 📚 API Endpoints

All endpoints require authentication unless specified otherwise.

### 👥 User Management

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| `GET` | `/api/usermanagement` | Get all users | ✅ |
| `GET` | `/api/usermanagement/{id}` | Get user by ID | ✅ |
| `POST` | `/api/usermanagement` | Create new user | ✅ |
| `PUT` | `/api/usermanagement/{id}` | Update user | ✅ |
| `DELETE` | `/api/usermanagement/{id}` | Delete user | ✅ |

### 🔓 Public Endpoints (No Auth)

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/swagger` | Swagger UI documentation |

## 🛠️ Setup & Installation

### Prerequisites
- .NET 8.0 SDK
- Visual Studio 2022 or VS Code
- Postman (for testing)

### Installation Steps

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd UserManagementAPI
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Run the application**
   ```bash
   dotnet run
   ```

4. **Access the application**
   - API: `https://localhost:5051`
   - Swagger: `https://localhost:5051/swagger`

## 📋 Usage Examples

### 🔐 Authenticated Request Example

```http
GET https://localhost:5051/api/usermanagement
Authorization: Bearer valid-token-123
Content-Type: application/json
```

### 👤 Create User

```http
POST https://localhost:5051/api/usermanagement
Authorization: Bearer valid-token-123
Content-Type: application/json

{
  "name": "John Doe",
  "email": "john.doe@example.com",
  "department": "IT"
}
```

### 👤 Update User

```http
PUT https://localhost:5051/api/usermanagement/1
Authorization: Bearer valid-token-123
Content-Type: application/json

{
  "name": "John Smith",
  "email": "john.smith@example.com",
  "department": "HR"
}
```

## 🛡️ Security Features

### Authentication Middleware
- **Token Validation**: Validates Bearer tokens
- **Header Inspection**: Ensures proper Authorization header format
- **Public Route Exclusion**: Allows access to documentation without auth

### Security Headers
- **HTTPS Redirection**: Enforces secure connections
- **HttpOnly Cookies**: Prevents XSS attacks
- **Input Validation**: Protects against injection attacks

### Data Validation
- **Required Fields**: Name, Email, Department
- **Email Format**: Valid email address validation
- **String Length**: Appropriate length constraints

## 🚨 Error Handling

The API provides consistent error responses:

### Authentication Errors (401)
```json
{
  "error": "Authorization header is required"
}
```

### Validation Errors (400)
```json
{
  "name": ["Name is required"],
  "email": ["Invalid email format"]
}
```

### Server Errors (500)
```json
{
  "error": "An internal server error occurred"
}
```

## 🔧 Testing with Postman

1. **Create a new Collection**
2. **Add Authorization Header** to collection:
   - Type: `Bearer Token`
   - Token: `valid-token-123`
3. **Create requests** for each endpoint
4. **Test error scenarios** by removing/changing the token

## 📊 Logging

The application logs:
- ✅ Request method and path
- ✅ Response status codes
- ✅ Processing time
- ✅ Authentication events
- ✅ Error details

## 🎯 Copilot Integration

This project was developed with Microsoft Copilot assistance for:
- Code generation and scaffolding
- Bug identification and fixes
- Middleware implementation
- Security best practices
- Documentation generation

## 🔄 Development Notes

### Middleware Pipeline Order
1. **Error Handling** - Catch all exceptions
2. **Authentication** - Verify user identity
3. **Logging** - Track request/response
4. **Routing** - Handle endpoint requests

### Token Management
- Current implementation uses a hardcoded token for demonstration
- In production, integrate with proper JWT or Identity Server
- Token validation should be against a database or identity provider

## 📞 Support

For issues related to:
- **Authentication**: Check token format and validity
- **Authorization**: Verify token has required permissions
- **API Access**: Ensure proper endpoint URLs and headers

## 📄 License


This project is developed for educational purposes as part of the TechHive Solutions internal tools development.
