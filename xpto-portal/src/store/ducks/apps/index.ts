import { Reducer } from 'redux';
import { AppsState, AppsTypes } from './types';

const INITIAL_STATE: AppsState = {
  data: [],
  error: '',
  loading: false,
};

const reducer: Reducer<AppsState> = (state = INITIAL_STATE, action) => {
  switch (action.type) {
    case AppsTypes.LOAD_REQUEST:
      return { ...state, loading: true, error: '' };
    case AppsTypes.SET_APPS:
      return {
        ...state,
        loading: false,
        error: '',
        data: action.payload,
      };
    case AppsTypes.LOAD_FAILURE:
      return {
        ...state,
        loading: false,
        error: action.payload,
      };
    default:
      return state;
  }
};

export default reducer;
