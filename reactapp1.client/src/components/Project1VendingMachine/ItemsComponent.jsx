import axios from "axios";
import { useEffect, useState } from "react";
import { Button, Modal, Form } from 'react-bootstrap';
import Table from 'react-bootstrap/Table';
import { getCategories, getItems } from "../../api/Project1VendingMachine/apicategories";
import CreateItemModal from "./CreateItemModal";
import { deleteItem } from '../../api/Project1VendingMachine/apicategories';

function ItemsComponent() {
    const [categories, setCategories] = useState([]);
    const [modalShow, setModalShow] = useState(false);
    const [loading, setLoading] = useState(true);

    const [selectedItem, setSelectedItem] = useState(null);
    const [items, setItems] = useState([]);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const data = await getItems();

                setItems(data);
            } catch (err) {
                console.error(err);
            } finally {
                setLoading(false); // ? always stop loading
            }
        };

        fetchData();
    }, []);
    useEffect(() => {
        const fetchCategories = async () => {
            const data = await getCategories();
            setCategories(data);
        };

        fetchCategories();
    }, []);
    const handleRefresh = async () => {
        const data = await getItems();
        setItems(data);
    };
    const handleEdit = (cat) => {
        setSelectedItem(cat);
        setModalShow(true);
    };
    const handleDelete = async (catId) => {
        try {
            await deleteItem(catId);
            await handleRefresh(); // ?? refresh list
        } catch (err) {
            console.error(err);
            alert("Delete failed");
        }
    }
    if (loading) {
        return <p>Loading...</p>;
    }

    if (!items.length) {
        return <p>No items</p>;
    }
  return (
      <div className="container">
          <h2>Items</h2>
          <Button onClick={() => setModalShow(true)}>
              Add Item
          </Button>
          <CreateItemModal
              show={modalShow}
              onHide={() => { setModalShow(false); setSelectedItem(null) }}
              item={selectedItem}
              onItemRefresh={handleRefresh}
              categories={categories }
          />
          <Table striped bordered hover variant="dark" responsive>
              <thead>
                  <tr>
                      <th>Name</th>
                      <th>Price</th>
                      <th>Category</th>
                      <th>Actions</th>
                  </tr>
              </thead>
              <tbody>
                  {items.map(cat => (
                      <tr key={cat.id}>
                          <td className="w-25">{cat.name}</td>
                          <td className="w-25">{cat.price}</td>
                          <td className="w-25">{cat.categoryName}</td>
                          <td className="w-25" >
                              <Button variant="warning" onClick={() => handleEdit(cat)}>Edit</Button>
                              <Button variant="danger" onClick={() => handleDelete(cat.id)}>Delete</Button>
                          </td>
                      </tr>
                  ))}
              </tbody>
          </Table>
      </div>
  );
}

export default ItemsComponent;