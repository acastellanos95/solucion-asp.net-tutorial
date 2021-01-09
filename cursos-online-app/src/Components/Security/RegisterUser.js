import { Container, Grid, TextField, Typography } from "@material-ui/core";
import React from "react";

const style = {
  paper: {
    marginTop: 8,
    display: "flex",
    flexDirection: "column",
    alignItems: "center",
  },
  form : {
    width : "100%",
    marginTop : 20
  },
};

const RegisterUser = () => {
  return (
    <Container component="main" maxWidth="md" justify="center">
      <div style={style.paper}>
        <Typography component="h1" variant="h5">
          Registro de usuario
        </Typography>
        <form style={style.form}>
          <Grid container spacing={2}>
            <Grid item xs={12} md={6}>
              <TextField name="nombre" variant="outlined" fullWidth label="Ingrese su nombre" />
            </Grid>
          </Grid>
        </form>
      </div>
    </Container>
  );
};

export default RegisterUser;
