import React, { useEffect, useState } from "react";
import { createSearchParams, useNavigate, useSearchParams } from "react-router-dom";
import Header from "../../components/header";
import Footer from "../../components/footer";
import "../../assets/styles/people.css";
import "react-tabs/style/react-tabs.css";
import ReactPaginate from "react-paginate";
import { nanoid } from "nanoid";
import { useContext } from "react";
import { UserContext } from "../../UserContext";

function People() {
    const [personPage, setPersonPage] = useState(null);
    const [searchParams, setSearchParams] = useSearchParams();
    const navigate = useNavigate();

    useEffect(() => {
        fetch(
            `http://mackenzythorpe-001-site1.btempurl.com/api/people/getpersonpage?id=${searchParams.get("id")}`,
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
                setPersonPage(data);
            })
            .catch((error) => {
                console.error("Error:", error);
            });
    }, []);

    const handleMovieClick = (e) => {
        e.preventDefault();
        const id = e.target.getAttribute("data-id");
        const params = { id: id};
        navigate({
            pathname: "/movie",
            search: `${createSearchParams(params)}`,
        });
    }

    return (
        <div className="mainContainer">
            <Header />
            <div className="people-container">
                <div className="person-movies">
                    <h1 className="person-hero-title"> Movies that <span> {personPage && personPage.person.Name} </span> is involved with... </h1>

                    {personPage && personPage.movies.map(m => {
                    return (
                        <div onClick={handleMovieClick} data-id={m.ID} key={nanoid()} className="person-movie">
                            <img data-id={m.ID} src={`http://mackenzythorpe-001-site1.btempurl.com/images/movies/posterimages/${m.PosterImage}`} />
                        </div>
                        )})
                    }
                </div>
                <div className="person-info">
                    <img src={personPage && `http://mackenzythorpe-001-site1.btempurl.com/images/people/${personPage.person.Image}`} />
                    <p> {personPage && personPage.person.Description} </p>
                </div>
            </div>
            <Footer />
        </div>
    );
}

export default People;
