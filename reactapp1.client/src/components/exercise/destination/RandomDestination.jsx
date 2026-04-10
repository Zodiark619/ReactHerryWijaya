import {useGetRandomDestinationQuery} from "./DestinationApi - Copy"



function RandomDestination() {
    const { data, isLoading, isSuccess, isError, error } = useGetRandomDestinationQuery()
    let content;
    if (isLoading) {
        content = <p>is loading</p>
    }
    else if (isSuccess) {
        content = (<div>
            {data.city} - {data.country }
        </div>)
    }
    return (
        <div>
   {content}
        </div>
  );
}

export default RandomDestination;