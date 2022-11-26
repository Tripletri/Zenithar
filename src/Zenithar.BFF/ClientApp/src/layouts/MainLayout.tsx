import {Container} from 'react-bootstrap'
import {Outlet} from 'react-router-dom'
import AppNavbar from '../lib/AppNavbar'
import ErrorBoundary from '../lib/ErrorBoundary'

const MainLayout = () => {
  return (
    <>
      <AppNavbar/>
      <Container className="my-5 text-white">
        <ErrorBoundary>
          <Outlet/>
        </ErrorBoundary>
      </Container>
    </>
  )
}

export default MainLayout