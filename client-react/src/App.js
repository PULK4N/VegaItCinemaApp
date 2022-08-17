import logo from "./logo.svg";
import "./App.css";
import ConfirmRequest from "./shared/components/ConfirmRequest/ConfirmRequest";
import { Routes, BrowserRouter as Router, Route } from "react-router-dom";
import MainNavBar from "./shared/components/Navigation/MainNavBar";
import Register from "./authentication/pages/Register";
import Login from "./authentication/pages/Login";
import EmailResetPassword from "./authentication/pages/EmailResetPassword";
import ResetPassword from "./authentication/pages/ResetPassword";
import EmailConfirmLanding from "./authentication/pages/EmailConfirmLanding";
import AllMoviesAdmin from "./movies/pages/AllMoviesAdmin";
import AllMovies from "./movies/pages/AllMovies";
import MovieAdmin from "./movies/pages/MovieAdmin";
import UpdateMovieAdmin from "./movies/pages/UpdateMovieAdmin";
import CreateMovieAdmin from "./movies/pages/CreateMovieAdmin";
import AllUsersAdmin from "./users/pages/AllUsersAdmin";
import AllGenresAdmin from "./genres/pages/AllGenresAdmin";
import UpdateGenreAdmin from "./genres/pages/UpdateGenreAdmin";
import CreateGenreAdmin from "./genres/pages/CreateGenreAdmin";
import AllMovieScreeningsAdmin from "./movieScreenings/pages/AllMovieScreeningsAdmin";
import UpdateMovieScreeningAdmin from "./movieScreenings/pages/UpdateMovieScreeningAdmin";
import CreateMovieScreeningAdmin from "./movieScreenings/pages/CreateMovieScreeningAdmin";
import "./assets/boostrap/bootstrap.css";

const ROLES = {
  Customer: 2001,
  Admin: 5150,
};

function App() {
  return (
    <>
      <MainNavBar />
      <main className="App">
        <Routes path="/">
          <Route path="" element={<AllMovies />} />
          <Route
            path="admin/movies/update/:id"
            element={<UpdateMovieAdmin />}
          />
          <Route path="admin/movies/new" element={<CreateMovieAdmin />} />
          <Route path="admin/movies" element={<AllMoviesAdmin />} />
          <Route path="admin/movies/id/:id" element={<MovieAdmin />} />

          <Route
            path="admin/movieScreenings/update/:id"
            element={<UpdateMovieScreeningAdmin />}
          />
          <Route
            path="admin/movieScreenings/new"
            element={<CreateMovieScreeningAdmin />}
          />
          <Route
            path="admin/movieScreenings"
            element={<AllMovieScreeningsAdmin />}
          />

          <Route
            path="admin/genres/update/:id"
            element={<UpdateGenreAdmin />}
          />
          <Route path="admin/genres/new" element={<CreateGenreAdmin />} />
          <Route path="admin/genres" element={<AllGenresAdmin />} />
          <Route path="register/" exact="true" element={<Register />} />
          <Route path="login/" exact="true" element={<Login />} />
          <Route
            path="email-reset-password/"
            exact="true"
            element={<EmailResetPassword />}
          />
          <Route path="admin/users" element={<AllUsersAdmin />} />
          <Route
            path="reset-password/"
            exact="true"
            element={<ResetPassword />}
          />
          <Route
            path="confirm-email/"
            exact="true"
            element={<EmailConfirmLanding />}
          />
        </Routes>
      </main>
    </>
  );
}

export default App;
