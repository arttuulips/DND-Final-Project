# Web Service Design & Implementation

## 1. How We Worked with the RESTful Web API

The RESTful web API was developed using ASP.NET Core, adhering to standard REST principles to manage authentication, user management, and blog posts. The API was designed to allow secure communication using JWT-based authentication and to ensure efficient CRUD operations. The development process involved separating responsibilities into controllers, logic interfaces, services, and data access objects (DAOs) to promote modularity and maintainability.

Authentication is handled through the `AuthController`, which issues JWT tokens for authenticated users. These tokens are then used to access protected endpoints in other controllers such as `BlogPostsController`.

### BlogPostsController

The `BlogPostsController` handles blog post operations such as creating, reading, updating, and deleting blog posts. It enforces authorization to ensure only authenticated users can perform actions, and ownership checks to restrict modifications to a user's own posts.

- **CreateAsync**: Associates a blog post with the authenticated user by extracting their `UserId` from the JWT.
- **GetAsync**: Allows filtering posts by query parameters such as `userName`, `publishedStatus`, and more.
- **UpdateAsync/DeleteAsync**: Restricts actions to the owner of the blog post.

**Key Features:**
- CRUD operations.
- Uses user-specific claims from JWT to manage ownership and access.

---

### UsersController

The `UsersController` manages user registration and retrieval. It ensures that new user registrations are validated and provides mechanisms to fetch user information.

- **CreateAsync**: Registers a new user after validating inputs.
- **GetAsync**: Fetches user details, with optional filtering by username.
- **GetUserByIdAsync**: Retrieves a user by their unique ID.

**Key Features:**
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
```
### Key Features of the Web API:
## 1. Authentication: Secured Using JWT Tokens <br?
Authentication is a core feature of the API and is implemented using JWT (JSON Web Token). JWT tokens ensure stateless, secure communication between the client and the server by embedding user-specific claims, such as UserId, Role, and Email. Upon successful login through the AuthController, a JWT is generated and returned to the client. This token must be included in the Authorization header for subsequent API requests to protected endpoints.
# Key Implementation in AuthController:
•	`POST /auth/login`: Verifies user credentials and generates a token.
•	GenerateJwt(User user) method: Encodes claims into the token and signs it using a symmetric key.

The token is validated on every request to ensure that only authenticated users can access protected resources.
```csharp
•	var tokenDescriptor = new SecurityTokenDescriptor
{
    Subject = new ClaimsIdentity(claims),
    Expires = DateTime.UtcNow.AddHours(1),
    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
    Issuer = config["Jwt:Issuer"],
    Audience = config["Jwt:Audience"]
};
```
## 2. Filtering: Querying Blog Posts

The API supports filtering mechanisms for blog posts through query parameters. This is implemented in the `BlogPostsController` with support from the `BlogPostEfcDao`. Users can filter blog posts by various parameters such as `UserName`, `UserId`, `Title`, and `Content`.

# Key Implementation in BlogPostEfcDao:
- The `GetAsync` method builds an `IQueryable` query dynamically based on the provided parameters. This design ensures flexibility, allowing users to specify one or more filters in their request.

Example filtering logic:

```csharp
if (!string.IsNullOrEmpty(searchParameters.UserName))
{
    query = query.Where(blogPost =>
        blogPost.Author.UserName.ToLower().Equals(searchParameters.UserName.ToLower()));
}

