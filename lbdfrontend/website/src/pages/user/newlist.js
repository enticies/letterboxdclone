import Header from "../../components/header";
import Footer from "../../components/footer";
import { nanoid } from "nanoid";
import ReactPaginate from "react-paginate";
import UserTabs from "../../components/usertabs/UserTabs";
import {
    createSearchParams,
    Link,
    useLocation,
    useNavigate,
    useParams,
} from "react-router-dom";
import defaultuser from "../../assets/images/users/defaultuser.jpg";
import "../../assets/styles/newlist.css";
import "react-tabs/style/react-tabs.css";
import { UserContext } from "../../UserContext";
import { useContext } from "react";
import { useEffect } from "react";
import { useState } from "react";

function NewList() {
    const [name, setName] = useState("");
    const [search, setSearch] = useState("");
    const [addedMovies, setAddedMovies] = useState([]);
    const [foundMovies, setFoundMovies] = useState([]);
     const { user, setUser } = useContext(UserContext);

    const navigate = useNavigate();

    useEffect(() => {
        if (search.trim().length !== 0) {
            fetch(`http://mackenzythorpe-001-site1.btempurl.com/api/movies/findmovie?str=${search}`, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
            })
                .then((response) => {
                    return response.json();
                })
                .then((data) => {
                    console.log(data);
                    setFoundMovies(data);
                })
                .catch((error) => {
                    console.error("Error:", error);
                });
        } else {
            setFoundMovies([]);
        }
    }, [search]);

    const handleMovieClick = (e) => {
        e.preventDefault();
        const id = e.target.getAttribute("data-id");
        if (addedMovies.some((m) => m.ID == id)) {
            return;
        }
        setAddedMovies((prevState) => {
            return [...prevState, foundMovies.find((m) => m.ID == id)];
        });
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        if (name.trim().length === 0 && addedMovies.length === 0) {
            return;
        }
        console.log(user.username);
        const body = {
            Name: name,
            OwnerUsername: user.username,
            movies: addedMovies.map(m => m.ID)
        }
        fetch("http://mackenzythorpe-001-site1.btempurl.com/api/lists/createlist", {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
                "Authorization": "Bearer " + user.token,
            },
            body: JSON.stringify(body),
        })
            .then((response) => {
                if (response.status === 201) {
                    navigate(`/user/${user.username}/lists`)
                }
            })
            .catch((error) => {
                console.error("Error:", error);
            });
    };

    const handleDelete = (e) => {
        e.preventDefault();
        const id = e.target.parentNode.parentNode.getAttribute("data-id");

        setAddedMovies(prevState => prevState.filter(m => m.ID != id));
    }

    return (
        <div className="mainContainer">
            <Header />
            <div className="container">
                <UserTabs />
                <div className="list-container">
                    <h1 className="new-list"> New List </h1>
                    <div className="add-film">
                        <p> Add a film </p>
                        <input
                            onChange={(e) => setSearch(e.target.value)}
                            value={search || ""}
                            type="text"
                            placeholder="Enter a movie name"
                        />
                        {foundMovies.length !== 0 ? (
                            <div className="found-movies">
                                {foundMovies.map((m) => (
                                    <button
                                        onClick={handleMovieClick}
                                        data-id={m.ID}
                                        key={nanoid()}
                                        className="movie"
                                    >
                                        {" "}
                                        {m.Name} {m.YearNumber}{" "}
                                    </button>
                                ))}
                            </div>
                        ) : null}
                    </div>

                    <div className="added-movies">
                        {addedMovies.length !== 0
                            ? addedMovies.map((m) => (
                                  <div data-id={m.ID} key={nanoid()} className="list-movie">
                                      <img
                                          src={`http://mackenzythorpe-001-site1.btempurl.com/images/movies/posterimages/${m.PosterImage}`}
                                      />
                                      <h1>
                                          {" "}
                                          {m.Name} {m.YearNumber}{" "}
                                      </h1>
                                      <button className="delete-button"> <i onClick={handleDelete} className="fa-solid fa-x"></i> </button>
                                  </div>
                              ))
                            : null}
                    </div>
                    <form onSubmit={handleSubmit}>
                        <input
                            onChange={(e) => setName(e.target.value)}
                            value={name}
                            type="text"
                            placeholder="Enter a list name"
                        />
                        <button className="save-list-button"> Save </button>
                    </form>
                </div>
                <Footer />
            </div>
        </div>
    );
}

export default NewList;
