export const BASE_URL = import.meta.env.VITE_API_URL

const productEndpoints = {
  products: BASE_URL + '/products',
  productsSearch: BASE_URL + '/products/search'
}

const authEndPoints = {
  login: BASE_URL + '/auth/login',
  refreshToken: BASE_URL + '/auth/refresh-token'
}

export default {
  productEndpoints,
  authEndPoints
}
