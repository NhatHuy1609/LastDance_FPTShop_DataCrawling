import React from 'react'
import { BaseProductDisplay } from '../types'
import Image from 'next/image'
import { formatCurrencyVND } from '@/utils/currency'
import Link from 'next/link'

type Props<T> = {
  item: T
}

function ProductItem<T extends BaseProductDisplay>({ item }: Props<T>) {
  const totalDiscount = item.price - item.priceDiscount
  const discountPercents = Math.ceil(((item.price - item.priceDiscount) / item.price) * 100)

  return (
    <div className='flex w-full flex-col gap-1'>
      <h1 className='text-xl font-bold'>{item.id}</h1>
      <Link
        className='aspect-[4/3] max-h-[250px] w-full cursor-pointer overflow-hidden'
        href={item.url}
        target='_blank'
      >
        <Image
          src={item.imageUrl}
          alt={item.name}
          width={300}
          height={300}
          className='size-full cursor-pointer object-cover transition-all hover:scale-[1.05]'
        />
      </Link>
      <span className='w-fit rounded-full bg-gray-300 px-1 py-[1px] text-xs text-black'>
        Trả góp 0%
      </span>
      <div className='flex gap-1'>
        <span className='text-xs text-gray-300 line-through decoration-black'>
          {formatCurrencyVND(item.price)}
        </span>
        <span className='text-sm text-red-500'>{discountPercents}%</span>
      </div>
      <span className='text-base font-semibold text-black'>
        {formatCurrencyVND(item.priceDiscount)}
      </span>
      <span className='text-xs text-green-500'>Giảm {formatCurrencyVND(totalDiscount)}</span>
      <p className='text-semibold truncate text-sm font-semibold text-black'>{item.name}</p>
    </div>
  )
}

export default ProductItem
