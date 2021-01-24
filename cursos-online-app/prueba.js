const axios = require("axios");

axios.post("http://localhost:5000/api/User/login", {
    "email" : "ak47andre95@gmail.com",
    "Password" : "Yoongihoseok95$"
}).then(response => {
    console.log(response);
});