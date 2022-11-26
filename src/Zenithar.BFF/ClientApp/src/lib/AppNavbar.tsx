import BasketButton from 'features/basket/BasketButton'
import {Container, Navbar} from 'react-bootstrap'
import {Link} from 'react-router-dom'

const AppNavbar = () => {
  return (
    <Navbar bg="dark" variant="dark" sticky={'top'}>
      <Container>
        <Link to="/" className="navbar-brand pt-0">Zenithar</Link>
      </Container>
      <BasketButton className="me-3"/>
    </Navbar>
  )
}

export default AppNavbar