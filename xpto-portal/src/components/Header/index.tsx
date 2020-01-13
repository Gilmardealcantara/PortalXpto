import React, { useCallback } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import IconButton from '@material-ui/core/IconButton';
import Typography from '@material-ui/core/Typography';
import InputBase from '@material-ui/core/InputBase';
import SearchIcon from '@material-ui/icons/Search';
import ExitToApp from '@material-ui/icons/ExitToApp';
import { useStyles } from './style';
import { ApplicationState } from 'src/store';
import { filterApps } from 'src/store/ducks/apps/actions';
import { setUser } from 'src/store/ducks/user/actions';
import history from 'src/routers/history';
import { AUTH_USER, AUTH_TOKEN } from 'src/utils/constants';

export default function PrimarySearchAppBar() {
  const dispatch = useDispatch();
  const apps = useSelector((state: ApplicationState) => state.apps);
  const classes = useStyles();

  const changeFilterHandle = useCallback((event: React.ChangeEvent<HTMLInputElement>) => {
    console.log(event.target.value);
    dispatch(filterApps(event.target.value));
  }, []);

  const menuId = 'primary-search-account-menu';
  console.log('Header');
  console.log(apps);
  return (
    <div className={classes.grow}>
      <AppBar position="static" color="primary" variant="outlined">
        <Toolbar className={classes.toolbar}>
          <Typography className={classes.title} variant="h6" noWrap>
            Portal XPTO
          </Typography>
          <div className={classes.search}>
            <div className={classes.searchIcon}>
              <SearchIcon />
            </div>
            <InputBase
              placeholder="Search…"
              classes={{
                root: classes.inputRoot,
                input: classes.inputInput,
              }}
              inputProps={{ 'aria-label': 'search' }}
              onChange={changeFilterHandle}
            />
          </div>

          <div className={classes.sectionDesktop}>
            <IconButton
              edge="end"
              aria-label="account of current user"
              aria-controls={menuId}
              aria-haspopup="true"
              onClick={() => {
                localStorage.removeItem(AUTH_USER);
                localStorage.removeItem(AUTH_TOKEN);
                dispatch(setUser(undefined));
                history.push('Login');
              }}
              color="inherit"
            >
              <ExitToApp />
            </IconButton>
          </div>
        </Toolbar>
      </AppBar>
    </div>
  );
}
