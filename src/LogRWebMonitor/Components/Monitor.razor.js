

window.blazorExtensions = {
	WriteLocalStorage: function (key, value) {
		window.localStorage.setItem(key, value);
	},
	ReadLocalStorage: function (key) {
		return window.localStorage.getItem(key);
	}
};
