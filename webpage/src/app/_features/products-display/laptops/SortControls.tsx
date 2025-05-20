import { Button } from '@/components/ui/button'
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue
} from '@/components/ui/select'
import { useState } from 'react'
import { useLaptopsContext } from './context'

export default function SortControls() {
  const { sortLaptops } = useLaptopsContext()
  const [sortBy, setSortBy] = useState('id')
  const [isDescending, setIsDescending] = useState(false)

  const handleSortChange = (value: string) => {
    setSortBy(value)
    sortLaptops?.(value, isDescending)
  }

  const toggleDirection = () => {
    setIsDescending((prev) => {
      const newDir = !prev
      sortLaptops?.(sortBy, newDir)
      return newDir
    })
  }

  return (
    <div>
      <h3 className='mb-2 font-medium'>Sắp xếp theo</h3>
      <div className='flex w-full gap-2'>
        <div className='flex-1'>
          <Select value={sortBy} onValueChange={handleSortChange}>
            <SelectTrigger className='w-full'>
              <SelectValue placeholder='Sắp xếp theo' />
            </SelectTrigger>
            <SelectContent>
              <SelectItem value='id'>Default</SelectItem>
              <SelectItem value='name'>Name</SelectItem>
              <SelectItem value='price'>Price</SelectItem>
              <SelectItem value='category'>Category</SelectItem>
            </SelectContent>
          </Select>
        </div>
        <Button variant='outline' onClick={toggleDirection} className='shrink-0'>
          {isDescending ? '▼' : '▲'}
        </Button>
      </div>
    </div>
  )
}
