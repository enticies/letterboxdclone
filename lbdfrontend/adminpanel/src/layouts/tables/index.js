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

// @mui material components
import Grid from "@mui/material/Grid";
import Card from "@mui/material/Card";

// Material Dashboard 2 React components
import MDBox from "components/MDBox";
import MDTypography from "components/MDTypography";

// Material Dashboard 2 React example components
import DashboardLayout from "examples/LayoutContainers/DashboardLayout";
import Footer from "examples/Footer";
import DataTable from "examples/Tables/DataTable";

// Data
import authorsTableData from "layouts/tables/data/genresTableData";
import projectsTableData from "layouts/tables/data/professionsTableData";
import peopleTableData from "layouts/tables/data/peopleTableData";
import moviesTableData from "layouts/tables/data/moviesTableData";

import { useNavigate } from "react-router-dom";
import { useEffect } from "react";

function Tables() {
  const { columns, rows } = authorsTableData();
  const { columns: pColumns, rows: pRows } = projectsTableData();
  const { columns: peopleColumns, rows: peopleRows } = peopleTableData();
  const { columns: movieColumns, rows: movieRows } = moviesTableData();
  const navigate = useNavigate();

  useEffect(() => {
    const token = localStorage.getItem("token");
    if (token == null) {
      navigate("/authentication/sign-in");
    }
    const myHeaders = new Headers();
    myHeaders.append('Authorization', "Bearer " + token);
    fetch("http://mackenzythorpe-001-site1.btempurl.com/api/admin/Accounts/CheckToken", {
      headers: myHeaders,
    })
      .then((response) => {
        console.log(response);
      })
      .catch(() => {
        localStorage.removeItem("token");
        navigate("/authentication/sign-in");
      });
  }, []);

  return (
    <DashboardLayout>
      {/* <DashboardNavbar /> */}
      <MDBox pt={6} pb={3}>
        <Grid container spacing={6}>
          <Grid item xs={12}>
            <Card>
              <MDBox
                mx={2}
                mt={-3}
                py={3}
                px={2}
                variant="gradient"
                bgColor="info"
                borderRadius="lg"
                coloredShadow="info"
              >
                <MDTypography variant="h6" color="white">
                  Genres Table
                </MDTypography>
                <MDTypography onClick={() => navigate("/CRUD/genre/genrecreate")} component="a" className="edit" href="#" variant="caption" color="white" fontWeight="medium">
                  Create
                </MDTypography>
              </MDBox>
              <MDBox pt={3}>
                <DataTable
                  table={{ columns, rows }}
                  isSorted={false}
                  entriesPerPage={false}
                  showTotalEntries={false}
                  noEndBorder
                />
              </MDBox>
            </Card>
          </Grid>
          <Grid item xs={12}>
            <Card>
              <MDBox
                mx={2}
                mt={-3}
                py={3}
                px={2}
                variant="gradient"
                bgColor="info"
                borderRadius="lg"
                coloredShadow="info"
              >
                <MDTypography variant="h6" color="white">
                  Professions Table
                </MDTypography>
                <MDTypography onClick={() => navigate("/CRUD/profession/professioncreate")} component="a" className="edit" href="#" variant="caption" color="white" fontWeight="medium">
                  Create
                </MDTypography>
              </MDBox>
              <MDBox pt={3}>
                <DataTable
                  table={{ columns: pColumns, rows: pRows }}
                  isSorted={false}
                  entriesPerPage={false}
                  showTotalEntries={false}
                  noEndBorder
                />
              </MDBox>
            </Card>
          </Grid>
          <Grid item xs={12}>
            <Card>
              <MDBox
                mx={2}
                mt={-3}
                py={3}
                px={2}
                variant="gradient"
                bgColor="info"
                borderRadius="lg"
                coloredShadow="info"
              >
                <MDTypography variant="h6" color="white">
                  People Table
                </MDTypography>
                <MDTypography onClick={() => navigate("/CRUD/person/personcreate")} component="a" className="edit" href="#" variant="caption" color="white" fontWeight="medium">
                  Create
                </MDTypography>
              </MDBox>
              <MDBox pt={3}>
                <DataTable
                  table={{ columns: peopleColumns, rows: peopleRows }}
                  isSorted={false}
                  entriesPerPage={false}
                  showTotalEntries={false}
                  noEndBorder
                />
              </MDBox>
            </Card>
          </Grid>
          <Grid item xs={12}>
            <Card>
              <MDBox
                mx={2}
                mt={-3}
                py={3}
                px={2}
                variant="gradient"
                bgColor="info"
                borderRadius="lg"
                coloredShadow="info"
              >
                <MDTypography variant="h6" color="white">
                  Movies Table
                </MDTypography>
                <MDTypography onClick={() => navigate("/CRUD/movie/moviecreate")} component="a" className="edit" href="#" variant="caption" color="white" fontWeight="medium">
                  Create
                </MDTypography>
              </MDBox>
              <MDBox pt={3}>
                <DataTable
                  table={{ columns: movieColumns, rows: movieRows }}
                  isSorted={false}
                  entriesPerPage={false}
                  showTotalEntries={false}
                  noEndBorder
                />
              </MDBox>
            </Card>
          </Grid>
        </Grid>
      </MDBox>
      <Footer />
    </DashboardLayout>
  );
}

export default Tables;
