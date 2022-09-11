import MDBox from "components/MDBox";
import MDTypography from "components/MDTypography";
import MDAvatar from "components/MDAvatar";
import MDBadge from "components/MDBadge";
import { useEffect, useState } from "react";
import { Routes, Route, Navigate, useNavigate, createSearchParams } from "react-router-dom";
import { doc } from "prettier";

export default function data() {
    const Person = ({ image, name }) => (
    <MDBox display="flex" alignItems="center" lineHeight={1}>
      <MDAvatar src={image} name={name} size="sm" />
      <MDBox ml={2} lineHeight={1}>
        <MDTypography display="block" variant="button" fontWeight="medium">
          {name}
        </MDTypography>
      </MDBox>
    </MDBox>
  );


  const [people, setPeople] = useState(null);
  const [professions, setProfessions] = useState([]);

  const token = localStorage.getItem("token");
  const myHeaders = new Headers();
  myHeaders.append('Authorization', "Bearer " + token);
  myHeaders.append("Content-Type", "application/json");

  useEffect(() => {
    placePeople();
    fetch(`http://mackenzythorpe-001-site1.btempurl.com/api/admin/Professions/GetAll`, {
      headers: myHeaders,
    })
      .then((response) => response.json())
      .then((d) => {
        setProfessions(d);
      });
  }, []);

  const placePeople = () => {
    fetch("http://mackenzythorpe-001-site1.btempurl.com/api/admin/People/GetAll", {
      headers: myHeaders,
    })
      .then((response) => response.json())
      .then((d) => setPeople(d));
  };

  const navigate = useNavigate();
  const routeChange = (e) => { 
    const id = e.target.dataset.id;
    const params = { id: id };
    const path = `/CRUD/person/personupdate`;
    navigate({
      pathname: path,
      search: `?${createSearchParams(params)}`,
    });
  };

  const deleteOrRestore = (id) => {
    const myHeaders = new Headers();
    myHeaders.append('Authorization', "Bearer " + token);
    fetch(`http://mackenzythorpe-001-site1.btempurl.com/api/admin/People/DeleteOrRestore?id=${id}`, {
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
        placePeople();
      });
  };
  const rows = [];
  if (people) {
    people.forEach((element) => {
      rows.push({
        person: <Person  image={`http://mackenzythorpe-001-site1.btempurl.com/images/people/${element.Image}`} name={element.Name} />,
        profession: (
          <MDTypography component="a" href="#" variant="caption" color="text" fontWeight="medium">
            {professions.find(p => p.ID == element.ProfessionID) && professions.find(p => p.ID == element.ProfessionID).Name}
          </MDTypography>
        ),
        status: (
          <MDTypography onClick={() => deleteOrRestore(element.ID)} component="a" href="#" variant="caption" color="text" fontWeight="medium">
            <MDBadge badgeContent={element.IsDeleted ? "Restore" : "Delete"} color={!element.IsDeleted ? "error" : "success"} variant="gradient" size="sm" />
          </MDTypography>
        ),
        action: (
          <MDTypography  onClick={routeChange} data-id={element.ID} component="a" href="#" variant="caption" color="text" fontWeight="medium">
            Edit
          </MDTypography>
        ),
      });
    });
  }

  return {
    columns: [
      { Header: "person", accessor: "person", width: "45%", align: "left" },
      { Header: "profession", accessor: "profession", width: "20%", align: "left" },
      { Header: "status", accessor: "status", align: "center" },
      { Header: "action", accessor: "action", align: "center" },
    ],
    rows: rows
  };
}
