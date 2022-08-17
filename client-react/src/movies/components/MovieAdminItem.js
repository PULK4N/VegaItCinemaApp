import { Button } from "bootstrap";
import React, { Component, useEffect, useState } from "react";
import axios from "../../api/axios";
import { Link, useNavigate, useParams } from "react-router-dom";
import ConfirmRequestWindow from "../../shared/components/ConfirmRequest/ConfirmRequest";
import Backdrop from "../../shared/components/ConfirmRequest/Backdrop";
import useAuth from "../../shared/hooks/auth-hook";
import ImageItem from "./ImageItem";

const MOVIES_ADMIN_URL = "/api/movies/";
const IMAGES_URL = "/api/images/";

const MovieAdminItem = (props) => {
  const { id } = useParams();
  const { auth } = useAuth();
  const navigate = useNavigate();
  const [confirmedRequest, setConfirmedRequest] = useState(false);

  const [loadedImage, setLoadedImage] = useState();

  const deleteHandler = async () => {
    try {
      await axios.delete(MOVIES_ADMIN_URL + id, {
        headers: {
          Authorization: `Bearer ${auth?.accessToken}`,
        },
      });
      navigate("/admin/movies");
    } catch (err) {
      setErrMsg("Login Failed");
    }
  };

  useEffect(() => {
    const fetchImage = async () => {
      try {
        setLoadedImage(
          "https://localhost:7160/api/images/" + props.item.posterImageId
        );
      } catch (err) {
        console.log(err);
      }
    };
    fetchImage();
  }, [props.item]);

  const [errMsg, setErrMsg] = useState("");

  return (
    <div>
      {confirmedRequest && (
        <Backdrop Backdrop={() => setConfirmedRequest(false)}></Backdrop>
      )}
      {confirmedRequest && (
        <ConfirmRequestWindow
          ConfirmRequest={deleteHandler}
          CancelRequest={() => setConfirmedRequest(false)}
        ></ConfirmRequestWindow>
      )}
      <h1>{props.item.name}</h1>
      <ImageItem src={loadedImage} />
      <p>Original name: {props.item.originalName}</p>
      <p>Duration: {props.item.durationMinutes}</p>
      <p>Rating: {props.item.rating}</p>
      <span>
        <Link
          type="button"
          state={{ fromDashboard: true }}
          to={`/admin/movies/update/${id}`}
        >
          <button className="btn">Update</button>
        </Link>
        <Link to={""}>
          <button className="btn" onClick={() => setConfirmedRequest(true)}>
            Delete
          </button>
        </Link>
      </span>
      <h3>Genres:</h3>
      <ol>
        {props.item.genres?.map((item) => (
          <li key={item.id}>{item.name}</li>
        ))}
      </ol>
    </div>
  );
};

export default MovieAdminItem;
