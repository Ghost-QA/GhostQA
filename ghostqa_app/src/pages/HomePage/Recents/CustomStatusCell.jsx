import React from "react";
import { TableCell, Chip } from "@mui/material";

const CustomStatusCell = ({ status }) => {
  const capitalizeFirstLetter = (string) => {
    return string?.charAt(0).toUpperCase() + string?.slice(1);
  };

  const getStatusStyle = () => {
    let style = {
      backgroundColor:
        status === "Passed"
          ? "rgb(7, 217, 176)"
          : status === "Failed" 
          ? "rgb(247, 77, 77)"
          : "transparent",
      color: "rgb(253, 253, 253)",
      borderRadius: "4px",
    };

    return style;
  };

  return (
      <Chip
        label={status && capitalizeFirstLetter(status)}
        style={{ ...getStatusStyle(), height: "24px" }}
      />
  );
};

export default CustomStatusCell;