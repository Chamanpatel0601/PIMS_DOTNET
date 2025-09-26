import React, { useState, useEffect } from 'react';
import { Routes, Route, Navigate } from 'react-router-dom';

import Navbar from './components/Navbar';
import Home from './components/Home';
import Login from './components/Login';
import Register from './components/Register';
import Dashboard from './components/Dashboard';
import ProductList from './components/ProductList';
import CategoryList from './components/CategoryList';
import InventoryList from './components/InventoryList';
import TransactionList from './components/TransactionList';
import ProtectedRoute from './components/ProtectedRoute';

import authService from './services/authService';

const App = () => {
    const [currentUser, setCurrentUser] = useState(undefined);

    useEffect(() => {
        const user = authService.getCurrentUser();
        if (user) setCurrentUser(user);
    }, []);

    const handleLogout = () => {
        authService.logout();
        setCurrentUser(undefined);
    };

    return (
        <div>
            <Navbar currentUser={currentUser} onLogout={handleLogout} />
            <div className="container mt-3">
                <Routes>
                    <Route path="/" element={<Home />} />
                    <Route path="/home" element={<Home />} />
                    <Route path="/login" element={<Login />} />
                    <Route path="/register" element={<Register />} />
                    <Route path="/dashboard" element={<Dashboard />} />

                    {/* Product list is accessible to both Admin and User */}
                    <Route 
                        path="/products" 
                        element={
                            <ProtectedRoute allowedRoles={['Admin', 'User']}>
                                <ProductList readOnly={currentUser?.role === 'User'} />
                            </ProtectedRoute>
                        } 
                    />

                    {/* Only Admin can access these CRUD pages */}
                    <Route 
                        path="/categories" 
                        element={
                            <ProtectedRoute allowedRoles={['Admin']}>
                                <CategoryList />
                            </ProtectedRoute>
                        } 
                    />
                    <Route 
                        path="/inventory" 
                        element={
                            <ProtectedRoute allowedRoles={['Admin']}>
                                <InventoryList />
                            </ProtectedRoute>
                        } 
                    />
                    <Route 
                        path="/transactions" 
                        element={
                            <ProtectedRoute allowedRoles={['Admin']}>
                                <TransactionList />
                            </ProtectedRoute>
                        } 
                    />

                    {/* Redirect unknown routes */}
                    <Route path="*" element={<Navigate to="/" />} />
                </Routes>
            </div>
        </div>
    );
};

export default App;
