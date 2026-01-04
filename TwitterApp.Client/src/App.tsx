import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Login from "./components/Auth/Login";
import Register from "./components/Auth/Register";
import Feed from "./components/Pages/Feed";
import CreateTweet from "./components/Pages/CreateTweet";
import UserProfilePage from "./components/Pages/UserProfile";


const App: React.FC = () => {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/feed" element={<Feed />} />
        <Route path="/create-tweet" element={<CreateTweet />} />
         <Route path="/user/:userId" element={<UserProfilePage />} />
      </Routes>
    </Router>
  );
};

export default App;
