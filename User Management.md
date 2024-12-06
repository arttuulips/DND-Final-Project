# WebApp User Authentication and Role-Based Authorization

## User Management Overview

### 1. User Registration
- Users are registered through the `AuthService` in the WebAPI.
- Input validation ensures that `UserName` and `Password` are not null or empty.
- After validation, the `User` object is stored in the database via the `UserEfcDao`.

```csharp
private readonly IUserDao userDao;

public AuthService(IUserDao userDao) {
    this.userDao = userDao;
}

public Task RegisterUser(User user) {
    if (string.IsNullOrEmpty(user.UserName)) {
        throw new ValidationException("Username cannot be null");
    }
    if (string.IsNullOrEmpty(user.Password)) {
        throw new ValidationException("Password cannot be null");
    }
    return Task.CompletedTask;
}
```

### 2.	User Login:
-    The login page collects the username and password.
-    The JwtAuthService sends these credentials to the WebAPI endpoint (/auth/login).
-    If credentials are valid:
-    A JWT token is returned.
-    The token is cached in the client-side service (Jwt).
-    A ClaimsPrincipal is created from the token to manage the user's authenticated state.

```csharp
public async Task<User> ValidateUser(string userName, string password)

{

    User? existingUser = await userDao.GetByUsernameAsync(userName);
    
    if (existingUser == null)
    {
        throw new Exception("User not found");
    }

    if (!existingUser.Password.Equals(password))
    {
        throw new Exception("Password mismatch");
    }

    return await Task.FromResult(existingUser);
}

public async Task LoginAsync(string userName, string password)
{
    UserLoginDto userLoginDto = new()
    {
        UserName = userName,
        Password = password
    };

    string userAsJson = JsonSerializer.Serialize(userLoginDto);
    StringContent content = new(userAsJson, Encoding.UTF8, "application/json");

    HttpResponseMessage response = await client.PostAsync("https://localhost:7159/auth/login", content);
    string responseContent = await response.Content.ReadAsStringAsync();

    if (!response.IsSuccessStatusCode)
    {
        throw new Exception(responseContent);
    }

    string token = responseContent;
    Jwt = token;

    ClaimsPrincipal principal = CreateClaimsPrincipal();

    OnAuthStateChanged.Invoke(principal);
}

private  ClaimsPrincipal CreateClaimsPrincipal()
{
    if (string.IsNullOrEmpty(Jwt))
    {
        return new ClaimsPrincipal();
    }

    IEnumerable<Claim> claims = ParseClaimsFromJwt(Jwt);
    
    ClaimsIdentity identity = new(claims, "jwt");

    ClaimsPrincipal principal = new(identity);
    return principal;
}
```
### 3. Authentication State Management:
-    The CustomAuthProvider synchronizes the authentication state with Blazor's AuthenticationStateProvider.
-    It uses the IAuthService to get or update the user's current claims.
-    When the authentication state changes (e.g., login or logout), NotifyAuthenticationStateChanged is triggered to update UI components.

```csharp
public override async Task<AuthenticationState> GetAuthenticationStateAsync()
{
    ClaimsPrincipal principal = await authService.GetAuthAsync();
    
    return new AuthenticationState(principal);
}

private void AuthStateChanged(ClaimsPrincipal principal)
{
    NotifyAuthenticationStateChanged(
        Task.FromResult(
            new AuthenticationState(principal)
        )
    );
}
```
### 4.	Authorization:
-    Policies: Defined in AuthorizationPolicies to enforce role- or security-level-based access control.
-    Example policies:
-    SecurityLevel4: Requires authenticated users with a SecurityLevel claim of 4 or 5.
-    MustBeAdmin: Requires the user to have the Administrator role.
-    SecurityLevel2OrAbove: Requires a SecurityLevel claim of at least 2.

```csharp
public static void AddPolicies(IServiceCollection services)
{
    services.AddAuthorizationCore(options =>
    {
        options.AddPolicy("SecurityLevel4", a =>
            a.RequireAuthenticatedUser().RequireClaim("SecurityLevel", "4", "5"));

        options.AddPolicy("MustBeAdmin", a =>
            a.RequireAuthenticatedUser().RequireClaim("Role", "Administrator"));

        options.AddPolicy("SecurityLevel2OrAbove", a =>
            a.RequireAuthenticatedUser().RequireAssertion(context =>
            {
                Claim? levelClaim = context.User.FindFirst(claim => claim.Type.Equals("SecurityLevel"));
                if (levelClaim == null) return false;
                return int.Parse(levelClaim.Value) >= 2;
            }));
    });
}

```

### 5. Login Page
-    Implements a form for user login:
-    Collects username and password.
-    On login success, navigates to the home page.
-    On failure, displays an error message.
-    Uses the AuthorizeView component to toggle UI visibility for authorized/unauthorized users.

```csharp
<AuthorizeView>
    <NotAuthorized>
        <div class="card">
            <h3>Please login</h3>
            <div class="field">
                <label>User name:</label>
                <input type="text" @bind="userName" />
            </div>
            <div class="field">
                <label style="text-align: center">Password:</label>
                <input type="password" @bind="password" />
            </div>
            @if (!string.IsNullOrEmpty(errorLabel))
            {
                <div class="field">
                    <label style="color: red">
                        @errorLabel
                    </label>
                </div>
            }
            <div class="field">
                <button class="loginbtn" @onclick="LoginAsync">Log in</button>
            </div>
        </div>
    </NotAuthorized>
    <Authorized>
    </Authorized>
</AuthorizeView>
```
