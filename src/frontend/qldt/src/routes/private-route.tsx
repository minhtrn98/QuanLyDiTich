import type { FC } from 'react'
import type { RouteProps } from 'react-router'

import React from 'react'
import { Navigate } from 'react-router-dom'
import { tokenService } from '@/services'
import PATH from '@/constants/path'

const PrivateRoute: FC<RouteProps> = (props) => {
  const logged = tokenService.getAccessToken()

  return logged ? (
    (props.element as React.ReactElement)
  ) : (
    <Navigate to={PATH.LOGIN} replace />
  )
}

export default PrivateRoute
