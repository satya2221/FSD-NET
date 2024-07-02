import {configureStore} from '@reduxjs/toolkit'
import adopsiHewan from './adoptedPetSlice'
import searchParams from './searchParamSlice'

const store = configureStore({
    reducer: {
        adopsiHewan,
        searchParams
    }
})

export default store