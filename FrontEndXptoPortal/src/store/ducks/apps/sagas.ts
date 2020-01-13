import { put, call } from 'redux-saga/effects';
import { loadAppsFail, setApps } from './actions';
import ApiService from 'src/services';

export function* loadApps() {
  try {
    const { data, error } = yield call(ApiService.GetApps);
    if (error) {
      yield put(loadAppsFail(error));
    } else {
      yield put(setApps(data));
    }
  } catch (error) {
    yield put(loadAppsFail(error));
  }
}
