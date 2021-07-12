window.SetUserInfo = (user) => {
    const localStorage = window.localStorage;
    localStorage.setItem("userKey", user);

    return true;
}

window.SetToLocalStorage = (key, value) => {
    const localStorage = window.localStorage;
    localStorage.setItem(key, value);
    return true;
}

window.GetUserInfo = (key) => {
    const localStorage = window.localStorage;
    return localStorage.getItem(key);
}