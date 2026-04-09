import { useSelector, useDispatch } from "react-redux"
import { decrement, increment, decrementMultiplier, incrementMultiplier } from "./counterSlice.js"
import { useState } from "react"
function Counter() {
    const [multiplier, setMultiplier] = useState(10)
    const count = useSelector((state) => state.counterStore.count)
    const dispatch = useDispatch()
    return (
      
      <div>
            <h1>counter= {count}</h1>
            <button onClick={() => dispatch(increment())}>Increment</button>
            <button onClick={() => dispatch(decrement())}>Decrement</button> 
            <input value={multiplier} onChange={(e) => setMultiplier(e.target.value)} />
            <button onClick={() => dispatch(incrementMultiplier(multiplier))}>Increment</button>
            <button onClick={() => dispatch(decrementMultiplier(multiplier))}>Decrement</button>
       </div >      
        
  );
}

export default Counter;