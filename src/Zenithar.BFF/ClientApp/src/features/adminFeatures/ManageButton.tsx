import React, {FC} from "react";
import {Button} from "react-bootstrap";
import {useNavigate} from "react-router-dom";

const ManageButton: FC = React.memo(() => {
  const navigate = useNavigate();

  return (
    <Button
      variant={"info"}
      onClick={() => navigate("/manage/products")}
    >
      Manage
    </Button>
  );
});

export default ManageButton;