import React from 'react'
import SortControls from './SortControls'
import FilterByPriceRange from './FilterByPriceRange'
import FilterByName from './FilterByName'

function LaptopsFilter() {
  return (
    <div className='space-y-6 rounded p-5'>
      <h3 className='border-b pb-3 text-center text-xl font-semibold'>Bộ lọc sản phẩm</h3>
      <FilterByName />
      <FilterByPriceRange />
      <SortControls />
    </div>
  )
}

export default LaptopsFilter
