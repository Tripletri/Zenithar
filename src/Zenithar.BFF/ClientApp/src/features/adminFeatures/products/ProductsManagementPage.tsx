import React, {VFC} from "react";
import PageHeader from "lib/PageHeader";
import ProductsTable from "./ProductsTable";

const ProductsManagementPage: VFC = React.memo(() => {
  return (
    <>
      <PageHeader>Products management</PageHeader>
      <ProductsTable/>
    </>
  );
});
ProductsManagementPage.displayName = "ProductsManagementPage";
export default ProductsManagementPage;