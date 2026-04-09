import { useSelector, useDispatch } from "react-redux"
import { destinationClicked } from "./destinationSlice"

function DestinationFact() {
    const selected = useSelector((state) => state.destinationStore.destinationSelected)
    if (selected == undefined) {
        return (
            <p>seecte destination</p>
        );

    } else {
        return (
            <div>
            <p>{selected.name}</p>
            <p>{selected.days}</p>
                <p>{selected.fact}</p></div>
        );
    }
 
}

export default DestinationFact;