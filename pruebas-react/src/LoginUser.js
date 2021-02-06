import {
  Avatar,
  Button,
  Container,
  TextField,
  Typography,
} from "@material-ui/core";
import LockOutlinedIcon from "@material-ui/icons/LockOutlined";
import axios from "axios";
import React, { useState } from "react";
import style from "./Tools/Style";

const LoginUser = () => {

  const [user, setUser] = useState({
    Email: "",
    Password: "",
  });

  const onChangeUserHandler = (e) => {
    const { name, value } = e.target;
    setUser((userBefore) => ({ ...userBefore, [name]: value }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    axios.post("http://localhost:5000/api/User/login",user).then((response) => {
      console.log("se logeo exitosamente al usuario", response);
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
            value={user.Email}
            fullWidth
            margin="normal"
            label="Ingrese su Email"
          />
          <TextField
            name="Password"
            type="password"
            variant="outlined"
            onChange={onChangeUserHandler}
            value={user.Password}
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