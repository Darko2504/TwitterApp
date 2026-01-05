import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import axiosInstance from "../../api/axiosInstance";
import type { UserProfile } from "../../types";

const UserProfilePage: React.FC = () => {
  const { userId } = useParams<{ userId: string }>();
  const navigate = useNavigate();

  const [user, setUser] = useState<UserProfile | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
    const token = localStorage.getItem("token");
    if (!token) {
      navigate("/login");
      return;
    }

    if (userId) {
      fetchUserProfile(userId, token);
    }
  }, [userId]);

  const fetchUserProfile = async (id: string, token: string) => {
    try {
      const res = await axiosInstance.get(`/User/profile/${id}`, {
        headers: { Authorization: `Bearer ${token}` },
      });

      if (res.data?.result) {
        setUser(res.data.result);
      } else {
        setError("User not found.");
        setUser(null);
      }
    } catch (err) {
      console.error("Failed to fetch user profile", err);
      setError("User not found.");
      setUser(null);
    } finally {
      setLoading(false);
    }
  };

  if (loading) {
    return <div className="text-white p-6">Loading user...</div>;
  }

  if (error || !user) {
    return <div className="text-white p-6">{error || "User not found."}</div>;
  }

  return (
    <div className="min-h-screen bg-black text-white">
      <div className="max-w-2xl mx-auto">
        {/* Top navbar */}
        <div className="p-4 border-b border-gray-700 flex items-center gap-4">
          <button
            onClick={() => navigate("/feed")}
            className="bg-gray-700 hover:bg-gray-600 text-white px-4 py-2 rounded"
          >
            Back
          </button>
          <h2 className="text-2xl font-bold">@{user.username}</h2>
        </div>

        {/* User posts */}
        {user.posts.length === 0 ? (
          <p className="text-gray-400 p-4 text-center">No posts available.</p>
        ) : (
          user.posts.map((post) => {
            const date = new Date(post.createdAt);
            const formattedDate = `${date.toLocaleDateString()} ${date.toLocaleTimeString()}`;

            return (
              <div
                key={post.id}
                className="border border-gray-700 rounded-lg p-4 mb-4 hover:bg-gray-900 transition-shadow shadow-sm"
              >
                <div className="flex items-center justify-between mb-2">
                  <span className="font-bold text-white cursor-default">
                    @{post.username ?? "unknown"}
                  </span>
                  <span className="text-gray-500 text-xs">{formattedDate}</span>
                </div>

                {/* Show retweet if it exists */}
                {post.isRetweet && post.originalContent ? (
                  <div className="text-gray-400 italic mb-2">
                    🔁 Retweeted from @{post.originalUsername ?? "unknown"}:
                    <div className="border-l border-gray-600 pl-2 mt-1">
                      {post.originalContent}
                    </div>
                  </div>
                ) : (
                  <p className="text-white text-lg">{post.content}</p>
                )}
              </div>
            );
          })
        )}
      </div>
    </div>
  );
};

export default UserProfilePage;
