import {
  Avatar,
  Button,
  Container,
  TextField,
  Typography,
} from "@material-ui/core";
import LockOutlinedIcon from "@material-ui/icons/LockOutlined";
import React from "react";
import style from "../Tools/Style";

const LoginUser = () => {
  return (
    <Container component="main" maxWidth="xs" justify="center">
      <div style={style.paper}>
        <Avatar style={style.avatar}>
          <LockOutlinedIcon style={style.icon} />
        </Avatar>
        <Typography component="h1" variant="h5">
          Login
        </Typography>
        <form style={style.form}>
          <TextField
            name="username"
            variant="outlined"
            fullWidth
            margin="normal"
            label="Ingrese su username"
          />
          <TextField
            name="password"
            type="password"
            variant="outlined"
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

export default LoginUser;
