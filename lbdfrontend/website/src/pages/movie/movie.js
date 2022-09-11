import React, { useEffect, useState } from "react";
import {
    createSearchParams,
    useNavigate,
    useSearchParams,
} from "react-router-dom";
import Header from "../../components/header";
import Footer from "../../components/footer";
import "../../assets/styles/movie.css";
import "react-tabs/style/react-tabs.css";
import ReactPaginate from "react-paginate";
import { nanoid } from "nanoid";

function Movie() {
    const [years, setYears] = useState([]);
    const [movie, setMovie] = useState(null);
    const [people, setPeople] = useState([]);
    const [genres, setGenres] = useState([]);
    const [reviews, setReviews] = useState([]);
    const [currentItems, setCurrentItems] = useState(null);
    const [pageCount, setPageCount] = useState(0);
    const [itemOffset, setItemOffset] = useState(0);
    const [searchParams, setSearchParams] = useSearchParams();

    const navigate = useNavigate();

    const itemsPerPage = 1;

    function Reviews({ currentReviews }) {
        const style = { "--rating": "2.5" };
        reviews.forEach((element) => {
            console.log(element.Rating);
        });
        return (
            <>
                {currentItems &&
                    currentItems.map((item) =>
                        item.Body ? (
                            <div
                                data-id={item.Id}
                                key={nanoid()}
                                className="review"
                            >
                                <div
                                    className="review-user"
                                    onClick={handleUserClick}
                                    data-id={item.Id}
                                >
                                    <img
                                        onClick={handleUserClick}
                                        data-id={item.Id}
                                        src={`http://mackenzythorpe-001-site1.btempurl.com/images/users/${item.OwnerImage}`}
                                    />
                                    <p>
                                        Review by <span> {item.Username} </span>
                                        <span className="comments">
                                            <i
                                                onClick={handleCommentClick}
                                                data-id={item.Id}
                                                className="fa-solid fa-comment"
                                            ></i>
                                            {item.CommentCount}
                                        </span>
                                        <ul
                                            className="rating-score"
                                            data-rating={`${item.Rating / 2}`}
                                        >
                                            <li className="rating-score-item"></li>
                                            <li className="rating-score-item"></li>
                                            <li className="rating-score-item"></li>
                                            <li className="rating-score-item"></li>
                                            <li className="rating-score-item"></li>
                                        </ul>
                                    </p>
                                </div>
                                <p className="review-body">{item.Body}</p>
                            </div>
                        ) : null
                    )}
            </>
        );
    }

    const handleUserClick = () => {};
    const handleCommentClick = (e) => {
        e.preventDefault();
        const params = { id: e.target.getAttribute("data-id") };
        navigate({
            pathname: "/review",
            search: `${createSearchParams(params)}`,
        });
    };

    useEffect(() => {
        fetch(
            `http://mackenzythorpe-001-site1.btempurl.com/api/movies/getbyid?id=${searchParams.get(
                "id"
            )}`,
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
                setMovie(data);
            })
            .catch((error) => {
                console.error("Error:", error);
            });
        fetch(
            `http://mackenzythorpe-001-site1.btempurl.com/api/people/getmoviepeople?id=${searchParams.get(
                "id"
            )}`,
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
                setPeople(data);
            })
            .catch((error) => {
                console.error("Error:", error);
            });
        fetch(
            `http://mackenzythorpe-001-site1.btempurl.com/api/movies/getmoviegenres?id=${searchParams.get(
                "id"
            )}`,
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
                setGenres(data);
            })
            .catch((error) => {
                console.error("Error:", error);
            });
        requestPages();
    }, []);

    const [ToggleState, setToggleState] = useState(1);

    const toggleTab = (index) => {
        setToggleState(index);
    };

    const getActiveClass = (index, className) =>
        ToggleState === index ? className : "";

    const handlePersonClick = (e) => {
        e.preventDefault();
        const id = e.target.getAttribute("data-personid");
        const params = { id: id};
        navigate({
            pathname: "/people",
            search: `${createSearchParams(params)}`,
        });
    };

    const requestPages = (index = 1) => {
        console.log(index);
        fetch(
            `http://mackenzythorpe-001-site1.btempurl.com/api/reviews/getmoviereviews?movieID=${searchParams.get(
                "id"
            )}&i=${index}`,
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
                console.log(data.Items);
                setCurrentItems(data.Items);
                setPageCount(data.TotalPage);
            })
            .catch((error) => {
                console.error("Error:", error);
            });
    };

    const handlePageClick = (event) => {
        requestPages(event.selected + 1);
        const newOffset = (event.selected * itemsPerPage) % currentItems.length;
        setItemOffset(newOffset);
    };

    return (
        <div className="mainContainer">
            <Header />
            <div className="movie-page-wrapper">
                <img
                    className="movie-poster"
                    src={
                        movie &&
                        `http://mackenzythorpe-001-site1.btempurl.com/images/movies/posterimages/${movie.PosterImage}`
                    }
                />
                <div className="inner-page-wrapper">
                    <div className="descriptionandbox">
                        <div className="description">
                            <h3>
                                {" "}
                                {movie && movie.Name} (
                                {movie && movie.YearNumber})
                            </h3>
                            <p> {movie && movie.Synopsis} </p>
                        </div>
                    </div>
                    <div className="movie-page-container">
                        <ul className="tab-list">
                            <li
                                className={`tabs ${getActiveClass(
                                    1,
                                    "active-tabs"
                                )}`}
                                onClick={() => toggleTab(1)}
                            >
                                Crew
                            </li>
                            <li
                                className={`tabs ${getActiveClass(
                                    2,
                                    "active-tabs"
                                )}`}
                                onClick={() => toggleTab(2)}
                            >
                                Genres
                            </li>
                        </ul>
                        <div className="content-container">
                            <div
                                className={`content ${getActiveClass(
                                    1,
                                    "active-content"
                                )}`}
                            >
                                {people.map((p) => (
                                    <a
                                        href="#"
                                        key={nanoid()}
                                        onClick={handlePersonClick}
                                        data-personid={p.ID}
                                    >
                                        {" "}
                                        {p.Name} ({p.ProfessionName}){" "}
                                    </a>
                                ))}
                            </div>
                            <div
                                className={`content ${getActiveClass(
                                    2,
                                    "active-content"
                                )}`}
                            >
                                {genres.map((p) => (
                                    <a className="deactive-genre"
                                        key={nanoid()}
                                    >
                                        {" "}
                                        {p.Name}{" "}
                                    </a>
                                ))}
                            </div>
                        </div>
                        <div className="reviews">
                            <h1 className="review-header"> Reviews </h1>
                            <Reviews currentItems={currentItems} />
                            <ReactPaginate
                                breakLabel="..."
                                nextLabel="Next"
                                onPageChange={handlePageClick}
                                pageRangeDisplayed={5}
                                pageCount={pageCount}
                                previousLabel="Previous"
                                renderOnZeroPageCount={null}
                                containerClassName="pagination-navigation"
                                pageLinkClassName="page-link"
                                previousLinkClassName="prev-link"
                                nextLinkClassName="next-link"
                                activeLinkClassName="active-link"
                            />
                        </div>
                    </div>
                </div>
            </div>
            <Footer />
        </div>
    );
}

export default Movie;
