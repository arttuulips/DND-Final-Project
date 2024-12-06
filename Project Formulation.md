# Wanderly Project: Travel Blog Platform  

## 1. Project Formulation & Initial Requirements  

### Project Description (Domain)  
The **"Wanderly"** platform is a software application designed as a **Travel Blog**, enabling users to share their travel experiences and adventures.  

#### Key Features:
- **User Accounts**:  
  - Register, securely log in, and contribute stories.  
  - Stories include titles, detailed content, countries visited, optional external links, and booking surveys.  
- **Story Browsing**:  
  - Both registered and unregistered users can browse and filter stories by destination.  
- **Administrative Tools**:  
  - Moderation tools to manage user accounts and content for platform integrity.  
- **Technology Stack**:  
  - **Frontend**: Blazor for a dynamic and responsive interface.  
  - **Backend**: ASP.NET Core Web API for RESTful services.  
  - **Database**: SQLite with Entity Framework Core (EFC) ORM for secure data storage.  

### Why This Project?  
This project combines the engaging concept of a travel blog with essential software development practices. It offers practical benefits for users while providing a solid technical foundation for the developer.  

By implementing features such as:  
- Authentication  
- User-generated content management  
- Role-based access  
- Future enhancements like search capabilities, media uploads, and responsive design  

The project aligns with modern development requirements while facilitating hands-on experience with Blazor, ASP.NET Core Web API, and SQLite.  

---

## Requirements (User Stories)  

### 1. User Registration & Login  
- **Registration**:  
  - As a new user, I want to register an account with my username and password to submit and manage travel stories.  
- **Login**:  
  - As a registered user, I want to securely log in to access my profile and manage my travel stories.  

### 2. Admin Role & Permissions  
- **Enhanced Permissions**:  
  - As an admin, I want the ability to perform all user actions (e.g., submit, edit, delete stories) and delete user accounts to maintain platform integrity.  
- **Moderation Tools**:  
  - As an admin, I want to review all user-submitted content to remove inappropriate or irrelevant stories from the platform.  

### 3. Travel Story Management (CRUD Operations)  
- **Story Submission**:  
  - As a user, I want to create a travel story by adding a title, content, and country details to share my experiences.  
- **Story Editing**:  
  - As a user, I want to edit my submitted stories to update or correct details.  
- **Story Deletion**:  
  - As a user, I want to delete my stories to manage my content effectively.  

### 4. Story Viewing  
- **Browse Stories**:  
  - As a visitor, I want to view a list of travel stories to explore others' experiences without registration and logging in.  
  - As a user, I want to seamlessly view other users’ stories.  

### 5. Country-Based Filtering  
- As a visitor or user, I want to filter stories by country to find relevant travel content.  

### 6. User Authentication & Security  
- **Secure Password Handling**:  
  - As a user and admin, I want my password to be securely hashed and stored to ensure data security.  
- **Session Management**:  
  - As a user and admin, I want a secure session management system to protect my account while logged in.  

---

## Delimitations  
Time constraints necessitate prioritizing core functionalities:  
- User registration, login, CRUD operations for travel stories, and API integration with Blazor.  

Advanced features such as:  
- Media uploads  
- Extensive admin controls beyond the described requirements  
- Full mobile responsiveness  

These are earmarked as future enhancements.  

---

## Core Focus  
- Developing a secure, user-friendly platform.  
- Role-based access with enhanced admin functionalities.  
- Communication between the Blazor frontend and ASP.NET Core Web API.  
- Meeting primary technical requirements for a functioning MVP.  
