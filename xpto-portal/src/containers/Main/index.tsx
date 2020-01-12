/* eslint-disable @typescript-eslint/no-explicit-any */
import React from 'react';
import { useSelector } from 'react-redux';
import Header from 'src/components/Header';
import { RouteComponentProps } from 'react-router-dom';
import { ApplicationState } from 'src/store';

interface Props {
  component: any;
  routeProps: RouteComponentProps;
}

const Main: React.FC<Props> = (props: Props) => {
  const user = useSelector((state: ApplicationState) => state.user);
  console.log(user.data);
  return (
    <div>
      <Header />
      {React.createElement(props.component, props.routeProps)}
    </div>
  );
};

export default Main;
