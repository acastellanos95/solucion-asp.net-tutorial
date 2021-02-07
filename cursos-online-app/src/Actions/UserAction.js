import HttpClient from "../Services/HttpClient";
import axios from 'axios';

const instancia = axios.create();
instancia.CancelToken = axios.CancelToken;
instancia.isCancel = axios.isCancel;

export const registerUser = (user) => {
  return new Promise((resolve, eject) => {
    instancia.post("/User/register", user).then(response => {
      resolve(response);
    });
  });
}

export const obtainCurrentUser = (dispatch) => {
  return new Promise((resolve, eject) => {
    HttpClient.get("/User")
    .then((response) => {
      console.log('response: ', response);
      if(response.data && response.data.imagenPerfil){
        let fotoPerfil = response.data.imagenPerfil;
        const nuevoFile = 'data:image/'+fotoPerfil.extension + ';base64,' + fotoPerfil.data;
        response.data.imagenPerfil = nuevoFile;
      }
      dispatch({
        type : 'INICIAR_SESION',
        sesion : response.data,
        autenticado : true
      });

      resolve(response);
    })
    .catch((error) => {
      console.log('error obtener perfil', error);
      
      resolve(error);
    });
  });
}

export const updateUser = (usuario, dispatch) => {
  return new Promise((resolve, reject) => {
    HttpClient.put("/User", usuario)
    .then(response => {
      if(response.data && response.data.imagenPerfil){
        let fotoPerfil = response.data.imagenPerfil;
        const nuevoFile = 'data:image/'+fotoPerfil.extension + ';base64,' + fotoPerfil.data;
        response.data.imagenPerfil = nuevoFile;
      }
      dispatch({
        type: "INICIAR_SESION",
        sesion: response.data,
        autenticado: true
      })
      resolve(response)
    })
    .catch(error => {
      resolve(error.response);
    });
  });
}

export const loginUser = (user, dispatch) => {
  return new Promise((resolve, reject) => {
    instancia.post("/User/login", user)
    .then(response => {
      if(response.data && response.data.imagenPerfil){
        let fotoPerfil = response.data.imagenPerfil;
        const nuevoFile = 'data:image/'+fotoPerfil.extension + ';base64,' + fotoPerfil.data;
        response.data.imagenPerfil = nuevoFile;
      }
      dispatch({
        type: "INICIAR_SESION",
        sesion: response.data,
        autenticado: true,
      });

      resolve(response);
    })
    .catch(error => {
      resolve(error.response);
    });
  });
}

