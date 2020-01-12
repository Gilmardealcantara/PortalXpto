/**
 * Action types
 */

export enum UserTypes {
  LOGIN_REQUEST = '@Users/LOGIN_REQUEST',
  SET_USER = '@Users/SET_USER',
  REQUEST_FAILURE = '@Users/REQUEST_FAILURE',
}

/**
 * Data types
 */

export interface Auth {
  email: string;
  password: string;
}

export interface User {
  id: number;
  name: string;
  email: string;
  userName: string;
  token: string;
}

/**
 * State type
 */
export interface UserState {
  readonly data?: User;
  readonly loading: boolean;
  readonly error: string;
}
