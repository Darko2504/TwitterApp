import React, { useState } from "react";
import axiosInstance from "../../api/axiosInstance";
import type { LoginRequest, AuthResponse, CustomResponse } from "../../types";
import { useNavigate } from "react-router-dom";

const Login: React.FC = () => {
  const navigate = useNavigate();
  const [form, setForm] = useState<LoginRequest>({
    username: "",
    password: "",
  });
  const [error, setError] = useState("");

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      const res = await axiosInstance.post<CustomResponse<AuthResponse>>(
        "/Auth/login",
        form
      );

      if (res.data.isSuccessfull && res.data.result) {
        localStorage.setItem("token", res.data.result.token);
        navigate("/feed");
      } else {
        setError(res.data.errors[0] || "Login failed");
      }
    } catch (err: any) {
      setError(err.response?.data?.errors?.[0] || "Login failed");
    }
  };

  return (
    <div className="flex min-h-screen w-full">
      {/* Left side: login form */}
      <div className="w-1/2 flex items-center justify-center bg-gray-50">
        <form
          className="bg-white p-12 rounded-xl shadow-lg w-4/5 max-w-2xl"
          onSubmit={handleSubmit}
        >
          <h2 className="text-4xl font-bold mb-8 text-blue-500 text-center">
            TwitterApp
          </h2>
          {error && <p className="text-red-500 mb-4">{error}</p>}
          <input
            type="text"
            name="username"
            placeholder="Username"
            value={form.username}
            onChange={handleChange}
            className="w-full p-3 border rounded mb-4 focus:outline-none focus:ring-2 focus:ring-blue-500"
            required
          />
          <input
            type="password"
            name="password"
            placeholder="Password"
            value={form.password}
            onChange={handleChange}
            className="w-full p-3 border rounded mb-4 focus:outline-none focus:ring-2 focus:ring-blue-500"
            required
          />
          <button
            type="submit"
            className="w-full bg-blue-500 text-white py-3 rounded hover:bg-blue-600 transition"
            onClick={() => navigate("/feed")}
          >
            Login
          </button>
          <p className="mt-4 text-sm text-right">
            Don't have an account?{" "}
            <span
              className="text-blue-500 cursor-pointer"
              onClick={() => navigate("/register")}
            >
              Register
            </span>
          </p>
        </form>
      </div>

      {/* Right side: logo */}
      <div className="w-1/2 flex items-center justify-center bg-blue-600">
        <img
          src="/logo.png" // put your logo in public/logo.png
          alt="TwitterApp Logo"
          className="w-2/3 h-2/3 object-contain"
        />
      </div>
    </div>
  );
};

export default Login;
