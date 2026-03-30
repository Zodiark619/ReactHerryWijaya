import axiosInstance from "./api";

export const getCategories = () => {
    return axiosInstance.get("/Category/GetAll");
};
export const getCategoryById = (id) => {
    return axiosInstance.get(`/Category/GetById/${id}`);
};