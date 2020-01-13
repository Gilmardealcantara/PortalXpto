import { action } from 'typesafe-actions';
import { UserTypes, User, Auth } from './types';

export const loginRequest = (auth: Auth) => action(UserTypes.LOGIN_REQUEST, auth);
export const setUser = (data?: User) => action(UserTypes.SET_USER, data);
export const requestFailure = (error: string) => action(UserTypes.REQUEST_FAILURE, error);
