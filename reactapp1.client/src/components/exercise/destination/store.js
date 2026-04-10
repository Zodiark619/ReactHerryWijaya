

import { configureStore } from "@reduxjs/toolkit"
import { destinationAPI } from "./destinationApi"
import { randomdestinationAPI } from "./DestinationApi - Copy"
export const store = configureStore({
    reducer: {
        [destinationAPI.reducerPath]: destinationAPI.reducer,
        [randomdestinationAPI.reducerPath]: randomdestinationAPI.reducer
    },
    middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(destinationAPI.middleware).concat(randomdestinationAPI.middleware)
})