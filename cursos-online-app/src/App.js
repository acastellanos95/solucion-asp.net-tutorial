import "./App.css";
import MuithemeProvider from "@material-ui/core/styles/MuiThemeProvider";
import theme from "./Theme/Theme";
import RegisterUser from "./Components/Security/RegisterUser";

function App() {
  return (
    <MuithemeProvider theme={theme}>
        <RegisterUser />
    </MuithemeProvider>
  );
}

export default App;
