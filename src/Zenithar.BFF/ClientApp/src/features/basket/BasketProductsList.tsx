import React from 'react'
import {ProductsBatchDto} from 'features/basket/basketApi'
import BasketProductCard from 'features/basket/BasketProductCard'
import styles from './BasketProductsList.module.scss'

interface BasketProductsListProps {
  productBatches: ProductsBatchDto[]
}

const BasketProductsList = ({productBatches}: BasketProductsListProps) => {
  return (
    <div className={styles.list}>
      {productBatches.map(batch => <BasketProductCard {...batch}/>)}
    </div>
  )
}

export default BasketProductsList