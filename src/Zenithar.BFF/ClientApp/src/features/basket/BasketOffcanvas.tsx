import React from 'react'
import {Button, Col, Offcanvas, Row, Spinner} from 'react-bootstrap'
import {OffcanvasProps} from 'react-bootstrap/Offcanvas'
import {basketApi} from 'features/basket/basketApi'
import BasketProductsList from 'features/basket/BasketProductsList'

function loadingBasketOffcanvas(props: OffcanvasProps) {
  return (
    <Offcanvas {...props}>
      <Offcanvas.Header closeButton>
        <Offcanvas.Title>Basket</Offcanvas.Title>
      </Offcanvas.Header>
      <Offcanvas.Body>
        Basket is loading
        <hr/>
        <Spinner animation={'border'}/>
      </Offcanvas.Body>
    </Offcanvas>
  )
}

const BasketOffcanvas = (props: OffcanvasProps) => {
  const {data: basket} = basketApi.useGetBasketQuery()
  const [clearBasket] = basketApi.useClearMutation()

  if (!basket)
    return loadingBasketOffcanvas(props)

  return (
    <Offcanvas {...props}>
      <Offcanvas.Header closeButton>
        <Col>
          <Offcanvas.Title>
            Basket
          </Offcanvas.Title>
          <Row>
            <Col>
              Items: {basket.totalCount}
            </Col>
            <Col>
              Price: {basket.totalPrice}$
            </Col>
          </Row>
        </Col>
      </Offcanvas.Header>
      <Offcanvas.Body>
        {basket.totalCount > 0
          ?
          <>
            <BasketProductsList productBatches={basket.productBatches}/>
            <Button
              variant="danger"
              onClick={() => clearBasket()}
            >
              Clear basket
            </Button>
          </>
          :
          <h4>Basket is empty</h4>
        }
      </Offcanvas.Body>
    </Offcanvas>
  )
}

export default BasketOffcanvas