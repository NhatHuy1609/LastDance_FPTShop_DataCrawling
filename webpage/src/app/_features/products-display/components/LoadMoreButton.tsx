import React from 'react'
import { cn } from '@/lib/utils'

type Props = {
  loadMoreFn: () => void
  disable?: boolean
}

function LoadMoreButton({ loadMoreFn, disable = false }: Props) {
  return (
    <button
      type='button'
      className={cn(
        'cursor-pointer rounded-full bg-black px-4 py-2 text-sm text-white hover:opacity-80',
        {
          'cursor-not-allowed opacity-60': disable
        }
      )}
      onClick={loadMoreFn}
    >
      Xem thêm sản phẩm
    </button>
  )
}

export default LoadMoreButton
