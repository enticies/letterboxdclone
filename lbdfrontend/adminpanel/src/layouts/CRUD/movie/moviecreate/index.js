import DashboardLayout from "examples/LayoutContainers/DashboardLayout";
import DashboardNavbar from "examples/Navbars/DashboardNavbar";
import React, { useEffect, useState } from "react";
import { nanoid } from "nanoid";
import MultiSelect from "react-multiple-select-dropdown-lite";
import "react-multiple-select-dropdown-lite/dist/index.css";
import axios from "axios";

import { useSearchParams, useNavigate } from "react-router-dom";

// Data

function MovieCreate() {
  const [newName, setNewName] = useState("");
  const [newSynopsis, setNewSynopsis] = useState("");
  const [newYearID, setNewYearID] = useState(null);
  const [posterImage, setPosterImage] = useState(null);
  const [backgroundImage, setBackgroundImage] = useState(null);
  const [years, setYears] = useState([]);
  const [people, setPeople] = useState([]);
  const [selectedPeople, setSelectedPeople] = useState(null);
  const [professions, setProfessions] = useState([]);
  const [genres, setGenres] = useState([]);
  const [selectedGenres, setSelectedGenres] = useState([]);

  const token = localStorage.getItem("token");
  const myHeaders = new Headers();
  myHeaders.append('Authorization', "Bearer " + token);
  myHeaders.append("Content-Type", "application/json");

  const handleName = (e) => {
    setNewName(e.target.value);
  };

  const handleSynopsis = (e) => {
    setNewSynopsis(e.target.value);
  };

  const handleYear = (e) => {
    const id = years.find(y => y.YearNumber == e.target.value).ID;
    setNewYearID(id);
  };

  const [searchParams] = useSearchParams();
  const navigate = useNavigate(); 
  const id = searchParams.get("id");
  useEffect(() => {
    fetch("http://mackenzythorpe-001-site1.btempurl.com/api/admin/Movies/GetYears", {
    // fetch("http://localhost:64531/api/admin/Movies/GetYears", {
      headers: myHeaders,
    })
    .then((response) => response.json())
    .then((d) => {
        setYears(d);
        setNewYearID(d[0].id);
      });
    fetch("http://mackenzythorpe-001-site1.btempurl.com/api/admin/People/GetAll", {
    // fetch("http://localhost:64531/api/admin/People/GetAll", {
      headers: myHeaders,
    })
      .then((response) => response.json())
      .then((d) => {
        setPeople(d);
      });
    fetch("http://mackenzythorpe-001-site1.btempurl.com/api/admin/Professions/GetAll", {
    // fetch("http://localhost:64531/api/admin/Professions/GetAll", {
      headers: myHeaders,
    })
      .then((response) => response.json())
      .then((d) => {
          setProfessions(d);
      });
    fetch("http://mackenzythorpe-001-site1.btempurl.com/api/admin/Genres/GetAll", {
    // fetch("http://localhost:64531/api/admin/Genres/GetAll", {
      headers: myHeaders,
    })
      .then((response) => response.json())
      .then((d) => {
          setGenres(d);
      });
  }, []);

  const handleSubmit = (e) => {
    e.preventDefault();
    console.log("here0");
    if (newName.trim().length !== 0 && newSynopsis.trim().length !== 0) {
      const formData = new FormData();
      if (newYearID == -1) {
        setNewYearID(years[0].Id);
      }
      if (selectedPeople.trim().length === 0) {
        console.log("You have to pick people.");
        return;
      }
     if (selectedGenres.trim().length === 0) {
        console.log("You have to pick genres.");
        return;
      }
      console.log("here");

      const selectedGenreIDs = selectedGenres.split(",").map(p => genres.find(e => e.Name == p).ID);
      const selectedPeopleIDs = selectedPeople.split(",").map(p => people.find(e => e.Name == p).ID);


      for (const element of selectedGenreIDs) {
        formData.append("Genres", element);
      }
      for (const element of selectedPeopleIDs) {
        formData.append("People", element);
      }

      console.log(posterImage);
      console.log(backgroundImage);
      console.log(newName);
      console.log(newSynopsis);
      console.log(newYearID);
      console.log(selectedGenreIDs);
      console.log(selectedPeopleIDs);
      formData.append("PosterImage", posterImage);
      formData.append("BackgroundImage", backgroundImage);
      formData.append("Name", newName);
      formData.append("Synopsis", newSynopsis);
      formData.append("YearID", newYearID);
      const options = {
        method: "PUT",
        body: formData,
        headers: myHeaders,
      };

      axios({
        method: "put",
        url: "http://mackenzythorpe-001-site1.btempurl.com/api/admin/Movies/Create",
        // url: "http://localhost:64531/api/admin/Movies/Create",
        data: formData,
        headers: { "Content-Type": "multipart/form-data",
                   "Authorization": "Bearer " + token,
      },
      })
        .then(function (response) {
          navigate("/tables");
        })
        .catch(function (response) {
          console.log(response);
      });
    }
  };


	const  handlePeople =  val  => {
		setSelectedPeople(val)
	};
  	const peopleOptions = people.map(p => {
        const professionObj = professions.find(profession => profession.ID === p.ProfessionID);
        return {
            label: `${p.Name} (${professionObj !== undefined ? professionObj.Name : ""})`,
            value: p.Name
        }
    });

    const  handleGenres =  val  => {
        setSelectedGenres(val)
	};
  	const genreOptions = genres.map(p => {
        return {
            label: `${p.Name}`,
            value: p.Name
        }
    });


  return (
    <DashboardLayout>
      <form onSubmit={handleSubmit}>
        <div>
          <label value="Name" htmlFor="new-name"> Name </label> 
          <input id="new-name" onChange={handleName} value={newName}  placeholder="New name." /> 
        </div>
        <div>
            <label value="Synopsis" htmlFor="new-synopsis"> Synopsis </label> 
            <input id="new-synopsis" onChange={handleSynopsis} value={newSynopsis} />
        </div>
        <div>
            <div  className="preview-values">
                <h4>People</h4>
                {selectedPeople}
            </div>

            <MultiSelect
                onChange={handlePeople}
                options={peopleOptions}
            />
        </div>

        <div>
            <div  className="preview-values">
                <h4>Genres</h4>
                {selectedGenres}
            </div>

            <MultiSelect
                onChange={handleGenres}
                options={genreOptions}
            />
        </div>
        
        <div>
          <label> PosterImage 
              <input onChange={(event) => {
                setPosterImage(event.target.files[0]);
              }}
                type="file" name="posterImage" accept="image/jpeg" />
          </label>
        </div>
        <div>
        <label> BackgroundImage 
            <input onChange={(event) => {
                setBackgroundImage(event.target.files[0]);
              }}
                type="file" name="posterImage" accept="image/jpeg" />
          </label>
        </div>
        <div>
           <label htmlFor="years">Choose a year:</label>
            <select onChange={handleYear} value={"haha"} id="years" name="years">
              {years.map(p => <option key={nanoid()} data-id={p.id} value={p.YearNumber}> {p.YearNumber} </option>)}
            </select> 
            <div>
                <h1>
                    Movie made in {years.find(y => y.ID == newYearID) != undefined ? years.find(y => y.ID == newYearID).YearNumber : ""}
                </h1>
            </div>
          </div>
        <button> Create </button>
      </form>
    </DashboardLayout>
  );
}

export default MovieCreate;