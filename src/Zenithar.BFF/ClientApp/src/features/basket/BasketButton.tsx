import React, {useState} from 'react'
import BasketOffcanvas from 'features/basket/BasketOffcanvas'
import {Button} from 'react-bootstrap'
import {ButtonProps} from 'react-bootstrap/Button'

const BasketButton = (props: ButtonProps) => {
  const [showBasket, setShowBasket] = useState(false)

  return (
    <>
      <Button
        variant={'info'}
        {...props}
        onClick={() => setShowBasket(true)}
      >
        <i className="bi bi-basket3"/>
      </Button>
      <BasketOffcanvas
        show={showBasket}
        onHide={() => setShowBasket(false)}
        placement={'end'}
      />
    </>
  )
}
export default BasketButton