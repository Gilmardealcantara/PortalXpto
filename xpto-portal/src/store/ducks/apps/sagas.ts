import { put } from 'redux-saga/effects';
import { loadSuccess, loadFailure } from './actions';
// import FetchHandler from '../../../services/FetchHandlerService';
import { AppsState } from './types';

export function* loadApps() {
  try {
    // const { response, error } = yield call(FetchHandler.get, 'UserPreferences');
    const error = false;
    if (error) {
      yield put(loadFailure(error));
    } else {
      // const json = yield response.json();
      const json: AppsState = yield new Promise((resolve, reject) => {
        setTimeout(() => {
          const userPreference: AppsState = {
            data: [
              {
                url: 'https://dtidigital.com.br/',
                title: 'dtidigital',
              },
              {
                url: 'https://www.nasa.gov/',
                title: 'Nasa',
              },
              {
                url: 'https://www.expedia.com.br/Hoteis',
                title: 'Hoteis',
              },
              {
                url: 'https://www.wikipedia.org/',
                title: 'wikipedia',
              },
            ],
            loading: false,
            error: '',
          };
          resolve(userPreference);
        }, 500);
      });
      yield put(loadSuccess(json.data));
    }
  } catch (error) {
    yield put(loadFailure(error));
  }
}
