const RouteGuard = () => {
    function hasJWT() {
        let flag = false;

        localStorage.getItem("token") ? flag = true : flag = false;

    return flag;
    }
};

export default RouteGuard;
