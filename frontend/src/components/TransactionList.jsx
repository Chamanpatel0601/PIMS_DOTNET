import React, { useState, useEffect } from 'react';
import '../styles/TransactionList.css'; 

const TransactionList = () => {
    const [transactions, setTransactions] = useState([]);
    const [products, setProducts] = useState([]);
    const [users, setUsers] = useState([]);

    // Fetch transactions
    const fetchTransactions = async () => {
        const res = await fetch('https://localhost:7175/api/InventoryTransaction');
        const data = await res.json();
        setTransactions(data);
    };

    // Fetch products
    const fetchProducts = async () => {
        const res = await fetch('https://localhost:7175/api/Product');
        const data = await res.json();
        setProducts(data);
    };

    // Fetch users
    const fetchUsers = async () => {
        const res = await fetch('https://localhost:7175/api/User');
        const data = await res.json();
        setUsers(data);
    };

    useEffect(() => {
        fetchProducts();
        fetchUsers();
        fetchTransactions();
    }, []);

    const getProductName = (productId) => {
        const product = products.find(p => p.productId === productId);
        return product ? product.name : productId;
    };

    const getUserName = (userId) => {
        const user = users.find(u => u.userId === userId);
        return user ? user.name : userId;
    };

    return (
        <div className="transaction-container">
            <h3>Inventory Transactions</h3>
            <table className="transaction-table">
                <thead>
                    <tr>
                        <th>Transaction ID</th>
                        <th>Product Name</th>
                        <th>Quantity Change</th>
                        <th>Reason</th>
                        <th>User Name</th>
                        <th>Date</th>
                    </tr>
                </thead>
                <tbody>
                    {transactions.map(tx => (
                        <tr key={tx.transactionId}>
                            <td>{tx.transactionId}</td>
                            <td>{getProductName(tx.productId)}</td>
                            <td>{tx.quantityChange}</td>
                            <td>{tx.reason}</td>
                            <td>{getUserName(tx.userId)}</td>
                            <td>{new Date(tx.transactionDate).toLocaleString()}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default TransactionList;
