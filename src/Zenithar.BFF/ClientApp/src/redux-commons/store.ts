import {combineReducers, configureStore} from '@reduxjs/toolkit'
import {TypedUseSelectorHook, useDispatch, useSelector} from 'react-redux'
import productsApi from 'features/products/productsApi'
import basketApi from 'features/basket/basketApi'

const rootReducer = combineReducers({
  [productsApi.reducerPath]: productsApi.reducer,
  [basketApi.reducerPath]: basketApi.reducer
})

const store = configureStore({
  reducer: rootReducer,
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(
      productsApi.middleware,
      basketApi.middleware
    )
})

export type AppStore = typeof store
export type AppDispatch = AppStore['dispatch']
export type RootState = ReturnType<typeof rootReducer>

export const useAppDispatch = () => useDispatch<AppDispatch>()
export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector

export const dispatch = store.dispatch

export default store