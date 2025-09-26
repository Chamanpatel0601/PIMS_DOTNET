// // Backend API ka base URL
// const API_URL = 'https://localhost:7175/api/User/';

// // ---------------- REGISTER ----------------
// const register = async (username, email, password, roleId) => {
//   const response = await fetch(API_URL + 'register', {
//     method: 'POST',
//     headers: {
//       'Content-Type': 'application/json',
//     },
//     body: JSON.stringify({ username, email, password, roleId }),
//   });

//   if (!response.ok) {
//     const errorData = await response.json();
//     throw new Error(errorData.message || 'Registration failed');
//   }

//   return response.json();
// };

// // ---------------- LOGIN ----------------
// const login = async (username, password) => {
//   const response = await fetch(API_URL + 'login', {
//     method: 'POST',
//     headers: {
//       'Content-Type': 'application/json',
//     },
//     body: JSON.stringify({ username, password }),
//   });

//   if (!response.ok) {
//     const errorData = await response.json();
//     throw new Error(errorData.message || 'Login failed');
//   }

//   const data = await response.json();

//   if (data.token) {
//     // token + user info localStorage me save karo
//     const userData = {
//       token: data.token,
//       userId: data.user.userId,
//       username: data.user.username,
//       role: data.user.role,
//     };

//     localStorage.setItem('user', JSON.stringify(userData));
//   }

//   return data;
// };

// // ---------------- LOGOUT ----------------
// const logout = () => {
//   localStorage.removeItem('user');
// };

// // ---------------- GET CURRENT USER ----------------
// const getCurrentUser = () => {
//   return JSON.parse(localStorage.getItem('user'));
// };

// // ---------------- EXPORT ----------------
// const authService = {
//   register,
//   login,
//   logout,
//   getCurrentUser,
// };

// export default authService;



// Backend API base URL


const API_URL = 'https://localhost:7175/api/User/';

// ---------------- REGISTER ----------------
const register = async (username, email, password, roleId) => {
  const response = await fetch(API_URL + 'register', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ username, email, password, roleId }),
  });

  if (!response.ok) {
    const errorData = await response.json();
    throw new Error(errorData.message || 'Registration failed');
  }

  return response.json();
};

// ---------------- LOGIN ----------------
const login = async (username, password) => {
  const response = await fetch(API_URL + 'login', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ username, password }),
  });

  if (!response.ok) {
    const errorData = await response.json();
    throw new Error(errorData.message || 'Login failed');
  }

  const data = await response.json();

  if (data.token) {
    // Save token and user info to localStorage
    const userData = {
      token: data.token,
      userId: data.user.userId,
      username: data.user.username,
      role: data.user.role, // "Admin" or "User"
    };
    localStorage.setItem('user', JSON.stringify(userData));
  }

  return data;
};

// ---------------- LOGOUT ----------------
const logout = () => {
  localStorage.removeItem('user');
};

// ---------------- GET CURRENT USER ----------------
const getCurrentUser = () => {
  return JSON.parse(localStorage.getItem('user'));
};

// ---------------- CHECK ROLE ----------------
const isAdmin = () => {
  const user = getCurrentUser();
  return user?.role === 'Admin';
};

const isUser = () => {
  const user = getCurrentUser();
  return user?.role === 'User';
};

// ---------------- EXPORT ----------------
const authService = {
  register,
  login,
  logout,
  getCurrentUser,
  isAdmin,
  isUser,
};

export default authService;
