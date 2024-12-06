# Data Access:
### How File Storage is Currently Used to Store Data
At present, the application does not directly utilize file-based storage for managing files like images, documents, or other media. Instead, the data is managed entirely within a `SQLite` database using `Entity Framework Core (EF Core)`. This database is a lightweight, file-based solution that ensures efficient, relational storage of entities such as `Users` and `BlogPosts`.
The `SQLite` database is stored as a `.db` file, configured in the `BlogPostContext` class, and acts as the primary storage medium for all structured data.
## 1. Database as File Storage
The `SQLite` database file (`BlogPost.db`) is stored on the server as defined in the `BlogPostContext` configuration:
```csharp
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    optionsBuilder.UseSqlite("Data Source = ../EfcDataAccess/BlogPost.db");
    optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
}
```
### File Location:
•	The database file (`BlogPost.db`) is stored in the `EfcDataAccess` directory relative to the project.
Data Structure:
•	Tables are created for entities such as `Users` and `BlogPosts`.
•	Relationships are defined between entities, such as linking blog posts to their authors.

## 2.Storing Structured Data
Entities like `Users` and `BlogPosts` are persisted in the `SQLite` database. `EF Core` is used to abstract database operations, allowing developers to interact with the data as objects rather than raw `SQL queries`.
Example: Blog Post Data Storage
When a blog post is created, its details are stored in the BlogPosts table. This includes attributes such as:
•	`Id`: Primary key.
•	`Title`, Content, and Country: Descriptive fields.
•	`CreatedAt`: Automatically set to the current timestamp using EF Core configuration.
```csharp
public async Task<BlogPost> CreateAsync(BlogPost blogPost)
{
    EntityEntry<BlogPost> added = await context.BlogPosts.AddAsync(blogPost);
    await context.SaveChangesAsync();
    return added.Entity;
}
```
## 3. Querying and Filtering Data
The application supports querying the database to retrieve specific data, such as blog posts by a particular user or those matching a search term. These queries are executed dynamically using `LINQ` and `EF Core`.
```csharp
public async Task<IEnumerable<BlogPost>> GetAsync(SearchBlogPostParametersDto searchParameters)
{
    IQueryable<BlogPost> query = context.BlogPosts.Include(blogPost => blogPost.Author).AsQueryable();

    if (!string.IsNullOrEmpty(searchParameters.UserName))
    {
        query = query.Where(blogPost =>
            blogPost.Author.UserName.ToLower().Equals(searchParameters.UserName.ToLower()));
    }

    if (searchParameters.UserId != null)
    {
        query = query.Where(t => t.Author.Id == searchParameters.UserId);
    }
    
    if (searchParameters.UserId != null)
    {
        query = query.Where(t => t.Author.Id == searchParameters.UserId);
    }
    
    if (!string.IsNullOrEmpty(searchParameters.ContentContains))
    {
        query = query.Where(t =>
            t.Content.ToLower().Contains(searchParameters.ContentContains.ToLower()));
    }

    if (!string.IsNullOrEmpty(searchParameters.CountryContains))
    {
        query = query.Where(t =>
            t.Country.ToLower().Contains(searchParameters.CountryContains.ToLower()));
    }

    if (!string.IsNullOrEmpty(searchParameters.TitleContains))
    {
        query = query.Where(t =>
            t.Title.ToLower().Contains(searchParameters.TitleContains.ToLower()));
    }
```



    List<BlogPost> result = await query.ToListAsync();
    return result;
}


# How LINQ Differs from Traditional SQL
Using `LINQ` (Language Integrated Query) provides a more integrated, type-safe, and object-oriented way of querying data compared to traditional `SQL`. Let's analyze the code provided and compare LINQ queries with the traditional `SQL` approach.
## LINQ Features in the Provided Code:
The `BlogPostEfcDao` and UserEfcDao classes rely on `Entity Framework Core (EF Core)`, which allows developers to use `LINQ` to query and manipulate data. `EF Core` translates these `LINQ` queries into `SQL` commands executed against the database.
## Filtering Blog Posts
LINQ Example (from BlogPostEfcDao.GetAsync):
```csharp
if (!string.IsNullOrEmpty(searchParameters.UserName))
{
    query = query.Where(blogPost =>
        blogPost.Author.UserName.ToLower().Equals(searchParameters.UserName.ToLower()));
}
 This query is written in C# and provides type safety. The database schema is abstracted away, and you work directly with objects like BlogPost and User while traditional SQL requires knowledge of database schema, relationships, and manual string concatenation for queries.

Fetching a Blog Post by ID
LINQ Example (from BlogPostEfcDao.GetByIdAsync)
BlogPost? found = await context.BlogPosts
    .Include(blogPost => blogPost.Author)
    .SingleOrDefaultAsync(blogPost => blogPost.Id == blogPostId);

LINQ include navigation properties (Author) easily using .Include(), which handles joins automatically  while traditional SQL requires explicit join conditions and selecting fields manually.

Filtering Users
LINQ Example (from UserEfcDao.GetAsync)
if (searchParameters.UsernameContains != null)
{
    usersQuery = usersQuery.Where(u =>
        u.UserName.ToLower().Contains(searchParameters.UsernameContains.ToLower()));
}
```
It simplifies writing filters using lambda expressions. The `.Where()` clause is intuitive and closely resembles natural language instead of manually constructing `LIKE` statements, which can be error-prone.

## Deleting a Blog Post
LINQ Example (from `BlogPostEfcDao.DeleteAsync`)
```csharp
context.BlogPosts.Remove(existing);
await context.SaveChangesAsync();
```
The deletion logic is encapsulated. You don't need to write raw `SQL`; `EF Core` translates the removal operation into the appropriate `SQL` command while traditional `SQL` requires manually constructing and executing the delete command.

## Advantages of LINQ
Type Safety: `LINQ` ensures that queries are validated at compile-time, reducing the risk of runtime errors.

