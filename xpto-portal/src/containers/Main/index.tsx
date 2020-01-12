/* eslint-disable @typescript-eslint/no-explicit-any */
import React from 'react';
import Header from 'src/components/Header';
import { RouteComponentProps } from 'react-router-dom';

interface Props {
  component: any;
  routeProps: RouteComponentProps;
}

const Main: React.FC<Props> = (props: Props) => {
  return (
    <div>
      <Header />
      {React.createElement(props.component, props.routeProps)}
    </div>
  );
};

export default Main;
