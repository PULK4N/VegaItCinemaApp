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

const GENRE_UPLOAD_URL = "/api/admin/genres/";

const CreateGenreAdmin = () => {
  const { id } = useParams();
  const errRef = useRef();
  const auth = useAuth();
  const navigate = useNavigate();

  const [name, setName] = useState("");

  const [errMsg, setErrMsg] = useState("");
  const [success, setSuccess] = useState(false);

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const response = await axios.post(
        GENRE_UPLOAD_URL,
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
      setErrMsg("Create Failed");
      errRef.current.focus();
    }
  };

  return (
    <>
      <h1>Create genre</h1>
      {success ? (
        <section>
          <h1>Success!</h1>
          <p>
            Genre has been created. <Link to={-1}></Link>
          </p>
        </section>
      ) : (
        <section>
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
            <button className="btn" onClick={handleSubmit}>
              Create genre
            </button>
          </form>
        </section>
      )}
    </>
  );
};

export default CreateGenreAdmin;
