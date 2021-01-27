import { AppBar } from "@material-ui/core";
import React from "react";
import BarSession from "./Bar/BarSession";

const AppNavBar = () => {
  return (
    <AppBar position="static">
      <BarSession />
    </AppBar>
  );
};

export default AppNavBar;
