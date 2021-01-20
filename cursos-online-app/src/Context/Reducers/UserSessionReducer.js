export const initialState = {
  user: {
    nombreCompleto: "",
    email: "",
    username: "",
    foto: "",
  },
  autenticado: false,
};

const userSessionReducer = (state = initialState, action) => {
  switch (action.type) {
    case "INICIAR_SESION":
      return {
        ...state,
        user: action.sesion,
        autenticado: action.autenticado,
      };

    case "SALIR_SESION":
      return {
        ...state,
        user: action.nuevoUsuario,
        autenticado: action.autenticado, //debería ser false
      };

    case "ACTUALIZAR_USUARIO":
      return {
        ...state,
        user: action.nuevoUsuario,
        autenticado: action.autenticado,
      };

    default:
      return state;
  }
};

export default userSessionReducer;
