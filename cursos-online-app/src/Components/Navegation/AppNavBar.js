import { AppBar } from "@material-ui/core";
import React from "react";
import { useStateValue } from "../../Context/Store";
import BarSession from "./Bar/BarSession";

const AppNavBar = () => {
  const [{sesionUsuario}, dispatch] = useStateValue();
  return sesionUsuario ? (sesionUsuario.autenticado === true ? <AppBar position="static"><BarSession /></AppBar> : null ) : null;
};

export default AppNavBar;
