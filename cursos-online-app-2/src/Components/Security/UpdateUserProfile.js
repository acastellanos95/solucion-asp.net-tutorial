import {
  Button,
  Container,
  Grid,
  TextField,
  Typography,
} from "@material-ui/core";
import React, { useEffect, useState } from "react";
import style from "../Tools/Style";
import { obtainCurrentUser, updateUser } from "../../Actions/UserAction";
import { useStateValue } from "../../Context/Store";

const UpdateUserProfile = () => {
  const [{ userSession}, dispatch] = useStateValue();
  const [profile, setProfile] = useState({
    nombreCompleto: "",
    email: "",
    username: "",
    password: "",
    confirmacionPassword: "",
  });

  useEffect(()=>{
    obtainCurrentUser(dispatch).then(response => {
      console.log("esta es la data del objeto response del usuario actual ", response);
      setProfile(response.data);
    }).catch(error => {
      console.log("hubo un error");
    });
  }, []);

  const onChangeProfileHandler = (e) => {
    const { name, value } = e.target;
    setProfile((profileBefore) => ({ ...profileBefore, [name]: value }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    updateUser(profile).then((response) => {
      if(response.status === 200){
        dispatch({
          type : "OPEN_SNACKBAR",
          openMessage : {
            open : true,
            mensaje : "Se guardaron exitosamente los cambios en perfil usuario"
          }
        });
        window.localStorage.setItem("JWT_token", response.data.token);
      }
      else{
        dispatch({
          type : "OPEN_SNACKBAR",
          openMessage : {
            open : true,
            mensaje : "Error al intentar guardar en " + Object.keys(response.data.errores)
          }
        });
      }
    });
  };

  return (
    <Container component="main" maxWidth="md" justify="center">
      <div style={style.paper}>
        <Typography component="h1" variant="h5">
          Perfil de usuario
        </Typography>
        <form style={style.form} onSubmit={handleSubmit}>
          <Grid container spacing={2}>
            <Grid item xs={12} md={12}>
              <TextField
                name="nombreCompleto"
                variant="outlined"
                onChange={onChangeProfileHandler}
                value={profile.nombreCompleto || ''}
                fullWidth
                label="Ingrese su nombre completo"
              />
            </Grid>
            <Grid item xs={12} md={6}>
              <TextField
                name="username"
                variant="outlined"
                onChange={onChangeProfileHandler}
                value={profile.username || ''}
                fullWidth
                label="Ingrese su username"
              />
            </Grid>
            <Grid item xs={12} md={6}>
              <TextField
                name="email"
                variant="outlined"
                onChange={onChangeProfileHandler}
                value={profile.email || ''}
                fullWidth
                label="Ingrese su email"
              />
            </Grid>
            <Grid item xs={12} md={6}>
              <TextField
                name="password"
                type="password"
                variant="outlined"
                onChange={onChangeProfileHandler}
                value={profile.password || ''}
                fullWidth
                label="Ingrese su password"
              />
            </Grid>
            <Grid item xs={12} md={6}>
              <TextField
                name="confirmacionPassword"
                type="password"
                variant="outlined"
                onChange={onChangeProfileHandler}
                value={profile.confirmacionPassword || ''}
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
                Guardar datos
              </Button>
            </Grid>
          </Grid>
        </form>
      </div>
    </Container>
  );
};

export default UpdateUserProfile;
