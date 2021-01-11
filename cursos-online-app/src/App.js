import "./App.css";
import {ThemeProvider as MuithemeProvider} from "@material-ui/core/styles";
import theme from "./Theme/Theme";
import RegisterUser from "./Components/Security/RegisterUser";
import LoginUser from "./Components/Security/LoginUser";
import CreateUserProfile from "./Components/Security/CreateUserProfile";

function App() {
  return (
    <MuithemeProvider theme={theme}>
        <RegisterUser />
        <LoginUser />
        <CreateUserProfile />
    </MuithemeProvider>
  );
}

export default App;
