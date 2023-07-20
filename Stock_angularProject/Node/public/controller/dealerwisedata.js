var app=angular.module("myApp",[])
app.controller('dealerCtrl', function ($scope, $http, $filter) {
    
    $scope.dealerwisedata = function () {
        console.log('hiii');
        $scope.getItemName = [];
        $http.get('/osp/home/dealerwisedata').then(function success(response) {
            $scope.dealerwisedata = response.data;

            angular.forEach(response.data, function (i) {
                if (!$scope.getItemName.some(j => j.Variety_Code == i.Variety_Code)) {
                    $scope.getItemName.push(i);
                }
            });
            $scope.getItemLength = $scope.getItemName.length

            var margeList = Enumerable.From($scope.dealerwisedata)
            .GroupBy(function (item) { return item.APP_FIRMNAME || 'NoSource';})
            .Select(function (item) {
                console.log(item.source,'k');
                return item.source;              
            })
            .ToArray();

        $scope.invoiceItems = margeList;
        console.log($scope.invoiceItems);
        }, function error(response) {
            console.log(response.status);
        }).catch(function err(error) {
            console.log('An error occurred...', error);
        });
    };

});