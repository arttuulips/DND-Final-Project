# 3. Web Application

## 1. Key Requirements Implementation

The web application is implemented using **Blazor WebAssembly** and relies on a **RESTful API** to provide functionality for managing users and blog posts. Below are the key requirements and their implementations:

---

### Authentication

- **JWT (JSON Web Token)** is used to authenticate users.
- Users provide their credentials (username and password) on the login page (`Login.razor`), which are sent to the backend. If valid, the backend returns a JWT used for subsequent requests.

**Example: Authentication Code in Login.razor**  
Where the `authService.LoginAsync` method sends the user's credentials to the backend `/auth/login` endpoint.

```csharp
private async Task LoginAsync()
{
    errorLabel = "";
    try
    {
        await authService.LoginAsync(userName, password);
        navMgr.NavigateTo("/");
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        errorLabel = $"Error: {e.Message}";
    }
}
```

### Blog Post Management
**The application allows users to:**
•	Create Blog Posts (CreateBlogPost.razor).
•	Browse Blog Posts (BrowseBlogPosts.razor).

**The application allows administrator to:***
•	Create Blog Posts (CreateBlogPost.razor).
•	View Blog Posts (ViewBlogPosts.razor).
•	Edit Blog Posts (EditBlogPost.razor).
•	Delete Blog Posts (ViewBlogPosts.razor).

Example: Creating BlogPosts in CreateBlogPost.razor where the BlogPostCreationDto is sent to the backend using the blogPostService.CreateAsync method.
```csharp
private async Task Create() { BlogPostCreationDto dto = new(blogPostTitle, blogPostContent, blogPostCountry); await blogPostService.CreateAsync(dto); showModal = true; }
```

### Filtering and Searching Blog Posts
•	Users can filter blog posts by attributes like username, country, or content.

Example: Filter Logic in BrowseBlogPost.razor where belove method interacts with the backend to fetch blog posts matching the given filters
```csharp

private async Task LoadBlogPosts() { blogPosts = await blogPostService.GetAsync( usernameFilter, userIdFilter, true, titleContainsFilter, contentContainsFilter, countryContainsFilter ); }
```
---

## 2. Overview of Pages

**Home Page (Home.razor)** 

•	Purpose: Acts as the landing page, introducing the platform with a carousel and a brief overview.
•	Features: Modern, responsive design with Bootstrap for styling.


---

**Login Page (Login.razor)**

•	Purpose: Allows users to log in by entering their credentials.
•	Features:
o	Sends a login request to the backend using authService.
o	Displays error messages for invalid credentials.


---

**Create Blog Post Page (CreateBlogPost.razor)**

•	Purpose: Enables users to create a new blog post.
•	Features:
o	Validates user input (e.g., title, content, country).
o	Sends the data to the backend using blogPostService.

---

**View Blog Posts Page (ViewBlogPosts.razor)**

•	Purpose: Displays a table of all blog posts with options to edit or delete.
•	Features:
o	Includes filtering options for attributes like username or country.
o	Displays a "Published" toggle button for each post.


---

**Edit Blog Post Page (EditBlogPost.razor)**

•	Purpose: Allows administrator to modify users  an existing blog post.
•	Features:
o	Sends update requests to the backend using blogPostService.


---

**View Users Page (ViewUsers.razor)**

•	Purpose: Lists all users in the system.
•	Access: Restricted to administrators using the "MustBeAdmin" policy.

---


## 3. Frontend and Backend Integration
The application uses Blazor WebAssembly to communicate with the backend RESTful API. The frontend interacts with the backend using HttpClient and various service interfaces (IUserService, IBlogPostService).

**Configuring HttpClient**
In Program.cs, HttpClient is configured for the application:
```csharp
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7159") });
```

Example: Blog Post Service Integration
The BlogPostHttpClient service handles CRUD operations for blog posts  where service is injected into Blazor pages like BrowseBlogPost.razor to display blog posts.
```csharp

public async Task<IEnumerable<BlogPost>> GetAsync(string usernameFilter, int? userIdFilter, bool? publishedFilter, string titleFilter, string contentFilter, string countryFilter) { string queryString = $"?username={usernameFilter}&userId={userIdFilter}&published={publishedFilter}&title={titleFilter}&content={contentFilter}&country={countryFilter}"; HttpResponseMessage response = await client.GetAsync($"BlogPosts{queryString}"); return await response.Content.ReadFromJsonAsync<IEnumerable<BlogPost>>(); }

```







