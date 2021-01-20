import HttpClient from "../Services/HttpClient";

export const registerUser = (user) => {
  return new Promise((resolve, eject) => {
    HttpClient.post("/user/register", user).then(response => {
      resolve(response);
    });
  });
}

export const obtainCurrentUser = (dispatch) => {
  return new Promise((resolve, eject) => {
    HttpClient.get("/user").then(response => {
      // dispatch({
      //   type : 'INICIAR_SESION',
      //   sesion : response.data,
      //   autenticado : true
      // });
      resolve(response);
    });
  });
}

export const updateUser = (user) => {
  return new Promise((resolve, eject) => {
    HttpClient.put("/user", user).then(response => {
      resolve(response);
    })
    .catch(error => {
      resolve(error.response);
    });
  });
}

export const loginUser = (user) => {
  return new Promise((resolve, eject) => {
    HttpClient.post("/user/login", user).then(response => {
      resolve(response);
    });
  });
}