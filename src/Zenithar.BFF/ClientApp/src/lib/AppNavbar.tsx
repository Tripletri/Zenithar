import BasketButton from "features/basket/BasketButton"
import {Container, Navbar} from "react-bootstrap"
import {Link} from "react-router-dom"
import ManageButton from "../features/adminFeatures/ManageButton";

const AppNavbar = () => {
  return (
    <Navbar bg="dark" variant="dark" sticky={"top"}>
      <Container>
        <Link to="/" className="navbar-brand pt-0">Zenithar</Link>
      </Container>
      <ManageButton/>
      <BasketButton className="me-3 ms-3"/>
    </Navbar>
  )
}

export default AppNavbar