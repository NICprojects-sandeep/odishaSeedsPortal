var app = angular.module("myApp", [])
app.controller('dealerCtrl', function ($scope, $http, $filter) {

    $scope.dealerwisedata12 = function () {
        console.log('hiii');
        $scope.getItemName = [];
        $scope.dealerwisedata=[];
        $scope.invoiceItems=[];
        $scope.invoiceItems1 =[];
        $scope.invoiceItems2=[];

        $http.get('/osp/home/dealerwisedata?year=' + $scope.year + '&season=' + $scope.season + '&crop=' + $scope.crop + '&district=' + $scope.dist).then(function success(response) {
            $scope.dealerwisedata = response.data;
            if ($scope.dealerwisedata.length > 0) {

            console.log($scope.dealerwisedata);

            // angular.forEach(response.data, function (i) {
            //     if (!$scope.getItemName.some(j => j.Variety_Code == i.Variety_Code)) {
            //         $scope.getItemName.push(i);
            //     }
            // });
            // $scope.getItemLength = $scope.getItemName.length

            var margeList = Enumerable.From($scope.dealerwisedata)
                .GroupBy(function (item) { return item.APP_FIRMNAME })
                .Select(function (item) {
                    return item.source;
                })
                .ToArray();

            $scope.invoiceItems = margeList;


            const addMissingVarieties = (sourceArray, targetArrays) => {
                sourceArray.forEach(item => {
                    const varietyCode = item.Variety_Code;
                    let foundInAnyArray = false;

                    targetArrays.forEach(targetArray => {
                        const existsInTarget = targetArray.some(targetItem => targetItem.Variety_Code === varietyCode);
                        if (!existsInTarget) {
                            const newVariety = {
                                ...item,
                                rcvnoofbags: 0,
                                avlnoofbags: 0,
                                APP_FIRMNAME: targetArray[0].APP_FIRMNAME, LICENCE_NO: targetArray[0].LICENCE_NO
                            };
                            targetArray.push(newVariety);
                            foundInAnyArray = true;
                        }
                    });

                    if (!foundInAnyArray) {
                        const newArray = [
                            {
                                ...item,
                                rcvnoofbags: 0,
                                avlnoofbags: 0
                            }
                        ];
                        targetArrays.push(newArray);
                    }
                });
            };
            for (let i = 0; i < margeList.length; i++) {
                const currentArray = margeList[i];
                const otherArrays = [...margeList.slice(0, i), ...margeList.slice(i + 1)];
                addMissingVarieties(currentArray, otherArrays);
            }

            margeList.forEach(array => {
                array.sort((a, b) => a.Variety_Name.localeCompare(b.Variety_Name));
            });
            console.log(margeList);
            $scope.invoiceItems1 = margeList;

            var margeList1 = Enumerable.From($scope.invoiceItems1)
                .GroupBy(function (item) { return item.APP_FIRMNAME })
                .Select(function (item) {
                    return item.source;
                })
                .ToArray();

            $scope.invoiceItems2 = margeList1[0];
            console.log($scope.invoiceItems2);
        }

        }, function error(response) {
            console.log(response.status);
        }).catch(function err(error) {
            console.log('An error occurred...', error);
        });
    };
    $scope.allfinYr = function () {
        $scope.allyr = []
        $http.get('/osp/home/allfinYr').then(function success(response) {
            $scope.allyr = response.data;
        }).catch(function err(error) {
            console.log('An error occurred...', error);
        });
    }
    $scope.getSeason = function () {
        $scope.allseason = []
        $http.get('/osp/home/getSeason?year=' + $scope.year).then(function success(response) {
            $scope.allseason = response.data;
            console.log($scope.allseason);
        }).catch(function err(error) {
            console.log('An error occurred...', error);
        });
    }
    $scope.loadAllCrop = function () {
        $scope.crops = []
        $http.get('/osp/home/loadAllCrop').then(function success(response) {
            $scope.crops = response.data;
            console.log($scope.crops);
        }).catch(function err(error) {
            console.log('An error occurred...', error);
        });
    }
    $scope.loadAllDistrict = function () {
        $scope.district = []
        $http.get('/osp/home/loadAllDistrict').then(function success(response) {
            $scope.district = response.data;
            console.log($scope.district);
        }).catch(function err(error) {
            console.log('An error occurred...', error);
        });
    }
});
