import { Reducer } from 'redux';
import { AppsState, AppsTypes } from './types';

const INITIAL_STATE: AppsState = {
  all: [],
  filtered: [],
  error: '',
  loading: false,
};

const reducer: Reducer<AppsState> = (state = INITIAL_STATE, action) => {
  switch (action.type) {
    case AppsTypes.LOAD_APPS:
      return { ...state, loading: true, error: '' };
    case AppsTypes.SET_APPS:
      return {
        ...state,
        loading: false,
        error: '',
        all: action.payload,
        filtered: action.payload,
      };
    case AppsTypes.LOAD_APPS_ERROR:
      return {
        ...state,
        loading: false,
        error: action.payload,
      };
    case AppsTypes.FILTER_APPS:
      return {
        ...state,
        filtered: state.all.filter(x =>
          x.title.toLocaleLowerCase().includes(action.payload.toLocaleLowerCase()),
        ),
      };
    default:
      return state;
  }
};

export default reducer;
