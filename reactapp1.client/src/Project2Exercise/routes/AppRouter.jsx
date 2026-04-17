



import { Route, Routes } from "react-router-dom"
import { ROUTES } from "../utility/constants"
import Home from "../pages/Home"
import MenuItemManagement from "../pages/menu/MenuItemManagement"
import Cart from "../pages/cart/Cart"
import Checkout from "../pages/cart/Checkout"
import Login from "../pages/auth/Login"
import Register from "../pages/auth/Register"
import OrderManagement from "../pages/order/OrderManagement"
import OrderConfirmation from "../pages/order/OrderConfirmation"
const AppRoutes = () => {
    return (
        <Routes>
            <Route path={ROUTES.HOME} element={<Home />} />
            <Route path={ROUTES.MENU_MANAGEMENT} element={<MenuItemManagement />} />
            <Route path={ROUTES.CART} element={<Cart />} />
            <Route path={ROUTES.CHECKOUT} element={<Checkout />} />
            <Route path={ROUTES.LOGIN} element={<Login />} />
            <Route path={ROUTES.REGISTER} element={<Register />} />
            <Route path={ROUTES.ORDER_MANAGEMENT} element={<OrderManagement />} />
            <Route path={ROUTES.ORDER_CONFIRMATION} element={<OrderConfirmation />} />
    </Routes>)
}

export default AppRoutes;