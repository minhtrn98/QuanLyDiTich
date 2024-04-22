export const LOCAL_STORAGE_ITEMS = Object.freeze({
  ACCESS_TOKEN: 'accessToken',
  REFRESH_TOKEN: 'refreshToken'
})

const DEFAULT_PAGING = Object.freeze({
  SKIP: 0,
  LIMIT: 20
})

const ERROR_TYPE = Object.freeze({
  TOKEN_EXPIRED: 'TokenExpiredException'
})

export default {
  DEFAULT_PAGING,
  ERROR_TYPE
}
