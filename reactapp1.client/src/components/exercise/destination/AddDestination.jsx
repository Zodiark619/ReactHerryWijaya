import { useState } from "react";
import { useAddDestinationMutation, useGetAllDestinationQuery } from "./DestinationApi"
function AddDestination() {
    const [newCity, setNewCity] = useState("")
    const [newCountry, setNewCountry] = useState("")
    const [addDestinationMutation, resultobj] = useAddDestinationMutation()
    const { data: destinations } = useGetAllDestinationQuery()
    const getNextId = () => {
        if (!destinations || destinations.length===0) {
            return 1;
        }
        const maxId = Math.max(...destinations.map((dest) => dest.id))
        return maxId + 1;
    }
    const handleAddDestination = (formData) => {
        const city = formData.get("city")
        const country = formData.get("country")

        addDestinationMutation({
            id: getNextId(),
            city: newCity,
            country: newCountry,
            daysNeeded:parseInt( Math.random() * 10)+1
        })
        setNewCity("")
        setNewCountry("")
    }
  return (
      <div>


          <form action={handleAddDestination }>
              <input placeholder="enter citiy..." name="city" value={newCity} onChange={(e) => setNewCity(e.target.value)} />
              <input placeholder="enter counter..." name="country" value={newCountry} onChange={(e) => setNewCountry(e.target.value)} />
              <button>Add</button>

          </form>

      </div> 
  );
}

export default AddDestination;