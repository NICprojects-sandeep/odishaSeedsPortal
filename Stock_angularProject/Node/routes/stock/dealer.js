var express = require('express');
var router = express.Router();
var bodyParser = require('body-parser');
var dealerBalModule = require('../../BAL/dealerBal');
var sha256 = require('js-sha256');
var csrf = require('csurf');
var csrfProtection = csrf();
var parseForm = bodyParser.urlencoded({ extended: false });
var cache = require('cache-headers');
var cors=require('cors');
const reqip = require('request-ip');
const permit = require('../../BAL/permission');

// router.post('/paymentStatusByFarmeId', dealerBalModule.paymentStatusByFarmeId);
router.get('/GetDealerLicenceByDistCodeUserType', permit.permission('SPO'), dealerBalModule.GetDealerLicenceByDistCodeUserType);
router.get('/GetDealerLicenceByDistCodeUserTypePacs',permit.permission('SPO'), dealerBalModule.GetDealerLicenceByDistCodeUserTypePacs);

router.get('/FILLFINYR',permit.permission('SPO'), dealerBalModule.FILLFINYR);
router.get('/FILLSEASSION',permit.permission('SPO'), dealerBalModule.FILLSEASSION);
router.get('/FILL_GODOWN',permit.permission('SPO'), dealerBalModule.FILL_GODOWN);
router.get('/FILL_CROPCATAGORY',permit.permission('SPO'), dealerBalModule.FILL_CROPCATAGORY);
router.get('/FILLCROPNAME',permit.permission('SPO'), dealerBalModule.FILLCROPNAME);
router.get('/FILLCROPVARIETY',permit.permission('SPO'), dealerBalModule.FILLCROPVARIETY);
router.get('/prebookingDetailsOfDealer',permit.permission('SPO'), dealerBalModule.prebookingDetailsOfDealer);
router.get('/fillAvailableStockDetails',permit.permission('SPO'), dealerBalModule.fillAvailableStockDetails);
router.get('/getSupplyType',permit.permission('SPO'), dealerBalModule.getSupplyType);
router.post('/fillDealerSaleDeatils',permit.permission('SPO'), dealerBalModule.fillDealerSaleDeatils);
router.get('/cashmemodetails',permit.permission('SPO'), dealerBalModule.cashmemodetails);
router.get('/FillLots',permit.permission('SPO'), dealerBalModule.FillLots);
router.get('/FillCrop',permit.permission('SPO'), dealerBalModule.FillCrop);
router.get('/FillVariety',permit.permission('SPO'), dealerBalModule.FillVariety);
router.post('/addinClass',permit.permission('SPO'), dealerBalModule.addinClass);
router.get('/allFillFinYr',permit.permission('SPO'), dealerBalModule.allFillFinYr);
router.get('/FillCropCategory',permit.permission('SPO'), dealerBalModule.FillCropCategory);
router.get('/FillCropByCategoryId',permit.permission('SPO'), dealerBalModule.FillCropByCategoryId);
router.get('/fillGodownwisestock',permit.permission('SPO'), dealerBalModule.fillGodownwisestock);
router.post('/receivedetails',permit.permission('SPO'), dealerBalModule.receivedetails);
router.post('/dateWiseSaleDetails',permit.permission('SPO'), dealerBalModule.dateWiseSaleDetails);
router.post('/dateWiseGodownTransferDetails',permit.permission('SPO'), dealerBalModule.dateWiseGodownTransferDetails);
router.post('/saledetails',permit.permission('SPO'), dealerBalModule.saledetails);
router.get('/getGodownmaster',permit.permission('SPO'), dealerBalModule.getGodownmaster);
router.get('/GetDistCodeFromDist',permit.permission('SPO'), dealerBalModule.GetDistCodeFromDist);
router.post('/paymentStatusByFarmeId',permit.permission('SPO'), dealerBalModule.paymentStatusByFarmeId);
router.post('/submitSeedSubsidyOfGrountnut',permit.permission('SPO'), dealerBalModule.submitSeedSubsidyOfGrountnut);
router.post('/ddutrnocheack',permit.permission('SPO'), dealerBalModule.ddutrnocheack);


















module.exports = router;