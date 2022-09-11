import DashboardLayout from "examples/LayoutContainers/DashboardLayout";
import DashboardNavbar from "examples/Navbars/DashboardNavbar";
import React, { useState } from "react";

import { useLocation, useSearchParams, useNavigate} from "react-router-dom";

// Data

function GenreUpdate() {
  const [newName, setNewName] = useState("");

  const handleChange = (e) => {
    setNewName(e.target.value);
  };

  const [searchParams] = useSearchParams();
  const navigate = useNavigate();
  const id = searchParams.get("id");
  const token = localStorage.getItem("token");

  if (token == null) {
    navigate("authentication/sign-in");
  }

  const handleSubmit = (e) => {
    e.preventDefault();
    if (newName.trim().length !== 0) {
      const sendData = {
        "id": id,
        "name": newName,

      };
      const myHeaders = new Headers();
      myHeaders.append('Authorization', "Bearer " + token);
      myHeaders.append("Content-Type", "application/json");

      console.log(JSON.stringify(sendData));

      fetch(`http://mackenzythorpe-001-site1.btempurl.com/api/admin/Genres/Update?id=${id}`, {
        method: "POST",
        headers: myHeaders,
        body: JSON.stringify(sendData),
      })
        .then((response) => {
          if (response.status == 204) {
            navigate("/tables");
          }
          console.log(response);
        })
    }
  };
  return (
    <DashboardLayout>
      <form onSubmit={handleSubmit}>
        <label value="Name" htmlFor="new-name"> Name </label> 
        <input id="new-name" onChange={handleChange} value={newName}  placeholder="New name." /> 
        <button> Update </button>
      </form>
    </DashboardLayout>
  );
}

export default GenreUpdate;