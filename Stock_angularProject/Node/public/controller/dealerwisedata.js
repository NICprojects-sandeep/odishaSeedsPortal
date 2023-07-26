var app = angular.module("myApp", [])
app.controller('dealerCtrl', function ($scope, $http, $filter) {

    $scope.dealerwisedata12 = function () {
        $scope.getItemName = [];
        $scope.dealerwisedata = [];
        $scope.invoiceItems = [];
        $scope.invoiceItems1 = [];
        $scope.invoiceItems2 = [];

        $http.get('/osp/home/dealerwisedata?year=' + $scope.year + '&season=' + $scope.season + '&crop=' + $scope.crop + '&district=' + $scope.dist).then(function success(response) {
            $scope.dealerwisedata = response.data;
            if ($scope.dealerwisedata.length > 0) {

                // angular.forEach(response.data, function (i) {
                //     if (!$scope.getItemName.some(j => j.Variety_Code == i.Variety_Code)) {
                //         $scope.getItemName.push(i);
                //     }
                // });
                // $scope.getItemLength = $scope.getItemName.length

                var margeList = Enumerable.From($scope.dealerwisedata)
                    .GroupBy(function (item) { return item.LICENCE_NO })
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
                            // console.log(targetArray[0].LICENCE_NO == 'ODGAN2/2017-18/0006');
                            // if (targetArray[0].LICENCE_NO == 'ODGAN2/2017-18/0006') {
                            //     console.log(item);
                            // }
                            if (!existsInTarget) {
                                const newVariety = {
                                    ...item,
                                    rcvnoofbags: 0,
                                    avlnoofbags: 0,
                                    APP_FIRMNAME: targetArray[0].APP_FIRMNAME, LICENCE_NO: targetArray[0].LICENCE_NO
                                };
                                targetArray.push(newVariety);
                                // if(targetArray[0].LICENCE_NO=='ODGAN2/2017-18/0016'){
                                // console.log(targetArray);
                                // }
                                foundInAnyArray = true;
                            }
                        });

                        if (!foundInAnyArray) {
                            const newArray = [
                                {
                                    ...item,
                                    rcvnoofbags: 0,
                                    avlnoofbags: 0,
                                    APP_FIRMNAME: '',
                                    LICENCE_NO: ''
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
                $scope.invoiceItems1 = margeList;

                var margeList1 = Enumerable.From($scope.invoiceItems1)
                    .GroupBy(function (item) { return item.LICENCE_NO })
                    .Select(function (item) {
                        return item.source;
                    })
                    .ToArray();

                $scope.invoiceItems2 = margeList1[0];
                var sumByVarietyCode = {};
                var sumByVarietyCode1 = {};

                for (var i = 0; i < $scope.invoiceItems2.length; i++) {
                    var innerArray = $scope.invoiceItems2[i];
                    for (var j = 0; j < innerArray.length; j++) {
                        var item = innerArray[j];
                        var varietyCode = item.Variety_Code;
                        var avlnoofbags = item.avlnoofbags;
                        var rcvnoofbags = item.rcvnoofbags;


                        // Check if the Variety_Code already exists in the sumByVarietyCode object
                        if (sumByVarietyCode.hasOwnProperty(varietyCode)) {
                            sumByVarietyCode[varietyCode] += avlnoofbags;
                            sumByVarietyCode1[varietyCode] += rcvnoofbags;

                        } else {
                            sumByVarietyCode[varietyCode] = avlnoofbags;
                            sumByVarietyCode1[varietyCode] = rcvnoofbags;

                        }
                    }
                }

                $scope.resultArray = [];
                for (var varietyCode in sumByVarietyCode) {
                    if (sumByVarietyCode.hasOwnProperty(varietyCode)) {
                        var sum = sumByVarietyCode[varietyCode];
                        var sum1 = sumByVarietyCode1[varietyCode];
                        $scope.resultArray.push({ Variety_Code: varietyCode, sum: sum, sum1: sum1 });
                    }
                }
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
        }).catch(function err(error) {
            console.log('An error occurred...', error);
        });
    }
    $scope.loadAllCrop = function () {
        $scope.crops = []
        $http.get('/osp/home/loadAllCrop').then(function success(response) {
            $scope.crops = response.data;
        }).catch(function err(error) {
            console.log('An error occurred...', error);
        });
    }
    $scope.loadAllDistrict = function () {
        $scope.district = []
        $http.get('/osp/home/loadAllDistrict').then(function success(response) {
            $scope.district = response.data;
        }).catch(function err(error) {
            console.log('An error occurred...', error);
        });
    }
});
