var express = require('express');
var router = express.Router();
var bodyParser = require('body-parser');
var adminBalModule = require('../../BAL/adminBal');
var sha256 = require('js-sha256');
var csrf = require('csurf');
var csrfProtection = csrf();
var parseForm = bodyParser.urlencoded({ extended: false });
var cache = require('cache-headers');
var cors=require('cors');
const reqip = require('request-ip');



router.get('/allFillFinYr', adminBalModule.allFillFinYr);
router.get('/FillCropCategory', adminBalModule.FillCropCategory);
router.get('/FillCropByCategoryId', adminBalModule.FillCropByCategoryId);
router.get('/fillCurrentstockPosition', adminBalModule.fillCurrentstockPosition);
router.get('/fillGodownWiseStock', adminBalModule.fillGodownWiseStock);
router.get('/FillDistrict', adminBalModule.FillDistrict);
router.post('/fillStateStockPosition', adminBalModule.fillStateStockPosition);
router.get('/FillCategoryId', adminBalModule.FillCategoryId);
router.post('/getVarietywiseLift', adminBalModule.getVarietywiseLift);
router.get('/FillCropByStock_Farmer', adminBalModule.FillCropByStock_Farmer);
router.post('/dealerPacsSale', adminBalModule.dealerPacsSale);
router.get('/dailyProgressReport', adminBalModule.dailyProgressReport);
router.get('/getPFMSStatus', adminBalModule.getPFMSStatus);
router.get('/distwisestockdetails', adminBalModule.distwisestockdetails);
router.get('/blockwisestockdetails', adminBalModule.blockwisestockdetails);
router.get('/previousYeardailyProgressReport', adminBalModule.previousYeardailyProgressReport);
router.get('/dealerwisestockdetails', adminBalModule.dealerwisestockdetails);
router.get('/schemewisedetails', adminBalModule.schemewisedetails);






module.exports = router;