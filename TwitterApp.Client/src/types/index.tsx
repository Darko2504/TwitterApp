export interface LoginRequest {
  username: string;
  password: string;
}

export interface RegisterRequest {
  username: string;
  email: string;
  password: string;
}

export interface CustomResponse<T> {
  result: T | null;
  isSuccessfull: boolean;
  errors: string[];
}

export interface AuthResponse {
  token: string;
  validTo: string;
}

export interface UserDto {
  id: string;
  username: string;
  email: string;
}

export interface Post {
  id: number;
  content: string | null;
  createdAt: string;
  userId: string;
  username: string | null;
  likesCount: number;
  isRetweet: boolean;
  retweetOfPostId: number | null;
  isLikedByCurrentUser?: boolean;
   originalContent?: string;
  originalUsername?: string;
}

export interface UserProfile {
  id: string;
  username: string;
  bio?: string;
  posts: Post[];
}
