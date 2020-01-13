import { Reducer } from 'redux';
import { UserState, UserTypes } from './types';

const INITIAL_STATE: UserState = {
  error: '',
  loading: false,
};

const reducer: Reducer<UserState> = (state = INITIAL_STATE, action) => {
  switch (action.type) {
    case UserTypes.LOGIN_REQUEST:
      return { ...state, loading: true, error: '' };
    case UserTypes.SET_USER:
      return {
        ...state,
        loading: false,
        error: '',
        data: action.payload,
      };
    case UserTypes.REQUEST_FAILURE:
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
