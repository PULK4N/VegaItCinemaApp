import { useRef, useState, useEffect } from "react";
import {
  faCheck,
  faTimes,
  faInfoCircle,
} from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import axios from "../../api/axios";
import { Link, useNavigate, useParams } from "react-router-dom";
import GenreFilterItem from "../../shared/components/Filter/GenreFilterItem";
import useAuth from "../../shared/hooks/auth-hook";
import ConfirmRequestWindow from "../../shared/components/ConfirmRequest/ConfirmRequest";
import Backdrop from "../../shared/components/ConfirmRequest/Backdrop";

const GENRE_UPLOAD_URL = "/api/admin/genres/";

const UpdateGenreAdmin = () => {
  const { id } = useParams();
  const errRef = useRef();
  const auth = useAuth();
  const [confirmedRequest, setConfirmedRequest] = useState(false);

  const [name, setName] = useState("");
  const [errMsg, setErrMsg] = useState("");
  const [success, setSuccess] = useState(false);

  const handleSubmit = async (e) => {
    e.preventDefault();
    setConfirmedRequest(false);
    try {
      // Update the formData object
      const response = await axios.put(
        GENRE_UPLOAD_URL + id,
        { name: name },
        {
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${auth?.auth?.accessToken}`,
            withCredentials: false,
          },
        }
      );
      setSuccess(true);
    } catch (err) {
      setErrMsg("Update Failed");
      errRef.current.focus();
    }
  };

  return (
    <>
      <h1>Update genre</h1>
      {success ? (
        <section>
          <h1>Success!</h1>
          <p>
            Genre has been updated. <Link to={-1}></Link>
          </p>
        </section>
      ) : (
        <section>
          <div>
            <form>
              <p
                ref={errRef}
                className={errMsg ? "errmsg" : "offscreen"}
                aria-live="assertive"
              >
                {errMsg}
              </p>
              <label htmlFor="name">Name:</label>
              <input
                type="text"
                id="name"
                onChange={(e) => setName(e.target.value)}
              />
              <button
                className="btn"
                onClick={(e) => {
                  e.preventDefault();
                  setConfirmedRequest(true);
                }}
              >
                Update genre
              </button>
            </form>
          </div>
          {confirmedRequest && (
            <Backdrop Backdrop={() => setConfirmedRequest(false)}></Backdrop>
          )}
          {confirmedRequest && (
            <ConfirmRequestWindow
              ConfirmRequest={handleSubmit}
              CancelRequest={() => setConfirmedRequest(false)}
            ></ConfirmRequestWindow>
          )}
        </section>
      )}
    </>
  );
};

export default UpdateGenreAdmin;
