(function () {
    'use strict';

    config.$inject = ['$routeProvider', '$locationProvider'];

    //create a new angular module called moviesApp
    angular.module('moviesApp', [
        'ngRoute', 'moviesServices' //module moviesApp depends on module ngRoutes and moviesServices
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
            .controller('MoviesDeleteController', MoviesDeleteController)
    .controller('DatePickerController', DatePickerController);

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
            },
            function (error) {
                _showValidationErrors($scope, error);
            }
            );
        };
    }

    MoviesEditController.$inject = ['$scope', '$routeParams', '$location', 'Movies'];
    function MoviesEditController($scope, $routeParams, $location, Movies) {
        $scope.movie = Movies.get({ id: $routeParams.id });
        $scope.edit = function () {
            $scope.movie.$save(function () {
                $location.path('/');
            },
            function (error) {
                _showValidationErrors($scope, error);
            }
            );
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

    DatePickerController.$inject = ['$scope'];

    function DatePickerController($scope) {
        $scope.open = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.opened = true;
        };
    }

    function _showValidationErrors($scope, error) {
        $scope.validationErrors = [];
        if (error.data && angular.isObject(error.data)) {
            for (var key in error.data) {
                $scope.validationErrors.push(error.data[key][0]);
            }
        } else {
            $scope.validationErrors.push('Could not add movie.');
        };

    }
})();
;(function () {
    'use strict';

    //var moviesServices =
    angular
    .module('moviesServices', ['ngResource'])
.factory('Movies', Movies);

    Movies.$inject = ['$resource'];

    //$resource is a dependency required for AJAX calls
    function Movies($resource) {
        return $resource('/api/movies/:id');
    }

})();
