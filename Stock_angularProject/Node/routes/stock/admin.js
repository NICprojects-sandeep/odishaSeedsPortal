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


router.get('/allFillFinYr', permit.permission('ADMI'), adminBalModule.allFillFinYr);
router.get('/FillCropCategory', permit.permission('ADMI'), adminBalModule.FillCropCategory);
router.get('/FillCropByCategoryId', permit.permission('ADMI'), adminBalModule.FillCropByCategoryId);
router.get('/fillCurrentstockPosition', permit.permission('ADMI'), adminBalModule.fillCurrentstockPosition);
router.get('/fillGodownWiseStock', permit.permission('ADMI'), adminBalModule.fillGodownWiseStock);
router.get('/FillDistrict', permit.permission('ADMI'), adminBalModule.FillDistrict);
router.post('/fillStateStockPosition', permit.permission('ADMI'), adminBalModule.fillStateStockPosition);
router.get('/FillCategoryId', permit.permission('ADMI'), adminBalModule.FillCategoryId);
router.post('/getVarietywiseLift', permit.permission('ADMI'), adminBalModule.getVarietywiseLift);
router.get('/FillCropByStock_Farmer', permit.permission('ADMI'), adminBalModule.FillCropByStock_Farmer);
router.post('/dealerPacsSale', permit.permission('ADMI'), adminBalModule.dealerPacsSale);
router.get('/dailyProgressReport', permit.permission('ADMI'), adminBalModule.dailyProgressReport);
router.get('/getPFMSStatus', permit.permission('ADMI'), adminBalModule.getPFMSStatus);
router.get('/distwisestockdetails', permit.permission('ADMI'), adminBalModule.distwisestockdetails);
router.get('/blockwisestockdetails', permit.permission('ADMI'), adminBalModule.blockwisestockdetails);
router.get('/previousYeardailyProgressReport', permit.permission('ADMI'), adminBalModule.previousYeardailyProgressReport);
router.get('/dealerwisestockdetails', permit.permission('ADMI'), adminBalModule.dealerwisestockdetails);
router.get('/schemewisedetails', permit.permission('ADMI'), adminBalModule.schemewisedetails);
router.post('/dealerwisewisesaledetails', permit.permission('ADMI'), adminBalModule.dealerwisewisesaledetails);
router.get('/getPFSMTransctionDetails', permit.permission('ADMI'), adminBalModule.getPFSMTransctionDetails);
router.get('/getPFSMTransctionDetailsDealerCodeWise', permit.permission('ADMI'), adminBalModule.getPFSMTransctionDetailsDealerCodeWise);
router.get('/dealerPacsPaymentdetails', permit.permission('ADMI'), adminBalModule.dealerPacsPaymentdetails);
router.get('/subsidyInvolovementdetails', permit.permission('ADMI'), adminBalModule.subsidyInvolovementdetails);
router.get('/getAllUserType', permit.permission('ADMI'), adminBalModule.getAllUserType);
router.get('/getUserId', permit.permission('ADMI'), adminBalModule.getUserId);
router.post('/resetPassword', permit.permission('ADMI'), adminBalModule.resetPassword);
router.get('/getprebookingDtl', permit.permission('ADMI'), adminBalModule.getprebookingDtl);

router.post('/getSearchprebookingDtl', permit.permission('ADMI'), adminBalModule.getSearchprebookingDtl);
router.get('/getPrebookingDistrict', permit.permission('ADMI'), adminBalModule.getPrebookingDistrict);
router.get('/getVariey', permit.permission('ADMI'), adminBalModule.getVariey);










module.exports = router;