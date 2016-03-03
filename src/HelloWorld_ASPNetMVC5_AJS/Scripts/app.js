(function () {
    'use strict';

    config.$inject = ['$routeProvider', '$locationProvider'];

    //create a new angular module called moviesApp
    angular.module('moviesApp', [
        'ngRoutes', 'moviesServices' //module moviesApp depends on module ngRoutes and moviesServices
    ]).config(config);

    function config($routeProvider, $locationProvider) {
        $routeProvider
            .when('/', {
                templateUrl: '/Views/list.html',
                controller: 'MoviesListController'
            })
            .when('/movies/add', {
                templateUrl: '/Views/add.html',
                controller: 'MoviesAddController'
            })
            .when('/movies/edit/:id', {
                templateUrl: '/Views/edit.html',
                controller: 'MoviesEditController'
            })
            .when('/movies/delete/:id', {
                templateUrl: '/Views/delete.html',
                controller: 'MoviesDeleteController'
            });

        $locationProvider.html5Mode(true);
    }
})();
