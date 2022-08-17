import React, { useEffect, useState } from "react";
import ReactPaginate from "react-paginate";
import { Link } from "react-router-dom";
import axios from "../../api/axios";
import NameFilterItem from "../../shared/components/Filter/NameFilterItem";
import useAuth from "../../shared/hooks/auth-hook";
import UserAdminTableItem from "../components/UserAdminTableItem";
import "./AllUsersAdmin.css";

const USERS_ADMIN_URL = "/api/admin/users";

const AllUsersAdmin = () => {
  const [users, setUsers] = useState([]);
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

  const getUsers = async () => {
    try {
      var url = USERS_ADMIN_URL;
      url += `?PageNumber=${pageNumber}&PageSize=${itemsPerPage}`;
      if (filterCharsArray.length != 0) {
        url += `&${toQuery({
          FirstLetters: Array.from(filterCharsArray),
          Name: filterName,
        })}`;
      } else if (filterName.length != 0) {
        url += `&${toQuery({ Name: filterName })}`;
      }
      await axios
        .get(url, {
          headers: {
            Authorization: `Bearer ${auth?.accessToken}`,
          },
        })
        .then((response) => {
          //console.log(response);
          setUsers(response?.data?.data);
          setPageCount(response?.data?.totalPages);
        });
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
    getUsers();
    console.log(pageNumber);
  }, [filterCharsArray, filterName]);

  useEffect(() => {
    const endOffset = pageNumber + itemsPerPage;
    console.log(`Loading items from ${pageNumber} to ${endOffset}`);
    getUsers();
    console.log("got users");
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
      <div className="users">
        <h1>Users</h1>
        <div className="movie-list__container">
          <table>
            <thead>
              <tr>
                <th className="item-head">Username</th>
                <th className="item-head">Email</th>
                <th className="item-head">DateOfBirth</th>
                <th className="item-head">IsBlocked</th>
                <th className="item-head">EmailConfirmed</th>
                <th className="item-head">Roles</th>
                <th className="item-head">Block user</th>
                <th className="item-head">Reset password</th>
                <th className="item-head">Delete</th>
              </tr>
            </thead>
            <tbody>
              {users.map((item) => (
                <UserAdminTableItem
                  item={item}
                  key={item.id}
                  refresh={getUsers}
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

export default AllUsersAdmin;
