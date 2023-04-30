import React, {useState, VFC} from "react";
import {Button, Spinner, Table} from "react-bootstrap";
import styles from "./ProductsTable.module.scss";
import CreateProductModal from "./create/CreateProductModal";
import UpdateProductModal from "./update/UpdateProductModal";
import productsApi, {ProductDto} from "../../products/productsApi";

const ProductsTable: VFC = React.memo(() => {
  const {
    data: productsResponse,
    isSuccess
  } = productsApi.useGetAllProductsQuery();
  const [showCreateModal, setShowCreateModal] = useState(false);

  if (!isSuccess)
    return <Spinner animation={"border"}/>;

  return (
    <>
      <CreateProductModal
        show={showCreateModal}
        onHide={() => setShowCreateModal(false)}
      />
      <Table striped bordered hover variant="dark">
        <thead>
        <tr className={styles.row}>
          <th>Preview</th>
          <th>Id</th>
          <th>Name</th>
          <th>Price</th>
          <th className="text-center">Edit</th>
          <th>
            <Button variant="success" onClick={() => setShowCreateModal(true)}>
              <i className="bi bi-plus-square"/>
            </Button>
          </th>
        </tr>
        </thead>
        <tbody>
        {productsResponse.items.map(product =>
          <ProductRow product={product} key={product.id}/>)}
        </tbody>
      </Table>
    </>
  );
});
ProductsTable.displayName = "ProductsTable";
export default ProductsTable;

interface ProductRowProp {
  product: ProductDto;
}

const ProductRow: VFC<ProductRowProp> = React.memo(({product}) => {
  const [show, setShow] = useState(false);
  const [deleteProduct, {isLoading}] = productsApi.useRemoveProductMutation();

  return (
    <>
      <UpdateProductModal
        product={product}
        show={show}
        onHide={() => setShow(false)}
        onUpdated={() => setShow(false)}
      />
      <tr className={styles.row}>
        <td className="text-center">
          <img
            className={styles.img}
            src={product.previewUrl}
            alt={product.name}
          />
        </td>
        <td>{product.id}</td>
        <td>{product.name}</td>
        <td>{product.price}$</td>
        <td onClick={() => setShow(true)}>
          <Button variant="warning">
            <i className="bi bi-pencil-square"></i>
          </Button>
        </td>
        <td>
          <Button
            variant="danger"
            disabled={isLoading}
            onClick={() => deleteProduct({id: product.id})}
          >
            <i className="bi bi-trash"></i>
          </Button>
        </td>
      </tr>
    </>
  );
});
ProductRow.displayName = "ProductRow";
