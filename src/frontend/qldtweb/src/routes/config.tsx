import { ROLE } from '@/models/enums/role.enum'
import type { FC, ReactElement } from 'react'

export interface WrapperRouteProps {
  /** document title locale id */
  title: string
  /** authorization？ */
  roles?: ROLE[]
  element: JSX.Element | ReactElement | null
}

const WrapperRouteComponent: FC<WrapperRouteProps> = ({
  title,
  roles,
  element
}) => {
  // const { loggedIn, role } = useAuth();
  // const navigate = useNavigate();

  if (title) {
    document.title = title
  }

  // return !roles || roles.some(ro => ro === role) ? (
  return true ? element : <div>403</div>
}

export default WrapperRouteComponent
