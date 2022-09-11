import Dashboard from "layouts/dashboard";
import Tables from "layouts/tables";
import SignIn from "layouts/authentication/sign-in";
import GenreUpdate from "layouts/CRUD/genre/genreupdate";
import GenreCreate from "layouts/CRUD/genre/genrecreate";
import ProfessionCreate from "layouts/CRUD/profession/professioncreate";
import ProfessionUpdate from "layouts/CRUD/profession/professionupdate";

import PersonCreate from "layouts/CRUD/person/personcreate";
import PersonUpdate from "layouts/CRUD/person/personupdate";

import MovieCreate from "layouts/CRUD/movie/moviecreate";
import MovieUpdate from "layouts/CRUD/movie/movieupdate";

import Icon from "@mui/material/Icon";

const routes = [
  {
    type: "collapse",
    name: "Dashboard",
    key: "dashboard",
    icon: <Icon fontSize="small">dashboard</Icon>,
    route: "/dashboard",
    component: <Dashboard />,
  },
  {
    type: "collapse",
    name: "Tables",
    key: "tables",
    icon: <Icon fontSize="small">table_view</Icon>,
    route: "/tables",
    component: <Tables />,
  },
  {
    type: "collapse",
    name: "Sign In",
    key: "sign-in",
    icon: <Icon fontSize="small">login</Icon>,
    route: "/authentication/sign-in",
    component: <SignIn />,
  },
  {
    type: "collapse",
    name: "GenreUpdate",
    key: "genreupdate",
    icon: <Icon fontSize="small">login</Icon>,
    route: "/crud/genre/genreupdate/",
    component: <GenreUpdate />,
  },
  {
    type: "collapse",
    name: "GenreCreate",
    key: "genrecreate",
    icon: <Icon fontSize="small">login</Icon>,
    route: "/crud/genre/genrecreate/",
    component: <GenreCreate />,
  },
  {
    type: "collapse",
    name: "ProfessionCreate",
    key: "professioncreate",
    icon: <Icon fontSize="small">login</Icon>,
    route: "/crud/profession/professioncreate/",
    component: <ProfessionCreate />,
  },
  {
    type: "collapse",
    name: "ProfessionUpdate",
    key: "professionupdate",
    icon: <Icon fontSize="small">login</Icon>,
    route: "/crud/profession/professionupdate/",
    component: <ProfessionUpdate />,
  },
  {
    type: "collapse",
    name: "personcreate",
    key: "personcreate",
    icon: <Icon fontSize="small">login</Icon>,
    route: "/crud/person/personcreate/",
    component: <PersonCreate />,
  },
  {
    type: "collapse",
    name: "personupdate",
    key: "personupdate",
    icon: <Icon fontSize="small">login</Icon>,
    route: "/crud/person/personupdate/",
    component: <PersonUpdate />,
  },
  {
    type: "collapse",
    name: "moviecreate",
    key: "moviecreate",
    icon: <Icon fontSize="small">login</Icon>,
    route: "/crud/movie/moviecreate/",
    component: <MovieCreate />,
  },
  {
    type: "collapse",
    name: "movieupdate",
    key: "movieupdate",
    icon: <Icon fontSize="small">login</Icon>,
    route: "/crud/movie/movieupdate/",
    component: <MovieUpdate />,
  },
];

export default routes;
