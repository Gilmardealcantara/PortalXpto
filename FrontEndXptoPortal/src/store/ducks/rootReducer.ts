import { combineReducers } from 'redux';
import apps from './apps';
import user from './user';

export default () =>
  combineReducers({
    user,
    apps,
  });
