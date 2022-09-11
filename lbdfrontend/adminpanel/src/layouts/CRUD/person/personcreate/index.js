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
import React, { useEffect, useState } from "react";
import axios from "axios";

import { useLocation, useSearchParams, useNavigate} from "react-router-dom";

// Data

function PersonCreate() {
  const [newName, setNewName] = useState("");
  const [newDescription, setNewDescription] = useState("");
  const [newProfessionID, setNewProfessionID] = useState(-1);
  const [selectedImage, setSelectedImage] = useState(null);


  const [professions, setProfessions] = useState([]);

  const token = localStorage.getItem("token");
  const myHeaders = new Headers();
  myHeaders.append('Authorization', "Bearer " + token);
  myHeaders.append("Content-Type", "application/json");

  useEffect(() => {
    fetch("http://mackenzythorpe-001-site1.btempurl.com/api/admin/Professions/GetAll", {
      headers: myHeaders,
    })
      .then((response) => response.json())
      .then((d) => {
        console.log(d);
        setProfessions(d);
        setNewProfessionID(d[0].ID);
      });
  }, []);

  const handleName = (e) => {
    setNewName(e.target.value);
  };
  const handleDescription = (e) => {
    setNewDescription(e.target.value);
  };

  const handleSelect = (e) => {
    const id = professions.find(a => a.Name === e.target.value).ID;
    setNewProfessionID(id);
  };

  const [searchParams] = useSearchParams();
  const navigate = useNavigate(); 
  const id = searchParams.get("id");


  const handleSubmit = (e) => {
    e.preventDefault();
    if (newName.trim().length !== 0 && newDescription.trim().length !== 0) {
      const formData = new FormData();
      if (newProfessionID == -1) {
        setNewProfessionID(professions[0].ID);
      }

      formData.append("File", selectedImage);
      formData.append("Name", newName);
      formData.append("Description", newDescription);
      formData.append("ProfessionID", newProfessionID);
      const options = {
        method: "PUT",
        headers: myHeaders,
        body: formData,
      };
      axios({
        method: "put",
        url: "http://mackenzythorpe-001-site1.btempurl.com/api/admin/People/Create",
        data: formData,
        headers: { "Content-Type": "multipart/form-data" ,
                   "Authorization": "Bearer " + token,
      },
      })
        .then(function (response) {
          //handle success
          navigate("/tables");
          console.log(response);
        })
        .catch(function (response) {
        //handle error
        console.log(response);
      });

      console.log(professions);

      // fetch("http://localhost:64531/api/admin/People/Create", {
      //   method: "PUT",
      //   headers: myHeaders,
      //   body: formData,
      // });
    }
  };


  return (
    <DashboardLayout>
      <form onSubmit={handleSubmit}>
        <div>
          <label value="Name" htmlFor="new-name"> Name </label> 
          <input id="new-name" onChange={handleName} value={newName}  placeholder="New name." /> 
        </div>
        <div>
          <label value="Description" htmlFor="new-description"> Description </label> 
          <input id="new-description" onChange={handleDescription} value={newDescription} />
        </div>
        <div>
          <label> Image 
              <input onChange={(event) => {
                console.log(event.target.files[0]);
                setSelectedImage(event.target.files[0]);
              }}
                type="file" name="myImage" accept="image/jpeg" />
          </label>
        </div>
        <div>
           <label htmlFor="professions">Choose a profession:</label>
            <select onChange={handleSelect} value={newProfessionID} id="professions" name="professions">
              {professions.map(p => <option data-id={p.ID} value={p.Name}> {p.Name} </option>)}
            </select> 
            {(professions && professions.find(p => p.ID === newProfessionID)) && <h1> Profession: {professions.find(p => p.ID === newProfessionID).Name}</h1> }
          </div>
        <button> Create </button>
      </form>
    </DashboardLayout>
  );
}

export default PersonCreate;