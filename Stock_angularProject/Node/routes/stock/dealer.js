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


// router.post('/paymentStatusByFarmeId', dealerBalModule.paymentStatusByFarmeId);
router.get('/GetDealerLicenceByDistCodeUserType', dealerBalModule.GetDealerLicenceByDistCodeUserType);
router.get('/GetDealerLicenceByDistCodeUserTypePacs', dealerBalModule.GetDealerLicenceByDistCodeUserTypePacs);

router.get('/FILLFINYR', dealerBalModule.FILLFINYR);
router.get('/FILLSEASSION', dealerBalModule.FILLSEASSION);
router.get('/FILL_GODOWN', dealerBalModule.FILL_GODOWN);
router.get('/FILL_CROPCATAGORY', dealerBalModule.FILL_CROPCATAGORY);
router.get('/FILLCROPNAME', dealerBalModule.FILLCROPNAME);
router.get('/FILLCROPVARIETY', dealerBalModule.FILLCROPVARIETY);
router.get('/prebookingDetailsOfDealer', dealerBalModule.prebookingDetailsOfDealer);
router.get('/fillAvailableStockDetails', dealerBalModule.fillAvailableStockDetails);
router.get('/getSupplyType', dealerBalModule.getSupplyType);
router.post('/fillDealerSaleDeatils', dealerBalModule.fillDealerSaleDeatils);
router.get('/cashmemodetails', dealerBalModule.cashmemodetails);
router.get('/FillLots', dealerBalModule.FillLots);
router.get('/FillCrop', dealerBalModule.FillCrop);
router.get('/FillVariety', dealerBalModule.FillVariety);
router.post('/addinClass', dealerBalModule.addinClass);
router.get('/allFillFinYr', dealerBalModule.allFillFinYr);
router.get('/FillCropCategory', dealerBalModule.FillCropCategory);
router.get('/FillCropByCategoryId', dealerBalModule.FillCropByCategoryId);
router.get('/fillGodownwisestock', dealerBalModule.fillGodownwisestock);
router.post('/receivedetails', dealerBalModule.receivedetails);
router.post('/dateWiseSaleDetails', dealerBalModule.dateWiseSaleDetails);
router.post('/dateWiseGodownTransferDetails', dealerBalModule.dateWiseGodownTransferDetails);
router.post('/saledetails', dealerBalModule.saledetails);
router.get('/getGodownmaster', dealerBalModule.getGodownmaster);
router.get('/GetDistCodeFromDist', dealerBalModule.GetDistCodeFromDist);
router.post('/paymentStatusByFarmeId', dealerBalModule.paymentStatusByFarmeId);
router.post('/submitSeedSubsidyOfGrountnut', dealerBalModule.submitSeedSubsidyOfGrountnut);


















module.exports = router;