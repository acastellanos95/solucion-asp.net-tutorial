import userSessionReducer from './UserSessionReducer';
import openSnackbarReduccer from './OpenSnackbarReducer';

export const mainReducer = ({userSession, openSnackbar}, action) => {
  return {
    userSession : userSessionReducer(userSession, action),
    openSnackbar : openSnackbarReduccer(openSnackbar, action)
  }
};