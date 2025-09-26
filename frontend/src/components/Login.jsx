import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import authService from '../services/authService';
import '../styles/Register.css'; // Assuming you want the same styling

const Login = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [message, setMessage] = useState('');
    const [loading, setLoading] = useState(false);
    const navigate = useNavigate();

    const handleLogin = async (e) => {
        e.preventDefault();
        setMessage('');
        setLoading(true);

        try {
            await authService.login(username, password);

            // Get logged-in user from localStorage
            const user = authService.getCurrentUser();

            if (user.role === 'Admin') {
                navigate('/dashboard'); // Admin can access CRUD pages
            } else {
                navigate('/products'); // Normal user only sees product list
            }

            window.location.reload(); // Refresh navbar to reflect user state
        } catch (error) {
            const resMessage = error.message || error.toString();
            setMessage(resMessage);
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="login-container">
            <div className="card card-container">
                <img
                    src="//ssl.gstatic.com/accounts/ui/avatar_2x.png"
                    alt="profile-img"
                    className="profile-img-card"
                />
                <form onSubmit={handleLogin}>
                    <div className="form-group">
                        <label htmlFor="username">Username</label>
                        <input
                            type="text"
                            className="form-control"
                            name="username"
                            value={username}
                            onChange={(e) => setUsername(e.target.value)}
                            required
                        />
                    </div>
                    <div className="form-group">
                        <label htmlFor="password">Password</label>
                        <input
                            type="password"
                            className="form-control"
                            name="password"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                            required
                        />
                    </div>
                    <div className="form-group">
                        <button className="btn btn-primary btn-block" disabled={loading}>
                            {loading && <span className="spinner-border spinner-border-sm"></span>}
                            <span>Login</span>
                        </button>
                    </div>
                    {message && (
                        <div className="form-group">
                            <div className="alert alert-danger" role="alert">
                                {message}
                            </div>
                        </div>
                    )}
                </form>
            </div>
        </div>
    );
};

export default Login;
