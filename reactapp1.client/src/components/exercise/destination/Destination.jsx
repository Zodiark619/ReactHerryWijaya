import { useState } from "react";
import { useGetAllDestinationQuery, useDeleteDestinationMutation, useUpdateDestinationMutation } from "./DestinationApi"

function Destination({ destination }) {
    const [deleteDestinationMutation] = useDeleteDestinationMutation()
    const [updateDestinationMutation] = useUpdateDestinationMutation()
    const [isUpdating, setIsUpdating] = useState(false)
    const [newCity, setNewCity] = useState('')
    const [newCountry, setNewCountry] = useState('')
    const handleEditState = () => {
        setNewCountry  (destination.country)
        setNewCity(destination.city)
        setIsUpdating(true)
    }
    const handleEditEnd = () => {
        setNewCountry  ('')
        setNewCity('')
        setIsUpdating(false)
    }
    const handleUpdate = () => {
        let city = '', country = '';
        if (newCity == '') {
            city = destination.city
        } else {
            city = newCity
        }
        if (newCountry == '') {
            country = destination.country
        } else {
            country = newCountry
        }
        updateDestinationMutation({
            id: destination.id,
            country: country,
            city: city,
            daysNeeded: destination.daysNeeded
        })
        setNewCity('')
        setNewCountry('')
        setIsUpdating(!isUpdating)
    }
    return <div  >

        {isUpdating ? 
            
            <div>
                <input placeholder="enter citiy..." name="city" value={newCity} onChange={(e) => setNewCity(e.target.value)} />
                 </div>
          
            :
            <div>
                <p>{destination.city} </p>
                

            </div>

        }
        {isUpdating ? 
            
            <div>
                <input placeholder="enter counter..." name="country" value={newCountry} onChange={(e) => setNewCountry(e.target.value)} />
              </div>
          
            :
            <div>
                <p>{destination.country} - {destination.daysNeeded}</p>


            </div>

        }




       
               {isUpdating && 
            <span>
                <button onClick={() => handleEditEnd()}>cancel</button>
                <button onClick={() => handleUpdate()}>update</button>
                    </span>
                }
                {!isUpdating && <span>
            <button onClick={() => deleteDestinationMutation({ id: destination.id })}>Delete</button>
            <button onClick={() => handleEditState()}>edit</button>



                </span>}
                </div>

     
}

export default Destination;