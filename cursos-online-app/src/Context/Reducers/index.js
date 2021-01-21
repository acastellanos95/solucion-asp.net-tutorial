import userSessionReducer from './UserSessionReducer';
import openSnackbarReduccer from './OpenSnackbarReducer';

export const mainReducer = ({userSession, openSnackbar}, action) => {
  return {
    sesionUsuario : userSessionReducer(userSession, action),
    openSnackbar : openSnackbarReduccer(openSnackbar, action)
  }
};