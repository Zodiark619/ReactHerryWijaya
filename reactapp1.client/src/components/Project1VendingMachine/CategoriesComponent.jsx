import axios from "axios";
import { useEffect, useState } from "react";
import { Button, Modal, Form } from 'react-bootstrap';
import Table from 'react-bootstrap/Table';
import { getCategories } from "../../api/Project1VendingMachine/apicategories";
import CreateCategoryModal from "./CreateCategoryModal";
import {   deleteCategory } from '../../api/Project1VendingMachine/apicategories';

function CategoriesComponent() {
    const [categories, setCategories] = useState([]);
    const [modalShow, setModalShow] = useState(false);
    const [loading, setLoading] = useState(true);

    const [selectedCategory, setSelectedCategory] = useState(null);
     useEffect(() => {
         const fetchData = async () => {
             try {
                 const data = await getCategories();
                 
                 setCategories(data );
             } catch (err) {
                 console.error(err);
             } finally {
                 setLoading(false); // ? always stop loading
             }
         };

         fetchData();
    }, []);
    const handleRefresh = async () => {
        const data = await getCategories();
        setCategories(data  );
    };
    const handleEdit = (cat) => {
        setSelectedCategory(cat);
        setModalShow(true);
    };
    const handleDelete =async (catId) => {
        try {
            await deleteCategory(catId);
            await handleRefresh(); // ?? refresh list
        } catch (err) {
            console.error(err);
            alert("Delete failed");
        }
    }
    if (loading) {
        return <p>Loading...</p>;
    }

    if (!categories.length) {
        return <p>No categories</p>;
    }
    return (
        <div className="container">
            <h2>Categories</h2>
            <Button onClick={() => setModalShow(true)}>
                Add Category
            </Button>
            <CreateCategoryModal
                show={modalShow}
                onHide={() => { setModalShow(false); setSelectedCategory(null) }}
                category={selectedCategory} 
                onCategoryRefresh={handleRefresh}
            />
            <Table striped bordered hover variant="dark" responsive>
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>                  
                    {categories.map(cat => (
                        <tr key={cat.id}>
                            <td className="w-75">{cat.name}</td>
                            <td className="w-25" >
                                <Button variant="warning" onClick={() => handleEdit(cat)}>Edit</Button>
                                <Button variant="danger" onClick={()=>handleDelete(cat.id) }>Delete</Button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </Table>            
        </div>
    );
} 

export default CategoriesComponent;