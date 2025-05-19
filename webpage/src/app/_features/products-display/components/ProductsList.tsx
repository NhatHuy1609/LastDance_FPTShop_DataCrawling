import React from 'react'
import { BaseProductDisplay } from '../types'
import ProductItem from './ProductItem'
import LoadMoreButton from './LoadMoreButton'

type Props<T> = {
  data: T[]
}

function ProductsList<T extends BaseProductDisplay>({ data }: Props<T>) {
  return (
    <ul className='grid w-full grid-cols-4 gap-6'>
      {data.map((item) => (
        <li key={item.id}>
          <ProductItem item={item} />
        </li>
      ))}
    </ul>
  )
}

export default ProductsList
