import "./App.css";
import {ThemeProvider as MuithemeProvider} from "@material-ui/core/styles";
import theme from "./Theme/Theme";
import RegisterUser from "./Components/Security/RegisterUser";
import LoginUser from "./Components/Security/LoginUser";
import UpdateUserProfile from "./Components/Security/UpdateUserProfile";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import { Grid } from "@material-ui/core";
import AppNavBar from "./Components/Navegation/AppNavBar";

function App() {
  return (
    <Router>
      <MuithemeProvider theme={theme}>
        <AppNavBar />
        <Grid container>
          <Switch>
            <Route exact path="/auth/login" component={LoginUser} />
            <Route exact path="/auth/register" component={RegisterUser} />
            <Route exact path="/auth/profile" component={UpdateUserProfile} />
            <Route exact path="/" component={LoginUser} />
          </Switch>
        </Grid>
      </MuithemeProvider>
    </Router>
  );
}

export default App;
