import React from 'react';
import authService from '../services/authService';

const Dashboard = () => {
  const currentUser = authService.getCurrentUser();

  if (!currentUser) {
    return (
      <div className="container">
        <header className="jumbotron">
          <h3>Access Denied!</h3>
          <p>Please log in to view this page.</p>
        </header>
      </div>
    );
  }

  return (
    <div className="container">
      <header className="jumbotron">
        <h3>
          Welcome, <strong>{currentUser.username}</strong>!
        </h3>
      </header>
      <p>
        <strong>User ID:</strong> {currentUser.userId}
      </p>
      <p>
        <strong>Role:</strong> {currentUser.role}
      </p>
      <p>
        <strong>JWT Token:</strong> {currentUser.token.substring(0, 30)}...
      </p>
    </div>
  );
};

export default Dashboard;
