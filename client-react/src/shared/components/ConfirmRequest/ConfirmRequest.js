import React from "react";
import { useState } from "react";
import "./ConfirmRequest.css";

const ConfirmRequestWindow = (props) => {
  return (
    <div className="confirmRequest">
      <p>Are you sure?</p>
      <button className="btn btn--alt" onClick={props.CancelRequest}>
        Cancel
      </button>
      <button
        className="btn btn-danger
      "
        onClick={props.ConfirmRequest}
      >
        Confirm
      </button>
    </div>
  );
};

export default ConfirmRequestWindow;
