import { useState } from "react";
import Counter from "./Counter";
import DestinationList from "./DestinationList"
import { useSelector, useDispatch } from "react-redux"
import DestinationFact from "./DestinationFact";
import ResetApp from "./ResetApp";

function Bobo() {
    const destination = useSelector((state) => state.destinationStore.destinationSelected)

    return (<div>
        <Counter/>
        <hr>
        </hr>
        <br>
        </br>
      selected destination=  {destination &&    destination.name}
        <br></br>
        <DestinationList />
        <hr>
        </hr>
        <DestinationFact />
        <hr>
        </hr>
        <ResetApp/>
    </div>)


}

export default Bobo;