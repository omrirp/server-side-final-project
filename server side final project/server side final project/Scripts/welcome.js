function welcome() {
    let user = JSON.parse(localStorage.getItem("user"));
    if (user == null) {
        return;
    }
    document.getElementById("welcome").innerHTML = "Welcome " + user.Name;    
}

function LogoutF() {
    if (localStorage.getItem("user") != null) {
        if (confirm("Are you sure ?")) {
            localStorage.clear();
            document.getElementById("welcome").innerHTML = "Welcome";
        }
    }
    else
    {
        alert("you are not Logged in");
    }
    return;
}