import {
  Avatar,
  Button,
  Drawer,
  IconButton,
  List,
  ListItem,
  ListItemText,
  makeStyles,
  Toolbar,
  Typography,
} from "@material-ui/core";
import React, { useState } from "react";
import { useStateValue } from "../../../Context/Store";
import reactImg from "../../../logo.svg";

const useStyles = makeStyles((theme) => ({
  seccionDesktop: {
    display: "none",
    [theme.breakpoints.up("md")]: {
      display: "flex",
    },
  },
  seccionMobile: {
    display: "flex",
    [theme.breakpoints.up("md")]: {
      display: "none",
    },
  },
  grow: {
    flexGrow: 1,
  },
  avatarSize: {
    width: 40,
    height: 40,
  },
  list: {
    width: 250,
  },
  listItemText : {
    fontSize : "14px",
    fontWeight : 600,
    paddingLeft : "15px",
    color : "#212121"
  },
}));

const BarSession = () => {
  const classes = useStyles();
  const [{ userSession }, dispatch] = useStateValue();
  const [abrirMenuIzquierdo, setAbrirMenuIzquierdo] = useState(false);
  const cerrarMenuIzquierdo = () => {
    setAbrirMenuIzquierdo(false);
  };

  return (
    <React.Fragment>
      <Drawer
        open={abrirMenuIzquierdo}
        onClose={cerrarMenuIzquierdo}
        anchor="left"
      >
        <div className={classes.list} onKeyDown={cerrarMenuIzquierdo} onClick={cerrarMenuIzquierdo}>
          <List>
            <ListItem button>
              <i className="material-icons">account_box</i>
              <ListItemText classes={{primary : classes.listItemText}} primary="Perfil" />
            </ListItem>
          </List>
        </div>
      </Drawer>
      <Toolbar>
        <IconButton color="inherit" onClick={()=>{setAbrirMenuIzquierdo(true);}}>
          <i className="material-icons">menu</i>
        </IconButton>
        <Typography variant="h6">Cursos Online</Typography>
        <div className={classes.grow}></div>
        <div className={classes.seccionDesktop}>
          <Button color="inherit">Salir</Button>
          <Button color="inherit">
            {userSession ? userSession.user.nombreCompleto : ""}
          </Button>
          <Avatar src={reactImg} />
        </div>
        <div className={classes.seccionMobile}>
          <IconButton color="inherit">
            <i className="material-icons">more_vert</i>
          </IconButton>
        </div>
      </Toolbar>
    </React.Fragment>
  );
};

export default BarSession;
