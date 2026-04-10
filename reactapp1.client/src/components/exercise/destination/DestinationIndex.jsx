import AddDestination from "./AddDestination";
import DestinationList from "./DestinationList";
import RandomDestination from "./RandomDestination";

function DestinationIndex() {
    return (
      <div>
      <p>Hello world!</p>
            <AddDestination />
            <hr>
            </hr>
            <DestinationList />
            <br></br>
            <hr>
            </hr>
            <RandomDestination />
      </div>
  );
}

export default DestinationIndex;