# 2. Web Service Design & Implementation

## 1. How We Worked with the RESTful Web API

The RESTful web API was developed using ASP.NET Core, adhering to standard REST principles to manage authentication, user management, and blog posts. The API was designed to allow secure communication using JWT-based authentication and to ensure efficient CRUD operations.

The development process involved separating responsibilities into controllers, logic interfaces, services, and data access objects (DAOs) to promote modularity and maintainability. Authentication is handled through the `AuthController`, which issues JWT tokens for authenticated users. These tokens are then used to access protected endpoints in other controllers such as `BlogPostsController`.

---

### BlogPostsController

The `BlogPostsController` handles blog post operations such as creating, reading, updating, and deleting blog posts. It enforces authorization to ensure only authenticated users can perform actions, and ownership checks to restrict modifications to a user's own posts.

- **CreateAsync**: Associates a blog post with the authenticated user by extracting their `UserId` from the JWT.
- **GetAsync**: Allows filtering posts by query parameters such as `userName`, `publishedStatus`, and more.
- **UpdateAsync/DeleteAsync**: Restricts actions to the owner of the blog post.

#### Key Features:
- CRUD operations.
- Uses user-specific claims from JWT to manage ownership and access.

---

### UsersController

The `UsersController` manages user registration and retrieval. It ensures that new user registrations are validated and provides mechanisms to fetch user information.

- **CreateAsync**: Registers a new user after validating inputs.
- **GetAsync**: Fetches user details, with optional filtering by username.
- **GetUserByIdAsync**: Retrieves a user by their unique ID.

#### Key Features:
- Implements validation for user input.
- Provides endpoints for user data retrieval, essential for managing relationships with other resources like blog posts.

---

### TestController

The `TestController` serves as a utility controller for testing authentication and authorization configurations. It includes endpoints for anonymous and authorized access.

- **allowanon**: Tests endpoints that do not require authentication.
- **authorized**: Verifies that a valid JWT is required for access.
- **manualcheck**: Demonstrates manual role-based access control by extracting claims from the JWT.

---

### AuthController

#### Code Example: JWT Token Generation

The `AuthController` generates JWT tokens with user-specific claims:

```csharp
private string GenerateJwt(User user)
{
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes(config["Jwt:Key"] ?? "");

    List<Claim> claims = GenerateClaims(user);

    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.UtcNow.AddHours(1),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        Issuer = config["Jwt:Issuer"],
        Audience = config["Jwt:Audience"]
    };

    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
}
