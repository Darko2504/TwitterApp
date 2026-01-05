import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import axiosInstance from "../../api/axiosInstance";
import PostCard from "../Post/PostCard";
import type { Post } from "../../types"; 

const Feed: React.FC = () => {
  const [posts, setPosts] = useState<Post[]>([]);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate(); 

  useEffect(() => {
    const token = localStorage.getItem("token");
    if (token) {
      fetchFeed(token); 
    } else {
      navigate("/login");
    }
  }, []);

  const fetchFeed = async (token: string) => {
    try {
      const res = await axiosInstance.get("/Post/feed", {
        headers: { Authorization: `Bearer ${token}` }, 
      });
      setPosts(res.data.result);
    } catch (err) {
      console.error("Failed to load feed", err);
      navigate("/login"); 
    } finally {
      setLoading(false);
    }
  };

  const handleLikeToggle = async (post: Post) => {
    const token = localStorage.getItem("token");
    if (!token) return;

    try {
      if (post.isLikedByCurrentUser) {
        await axiosInstance.post(`/Post/${post.id}/unlike`, {}, {
          headers: { Authorization: `Bearer ${token}` },
        });
      } else {
        await axiosInstance.post(`/Post/${post.id}/like`, {}, {
          headers: { Authorization: `Bearer ${token}` },
        });
      }
      fetchFeed(token);
    } catch (err) {
      console.error("Error liking/unliking post", err);
    }
  };

  const handleRetweet = async (postId: number) => {
    const token = localStorage.getItem("token");
    if (!token) return;

    try {
      await axiosInstance.post(`/Post/${postId}/retweet`, {}, {
        headers: { Authorization: `Bearer ${token}` },
      });
      fetchFeed(token);
    } catch (err) {
      console.error("Error retweeting post", err);
    }
  };

  const handleLogout = () => {
    localStorage.removeItem("token"); 
    navigate("/login"); 
  };

  const handleCreateTweet = () => {
    navigate("/create-tweet");
  };

  if (loading) {
    return <div className="text-white p-6">Loading feed...</div>;
  }

  return (
    <div className="min-h-screen bg-black text-white">
      <div className="max-w-2xl mx-auto">
        <div className="flex justify-between items-center p-4 border-b border-gray-700">
          <h1 className="text-2xl font-bold">Home</h1>
          <div className="flex gap-4">
            <button
              onClick={handleCreateTweet}
              className="bg-blue-500 hover:bg-blue-600 text-white px-4 py-2 rounded"
            >
              Create Tweet
            </button>
            <button
              onClick={handleLogout}
              className="bg-red-500 hover:bg-red-600 text-white px-4 py-2 rounded"
            >
              Logout
            </button>
          </div>
        </div>

        {posts.length === 0 ? (
          <p className="text-gray-400 p-4">No posts available.</p>
        ) : (
          posts.map((post) => (
            <PostCard
              key={post.id}
              post={post}
              onLikeToggle={() => handleLikeToggle(post)}
              onRetweet={() => handleRetweet(post.id)}
            />
          ))
        )}
      </div>
    </div>
  );
};

export default Feed;
