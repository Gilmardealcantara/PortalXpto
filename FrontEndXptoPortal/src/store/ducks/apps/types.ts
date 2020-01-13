/**
 * Action types
 */

export enum AppsTypes {
  LOAD_APPS = '@Apps/LOAD_APPS',
  LOAD_APPS_ERROR = '@Apps/LOAD_APPS_ERROR',
  SET_APPS = '@Apps/SET_APPS',
  FILTER_APPS = '@Apps/FILTER_APPS',
}

/**
 * Data types
 */
export interface App {
  id: number;
  title: string;
  url: string;
}

/**
 * State type
 */
export interface AppsState {
  readonly all: App[];
  readonly filtered: App[];
  readonly loading: boolean;
  readonly error: string;
}
