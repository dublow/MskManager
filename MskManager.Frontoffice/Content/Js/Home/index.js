(function () {
    var me = function (auth) {
        DZ.api('/user/me',
            function (response) {
                console.log(response);
                $.post("/Home/Deezer",
                    { accessToken: auth.accessToken, id: response.id },
                    function () {

                    });
            });
    };
    var login = function () {
        DZ.login(function (response) {
                if (response.authResponse) {
                    me(response.authResponse);
                } else {
                    console.log('User cancelled login or did not fully authorize.');
                }
            },
            { perms: 'basic_access,email' });
    };
    var loginStatus = function (notLogged) {
        DZ.getLoginStatus(function (response) {
            if (response.authResponse) {
                me(response.authResponse);
            } else {
                notLogged();
            }
        });
    };
    
    var init = function() {
        DZ.init({
            appId: '163265',
            channelUrl: 'http://localhost:9998/Home/Channel'
        });
        loginStatus(function() {
            $("#login").on("click", login);
        });
    };
    
    init();
})();

