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
import "../../assets/styles/userlists.css";
import "react-tabs/style/react-tabs.css";
import { UserContext } from "../../UserContext";
import { useContext } from "react";
import { useEffect } from "react";
import { useState } from "react";

function UserLists() {
    const { user, setUser } = useContext(UserContext);
    const [currentItems, setCurrentItems] = useState(null);
    const [pageCount, setPageCount] = useState(0);
    // Here we use item offsets; we could also use page offsets
    // following the API or data you're working with.
    const [itemOffset, setItemOffset] = useState(0);
    const userName = useParams().username;
    const itemsPerPage = 1;
    const navigate = useNavigate();

    function Items({ currentItems }) {
        return (
            <>
                {currentItems &&
                    currentItems.map((list) => {
                        return (
                            <div
                                onClick={handleListClick}
                                data-id={list.Id}
                                className="list"
                            >
                                {user && user.username == userName ? (
                                    <button
                                        data-id={list.Id}
                                        onClick={handleDelete}
                                        className="delete-news-button"
                                    >
                                        {" "}
                                        X{" "}
                                    </button>
                                ) : null}
                                <h1 data-id={list.Id} className="name">
                                    {" "}
                                    {list.Name}{" "}
                                </h1>
                                <h2 data-id={list.Id}>
                                    {" "}
                                    {list.MovieCount} movies{" "}
                                </h2>
                            </div>
                        );
                    })}
            </>
        );
    }

    const handleDelete = (e) => {
        e.preventDefault();
        e.stopPropagation();
        const id = e.target.getAttribute("data-id");
        fetch(
            `http://mackenzythorpe-001-site1.btempurl.com/api/lists?id=${id}`,
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

    const handleListClick = (e) => {
        e.preventDefault();
        const id = e.target.getAttribute("data-id");
        const params = { id: e.target.getAttribute("data-id") };
        navigate({
            pathname: `/user/${user.username}/list`,
            search: `${createSearchParams(params)}`,
        });
    };

    const requestPages = (index = 1) => {
        fetch(
            `http://mackenzythorpe-001-site1.btempurl.com/api/lists/getuserlists?userName=${userName}&i=${index}`,
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

    const handleNewListClick = (e) => {
        e.preventDefault();

        navigate(`/user/${user.username}/newlist`);
    };
    return (
        <div className="mainContainer">
            <Header />
            <div className="container">
                <UserTabs />
                <div className="lists-container">
                    <div className="lists">
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
                    <button onClick={handleNewListClick} className="new-list">
                        {" "}
                        Start a new list{" "}
                    </button>
                </div>
            </div>
            <Footer />
        </div>
    );
}

export default UserLists;
