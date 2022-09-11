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
import "../../assets/styles/usernews.css";
import "react-tabs/style/react-tabs.css";
import { UserContext } from "../../UserContext";
import { useContext } from "react";
import { useEffect } from "react";
import { useState } from "react";
import UserTabs from "../../components/usertabs/UserTabs";

function UserNews() {
    // We start with an empty list of items.
    const [currentItems, setCurrentItems] = useState(null);
    const [pageCount, setPageCount] = useState(0);
    // Here we use item offsets; we could also use page offsets
    // following the API or data you're working with.
    const [itemOffset, setItemOffset] = useState(0);
    const [userNews, setUserNews] = useState([]);
    const userName = useParams().username;
    const itemsPerPage = 1;
    const navigate = useNavigate();
    const { user, setUser } = useContext(UserContext);


    const handleDelete = (e) => {
        e.preventDefault();
        e.stopPropagation();
        const id = e.target.getAttribute("data-id");
        fetch(`http://mackenzythorpe-001-site1.btempurl.com/api/news/deletenews?id=${id}`, {
            method: "DELETE",
            headers: {
                "Content-Type": "application/json",
                "Authorization": "Bearer " + user.token,
            },
        })
            .then((response) => {
                console.log(response);
                if (response.status === 200) {
                    requestPages();
                }
            })
            .catch((error) => {
                console.error("Error:", error);
            });
    }

    function Items({ currentItems }) {
        return (
            currentItems && currentItems.map(n => (
                <div onClick={handleClick} key={nanoid()}  className="news-article">
                    {user && user.username == userName ? <button data-id={n.Id} onClick={handleDelete} className="delete-news-button"> X </button> : null}
                    <img data-id={n.Id} src={`http://mackenzythorpe-001-site1.btempurl.com/images/news/${n.Image}`} />
                    <h1 data-id={n.Id} > {n.Title} </h1>
                    <p data-id={n.Id}> {n.Body} </p>
                </div>
            ))
        );
    }
    const requestPages = (index = 1) => {
        fetch(
            `http://mackenzythorpe-001-site1.btempurl.com/api/news/getusernews?userName=${userName}&i=${index}`,
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
            pathname: "/news",
            search: `${createSearchParams(params)}`,
        });
    };
    const handlePageClick = (event) => {
        requestPages(event.selected + 1);
        const newOffset = (event.selected * itemsPerPage) % currentItems.length;
        setItemOffset(newOffset);
    };

    const handleNewNews = (e) => {
        e.preventDefault();
        navigate(`/user/${user.userName}/news/new`);
    }

    return (
        <div className="mainContainer">
            <Header />
            <div className="container">
                <UserTabs />
                <div className="news-container">
                    <div className="news">
                        <Items currentItems={currentItems} />
                    </div>
                    <button onClick={handleNewNews} className="create-news"> New Article </button>
                </div>
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
    );
}

export default UserNews;
