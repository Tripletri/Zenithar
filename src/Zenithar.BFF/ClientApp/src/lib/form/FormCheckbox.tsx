import React, {VFC} from "react";
import {Form, FormCheckProps} from "react-bootstrap";
import {useField} from "formik";

interface FormCheckboxProps extends FormCheckProps {
  field: string;
}

const FormCheckbox: VFC<FormCheckboxProps> = React.memo(({field, ...props}) => {
  const [fieldProps, meta] = useField(field);

  return (
    <>
      <Form.Check
        {...fieldProps}
        {...props}
        isInvalid={meta.touched && !!meta.error}
        feedbackType="invalid"
        feedback={meta.error}
        id={field}
        checked={meta.value}
      />
    </>
  );
});
FormCheckbox.displayName = "FormCheck";
export default FormCheckbox;