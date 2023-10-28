var express = require('express');
var router = express.Router();
var bodyParser = require('body-parser');
var balModule = require('../../models/stock/farmersaleBAL');
var sha256 = require('js-sha256');
var csrf = require('csurf');
var csrfProtection = csrf();
var parseForm = bodyParser.urlencoded({ extended: false });
var cache = require('cache-headers');
var cors = require('cors');
const reqip = require('request-ip')
const balModule1 = require('../../BAL/farmersaleBal1');
const permit = require('../../BAL/permission');
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


router.get('/GETCOMPTYPEFROMLICNO', function (req, res, next) {//ODBAL1/2014-15/0010
  res.get('X-Frame-Options');
  var LicNo = req.query.LIC_NO;
  balModule.GetCompTypeFromLicNo(LicNo, function success(response) {
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
  balModule.FillDealerStockCrop(LicNo, FinYr, Season, function success(response) {
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
  balModule.FillDealerStockCropVariety(LicNo, FinYr, Season, CropCode, function success(response) {
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

// router.get('/GETFARMERINFO', function (req, res, next) {
//   res.get('X-Frame-Options');
//   var FarmerId = req.query.FARMER_ID;
//   balModule.GetFarmerInfo(FarmerId, function success(response) {
//     res.send(response);      
//   }, function error(response) {
//     console.log(response.status);
//   });
// });

router.get('/GETFARMERRECVCROP', function (req, res, next) {//BAL/103086,2021-22,R
  res.get('X-Frame-Options');
  var FarmerId = req.query.FARMER_ID;
  var FinYr = req.query.FIN_YR;
  var Season = req.query.SEASON;
  balModule.GetFarmerRecvCrop(FarmerId, FinYr, Season, function success(response) {
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
  balModule.GetDealerStockCrop(LicNo, FinYear, Season, function success(response) {
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
  balModule.GetDealerStockVariety(LicNo, FinYear, Season, CropCode, function success(response) {
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
  balModule.GetDealerStock(LicNo, FinYear, Season, CropCode, VarietyCode, function success(response) {
    res.send(response);
  }, function error(response) {
    console.log(response.status);
  });
});



router.get('/GetFirmName', balModule1.GetFirmName);
router.get('/GetFarmerInvHdr', balModule1.GetFarmerInvHdr);
router.get('/GetFarmerInv', balModule1.GetFarmerInv);
router.get('/GetFarmerDtl', balModule1.GetFarmerDtl);

router.get('/RptDateWiseSale', balModule1.RptDateWiseSale);

// ------------------------------------postgress  ----------------------------
router.get('/GETDISTCODEFROMLICNO', permit.permission('Dealer'), balModule1.GETDISTCODEFROMLICNO);
router.get('/getStockReceivedData', permit.permission('Dealer'), balModule1.getStockReceivedData);
router.get('/getPreBookingDetails', permit.permission('Dealer'), balModule1.getPreBookingDetails);
router.get('/sendOtp', permit.permission('Dealer'), balModule1.sendOtp);
router.get('/ValidateOTP', permit.permission('Dealer'), balModule1.ValidateOTP);
router.get('/FillCrops', permit.permission('Dealer'), balModule1.FillCrops);
router.get('/FillVariety', permit.permission('Dealer'), balModule1.FillVariety);
router.get('/FILLFINYR', permit.permission('Dealer'), balModule1.FILLFINYR);
router.get('/FILLSEASSION', permit.permission('Dealer'), balModule1.FILLSEASSION);
router.get('/FILLDEALERSTOCK', permit.permission('Dealer'), balModule1.FILLDEALERSTOCK);
router.post('/InsertSaleDealer', permit.permission('Dealer'), balModule1.InsertSaleDealer);
router.get('/GETFARMERINFO', permit.permission('Dealer'), balModule1.GETFARMERINFO);
router.get('/getCurrentstockDetails', permit.permission('Dealer'), balModule1.getCurrentstockDetails);
router.get('/getPaymentResponse', permit.permission('Dealer'), balModule1.getPaymentResponse);
router.get('/GetDistCodeFromDist', permit.permission('Dealer'), balModule1.GetDistCodeFromDist);
router.post('/paymentStatusByFarmeId', permit.permission('Dealer'), balModule1.paymentStatusByFarmeId);
router.get('/allFillFinYr', permit.permission('Dealer'), balModule1.allFillFinYr);
router.get('/FillCropCategory', permit.permission('Dealer'), balModule1.FillCropCategory);
router.get('/FillCropByCategoryId', permit.permission('Dealer'), balModule1.FillCropByCategoryId);
router.get('/fillGodownwisestock', permit.permission('Dealer'), balModule1.fillGodownwisestock);
///////crypto
const crypto = require('crypto');
const AADHARNO = '749609663932';



function IV() {
  const chars = 'abcdefghijklmnopqrstuvwxyz0123456789/-_@#!$%&';
  let iv = '';
  for (let i = 0; i < 16; i++) {
    // iv += chars[Math.floor(Math.random() * chars.length)];
    const randomIndex = Math.floor(Math.random() * chars.length);
    iv += chars.charAt(randomIndex);
  }
  return iv;
}

async function main() {
  try {
    if (/^\d+$/.test(AADHARNO.trim()) && AADHARNO.trim().length === 12 && AADHARNO) {
      const iv = IV();
      const aes = crypto.createCipheriv('aes-128-cbc', '0cwiza8@ms/e_-3d', iv);
      let encryptedText = aes.update(AADHARNO, 'utf-8', 'base64');
      encryptedText += aes.final('base64');
      console.log('Encrypted AADHAR No:', encryptedText);
      var encrypts = 'LflSZCzDAln8cLamNaqrhg=='
      var ivvalue = 'c32c8r%f--cf/ag0'
      const decryptedAADHARNO = aes256CbcDecrypt(encrypts, '0cwiza8@ms/e_-3d', ivvalue);

      // const decryptedAADHARNO = aes256CbcDecrypt(encryptedText, '0cwiza8@ms/e_-3d', iv);
      console.log('Decrypted AADHAR No:', decryptedAADHARNO);
    } else {
      console.log("Enter AAdhaar No. correctly !!!");
    }

    // Rest of your logic

    console.log('Done !!!');
    // }
  } catch (error) {
    throw error;
  }
}
function aes256CbcDecrypt(encryptedData, key, iv) {
  const decipher = crypto.createDecipheriv('aes-128-cbc', key, iv);
  let decrypted = decipher.update(encryptedData, 'base64', 'utf8');
  decrypted += decipher.final('utf8');
  return decrypted;
}
// main();

//right code
// router.get('/GETFARMERINFO', function (req, res, next) {
//   res.get('X-Frame-Options');
//   var FarmerId = req.query.FARMER_ID;
//   balModule.GetFarmerInfo(FarmerId, function success(response) {
//     res.send(response);      
//   }, function error(response) {
//     console.log(response.status);
//   });
// });

// router.get('/GETDISTCODEFROMLICNO', function (req, res, next) {//ODBAL1/2014-15/0010
//   res.get('X-Frame-Options');
//   var LicNo = 'ODPUR3/2018-19/0018';    
//   balModule.GetDistCodeFromLicNo(LicNo, function success(response) {
//     res.send(response);
//   }, function error(response) {
//     console.log(response.status);
//   });
// });

// router.post('/InsertSaleDealer', async (req, res, next)=> {//T
//   res.get('X-Frame-Options');
//   req.body.DIST_CODE=await balModule.GetDistCodeByLicNo(req.body)
//   req.body.DAO_CD=await balModule.GetDAOCodeByLicNo(req.body)

//   req.body.UPDATED_BY=req.body.LICENCE_NO
//   req.body.USERIP = reqip.getClientIp(req);

//   const result = await balModule.InsertSaleDealer(req.body);
//       res.send(result);
// });

// router.get('/FILLDEALERSTOCK', function (req, res, next) {//ODBAL1/2014-15/0010,2021-22,R,C026,V387,OSSC
//   console.log('jjjj');
//   res.get('X-Frame-Options');
//   var LicNo = req.query.LIC_NO;
//   var FinYr = req.query.FIN_YR;
//   var Season = req.query.SEASSION;
//   var CropCode = req.query.CROP_CODE;
//   var VarietyCode = req.query.CROP_VERID;
//   var UserType = req.query.USER_TYPE;
//   balModule.FillDealerStock(LicNo,FinYr,Season,CropCode,VarietyCode,UserType, function success(response) {
//     res.send(response);
//   }, function error(response) {
//     console.log(response.status);
//   });
// });


// router.get('/FillVariety', async (req, res, next)=> {//T
//   res.get('X-Frame-Options');
//   const result = await balModule.FillVariety(req.query);
//       res.send(result);
// });
// router.get('/FILLFINYR', function (req, res, next) {//T
//   res.get('X-Frame-Options');
//   var Status = req.query.STATUS;    
//   balModule.FillFinYr(Status, function success(response) {
//     res.send(response);
//   }, function error(response) {
//     console.log(response.status);
//   });
// });

// router.get('/FILLSEASSION', function (req, res, next) {//2021-22,T
//   res.get('X-Frame-Options');
//   var FinYr = req.query.FIN_YR;
//   var Status = req.query.STATUS;
//   balModule.FillSeassion(FinYr,Status, function success(response) {
//     res.send(response);
//   }, function error(response) {
//     console.log(response.status);
//   });
// });




module.exports = router;