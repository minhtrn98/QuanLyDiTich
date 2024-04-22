import { Suspense } from 'react'
import RenderRouter from './routes'

function App() {
  return (
    <Suspense fallback={null}>
      <RenderRouter />
    </Suspense>
  )
}

export default App
