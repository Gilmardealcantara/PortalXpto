/* eslint-disable @typescript-eslint/explicit-function-return-type */
import React from 'react';
import { Route, Redirect, RouteProps, RouteComponentProps } from 'react-router-dom';
import { AUTH_TOKEN_KEY } from 'src/utils/constants';
import Main from 'src/containers/Main';

enum AuthenticatedResp {
  unauthorized,
  forbidden,
  ok,
}

export const isAuthenticated = (): AuthenticatedResp => {
  if (localStorage.getItem(AUTH_TOKEN_KEY)) {
    return AuthenticatedResp.ok;
  }
  return AuthenticatedResp.unauthorized;
};

const PrivateRoute = ({ component, ...rest }: RouteProps) => {
  if (!component) {
    throw Error('component is undefined');
  }

  const render = (props: RouteComponentProps): React.ReactNode => {
    const isAuth = isAuthenticated();
    if (isAuth === AuthenticatedResp.ok) return <Main component={component} routeProps={props} />;
    else if (isAuth === AuthenticatedResp.forbidden) return <Redirect to={{ pathname: '' }} />;
    else return <Redirect to={{ pathname: '/Login' }} />;
  };

  return <Route {...rest} render={render} />;
};

export default PrivateRoute;
