import { useState } from "react"
 import CategoriesComponent from "./components/Project1VendingMachine/CategoriesComponent"
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Coba from "./components/Project1VendingMachine/CreateCategoryModal";
import ItemsComponent from "./components/Project1VendingMachine/ItemsComponent";
import Bobo from "./components/exercise/Bobo";
import { store } from "./components/exercise/store";
import {Provider } from "react-redux"
function App() {
    return (
        <Provider store={store}>

        <BrowserRouter>
            <Routes>
               
                <Route path="/" element={<CategoriesComponent />} />
                <Route path="/a" element={<ItemsComponent />} />
                <Route path="/bobo" element={<Bobo />} />

            </Routes>
            </BrowserRouter>
        </Provider>

    );


}

export default App