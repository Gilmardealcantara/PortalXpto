/* eslint-disable no-console */
import { AUTH_USER, AUTH_TOKEN } from '../utils/constants';

export interface ComonError {
  statusCode: number;
  Message: string;
}

export interface Response {
  data?: any;
  error?: ComonError;
}

export default class FetchAPI {
  private baseURI: string;

  constructor(url: string) {
    this.baseURI = url;
  }

  private formatURL = (resource: string) => this.baseURI + resource;

  private getHeaders = () =>
    new Headers({
      Accept: 'application/json',
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem(AUTH_TOKEN)}`,
    });

  private sendRequest = async (uri: string, requestInfo: RequestInit): Promise<Response> => {
    const response = await fetch(uri, requestInfo).catch(x => x);

    if (response.status === 401) {
      localStorage.removeItem(AUTH_TOKEN);
      localStorage.removeItem(AUTH_USER);
    }
    if (!response.ok) {
      const error = {
        statusCode: response.status,
        Message: 'Request Error, verify your browser console',
      };
      return { error, data: null };
    }
    return { error: undefined, data: await response.json() };
  };

  async get(resource: string): Promise<Response> {
    const uri = this.formatURL(resource);
    const requestInfo = {
      headers: this.getHeaders(),
    };
    return this.sendRequest(uri, requestInfo);
  }

  post(resource: string, data: any): Promise<Response> {
    const uri = this.formatURL(resource);
    const requestInfo = {
      method: 'POST',
      body: JSON.stringify(data),
      headers: this.getHeaders(),
    };
    return this.sendRequest(uri, requestInfo);
  }

  update(resource: string, data: any): Promise<Response> {
    const uri = this.formatURL(resource);
    const requestInfo = {
      method: 'PUT',
      body: JSON.stringify(data),
      headers: this.getHeaders(),
    };
    return this.sendRequest(uri, requestInfo);
  }

  delete(resource: string, data: any): Promise<Response> {
    let uri = this.formatURL(resource);
    uri = `${uri}/${data}`;
    const requestInfo = {
      method: 'DELETE',
      headers: this.getHeaders(),
    };
    return this.sendRequest(uri, requestInfo);
  }

  deleteMany(resource: string, data: number[]): Promise<Response> {
    let uri = this.formatURL(resource);
    uri = `${uri}/?${data.reduce((acc, x) => `${acc}commentIds=${x}&`, '')}`;
    const requestInfo = {
      method: 'DELETE',
      headers: this.getHeaders(),
    };
    return this.sendRequest(uri, requestInfo);
  }
}
