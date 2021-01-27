import HttpClient from "../Services/HttpClient";

export const registerUser = (user) => {
  return new Promise((resolve, eject) => {
    HttpClient.post("/User/register", user).then(response => {
      resolve(response);
    });
  });
}

export const obtainCurrentUser = () => {
  return new Promise((resolve, eject) => {
    HttpClient.get("/User")
    .then((response) => {

      resolve(response);
    })
    .catch((error) => {
      console.log('error obtener perfil', error);
      
      resolve(error);
    });
  });
}

export const updateUser = (user) => {
  return new Promise((resolve, eject) => {
    HttpClient.put("/User", user)
    .then(response => {
      resolve(response);
    })
    .catch(error => {
      resolve(error.response);
    });
  });
}

export const loginUser = (user) => {
  return new Promise((resolve, eject) => {
    HttpClient.post("/User/login", user)
    .then(response => {
      resolve(response);
    })
    .catch(error => {
      resolve(error.response);
    });
  });
}

