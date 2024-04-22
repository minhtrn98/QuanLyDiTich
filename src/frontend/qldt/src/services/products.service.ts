import qs from 'query-string'
import { ApiResponse } from '@/models/types/response'
import { endpoints } from '@/constants'
import axios from 'axios'
import { Product } from '@/models/types/products'

const getInfiniteProducts = async ({
  pageParam,
  queryKey
}: {
  pageParam: { skip: number; limit: number }
  queryKey: string[]
}) => {
  const url = qs.stringifyUrl({
    url: endpoints.productEndpoints.productsSearch,
    query: { ...pageParam, q: queryKey[1] }
  })
  try {
    const apiResponse = await axios.get(url)

    return apiResponse.data as ApiResponse<'products', Product>
  } catch (error) {
    throw new Error('Error fetching products')
  }
}

export default { getInfiniteProducts }
