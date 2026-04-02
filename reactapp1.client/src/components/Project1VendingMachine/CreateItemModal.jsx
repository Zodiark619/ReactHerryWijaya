import { useState, useEffect } from 'react';
import { Button, Modal, Form } from 'react-bootstrap';
import { createItem, editItem } from '../../api/Project1VendingMachine/apicategories';

function CreateItemModal({ show, onHide, onItemRefresh, item, categories }) {
    const [formData, setFormData] = useState({
        name: '',
        price: 0,
        categoryId: 0
    });

    const handleChange = (e) => {
        const { name, value } = e.target;

        setFormData(prev => ({
            ...prev,
            [name]: value
        }));
    };


    useEffect(() => {
        if (item) {
            setFormData({
                name: item.name,
                price: item.price,
                categoryId: item.categoryId
            });
        } else {
            setFormData({
                name: '',
                price: 0,
                categoryId: 0
            });
        }
    }, [item]);
    const [saving, setSaving] = useState(false);

    const handleSave = async () => {
        if (!formData.name.trim()
            || formData.price <= 0
            || formData.categoryId <= 0
        ) return;

        setSaving(true);
        try {
            if (item) {
                await editItem({
                    name: formData.name,
                    price: formData.price,
                    categoryId: formData.categoryId
                }, item.id);
            } else {
                await createItem({
                    name: formData.name,
                    price: formData.price,
                    categoryId: formData.categoryId
                });
            }

            await onItemRefresh();
            onHide();
            setFormData({
                name: '',
                price: 0,
                categoryId: 0
            });
        } finally {
            setSaving(false);
        }
    };
    return (
        <Modal show={show} onHide={onHide} centered>
            <Modal.Header closeButton>
                <Modal.Title>Category</Modal.Title>
            </Modal.Header>

            <Modal.Body>
                <Form>
                    <Form.Group controlId="itemName">
                        <Form.Label>Name</Form.Label>
                        <Form.Control
                            type='text'
                            name="name"
                            value={formData.name}
                            onChange={handleChange}
                        />
                    </Form.Group>
                    <Form.Group controlId="itemPrice">
                        <Form.Label>Price</Form.Label>
                        <Form.Control
                            type='number'
                            name="price"
                            value={formData.price}
                            onChange={handleChange}
                        />
                    </Form.Group>
                    <Form.Group controlId="itemCategoryName">
                        <Form.Label>Category Name</Form.Label>
                        <Form.Select
                            name="categoryId"
                            value={formData.categoryId}
                            onChange={handleChange}
                        >
                            <option value="">Select category</option>
                            {categories.map(c => (
                                <option key={c.id} value={c.id}>
                                    {c.name}
                                </option>
                            ))}
                        </Form.Select>
                    </Form.Group>
                </Form>
            </Modal.Body>

            <Modal.Footer>
                <Button onClick={onHide}>Close</Button>
                <Button onClick={handleSave} disabled={saving}>
                    {saving ? "Saving..." : "Save"}
                </Button>
            </Modal.Footer>
        </Modal>
    );
}

export default CreateItemModal;