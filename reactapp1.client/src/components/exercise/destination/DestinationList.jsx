
import Destination from "./Destination";
import { useGetAllDestinationQuery, useDeleteDestinationMutation } from "./DestinationApi"
import { useGetRandomDestinationQuery } from "./DestinationApi - Copy"
import { useAddDestinationMutation } from "./DestinationApi"

function DestinationList() {
    const { data, isLoading, isSuccess, isError, error } = useGetAllDestinationQuery()
    const { data: data1,
        isLoading: isLoading1,
        isSuccess: isSuccess1,
        isError: isError1,
        error: error1 } = useGetRandomDestinationQuery()
    const [addDestinationMutation, resultobj] = useAddDestinationMutation()

    
    const addRandom = () => {


         
        addDestinationMutation({
            id: parseInt(Math.random() * 12120) + 1,
            city: data1.city,
            country: data1.country,
            daysNeeded: parseInt(Math.random() * 10) + 1
        })
        
    }
     let content;
    if (isLoading) {
        content = <p>Loading</p>
    } else if (isError) {
        content = <p>{error}</p>
    } else if (isSuccess) {
        content = data.map((destination, index) => {


            return  <Destination destination={destination} key={destination.id } />
            

        })
    }
    return (
        <div>
            <button onClick={ ()=>addRandom()}>add random</button>


            {content}</div>
    ); 
}

export default DestinationList;