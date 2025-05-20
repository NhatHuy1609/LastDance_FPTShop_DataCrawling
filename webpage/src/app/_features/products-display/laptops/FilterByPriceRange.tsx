import { Input } from '@/components/ui/input'
import { Button } from '@/components/ui/button'
import { useState } from 'react'
import { useLaptopsContext } from './context'

export default function FilterByPriceRange() {
  const { filterLaptopsByPrice } = useLaptopsContext()
  const [min, setMin] = useState('')
  const [max, setMax] = useState('')

  const applyPriceFilter = () => {
    const minPrice = min ? Number(min) : undefined
    const maxPrice = max ? Number(max) : undefined
    filterLaptopsByPrice?.(minPrice, maxPrice)
  }

  return (
    <div>
      <h3 className='mb-2 font-medium'>Giá</h3>
      <div className='grid grid-cols-2 gap-2'>
        <Input
          type='number'
          placeholder='Giá tối thiểu'
          value={min}
          onChange={(e) => setMin(e.target.value)}
        />
        <Input
          type='number'
          placeholder='Giá tối đa'
          value={max}
          onChange={(e) => setMax(e.target.value)}
        />
      </div>
      <Button
        onClick={applyPriceFilter}
        className='mt-2 w-full cursor-pointer bg-sky-500 text-white hover:bg-sky-600'
      >
        Áp dụng bộ lọc giá
      </Button>
    </div>
  )
}
