import React from "react";
import { Link, useLocation, useParams } from "react-router-dom";

function UserTabs() {
    const userName = useParams().username;
    const location = useLocation();
    const currentTab = location.pathname.split("/").slice(-1)[0];
    return (
        <div className="tabs">
            <div className={currentTab === userName ? "tab active" : "tab"}>
                <Link to={`/user/${userName}`}> Profile </Link>
            </div>
            <div className={currentTab === "films" ? "tab active" : "tab"}>
                <Link to={`/user/${userName}/films`}> Films </Link>
            </div>
            <div className={currentTab === "lists" ? "tab active" : "tab"}>
                <Link to={`/user/${userName}/lists`}> Lists </Link>
            </div>
            <div className={currentTab === "reviews" ? "tab active" : "tab"}>
                <Link to={`/user/${userName}/reviews`}> Reviews </Link>
            </div>
            <div className={currentTab === "followers" ? "tab active" : "tab"}>
                <Link to={`/user/${userName}/followers`}> Followers </Link>
            </div>
            <div className={currentTab === "followees" ? "tab active" : "tab"}>
                <Link to={`/user/${userName}/followees`}> Followees </Link>
            </div>
            <div className={currentTab === "news" ? "tab active" : "tab"}>
                <Link to={`/user/${userName}/news`}> News </Link>
            </div>
        </div>
    );
}

export default UserTabs;
