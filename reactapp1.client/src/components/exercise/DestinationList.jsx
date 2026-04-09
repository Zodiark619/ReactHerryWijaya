
import { useState } from "react"
import { useSelector, useDispatch } from "react-redux"
import { destinationClicked } from "./destinationSlice"


function DestinationList() {
    const destinations = useSelector((state) => state.destinationStore.destinations)
    const dispatch = useDispatch()
    return destinations.map((destination, index) => {
        return <div key={index }>
            <p>{ destination.name}</p>
             
            <button onClick={() => dispatch(destinationClicked(destination))}>details</button>
        </div>
    })
   
}

export default DestinationList;