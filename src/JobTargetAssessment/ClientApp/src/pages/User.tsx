import { useState, useEffect } from "react";
import type { IUser } from "../core/User";
import Modal from "../components/Modal";
import UserCard from "../components/UserCard";

const User: React.FC = () => {
  const [users, setUsers] = useState<IUser[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<Error | null>(null);
  const [selectedUser, setSelectedUser] = useState<IUser | null>(null);
  const [showModal, setShowModal] = useState(false);
  const [isEditMode, setIsEditMode] = useState(false);

  const API_BASE ="http://localhost:5208/api";

  const emptyUser: IUser = {
    id: 0,
    name: "",
    userName: "",
    email: "",
    phone: "",
    website: "",
  };

  useEffect(() => {
    fetchUsers();
  }, []);

  const fetchUsers = async () => {
    try {
      setLoading(true);
      const response = await fetch(`${API_BASE}/user`);
      if (!response.ok) throw new Error("Failed to fetch users");
      const data = await response.json();
      setUsers(data);
      setError(null);
    } catch (err) {
      if (err instanceof Error) setError(err);
    } finally {
      setLoading(false);
    }
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setSelectedUser((prev) => (prev ? { ...prev, [name]: value } : null));
  };

  const handleCreate = () => {
    setSelectedUser(emptyUser);
    setIsEditMode(false);
    setShowModal(true);
  };

  const handleEdit = (user: IUser) => {
    setSelectedUser(user);
    setIsEditMode(true);
    setShowModal(true);
  };

  const handleClose = () => {
    setShowModal(false);
    setSelectedUser(null);
  };

  const handleSave = async () => {
    if (!selectedUser) return;

    try {
      if (isEditMode) {
        const response = await fetch(`${API_BASE}/user/${selectedUser.id}`, {
          method: "PUT",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify(selectedUser),
        });

        if (!response.ok) throw new Error("Failed to update user");

        setUsers((prev) =>
          prev.map((u) => (u.id === selectedUser.id ? selectedUser : u)),
        );
      } else {
        const response = await fetch(`${API_BASE}/user`, {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify(selectedUser),
        });

        if (!response.ok) throw new Error("Failed to create user");

        const newUser = await response.json();
        setUsers((prev) => [...prev, newUser]);
      }

      handleClose();
    } catch (err) {
      if (err instanceof Error) setError(err);
    }
  };

  if (loading) return <div>Loading...</div>;
  if (error) return <div>{error.message}</div>;

  return (
    <>
      <div style={{ padding: "1rem" }}>
        <button
          type="button"
          className="btn btn-primary"
          onClick={handleCreate}
        >
          Create User
        </button>
      </div>

      <div
        style={{
          display: "flex",
          flexWrap: "wrap",
          gap: "1rem",
          padding: "1rem",
        }}
      >
        {users.map((user) => (
          <UserCard key={user.id} user={user} onEdit={handleEdit} />
        ))}
      </div>

      <Modal
        show={showModal}
        user={selectedUser ?? emptyUser}
        isEditMode={isEditMode}
        onChange={handleChange}
        onSave={handleSave}
        onClose={handleClose}
      />
    </>
  );
};

export default User;
