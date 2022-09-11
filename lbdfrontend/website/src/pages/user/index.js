import Header from "../../components/header";
import Footer from "../../components/footer";
import { nanoid } from "nanoid";
import {
    createSearchParams,
    Link,
    useLocation,
    useNavigate,
    useParams,
} from "react-router-dom";
import defaultuser from "../../assets/images/users/defaultuser.jpg";
import "../../assets/styles/user.css";
import "react-tabs/style/react-tabs.css";
import { UserContext } from "../../UserContext";
import { useContext } from "react";
import { useEffect } from "react";
import { useState } from "react";
import UserTabs from "../../components/usertabs/UserTabs";

function User() {
    const { user, setUser } = useContext(UserContext);
    const userName = useParams().username;
    const [following, setFollowing] = useState(false);
    const [userStats, setUserStats] = useState(null);
    const [recentReviews, setRecentReviews] = useState([]);


    const navigate = useNavigate();
    useEffect(() => {
        console.log("hre");
        fetch(
            `http://mackenzythorpe-001-site1.btempurl.com/api/accounts/getuser?userName=${userName}`,
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
                setUserStats(data);
            })
            .catch((error) => {
                console.error("Error:", error);
            });
        fetch(
            `http://mackenzythorpe-001-site1.btempurl.com/api/reviews/getrecentreviews?userName=${userName}`,
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
                setRecentReviews(data);
            })
            .catch((error) => {
                console.error("Error:", error);
            });

             if (!user) {
            return;
        }
        if (user.username !== userName) {
            fetch(
                `http://mackenzythorpe-001-site1.btempurl.com/api/accounts/checkfollow?followerUsername=${
                    user && user.username
                }&followeeUsername=${userName}`,
                {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json",
                        Authorization: "Bearer " + user.token,
                    },
                }
            )
                .then((response) => {
                    return response.json();
                })
                .then((data) => {
                    setFollowing(data);
                })
                .catch((error) => {
                    console.error("Error:", error);
                });
        }
    }, [user, following, userName]);

    const handleFollow = (e) => {
        e.preventDefault();
        fetch(
            `http://mackenzythorpe-001-site1.btempurl.com/api/accounts/follow?followerUsername=${user.username}&followeeUsername=${userName}`,
            {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    Authorization: "Bearer " + user.token,
                },
            }
        )
            .then((response) => {
                return response.json();
            })
            .then((data) => {
                setFollowing(data);
            })
            .catch((error) => {
                console.error("Error:", error);
            });
    };

    const [ToggleState, setToggleState] = useState(1);

    const toggleTab = (index) => {
        setToggleState(index);
    };

    const getActiveClass = (index, className) =>
        ToggleState === index ? className : "";
    const handleReviewClick = (e) => {
        e.preventDefault();
        const id = e.target.getAttribute("data-id");
        const params = { id: e.target.getAttribute("data-id") };
        navigate({
            pathname: "/review",
            search: `${createSearchParams(params)}`,
        });
    };

    const handleEditProfile = (e) => {
        e.preventDefault();
        navigate(`/user/${userName}/settings`);
    };

    return (
        <div className="mainContainer">
            <Header />
            <div className="profile-container">
                <div className="first">
                    <div className="left">
                        <img
                            src={
                                userStats &&
                                `http://mackenzythorpe-001-site1.btempurl.com/images/users/${userStats.Image}`
                            }
                        />
                        <div>
                            <h1> {userName} </h1>
                            {user ? (
                                userName === user.username ? (
                                    <button onClick={handleEditProfile}>
                                        {" "}
                                        Edit Profile{" "}
                                    </button>
                                ) : following ? (
                                    <button
                                        className="following"
                                        onClick={handleFollow}
                                    >
                                        Following
                                    </button>
                                ) : (
                                    <button
                                        className="not-following"
                                        onClick={handleFollow}
                                    >
                                        Follow
                                    </button>
                                )
                            ) : null}
                        </div>
                    </div>
                    <div className="right">
                        <div className="stat">
                            <h2> {userStats && userStats.FilmCount} </h2>
                            <p> Films </p>
                        </div>
                        <div className="stat">
                            <h2> {userStats && userStats.ListCount} </h2>
                            <p> Lists </p>
                        </div>
                        <div className="stat">
                            <h2> {userStats && userStats.FollowerCount} </h2>
                            <p> Followers </p>
                        </div>
                        <div className="stat">
                            <h2> {userStats && userStats.FolloweeCount} </h2>
                            <p> Following </p>
                        </div>
                    </div>
                </div>
                <UserTabs />

                <div className="recent-activity">
                    {recentReviews.map((r) => {
                        return (
                            <div key={nanoid()} className="review-card">
                                <img
                                    data-id={r.Id}
                                    onClick={handleReviewClick}
                                    src={`http://mackenzythorpe-001-site1.btempurl.com/images/movies/posterimages/${r.Image}`}
                                />
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
                    })}
                </div>
            </div>
            <Footer />
        </div>
    );
}

export default User;
