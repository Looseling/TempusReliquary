import { Button, Container, Nav, Navbar as NavbarBs } from "react-bootstrap";
import { NavLink } from "react-router-dom";

function Navbar() {
  return (
    <NavbarBs sticky="top" className="bg-white shadow-sm mb-3">
      <Container>
        <Nav className="me-auto">
          <Nav.Link to="/timecapsule" as={NavLink}>
            TimeCapsules
          </Nav.Link>
          <Nav.Link to="/register" as={NavLink}>
            register
          </Nav.Link>
          <Nav.Link to="/login" as={NavLink}>
            login
          </Nav.Link>
        </Nav>
      </Container>
    </NavbarBs>
  );
}

export default Navbar;
