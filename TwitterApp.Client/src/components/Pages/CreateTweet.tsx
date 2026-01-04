import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import axiosInstance from "../../api/axiosInstance";

interface CreatePostRequest {
  content: string;
  retweetOfPostId?: number | null;
}

const CreateTweet: React.FC = () => {
  const [content, setContent] = useState("");
  const [error, setError] = useState("");
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError("");

    const postData: CreatePostRequest = {
      content,
      retweetOfPostId: null, 
    };

    try {
      await axiosInstance.post("/Post", postData);
      navigate("/feed"); 
    } catch (err: any) {
      setError(err.response?.data?.errors?.[0] || "Failed to create tweet");
    }
  };

  return (
    <div className="min-h-screen flex items-center justify-center bg-black text-white">
      <form
        className="bg-gray-900 p-8 rounded shadow-md w-full max-w-md"
        onSubmit={handleSubmit}
      >
        <h2 className="text-2xl font-bold mb-6 text-center">Create Tweet</h2>
        {error && <p className="text-red-500 mb-4">{error}</p>}

        <textarea
          placeholder="What's happening?"
          value={content}
          onChange={(e) => setContent(e.target.value)}
          className="w-full p-3 border border-gray-700 rounded mb-4 bg-black text-white resize-none"
          rows={5}
          required
        />

        <button
          type="submit"
          className="w-full bg-blue-500 hover:bg-blue-600 py-3 rounded"
        >
          Tweet
        </button>

        <button
          type="button"
          onClick={() => navigate("/feed")}
          className="w-full mt-4 bg-gray-700 hover:bg-gray-600 py-3 rounded"
        >
          Cancel
        </button>
      </form>
    </div>
  );
};

export default CreateTweet;
