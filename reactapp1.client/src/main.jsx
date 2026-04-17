import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
 import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap/dist/js/bootstrap.bundle.min.js";
import App from './App.jsx'
import { BrowserRouter } from "react-router-dom"
import "bootstrap/dist/css/bootstrap.min.css"
import "bootstrap-icons/font/bootstrap-icons.css"
import "bootstrap/dist/js/bootstrap.bundle.min.js"
import { Provider } from "react-redux"
import store from "./Project2Exercise/store/store"
createRoot(document.getElementById('root')).render(

    <Provider store={store }>
    <BrowserRouter>
    
    
    <App />
    </BrowserRouter>
    </Provider>
)
