import { useState } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import { Form, Button, Container } from "react-bootstrap";
import { login } from "../services/UserService";
import { setAuthToken } from "../utilities/http-common";
import { Button as Button2 } from "@material-tailwind/react/";
const Login = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const history = useNavigate();

  const handleLogin = async () => {
    try {
      const data = await login(email, password);
      localStorage.setItem("token", data.token);
      localStorage.setItem("UserName", email);

      setAuthToken(data.token);
      toast.success("Login successful!", {
        position: toast.POSITION.TOP_CENTER,
      });
      history("/timecapsule");
    } catch (err) {
      toast.error("Login failed!", { position: toast.POSITION.TOP_CENTER });
    }
  };

  return (
    <>
      <Form style={{ width: "300px" }}>
        <h3 className="text-center mb-4">Login</h3>
        <Form.Group controlId="formBasicUsername">
          <Form.Label>Email</Form.Label>
          <Form.Control
            type="text"
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

        <Button variant="primary" className="w-100" onClick={handleLogin}>
          Login
        </Button>
      </Form>
      <Button2>asds</Button2>
    </>
  );
};

export default Login;
