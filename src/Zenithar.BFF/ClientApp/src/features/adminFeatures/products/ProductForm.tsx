import React, {VFC} from "react";
import AppFrom from "lib/form/AppFrom";
import {Button, FloatingLabel, FormControl} from "react-bootstrap";
import FormInput from "lib/form/FormInput";
import {FormikProvider, useFormik} from "formik";
import {ButtonVariant} from "react-bootstrap/types";
import {FormikHelpers} from "formik/dist/types";
import * as yup from "yup";

export interface ProductFormValues {
  name: string;
  price: number;
  image?: any;
}

export const productValidationSchema = yup.object({
  name: yup.string().label("Name").min(3),
  price: yup.number().label("Price").min(1)
});

export interface ProductFormProps {
  buttonText: string;
  buttonVariant?: ButtonVariant;
  isLoading?: boolean;
  onSubmit: (values: ProductFormValues, formikHelpers: FormikHelpers<ProductFormValues>) => void | Promise<any>;
  initialValues: ProductFormValues;
}

const ProductForm: VFC<ProductFormProps> = React.memo((
  {isLoading, buttonText, buttonVariant, onSubmit, initialValues}) => {
  const formik = useFormik<ProductFormValues>({
    initialValues: initialValues,
    validationSchema: productValidationSchema,
    onSubmit: onSubmit
  });

  return (
    <FormikProvider value={formik}>
      <AppFrom onSubmit={formik.handleSubmit}>
        <FloatingLabel
          label="Name"
          controlId="product-name"
        >
          <FormInput
            field="name"
            placeholder="Name"
          />
        </FloatingLabel>

        <FloatingLabel
          label="Price"
          controlId="price"
        >
          <FormInput
            field="price"
            placeholder=""
            type="number"
          />
        </FloatingLabel>

        <FormControl
          type="file"
          accept={"image/*"}
          className={"mb-3"}
          onChange={(event) => {
            //@ts-expect-error
            formik.setFieldValue("image", event.currentTarget.files[0]);
          }}
        />

        <Button
          variant={buttonVariant}
          className={"w-100"}
          type="submit"
          disabled={isLoading}
        >
          {buttonText}
        </Button>

      </AppFrom>
    </FormikProvider>
  );
});
ProductForm.displayName = "ProductForm";
export default ProductForm;
