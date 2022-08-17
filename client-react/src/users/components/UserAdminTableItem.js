import React, { Component, useEffect, useState } from "react";
import axios from "../../api/axios";
import { Link, useNavigate } from "react-router-dom";
import useAuth from "../../shared/hooks/auth-hook";
import ConfirmRequestWindow from "../../shared/components/ConfirmRequest/ConfirmRequest";
import Backdrop from "../../shared/components/ConfirmRequest/Backdrop";

const ROLES_ADMIN_URL = "/api/admin/roles/";
const USERS_ADMIN_URL = "/api/admin/users/";

const UserAdminTableItem = (props) => {
  const { auth } = useAuth();
  const [roles, setRoles] = useState("");
  const [confirmedRequest, setConfirmedRequest] = useState(false);

  const resetPasswordHandler = async (e) => {
    e.preventDefault();
    try {
      await axios.post(
        USERS_ADMIN_URL + "get-reset-link/",
        { email: props.item.email },
        {
          headers: {
            Authorization: `Bearer ${auth?.accessToken}`,
          },
        }
      );
      props.refresh();
    } catch (err) {
      setErrMsg("Reset password failed");
    }
  };

  useEffect(() => {
    const getRoles = async () => {
      if (props?.item?.id != null) {
        try {
          axios
            .get(ROLES_ADMIN_URL + "get-user-roles/" + props.item.id, {
              headers: {
                Authorization: `Bearer ${auth?.accessToken}`,
              },
            })
            .then((response) => {
              console.log(response);
              if (Array.isArray(response.data))
                setRoles(response.data.join(","));
            });
          props.refresh();
        } catch (err) {
          setErrMsg("Getting roles failed");
        }
      }
    };
    getRoles();
  }, [props.id]);

  const blockHandler = async (e) => {
    e.preventDefault();
    if (props.item.isBlocked == true) {
      try {
        console.log({
          headers: {
            Authorization: `Bearer ${auth?.accessToken}`,
          },
        });
        await axios.put(
          USERS_ADMIN_URL + `unblock/${props.item.id}`,
          {},
          {
            headers: {
              Authorization: `Bearer ${auth?.accessToken}`,
            },
          }
        );
        props.refresh();
      } catch (err) {
        setErrMsg("Block failed");
      }
    } else {
      try {
        console.log({
          headers: {
            Authorization: `Bearer ${auth?.accessToken}`,
          },
        });
        await axios.put(
          USERS_ADMIN_URL + `block/${props.item.id}`,
          {},
          {
            headers: {
              Authorization: `Bearer ${auth?.accessToken}`,
            },
          }
        );
        props.refresh();
      } catch (err) {
        setErrMsg("Block failed");
      }
    }
  };

  const deleteHandler = async (e) => {
    e.preventDefault();
    try {
      await axios.delete(USERS_ADMIN_URL + props.item.id, {
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
        <td>{props.item.username}</td>
        <td>{props.item.email}</td>
        <td>{props.item.dateOfBirth}</td>
        <td>{String(props.item.isBlocked)}</td>
        <td>{String(props.item.emailConfirmed)}</td>
        <td>{roles}</td>
        <td>
          {props.item.isBlocked == false && (
            <Link to={""}>
              <button onClick={blockHandler}>Block user</button>
            </Link>
          )}
          {props.item.isBlocked && (
            <Link to={""}>
              <button onClick={blockHandler}>Unblock user</button>
            </Link>
          )}
        </td>
        <td>
          <Link to={""}>
            <button onClick={resetPasswordHandler}>Reset password</button>
          </Link>
        </td>
        <td>
          <Link to={"#"}>
            <button onClick={() => setConfirmedRequest(true)}>Delete</button>
          </Link>
        </td>
      </tr>
    </>
  );
};

export default UserAdminTableItem;
