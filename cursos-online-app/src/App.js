import "./App.css";
import { ThemeProvider as MuithemeProvider } from "@material-ui/core/styles";
import { Grid, Snackbar } from "@material-ui/core";
import theme from "./Theme/Theme";
import RegisterUser from "./Components/Security/RegisterUser";
import LoginUser from "./Components/Security/LoginUser";
import UpdateUserProfile from "./Components/Security/UpdateUserProfile";
import { obtainCurrentUser } from "./Actions/UserAction";
import AppNavBar from "./Components/Navegation/AppNavBar";
import React from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import { useStateValue } from "./Context/Store";
import { useEffect, useState } from "react";

function App() {
  const [{ sesionUsuario, openSnackbar }, dispatch] = useStateValue();
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
  }, [iniciaApp]);

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
              <Route exact path="/auth/register" component={RegisterUser} />
              <Route exact path="/auth/profile" component={UpdateUserProfile} />
              <Route exact path="/" component={UpdateUserProfile} />
            </Switch>
          </Grid>
        </MuithemeProvider>
      </Router>
    </React.Fragment>
  );
}

export default App;
