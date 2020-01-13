import React from 'react';
import { useSelector } from 'react-redux';

import GridList from '@material-ui/core/GridList';
import GridListTile from '@material-ui/core/GridListTile';
import { useStyles } from './style';
import { ApplicationState } from 'src/store';

export default function Home() {
  const classes = useStyles();
  const { apps, user } = useSelector((state: ApplicationState) => state);

  console.log(apps);

  return (
    <div className={classes.root}>
      <GridList cellHeight={500} className={classes.gridList}>
        {user.data ? (
          apps.filtered.map(app => (
            <GridListTile key={app.id}>
              <div className={classes.cardContent}>
                <div className={classes.cardapp}>{app.title}</div>
                <iframe
                  src={`${app.url}?token=${user.data!.token}`}
                  height="90%"
                  width="90%"
                  title={app.title}
                ></iframe>
              </div>
            </GridListTile>
          ))
        ) : (
          <div />
        )}
      </GridList>
    </div>
  );
}
