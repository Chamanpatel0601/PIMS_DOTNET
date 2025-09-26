import React, { useEffect, useState } from "react";
import "../styles/CategoryList.css"; // Import CSS

const API_URL = "https://localhost:7175/api/Category";

const CategoryList = () => {
  const [categories, setCategories] = useState([]);
  const [editingCategory, setEditingCategory] = useState(null);
  const [formData, setFormData] = useState({ categoryName: "", description: "" });
  const [showForm, setShowForm] = useState(false);

  const fetchCategories = () => {
    fetch(API_URL)
      .then((res) => res.json())
      .then((data) => setCategories(data))
      .catch((err) => console.error("Error fetching categories:", err));
  };

  useEffect(() => {
    fetchCategories();
  }, []);

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    const method = editingCategory ? "PUT" : "POST";
    const url = editingCategory ? `${API_URL}/${editingCategory.categoryId}` : API_URL;

    try {
      const res = await fetch(url, {
        method,
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(
          editingCategory ? { ...formData, categoryId: editingCategory.categoryId } : formData
        ),
      });

      if (!res.ok) throw new Error("Failed to save category");

      setFormData({ categoryName: "", description: "" });
      setEditingCategory(null);
      setShowForm(false);
      fetchCategories();
    } catch (err) {
      console.error(err);
    }
  };

  const handleDelete = async (id) => {
    if (!window.confirm("Are you sure you want to delete this category?")) return;

    try {
      const res = await fetch(`${API_URL}/${id}`, { method: "DELETE" });
      if (!res.ok) throw new Error("Delete failed");
      fetchCategories();
    } catch (err) {
      console.error(err);
    }
  };

  const handleEdit = (cat) => {
    setEditingCategory(cat);
    setFormData({ categoryName: cat.categoryName, description: cat.description || "" });
    setShowForm(true);
  };

  return (
    <div className="category-container">
      <h2 className="category-title">Manage Categories</h2>

      {!showForm && (
        <div className="add-btn-container">
          <button className="btn-add" onClick={() => setShowForm(true)}>
            Add Category
          </button>
        </div>
      )}

      {showForm && (
        <form onSubmit={handleSubmit} className="category-form">
          <div className="form-group">
            <label>Name</label>
            <input
              name="categoryName"
              className="form-input"
              value={formData.categoryName}
              onChange={handleChange}
              required
            />
          </div>
          <div className="form-group">
            <label>Description</label>
            <input
              name="description"
              className="form-input"
              value={formData.description}
              onChange={handleChange}
            />
          </div>
          <div className="form-buttons">
            <button className="btn-save" type="submit">
              {editingCategory ? "Update Category" : "Add Category"}
            </button>
            <button
              className="btn-cancel"
              type="button"
              onClick={() => {
                setEditingCategory(null);
                setFormData({ categoryName: "", description: "" });
                setShowForm(false);
              }}
            >
              Cancel
            </button>
          </div>
        </form>
      )}

      <table className="category-table">
        <thead>
          <tr>
            <th>Name</th>
            <th>Description</th>
            <th className="actions-col">Actions</th>
          </tr>
        </thead>
        <tbody>
          {categories.map((c) => (
            <tr key={c.categoryId}>
              <td>{c.categoryName}</td>
              <td>{c.description}</td>
              <td>
                <button className="btn-edit" onClick={() => handleEdit(c)}>
                  Edit
                </button>
                <button className="btn-delete" onClick={() => handleDelete(c.categoryId)}>
                  Delete
                </button>
              </td>
            </tr>
          ))}
          {categories.length === 0 && (
            <tr>
              <td colSpan="3" className="no-data">
                No categories found
              </td>
            </tr>
          )}
        </tbody>
      </table>
    </div>
  );
};

export default CategoryList;
