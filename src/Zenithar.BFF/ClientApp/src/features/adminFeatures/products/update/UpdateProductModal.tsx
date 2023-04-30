import React, {VFC} from "react";
import {Modal, Row} from "react-bootstrap";
import {ModalProps} from "react-bootstrap/Modal";
import {ProductDto} from "features/products/productsApi";
import UpdateProductForm from "./UpdateProductForm";

export interface UpdateProductModalProps extends ModalProps {
  product: ProductDto;
  onUpdated?: () => void;
}

const UpdateProductModal: VFC<UpdateProductModalProps> =
  React.memo(({product, onUpdated, ...props}) => {
    return (
      <Modal
        centered
        {...props}
      >
        <Modal.Header closeButton>
          <Row>
            <Modal.Title>Update product</Modal.Title>
            <div className="d-block text-muted small">Id: {product.id}</div>
          </Row>
        </Modal.Header>
        <Modal.Body>
          <UpdateProductForm initialProduct={product} onUpdated={onUpdated}/>
        </Modal.Body>
      </Modal>
    );
  });

export default UpdateProductModal;
UpdateProductModal.displayName = "UpdateProductModal";