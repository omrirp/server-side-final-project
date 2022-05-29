function welcome() {
    let user = JSON.parse(localStorage.getItem("user"));
    if (user == null) {
        return;
    }
    document.getElementById("welcome").innerHTML = "Welcome " + user.Name;    
}