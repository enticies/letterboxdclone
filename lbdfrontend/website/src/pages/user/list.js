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
    useSearchParams,
} from "react-router-dom";
import defaultuser from "../../assets/images/users/defaultuser.jpg";
import "../../assets/styles/list.css";
import "react-tabs/style/react-tabs.css";
import { UserContext } from "../../UserContext";
import { useContext } from "react";
import { useEffect } from "react";
import { useState } from "react";

function List() {
    const [addedMovies, setAddedMovies] = useState([]);
    const { user, setUser } = useContext(UserContext);
    const [searchParams, setSearchParams] = useSearchParams();
    const navigate = useNavigate();
    const id = searchParams.get("id");

    useEffect(() => {
        fetch(
            `http://mackenzythorpe-001-site1.btempurl.com/api/lists/getlistmovies?id=${id}`,
            {
                method: "GET",
                headers: {
                    "Content-Type": "application/json",
                },
            }
        )
            .then((response) => {
                return response.json();
            })
            .then((data) => {
                console.log(data);
                setAddedMovies(data);
            })
            .catch((error) => {
                console.error("Error:", error);
            });
    }, []);

    const handleMovieClick = (e) => {
        e.preventDefault();
        const id = e.target.getAttribute("data-id");
        const params = { id:  id};
        navigate({
            pathname: `/movie`,
            search: `${createSearchParams(params)}`,
        });

    };

    return (
        <div className="mainContainer">
            <Header />
            <div className="container">
                <UserTabs />
                <div className="list-container">
                    <h1 className="new-list"> {} </h1>

                    <div className="added-movies">
                        {addedMovies.length !== 0
                            ? addedMovies.map((m) => (
                                  <div
                                      data-id={m.ID}
                                      key={nanoid()}
                                      className="list-movie"
                                      onClick={handleMovieClick}
                                  >
                                      <img
                                        data-id={m.ID}
                                          src={`http://mackenzythorpe-001-site1.btempurl.com/images/movies/posterimages/${m.PosterImage}`}
                                      />
                                      <h1 data-id={m.ID}>
                                          {" "}
                                          {m.Name} 
                                      </h1>
                                  </div>
                              ))
                            : null}
                    </div>
                </div>
                <Footer />
            </div>
        </div>
    );
}


export default List;
