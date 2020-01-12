import { action } from 'typesafe-actions';
import { AppsTypes, App } from './types';

export const loadRequest = () => action(AppsTypes.LOAD_REQUEST);
export const loadSuccess = (data: App[]) => action(AppsTypes.SET_APPS, data);
export const loadFailure = (error: string) => action(AppsTypes.LOAD_FAILURE, error);
