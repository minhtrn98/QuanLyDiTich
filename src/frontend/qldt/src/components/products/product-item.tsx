import { Product } from '@/models/types/products'

interface ProductItemProps {
  product: Product
}

export const ProductItem = ({ product }: ProductItemProps) => {
  return (
    <div className='w-96 bg-white rounded-lg shadow-md p-4 mb-4 overflow-ellipsis truncate'>
      <img
        src={product.thumbnail}
        alt={product.title}
        className='w-fit h-auto mb-4 object-contain'
      />
      <h2 className='text-lg font-semibold text-gray-800 mb-2 overflow-ellipsis truncate'>
        {product.description}
      </h2>
      <p className='text-gray-600 mb-2'>{product.description}</p>
      <p className='text-gray-800 font-semibold mb-2'>
        Price: ${product.price}
      </p>
      <div className='flex items-center'>
        <span className='text-gray-600 mr-2'>Brand:</span>
        <span className='text-gray-800 font-semibold'>{product.brand}</span>
      </div>
      <div className='flex items-center mt-2'>
        <span className='text-gray-600 mr-2'>Category:</span>
        <span className='text-gray-800 font-semibold'>{product.category}</span>
      </div>
    </div>
  )
}
