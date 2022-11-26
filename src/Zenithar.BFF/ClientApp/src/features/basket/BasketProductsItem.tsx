import React from 'react'
import {Image, ListGroup, Table} from 'react-bootstrap'
import {ProductsBatchDto} from 'features/basket/basketApi'

const BasketProductsItem = ({product, quantity}: ProductsBatchDto) => {
  return (
    <ListGroup.Item>
      <Image
        className={'me-1'}
        rounded
        src={product.previewUrl}
        style={{'maxHeight': '50px', 'maxWidth': '50px'}}
      />
      <span className="fw-bold overflow-hidden">
        {product.name}
      </span>
      <Table striped bordered hover size="sm" className={'mt-1'}>
        <thead>
        <tr>
          <th>Quantity</th>
          <th>Price</th>
          <th>Sum</th>
        </tr>
        </thead>
        <tbody>
        <tr>
          <th>{quantity}</th>
          <th>{product.price}$</th>
          <th>{product.price * quantity}$</th>
        </tr>
        </tbody>
      </Table>
    </ListGroup.Item>
  )
}

export default BasketProductsItem
