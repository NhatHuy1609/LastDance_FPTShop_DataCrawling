import React, { useContext, useState } from 'react'
import { GamingGearsContext } from './context'

function GamingGearsFilter() {
  const {
    filterGamingGearsByName,
    filterGamingGearsByCategory,
    filterGamingGearsByPrice,
    sortGamingGears
  } = useContext(GamingGearsContext)
  const [name, setName] = useState('')
  const [category, setCategory] = useState('')
  const [minPrice, setMinPrice] = useState<number>()
  const [maxPrice, setMaxPrice] = useState<number>()
  const [sortBy, setSortBy] = useState('')
  const [isDescending, setIsDescending] = useState(false)

  const handleNameChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setName(e.target.value)
    filterGamingGearsByName(e.target.value)
  }

  const handleCategoryChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    setCategory(e.target.value)
    filterGamingGearsByCategory(e.target.value)
  }

  const handlePriceChange = () => {
    filterGamingGearsByPrice(minPrice, maxPrice)
  }

  const handleSortChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    setSortBy(e.target.value)
    sortGamingGears(e.target.value, isDescending)
  }

  const handleSortDirectionChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setIsDescending(e.target.checked)
    sortGamingGears(sortBy, e.target.checked)
  }

  return (
    <div className='rounded-lg bg-white/10 p-6 backdrop-blur-sm'>
      <h2 className='mb-6 text-xl font-semibold text-white'>Bộ lọc sản phẩm</h2>
      <div className='flex flex-col gap-6'>
        <div className='flex flex-col gap-2'>
          <label htmlFor='name' className='text-sm font-medium text-slate-200'>
            Tên sản phẩm
          </label>
          <input
            type='text'
            id='name'
            value={name}
            onChange={handleNameChange}
            className='rounded-lg border border-slate-600 bg-white/5 px-4 py-2 text-white placeholder-slate-400 focus:border-sky-500 focus:ring-1 focus:ring-sky-500 focus:outline-none'
            placeholder='Nhập tên sản phẩm...'
          />
        </div>

        <div className='flex flex-col gap-2'>
          <label htmlFor='category' className='text-sm font-medium text-slate-200'>
            Danh mục
          </label>
          <select
            id='category'
            value={category}
            onChange={handleCategoryChange}
            className='rounded-lg border border-slate-600 bg-white/5 px-4 py-2 text-white focus:border-sky-500 focus:ring-1 focus:ring-sky-500 focus:outline-none'
          >
            <option value=''>Tất cả</option>
            <option value='Chuột'>Chuột</option>
            <option value='Bàn phím'>Bàn phím</option>
            <option value='Tai nghe'>Tai nghe</option>
            <option value='Lót chuột'>Lót chuột</option>
          </select>
        </div>

        <div className='flex flex-col gap-2'>
          <label className='text-sm font-medium text-slate-200'>Giá</label>
          <div className='flex gap-4'>
            <input
              type='number'
              value={minPrice || ''}
              onChange={(e) => setMinPrice(Number(e.target.value))}
              onBlur={handlePriceChange}
              className='w-1/2 rounded-lg border border-slate-600 bg-white/5 px-4 py-2 text-white placeholder-slate-400 focus:border-sky-500 focus:ring-1 focus:ring-sky-500 focus:outline-none'
              placeholder='Giá tối thiểu'
            />
            <input
              type='number'
              value={maxPrice || ''}
              onChange={(e) => setMaxPrice(Number(e.target.value))}
              onBlur={handlePriceChange}
              className='w-1/2 rounded-lg border border-slate-600 bg-white/5 px-4 py-2 text-white placeholder-slate-400 focus:border-sky-500 focus:ring-1 focus:ring-sky-500 focus:outline-none'
              placeholder='Giá tối đa'
            />
          </div>
        </div>

        <div className='flex flex-col gap-2'>
          <label htmlFor='sort' className='text-sm font-medium text-slate-200'>
            Sắp xếp theo
          </label>
          <div className='flex gap-4'>
            <select
              id='sort'
              value={sortBy}
              onChange={handleSortChange}
              className='flex-1 rounded-lg border border-slate-600 bg-white/5 px-4 py-2 text-white focus:border-sky-500 focus:ring-1 focus:ring-sky-500 focus:outline-none'
            >
              <option value=''>Mặc định</option>
              <option value='name'>Tên</option>
              <option value='price'>Giá</option>
              <option value='category'>Danh mục</option>
            </select>
            <label className='flex items-center gap-2'>
              <input
                type='checkbox'
                checked={isDescending}
                onChange={handleSortDirectionChange}
                className='h-4 w-4 rounded border-slate-600 bg-white/5 text-sky-500 focus:ring-sky-500'
              />
              <span className='text-sm text-slate-200'>Giảm dần</span>
            </label>
          </div>
        </div>
      </div>
    </div>
  )
}

export default GamingGearsFilter
