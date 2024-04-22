import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.tsx'
import './index.css'
import { QueryProvider } from './components/providers/query-provider.tsx'
import { Provider } from 'react-redux'
import store from './stores/index.ts'
import { HistoryRouter, history } from './routes/history.tsx'

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <Provider store={store}>
      <QueryProvider>
        <HistoryRouter history={history}>
          <App />
        </HistoryRouter>
      </QueryProvider>
    </Provider>
  </React.StrictMode>
)
