import { CFormCheck } from "@coreui/react";
import React, { useEffect, useState } from "react";
import axios from "../../../api/axios";
import "./NameFilter.css";
import useAuth from "../../hooks/auth-hook";
const GENRES_URL = "/api/genres";

const GenreFilterItem = (props) => {
  const [genres, setGenres] = useState([]);
  const auth = useAuth();

  useEffect(() => {
    try {
      axios
        .get(GENRES_URL, {
          headers: {
            Authorization: `Bearer ${auth?.accessToken}`,
          },
        })
        .then((response) => setGenres(response.data.data));
    } catch (err) {
      console.log(err);
    }
  }, []);

  const handleButtonCheck = (event) => {
    var charsCopy = [...props.filterGenresArray];
    if (event.target.checked) {
      charsCopy.push(event.target.id);
    } else {
      charsCopy = charsCopy.filter((char) => char != event.target.id);
    }
    console.log(charsCopy);
    props.setFilterGenresArray(charsCopy);
  };

  return (
    <>
      <div id="name-filter">
        <div>
          <ul>
            {genres.map((item) => (
              <CFormCheck
                inline
                id={item.id}
                label={item.name}
                key={item.id}
                onChange={handleButtonCheck}
              />
            ))}
          </ul>
        </div>
      </div>
    </>
  );
};

export default GenreFilterItem;
