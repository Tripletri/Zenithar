import React, {VFC} from "react";
import {ModalProps} from "react-bootstrap/Modal";
import {Modal} from "react-bootstrap";
import CreateProductForm from "./CreateProductForm";

const CreateProductModal: VFC<ModalProps> = React.memo((props) => {
  return (
    <Modal centered {...props}>
      <Modal.Header closeButton>
        <Modal.Title>Create product</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <CreateProductForm/>
      </Modal.Body>
    </Modal>
  );
});
CreateProductModal.displayName = "CreateProductModal";
export default CreateProductModal;