import React from 'react';
import { createStyles, Theme, makeStyles } from '@material-ui/core/styles';
import TextField from '@material-ui/core/TextField';
import Grid from '@material-ui/core/Grid';
import { AccountCircle, Lock } from '@material-ui/icons';
import { Button } from '@material-ui/core';

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    container: {
      display: 'flex',
      flex: 1,
      flexDirection: 'column',
      alignItems: 'center',
      justifyContent: 'center',
      width: '100vw',
      height: '100vh',
    },
    loginCard: {
      width: '30vw',
      display: 'flex',
      flexDirection: 'column',
    },
    loginInput: {
      display: 'flex',
      alignItems: 'flex-end',
      justifyContent: 'space-between',
    },
    title: {
      fontWeight: 'bold',
      display: 'flex',
      justifyContent: 'center',
      color: theme.palette.primary.main,
    },
    buttonSubmit: {
      marginTop: '20px',
    },
  }),
);

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
