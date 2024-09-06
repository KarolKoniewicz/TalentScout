window.localStorageSetItem = (key, value) => {
    localStorage.setItem(key, value);
};

window.localStorageGetItem = (key) => {
    return localStorage.getItem(key);
};


window.localStorageRemoveItem = (key) => {
    localStorage.removeItem(key);
};

window.sessionStorageSetItem = (key, value) => {
    sessionStorage.setItem(key, value);
};

window.sessionStorageGetItem = (key) => {
    return sessionStorage.getItem(key);
};

window.sessionStorageRemoveItem = (key) => {
    sessionStorage.removeItem(key);
};