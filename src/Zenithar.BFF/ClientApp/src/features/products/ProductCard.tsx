import {ProductDto} from 'features/products/productsApi'
import styles from './ProductCard.module.scss'
import {Button, Card} from 'react-bootstrap'
import {basketApi} from 'features/basket/basketApi'

const ProductCard = ({id, name, previewUrl}: ProductDto) => {
  const [addProduct] = basketApi.useAddProductMutation()

  return (
    <Card className={styles.card}>
      <Card.Img
        variant="top"
        className={styles.img}
        src={previewUrl}
      />
      <Card.Body className={styles.cardBody}>
        {name}
      </Card.Body>
      <Card.Footer>
        <Button
          variant={'light'}
          className="w-100"
          onClick={() => addProduct(id)}
        >
          Add to basket
        </Button>
      </Card.Footer>
    </Card>
  )
}

export default ProductCard