import React, {FC} from "react";
import {Form} from "react-bootstrap";
import styles from "./AppForm.module.scss";
import {FormProps} from "react-bootstrap/Form";

const AppFrom: FC<FormProps> = React.memo(({children, ...props}) => {

  return (
    <Form noValidate className={styles.appForm} {...props}>
      {children}
    </Form>
  );
});
AppFrom.displayName = "AppFrom";
export default AppFrom;