const axios = require("axios");

axios.default.defaults.baseURL = "http://localhost:5000/api";

const user = {
  "email" : "ak47andre95@gmail.com",
  "password" : "Yoongihoseok95$"
}

const GenericRequest = {
  get : (url) => axios.get(url),
  post : (url, body) => axios.post(url, body),
  put : (url, body) => axios.put(url, body),
  delete : (url) => axios.delete(url)
};

const loginUser = (user) => {
  return new Promise((resolve, eject) => {
    GenericRequest.post("/User/login", user).then(response => {
      resolve(response);
    })
    .catch(error => {
      resolve(error.response);
    });
  });
}

loginUser(user).then(response => {
  console.log("se logeo exitosamente al usuario", response);
});