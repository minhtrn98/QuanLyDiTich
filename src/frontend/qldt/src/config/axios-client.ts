import { endpoints } from '@/constants'
import { GlobalError, tokenService } from '@/services'
import type { AxiosResponse } from 'axios'
import axios from 'axios'

const { authEndPoints } = endpoints

const axiosClient = axios.create({
  headers: {
    'Content-Type': 'application/json'
  }
})

axiosClient.interceptors.request.use((config: any) => {
  const token = tokenService.getAccessToken()

  if (token) {
    config.headers.Authorization = `Bearer ${token}`
  }

  return config
})

axiosClient.interceptors.response.use(
  (res: AxiosResponse) => {
    return res
  },
  async (err) => {
    GlobalError.checkTokenExpiredError(err)

    const originalConfig = err.config
    const refreshTokenUrl = authEndPoints.refreshToken

    if (originalConfig.url !== refreshTokenUrl && err.response) {
      // Access Token was expired
      if (err.response.status === 401 && !originalConfig._retry) {
        originalConfig._retry = true

        try {
          const refreshToken = tokenService.getRefreshToken()

          const res = await axiosClient.post(refreshTokenUrl, { refreshToken })

          tokenService.updateAccessToken(res.data.accessToken)
          tokenService.updateRefreshToken(res.data.refreshToken)

          return axiosClient(originalConfig)
        } catch (_error) {
          tokenService.clearToken()

          return Promise.reject(_error)
        }
      }
    }

    return Promise.reject(err)
  }
)
export default axiosClient
