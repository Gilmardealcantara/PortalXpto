import React from 'react';
import { makeStyles, createStyles, Theme } from '@material-ui/core';

export const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    container: {
      display: 'flex',
      flex: 1,
      flexDirection: 'column',
      alignItems: 'center',
      justifyContent: 'center',
      width: '100vw',
      height: '50px',
      backgroundColor: 'grey',
      paddingTop: 0,
      marginTop: 0,
    },
  }),
);

const Header: React.FC = () => {
  const classes = useStyles();

  return <div className={classes.container}>Header</div>;
};

export default Header;
