
import React from 'react';
import { Navigate } from 'react-router-dom';
import authService from '../services/authService';

const ProtectedRoute = ({ children, allowedRoles }) => {
  const currentUser = authService.getCurrentUser();

  if (!currentUser) {
    // Not logged in
    return <Navigate to="/login" />;
  }

  if (!allowedRoles.includes(currentUser.role)) {
    // Logged in but role not allowed
    return <div className="text-center mt-4">
      <h3>Access Denied</h3>
      <p>You do not have permission to view this page.</p>
    </div>;
  }

  return children;
};

export default ProtectedRoute;
