import { useEffect, useRef } from "react";
import { Modal as BootstrapModal } from "bootstrap";
import type { IUser } from "../core/User";
import FormInput from "./FormInput";

interface ModalProps {
  show: boolean;
  user: IUser;
  isEditMode: boolean;
  onChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
  onSave: () => void;
  onClose: () => void;
}

const Modal: React.FC<ModalProps> = ({
  show,
  user,
  isEditMode,
  onChange,
  onSave,
  onClose,
}) => {
  const modalRef = useRef<HTMLDivElement>(null);
  const bootstrapModal = useRef<BootstrapModal | null>(null);

  useEffect(() => {
    if (modalRef.current) {
      bootstrapModal.current = new BootstrapModal(modalRef.current, {
        backdrop: true,
        keyboard: true,
      });

      modalRef.current.addEventListener("hidden.bs.modal", onClose);
    }

    return () => {
      bootstrapModal.current?.dispose();
    };
  }, []);

  useEffect(() => {
    if (!bootstrapModal.current) return;

    if (show) {
      bootstrapModal.current.show();
    } else {
      bootstrapModal.current.hide();
    }
  }, [show]);

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    onSave();
  };

  return (
    <div className="modal fade" ref={modalRef} tabIndex={-1}>
      <div className="modal-dialog">
        <div className="modal-content">
          <div className="modal-header">
            <h5 className="modal-title">
              {isEditMode ? `Edit User - ${user.name}` : "Create New User"}
            </h5>
            <button
              type="button"
              className="btn-close"
              aria-label="Close"
              onClick={() => bootstrapModal.current?.hide()}
            />
          </div>

          <form onSubmit={handleSubmit}>
            <div className="modal-body">
              <FormInput
                id="name"
                name="name"
                label="Name"
                value={user.name}
                onChange={onChange}
                placeholder="Enter name"
                required
              />

              <FormInput
                id="userName"
                name="userName"
                label="Username"
                value={user.userName}
                onChange={onChange}
                placeholder="Enter username"
                required
              />

              <FormInput
                id="email"
                name="email"
                label="Email"
                type="email"
                value={user.email}
                onChange={onChange}
                placeholder="Enter email"
                required
              />
            </div>

            <div className="modal-footer">
              <button
                type="button"
                className="btn btn-secondary"
                onClick={() => bootstrapModal.current?.hide()}
              >
                Close
              </button>
              <button type="submit" className="btn btn-primary">
                {isEditMode ? "Save Changes" : "Create User"}
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  );
};

export default Modal;
