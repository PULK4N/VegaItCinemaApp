import { useRef, useState, useEffect } from "react";
import { Link, useNavigate, useSearchParams } from "react-router-dom";

import axios from "../../api/axios";
import "./Login.css";

const CONFIRM_EMAIL_URL = "/api/users/confirm";

const ResetPassword = () => {
  const [searchParams, setSearchParams] = useSearchParams();
  var token = String(searchParams.get("token"));
  const email = searchParams.get("email");
  const navigate = useNavigate();

  const errRef = useRef();
  const [errMsg, setErrMsg] = useState("");

  useEffect(() => {
    try {
      console.log(JSON.stringify({ email, token }));
      token = token.replace(/ /g, "+");
      const response = axios
        .post(CONFIRM_EMAIL_URL, JSON.stringify({ email, token }), {
          headers: { "Content-Type": "application/json" },
        })
        .then(console.log(JSON.stringify(response?.data)))
        .then(navigate("/"));
    } catch (err) {
      setErrMsg("Reset password Failed");
    }
  }, []);

  return (
    <section>
      <p>‎</p>
      <h1>{errRef ? "Success" : "Token expired"}</h1>
      <p>aa</p>
      <p>‎</p>
    </section>
  );
};

export default ResetPassword;
