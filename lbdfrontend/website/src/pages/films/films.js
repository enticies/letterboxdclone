import Header from "../../components/header";
import Footer from "../../components/footer";
import "../../assets/styles/films.css";
import { useContext, useEffect, useState } from "react";
import { UserContext } from "../../UserContext";
import { nanoid } from "nanoid";
import { createSearchParams, useNavigate } from "react-router-dom";

function Films() {
    const [filmPage, setFilmPage] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        fetch(
            `http://mackenzythorpe-001-site1.btempurl.com/api/film`,
            // `http://localhost:64531/api/film`,
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
                setFilmPage(data);
            })
            .catch((error) => {
                console.error("Error:", error);
            });
    }, []);

    const RecentFilms = () => {
        return (
            filmPage &&
            filmPage.recentMovies.map((f) => (
                <div onClick={handleMovieClick} data-id={f.ID} key={nanoid()} className="recent-movie">
                    <img
                        data-id={f.ID}
                        src={`http://mackenzythorpe-001-site1.btempurl.com/images/movies/posterimages/${f.PosterImage}`}
                    />
                </div>
            ))
        );
    };

    const RecentReviews = () => {
        return (
            filmPage &&
            filmPage.recentReviews.map((r) => (
                <div key={nanoid()} className="film-recent-review">
                    <img
                        onClick={handleMovieClick}
                        data-id={r.MovieID}
                        className="review-movie-image"
                        src={`http://mackenzythorpe-001-site1.btempurl.com/images/movies/posterimages/${r.Image}`}
                    />
                    <div className="film-review-body">
                        <h1> {r.MovieName} </h1>
                        <div className="film-review-owner">
                            <img
                                onClick={handleUserClick}
                                data-username={r.Username}
                                src={`http://mackenzythorpe-001-site1.btempurl.com/images/users/${r.OwnerImage}`}
                            />
                            <p onClick={handleUserClick} data-username={r.Username}> {r.Username} </p>
                            <ul
                                className="rating-score"
                                data-rating={`${r.Rating / 2}`}
                            >
                                <li className="rating-score-item"></li>
                                <li className="rating-score-item"></li>
                                <li className="rating-score-item"></li>
                                <li className="rating-score-item"></li>
                                <li className="rating-score-item"></li>
                            </ul>
                        </div>
                        <div className="film-review-text">
                            <p>
                                {r.Body}
                            </p>
                        </div>
                    </div>
                </div>
            ))
        );
    };

    const handleUserClick = (e) => {
        e.preventDefault();
        const username = e.target.getAttribute("data-username");
        navigate(`/user/${username}`);
    }

    const handleMovieClick = (e) => {
        e.preventDefault();
        const params = { id:  e.target.getAttribute("data-id")};
        navigate({
            pathname: "/movie",
            search: `${createSearchParams(params)}`,
        });
    }



    return (
        <div className="mainContainer">
            <Header />
            <div className="films-container">
                <div className="recent-movies">
                    <h3> Recent movies </h3>
                    <RecentFilms />
                </div>
                <div className="recent-reviews-films">
                    <RecentReviews />
                </div>
            </div>
            <Footer />
        </div>
    );
}

export default Films;
