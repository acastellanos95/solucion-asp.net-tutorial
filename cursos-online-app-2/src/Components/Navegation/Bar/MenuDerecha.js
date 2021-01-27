import { Avatar, List, ListItem, ListItemText } from "@material-ui/core";
import React from "react";
import reactImg from "../../../logo.svg";

const MenuDerecha = ({ classes, salirSesion, user }) => (
  <div className={classes.list}>
    <List>
      <ListItem button >
        <Avatar src={ reactImg} />
        <ListItemText
          classes={{ primary: classes.listItemText }}
          primary={user ? user.nombreCompleto : ""}
        />
      </ListItem>
      <ListItem button onClick={salirSesion}>
        <ListItemText
          classes={{ primary: classes.listItemText }}
          primary="Salir"
        />
      </ListItem>
    </List>
  </div>
);

export default MenuDerecha;
