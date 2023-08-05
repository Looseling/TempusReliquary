import React from "react";
import { useNavigate } from "react-router-dom";
import Home from "./Home";

function Greeting() {
  const navigate = useNavigate();

  const LoginRedirect = (e: any) => {
    e.preventDefault();
    navigate("/Login");
  };
  const RegisterRedirect = (e: any) => {
    e.preventDefault();
    navigate("/Register");
  };
  return (
    <>
      <div className="header">
        <h1>Welcome to TimeReliguary</h1>
      </div>
      <div className="main">
        <p>
          TimeReliguary is a time capsule project, where you can store your most
          cherished memories, thoughts, and items for your future self or others
          to discover. Register today and start your personal journey through
          time.
        </p>
        {localStorage.getItem("UserName") == null && (
          <div className="buttons">
            <button onClick={LoginRedirect}>Login</button>
            <button onClick={RegisterRedirect}>Register</button>
          </div>
        )}

        <div>
          <h2>View public sealed time capsules</h2>
          <Home></Home>
        </div>
      </div>
    </>
  );
}

export default Greeting;
