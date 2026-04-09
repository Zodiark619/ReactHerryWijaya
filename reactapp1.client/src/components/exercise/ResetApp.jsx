import { useSelector, useDispatch } from "react-redux"

import { resetRedux } from "./actions"

function ResetApp() {
    const dispatch = useDispatch()
    const resetCounteranddestination = () => {
        // dispatch(reset())
        dispatch(resetRedux())
    }
    return (
        <button onClick={() => resetCounteranddestination()}> Reset </button >
  );
}

export default ResetApp;