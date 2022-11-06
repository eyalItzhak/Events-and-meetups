import React from "react"; //react compnent return jsx
import { Button, Container, Menu } from "semantic-ui-react";

export default function NavBar() {
  return (
    <Menu inverted fixed="top">
      <Container>
        <Menu.Item header>
          <img src="/assets/logo.png" alt="logo" style={{marginRight : "10px"}} />
          Reactivity!
        </Menu.Item>
        <Menu.Item name="Activities" />
        <Menu.Item>
          <Button positive content="create Activity" />
        </Menu.Item>
      </Container>
    </Menu>
  );
}
