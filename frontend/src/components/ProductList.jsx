// import React, { useEffect, useState } from "react";
// import "../styles/ProductList.css"; // Import your custom CSS

// const API_URL = "https://localhost:7175/api/Product";
// const CATEGORY_URL = "https://localhost:7175/api/Category";

// const ProductList = () => {
//   const [products, setProducts] = useState([]);
//   const [categories, setCategories] = useState([]);
//   const [editingProduct, setEditingProduct] = useState(null);
//   const [formData, setFormData] = useState({
//     sku: "",
//     name: "",
//     description: "",
//     price: "",
//     categoryIds: [],
//   });
//   const [showForm, setShowForm] = useState(false);

//   // Fetch products
//   const fetchProducts = () => {
//     fetch(API_URL)
//       .then((res) => res.json())
//       .then((data) => setProducts(data))
//       .catch((err) => console.error("Error fetching products:", err));
//   };

//   // Fetch categories
//   const fetchCategories = () => {
//     fetch(CATEGORY_URL)
//       .then((res) => res.json())
//       .then((data) => setCategories(data))
//       .catch((err) => console.error("Error fetching categories:", err));
//   };

//   useEffect(() => {
//     fetchProducts();
//     fetchCategories();
//   }, []);

//   // Handle input change
//   const handleChange = (e) => {
//     setFormData({ ...formData, [e.target.name]: e.target.value });
//   };

//   // Handle multiple select for categories
//   const handleCategorySelect = (e) => {
//     setFormData({
//       ...formData,
//       categoryIds: Array.from(e.target.selectedOptions, (opt) => parseInt(opt.value)),
//     });
//   };

//   // Add or Update product
//   const handleSubmit = async (e) => {
//     e.preventDefault();

//     const method = editingProduct ? "PUT" : "POST";
//     const url = editingProduct ? `${API_URL}/${editingProduct.productId}` : API_URL;

//     try {
//       const res = await fetch(url, {
//         method,
//         headers: { "Content-Type": "application/json" },
//         body: JSON.stringify(
//           editingProduct ? { ...formData, productId: editingProduct.productId } : formData
//         ),
//       });

//       if (!res.ok) throw new Error("Failed to save product");

//       const savedProduct = await res.json();

//       if (editingProduct) {
//         setProducts(products.map((p) => (p.productId === savedProduct.productId ? savedProduct : p)));
//       } else {
//         setProducts([savedProduct, ...products]);
//       }

//       setFormData({ sku: "", name: "", description: "", price: "", categoryIds: [] });
//       setEditingProduct(null);
//       setShowForm(false);
//     } catch (err) {
//       console.error(err);
//     }
//   };

//   // Delete product
//   const handleDelete = async (id) => {
//     if (!window.confirm("Are you sure you want to delete this product?")) return;

//     try {
//       const res = await fetch(`${API_URL}/${id}`, { method: "DELETE" });
//       if (!res.ok) throw new Error("Delete failed");
//       setProducts(products.filter((p) => p.productId !== id));
//     } catch (err) {
//       console.error(err);
//     }
//   };

//   // Edit product
//   const handleEdit = (prod) => {
//     setEditingProduct(prod);
//     setFormData({
//       sku: prod.sku,
//       name: prod.name,
//       description: prod.description || "",
//       price: prod.price,
//       categoryIds: prod.categories?.map((c) => c.categoryId) || [],
//     });
//     setShowForm(true);
//   };

//   return (
//     <div className="product-container">
//       <h2>Manage Products</h2>

//       {/* Add Product Button */}
//       <button
//         className="btn-add"
//         onClick={() => {
//           setEditingProduct(null);
//           setFormData({ sku: "", name: "", description: "", price: "", categoryIds: [] });
//           setShowForm(!showForm);
//         }}
//       >
//         {showForm ? "Hide Form" : "Add Product"}
//       </button>

