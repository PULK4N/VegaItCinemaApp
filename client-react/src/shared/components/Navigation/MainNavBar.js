import React, { useEffect, useState } from "react";
import "./MainNavBar.css";
import CinemaLogo from "../../../assets/cinema-logo.png";
import useAuth from "../../hooks/auth-hook";
import { Link, useNavigate, useLocation } from "react-router-dom";

const MainNavBar = () => {
  const auth = useAuth();
  const navigate = useNavigate();

  const signOutHandler = () => {
    auth.setAuth({});
    navigate("/");
  };

  return (
    <nav className="nav">
      <Link to="/">
        <img className="main-logo" src={CinemaLogo} />
      </Link>

      <ul>
        <li>
          <button className="btn" onClick={() => navigate("/")}>
            Home
          </button>
        </li>
        {auth?.auth?.roles?.includes("Admin") && (
          <li>
            <button className="btn" onClick={() => navigate("/admin/movies")}>
              Movies
            </button>
          </li>
        )}
        {auth?.auth?.roles?.includes("Admin") && (
          <li>
            <button
              className="btn"
              onClick={() => navigate("/admin/movieScreenings")}
            >
              Movie screenings
            </button>
          </li>
        )}
        {auth?.auth?.roles?.includes("Admin") && (
          <li>
            <button className="btn" onClick={() => navigate("/admin/genres")}>
              Genres
            </button>
          </li>
        )}

        {auth?.auth?.roles?.includes("Admin") && (
          <li>
            <button className="btn" onClick={() => navigate("/admin/users")}>
              Users
            </button>
          </li>
        )}
        {auth?.auth?.user != null && (
          <li>
            <button className="btn" onClick={signOutHandler}>
              Sign out
            </button>
          </li>
        )}
        {auth?.auth?.user == null && (
          <li>
            <button className="btn" onClick={() => navigate("/login")}>
              Sign in
            </button>
          </li>
        )}
      </ul>
    </nav>
  );
};

export default MainNavBar;
