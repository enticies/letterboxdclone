import react, { useState } from "react";
import "../../assets/styles/registerModal.css";

function RegisterModal({closeRegisterModal}) {
    const [inputs, setInputs] = useState({});

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

        if (inputs.username.trim().length < 6) {
            const p = document.createElement("p");
            p.textContent = "Username must be at least 6 characters.";
            errorElement.appendChild(p);
            return;
        }
        const data = {
            Email: inputs.email,
            Username: inputs.username,
            Password: inputs.password,
            ConfirmPassword: inputs.password,
        };

        fetch("http://mackenzythorpe-001-site1.btempurl.com/api/accounts/register", {
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
                closeRegisterModal(false);
                return response.json();
            }
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
                <button className="modalCloseBtn" onClick={() => closeRegisterModal(false)}> <i className="fa-solid fa-xmark"></i> </button>
                <div className="modalTextInput email" >
                    <label htmlFor="emailInput"> Email Address </label>
                    <input name="email" value={inputs.email || ""} onChange={handleChange} type="email" id="emailInput" required /> 
                </div>
                <div className="modalTextInput username">
                    <label htmlFor="username" > Username </label>
                    <input name="username" value={inputs.username || ""} onChange={handleChange} type="text" id="username" required/> 
                </div>
                <div className="modalTextInput password">
                    <label htmlFor="password"> Password </label>
                    <input name="password" value={inputs.password || ""} onChange={handleChange} type="password" id="username" required/> 
                </div>
                <div className="errorOutput">
                </div> 
                <button className="signUpBtn"> Sign Up </button>
            </form>
        </div>
    )
}

export default RegisterModal;