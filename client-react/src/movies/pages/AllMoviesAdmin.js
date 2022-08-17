import React, { useEffect, useState } from "react";
import ReactPaginate from "react-paginate";
import { Link } from "react-router-dom";
import axios from "../../api/axios";
import NameFilterItem from "../../shared/components/Filter/NameFilterItem";
import useAuth from "../../shared/hooks/auth-hook";
import MovieAdminTableItem from "../components/MovieAdminTableItem";
import "./AllMoviesAdmin.css";

const MOVIES_ADMIN_URL = "/api/admin/movies";

const AllMoviesAdmin = () => {
  const [movies, setMovies] = useState([]);
  const [filterName, setFilterName] = useState("");
  const { auth } = useAuth();
  const [filterCharsArray, setFilterCharsArray] = useState([]);

  const [pageCount, setPageCount] = useState(1);
  const [pageNumber, setPageNumber] = useState(1);
  const itemsPerPage = 10;

  const toQuery = (obj) => {
    var str = [];
    for (var p in obj) {
      if (Array.isArray(obj[p])) {
        if (obj[p].length != 0)
          for (let i = 0; i < obj[p].length; i++) {
            console.log(
              encodeURIComponent(p) + "=" + encodeURIComponent(obj[p][i])
            );
            str.push(
              encodeURIComponent(p) + "=" + encodeURIComponent(obj[p][i])
            );
          }
      } else if (obj.hasOwnProperty(p)) {
        if (obj[p].length != 0) {
          console.log(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
          str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
        }
      }
    }
    return str.join("&");
  };

  const getMovies = async () => {
    try {
      var url = MOVIES_ADMIN_URL;
      url += `?PageNumber=${pageNumber}&PageSize=${itemsPerPage}`;
      if (filterCharsArray.length != 0) {
        url += `&${toQuery({
          FirstLetters: Array.from(filterCharsArray),
          Name: filterName,
        })}`;
      } else if (filterName.length != 0) {
        url += `&${toQuery({ Name: filterName })}`;
      }
      console.log(url);
      const response = await axios.get(url, {
        headers: {
          Authorization: `Bearer ${auth?.accessToken}`,
        },
      });
      const resMovies = response?.data?.data;
      setMovies(resMovies);
      setPageCount(response?.data?.totalPages);
      console.log(response);
    } catch (err) {
      console.log(err);
    }
  };

  const handlePageClick = (event) => {
    const newOffset = event.selected + 1;
    console.log(
      `User requested page number ${event.selected}, which is offset ${newOffset}`
    );
    setPageNumber(newOffset);
  };

  useEffect(() => {
    getMovies();
    console.log(pageNumber);
  }, [filterCharsArray, filterName]);

  useEffect(() => {
    const endOffset = pageNumber + itemsPerPage;
    console.log(`Loading items from ${pageNumber} to ${endOffset}`);
    getMovies();
    console.log("got movies");
  }, [pageNumber]);
  return (
    <>
      <div className="container">
        <NameFilterItem
          filterCharsArray={filterCharsArray}
          setFilterCharsArray={setFilterCharsArray}
          setFilterName={setFilterName}
        ></NameFilterItem>
      </div>
      <Link to={`new`}>
        <button className="btn">New movie</button>
      </Link>
      <div className="movies">
        <h1>Movies</h1>
        <div className="movie-list__container">
          <table>
            <thead>
              <tr>
                <th className="item-head">Name</th>
                <th className="item-head">Original Name</th>
                <th className="item-head">Duration Minutes</th>
                <th className="item-head">Rating</th>
                <th className="item-head">View</th>
                <th className="item-head">Update</th>
                <th className="item-head">Delete</th>
              </tr>
            </thead>
            <tbody>
              {movies.map((item) => (
                <MovieAdminTableItem
                  item={item}
                  key={item.id}
                  refresh={getMovies}
                />
              ))}
            </tbody>
          </table>
        </div>
        <div className="page-items">
          <ReactPaginate
            breakLabel="..."
            nextLabel="next >"
            onPageChange={handlePageClick}
            pageRangeDisplayed={5}
            pageCount={pageCount}
            previousLabel="< previous"
            renderOnZeroPageCount={null}
            itemClass="page-item"
            linkClass="page-link"
          />
        </div>
      </div>
    </>
  );
};

export default AllMoviesAdmin;
