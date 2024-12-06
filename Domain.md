### Domains in the Application

The Domain Layer of the application is the core layer that defines the essential entities, data transfer objects (DTOs), and policies for the system. It serves as the bridge between the business logic and the underlying data, ensuring a clean separation of concerns and maintainability.


## Key Elements in the Domain Layer

### Models
Models represent the main data structures used across the application. They define the core entities such as `User` and `BlogPost`.

Example: `BlogPost` Model 

The `BlogPost` model defines the structure of a blog post, including its relationship with the `User` model.

```csharp
public class BlogPost {
public int Id { get; set; }
public User Author { get; private set; }
public int AuthorId { get; set; }
public string Title { get; private set; }
public string Content { get; private set; }
public string Country { get; private set; }
public DateTime CreatedAt { get; set; }
public bool IsPublished { get; set; }

public BlogPost(int authorId, string title, string content, string country) { AuthorId = authorId; Title = title; Content = content; Country = country; CreatedAt = DateTime.UtcNow; IsPublished = false; } }
```

# Key Properties:
•	`AuthorId`: Links the blog post to its creator.
•	`IsPublished`: Indicates whether the blog post is published.
•	`CreatedAt`: Automatically records when the blog post was created.

Example: User Model The User model represents a registered user of the application.
```csharp
public class User {
public int Id { get; set; }
public string UserName { get; set; }
public string Password { get; set; }
public string Email { get; set; }
public string Name { get; set; }
public string Role { get; set; }
public int SecurityLevel { get; set; } }
```
# Key Properties:
o	Role: Defines the user's role (e.g., Administrator, User).
o	SecurityLevel: Controls access based on hierarchical permissions.
o	Password: Stored in plain text for simplicity but should ideally be hashed.

### Data Transfer Objects (DTOs)
DTOs are lightweight objects used to transfer data between the frontend, backend, and database layers. They decouple the internal structure of models from external APIs.

# BlogPost DTOs:
•	`BlogPostCreationDto`: Used for creating a new blog post.
•	`BlogPostUpdateDto`: Used for updating an existing blog post.
•	`BlogPostBasicDto`: Represents a simplified view of a blog post for frontend display.
•	`SearchBlogPostParametersDt`o: Allows filtering and searching of blog posts.

Example: BlogPostBasicDto that provides a read-only, summarized representation of a blog post.
```csharp
public class BlogPostBasicDto
{
    public int Id { get; }
    public string AuthorName { get; }
    public string Title { get; }
    public string Content { get; }
    public string Country { get; }
    public bool IsPublished { get; }

    public BlogPostBasicDto(int id, string authorName, string title, string content, string country, bool isPublished)
    {
        Id = id;
        AuthorName = authorName;
        Title = title;
        Content = content;
        Country = country;
        IsPublished = isPublished;
    }
}
```

### User DTOs:
•	`UserCreationDto`: Contains data required to create a new user.
•	`UserLoginDto`: Captures the credentials needed for user login.
•	`SearchUserParametersDto`: Supports filtering users by specific criteria.
Example: UserCreationDto simplifies the user creation process by only including essential data.
```csharp
public class UserCreationDto
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
    public int SecurityLevel { get; set; }

    public UserCreationDto(string userName, string password, string email, string name, string role, int securityLevel)
    {
        UserName = userName;
        Password = password;
        Email = email;
        Name = name;
        Role = role;
        SecurityLevel = securityLevel;
    }
}
```
