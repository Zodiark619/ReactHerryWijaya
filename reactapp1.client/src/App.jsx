import { useState } from "react"
 import CategoriesComponent from "./components/Project1VendingMachine/CategoriesComponent"
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Coba from "./components/Project1VendingMachine/CreateCategoryModal";
import ItemsComponent from "./components/Project1VendingMachine/ItemsComponent";
import Bobo from "./components/exercise/Bobo";
function App() {
    return (
        <BrowserRouter>
            <Routes>
               
                <Route path="/" element={<CategoriesComponent />} />
                <Route path="/a" element={<ItemsComponent />} />
                <Route path="/bobo" element={<Bobo />} />

            </Routes>
        </BrowserRouter>
    );


}

export default App