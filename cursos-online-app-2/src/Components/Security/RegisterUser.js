import {
  Button,
  Container,
  Grid,
  TextField,
  Typography,
} from "@material-ui/core";
import React, { useState } from "react";
import style from "../Tools/Style";
import { registerUser } from "../../Actions/UserAction";

const RegisterUser = () => {
  const [user, setUser] = useState({
    NombreCompleto: "",
    Email: "",
    Username: "",
    Password: "",
    ConfirmacionPassword: "",
  });

  const onChangeUserHandler = (e) => {
    const { name, value } = e.target;
    setUser((userBefore) => ({ ...userBefore, [name]: value }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    registerUser(user).then((response) => {
      console.log("se registro exitosamente al usuario", response);
      window.localStorage.setItem("JWT_token", response.data.token);
    });
    setUser({
      NombreCompleto: "",
      Email: "",
      Username: "",
      Password: "",
      ConfirmacionPassword: "",
    });
  };

  return (
    <Container component="main" maxWidth="md" justify="center">
      <div style={style.paper}>
        <Typography component="h1" variant="h5">
          Registro de usuario
        </Typography>
        <form style={style.form} onSubmit={handleSubmit}>
          <Grid container spacing={2}>
            <Grid item xs={12} md={6}>
              <TextField
                name="NombreCompleto"
                variant="outlined"
                onChange={onChangeUserHandler}
                value={user.NombreCompleto}
                fullWidth
                label="Ingrese su nombre completo"
              />
            </Grid>
            <Grid item xs={12} md={6}>
              <TextField
                name="Email"
                variant="outlined"
                onChange={onChangeUserHandler}
                value={user.Email}
                fullWidth
                label="Ingrese su email"
              />
            </Grid>
            <Grid item xs={12} md={6}>
              <TextField
                name="Username"
                variant="outlined"
                onChange={onChangeUserHandler}
                value={user.Username}
                fullWidth
                label="Ingrese su username"
              />
            </Grid>
            <Grid item xs={12} md={6}>
              <TextField
                name="Password"
                type="password"
                variant="outlined"
                onChange={onChangeUserHandler}
                value={user.Password}
                fullWidth
                label="Ingrese su password"
              />
            </Grid>
            <Grid item xs={12} md={6}>
              <TextField
                name="ConfirmacionPassword"
                type="password"
                variant="outlined"
                onChange={onChangeUserHandler}
                value={user.ConfirmacionPassword}
                fullWidth
                label="Confirme su password"
              />
            </Grid>
          </Grid>
          <Grid container justify="center">
            <Grid item xs={12} md={6}>
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
            </Grid>
          </Grid>
        </form>
      </div>
    </Container>
  );
};

export default RegisterUser;
