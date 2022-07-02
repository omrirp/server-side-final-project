function welcome() {
    let user = JSON.parse(localStorage.getItem("user"));
    $("#admin").show();
    if (user == null || user.Name != "ADMIN") {
        $("#admin").hide();
        document.getElementById("welcome").innerHTML = "Welcome " + user.Name;
        return;
    }
    document.getElementById("welcome").innerHTML = "Welcome " + user.Name;    
}

function LogoutF() {
    if (localStorage.getItem("user") != null) {
        if (confirm("Are you sure ?")) {
            localStorage.clear();
            document.getElementById("welcome").innerHTML = "Welcome";
            window.location.replace("index.html");
        }
    }
    else
    {
        alert("you are not Logged in");
    }
    return;
}