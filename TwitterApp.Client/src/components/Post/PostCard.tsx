import React from "react";
import type { Post } from "../../types";
import { useNavigate } from "react-router-dom";

interface Props {
  post: Post;
  onLikeToggle?: () => void;
  onRetweet?: () => void;
}

const PostCard: React.FC<Props> = ({ post, onLikeToggle, onRetweet }) => {
  const navigate = useNavigate();

  const handleUsernameClick = () => {
    navigate(`/user/${post.userId}`); 
  };

  return (
    <div className="border-b border-gray-700 p-4 hover:bg-gray-900 transition">
      {post.isRetweet && (
        <p className="text-xs text-gray-400 mb-1">🔁 Retweeted</p>
      )}

      <p
        className="font-semibold text-white cursor-pointer hover:underline"
        onClick={handleUsernameClick}
      >
        @{post.username ?? "unknown"}
      </p>

      {post.content && <p className="text-white mt-2">{post.content}</p>}

      <div className="flex gap-6 mt-4 text-sm text-gray-400">
        <button onClick={onLikeToggle} className="hover:text-red-400">
          ❤️ {post.likesCount}
        </button>

        <button onClick={onRetweet} className="hover:text-green-400">
          🔁 Retweet
        </button>
      </div>
    </div>
  );
};

export default PostCard;
