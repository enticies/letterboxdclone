import React from "react";
import "../../assets/styles/footer.css";

function Footer() {
    return (
        <footer>
            <p> © Letterboxd Limited. Made by fans in Aotearoa. Film data from TMDb. Mobile site. <a target="_blank" className="icon" href="https://twitter.com/letterboxd"> <i className="fa-brands fa-twitter"></i> </a> <a target="_blank" className="icon" href="https://www.facebook.com/letterboxd"> <i className="fa-brands fa-facebook"></i> </a> <a target="_blank" className="icon" href="https://www.youtube.com/c/letterboxdhq"> <i className="fa-brands fa-youtube"></i> </a> </p>
        </footer>
    );
}

export default Footer;