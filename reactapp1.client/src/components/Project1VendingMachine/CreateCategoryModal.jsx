
import { useState, useEffect } from 'react';
import { Button, Modal, Form } from 'react-bootstrap';
import { createCategory, editCategory, deleteCategory } from '../../api/Project1VendingMachine/apicategories';

function CreateCategoryModal({ show, onHide,  onCategoryRefresh,category }) {
    const [name, setName] = useState('');
    useEffect(() => {
        if (category) {
            setName(category.name); 
        } else {
            setName('');  
        }
    }, [category]);
    const [saving, setSaving] = useState(false);

    const handleSave = async () => {
        if (!name.trim()) return;

        setSaving(true);
        try {
            if (category) {
                await editCategory({ name }, category.id);
            } else {
                await createCategory({ name });
            }

            await onCategoryRefresh();
            onHide();
            setName("");
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
                    <Form.Group controlId="categoryName">
                        <Form.Label>Name</Form.Label>
                        <Form.Control
                            type="text"
                            value={name}
                            onChange={(e) => setName(e.target.value)}
                        />
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


export default CreateCategoryModal;