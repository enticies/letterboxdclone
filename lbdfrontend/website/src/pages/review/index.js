import React, { useEffect, useState } from "react";
import { useNavigate, useSearchParams } from "react-router-dom";
import Header from "../../components/header";
import Footer from "../../components/footer";
import "../../assets/styles/review.css";
import "react-tabs/style/react-tabs.css";
import ReactPaginate from "react-paginate";
import { nanoid } from "nanoid";
import { useContext } from "react";
import { UserContext } from "../../UserContext";

function Review() {
    const [movieID, setMovieID] = useState(null);
    const [review, setReview] = useState(null);
    const [searchParams, setSearchParams] = useSearchParams();
    const { user, setUser } = useContext(UserContext);
    const [commentBody, setCommentBody] = useState("");
    const [comments, setComments] = useState([]);

    useEffect(() => {
        fetch(
            `http://mackenzythorpe-001-site1.btempurl.com/api/reviews/getreview?reviewID=${searchParams.get(
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
                console.log(data);
                setReview(data);
            })
            .catch((error) => {
                console.error("Error:", error);
            });
        requestComments();
    }, []);
    const requestComments = () => {
        fetch(
            `http://mackenzythorpe-001-site1.btempurl.com/api/comments/getreviewcomments?reviewID=${searchParams.get(
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
                console.log(data);
                setComments(data);
            })
            .catch((error) => {
                console.error("Error:", error);
            });
    };
    const handleSubmit = (e) => {
        e.preventDefault();
        const data = {
            OwnerId: user.id,
            Body: commentBody,
            ReviewId: review.Id,
        };
        fetch(
            "http://mackenzythorpe-001-site1.btempurl.com/api/comments/create",
            {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json",
                    Authorization: "Bearer " + user.token,
                },
                body: JSON.stringify(data),
            }
        )
            .then((response) => {
                if (response.status == 201) {
                    requestComments();
                }
            })
            .catch((error) => {
                console.error("Error:", error);
            });
    };

    const handleDelete = (e) => {
        e.preventDefault();
        const id = e.target.getAttribute("data-id");
        fetch(
            `http://mackenzythorpe-001-site1.btempurl.com/api/comments/deletecomment?commentID=${id}`,
            {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    Authorization: "Bearer " + user.token,
                },
            }
        )
            .then((response) => {
                if (response.status == 200) {
                    requestComments();
                }
            })
            .catch((error) => {
                console.error("Error:", error);
            });
        requestComments();
    };

    return (
        <div className="mainContainer">
            <Header />
            <div className="reviewContainer">
                <img
                    src={
                        review &&
                        `http://mackenzythorpe-001-site1.btempurl.com/images/movies/posterimages/${review.Image}`
                    }
                />
                <div className="right">
                    <p className="review-owner">
                        {" "}
                        Review by {review && review.Username}{" "}
                        <ul
                            className="rating-score"
                            data-rating={`${review && review.Rating / 2}`}
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
                        </ul>{" "}
                    </p>
                    <p className="review-body"> {review && review.Body} </p>
                </div>
            </div>
            <div className="comments">
                <h1> Comments </h1>
                {comments.map((c) => (
                    <div className="comment">
                        <div className="left">
                            <img src={review && `http://mackenzythorpe-001-site1.btempurl.com/images/users/${review.OwnerImage}`}/>
                            <span>
                                {c.Username}{" "}
                                {c.Username == user.username && (
                                    <i
                                        data-id={c.Id}
                                        onClick={handleDelete}
                                        className="fa-solid fa-trash"
                                    ></i>
                                )}{" "}
                            </span>{" "}
                        </div>{" "}
                        <div className="comment-body"> {c.Body} </div>{" "}
                    </div>
                ))}
                <form onSubmit={handleSubmit} className="add-comment">
                    <textarea
                        value={commentBody}
                        onChange={(e) => setCommentBody(e.target.value)}
                        placeholder={`Add a comment as ${
                            user && user.username
                        }`}
                        className="add-comment"
                    ></textarea>
                    <button> POST </button>
                </form>
            </div>
            <Footer />
        </div>
    );
}

export default Review;
