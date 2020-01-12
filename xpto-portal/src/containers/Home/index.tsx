import React from 'react';
import GridList from '@material-ui/core/GridList';
import GridListTile from '@material-ui/core/GridListTile';
import { useStyles } from './style';

interface AppData {
  title: string;
  url: string;
}

const appData: AppData[] = [
  {
    url: 'https://dtidigital.com.br/',
    title: 'dtidigital',
  },
  {
    url: 'https://www.nasa.gov/',
    title: 'Nasa',
  },
  {
    url: 'https://www.expedia.com.br/Hoteis',
    title: 'Hoteis',
  },
  {
    url: 'https://www.wikipedia.org/',
    title: 'wikipedia',
  },
];

export default function TitlebarGridList() {
  const classes = useStyles();

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
