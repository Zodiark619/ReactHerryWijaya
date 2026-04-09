import { configureStore } from "@reduxjs/toolkit"

import {counterReducer } from "./counterSlice.js"
import { destinationReducer } from "./destinationSlice.js"


export const store = configureStore({
    reducer: { counterStore: counterReducer, destinationStore: destinationReducer }
})