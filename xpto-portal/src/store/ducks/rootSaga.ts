import { all, takeLatest } from 'redux-saga/effects';
import { AppsTypes } from './apps/types';
import { UserTypes } from './user/types';
import { loadApps } from './apps/sagas';
import { login } from './user/sagas';

export default function* rootSaga() {
  return yield all([
    takeLatest(UserTypes.LOGIN_REQUEST, login),
    takeLatest(AppsTypes.LOAD_REQUEST, loadApps),
  ]);
}
