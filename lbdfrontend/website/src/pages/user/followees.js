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
import "../../assets/styles/userfollowers.css";
import "react-tabs/style/react-tabs.css";
import { UserContext } from "../../UserContext";
import { useContext } from "react";
import { useEffect } from "react";
import { useState } from "react";
import UserTabs from "../../components/usertabs/UserTabs";

function UserFollowees() {
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
        return (currentItems && currentItems.map(f => <div data-username={f.UserName} onClick={handleClick} key={nanoid()} className="follower">
                                            <img data-username={f.UserName} src={defaultuser} />
                                            <h1 data-username={f.UserName} > {f.UserName} </h1>
                                        </div>));
    }

    const requestPages = (index = 1) => {
        fetch(
            `http://mackenzythorpe-001-site1.btempurl.com/api/accounts/getuserfollowees?userName=${userName}&i=${index}`,
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
    const handleClick = (e) => {
        e.preventDefault();
        const username = e.target.getAttribute("data-username");
        navigate({
            pathname: `/user/${username}`,
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

    return (
        <div className="mainContainer">
            <Header />
            <div className="container">
                <UserTabs />
                <div className="followers">
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

export default UserFollowees;