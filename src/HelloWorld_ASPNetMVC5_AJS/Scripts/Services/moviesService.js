(function () {
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
