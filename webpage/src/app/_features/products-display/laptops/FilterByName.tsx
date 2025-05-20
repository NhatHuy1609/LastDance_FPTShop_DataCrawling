import { Input } from '@/components/ui/input'
import { useLaptopsContext } from './context'

export default function FilterByName() {
  const { filterLaptopsByCategory } = useLaptopsContext()

  return (
    <div>
      <h3 className='mb-2 font-medium'>Tên sản phẩm</h3>
      <Input
        placeholder='Nhập tên sản phẩm...'
        onChange={(e) => filterLaptopsByCategory?.(e.target.value)}
        className='w-full'
      />
    </div>
  )
}
