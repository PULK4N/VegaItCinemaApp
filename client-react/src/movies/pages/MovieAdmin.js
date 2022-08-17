import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import MovieAdminItem from "../components/MovieAdminItem";
import axios from "../../api/axios";
import useAuth from "../../shared/hooks/auth-hook";

const MOVIES_ADMIN_URL = "/api/movies/";

const MovieAdmin = () => {
  const { auth } = useAuth;
  const { id } = useParams();
  const [loadedMovie, setLoadedMovie] = useState({});

  useEffect(() => {
    const fetchMovie = async () => {
      const response = await axios.get(MOVIES_ADMIN_URL + id, {
        headers: {
          Authorization: `Bearer ${auth?.accessToken}`,
        },
      });
      setLoadedMovie(response.data);
    };
    fetchMovie();
  }, []);

  return (
    <>
      <MovieAdminItem item={loadedMovie} key={id}></MovieAdminItem>
    </>
  );
};

export default MovieAdmin;
