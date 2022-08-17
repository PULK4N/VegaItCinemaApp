import { useRef, useState, useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";

import axios from "../../api/axios";
import "./Login.css";

const EMAIL_RESET_PASSWORD_URL = "/api/users/get-reset-link";

const EmailResetPassword = () => {
  const navigate = useNavigate();

  const userRef = useRef();
  const errRef = useRef();

  const [user, setUser] = useState("");
  const [errMsg, setErrMsg] = useState("");

  useEffect(() => {
    userRef.current.focus();
  }, []);

  useEffect(() => {
    setErrMsg("");
  }, [user]);

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const response = await axios.post(
        EMAIL_RESET_PASSWORD_URL,
        JSON.stringify({ email: user }),
        {
          headers: { "Content-Type": "application/json" },
          withCredentials: true,
        }
      );
      console.log(JSON.stringify(response?.data));
      console.log("I was here 1");
      setUser("");
      navigate("/");
    } catch (err) {
      setErrMsg("Reset password Failed");
      errRef.current.focus();
    }
  };

  return (
    <section>
      <p
        ref={errRef}
        className={errMsg ? "errmsg" : "offscreen"}
        aria-live="assertive"
      >
        {errMsg}
      </p>
      <h1>Reset password</h1>
      <form onSubmit={handleSubmit}>
        <label htmlFor="email">Email:</label>
        <input
          type="text"
          id="email"
          ref={userRef}
          autoComplete="off"
          onChange={(e) => setUser(e.target.value)}
          value={user}
          required
        />
        <div>
          <p id="forgot-password" className="alignright link-forgot-password">
            <Link to="/login">back to login</Link>
          </p>
        </div>
        <button className="btn">Confirm</button>
      </form>
    </section>
  );
};

export default EmailResetPassword;
