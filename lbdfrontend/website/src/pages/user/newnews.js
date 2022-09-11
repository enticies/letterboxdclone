import Header from "../../components/header";
import Footer from "../../components/footer";
import { nanoid } from "nanoid";
import ReactPaginate from "react-paginate";
import UserTabs from "../../components/usertabs/UserTabs";
import {
    createSearchParams,
    Link,
    Navigate,
    useLocation,
    useNavigate,
    useParams,
} from "react-router-dom";
import defaultuser from "../../assets/images/users/defaultuser.jpg";
import "../../assets/styles/newnews.css";
import "react-tabs/style/react-tabs.css";
import { UserContext } from "../../UserContext";
import { useContext } from "react";
import { useEffect } from "react";
import { useState } from "react";

function NewNews() {
    const [title, setTitle] = useState("");
    const [body, setBody] = useState("");
    const [image, setImage] = useState(null);
    const { user, setUser } = useContext(UserContext);

    const navigate = useNavigate();

    const handleSubmit = (e) => {
        e.preventDefault();

        const formData = new FormData();

        formData.append("Image", image);
        formData.append("Body", body);
        formData.append("Title", title);
        formData.append("OwnerUserName", user.username);

        fetch("http://mackenzythorpe-001-site1.btempurl.com/api/news/createnews", {
            method: "PUT",
            headers: {
                // "Content-Type": "multipart/form-data",
                Authorization: "Bearer " + user.token,
            },
            body: formData,
        })
            .then((response) => {
                console.log(response);
                if (response.ok) {
                    navigate(`/user/${user.username}/news`)
                }
                return response.json();
            })
            .then((data) => {
                console.log(data);
            })
            .catch((error) => {
                console.error("Error:", error);
            });
    };

    return (
        <div className="mainContainer">
            <Header />
            <div className="container">
                <UserTabs />
                <form onSubmit={handleSubmit}>
                    <label htmlFor="title"> Title </label>
                    <input
                        onChange={(e) => setTitle(e.target.value)}
                        value={title}
                        id="title"
                        type="text"
                        placeholder="Enter a title"
                    />
                    <label htmlFor="body"> Body </label>
                    <textarea
                        onChange={(e) => setBody(e.target.value)}
                        value={body}
                        id="body"
                        type="text"
                        placeholder="Enter a body"
                    />
                    <label htmlFor="image"> Image </label>
                    <input
                        onChange={(e) => setImage(e.target.files[0])}
                        accept="image/jpeg"
                        id="image"
                        type="file"
                    />

                    <button className="save-news-button"> Save </button>
                </form>
            </div>
            <Footer />
        </div>
    );
}

export default NewNews;
