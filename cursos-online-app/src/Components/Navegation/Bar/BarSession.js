import {
  Avatar,
  Button,
  Drawer,
  IconButton,
  makeStyles,
  Toolbar,
  Typography,
} from "@material-ui/core";
import React, { useState } from "react";
import { withRouter } from "react-router-dom";
import { useStateValue } from "../../../Context/Store";
import reactImg from "../../../logo.svg";
import MenuDerecha from "./MenuDerecha";
import MenuIzquierda from "./MenuIzquierda";

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
  listItemText: {
    fontSize: "14px",
    fontWeight: 600,
    paddingLeft: "15px",
    color: "#212121",
  },
}));

const BarSession = (props) => {
  const classes = useStyles();
  const [{ sesionUsuario }, dispatch] = useStateValue();
  console.log(sesionUsuario);
  const [abrirMenuIzquierdo, setAbrirMenuIzquierdo] = useState(false);
  const [abrirMenuDerecho, setAbrirMenuDerecho] = useState(false);

  const cerrarMenuDerecho = () => {
    setAbrirMenuDerecho(false);
  };

  const cerrarMenuIzquierdo = () => {
    setAbrirMenuIzquierdo(false);
  };

  const salirSesionApp = () => {
    localStorage.removeItem("JWT_token");
    props.history.push("/auth/login");
    dispatch({
      type: "SALIR_SESION",
      nuevoUsuario: null,
      autenticado: false
    });
  };

  return (
    <React.Fragment>
      <Drawer
        open={abrirMenuIzquierdo}
        onClose={cerrarMenuIzquierdo}
        anchor="left"
      >
        <div
          className={classes.list}
          onKeyDown={cerrarMenuIzquierdo}
          onClick={cerrarMenuIzquierdo}
        >
          <MenuIzquierda classes={classes} />
        </div>
      </Drawer>
      <Drawer
        open={abrirMenuDerecho}
        onClose={cerrarMenuDerecho}
        anchor="right"
      >
        <div
          className={classes.list}
          onKeyDown={cerrarMenuDerecho}
          onClick={cerrarMenuDerecho}
        >
          <MenuDerecha classes={classes} salirSesion={salirSesionApp} usuario={sesionUsuario ? sesionUsuario.user : null} />
        </div>
      </Drawer>
      <Toolbar>
        <IconButton
          color="inherit"
          onClick={() => {
            setAbrirMenuIzquierdo(true);
          }}
        >
          <i className="material-icons">menu</i>
        </IconButton>
        <Typography variant="h6">Cursos Online</Typography>
        <div className={classes.grow}></div>
        <div className={classes.seccionDesktop}>
          <Button color="inherit" onClick={salirSesionApp}>Salir</Button>
          <Button color="inherit">
            {sesionUsuario ? sesionUsuario.usuario.nombreCompleto : ""}
          </Button>
          <Avatar src={sesionUsuario.usuario.imagenPerfil||reactImg} />
        </div>
        <div className={classes.seccionMobile}>
          <IconButton color="inherit" onClick={() => {
            setAbrirMenuDerecho(true);
          }}>
            <i className="material-icons">more_vert</i>
          </IconButton>
        </div>
      </Toolbar>
    </React.Fragment>
  );
};

export default withRouter(BarSession);
