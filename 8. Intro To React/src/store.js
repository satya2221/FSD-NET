import {configureStore} from '@reduxjs/toolkit'
import adopsiHewan from './adoptedPetSlice'
import searchParams from './searchParamSlice'
import { petApi } from './petApiService'

const store = configureStore({
    reducer: {
        adopsiHewan,
        searchParams,
        [petApi.reducerPath]: petApi.reducer
    },
    middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(petApi.middleware)
})

export default store