/**
 * Action types
 */

export enum AppsTypes {
  LOAD_APPS = '@Apps/LOAD_APPS',
  LOAD_APPS_ERROR = '@Apps/LOAD_APPS_ERROR',
  SET_APPS = '@Apps/SET_APPS',
}

/**
 * Data types
 */
export interface App {
  title: string;
  url: string;
}

/**
 * State type
 */
export interface AppsState {
  readonly data: App[];
  readonly loading: boolean;
  readonly error: string;
}
