
# TwitterApp Clone

A Twitter-like social media application built with **React + TypeScript** on the frontend and **ASP.NET Core** with **C#** on the backend. Users can register, log in, create tweets, like/unlike, retweet, and view other user profiles.

---

## 🚀 Features

- User registration and login with JWT authentication
- Create, like, unlike, and retweet posts
- View a personalized feed
- User profile pages with posts
- Responsive UI with **Tailwind CSS**
- Smooth navigation using **React Router**

---

## 🖥️ Technologies Used

**Frontend:**

- React 18 + TypeScript
- Tailwind CSS for styling
- Axios for API calls
- React Router for navigation

**Backend:**

- ASP.NET Core Web API
- C# and Entity Framework Core
- ASP.NET Identity for user management
- AutoMapper for DTO mapping
- JWT-based authentication
- PostgreSQL / SQL Server (or your chosen DB)

**Development Tools:**

- Vite (Frontend bundler)
- npm for dependency management
- Visual Studio / VS Code
- Git & GitHub

---


## 💻 Running Locally

### Backend

1. Open the `TwitterApp.Server` project in **Visual Studio** or VS Code.
2. Update the connection string in `appsettings.json` to match your database.
3. Run database migrations (if using EF Core):
   ```bash
   dotnet ef database update
Start the backend server:

dotnet run
Default API URL: http://localhost:5058/api

Frontend
Navigate to the frontend folder:
cd TwitterApp.Client
Install dependencies:
npm install
Start the frontend development server:
npm run dev
Open your browser at http://localhost:5173 (or the URL Vite outputs).

🏗️ Architecture Overview

Backend uses all of the good practices like:
-Repository pattern for data manipulation and security
-N-Tier architecture for good structure, readibility and scalability

Frontend communicates with the backend API via Axios.

JWT tokens handle authentication. Tokens are stored in localStorage.

Posts and user actions (like/retweet) are processed via backend services, then returned as DTOs.

The app is fully component-based with reusable PostCard, Feed, Login/Register forms, and UserProfile pages.

Tailwind CSS ensures a responsive, clean design.

📌 Notes
Ensure the backend is running before starting the frontend.

JWT token expiration will redirect users to the login page.

All sensitive data is securely handled on the backend.


Made with ❤️ by Darko Milanovski


