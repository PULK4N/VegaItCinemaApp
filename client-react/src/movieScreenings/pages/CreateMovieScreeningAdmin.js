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
import MovieScreeningSelector from "../components/MovieSelectorAdmin";

const GENRE_UPLOAD_URL = "/api/admin/movieScreenings/";

const CreateMovieScreeningAdmin = () => {
  const errRef = useRef();
  const auth = useAuth();

  const [movieId, setMovieId] = useState();
  const [startTime, setStartTime] = useState();
  const [ticketPrice, setTicketPrice] = useState();
  const [numOfRows, setNumOfRows] = useState();
  const [numOfColumns, setNumOfColumns] = useState();

  const [errMsg, setErrMsg] = useState("");
  const [success, setSuccess] = useState(false);

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const response = await axios.post(
        GENRE_UPLOAD_URL,
        { movieId, startTime, ticketPrice, numOfRows, numOfColumns },
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
      setErrMsg("Create Failed");
      errRef.current.focus();
    }
  };

  return (
    <>
      <div className="movieScreening-creation">
        <h1>Create movie screening</h1>
        {success ? (
          <section className="">
            <h1>Success!</h1>
            <p>
              MovieScreening has been created. <Link to={-1}></Link>
            </p>
          </section>
        ) : (
          <div className="container">
            <section>
              <form className="form">
                <p
                  ref={errRef}
                  className={errMsg ? "errmsg" : "offscreen"}
                  aria-live="assertive"
                >
                  {errMsg}
                </p>
                <label htmlFor="movieId">Movie:</label>
                <MovieScreeningSelector setMovieId={setMovieId} />
                <label htmlFor="startTime">Starting time:</label>
                <input
                  type="datetime-local"
                  id="startTime"
                  onChange={(e) => {
                    setStartTime(e.target.value);
                  }}
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
                  Create movie screening
                </button>
              </form>
            </section>
          </div>
        )}
      </div>
    </>
  );
};

export default CreateMovieScreeningAdmin;
