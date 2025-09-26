import React, { useState, useEffect } from 'react';
import authService from '../services/authService';
import '../styles/InventoryList.css'; 

const InventoryList = () => {
    const [inventories, setInventories] = useState([]);
    const [products, setProducts] = useState([]);
    const [loading, setLoading] = useState(true);
    const [newInventory, setNewInventory] = useState({
        productId: '',
        quantity: 0,
        warehouseLocation: '',
        lowStockThreshold: 10
    });

    // Fetch inventories
    const fetchInventories = async () => {
        const res = await fetch('https://localhost:7175/api/Inventory');
        const data = await res.json();
        setInventories(data);
        setLoading(false);
    };

    // Fetch products
    const fetchProducts = async () => {
        const res = await fetch('https://localhost:7175/api/Product');
        const data = await res.json();
        setProducts(data);
    };

    useEffect(() => {
        fetchProducts();
        fetchInventories();
    }, []);

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setNewInventory(prev => ({ ...prev, [name]: value }));
    };

    const handleCreate = async () => {
        if (!newInventory.productId) return alert('Product is required');

        await fetch('https://localhost:7175/api/Inventory/create', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(newInventory)
        });

        setNewInventory({ productId: '', quantity: 0, warehouseLocation: '', lowStockThreshold: 10 });
        fetchInventories();
    };

    const handleUpdate = async (inventoryId) => {
        const inventory = inventories.find(inv => inv.inventoryId === inventoryId);
        const updatedQuantity = parseInt(prompt('Enter new quantity:', inventory.quantity));
        if (isNaN(updatedQuantity)) return;

        const userId = authService.getCurrentUser()?.user?.userId;
        await fetch(`https://localhost:7175/api/Inventory/audit?productId=${inventory.productId}&newQuantity=${updatedQuantity}&reason=Update&userId=${userId}`, {
            method: 'POST'
        });
        fetchInventories();
    };

    const handleDelete = async (inventoryId) => {
        if (!window.confirm('Are you sure to delete this inventory?')) return;
        await fetch(`https://localhost:7175/api/Inventory/${inventoryId}`, { method: 'DELETE' });
        fetchInventories();
    };

    const handleAdjust = async (productId) => {
        const quantityChange = parseInt(prompt('Enter quantity change:'));
        const reason = prompt('Enter reason:');
        const user = authService.getCurrentUser();
        if (!quantityChange || !reason) return;

        await fetch('https://localhost:7175/api/Inventory/adjust', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                ProductId: productId,
                QuantityChange: quantityChange,
                Reason: reason,
                UserId: user?.user?.userId
            })
        });

        fetchInventories();
    };

    // Helper to get product name by ID
    const getProductName = (id) => {
        const product = products.find(p => p.productId === id);
        return product ? product.name : id;
    };

    if (loading) return <p>Loading...</p>;

    return (
        <div className="inventory-container">
            <h3>Inventory Management</h3>

            {/* Create Inventory */}
            <div className="inventory-form">
                <select name="productId" value={newInventory.productId} onChange={handleInputChange}>
                    <option value="">Select Product</option>
                    {products.map(p => (
                        <option key={p.productId} value={p.productId}>{p.name}</option>
                    ))}
                </select>
                <input type="number" name="quantity" placeholder="Quantity" value={newInventory.quantity} onChange={handleInputChange} />
                <input type="text" name="warehouseLocation" placeholder="Warehouse Location" value={newInventory.warehouseLocation} onChange={handleInputChange} />
                <input type="number" name="lowStockThreshold" placeholder="Low Stock Threshold" value={newInventory.lowStockThreshold} onChange={handleInputChange} />
                <button onClick={handleCreate}>Create Inventory</button>
            </div>

            {/* Inventory Table */}
            <table className="inventory-table">
                <thead>
                    <tr>
                        <th>Product Name</th>
                        <th>Quantity</th>
                        <th>Warehouse</th>
                        <th>Low Stock</th>
                        <th>Last Updated</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {inventories.map(inv => (
                        <tr key={inv.inventoryId}>
                            <td>{getProductName(inv.productId)}</td>
                            <td>{inv.quantity}</td>
                            <td>{inv.warehouseLocation}</td>
                            <td>{inv.lowStockThreshold}</td>
                            <td>{new Date(inv.lastUpdated).toLocaleString()}</td>
                            <td>
                                <button onClick={() => handleAdjust(inv.productId)}>Adjust</button>
                                <button onClick={() => handleUpdate(inv.inventoryId)}>Update</button>
                                <button onClick={() => handleDelete(inv.inventoryId)}>Delete</button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default InventoryList;
