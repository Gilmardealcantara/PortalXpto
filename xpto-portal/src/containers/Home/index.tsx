import React, { useContext } from 'react';
import GridList from '@material-ui/core/GridList';
import GridListTile from '@material-ui/core/GridListTile';
import { useStyles } from './style';
import AppContext from 'src/context';

export default function TitlebarGridList() {
  const classes = useStyles();
  const { appData } = useContext(AppContext);

  return (
    <div className={classes.root}>
      <GridList cellHeight={500} className={classes.gridList}>
        {appData.map(app => (
          <GridListTile key={app.url}>
            <div className={classes.cardContent}>
              <div className={classes.cardapp}>{app.title}</div>
              <iframe src={app.url} height="90%" width="90%" title={app.title}></iframe>
            </div>
          </GridListTile>
        ))}
      </GridList>
    </div>
  );
}
