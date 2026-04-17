import { useState } from "react"
 import CategoriesComponent from "./components/Project1VendingMachine/CategoriesComponent"
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Coba from "./components/Project1VendingMachine/CreateCategoryModal";
import ItemsComponent from "./components/Project1VendingMachine/ItemsComponent";
import Bobo from "./components/exercise/Bobo";
//import { store } from "./components/exercise/store";
import { store } from "./components/exercise/destination/store";
import {Provider } from "react-redux"
import { destinationAPI } from "./components/exercise/destination/DestinationApi";
import { ApiProvider } from "@reduxjs/toolkit/query/react"
import AppRoutes from "./Project2Exercise/routes/AppRouter"
import Header from "./Project2Exercise/components/layout/Header";
import Footer from "./Project2Exercise/components/layout/Footer";
import { ToastContainer}  from "react-toastify"
function App() {
    return (
        //<Provider store={store}>
        // <ApiProvider api={destinationAPI }>

       // </ApiProvider>
        //</Provider>





        //<Provider store={store}>
        //    <BrowserRouter>
        //        <Routes>

        //            <Route path="/" element={<CategoriesComponent />} />
        //            <Route path="/a" element={<ItemsComponent />} />
        //            <Route path="/bobo" element={<Bobo />} />

        //        </Routes>
        //    </BrowserRouter>
        //</Provider>




        <div className="d-flex flex-column min-vh-100 bg-body">

            <Header/>

            <main className="flex-grow-1">
            <AppRoutes />
            </main>

            <Footer />
            <ToastContainer
                position="top-right"
                autoClose={5000}
                hideProgressBar={false}
                newestOnTop={false}
                closeOnClick={false}
                rtl={false}
                pauseOnFocusLoss
                draggable
                pauseOnHover
                theme="light" 
            />
        </div>
    );


}

export default App