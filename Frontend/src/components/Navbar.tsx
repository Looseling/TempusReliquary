import { Button, Container, Nav, Navbar as NavbarBs } from "react-bootstrap";
import { NavLink } from "react-router-dom";
import { useNavigate } from "react-router-dom";

function Navbar() {
  const history = useNavigate();

  // HANDLE LOGOUT EVENT
  const handleLogout = (e: any) => {
    e.preventDefault();
    console.log("Logout");

    // CLEAR DATA FROM STORAGE
    localStorage.clear();
    sessionStorage.clear();

    history("/Public");
  };

  return (
    <NavbarBs sticky="top" className="bg-primary shadow-sm mb-3">
      <Container>
        <NavbarBs.Brand as={NavLink} to="/">
          TimeReliguary
        </NavbarBs.Brand>
        <Nav className="me-auto">
          <Nav.Link href="/TimeCapsule">Time Capsules</Nav.Link>
          <Nav.Link href="/about">about</Nav.Link>
        </Nav>
      </Container>

      <Nav className="me-auto justify-content-end">
        {localStorage.getItem("UserName") == null && (
          <>
            <Nav.Link to="/register" as={NavLink}>
              Register
            </Nav.Link>
            <Nav.Link to="/login" as={NavLink}>
              Login
            </Nav.Link>
          </>
        )}
        {localStorage.getItem("UserName") != null && (
          <>
            <NavbarBs.Text>{localStorage.getItem("UserName")}</NavbarBs.Text>
            <Button onClick={handleLogout}>logout</Button>
          </>
        )}
      </Nav>
    </NavbarBs>
  );
}

export default Navbar;
