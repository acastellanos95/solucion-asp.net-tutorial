import axios from "axios";

axios.defaults.baseURL = "http://localhost:5000/api";

axios.interceptors.request.use((config) => {

  const securityToken = window.localStorage.getItem("JWT_token");
  if(securityToken){
    config.headers.Authorization = "Bearer " + securityToken;
    return config;
  }

}, err => {
  return Promise.reject(err);
});

const GenericRequest = {
  get : (url) => axios.get(url),
  post : (url, body) => axios.post(url, body),
  put : (url, body) => axios.put(url, body),
  delete : (url) => axios.delete(url)
};

export default GenericRequest;