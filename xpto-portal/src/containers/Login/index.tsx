import React from 'react';
import TextField from '@material-ui/core/TextField';
import { AccountCircle, Lock } from '@material-ui/icons';
import { Button } from '@material-ui/core';
import { useStyles } from './style';

const Login: React.FC = () => {
  const classes = useStyles();

  return (
    <div className={classes.container}>
      <div className={classes.loginCard}>
        <div className={classes.title}>Portal XPTO</div>
        <div className={classes.loginInput}>
          <AccountCircle color="primary" />
          <TextField id="input-with-icon-grid" label="Login" fullWidth={true} />
        </div>
        <div className={classes.loginInput}>
          <Lock color="primary" />
          <TextField id="input-with-icon-grid" label="Senha" type="password" fullWidth={true} />
        </div>
        <Button className={classes.buttonSubmit} fullWidth={true} variant="outlined" color="primary" disableElevation>
          Entrar
        </Button>
      </div>
    </div>
  );
};

export default Login;
