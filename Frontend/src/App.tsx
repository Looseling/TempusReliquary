import { Routes, Route } from "react-router-dom";
import { Container } from "react-bootstrap";
import Home from "./pages/Home";
import About from "./pages/About";
import NavBar from "./components/Navbar";
import { ShoppingCartProvider } from "./context/ShoppingCartContext";
import TimeCapsule from "./pages/TimeCapsule";
import Detail from "./pages/Detail";

function App() {
  return (
    <>
      <ShoppingCartProvider>
        <NavBar />
        <Container className="mb-4">
          <Routes>
            <Route path="/" element={<Home />}></Route>
            <Route path="/about" element={<About />}></Route>
            <Route path="/TimeCapsule" element={<TimeCapsule />}></Route>
            <Route path="/TimeCapsule/detail/:id" element={<Detail />}></Route>
          </Routes>
        </Container>
      </ShoppingCartProvider>
    </>
  );
}

export default App;
