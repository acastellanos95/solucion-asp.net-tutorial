import {
  Avatar,
  Button,
  Container,
  TextField,
  Typography,
} from "@material-ui/core";
import LockOutlinedIcon from "@material-ui/icons/LockOutlined";
import React, { useEffect, useState } from "react";
import style from "../Tools/Style";
import axios from "axios";
import { loginUser } from "../../Actions/UserAction";
import { withRouter } from "react-router-dom";
import { useStateValue } from "../../Context/Store";

const LoginUser = (props) => {
  const [{usuarioSesion}, dispatch] = useStateValue();
  const [usuario, setUsuario] = useState({
    Email: "",
    Password: "",
  });

  const onChangeUserHandler = (e) => {
    const { name, value } = e.target;
    setUsuario((userBefore) => ({ ...userBefore, [name]: value }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    loginUser(usuario, dispatch).then(response => {
      if(response.status === 200){
        window.localStorage.setItem("JWT_token", response.data.token);
        props.history.push("/auth/profile");
      }else{
        dispatch({
          type: "OPEN_SNACKBAR",
          openMensaje:{
            open: true,
            mensaje: "Las credenciales del usuario son incorrectas"
          }
        });
      }
    });
  };

  return (
    <Container component="main" maxWidth="xs" justify="center">
      <div style={style.paper}>
        <Avatar style={style.avatar}>
          <LockOutlinedIcon style={style.icon} />
        </Avatar>
        <Typography component="h1" variant="h5">
          Login
        </Typography>
        <form style={style.form} onSubmit={handleSubmit}>
          <TextField
            name="Email"
            variant="outlined"
            onChange={onChangeUserHandler}
            value={usuario.Email}
            fullWidth
            margin="normal"
            label="Ingrese su Email"
          />
          <TextField
            name="Password"
            type="password"
            variant="outlined"
            onChange={onChangeUserHandler}
            value={usuario.Password}
            fullWidth
            margin="normal"
            label="Ingrese su password"
          />
          <Button
            type="submit"
            fullWidth
            variant="contained"
            color="primary"
            size="large"
            style={style.submit}
          >
            Enviar
          </Button>
        </form>
      </div>
    </Container>
  );
};

export default withRouter(LoginUser);
