import { createStore, applyMiddleware, Store } from 'redux';
import createSagaMiddleware from 'redux-saga';

import { AppsState } from './ducks/apps/types';
import { UserState } from './ducks/user/types';
import rootReducer from './ducks/rootReducer';
import rootSaga from './ducks/rootSaga';

export interface ApplicationState {
  user: UserState;
  apps: AppsState;
}

const sagaMiddleware = createSagaMiddleware();

const middlewares = [sagaMiddleware];

const store: Store<ApplicationState> = createStore(rootReducer(), applyMiddleware(...middlewares));

sagaMiddleware.run(rootSaga);

export default store;
