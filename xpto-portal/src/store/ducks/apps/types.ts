/**
 * Action types
 */

export enum AppsTypes {
  LOAD_REQUEST = '@Apps/LOAD_REQUEST',
  LOAD_FAILURE = '@Apps/LOAD_FAILURE',
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
