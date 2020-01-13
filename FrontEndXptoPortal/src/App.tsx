import React from 'react';
import Routes from 'src/routers';
import { Provider } from 'react-redux';
import store from 'src/store';

const App: React.FC = () => (
  <Provider store={store}>
    <Routes />
  </Provider>
);

export default App;
