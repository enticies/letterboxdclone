import { useEffect, useState } from "react";
import "../../assets/styles/findMovieModal.css";
import { nanoid } from "nanoid";

function FindMovieModal({closeFindMovieModal, closeLogMovieModalOpen, setMovieID}) {
    const [ searchMovie, setSearchMovie ] = useState("");
    const [ movies, setMovies ] = useState([]);

    useEffect(() => {
        if (searchMovie.trim().length !== 0) {
            fetch(`http://mackenzythorpe-001-site1.btempurl.com/api/movies/findmovie?str=${searchMovie}`, {
                method: "POST",
                headers: {
                'Content-Type': 'application/json',
            },
            })
            .then((response) => {
                return response.json();
            })
            .then((data) => {
                setMovies(data);
            })
            .catch((error) => {
                console.error('Error:', error);
            });
        }
    }, [searchMovie]);

    const handleChange = (e) => {
        setSearchMovie(e.target.value);
    }

    return (
        <div className="mainModal">
            <div className="backgroundContainerFind">
            </div>
            <form className="mainContainerFind">
                <button className="modalCloseBtn" onClick={() => closeFindMovieModal(false)}> <i className="fa-solid fa-xmark"></i> </button>
                <div className="modalTextInput">
                    <label htmlFor="moviename"> Name of Film </label>
                    <input name="moviename" value={searchMovie || ""} onChange={handleChange} type="text" id="moviename" required /> 
                    {
                        movies.length !== 0 ? (
                            <div className="findmovie-movies">
                                {movies.map(m => <button onClick={() => {
                                                            setMovieID(m.ID);
                                                            closeLogMovieModalOpen(true);
                                                            closeFindMovieModal(false);
                                                            }} 
                                        key={nanoid()} className="find-movie"> {m.Name} {m.YearNumber} </button>)}
                            </div>
                        )
                        :
                        null
                    }
                </div>
            </form>
        </div>
    )
}

export default FindMovieModal;
