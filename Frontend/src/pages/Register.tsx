import React, { useState } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import { Form, Button, Container } from "react-bootstrap";
import { register } from "../services/UserService";
import { setAuthToken } from "../utilities/http-common";

const Register = () => {
  const [username, setUsername] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const history = useNavigate();

  const handleRegister = async () => {
    try {
      const data = await register(username, email, password);
      localStorage.setItem("token", data.token);
      setAuthToken(data.token);
      toast.success("Registration successful!", {
        position: toast.POSITION.TOP_CENTER,
      });
      history("/timecapsule");
    } catch (err) {
      toast.error("Registration failed!", {
        position: toast.POSITION.TOP_CENTER,
      });
    }
  };
  return (
    <Container
      className="d-flex justify-content-center align-items-center"
      style={{ minHeight: "100vh" }}
    >
      <Form style={{ width: "300px" }}>
        <h3 className="text-center mb-4">Register</h3>
        <Form.Group controlId="formBasicUsername">
          <Form.Label>Username</Form.Label>
          <Form.Control
            type="text"
            placeholder="Enter username"
            onChange={(e) => setUsername(e.target.value)}
          />
        </Form.Group>

        <Form.Group controlId="formBasicEmail">
          <Form.Label>Email address</Form.Label>
          <Form.Control
            type="email"
            placeholder="Enter email"
            onChange={(e) => setEmail(e.target.value)}
          />
        </Form.Group>

        <Form.Group controlId="formBasicPassword">
          <Form.Label>Password</Form.Label>
          <Form.Control
            type="password"
            placeholder="Password"
            onChange={(e) => setPassword(e.target.value)}
          />
        </Form.Group>

        <Button variant="primary" w-100 onClick={handleRegister}>
          Register
        </Button>
      </Form>
    </Container>
  );
};

export default Register;
