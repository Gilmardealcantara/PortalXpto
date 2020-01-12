import { call, put } from 'redux-saga/effects';
import { AnyAction } from 'redux';
import { setUser, requestFailure } from './actions';
import { AUTH_USER } from 'src/utils/constants';

import { UserState, User } from './types';

export function* login({ payload }: AnyAction) {
  try {
    // const { response, error } = yield call(FetchHandler.post, 'Security', payload);
    const error = false;
    if (error) {
      yield put(requestFailure(error));
    } else {
      // const json: UserState = yield response.json();
      const json: UserState = yield new Promise((resolve, reject) => {
        setTimeout(() => {
          const data: UserState = {
            data: {
              id: 1,
              name: 'Gilmar Alcântara',
              email: 'gilmar.alcantara@ibm.com',
              userName: 'gilmar.alcantara',
              token: '',
            },
            loading: false,
            error: '',
          };
          resolve(data);
        }, 3000);
      });
      localStorage.setItem(AUTH_USER, JSON.stringify(json.data));
      yield put(setUser(json.data));
    }
  } catch (error) {
    yield put(requestFailure(error));
  }
}
