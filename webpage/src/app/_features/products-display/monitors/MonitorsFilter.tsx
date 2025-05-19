import React from 'react'
import FilterByName from './FilterByName'
import FilterByCategory from './FilterByCategory'
import FilterByPriceRange from './FilterByPriceRange'
import SortControls from './SortControls'

function MonitorsFilter() {
  return (
    <div className='space-y-6 p-5 rounded'>
      <h3 className='text-center text-xl font-semibold border-b pb-3'>Bộ lọc sản phẩm</h3>
      <FilterByName />
      <FilterByCategory />
      <FilterByPriceRange />
      <SortControls />
    </div>
  )
}

export default MonitorsFilter