//       {/* Form */}
//       {showForm && (
//         <form onSubmit={handleSubmit} className="product-form">
//           <div className="form-group">
//             <label>SKU</label>
//             <input name="sku" value={formData.sku} onChange={handleChange} required />
//           </div>
//           <div className="form-group">
//             <label>Name</label>
//             <input name="name" value={formData.name} onChange={handleChange} required />
//           </div>
//           <div className="form-group">
//             <label>Description</label>
//             <textarea name="description" value={formData.description} onChange={handleChange} />
//           </div>
//           <div className="form-group">
//             <label>Price</label>
//             <input
//               type="number"
//               name="price"
//               value={formData.price}
//               onChange={handleChange}
//               required
//             />
//           </div>
//           <div className="form-group">
//             <label>Categories</label>
//             <select multiple value={formData.categoryIds} onChange={handleCategorySelect}>
//               {categories.map((c) => (
//                 <option key={c.categoryId} value={c.categoryId}>
//                   {c.categoryName}
//                 </option>
//               ))}
//             </select>
//           </div>
//           <div className="form-buttons">
//             <button type="submit" className="btn-submit">
//               {editingProduct ? "Update Product" : "Add Product"}
//             </button>
//             {editingProduct && (
//               <button
//                 type="button"
//                 className="btn-cancel"
//                 onClick={() => {
//                   setEditingProduct(null);
//                   setFormData({ sku: "", name: "", description: "", price: "", categoryIds: [] });
//                   setShowForm(false);
//                 }}
//               >
//                 Cancel
//               </button>
//             )}
//           </div>
//         </form>
//       )}

//       {/* Product Table */}
//       <table className="product-table">
//         <thead>
//           <tr>
//             <th>Product ID</th>
//             <th>SKU</th>
//             <th>Name</th>
//             <th>Price</th>
//             <th>Categories</th>
//             <th>Actions</th>
//           </tr>
//         </thead>
//         <tbody>
//           {products.map((p) => (
//             <tr key={p.productId}>
//               <td>{p.productId}</td>
//               <td>{p.sku}</td>
//               <td>{p.name}</td>
//               <td>{p.price}</td>
//               <td>{p.categories?.map((c) => c.categoryName).join(", ")}</td>
//               <td>
//                 <button className="btn-edit" onClick={() => handleEdit(p)}>
//                   Edit
//                 </button>
//                 <button className="btn-delete" onClick={() => handleDelete(p.productId)}>
//                   Delete
//                 </button>
//               </td>
//             </tr>
//           ))}
//         </tbody>
//       </table>
//     </div>
//   );
// };

// export default ProductList;



import React, { useEffect, useState } from "react";
import "../styles/ProductList.css"; // Import your custom CSS
import authService from "../services/authService";

const API_URL = "https://localhost:7175/api/Product";
const CATEGORY_URL = "https://localhost:7175/api/Category";

