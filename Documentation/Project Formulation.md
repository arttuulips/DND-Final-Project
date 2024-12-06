# 1. Project Formulation & Initial Requirements  

## Project Description (Domain)  
The "Wanderly" platform is a software application designed as a Travel Blog, allowing users to share their travel experiences and adventures. Users can register to create an account, securely log in, and contribute by submitting blog posts. These posts include titles, detailed content, and information about the countries visited, along with optional links to external sites and surveys for booking the experiences described and feedback.  

The platform also provides functionality for both registered and unregistered users to browse and explore stories, with features to filter by destination. Additionally, administrators have elevated security levels, thus privileges to moderate content and manage user accounts. Overall, the platform offers browsing and filtering capabilities for visitors, along with communication between the frontend and backend services.  

This project will be created using Blazor for a responsive and dynamic user interface, combined with ASP.NET Core Web API for a RESTful backend. User data and content will be stored securely using SQLite with ORM capabilities through Entity Framework Core (EFC). 

## Why This Project?  
Travel blogs are an exciting way for people to document and share their travel experiences, while also offering inspiration to others looking for new destinations or travel tips. Technically, this project covers essential software development concepts like authentication and CRUD operations, while also focusing on development practices such as RESTful APIs and using Blazor for an engaging frontend experience.

Our aim for implementing a travel blog is to create a platform that is practical, user-driven, and scalable. The project also allows us to deal with real-world challenges like managing user-generated content and securing user data.  

## Requirements (User Stories)  

### 1. User Registration & Login  
- **Registration**:  
  - As a new user, I want to register an account with my username and password to submit travel stories.  
- **Login**:  
  - As a registered user/admin, I want to securely log in to post new stories and manage travel stories.  

### Admin Role & Role based access  
- **Enhanced Permissions**:  
  - As an admin, I want the ability to perform all user actions (e.g., submit, edit, delete stories) and see user accounts to track platform usage.  
- **Moderation Tools**:  
  - As an admin, I want to review all user-submitted content to remove inappropriate or irrelevant stories from the platform.  

### 2. Travel Story Management (CRUD Operations)  
- **Story Submission**:  
  - As a user, I want to create a travel story by adding a title, content, and country details to share my experiences.  
- **Story Editing**:  
  - As an admin, I want to edit user submitted stories to update or correct details.  
- **Story Deletion**:  
  - As an admin, I want to delete user stories to manage content effectively.  

### 3. Story Viewing  
- **Browse Stories**:  
  - As a visitor, I want to view a list of travel stories to explore others' experiences without registration and logging in.  
  - As a user, I want to view other users’ stories.  

### 4. Filtering  
- As a visitor or user, I want to filter stories by country, name, id etc. to find relevant travel content.  

### 5. User Authentication & Security  
- **Secure Password Handling**:  
  - As a user and admin, I want my password to be securely hashed and stored to ensure data security.  
- **Session Management**:  
  - As a user and admin, I want a secure session management system to protect my account while logged in.  

## Delimitations  
Time constraints necessitate prioritizing core functionalities like user registration, login, CRUD operations for travel stories, and API integration with Blazor.  

Advanced features like media uploads, extensive admin controls beyond the described requirements, and full mobile responsiveness are earmarked as future enhancements.  

## Core Focus  
- Developing a secure, user-friendly platform.  
- Role-based access with enhanced admin functionalities.  
- Achieving communication between the Blazor frontend and ASP.NET Core Web API and database.  
- Meeting primary technical requirements for a functioning MVP.  

