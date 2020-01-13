import { Theme, makeStyles, createStyles } from '@material-ui/core';

export const useStyles = makeStyles((theme: Theme) =>
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
