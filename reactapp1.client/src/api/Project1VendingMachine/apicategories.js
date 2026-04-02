import axiosInstance from "./api";
export const getCategories =async () => {
    //return axiosInstance.get("/Category/GetAll") ;
   const res =await  axiosInstance.get("/Category/GetAll");
    return res.data;
};
export const getCategoryById =async (id) => {
    return await axiosInstance.get(`/Category/Get/${id}`);
};
export const createCategory = async (category) => {
    return await axiosInstance.post(`/Category/Create`,category);
};
export const editCategory = async (category,id) => {
    return await axiosInstance.put(`/Category/Edit/${id}`,category);
};
export const deleteCategory = async (id) => {
    return await axiosInstance.delete(`/Category/Delete/${id}`);
};
export const getItems = async () => {
    //return axiosInstance.get("/Category/GetAll") ;
    const res = await axiosInstance.get("/Item/GetAll");
    return res.data;
};
export const getItemById = async (id) => {
    return await axiosInstance.get(`/Item/Get/${id}`);
};
export const createItem = async (category) => {
    return await axiosInstance.post(`/Item/Create`, category);
};
export const editItem = async (category, id) => {
    return await axiosInstance.put(`/Item/Edit/${id}`, category);
};
export const deleteItem = async (id) => {
    return await axiosInstance.delete(`/Item/Delete/${id}`);
};