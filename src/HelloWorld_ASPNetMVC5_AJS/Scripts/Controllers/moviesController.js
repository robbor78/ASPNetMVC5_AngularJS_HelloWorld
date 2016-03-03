(function () {
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
