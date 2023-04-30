import React, {VFC} from "react";
import productsApi, {ProductDto} from "../../../products/productsApi";
import ProductForm from "../ProductForm";

export interface EditProductFormProps {
  initialProduct: ProductDto;
  onUpdated?: () => void;
}

const UpdateProductForm: VFC<EditProductFormProps> =
  React.memo(({initialProduct, onUpdated}) => {
    const [updateProduct, {isLoading}] = productsApi.useUpdateProductMutation();

    return (
      <ProductForm
        buttonText={"Update"}
        buttonVariant={"warning"}
        initialValues={initialProduct}
        isLoading={isLoading}
        onSubmit={async values => {
          await updateProduct({id: initialProduct.id, ...values});
          onUpdated?.();
        }}
      />
    );
  })
;
UpdateProductForm.displayName = "UpdateProductForm";
export default UpdateProductForm;