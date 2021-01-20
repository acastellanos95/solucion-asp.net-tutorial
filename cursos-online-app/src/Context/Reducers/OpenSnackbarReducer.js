const initialState = {
  open: false,
  mensaje: "",
};

const openSnackbarReducer = (state = initialState, action) => {
  console.log('action', action);
  switch (action.type) {
    case "OPEN_SNACKBAR":
      return {
        ...state,
        open: action.openMessage.open,
        mensaje: action.openMessage.mensaje,
      };

    default:
      return state;
  }
};

export default openSnackbarReducer;
