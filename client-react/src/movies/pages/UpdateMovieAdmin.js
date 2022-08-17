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

const MOVIE_UPLOAD_URL = "/api/admin/movies/";

const UpdateMovieAdmin = () => {
  const { id } = useParams();
  const errRef = useRef();
  const auth = useAuth();
  const [confirmedRequest, setConfirmedRequest] = useState(false);

  const [name, setName] = useState("");

  const [originalName, setOriginalName] = useState("");

  const [durationMinutes, setDurationMinutes] = useState(0);

  const [genreIdsToAdd, setGenreIdsToAdd] = useState([]);

  const [genreIdsToRemove, setGenreIdsToRemove] = useState([]);

  const [posterImage, setPosterImage] = useState();

  const [filterGenresArrayToAdd, setFilterGenresArrayToAdd] = useState([]);

  const [filterGenresArrayToRemove, setFilterGenresArrayToRemove] = useState(
    []
  );

  const onFileChange = (event) => {
    // Update the state
    setPosterImage(event.target.files[0]);
  };

  const [errMsg, setErrMsg] = useState("");
  const [success, setSuccess] = useState(false);

  const handleSubmit = async (e) => {
    e.preventDefault();
    setConfirmedRequest(false);
    try {
      var formData = new FormData();
      if (name) formData.append("Name", String(name));
      if (originalName) formData.append("OriginalName", String(originalName));
      if (durationMinutes)
        formData.append("DurationMinutes", String(durationMinutes));
      genreIdsToAdd.forEach((item) => {
        formData.append("GenreIdsToAdd", item);
      });
      genreIdsToRemove.forEach((item) => {
        formData.append("GenreIdsToAdd", item);
      });
      if (posterImage)
        formData.append("PosterImage", posterImage, posterImage.name);
      for (var pair of formData.entries()) {
        console.log(pair[0] + ", " + pair[1]);
      }

      // Update the formData object
      const response = await axios.put(MOVIE_UPLOAD_URL + id, formData, {
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${auth?.auth?.accessToken}`,
          withCredentials: false,
        },
      });
      setSuccess(true);
    } catch (err) {
      setErrMsg("Update Failed");
      errRef.current.focus();
    }
  };

  return (
    <>
      <h1>Update movie</h1>
      {success ? (
        <section>
          <h1>Success!</h1>
          <p>
            Movie has been updated. <Link to={-1}></Link>
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

              {/* email part */}
              <label htmlFor="originalName">Original name:</label>
              <input
                type="text"
                id="originalName"
                onChange={(e) => setOriginalName(e.target.value)}
              />
              <label htmlFor="duration">Duration:</label>
              <input
                type="text"
                id="duration"
                onChange={(e) => setDurationMinutes(e.target.value)}
              />
              <GenreFilterItem
                filterGenresArray={filterGenresArrayToAdd}
                setFilterGenresArray={setFilterGenresArrayToAdd}
              ></GenreFilterItem>
              <div>
                <h2>Genres to remove</h2>
                <GenreFilterItem
                  filterGenresArray={filterGenresArrayToRemove}
                  setFilterGenresArray={setFilterGenresArrayToRemove}
                ></GenreFilterItem>
              </div>

              <label htmlFor="posterImage">Duration:</label>
              <input id="posterImage" type="file" onChange={onFileChange} />

              <button
                className="btn"
                onClick={(e) => {
                  e.preventDefault();
                  setConfirmedRequest(true);
                }}
              >
                Update movie
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

export default UpdateMovieAdmin;
