import React from 'react'
import {basketApi, ProductsBatchDto} from 'features/basket/basketApi'
import {Button, Card, Col, Image, Row} from 'react-bootstrap'
import styles from './BasketProductCard.module.scss'


const BasketProductCard = ({product, quantity}: ProductsBatchDto) => {
  const [addProduct, {isLoading: idAdding}] = basketApi.useAddProductMutation()
  const [removeProduct, {isLoading: isRemoving}] = basketApi.useRemoveProductMutation()

  return (
    <Card className={styles.card}>
      <Row className={`g-0`}>
        <Col xs={'3'}>
          <Image
            src={product.previewUrl}
            className={`rounded-start ${styles.img}`}
          />
        </Col>
        <Col xs={9}>
          <Card.Body>
            <Card.Title className={styles.title}>
              {product.name}
            </Card.Title>
            <Col>
              <Row className="align-items-center">
                <Col xs={5}>
                  Quantity:
                </Col>
                <Col className="gx-0">
                  {quantity}
                </Col>
                <Col xs={5}>
                  <Button
                    variant="danger"
                    className={styles.btn}
                    onClick={() => removeProduct(product.id)}
                    disabled={isRemoving}
                  >
                    <i className="bi bi-dash-square"/>
                  </Button>
                  <Button
                    variant="success"
                    className={styles.btn + ' ms-1'}
                    onClick={() => addProduct(product.id)}
                    disabled={idAdding}
                  >
                    <i className="bi bi-plus-square"/>
                  </Button>
                </Col>
              </Row>
              <Row>
                <Col xs={5}>
                  Price:
                </Col>
                <Col className="gx-0">
                  {product.price}$
                </Col>
              </Row>
              <Row>
                <Col xs={5}>
                  TotalPrice:
                </Col>
                <Col className="gx-0">
                  {product.price * quantity}$
                </Col>
              </Row>
            </Col>
          </Card.Body>
        </Col>
      </Row>
    </Card>
  )
}

export default BasketProductCard