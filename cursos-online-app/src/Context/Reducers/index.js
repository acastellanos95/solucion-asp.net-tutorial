import userSessionReducer from './UserSessionReducer';
import openSnackbarReduccer from './OpenSnackbarReducer';

export const mainReducer = ({sesionUsuario, openSnackbar}, action) => {
  return {
    sesionUsuario : userSessionReducer(sesionUsuario, action),
    openSnackbar : openSnackbarReduccer(openSnackbar, action)
  }
};