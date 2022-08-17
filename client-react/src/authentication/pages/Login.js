import { useRef, useState, useEffect, useContext } from "react";
import useAuth from "../../shared/hooks/auth-hook";
import { Link, useNavigate, useLocation } from "react-router-dom";

import axios from "../../api/axios";
import "./Login.css";

const LOGIN_URL = "/api/users/login";

const Login = () => {
  const auth = useAuth();

  const navigate = useNavigate();

  const userRef = useRef();
  const errRef = useRef();

  const [user, setUser] = useState("");
  const [pwd, setPwd] = useState("");
  const [errMsg, setErrMsg] = useState("");

  const [showPassword, setShowPassword] = useState(false);

  useEffect(() => {
    userRef.current.focus();
  }, []);

  useEffect(() => {
    setErrMsg("");
  }, [user, pwd]);

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      console.log(auth);
      const response = await axios.post(
        LOGIN_URL,
        JSON.stringify({ emailOrUsername: user, password: pwd }),
        {
          headers: { "Content-Type": "application/json" },
          withCredentials: true,
        }
      );
      console.log(JSON.stringify(response?.data));
      const accessToken = response?.data?.token;
      const roles = response?.data?.roles;
      auth.setAuth({ user, pwd, roles, accessToken });
      setUser("");
      setPwd("");
      navigate("/");
    } catch (err) {
      setErrMsg("Login Failed");
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
      <h1>Sign In</h1>
      <form onSubmit={handleSubmit}>
        <label htmlFor="username">Username:</label>
        <input
          type="text"
          id="username"
          ref={userRef}
          autoComplete="off"
          onChange={(e) => setUser(e.target.value)}
          value={user}
          required
        />

        <label htmlFor="password">Password:</label>
        <input
          type={showPassword ? "text" : "password"}
          id="password"
          onChange={(e) => setPwd(e.target.value)}
          value={pwd}
          required
        />
        <div>
          <p id="show-password" className="alignright">
            Show password‎ ‎ ‎
            <input
              id="showPassword"
              type="checkbox"
              onClick={() =>
                showPassword ? setShowPassword(false) : setShowPassword(true)
              }
            />
          </p>
          <p id="forgot-password" className="alignleft link-forgot-password">
            <Link to="/email-reset-password">forgot password?</Link>
          </p>
        </div>
        <button className="aligncenter">Sign In</button>
      </form>
      <span>
        Need an Account? <Link to="/register">Sign Up</Link>
      </span>
    </section>
  );
};

export default Login;