const ProductList = () => {
  const [products, setProducts] = useState([]);
  const [categories, setCategories] = useState([]);
  const [editingProduct, setEditingProduct] = useState(null);
  const [formData, setFormData] = useState({
    sku: "",
    name: "",
    description: "",
    price: "",
    categoryIds: [],
  });
  const [showForm, setShowForm] = useState(false);

  const currentUser = authService.getCurrentUser();
  const readOnly = currentUser?.role === "User"; // User cannot perform CRUD

  // Fetch products
  const fetchProducts = () => {
    fetch(API_URL)
      .then((res) => res.json())
      .then((data) => setProducts(data))
      .catch((err) => console.error("Error fetching products:", err));
  };

  // Fetch categories
  const fetchCategories = () => {
    fetch(CATEGORY_URL)
      .then((res) => res.json())
      .then((data) => setCategories(data))
      .catch((err) => console.error("Error fetching categories:", err));
  };

  useEffect(() => {
    fetchProducts();
    fetchCategories();
  }, []);

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleCategorySelect = (e) => {
    setFormData({
      ...formData,
      categoryIds: Array.from(e.target.selectedOptions, (opt) => parseInt(opt.value)),
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (readOnly) return;

    const method = editingProduct ? "PUT" : "POST";
    const url = editingProduct ? `${API_URL}/${editingProduct.productId}` : API_URL;

    try {
      const res = await fetch(url, {
        method,
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(
          editingProduct ? { ...formData, productId: editingProduct.productId } : formData
        ),
      });

      if (!res.ok) throw new Error("Failed to save product");

      const savedProduct = await res.json();
      if (editingProduct) {
        setProducts(products.map((p) => (p.productId === savedProduct.productId ? savedProduct : p)));
      } else {
        setProducts([savedProduct, ...products]);
      }

      setFormData({ sku: "", name: "", description: "", price: "", categoryIds: [] });
      setEditingProduct(null);
      setShowForm(false);
    } catch (err) {
      console.error(err);
    }
  };

  const handleDelete = async (id) => {
    if (readOnly) return;
    if (!window.confirm("Are you sure you want to delete this product?")) return;

    try {
      const res = await fetch(`${API_URL}/${id}`, { method: "DELETE" });
      if (!res.ok) throw new Error("Delete failed");
      setProducts(products.filter((p) => p.productId !== id));
    } catch (err) {
      console.error(err);
    }
  };

  const handleEdit = (prod) => {
    if (readOnly) return;

    setEditingProduct(prod);
    setFormData({
      sku: prod.sku,
      name: prod.name,
      description: prod.description || "",
      price: prod.price,
      categoryIds: prod.categories?.map((c) => c.categoryId) || [],
    });
    setShowForm(true);
  };

  return (
    <div className="product-container">
      <h2>Manage Products</h2>

      {/* Add Product Button */}
      {!readOnly && (
        <button
          className="btn-add"
          onClick={() => {
            setEditingProduct(null);
            setFormData({ sku: "", name: "", description: "", price: "", categoryIds: [] });
            setShowForm(!showForm);
          }}
        >
          {showForm ? "Hide Form" : "Add Product"}
        </button>
      )}

      {/* Form */}
      {showForm && !readOnly && (
        <form onSubmit={handleSubmit} className="product-form">
          <div className="form-group">
            <label>SKU</label>
            <input name="sku" value={formData.sku} onChange={handleChange} required />
          </div>
          <div className="form-group">
            <label>Name</label>
            <input name="name" value={formData.name} onChange={handleChange} required />
          </div>
          <div className="form-group">
            <label>Description</label>
            <textarea name="description" value={formData.description} onChange={handleChange} />
          </div>
          <div className="form-group">
            <label>Price</label>
            <input
              type="number"
              name="price"
              value={formData.price}
              onChange={handleChange}
              required
            />
          </div>
          <div className="form-group">
            <label>Categories</label>
            <select multiple value={formData.categoryIds} onChange={handleCategorySelect}>
              {categories.map((c) => (
                <option key={c.categoryId} value={c.categoryId}>
                  {c.categoryName}
                </option>
              ))}
            </select>
          </div>
          <div className="form-buttons">
            <button type="submit" className="btn-submit">
              {editingProduct ? "Update Product" : "Add Product"}
            </button>
            {editingProduct && (
              <button
                type="button"
                className="btn-cancel"
                onClick={() => {
                  setEditingProduct(null);
                  setFormData({ sku: "", name: "", description: "", price: "", categoryIds: [] });
                  setShowForm(false);
                }}
              >
                Cancel
              </button>
            )}
          </div>
        </form>
      )}

      {/* Product Table */}
      <table className="product-table">
        <thead>
          <tr>
            <th>Product ID</th>
            <th>SKU</th>
            <th>Name</th>
            <th>Price</th>
            <th>Categories</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {products.map((p) => (
            <tr key={p.productId}>
              <td>{p.productId}</td>
              <td>{p.sku}</td>
              <td>{p.name}</td>
              <td>{p.price}</td>
              <td>{p.categories?.map((c) => c.categoryName).join(", ")}</td>
              <td>
                <button className="btn-edit" onClick={() => handleEdit(p)} disabled={readOnly}>
                  Edit
                </button>
                <button className="btn-delete" onClick={() => handleDelete(p.productId)} disabled={readOnly}>
                  Delete
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default ProductList;
