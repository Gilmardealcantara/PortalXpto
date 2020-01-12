import { createContext } from 'react';

interface AppData {
  title: string;
  url: string;
}

export interface AppContext {
  appData: AppData[];
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

const context = createContext<AppContext>({ appData });

export default context;
