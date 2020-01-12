/* eslint-disable @typescript-eslint/no-explicit-any */
import React, { useEffect } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import Header from 'src/components/Header';
import { RouteComponentProps } from 'react-router-dom';
import { ApplicationState } from 'src/store';
import { AUTH_USER } from 'src/utils/constants';
import { setUser } from 'src/store/ducks/user/actions';

interface Props {
  component: any;
  routeProps: RouteComponentProps;
}

const Main: React.FC<Props> = (props: Props) => {
  const user = useSelector((state: ApplicationState) => state.user);
  const dispatch = useDispatch();
  console.log(user.data);

  useEffect(() => {
    const localUser = JSON.parse(localStorage.getItem(AUTH_USER) as string);
    dispatch(setUser(localUser));
  }, []);

  return (
    <div>
      <Header />
      {React.createElement(props.component, props.routeProps)}
    </div>
  );
};

export default Main;
