import {Route, Routes } from 'react-router-dom'
import MainLayout from './layouts/MainLayout'
import ProductsListPage from './features/products/ProductsListPage'


function App() {
  return (
    <Routes>
      <Route element={<MainLayout/>}>
        <Route index element={<ProductsListPage/>}/>
      </Route>
    </Routes>
  )
}

export default App
