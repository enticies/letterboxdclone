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
import DashboardLayout from "examples/LayoutContainers/DashboardLayout";
import DashboardNavbar from "examples/Navbars/DashboardNavbar";
import React, { useState } from "react";

import { useLocation, useSearchParams, useNavigate} from "react-router-dom";

// Data

function ProfessionCreate() {
  const [newName, setNewName] = useState("");

  const handleChange = (e) => {
    setNewName(e.target.value);
  };

  const [searchParams] = useSearchParams();
  const navigate = useNavigate(); 
  const id = searchParams.get("id");

  const token = localStorage.getItem("token");
  const myHeaders = new Headers();
  myHeaders.append('Authorization', "Bearer " + token);
  myHeaders.append("Content-Type", "application/json");

  const handleSubmit = (e) => {
    e.preventDefault();
    if (newName.trim().length !== 0) {
      const sendData = {
        "name": newName
      };

      console.log(JSON.stringify(sendData));

      fetch(`http://mackenzythorpe-001-site1.btempurl.com/api/admin/Professions/Create`, {
        method: "PUT",
        headers: myHeaders,
        body: JSON.stringify(sendData),
      })
        .then((response) => {
        if (response.status === 201) {
          console.log("here");
          navigate("/tables");
        }
      });
    }
  };
  return (
    <DashboardLayout>
      <form onSubmit={handleSubmit}>
        <label value="Name" htmlFor="new-name"> Name </label> 
        <input id="new-name" onChange={handleChange} value={newName}  placeholder="New name." /> 
        <button> Create </button>
      </form>
    </DashboardLayout>
  );
}

export default ProfessionCreate;