import { Input } from '@/components/ui/input'
import { useMonitorsContext } from './context'

export default function FilterByName() {
  const { filterMonitorsByName } = useMonitorsContext()

  return (
    <div>
      <h3 className='mb-2 font-medium'>Tên sản phẩm</h3>
      <Input
        placeholder='Nhập tên sản phẩm...'
        onChange={(e) => filterMonitorsByName?.(e.target.value)}
        className="w-full"
      />
    </div>
  )
}
