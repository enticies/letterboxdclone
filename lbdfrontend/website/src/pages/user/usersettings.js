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
import "../../assets/styles/usersettings.css";
import "react-tabs/style/react-tabs.css";
import { UserContext } from "../../UserContext";
import { useContext } from "react";
import { useEffect } from "react";
import { useState } from "react";
import UserTabs from "../../components/usertabs/UserTabs";
import axios from "axios";

function UserSettings() {
    const { user, setUser } = useContext(UserContext);
    const [ToggleState, setToggleState] = useState(1);
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [username, setUsername] = useState("");
    const [image, setImage] = useState(null);
    const navigate = useNavigate();

    const onImageChange = (e) => {
        setImage(e.target.files[0]);
    };

    const toggleTab = (index) => {
        setToggleState(index);
    };

    const getActiveClass = (index, className) =>
        ToggleState === index ? className : "";

    const handleSaveImage = (e) => {
        e.preventDefault();
        const formData = new FormData();
        formData.append("UserName", user.username);
        formData.append("Image", image);
        if (image === null) {
            return;
        }

        axios({
            method: "post",
            url: "http://mackenzythorpe-001-site1.btempurl.com/api/accounts/changeuseravatar",
            data: formData,
            headers: {
                "Content-Type": "multipart/form-data",
                Authorization: "Bearer " + user.token,
            },
        })
            .then(function (response) {
                console.log(response);
                if (response.status === 200) {
                    navigate(`/user/${user.username}`);
                }
            })
            .catch(function (response) {
                console.log(response);
            });
    };

    const loginAgain = () => {
        const data = {
            EmailOrUsername: email,
            Password: password,
        };
        fetch(
            "http://mackenzythorpe-001-site1.btempurl.com/api/accounts/login",
            {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(data),
            }
        )
            .then((response) => {
                return response.json();
            })
            .then((data) => {
                console.log(data);
                localStorage.setItem("userData", JSON.stringify(data.userData));
                setUser(data.userData);
            })
            .catch((error) => {
                console.error("Error:", error);
            });
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        const body = {
            UserName: username,
            Password: password,
            Email: email,
        };
        fetch(
            `http://mackenzythorpe-001-site1.btempurl.com/api/accounts/changeusercredentials?userName=${user.username}`,
            {
                method: "POST",
                headers: {
                    Authorization: "Bearer " + user.token,
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(body),
            }
        )
            .then((response) => {
                console.log(response);
                if (response.ok) {
                    loginAgain();
                }
            })
            .catch((error) => {
                console.error("Error:", error);
            });
    };

    return (
        <div className="mainContainer">
            <Header />
            <div className="settings-container">
                <h1> Account Settings </h1>
                <ul className="tab-list">
                    <li
                        className={`tabs ${getActiveClass(1, "active-tabs")}`}
                        onClick={() => toggleTab(1)}
                    >
                        Profile
                    </li>
                    <li
                        className={`tabs ${getActiveClass(2, "active-tabs")}`}
                        onClick={() => toggleTab(2)}
                    >
                        Avatar
                    </li>
                </ul>
                <div className="content-container">
                    <div
                        className={`content ${getActiveClass(
                            1,
                            "active-content"
                        )}`}
                    >
                        <form
                            onSubmit={handleSubmit}
                            className="change-credentials"
                        >
                            <label htmlFor="email"> Email </label>
                            <input
                                id="email"
                                type="email"
                                onChange={(e) => setEmail(e.target.value)}
                                value={email}
                                placeholder="Enter a new email"
                                className="email"
                                required
                            />
                            <label htmlFor="username"> Username </label>
                            <input
                                minLength="6"
                                id="username"
                                type="text"
                                onChange={(e) => setUsername(e.target.value)}
                                value={username}
                                placeholder="Enter a new username"
                                className="username"
                                required
                            />
                            <label htmlFor="password"> Password </label>
                            <input
                                minLength="6"
                                id="password"
                                type="password"
                                onChange={(e) => setPassword(e.target.value)}
                                value={password}
                                placeholder="Enter a new password"
                                className="password"
                                required
                            />
                            <button className="save"> Save </button>
                        </form>
                    </div>
                    <div
                        className={`content ${getActiveClass(
                            2,
                            "active-content"
                        )}`}
                    >
                        <div className="save-image">
                            <input
                                type="file"
                                accept="image/jpeg"
                                onChange={onImageChange}
                            />
                            <button
                                className="save-image-button"
                                onClick={handleSaveImage}
                            >
                                {" "}
                                Save{" "}
                            </button>
                        </div>
                    </div>
                </div>
            </div>
            <Footer />
        </div>
    );
}

export default UserSettings;