if (!string.IsNullOrEmpty(searchParameters.ContentContains))
{
    query = query.Where(t =>
        t.Content.ToLower().Contains(searchParameters.ContentContains.ToLower()));
}
```

`GET /blogPosts?userName=JohnDoe&titleContains=Travel` endpoint retrieves all blog posts authored by JohnDoe with titles containing the word "Travel."

## 3.Authorization: Enforced with [Authorize] Attributes
Authorization ensures that only users with valid credentials and permissions can access specific resources. The [Authorize] attribute is used to enforce authentication for endpoints that require it. Additionally, role-based access control (RBAC) is implemented by verifying claims embedded in the JWT.
Example Implementation in BlogPostsController:
•	[Authorize] ensures that only authenticated users can access the POST endpoint for creating blog posts.
•	Ownership validation uses the UserId claim from the JWT to ensure that users can only modify their own posts.
```csharp
var userId = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
BlogPost created = await blogPostLogic.CreateAsync(dto, userId);
```
## 4. Separation of Concerns
The API adheres to the principle of Separation of Concerns by organizing responsibilities into layers:
1.	`Controllers`: Handle HTTP requests, validate inputs, and return HTTP responses.
2.	`Services/Logic`: Contain business rules and logic for processing data.
3.	`Data Access Objects (DAOs)`: Interact with the database to perform CRUD operations.
This design promotes modularity, testability, and scalability. 
•	`AuthService` handles user validation and registration logic.
•	`BlogPostEfcDao` manages database interactions for blog posts.
•	`BlogPostsController` delegates business logic to IBlogPostLogic.
Example: BlogPost Data Access `(BlogPostEfcDao)` The BlogPostEfcDao uses Entity Framework Core to interact with the database ensures that database operations are abstracted, keeping the controller and service layers independent of the underlying data storage.

```csharp
public async Task<BlogPost?> GetByIdAsync(int blogPostId)
{
    BlogPost? found = await context.BlogPosts
        .Include(blogPost => blogPost.Author)
        .SingleOrDefaultAsync(blogPost => blogPost.Id == blogPostId);
    return found;
}
Example: User Data Access (UserEfcDao)  where the UserEfcDao retrieves user information from the database, ensuring that controllers only focus on HTTP logic.
public async Task<User?> GetByUsernameAsync(string userName)
{
    User? existing = await context.Users.FirstOrDefaultAsync(u =>
        u.UserName.ToLower().Equals(userName.ToLower())
    );
    Console.WriteLine($"User {existing?.Id} has been found");
    return existing;
}
```
### How we use file storage to store data
Before switching to Entity Framework Core (EFC) DAOs, the application was using a file-based storage system implemented using JSON files. The key idea was to store all data in a single JSON file (data.json) and use in-memory collections to manage and manipulate data during the application's runtime. Here's how the system worked:

## 1. File Storage Architecture
The file storage system was organized as follows:
•	`DataContainer`: Acts as a simple container for in-memory data during runtime.
•	`FileContext`: Handles file reading, writing, and in-memory data management.
•	`DAOs (Data Access Objects)`: Provide an abstraction layer to interact with data and perform CRUD operations.

## 2. Detailed Explanation of Components
```csharp
DataContainer
```
This class defines the structure of the data to be stored in data.json. It includes two collections:
•	`Users`: A collection of User objects.
•	`BlogPosts`: A collection of BlogPost objects.
```csharp
public class DataContainer
{
    public ICollection<User> Users { get; set; }
    public ICollection<BlogPost> BlogPosts { get; set; }
}
```
When the application starts, this structure is loaded into memory from the JSON file.
```csharp
FileContext
```
This class provides core functionality to load and save data to the JSON file.
•	Lazy Loading: Data is only loaded when accessed, using the `LoadData` method. If the JSON file doesn't exist, it creates a new `DataContainer` with empty lists.
```csharp
string content = File.ReadAllText(FilePath);
dataContainer = JsonSerializer.Deserialize<DataContainer>(content);
```
# Saving Data
Changes to data are saved to the JSON file by serializing the in-memory DataContainer.
```csharp
DAOs
```
The DAOs provide abstraction to interact with the data in a structured manner. They use the `FileContext` to manipulate the in-memory data and persist changes to the JSON file.
•	`BlogPostFileDao`: Implements methods for managing BlogPost data.
•	`CreateAsync`: Assigns a new unique ID, adds the BlogPost to the collection, and saves changes.
```csharp
public Task<BlogPost> CreateAsync(BlogPost blogPost)
{
    int id = 1;
    if (context.BlogPosts.Any())
    {
        id = context.BlogPosts.Max(t => t.Id);
        id++;
    }

    blogPost.Id = id;
    
    context.BlogPosts.Add(blogPost);
    context.SaveChanges();

    return Task.FromResult(blogPost);
}
```
# Query and Filtering
Fetches blog posts based on provided search parameters, such as title, content, or author details.
```csharp
public Task<IEnumerable<BlogPost>> GetAsync(SearchBlogPostParametersDto searchParams)
public Task<IEnumerable<BlogPost>> GetAsync(SearchBlogPostParametersDto searchParams)
```
`UserFileDao`: Handles operations for the User data in a similar manner.

# `CreateAsync`: Adds a new user with a unique ID and saves changes.
```csharp
public Task<User> CreateAsync(User user)
{
    int userId = 1;
    if (context.Users.Any())
    {
        userId = context.Users.Max(u => u.Id);
        userId++;
    }

    user.Id = userId;

    context.Users.Add(user);
    context.SaveChanges();

    return Task.FromResult(user);
}
```
# Search and Retrieve
Implements methods to retrieve users by username, ID, or search parameters.

## 3. Workflow Before Switching to EFC
# 1.Initialize FileContext
The application creates an instance of FileContext to handle data storage.
```csharp
var fileContext = new FileContext();
```
# 2. DAO Interactions
The DAOs (e.g., `BlogPostFileDao`, `UserFileDao`) are instantiated with `FileContext` and used for operations.
Example: Creating a new blog post.
```csharp
var blogPostDao = new BlogPostFileDao(fileContext);
var newPost = new BlogPost { Title = "First Post", Content = "Hello World!" };
await blogPostDao.CreateAsync(newPost);
```
# 3. Persistence
Changes made via DAOs are saved to data.json when `SaveChanges` is called in the `FileContext`.
Example JSON structure in data.json:
```csharp{
  "Users": [
    {
      "Id": 1,
      "UserName": "Natalia",
      "BlogPosts": []
    },
    "BlogPosts": [
  {
    "Id": 2,
    "Author": {
      "Id": 5,
      "UserName": "Snookie",
      "BlogPosts": []
    },
    "Title": "Holiday at my grandparents",
    "Content": "I love them the most",
    "Country": "Poland",
    "CreatedAt": "2024-11-30T23:37:28.3436941Z",
    "IsPublished": false
  },
```



