import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue
} from '@/components/ui/select'
import { useMonitorsContext } from './context'
import { useState, useEffect } from 'react'

const categories: { label: string; value: string }[] = [
  { label: 'Tất cả danh mục', value: 'Monitor' },
  { label: 'Màn hình game', value: 'Gaming Monitor' },
  { label: 'Màn hình cong', value: 'Curved Monitor' },
  { label: 'Màn hình đồ họa', value: 'Graphic Monitor' }
]

export default function FilterByCategory() {
  const { filterMonitorsByCategory } = useMonitorsContext()
  const [selected, setSelected] = useState('Monitor')

  // Apply initial filter when component mounts
  useEffect(() => {
    filterMonitorsByCategory?.(selected)
  }, [])

  const handleChange = (value: string) => {
    setSelected(value)
    filterMonitorsByCategory?.(value)
  }

  return (
    <div>
      <h3 className='mb-2 font-medium'>Danh mục</h3>
      <Select value={selected} onValueChange={handleChange}>
        <SelectTrigger className='w-full'>
          <SelectValue placeholder='Tất cả danh mục' />
        </SelectTrigger>
        <SelectContent>
          {categories.map((category) => (
            <SelectItem key={category.value} value={category.value}>
              {category.label}
            </SelectItem>
          ))}
        </SelectContent>
      </Select>
    </div>
  )
}
