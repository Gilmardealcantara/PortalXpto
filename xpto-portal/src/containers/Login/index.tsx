import React, { useState, useCallback, useEffect } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import TextField from '@material-ui/core/TextField';
import { AccountCircle, Lock } from '@material-ui/icons';
import { Button } from '@material-ui/core';
import { useStyles } from './style';
import history from 'src/routers/history';
import { AUTH_USER } from 'src/utils/constants';
import { ApplicationState } from 'src/store';
import * as UserActions from 'src/store/ducks/user/actions';
import { Auth } from 'src/store/ducks/user/types';

const initialState: Auth = {
  email: 'gilmardealcantara@gmail.com',
  password: '123456',
};

const Login: React.FC = () => {
  const classes = useStyles();
  const user = useSelector((state: ApplicationState) => state.user);
  const dispatch = useDispatch();
  const [auth, setAuth] = useState<Auth>(initialState);

  useEffect(() => {
    if (user.data) {
      history.push('Home');
    }
  }, [user]);

  const handleChange = (name: keyof Auth) => (event: React.ChangeEvent<HTMLInputElement>) => {
    setAuth({ ...auth, [name]: event.target.value });
  };

  const handleLogin = useCallback(() => {
    console.log(auth);
    dispatch(UserActions.loginRequest(auth));
  }, [auth]);

  return (
    <div className={classes.container}>
      <div className={classes.loginCard}>
        <div className={classes.title}>Portal XPTO</div>
        <div className={classes.loginInput}>
          <AccountCircle color="primary" />
          <TextField
            id="input-with-icon-grid"
            label="Login"
            fullWidth={true}
            value={auth.email}
            onChange={handleChange('email')}
          />
        </div>
        <div className={classes.loginInput}>
          <Lock color="primary" />
          <TextField
            id="input-with-icon-grid"
            label="Senha"
            type="password"
            fullWidth={true}
            value={auth.password}
            onChange={handleChange('password')}
          />
        </div>
        <Button
          className={classes.buttonSubmit}
          fullWidth={true}
          variant="outlined"
          color="primary"
          disableElevation
          onClick={handleLogin}
        >
          {user.loading ? 'loading...' : 'Entrar'}
        </Button>
      </div>
    </div>
  );
};

export default Login;
