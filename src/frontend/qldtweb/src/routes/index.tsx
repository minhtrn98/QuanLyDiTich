import { lazy, type FC } from 'react'
import type { RouteObject } from 'react-router'

import { useRoutes } from 'react-router-dom'
import WrapperRouteComponent from './config'
import PATH from '@/constants/path'

const LayoutPage = lazy(() => import('@/layout'))
const LoginPage = lazy(() => import('@/pages/login'))

const routeList: RouteObject[] = [
  {
    path: PATH.LOGIN,
    element: <WrapperRouteComponent element={<LoginPage />} title='Login' />
  },

  {
    path: '*',
    element: (
      <WrapperRouteComponent element={<div>Not Found</div>} title='404' />
    )
  }
]

const RenderRouter: FC = () => {
  const element = useRoutes(routeList)

  // historyNavigation.navigate = useNavigate();
  // historyNavigation.location = useLocation();

  return element
}

export default RenderRouter
