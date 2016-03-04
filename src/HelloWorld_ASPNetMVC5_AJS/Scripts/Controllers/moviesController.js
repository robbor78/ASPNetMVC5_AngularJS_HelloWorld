(function () {
    'use strict';

    angular
        .module('moviesApp')
        .controller('MoviesListController', MoviesListController)
    .controller('MoviesAddController', MoviesAddController)
        .controller('MoviesEditController', MoviesEditController)
            .controller('MoviesDeleteController', MoviesDeleteController)
    .controller('DatePickerController', DatePickerController);

    MoviesListController.$inject = ['$scope', 'Movies', 'canEdit']; //required for minification
    //Movies parameter refers to the moviesServices
    function MoviesListController($scope, Movies, canEdit) {
        $scope.canEdit = canEdit;
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
