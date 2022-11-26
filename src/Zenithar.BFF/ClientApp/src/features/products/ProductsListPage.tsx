import PageHeader from 'lib/PageHeader'
import {Col, Row, Spinner} from 'react-bootstrap'
import ProductCard from 'features/products/ProductCard'
import productsApi, {ProductDto} from 'features/products/productsApi'

const ProductsListPage = () => {
  const {data: productsResponse} = productsApi.useGetAllProductsQuery()

  return (
    <>
      <PageHeader>Products</PageHeader>
      <Row xs={1} sm={2} md={3} className="g-4 justify-content-center">
        {productsResponse
          ? <ProductsList products={productsResponse.items}/>
          : <Spinner animation={'border'} variant="info"/>
        }
      </Row>
    </>
  )
}

const ProductsList = ({products}: { products: ProductDto[] }) => {
  return (
    <>
      {products.map(product =>
        <Col key={product.id}>
          <ProductCard {...product} />
        </Col>
      )}
    </>
  )
}


export default ProductsListPage