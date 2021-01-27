import { Grid, Snackbar} from '@material-ui/core';
import { ThemeProvider as MuithemeProvider } from "@material-ui/core/styles";
import { useEffect, useState } from 'react';
import { obtainCurrentUser, registerUser } from './Actions/UserAction';
import './App.css';
import LoginUser from './Components/Security/LoginUser';
import { useStateValue } from './Context/Store';
import React from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import AppNavBar from './Components/Navegation/AppNavBar';
import UpdateUserProfile from './Components/Security/UpdateUserProfile';
import theme from './Theme/Theme';

function App() {
  const [{ openSnackbar }, dispatch] = useStateValue();
  const [iniciaApp, setIniciaApp] = useState(false);

  useEffect(() => {
    if (!iniciaApp) {
      obtainCurrentUser(dispatch)
        .then((response) => {
          setIniciaApp(true);
        })
        .catch((error) => {
          setIniciaApp(true);
        });
    }
  }, [iniciaApp, dispatch]);

  return (
    <React.Fragment>
      <Snackbar
        anchorOrigin={{ vertical: "bottom", horizontal: "center" }}
        open={openSnackbar ? openSnackbar.open : false}
        autoHideDuration={3000}
        ContentProps={{ "aria-describedby": "message-id" }}
        message={
          <span id="message-id">
            {openSnackbar ? openSnackbar.mensaje : ""}
          </span>
        }
        onClose={() =>
          dispatch({
            type: "OPEN_SNACKBAR",
            openMessage: {
              open: false,
              mensaje: "",
            },
          })
        }
      ></Snackbar>
      <Router>
        <MuithemeProvider theme={theme}>
          <AppNavBar />
          <Grid container>
            <Switch>
              <Route exact path="/auth/login" component={LoginUser} />
              <Route exact path="/auth/register" component={registerUser} />
              <Route exact path="/auth/profile" component={UpdateUserProfile} />
              <Route exact path="/" component={LoginUser} />
            </Switch>
          </Grid>
        </MuithemeProvider>
      </Router>
    </React.Fragment>
  );
}

export default App;
