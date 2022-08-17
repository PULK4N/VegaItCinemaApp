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

const MOVIE_UPLOAD_URL = "/api/admin/movies/";

const UpdateMovieAdmin = () => {
  const { id } = useParams();
  const errRef = useRef();
  const auth = useAuth();
  const navigate = useNavigate();

  const [name, setName] = useState("");

  const [originalName, setOriginalName] = useState("");

  const [durationMinutes, setDurationMinutes] = useState(0);

  const [genreIds, setGenreIds] = useState([]);

  const [posterImage, setPosterImage] = useState();

  const onFileChange = (event) => {
    // Update the state
    setPosterImage(event.target.files[0]);
  };

  const [errMsg, setErrMsg] = useState("");
  const [success, setSuccess] = useState(false);

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      var formData = new FormData();
      if (name) formData.append("Name", String(name));
      if (originalName) formData.append("OriginalName", String(originalName));
      if (durationMinutes)
        formData.append("DurationMinutes", String(durationMinutes));
      genreIds.forEach((item) => {
        formData.append("GenreIds", item);
      });
      if (posterImage)
        formData.append("PosterImage", posterImage, posterImage.name);
      for (var pair of formData.entries()) {
        console.log(pair[0] + ", " + pair[1]);
      }
      console.log(auth);
      const response = await axios.post(MOVIE_UPLOAD_URL, formData, {
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
      <h1>Create movie</h1>
      {success ? (
        <section>
          <h1>Success!</h1>
          <p>
            Movie has been created. <Link to={-1}></Link>
          </p>
        </section>
      ) : (
        <section>
          <div className="container">
            <form className="form-group">
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
                filterGenresArray={genreIds}
                setFilterGenresArray={setGenreIds}
              ></GenreFilterItem>

              <label htmlFor="posterImage">Duration:</label>
              <input
                class="form-control-file"
                id="posterImage"
                type="file"
                onChange={onFileChange}
              />

              <button className="btn" onClick={handleSubmit}>
                Create movie
              </button>
            </form>
          </div>
        </section>
      )}
    </>
  );
};

export default UpdateMovieAdmin;
