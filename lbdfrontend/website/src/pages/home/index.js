import Header from "../../components/header";
import Footer from "../../components/footer";
import "../../assets/styles/home.css";
import { useContext, useEffect, useState } from "react";
import { UserContext } from "../../UserContext";
import { nanoid } from "nanoid";
import { createSearchParams, useNavigate } from "react-router-dom";

function Home() {
    const { user, setUser } = useContext(UserContext);
    const [home, setHome] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        fetch(
            `http://mackenzythorpe-001-site1.btempurl.com/api/home`,
            // `http://localhost:64531/api/home`,
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
                setHome(data);
            })
            .catch((error) => {
                console.error("Error:", error);
            });
    }, []);

    const handleReviewClick = (e) => {
        e.preventDefault();

        const id = e.target.getAttribute("data-id");
        const params = { id: id };
        navigate({
            pathname: "/review",
            search: `${createSearchParams(params)}`,
        });
    };

    const handleUserNameClick = (e) => {
        e.preventDefault();
        const username = e.target.getAttribute("data-username");
        navigate(`/user/${username}`);
    };

    const handleListClick = (e) => {
        e.preventDefault();
        const id = e.target.getAttribute("data-id");
        const username = e.target.getAttribute("data-username");

        const params = { id: id };
        navigate({
            pathname: `/user/${username}/list`,
            search: `${createSearchParams(params)}`,
        });        
    }

    const RecentLists = () => {
        return home && home.recentLists.map((l) => 
            <div className="recent-list" key={nanoid()}>
                <h1 data-username={l.OwnerUsername} data-id={l.Id} onClick={handleListClick}> {l.Name} </h1> 
                <p> List By <span data-username={l.OwnerUsername} onClick={handleUserNameClick}> {l.OwnerUsername} </span> </p> 
            </div>);
    };

    const RecentReviews = () => {
        return (
            home &&
            home.reviews.map((r) => {
                return (
                    <div key={nanoid()} className="home-review-card">
                        <div>
                            <img
                                data-id={r.Id}
                                onClick={handleReviewClick}
                                src={`http://mackenzythorpe-001-site1.btempurl.com/images/movies/posterimages/${r.Image}`}
                            />
                            <p className="review-by">
                                {" "}
                                Review By{" "}
                                <span
                                    onClick={handleUserNameClick}
                                    data-username={r.Username}
                                >
                                    {" "}
                                    {r.Username}{" "}
                                </span>{" "}
                            </p>
                        </div>
                        <ul
                            className="rating-score"
                            data-rating={`${r.Rating / 2}`}
                        >
                            <li
                                key={nanoid()}
                                className="rating-score-item"
                            ></li>
                            <li
                                key={nanoid()}
                                className="rating-score-item"
                            ></li>
                            <li
                                key={nanoid()}
                                className="rating-score-item"
                            ></li>
                            <li
                                key={nanoid()}
                                className="rating-score-item"
                            ></li>
                            <li
                                key={nanoid()}
                                className="rating-score-item"
                            ></li>
                        </ul>
                    </div>
                );
            })
        );
    };

    const handleLatestNewsClick = (e) => {
        e.preventDefault();

        const id = home.latestNews.Id;
        const params = { id: id };
        navigate({
            pathname: "/news",
            search: `${createSearchParams(params)}`,
        });
    };

    const handleNewsClick = (e) => {
        e.preventDefault();
        const id = e.target.getAttribute("data-id");
        const params = { id: id };
        navigate({
            pathname: "/news",
            search: `${createSearchParams(params)}`,
        });
    }

    const RecentNews = () => {
        return home && home.recentNews.map(n =>
                <div key={nanoid()} className="recent-news-single">
                    <img src={`http://mackenzythorpe-001-site1.btempurl.com/images/news/${n.Image}`} />
                    <h1 onClick={handleNewsClick} data-id={n.Id}> {n.Title} </h1>
                    <p> {n.Body} </p>
                </div>
            )
    }

    return (
        <div className="mainContainer">
            <Header />
            {/* <div className="home-container">
                <div className="recent-reviews">
                    <h1> Here are some recent reviews from our members... </h1>
                    <RecentReviews />
                </div>
                <div onClick={handleLatestNewsClick} className="latest-news">
                    <h3> Latest news </h3>
                    <div className="inner-container">
                        {home && (
                            <img
                                src={`http://mackenzythorpe-001-site1.btempurl.com/images/news/${home.latestNews.Image}`}
                            />
                        )}
                        <div className="inner-text">
                            <h1> {home && home.latestNews.Title} </h1>
                            <p> {home && home.latestNews.Body} </p>
                        </div>
                    </div>
                </div>
                <div className="recent-lists">
                    <h3> Recent lists from our members... </h3>
                    <RecentLists />
                </div>
                <div className="recent-news">
                    <h3> Recent stories from our members... </h3>
                    <RecentNews />
                </div>
            </div>
            <Footer /> */}
        </div>
    );
}

export default Home;
