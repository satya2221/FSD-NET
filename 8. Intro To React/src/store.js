import {configureStore} from '@reduxjs/toolkit'
import adopsiHewan from './adoptedPetSlice'

const store = configureStore({
    reducer: {
        adopsiHewan
    }
})

export default store