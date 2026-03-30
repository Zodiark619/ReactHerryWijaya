import axios from "axios";
import { useEffect, useState } from "react";

import { getCategories } from "../../api/Project1VendingMachine/apicategories";
function CategoriesComponent() {
    const [categories, setCategories] = useState([]);

    useEffect(() => {
        getCategories()
            .then(response => {
                setCategories(response.data);
            })
            .catch(error => {
                console.error("Error fetching categories:", error);
            });
    }, []);

    return (
        <div>
            <h2>Categories</h2>
            <ul>
               

                {categories.length>0 ? (
                    
                        categories.map(cat => (
                            <li key={cat.id}>{cat.name}</li>
                        ))
                    
                ) : (
                    <p>Loading...</p>
                )}
            </ul>
        </div>
    );
}

export default CategoriesComponent;