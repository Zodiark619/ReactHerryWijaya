import { createSlice } from "@reduxjs/toolkit"
 
import { resetRedux } from "./actions"
const initialState = { count: 10 }

export const counterSlice = createSlice({
    name: "counter",
    initialState: initialState,
    reducers: {
        increment: (state) => {
            state.count += 1
        },
        decrement: (state) => {
            state.count -= 1

        },
        incrementMultiplier: (state, action) => {
            state.count += Number(action.payload)
        },
        decrementMultiplier: (state, action) => {
            state.count -= Number(action.payload)
        },
        //reset: (state) => {
        //    state.count=10
        //}

    },
    extraReducers: (builder) => {
        //builder.addCase(resetDestination.toString(), (state,action) => {
        //    state.count = 10
        //})
        builder.addCase(resetRedux.toString(), (state, action) => {
            state.count = 10
        })
    }
})

export const { increment, decrement, incrementMultiplier, decrementMultiplier  } = counterSlice.actions
export const counterReducer = counterSlice.reducer