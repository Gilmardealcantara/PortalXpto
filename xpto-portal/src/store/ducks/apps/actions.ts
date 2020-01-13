import { action } from 'typesafe-actions';
import { AppsTypes, App } from './types';

export const loadApps = () => action(AppsTypes.LOAD_APPS);
export const setApps = (data: App[]) => action(AppsTypes.SET_APPS, data);
export const loadAppsFail = (error: string) => action(AppsTypes.LOAD_APPS_ERROR, error);
