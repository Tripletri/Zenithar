import React, {VFC} from "react";
import ProductForm, {ProductFormValues} from "../ProductForm";
import productsApi from "../../../products/productsApi";

const initialValues: ProductFormValues = {
  name: "",
  price: 0
};

const CreateProductForm: VFC = React.memo(() => {
  const [createProduct, {isLoading}] = productsApi.useCreateProductMutation();

  return (
    <ProductForm
      buttonText={"Create"}
      buttonVariant={"success"}
      initialValues={initialValues}
      isLoading={isLoading}
      onSubmit={async (values, form) => {
        console.debug(values);

        if (values.image === undefined) {
          return;
        }

        await createProduct({...values, image: values.image});
        form.resetForm();
      }}
    />
  );
});
CreateProductForm.displayName = "CreateProductForm";
export default CreateProductForm;