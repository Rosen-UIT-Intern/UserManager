define(['knockout', 'plugins/http', 'plugins/router', 'jquery', 'knockout.validation'],
    function (ko, http, router, $) {
        //list of users
        var lUsers = ko.observableArray([]);
        var keySearch = ko.observable();

        //Info of user
        function userInfo(data) {
            var self = this;
            self.firstName = ko.observable(data.firstName);
            self.lastName = ko.observable(data.lastName);
            self.role = ko.observable(data.role);
            self.group = ko.observable(data.group);
            self.organization = ko.observable(data.organization);
            self.image = ko.observable(data.image);
        }

        var addUser = function () {
            router.navigate("create");
        }

        var getAllUsers = function () {

            //clear
            lUsers.removeAll();

            http.get('https://localhost:5001/api/user')
                .then(function (u) {

                    console.log(u);

                    u.forEach(element => {
                        lUsers.push(element);
                    });

                }, function (error) {
                    alert("Error: Can't connect to server.");
                });


            // $.ajax({
            //     url: 'https://localhost:5001/api/user',
            //     // data: this.toJSON(data),
            //     type: 'GET',
            //     contentType: 'application/json',
            //     dataType: 'json',
            //     // headers: ko.toJS(headers),
            //     success: function (u) {
            //         console.log(u);

            //         u.forEach(element => {
            //             lUsers.push(element);
            //         });
            //     },
            //     error: function (jqXHR, textStatus, errorThrown) {
            //         // if (jqXHR.status === '401') {
            //         // }

            //         alert("Error: Can't connect to server.");
            //     }

            // });
        }

        var searchUser = function(keySearch){
            lUsers.removeAll();

            http.get('https://localhost:5001/api/search?name=' + keySearch)
                .then(function (u) {

                    console.log(u);

                    u.forEach(element => {
                        lUsers.push(element);
                    });

                }, function (error) {
                    alert("Error: Can't connect to server.");
                });
        }

        var viewProfile = function (profile) {
            console.log(profile);
            router.navigate("profile/" + profile.id);
        }

        return {
            activate: function () {
                getAllUsers();
            },
            lUsers: lUsers,
            addUser: addUser,
            viewProfile: viewProfile,
            keySearch: keySearch,
            search: function(keySearch){
                console.log(this.keySearch());
                searchUser(this.keySearch());
            },
        };
    });