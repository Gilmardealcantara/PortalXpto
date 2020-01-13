import { call, put } from 'redux-saga/effects';
import { AnyAction } from 'redux';
import { setUser, requestFailure } from './actions';
import { AUTH_USER, AUTH_TOKEN } from 'src/utils/constants';
import ApiService from 'src/services';
import { User } from './types';

export function* login({ payload }: AnyAction) {
  try {
    const { data, error }: { data: User; error: any } = yield call(ApiService.Login, payload);

    if (error) {
      yield put(requestFailure(error));
    } else {
      localStorage.setItem(AUTH_USER, JSON.stringify(data));
      localStorage.setItem(AUTH_TOKEN, JSON.stringify(data.token));
      yield put(setUser(data));
    }
  } catch (error) {
    yield put(requestFailure(error));
  }
}
