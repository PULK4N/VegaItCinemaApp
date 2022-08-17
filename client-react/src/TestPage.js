import { useRef, useState, useEffect, useContext } from "react";
import useAuth from "./shared/hooks/auth-hook";
import { Link, useNavigate, useLocation } from "react-router-dom";

const LOGIN_URL = "/api/users/login";

const TestPage = () => {
  return (
    <section>
      <div>
        <h1>AAAAAAAA TEST</h1>
      </div>
    </section>
  );
};

export default TestPage;
