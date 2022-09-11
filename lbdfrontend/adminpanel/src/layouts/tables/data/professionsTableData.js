import MDBox from "components/MDBox";
import MDTypography from "components/MDTypography";
import MDBadge from "components/MDBadge";
import { useEffect, useState } from "react";
import { Routes, Route, Navigate, useNavigate, createSearchParams } from "react-router-dom";
import { doc } from "prettier";

export default function data() {
  const Profession = ({ name }) => (
    <MDBox display="flex" alignItems="center" lineHeight={1}>
      <MDBox ml={2} lineHeight={1}>
        <MDTypography display="block" variant="button" fontWeight="medium">
          {name}
        </MDTypography>
      </MDBox>
    </MDBox>
  );

  const token = localStorage.getItem("token");
  const myHeaders = new Headers();
  myHeaders.append('Authorization', "Bearer " + token);
  myHeaders.append("Content-Type", "application/json")

  const [professions, setProfessions] = useState(null);

  useEffect(() => {
    const token = localStorage.getItem("token");
    if (token == null) {
      navigate("/authentication/sign-in");
    }
    placeProfessions(token);
  }, []);

  const placeProfessions = (token) => {
    fetch("http://mackenzythorpe-001-site1.btempurl.com/api/admin/Professions/GetAll", {
      headers: myHeaders,
    })
      .then((response) => response.json())
      .then((d) => setProfessions(d));
  };

  const navigate = useNavigate(); 
  const routeChange = (e) => { 
    const id = e.target.dataset.id;
    const params = { id: id };
    const path = `/CRUD/profession/professionupdate`;
    navigate({
      pathname: path,
      search: `?${createSearchParams(params)}`,
    });
  };

  const deleteOrRestore = (id) => {
    fetch(`http://mackenzythorpe-001-site1.btempurl.com/api/admin/Professions/DeleteOrRestore?id=${id}`, {
        method: "POST",
        headers: myHeaders,
    })
    .then((response) => {
          if (response.status == 200) {
            navigate("/tables");
          }
          console.log(response);
    })
    .then(() => {
      placeProfessions();
    })
  };

  const rows = [];

  if (professions) {
    professions.forEach((element) => {
      rows.push({
        genre: <Profession name={element.Name}/>,
        status: (
          <MDTypography onClick={() => deleteOrRestore(element.ID)} component="a" href="#" variant="caption" color="text" fontWeight="medium">
            <MDBadge badgeContent={element.IsDeleted ? "Restore" : "Delete"} color={!element.IsDeleted ? "error" : "success"} variant="gradient" size="sm" />
          </MDTypography>
        ),
        action: (
          <MDTypography onClick={routeChange} data-id={element.ID} component="a" className="edit" href="#" variant="caption" color="text" fontWeight="medium">
            Edit
          </MDTypography>
        ),
      });
    });
  }
  return {
    columns: [
      { Header: "genre", accessor: "genre", width: "45%", align: "left" },
      { Header: "status", accessor: "status", align: "center" },
      { Header: "action", accessor: "action", align: "center" },
    ],
    rows: rows
  };
}
