import { Input } from '@/components/ui/input'
import React from 'react'
import { useWashingMachinesContext } from './context'

function WashingMachinesFilter() {
  const context = useWashingMachinesContext()
  const { filterWashingMachinesByName } = context || {}

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { value } = e.target
    filterWashingMachinesByName?.(value)
  }

  return (
    <div className='h-full w-full px-1 py-2'>
      <h3 className='text-center text-xl font-semibold'>Bộ lọc tìm kiếm</h3>
      <div className='mt-3'>
        <Input placeholder='Nhập tên sản phẩm...' onChange={handleInputChange} />
      </div>
    </div>
  )
}

export default WashingMachinesFilter
