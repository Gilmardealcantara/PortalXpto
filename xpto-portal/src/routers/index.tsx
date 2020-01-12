import React from 'react';
import { Switch, Route, BrowserRouter as Router } from 'react-router-dom';
import Login from 'src/containers/Login';
import PrivateRoute from './PrivateRoute';
import Home from 'src/containers/Home';

const Routes: React.FC = () => {
  return (
    <Router>
      <Switch>
        <Route exact path="/Login" component={Login} />
        <PrivateRoute path="/" component={Home} />
      </Switch>
    </Router>
  );
};

export default Routes;
