import axiosInstance from "./api";

export const getCategories =async () => {
    //return axiosInstance.get("/Category/GetAll") ;
   const res =await  axiosInstance.get("/Category/GetAll");
    return res.data;
};
export const getCategoryById =async (id) => {
    return await axiosInstance.get(`/Category/GetById/${id}`);
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