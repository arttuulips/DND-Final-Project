# Project Conclusion & Demonstration
## Group Dynamic
The project was initially undertaken by a group of five members. However, during the development process, differences in communication styles and work approaches led to challenges within the group. As a result, the group decided to split, leaving four members to continue working on the project.
Adding to these difficulties, a significant portion of the code was inadvertently removed from the main repository and could no longer be accessed. This setback forced us to restart the project almost entirely from scratch, which was highly stressful given the already tight timeline.
To address these challenges, the remaining team decided to adopt a more collaborative and structured approach. Following the guidance of Troels Mortensen, we implemented a system where all team members worked together closely. We sat down as a group and rebuilt the project step by step, ensuring clear communication and alignment at every stage. This second attempt was more cohesive and allowed us to complete the project despite the earlier setbacks.

## Summary of Project Outcome
Despite the initial challenges, the project was successfully completed. The second approach, characterized by collaboration and guided development, resulted in a functional platform that met the majority of the requirements outlined at the project's inception. Key functionalities such as user registration, login, role-based access, blog post creation, and post management were implemented, with special attention to secure authentication and modular design.

The Travel Blog application was completed as a functional platform for users to register, log in, and create, and view blog posts. The project met the key requirements, including secure authentication using JWT, role-based access control for administrators, and the ability for users to filter and browse blog posts by country. The blog post management for admin features, such as creating, editing, and deleting posts, were fully implemented. Although some additional features like advanced search and user profile management were not included, the application successfully fulfilled its core objectives, resulting in a well-rounded and secure travel blog platform.

## Updated Requirements List
### Implemented Features:
-  User Registration & Login: Fully functional with secure authentication mechanisms.
-  Submit Travel Stories: Users can create blog posts with country-specific tags.
-  View Travel Stories: Visitors and users can browse and filter stories by country.
-  Role-Based Access: Admins have extended permissions to see user accounts and perform CRUD operations on user submitted stories.
-  Secure Authentication: JWT-based authentication ensures user data and access are protected.
-  Data Base Connection: SQLite connection.

### Not Implemented Features (Future Scope):
-  Admin can manage user accounts.
-  A user can edit or delete their own old stories on the View Blog Posts page.
-  Users can follow other users and see their profile page.
-  Advanced search and filtering.
-  Profile management features beyond basic functionality.
-  Media uploads and extensive UI/UX improvements.

## Group Challenges and Resolutions
The development process was not without its hurdles. Two significant challenges stood out:

### 1.	Using GitHub and Managing Version Control
Initially, we faced difficulties in effectively using GitHub for version control, especially when it came to committing changes collaboratively. Synchronizing our work and resolving merge conflicts felt overwhelming at times. To overcome this, we decided to adopt a collaborative approach by working  on every part of the project together, and after each session, we ensured that the most up-to-date version of the project was saved and shared among team members to maintain consistency and avoid discrepancies. This decision improved efficiency and allowed us to focus on coding rather than dealing with technical issues in version control. Additionally, while this approach was more time-consuming, it ensured that every team member gained a thorough understanding of the entire project

### 2.	Compatibility with .NET 8.0
Another major challenge was navigating compatibility issues with NuGet packages and extensions in .NET 8.0. Contrary to our expectations, the latest versions of certain packages were not always compatible with .NET 8.0. This required extensive research to identify the appropriate versions and configurations. We spent considerable time troubleshooting these issues and seeking external help, whether from online documentation, forums, or knowledgeable peers. While this process was time-consuming, it ultimately enriched our understanding of dependency management and software ecosystems.

### 3.	Complexity and Accessibility of Provided Resources
   Another challenge we faced was that the resources provided were not always straight-forward or easy to comprehend. Even after reading them multiple times, we often found it difficult to fully grasp the concepts, as we are not software engineers by training. This lack of familiarity made it challenging to figure things out, as many aspects of the development process were not intuitive for us. It required significant effort, persistence, and additional research to overcome these obstacles and implement the required functionalities.
