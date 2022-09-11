import MDBox from "components/MDBox";
import MDTypography from "components/MDTypography";
import MDAvatar from "components/MDAvatar";
import MDBadge from "components/MDBadge";
import { useEffect, useState } from "react";
import { Routes, Route, Navigate, useNavigate, createSearchParams } from "react-router-dom";
import { doc } from "prettier";

export default function data() {
  const Movie = ({ image, name }) => (
    <MDBox display="flex" alignItems="center" lineHeight={1}>
      <MDAvatar src={image} name={name} size="sm" />
      <MDBox ml={2} lineHeight={1}>
        <MDTypography display="block" variant="button" fontWeight="medium">
          {name}
        </MDTypography>
      </MDBox>
    </MDBox>
  );

  const [movies, setMovies] = useState(null);

  const token = localStorage.getItem("token");
  const myHeaders = new Headers();
  myHeaders.append("Authorization", "Bearer " + token);
  myHeaders.append("Content-Type", "application/json");

  useEffect(() => {
    placeMovies();
  }, []);

  const placeMovies = () => {
    fetch(`http://mackenzythorpe-001-site1.btempurl.com/api/admin/Movies/GetAll`, {
      headers: myHeaders,
    })
      .then((response) => response.json())
      .then((d) => {
        setMovies(d);
      });
  };

  const navigate = useNavigate();
  const routeChange = (e) => {
    const id = e.target.dataset.id;
    const params = { id: id };
    const path = `/CRUD/movie/movieupdate`;
    navigate({
      pathname: path,
      search: `?${createSearchParams(params)}`,
    });
  };

  const deleteOrRestore = (id) => {
    fetch(
      `http://mackenzythorpe-001-site1.btempurl.com/api/admin/Movies/DeleteOrRestore?id=${id}`,
      {
        method: "POST",
        headers: myHeaders,
      }
    )
      .then((response) => {
        if (response.status == 204) {
          navigate("/tables");
        }
        console.log(response);
      })
      .then(() => {
        placeMovies();
      });
  };
  const rows = [];
  if (movies) {
    movies.forEach((element) => {
      rows.push({
        movie: (
          <Movie
            image={`http://mackenzythorpe-001-site1.btempurl.com/images/movies/posterimages/${element.PosterImage}`}
            name={element.Name}
          />
        ),

        status: (
          <MDTypography
            onClick={() => deleteOrRestore(element.ID)}
            component="a"
            href="#"
            variant="caption"
            color="text"
            fontWeight="medium"
          >
            <MDBadge
              badgeContent={element.IsDeleted ? "Restore" : "Delete"}
              color={!element.IsDeleted ? "error" : "success"}
              variant="gradient"
              size="sm"
            />
          </MDTypography>
        ),
        action: (
          <MDTypography
            onClick={routeChange}
            data-id={element.ID}
            component="a"
            href="#"
            variant="caption"
            color="text"
            fontWeight="medium"
          >
            Edit
          </MDTypography>
        ),
      });
    });
  }

  return {
    columns: [
      { Header: "movie", accessor: "movie", width: "45%", align: "left" },
      { Header: "status", accessor: "status", align: "center" },
      { Header: "action", accessor: "action", align: "center" },
    ],
    rows: rows,
  };
}
