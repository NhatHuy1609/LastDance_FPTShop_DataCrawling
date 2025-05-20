import { Input } from '@/components/ui/input'
import { useTelevisionsContext } from './context'

export default function FilterByName() {
  const { filterTelevisionsByName } = useTelevisionsContext()

  return (
    <div>
      <h3 className='mb-2 font-medium'>Tên sản phẩm</h3>
      <Input
        placeholder='Nhập tên sản phẩm...'
        onChange={(e) => filterTelevisionsByName?.(e.target.value)}
        className='w-full'
      />
    </div>
  )
}
