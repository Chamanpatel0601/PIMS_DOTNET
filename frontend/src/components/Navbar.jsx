import React from 'react';
import { Link, useNavigate } from 'react-router-dom';
import authService from '../services/authService';

const Navbar = ({ currentUser, onLogout }) => {
    const navigate = useNavigate();

    const handleLogout = () => {
        authService.logout();
        onLogout();
        navigate('/login');
    };

    return (
        <nav className="navbar navbar-expand navbar-dark bg-dark">
            <Link to="/" className="navbar-brand">PIMS</Link>
            <div className="navbar-nav mr-auto">
                <li className="nav-item">
                    <Link to="/home" className="nav-link">Home</Link>
                </li>

                {/* Only Admin links */}
                {currentUser?.role === 'Admin' && (
                    <>
                        <li className="nav-item">
                            <Link to="/products" className="nav-link">Products</Link>
                        </li>
                        <li className="nav-item">
                            <Link to="/categories" className="nav-link">Categories</Link>
                        </li>
                        <li className="nav-item">
                            <Link to="/inventory" className="nav-link">Inventory</Link>
                        </li>
                        <li className="nav-item">
                            <Link to="/transactions" className="nav-link">Transactions</Link>
                        </li>
                    </>
                )}
            </div>

            {currentUser ? (
                <div className="navbar-nav ml-auto">
                    <li className="nav-item nav-link">Welcome, {currentUser.username}</li>
                    <li className="nav-item">
                        <a href="/login" className="nav-link" onClick={handleLogout}>Logout</a>
                    </li>
                </div>
            ) : (
                <div className="navbar-nav ml-auto">
                    <li className="nav-item">
                        <Link to="/login" className="nav-link">Login</Link>
                    </li>
                    <li className="nav-item">
                        <Link to="/register" className="nav-link">Sign Up</Link>
                    </li>
                </div>
            )}
        </nav>
    );
};

export default Navbar;
