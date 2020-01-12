import React, { useState } from 'react';
import TextField from '@material-ui/core/TextField';
import { AccountCircle, Lock } from '@material-ui/icons';
import { Button } from '@material-ui/core';
import { useStyles } from './style';
import history from 'src/routers/history';
import { AUTH_USER } from 'src/utils/constants';

interface Auth {
  username: string;
  password: string;
}

const initialState: Auth = {
  username: 'gilmar',
  password: '1234',
};

const Login: React.FC = () => {
  const classes = useStyles();
  const [auth, setAuth] = useState<Auth>(initialState);

  const handleChange = (name: keyof Auth) => (event: React.ChangeEvent<HTMLInputElement>) => {
    setAuth({ ...auth, [name]: event.target.value });
  };

  const handleLogin = () => {
    if (auth.username === initialState.username && auth.password === initialState.password) {
      // localStorage.setItem(AUTH_TOKEN_KEY, 'anyToken');
      history.push('Home');
    }
  };

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
            value={auth.username}
            onChange={handleChange('username')}
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
          Entrar
        </Button>
      </div>
    </div>
  );
};

export default Login;
