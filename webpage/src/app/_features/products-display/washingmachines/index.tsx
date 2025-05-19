import { useWashingMachines } from '@/hooks/apis/washingmachine/useWashingMachines'
import React, { useEffect, useState } from 'react'
import ProductsList from '../components/ProductsList'
import { WashingMachinesContext } from './context'
import { WashingMachine } from '@/entities/washingmachine/washingmachine.types'
import LoadMoreButton from '../components/LoadMoreButton'
import WashingMachinesFilter from './WashingMachinesFilter'


function WashingMachinesDisplay() {
  const { fetchNextPage, hasNextPage, isFetchingNextPage, data, ...result } = useWashingMachines()
  const [washingMachines, setWashingMachines] = useState<WashingMachine[]>(
    () => data?.pages.flatMap((page) => page.washingMachines) || []
  )

  // Update laptops state when data from API changes
  useEffect(() => {
    setWashingMachines((prev) => [...(data?.pages.flatMap((page) => page.washingMachines) || [])])
  }, [data])

  const filterWashingMachinesByName = (name: string) => {
    const filteredWashingMachines = washingMachines.filter((washingMachine) =>
      washingMachine.name.toLowerCase().includes(name.toLowerCase())
    )
    setWashingMachines([...filteredWashingMachines])
  }

  const handleLoadMoreWashingMachines = () => {
    fetchNextPage()
  }

  return (
    <WashingMachinesContext
      value={{
        filterWashingMachinesByName,
        fetchNextPage,
        hasNextPage,
        isFetchingNextPage
      }}
    >
      <div className='relative flex w-full gap-4 p-4'>
        <div className='w-1/4 rounded-md border border-black/60'>
          <WashingMachinesFilter />
        </div>
        <div className='w-full'>
          <ProductsList data={washingMachines} />
          <div className='mt-6 flex w-full justify-center'>
            <LoadMoreButton loadMoreFn={handleLoadMoreWashingMachines} disable={!hasNextPage} />
          </div>
        </div>
      </div>
    </WashingMachinesContext>
  )
}

export default WashingMachinesDisplay
