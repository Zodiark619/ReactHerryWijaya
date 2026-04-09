import { createSlice } from "@reduxjs/toolkit"
import { resetRedux } from "./actions"

const initialState = () => {
    return {
        destinations: [
            {
                name: 'hong kong',
                days: 7,
                fact:'worls long asdadwq'
            },
            {
                name: 'asdddw',
                days: 22,
                fact: 'ggg jyjyy'
            },
            {
                name: '21d sfsdfsd',
                days: 8999,
                fact: '1221 sadsa'
            },
        ],
        destinationSelected:null
    }
}

export const destinationSlice = createSlice({
    name: "destination",
    initialState: initialState,
    reducers: { 
        destinationClicked: (state, action) => {
            state.destinationSelected =action.payload
        },
        resetDestination: (state) => {
           // state.destinationSelected=null
        }
    }
    ,
    extraReducers: (builder) => {
        //builder.addCase(resetDestination.toString(), (state,action) => {
        //    state.count = 10
        //})
        builder.addCase(resetRedux.toString(), (state, action) => {
            state.destinationSelected = null
        })
    }
})

export const { destinationClicked } = destinationSlice.actions
export const destinationReducer = destinationSlice.reducer