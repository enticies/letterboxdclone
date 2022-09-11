import Header from "../../components/header";
import Footer from "../../components/footer";
import { nanoid } from "nanoid";
import ReactPaginate from "react-paginate";
import {
    createSearchParams,
    Link,
    useLocation,
    useNavigate,
    useParams,
} from "react-router-dom";
import defaultuser from "../../assets/images/users/defaultuser.jpg";
import "../../assets/styles/userfilms.css";
import "react-tabs/style/react-tabs.css";
import { UserContext } from "../../UserContext";
import { useContext } from "react";
import { useEffect } from "react";
import { useState } from "react";
import UserTabs from "../../components/usertabs/UserTabs";

function UserFilms() {
    // We start with an empty list of items.
    const [currentItems, setCurrentItems] = useState(null);
    const [pageCount, setPageCount] = useState(0);
    // Here we use item offsets; we could also use page offsets
    // following the API or data you're working with.
    const [itemOffset, setItemOffset] = useState(0);
    const [userReviews, setUserReviews] = useState([]);
    const userName = useParams().username;
    const itemsPerPage = 1;
    const navigate = useNavigate();
    function Items({ currentItems }) {
        return (
            <>
                {currentItems &&
                    currentItems.map((item) => (
                        <div
                            data-id={item.ID}
                            key={nanoid()}
                            className="film-card"
                        >
                            <img
                                onClick={handleClick}
                                data-id={item.MovieID}
                                src={`http://mackenzythorpe-001-site1.btempurl.com/images/movies/posterimages/${item.Image}`}
                            />
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
                        </div>
                    ))}
            </>
        );
    }
    const requestPages = (index = 1) => {
        fetch(
            `http://mackenzythorpe-001-site1.btempurl.com/api/reviews/getuserreviews?userName=${userName}&i=${index}`,
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

    const handleClick = (e) => {
        e.preventDefault();
        const params = { id: e.target.getAttribute("data-id") };
        navigate({
            pathname: "/movie",
            search: `${createSearchParams(params)}`,
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
            <div className="container">
                <UserTabs />
                <div className="films">
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
            </div>
            <Footer />
        </div>
    );
}

export default UserFilms;
