import jwtDecode from "jwt-decode";
import react, { useContext, useState } from "react";
import "../../assets/styles/registerModal.css";
import {UserContext} from "../../UserContext";

function LoginModal({closeLoginModal}) {
    const [inputs, setInputs] = useState({});
    const { user, setUser } = useContext(UserContext);

    const handleChange = (e) => {
        const name = e.target.name;
        const value = e.target.value;
        setInputs(prevState => {
            return {
                ...prevState,
                [name]: value,
            }
        });
    }

    const handleSubmit = (e) => {
        e.preventDefault();
        const errorElement = document.getElementsByClassName("errorOutput")[0];
        const genericErrorMessage = "Something went wrong.";
        errorElement.textContent = "";

        const data = {
            EmailOrUsername: inputs.emailorusername,
            Password: inputs.password,
        };

        fetch("http://mackenzythorpe-001-site1.btempurl.com/api/accounts/login", {
            method: "POST",
            headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data),
        })
        .then((response) => {
            if (!response.ok) {
                errorElement.textContent = genericErrorMessage;
            }
            else {
                closeLoginModal(false);
                return response.json();
            }
        })
        .then((data) => {
            console.log(data);
            localStorage.setItem("userData", JSON.stringify(data.userData));
            setUser(data.userData);

        })
        .catch((error) => {
            errorElement.textContent = genericErrorMessage;
            console.error('Error:', error);
        });


    }


    return (
        <div className="mainModal">
            <div className="backgroundContainerRegister">
            </div>
            <form className="mainContainerRegister" onSubmit={handleSubmit}>
                <button className="modalCloseBtn" onClick={() => closeLoginModal(false)}> <i className="fa-solid fa-xmark"></i> </button>
                <div className="modalTextInput email">
                    <label htmlFor="emailorusername"> Email or Username </label>
                    <input name="emailorusername" value={inputs.emailorusername || ""} onChange={handleChange} type="text" id="emailInput" required /> 
                </div>
                <div className="modalTextInput password">
                    <label htmlFor="password"> Password </label>
                    <input name="password" value={inputs.password || ""} onChange={handleChange} type="password" id="username" required/> 
                </div>
                <div className="errorOutput">
                </div> 
                <button className="signUpBtn"> Sign In </button>
            </form>
        </div>
    )
}

export default LoginModal;