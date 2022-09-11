/**
=========================================================
* Material Dashboard 2 React - v2.1.0
=========================================================

* Product Page: https://www.creative-tim.com/product/material-dashboard-react
* Copyright 2022 Creative Tim (https://www.creative-tim.com)

Coded by www.creative-tim.com

 =========================================================

* The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
*/

// import { useState } from "react";

// react-router-dom components
// import { Link } from "react-router-dom";

// @mui material components
import Card from "@mui/material/Card";
// import Switch from "@mui/material/Switch";
// import Grid from "@mui/material/Grid";
// import MuiLink from "@mui/material/Link";

import MDBox from "components/MDBox";
import MDInput from "components/MDInput";
import MDButton from "components/MDButton";
import BasicLayout from "layouts/authentication/components/BasicLayout";
import React, { useState } from "react";

// Images
import bgImage from "assets/images/bg-sign-in-basic.jpeg";
import { Navigate, useNavigate } from "react-router-dom";

function Basic() {
  const [emailOrUserName, setEmailOrUserName ] = useState("");
  const [password, setPassword ] = useState("");

  const navigate = useNavigate();

  const handleEmail = (e) => {
    setEmailOrUserName(e.target.value);
  };

  const handlePassword = (e) => {
    setPassword(e.target.value);
  };

  const handleSignIn = () => {
    const sendData = {
      "EmailOrUsername": emailOrUserName,
      "Password": password
    };
    fetch(`http://mackenzythorpe-001-site1.btempurl.com/api/admin/Accounts/Login`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(sendData),
    })
      .then((response) => {
        console.log(response);
        return response.json();
      })
      .then((data) => {
        localStorage.setItem("token", data.token);
        navigate("/tables");
      })
      .catch(() => {
        console.log("Login failed.");
      });
  };

  return (
    <BasicLayout image={bgImage}>
      <Card>
        <MDBox pt={4} pb={3} px={3}>
          <MDBox component="form" role="form">
            <MDBox mb={2}>
              <MDInput value={emailOrUserName} onChange={handleEmail} type="email" label="Email" fullWidth />
            </MDBox>
            <MDBox mb={2}>
              <MDInput value={password} onChange={handlePassword} type="password" label="Password" fullWidth />
            </MDBox>
            <MDBox mt={4} mb={1}>
              <MDButton onClick={handleSignIn} variant="gradient" color="info" fullWidth>
                sign in
              </MDButton>
            </MDBox>
          </MDBox>
        </MDBox>
      </Card>
    </BasicLayout>
  );
}

export default Basic;
