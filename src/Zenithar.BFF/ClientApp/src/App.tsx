import {Route, Routes} from "react-router-dom"
import MainLayout from "./layouts/MainLayout"
import ProductsListPage from "./features/products/ProductsListPage"
import ProductsManagementPage from "./features/adminFeatures/products/ProductsManagementPage";


function App() {
  return (
    <Routes>
      <Route element={<MainLayout/>}>
        <Route index element={<ProductsListPage/>}/>
        <Route path="/manage/products" element={<ProductsManagementPage/>}/>
      </Route>
    </Routes>
  )
}

export default App
