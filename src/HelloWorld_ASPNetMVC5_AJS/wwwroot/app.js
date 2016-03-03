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
;(function () {
    'use strict';

    angular
        .module('moviesApp')
        .controller('MoviesListController', MoviesListController)
    .controller('MoviesAddController', MoviesAddController)
        .controller('MoviesEditController', MoviesEditController)
            .controller('MoviesDeleteController', MoviesDeleteController);

    MoviesListController.$inject = ['$scope', 'Movies']; //required for minification
    //Movies parameter refers to the moviesServices
    function MoviesListController($scope, Movies) {
        $scope.movies = Movies.query(); //call the moviesServices
    }

    MoviesAddController.$inject = ['$scope', '$location', 'Movies'];
    function MoviesAddController($scope, $location, Movies) {
        $scope.movie = new Movies();
        $scope.add = function () {
            $scope.movie.$save(function () {
                $location.path('/');
            });
        };
    }

    MoviesEditController.$inject = ['$scope', '$routeParams', '$location', 'Movies'];
    function MoviesEditController($scope, $routeParams, $location, Movies) {
        $scope.movie = Movies.get({ id: $routeParams.id });
        $scope.edit = function () {
            $scope.movie.$save(function () {
                $location.path('/');
            });
        };
    }

    MoviesDeleteController.$inject = ['$scope', '$routeParams', '$location', 'Movies'];
    function MoviesDeleteController($scope, $routeParams, $location, Movies) {
        $scope.movie = Movies.get({ id: $routeParams.id });
        $scope.remove = function () {
            $scope.movie.$remove({ id: $scope.movie.Id }, function () {
                $location.path('/');
            });
        };
    }
})();
;(function () {
    'use strict';

    var moviesServices = angular
        .module('moviesServices', ['ngResource'])
    .factory('Movies', Movies);

    Movies.$inject = ['$resource'];

    //$resource is a dependency required for AJAX calls
    function Movies($resource) {
        return $resource('/api/movies/:id');
    }

})();
