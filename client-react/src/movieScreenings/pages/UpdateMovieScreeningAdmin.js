import { useRef, useState, useEffect } from "react";
import {
  faCheck,
  faTimes,
  faInfoCircle,
} from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import axios from "../../api/axios";
import { Link, useNavigate, useParams } from "react-router-dom";
import useAuth from "../../shared/hooks/auth-hook";
import ConfirmRequestWindow from "../../shared/components/ConfirmRequest/ConfirmRequest";
import Backdrop from "../../shared/components/ConfirmRequest/Backdrop";

const MOVIE_SCREENING_UPLOAD_URL = "/api/admin/movieScreenings/";

const UpdateMovieScreeningAdmin = () => {
  const { id } = useParams();
  const errRef = useRef();
  const auth = useAuth();
  const [confirmedRequest, setConfirmedRequest] = useState(false);

  const [startTime, setStartTime] = useState();
  const [ticketPrice, setTicketPrice] = useState();
  const [numOfRows, setNumOfRows] = useState();
  const [numOfColumns, setNumOfColumns] = useState();
  const [errMsg, setErrMsg] = useState("");
  const [success, setSuccess] = useState(false);

  const handleSubmit = async (e) => {
    e.preventDefault();
    setConfirmedRequest(false);
    try {
      // Update the formData object
      const response = await axios.put(
        MOVIE_SCREENING_UPLOAD_URL + id,
        { startTime, ticketPrice, numOfRows, numOfColumns },
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
      <h1>Update movie screening</h1>
      {success ? (
        <section>
          <h1>Success!</h1>
          <p>
            Movie screening has been updated. <Link to={-1}></Link>
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
              <label htmlFor="startTime">Starting time:</label>
              <input
                type="datetime-local"
                id="startTime"
                onChange={(e) => setStartTime(e.target.value)}
              />
              <label htmlFor="ticketPrice">Ticket price:</label>
              <input
                type="number"
                step="0.01"
                id="ticketPrice"
                onChange={(e) => setTicketPrice(e.target.value)}
              ></input>
              <label htmlFor="numOfRows">Number of rows:</label>
              <input
                type="number"
                id="numOfRows"
                onChange={(e) => setNumOfRows(e.target.value)}
              />
              <label htmlFor="numOfColumns">Number of columns:</label>
              <input
                type="number"
                id="numOfColumns"
                onChange={(e) => setNumOfColumns(e.target.value)}
              />
              <button className="btn" onClick={handleSubmit}>
                Update movie screening
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

export default UpdateMovieScreeningAdmin;
