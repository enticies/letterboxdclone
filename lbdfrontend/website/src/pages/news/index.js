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
    useSearchParams,
} from "react-router-dom";
import "react-tabs/style/react-tabs.css";
import "../../assets/styles/news.css";
import { UserContext } from "../../UserContext";
import { useContext } from "react";
import { useEffect } from "react";
import { useState } from "react";
import UserTabs from "../../components/usertabs/UserTabs";

function News() {
    const [ searchParams, setSearchParams ] = useSearchParams();
    const [ news, setNews ] = useState(null);
    const id = searchParams.get("id");

    const navigate = useNavigate();


    useEffect(() => {
        fetch(
            `http://mackenzythorpe-001-site1.btempurl.com/api/news/getbyid?id=${id}`,
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
                setNews(data);
            })
            .catch((error) => {
                console.error("Error:", error);
            });
    }, []);

    const handleAuthorClick = (e) => {
        e.preventDefault();
        navigate(`/user/${news.OwnerUsername}`);
    }

    return (
        <div className="mainContainer">
            <Header />
            <div className="article">
                <h1> {news && news.Title} </h1>
                <img src={news && `http://mackenzythorpe-001-site1.btempurl.com/images/news/${news.Image}`} />
                <p> {news && news.Body} </p>
                <p onClick={handleAuthorClick} className="author"> By {news && news.OwnerUsername} </p>
            </div>
            <Footer />
        </div>
    );
}

export default News;
