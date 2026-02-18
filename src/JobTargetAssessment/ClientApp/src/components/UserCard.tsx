import React from "react";
import type { IUser } from "../core/User";

interface UserCardProps {
  user: IUser;
  onEdit: (user: IUser) => void;
}

const UserCard: React.FC<UserCardProps> = ({ user, onEdit, }) => {
  return (
    <div className="card" style={{ maxWidth: "18rem" }}>
      <div className="card-body">
        <h5 className="card-title">{user.name}</h5>
        <h6 className="card-subtitle mb-2 text-body-secondary">
          {user?.company?.name}
        </h6>
        <p className="card-text">{user.email}</p>

        <button
          type="button"
          className="btn btn-primary"
          onClick={() => onEdit(user)}
        >
          Update
        </button>

        <button
          type="button"
          className="btn btn-danger"
        >
          Delete
        </button>
      </div>
    </div>
  );
};

export default UserCard;
