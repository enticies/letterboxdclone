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
import "../../assets/styles/reviews.css";
import "react-tabs/style/react-tabs.css";
import { UserContext } from "../../UserContext";
import { useContext } from "react";
import { useEffect } from "react";
import { useState } from "react";

function Reviews() {
    const [currentItems, setCurrentItems] = useState(null);
    const [pageCount, setPageCount] = useState(0);
    // Here we use item offsets; we could also use page offsets
    // following the API or data you're working with.
    const [itemOffset, setItemOffset] = useState(0);
    const userName = useParams().username;
    const itemsPerPage = 1;
    const { user, setUser } = useContext(UserContext);

    const navigate = useNavigate();

    const handleDelete = (e) => {
        e.preventDefault();
        e.stopPropagation();

        const id = e.target.getAttribute("data-id");
        fetch(
            `http://mackenzythorpe-001-site1.btempurl.com/api/reviews/deletereview?id=${id}`,
            {
                method: "DELETE",
                headers: {
                    "Content-Type": "application/json",
                    Authorization: "Bearer " + user.token,
                },
            }
        )
            .then((response) => {
                console.log(response);
                if (response.status === 200) {
                    requestPages();
                }
            })
            .catch((error) => {
                console.error("Error:", error);
            });
    };

    function Items({ currentItems }) {
        return (
            <>
                {currentItems &&
                    currentItems.map((r) => {
                        return (
                            <div
                                data-id={r.Id}
                                onClick={handleReviewClick}
                                key={nanoid()}
                                className="review-card-small"
                            >
                                {user && user.username == userName ? (
                                    <button
                                        data-id={r.Id}
                                        onClick={handleDelete}
                                        className="delete-news-button"
                                    >
                                        {" "}
                                        X{" "}
                                    </button>
                                ) : null}
                                <img
                                    data-id={r.Id}
                                    src={`http://mackenzythorpe-001-site1.btempurl.com/images/movies/posterimages/${r.Image}`}
                                />
                                <div data-id={r.Id}>
                                    <h1 data-id={r.Id}> {r.MovieName} </h1>
                                    <p data-id={r.Id}> {r.Body} </p>
                                </div>
                            </div>
                        );
                    })}
            </>
        );
    }
    const requestPages = (index = 1) => {
        fetch(
            `http://mackenzythorpe-001-site1.btempurl.com/api/reviews/getalluserreviews?userName=${userName}&i=${index}`,
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
                setCurrentItems(data.Items);
                setPageCount(data.TotalPage);
            })
            .catch((error) => {
                console.error("Error:", error);
            });
    };
    useEffect(() => {
        requestPages();
    }, []);

    const handlePageClick = (event) => {
        requestPages(event.selected + 1);
        const newOffset = (event.selected * itemsPerPage) % currentItems.length;
        setItemOffset(newOffset);
    };

    const handleReviewClick = (e) => {
        e.preventDefault();
        const params = { id: e.target.getAttribute("data-id") };
        navigate({
            pathname: `/review`,
            search: `${createSearchParams(params)}`,
        });
    };

    return (
        <div className="mainContainer">
            <Header />
            <div className="container">
                <UserTabs />
                <div className="reviews">
                    <Items currentItems={currentItems} />
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
                <Footer />
            </div>
        </div>
    );
}

export default Reviews;
