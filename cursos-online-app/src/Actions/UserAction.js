import HttpClient from "../Services/HttpClient";

export const registerUser = (user) => {
  return new Promise((resolve, eject) => {
    HttpClient.post("/user/register", user).then(response => {
      resolve(response);
    });
  });
}

export const obtainCurrentUser = () => {
  return new Promise((resolve, eject) => {
    HttpClient.get("/user").then(response => {
      resolve(response);
    });
  });
}

export const updateUser = (user) => {
  return new Promise((resolve, eject) => {
    HttpClient.put("/user", user).then(response => {
      resolve(response);
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