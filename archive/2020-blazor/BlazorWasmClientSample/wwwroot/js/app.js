﻿window.jk = {
    localStorageGet: function (key) {
        let value = localStorage.getItem(key);
        return value === null ? null : JSON.parse(value);
    },
    localStorageSet: function (key, value) {
        let json = JSON.stringify(value);
        localStorage.setItem(key, json);
    },
    pageTitle: 'Blazor Samples',
    resetPageTitle: function () {
        document.title = this.pageTitle;
    },
    setPageTitle: function (text) {
        document.title = text + ' - ' + this.pageTitle;
    }
}
