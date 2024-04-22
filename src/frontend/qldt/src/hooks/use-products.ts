import { useInfiniteQuery } from '@tanstack/react-query'
import { common } from '@/constants'
import { productsService } from '@/services'

const { DEFAULT_PAGING } = common
const { getInfiniteProducts } = productsService

interface Props {
  q: string
}

const useInfiniteProducts = ({ q }: Props) => {
  return useInfiniteQuery({
    queryKey: ['products', q],
    queryFn: getInfiniteProducts,
    initialPageParam: {
      skip: DEFAULT_PAGING.SKIP,
      limit: DEFAULT_PAGING.LIMIT
    },
    getNextPageParam: (lastPage) => {
      if (lastPage.skip + lastPage.limit >= lastPage.total) {
        return undefined
      }

      return {
        skip: lastPage.skip + lastPage.limit,
        limit: lastPage.limit
      }
    }
  })
}

export default {
  useInfiniteProducts
}
