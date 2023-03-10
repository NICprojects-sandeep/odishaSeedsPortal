var express = require('express');
var router = express.Router();
var bodyParser = require('body-parser');
var balModule = require('../../models/stock/farmersaleBAL');
var sha256 = require('js-sha256');
var csrf = require('csurf');
var csrfProtection = csrf();
var parseForm = bodyParser.urlencoded({ extended: false });
var cache = require('cache-headers');
var cors=require('cors');
const reqip = require('request-ip')
var overrideConfig = {
    'maxAge': 2000,
    'setPrivate': true
  };
  
  router.get('/', function (req, res, next) {
    res.get('X-Frame-Options');
    res.render('home', { title: 'Home' });
  });

  router.get('/CHECKACCESSMODE', function (req, res, next) {//ODSON1/2014-15/0001/E
    res.get('X-Frame-Options');
    var RefNo = req.query.REF_NO;    
    balModule.CheckAccessMode(RefNo, function success(response) {
      res.send(response);
    }, function error(response) {
      console.log(response.status);
    });
  });

  router.get('/GETDISTCODEFROMLICNO', function (req, res, next) {//ODBAL1/2014-15/0010
    res.get('X-Frame-Options');
    var LicNo = req.query.LIC_NO;    
    balModule.GetDistCodeFromLicNo(LicNo, function success(response) {
      res.send(response);
    }, function error(response) {
      console.log(response.status);
    });
  });

  router.get('/GETCOMPTYPEFROMLICNO', function (req, res, next) {//ODBAL1/2014-15/0010
    res.get('X-Frame-Options');
    var LicNo = req.query.LIC_NO;    
    balModule.GetCompTypeFromLicNo(LicNo, function success(response) {
      res.send(response);
    }, function error(response) {
      console.log(response.status);
    });
  });

  router.get('/FILLFINYR', function (req, res, next) {//T
    res.get('X-Frame-Options');
    var Status = req.query.STATUS;    
    balModule.FillFinYr(Status, function success(response) {
      res.send(response);
    }, function error(response) {
      console.log(response.status);
    });
  });

  router.get('/FILLSEASSION', function (req, res, next) {//2021-22,T
    res.get('X-Frame-Options');
    var FinYr = req.query.FIN_YR;
    var Status = req.query.STATUS;
    balModule.FillSeassion(FinYr,Status, function success(response) {
      res.send(response);
    }, function error(response) {
      console.log(response.status);
    });
  });
  router.get('/FILLDEALERSTOCKCROP', function (req, res, next) {//ODBAL1/2014-15/0010,2021-22,R
    res.get('X-Frame-Options');
    var LicNo = req.query.LIC_NO;
    var FinYr = req.query.FIN_YR;
    var Season = req.query.SEASSION;
    balModule.FillDealerStockCrop(LicNo,FinYr,Season, function success(response) {
      res.send(response);
    }, function error(response) {
      console.log(response.status);
    });
  });

  router.get('/FILLDEALERSTOCKCROPVARIETY', function (req, res, next) {//ODBAL1/2014-15/0010,2021-22,R,C026
    res.get('X-Frame-Options');
    var LicNo = req.query.LIC_NO;
    var FinYr = req.query.FIN_YR;
    var Season = req.query.SEASSION;
    var CropCode = req.query.CROP_CODE;
    balModule.FillDealerStockCropVariety(LicNo,FinYr,Season,CropCode, function success(response) {
      res.send(response);
    }, function error(response) {
      console.log(response.status);
    });
  });

  router.get('/FILLDEALERSTOCK', function (req, res, next) {//ODBAL1/2014-15/0010,2021-22,R,C026,V387,OSSC
    res.get('X-Frame-Options');
    var LicNo = req.query.LIC_NO;
    var FinYr = req.query.FIN_YR;
    var Season = req.query.SEASSION;
    var CropCode = req.query.CROP_CODE;
    var VarietyCode = req.query.CROP_VERID;
    var UserType = req.query.USER_TYPE;
    balModule.FillDealerStock(LicNo,FinYr,Season,CropCode,VarietyCode,UserType, function success(response) {
      res.send(response);
    }, function error(response) {
      console.log(response.status);
    });
  });
  
  router.get('/GETFARMERSTATUS', function (req, res, next) {      
    res.get('X-Frame-Options');
    var FarmerId = req.query.FARMER_ID;
    balModule.GetFarmerStatus(FarmerId, function success(response) {        
      res.send(response);
    }, function error(response) {
      console.log(response.status);
    });
  });

  router.get('/GETFARMERINFO', function (req, res, next) {
    res.get('X-Frame-Options');
    var FarmerId = req.query.FARMER_ID;
    balModule.GetFarmerInfo(FarmerId, function success(response) {
      res.send(response);      
    }, function error(response) {
      console.log(response.status);
    });
  });

  router.get('/GETFARMERRECVCROP', function (req, res, next) {//BAL/103086,2021-22,R
    res.get('X-Frame-Options');
    var FarmerId = req.query.FARMER_ID;
    var FinYr = req.query.FIN_YR;
    var Season = req.query.SEASON;
    balModule.GetFarmerRecvCrop(FarmerId,FinYr,Season, function success(response) {
      res.send(response);
    }, function error(response) {
      console.log(response.status);
    });
  });

  // router.post('/GETDEALERSTOCKCROP', function (req, res, next) {//ODBAL1/2019-20/0033,2021-22,R
  //   res.get('X-Frame-Options');
  //   var LicNo = req.body.LIC_NO;
  //   var FinYr = req.body.FIN_YR;
  //   var Season = req.body.SEASON;
  //   balModule.GetDealerStockCrop(LicNo,FinYr,Season, function success(response) {
  //     res.send(response);
  //   }, function error(response) {
  //     console.log(response.status);
  //   });
  // });
  router.get('/GETDEALERSTOCKCROP', function (req, res, next) {//ODBAL1/2019-20/0033,2021-22,R
    res.get('X-Frame-Options');
    var LicNo = req.query.LicNo;
    var FinYear = req.query.FinYr;
    var Season = req.query.Season;
    balModule.GetDealerStockCrop(LicNo,FinYear,Season, function success(response) {
      res.send(response);
    }, function error(response) {
      console.log(response.status);
    });
  });

  router.get('/GETDEALERSTOCKVARIETY', function (req, res, next) {//ODBAL1/2019-20/0033,2021-22,R
    res.get('X-Frame-Options');
    var LicNo = req.query.LicNo;
    var FinYear = req.query.FinYr;
    var Season = req.query.Season;
    var CropCode = req.query.CropId;
    balModule.GetDealerStockVariety(LicNo,FinYear,Season,CropCode, function success(response) {
      res.send(response);
    }, function error(response) {
      console.log(response.status);
    });
  });

  router.get('/GETDEALERSTOCK', function (req, res, next) {//ODBAL1/2019-20/0033,2021-22,R
    res.get('X-Frame-Options');
    var LicNo = req.query.LicNo;
    var FinYear = req.query.FinYr;
    var Season = req.query.Season;
    var CropCode = req.query.CropId;
    var VarietyCode = req.query.VarietyId;
    balModule.GetDealerStock(LicNo,FinYear,Season,CropCode,VarietyCode, function success(response) {
      res.send(response);
    }, function error(response) {
      console.log(response.status);
    });
  });
  router.get('/FillCrops', async (req, res, next)=> {//T
    res.get('X-Frame-Options');
    const result = await balModule.FillCrops(req.query);
        res.send(result);
  });
  router.get('/FillVariety', async (req, res, next)=> {//T
    res.get('X-Frame-Options');
    const result = await balModule.FillVariety(req.query);
        res.send(result);
  });
  router.get('/getStockReceivedData', async (req, res, next)=> {//T
    res.get('X-Frame-Options');
    const result = await balModule.getStockReceivedData(req.query);
        res.send(result);
  });
  router.get('/getPreBookingDetails', async (req, res, next)=> {//T
    res.get('X-Frame-Options');
    const result = await balModule.getPreBookingDetails(req.query);
        res.send(result);
  });
  router.post('/InsertSaleDealer', async (req, res, next)=> {//T
    res.get('X-Frame-Options');
    req.body.DIST_CODE=await balModule.GetDistCodeByLicNo(req.body)
    req.body.DAO_CD=await balModule.GetDAOCodeByLicNo(req.body)

    req.body.UPDATED_BY=req.body.LICENCE_NO
    req.body.USERIP = reqip.getClientIp(req);
    
    console.log(req.body);
    // const result = await balModule.InsertSaleDealer(req.body);
    //     res.send(result);
  });
  module.exports = router;