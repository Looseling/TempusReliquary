import { Routes, Route } from "react-router-dom";
import { Container } from "react-bootstrap";
import Home from "./pages/Home";
import About from "./pages/About";
import NavBar from "./components/Navbar";
import TimeCapsule from "./pages/TimeCapsule";
import Detail from "./pages/Detail";
import Register from "./pages/Register";
import Login from "./pages/Login";
import ViewTimeCapsule from "./pages/ViewTimeCapsule";
import Greeting from "./pages/Greeting";

function App() {
  return (
    <>
      <NavBar />
      <Container className="mb-4">
        <Routes>
          <Route path="/Public" element={<Home />}></Route>
          <Route path="/" element={<Greeting />}></Route>

          <Route
            path="/ViewTimeCapsule/:id"
            element={<ViewTimeCapsule />}
          ></Route>
          <Route path="/Register" element={<Register />}></Route>
          <Route path="/Login" element={<Login />}></Route>
          <Route path="/about" element={<About />}></Route>
          <Route path="/TimeCapsule" element={<TimeCapsule />}></Route>
          <Route path="/TimeCapsule/detail/:id" element={<Detail />}></Route>
        </Routes>
      </Container>
    </>
  );
}

export default App;
