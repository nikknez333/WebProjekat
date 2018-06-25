$(document).ready(function () {
    if (localStorage.getItem("Logged") == null)
        presentLogIn();
    else
        presentApp();
});