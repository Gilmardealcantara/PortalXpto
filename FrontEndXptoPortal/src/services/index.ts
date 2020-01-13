import FetchAPI, { Response } from './FetchAPIServices';
import { API } from '../utils/constants';
import { Auth } from 'src/store/ducks/user/types';

const fetchApi = new FetchAPI(API);

export default class ApiService {
  static async GetApps(): Promise<Response> {
    return fetchApi.get(`Apps`);
  }

  static async Login(auth: Auth): Promise<Response> {
    return fetchApi.post('Accounts/Login', auth);
  }
}
