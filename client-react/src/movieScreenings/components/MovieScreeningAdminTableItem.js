import { Button } from "bootstrap";
import React, { Component, useEffect, useState } from "react";
import axios from "../../api/axios";
import { Link, useNavigate } from "react-router-dom";
import useAuth from "../../shared/hooks/auth-hook";
import ConfirmRequestWindow from "../../shared/components/ConfirmRequest/ConfirmRequest";
import Backdrop from "../../shared/components/ConfirmRequest/Backdrop";

const MOVIES_SCREENING_ADMIN_URL = "/api/admin/movieScreenings/";

const MovieScreeningAdminTableItem = (props) => {
  const [confirmedRequest, setConfirmedRequest] = useState(false);
  const { auth } = useAuth();

  const deleteHandler = async () => {
    try {
      await axios.delete(MOVIES_SCREENING_ADMIN_URL + props.item.id, {
        headers: {
          Authorization: `Bearer ${auth?.accessToken}`,
        },
      });
      props.refresh();
    } catch (err) {
      setErrMsg("Delete Failed");
    }
  };

  const [errMsg, setErrMsg] = useState("");

  return (
    <>
      {confirmedRequest && (
        <Backdrop Backdrop={() => setConfirmedRequest(false)}></Backdrop>
      )}
      {confirmedRequest && (
        <ConfirmRequestWindow
          ConfirmRequest={deleteHandler}
          CancelRequest={() => setConfirmedRequest(false)}
        ></ConfirmRequestWindow>
      )}
      <tr>
        <td>{props.item.name}</td>
        <td>{props.item.startTime}</td>
        <td>{props.item.ticketPrice}</td>
        <td>{props.item.numOfRows}</td>
        <td>{props.item.numOfColumns}</td>
        <td>
          <Link to={`update/${props.item.id}`}>
            <button>Update</button>
          </Link>
        </td>
        <td>
          <Link to={""}>
            <button onClick={() => setConfirmedRequest(true)}>Delete</button>
          </Link>
        </td>
      </tr>
    </>
  );
};

export default MovieScreeningAdminTableItem;
