import { Loader2 } from 'lucide-react'
import { InfiniteScroll } from '../infinite-scroll'
import { ElementRef, useRef, useState } from 'react'
import { useDebounce } from '@/hooks/use-debounce'
import { productsHook } from '@/hooks'
import { InputSearch } from '../input/input-search'
import { ProductItem } from './product-item'

const { useInfiniteProducts } = productsHook

export const ProductList = () => {
  const [searchValue, setSearchValue] = useState('')

  const q = useDebounce(searchValue, 300)

  const inputRef = useRef<ElementRef<'input'>>(null)

  const handleSearchChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setSearchValue(e.target.value)
  }

  const { data, isFetching, isLoading, fetchNextPage } = useInfiniteProducts({
    q
  })

  const products = data?.pages.flatMap((page) => page.products) ?? []
  const totalProducts = data?.pages[0].total ?? 0
  const isEmpty = !products.length && !isFetching

  return (
    <InfiniteScroll
      loader={
        <Loader2 className='h-10 w-10 animate-spin m-auto text-blue-500' />
      }
      fetchNextPage={fetchNextPage}
      isFetching={isFetching}
      isLoading={isLoading}
      hasMore={products.length !== totalProducts}
      isEmpty={isEmpty}
    >
      <div className='flex flex-col m-auto justify-center items-center w-max space-y-2 p-4'>
        <InputSearch
          ref={inputRef}
          placeholder='Enter product name...'
          onChange={handleSearchChange}
        />
        {products.map((product) => (
          <ProductItem product={product} key={product.id} />
        ))}
      </div>
    </InfiniteScroll>
  )
}
