var app = angular.module("myApp", [])
app.controller('dealer1Ctrl', function ($scope, $http, $filter) {

   
    $scope.allfinYr = function () {
        $scope.allyr = []
        $http.get('/osp/home/manojdata?vcode='+$scope.vcode+'&updatedby='+$scope.updatedby).then(function success(response) {
            $scope.allyr = response.data;
            console.log($scope.allyr);
        }).catch(function err(error) {
            console.log('An error occurred...', error);
        });
    }
  
});
