import {
  Button,
  Container,
  Grid,
  TextField,
  Typography,
} from "@material-ui/core";
import React, { useState } from "react";
import style from "../Tools/Style";
import {
  KeyboardDatePicker,
  MuiPickersUtilsProvider,
} from "@material-ui/pickers";
import DateFnsUtils from "@date-io/date-fns";
import ImageUploader from 'react-images-upload';
import {v4 as uuidv4} from 'uuid';
import { obtenerDataImagen } from "../../Actions/ImagenAction";
import { guardarCurso } from "../../Actions/CursoAction";

const NuevoCurso = () => {
  const [fechaSeleccionada, setFechaSeleccionada] = useState(new Date());
  const [imagenCurso, setImagenCurso] = useState(null);
  const [curso, setCurso] = useState({
    Titulo: "",
    Descripcion: "",
    Precio: 0.0,
    Promocion: 0.0,
  });

  const onChangeCursoHandler = (e) => {
    const {name, value} = e.target;

    setCurso( anteriorCurso => ({
        ...anteriorCurso,
        [name]: value
    }))
  };

  const onSubmitCursoHandler = (e) => {
    e.preventDefault();
    const cursoId = uuidv4();

    const objetoCurso = {
        titulo: curso.Titulo,
        descripcion: curso.Descripcion,
        promocion: parseFloat(curso.Promocion || 0.0),
        precio: parseFloat(curso.Precio || 0.0),
        fechaPublicacion: fechaSeleccionada,
        cursoId: cursoId
    };

    const objetoImagen = {
        nombre: imagenCurso.nombre,
        data: imagenCurso.data,
        extension: imagenCurso.extension,
        objetoReferencia: cursoId,
    }

    guardarCurso(objetoCurso, objetoImagen).then(respuestas => {
        console.log('respuestas arreglo: ', respuestas);
    });
  };

  const fotoKey = uuidv4();

  const subirFoto = imagenes => {
    const foto = imagenes[0];
    obtenerDataImagen(foto).then((respuesta) => {
        setImagenCurso(respuesta);
    });
  };

  return (
    <Container component="main" maxWidth="md" justify="center">
      <div style={style.paper}>
        <Typography component="h1" variant="h5">
          Registro de nuevo curso
        </Typography>
        <form style={style.form} onSubmit={onSubmitCursoHandler}>
          <Grid container spacing={2}>
            <Grid item xs={12} md={12}>
              <TextField
                value={curso.Titulo}
                onChange={onChangeCursoHandler}
                name="Titulo"
                variant="outlined"
                fullWidth
                label="Ingrese Título"
              />
            </Grid>
            <Grid item xs={12} md={12}>
              <TextField
                value={curso.Descripcion}
                onChange={onChangeCursoHandler}
                name="Descripcion"
                variant="outlined"
                fullWidth
                label="Ingrese Descripción"
              />
            </Grid>
            <Grid item xs={12} md={6}>
              <TextField
                value={curso.Precio}
                onChange={onChangeCursoHandler}
                name="Precio"
                variant="outlined"
                fullWidth
                label="Ingrese Precio Normal"
              />
            </Grid>
            <Grid item xs={12} md={6}>
              <TextField
                value={curso.Promocion}
                onChange={onChangeCursoHandler}
                name="Promocion"
                variant="outlined"
                fullWidth
                label="Ingrese Precio Promoción"
              />
            </Grid>
            <Grid item xs={12} md={6}>
              <MuiPickersUtilsProvider utils={DateFnsUtils}>
                <KeyboardDatePicker
                  value={fechaSeleccionada}
                  onChange={setFechaSeleccionada}
                  margin="normal"
                  id="fecha-publicacion-id"
                  label="Seleccione fecha de publicación"
                  format="dd/MM/yyyy"
                  fullWidth
                  KeyboardButtonProps={{
                    "aria-label": "change date",
                  }}
                />
              </MuiPickersUtilsProvider>
            </Grid>
            <Grid item xs={12} md={6}>
              <ImageUploader 
                withIcon={false}
                key={fotoKey}
                singleImage={true}
                buttonText='Seleccione una imagen del curso'
                onChange={subirFoto}
                imgExtension={['.jpg', '.gif', '.png', 'jpeg']}
                maxFileSize={5242880}
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
                Guardar Curso
              </Button>
            </Grid>
          </Grid>
        </form>
      </div>
    </Container>
  );
};

export default NuevoCurso;
