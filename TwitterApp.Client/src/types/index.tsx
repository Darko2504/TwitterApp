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
