window.SetUserInfo = (user) => {
    console.log("Set Userinfo", user);
    const localStorage = window.localStorage;
    localStorage.setItem("userKey", user);

    return true;
}

window.GetUserInfo = (key) => {
    console.log("Get Userinfo", key);
    const localStorage = window.localStorage;
    return localStorage.getItem(key);
}