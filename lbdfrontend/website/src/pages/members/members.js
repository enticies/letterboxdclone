import Header from "../../components/header";
import Footer from "../../components/footer";
import "../../assets/styles/members.css";
import { useContext, useEffect, useState } from "react";
import { UserContext } from "../../UserContext";
import { nanoid } from "nanoid";
import { createSearchParams, Navigate, useNavigate } from "react-router-dom";
import ReactPaginate from "react-paginate";

function Members() {
    const [membersPage, setMembersPage] = useState(null);
    const [currentItems, setCurrentItems] = useState(null);
    const [pageCount, setPageCount] = useState(0);
    // Here we use item offsets; we could also use page offsets
    // following the API or data you're working with.
    const [itemOffset, setItemOffset] = useState(0);

    const itemsPerPage = 1;

    const navigate = useNavigate();

    function Items({ currentItems }) {
        return (
            currentItems &&
            currentItems.map((item) => (
                <div key={nanoid()} className="member">
                    <img
                        onClick={handleUserClick}
                        data-username={item.UserName}
                        src={`http://mackenzythorpe-001-site1.btempurl.com/images/users/${item.Image}`}
                    />
                    <div 
                        className="member-body">
                        <h1
                            onClick={handleUserClick}
                            data-username={item.UserName}
                        > {item.UserName} </h1>
                        <p> {item.ReviewCount} reviews </p>
                    </div>
                </div>
            ))
        );
    }

    useEffect(() => {
        console.log(currentItems);
    }, [currentItems]);

    const requestPages = (index = 1) => {
        fetch(
            `http://mackenzythorpe-001-site1.btempurl.com/api/Members/getpaginatedusers?i=${index}`,
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
                console.log(data.TotalPage);
                setCurrentItems(data.members.Items);
                setPageCount(data.members.TotalPage);
            })
            .catch((error) => {
                console.error("Error:", error);
            });
    };

    useEffect(() => {
        requestPages();
        fetch(`http://mackenzythorpe-001-site1.btempurl.com/api/members`, {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
            },
        })
            .then((response) => {
                return response.json();
            })
            .then((data) => {
                setMembersPage(data);
            })
            .catch((error) => {
                console.error("Error:", error);
            });
    }, []);

    const RecentMembers = () => {
        return (
            membersPage &&
            membersPage.recentMembers.map((m) => (
                <div key={nanoid()} className="recent-member">
                    <img
                        src={`http://mackenzythorpe-001-site1.btempurl.com/images/users/${m.Image}`}
                    />
                    <h4> {m.UserName} </h4>
                    <h6> {m.ReviewCount} reviews </h6>
                </div>
            ))
        );
    };

    const handlePageClick = (event) => {
        requestPages(event.selected + 1);
        const newOffset = (event.selected * itemsPerPage) % currentItems.length;
        setItemOffset(newOffset);
    };

    const handleUserClick = (e) => {
        e.preventDefault();
        const username = e.target.getAttribute("data-username");
        navigate(`/user/${username}`);

    }

    return (
        <div className="mainContainer">
            <Header />
            <div className="members-container">
                <h1 className="title-hero">
                    Film lovers, critics and friends — find our members.
                </h1>
                <div className="recent-members">
                    <RecentMembers />
                </div>
                <div className="members-members">
                    <h1 className="our-members"> Our members... </h1>
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

export default Members;
