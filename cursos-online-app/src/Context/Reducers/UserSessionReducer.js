export const initialState = {
  usuario: {
    nombreCompleto: "",
    email: "",
    username: "",
    imagen: "",
  },
  autenticado: false,
};

const userSessionReducer = (state = initialState, action) => {
  switch (action.type) {
    case "INICIAR_SESION":
      return {
        ...state,
        usuario: action.sesion,
        autenticado: action.autenticado,
      };

    case "SALIR_SESION":
      return {
        ...state,
        usuario: action.nuevoUsuario,
        autenticado: action.autenticado, //debería ser false
      };

    case "ACTUALIZAR_USUARIO":
      return {
        ...state,
        usuario: action.nuevoUsuario,
        autenticado: action.autenticado,
      };

    default:
      return state;
  }
};

export default userSessionReducer;
