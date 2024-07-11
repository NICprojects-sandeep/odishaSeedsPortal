var express = require('express');
var router = express.Router();
var bodyParser = require('body-parser');
var adminBalModule = require('../../BAL/adminBal');
var sha256 = require('js-sha256');
var csrf = require('csurf');
var csrfProtection = csrf();
var parseForm = bodyParser.urlencoded({ extended: false });
var cache = require('cache-headers');
var cors = require('cors');
const reqip = require('request-ip');
const permit = require('../../BAL/permission');


router.get('/allFillFinYr', permit.permission('ADMI','jrcs'), adminBalModule.allFillFinYr);
router.get('/FillCropCategory', permit.permission('ADMI','jrcs'), adminBalModule.FillCropCategory);
router.get('/FillCropByCategoryId', permit.permission('ADMI','jrcs'), adminBalModule.FillCropByCategoryId);
router.get('/fillCurrentstockPosition', permit.permission('ADMI','jrcs'), adminBalModule.fillCurrentstockPosition);
router.get('/fillGodownWiseStock', permit.permission('ADMI','jrcs'), adminBalModule.fillGodownWiseStock);
router.get('/FillDistrict', permit.permission('ADMI','jrcs'), adminBalModule.FillDistrict);
router.post('/fillStateStockPosition', permit.permission('ADMI','jrcs'), adminBalModule.fillStateStockPosition);
router.get('/FillCategoryId', permit.permission('ADMI','jrcs'), adminBalModule.FillCategoryId);
router.post('/getVarietywiseLift', permit.permission('ADMI','jrcs'), adminBalModule.getVarietywiseLift);
router.get('/FillCropByStock_Farmer', permit.permission('ADMI','jrcs'), adminBalModule.FillCropByStock_Farmer);
router.post('/dealerPacsSale', permit.permission('ADMI','jrcs'), adminBalModule.dealerPacsSale);
router.get('/dailyProgressReport', permit.permission('ADMI','jrcs'), adminBalModule.dailyProgressReport);
router.get('/getPFMSStatus', permit.permission('ADMI','jrcs'), adminBalModule.getPFMSStatus);
router.get('/distwisestockdetails', permit.permission('ADMI','jrcs'), adminBalModule.distwisestockdetails);
router.get('/blockwisestockdetails', permit.permission('ADMI','jrcs'), adminBalModule.blockwisestockdetails);
router.get('/previousYeardailyProgressReport', permit.permission('ADMI','jrcs'), adminBalModule.previousYeardailyProgressReport);
router.get('/dealerwisestockdetails', permit.permission('ADMI','jrcs'), adminBalModule.dealerwisestockdetails);
router.get('/schemewisedetails', permit.permission('ADMI','jrcs'), adminBalModule.schemewisedetails);
router.post('/dealerwisewisesaledetails', permit.permission('ADMI','jrcs'), adminBalModule.dealerwisewisesaledetails);
router.get('/getPFSMTransctionDetails', permit.permission('ADMI','jrcs'), adminBalModule.getPFSMTransctionDetails);
router.get('/getPFSMTransctionDetailsDealerCodeWise', permit.permission('ADMI','jrcs'), adminBalModule.getPFSMTransctionDetailsDealerCodeWise);
router.get('/dealerPacsPaymentdetails', permit.permission('ADMI','jrcs'), adminBalModule.dealerPacsPaymentdetails);
router.get('/subsidyInvolovementdetails', permit.permission('ADMI','jrcs'), adminBalModule.subsidyInvolovementdetails);
router.get('/getAllUserType', permit.permission('ADMI','jrcs'), adminBalModule.getAllUserType);
router.get('/getUserId', permit.permission('ADMI','jrcs'), adminBalModule.getUserId);
router.post('/resetPassword', permit.permission('ADMI','jrcs'), adminBalModule.resetPassword);
router.get('/getprebookingDtl', permit.permission('ADMI','jrcs'), adminBalModule.getprebookingDtl);

router.post('/getSearchprebookingDtl', permit.permission('ADMI','jrcs'), adminBalModule.getSearchprebookingDtl);
router.get('/getPrebookingDistrict', permit.permission('ADMI','jrcs'), adminBalModule.getPrebookingDistrict);
router.get('/getVariey', permit.permission('ADMI','jrcs'), adminBalModule.getVariey);
router.get('/getSearchErupidata', permit.permission('ADMI','jrcs'),  adminBalModule.getSearchErupidata);
router.get('/getErupiDistrict', permit.permission('ADMI','jrcs'), adminBalModule.getErupiDistrict);
router.get('/getErupiBlock', permit.permission('ADMI','jrcs'), adminBalModule.getErupiBlock);
router.post('/getParticularSearchErupidata',adminBalModule.getParticularSearchErupidata);
router.post('/geterupiStockDetails',adminBalModule.geterupiStockDetails);











module.exports = router;