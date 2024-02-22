var express = require('express');
var router = express.Router();
var bodyParser = require('body-parser');
var dealerBalModule = require('../../BAL/dealerBal');
var sha256 = require('js-sha256');
var csrf = require('csurf');
var csrfProtection = csrf();
var parseForm = bodyParser.urlencoded({ extended: false });
var cache = require('cache-headers');
var cors = require('cors');
const reqip = require('request-ip');
const permit = require('../../BAL/permission');
const sha512 = require('js-sha512');
const request = require('request');

// router.post('/paymentStatusByFarmeId', dealerBalModule.paymentStatusByFarmeId);
router.get('/GetDealerLicenceByDistCodeUserType', permit.permission('SPO'), dealerBalModule.GetDealerLicenceByDistCodeUserType);
router.get('/GetDealerLicenceByDistCodeUserTypePacs', permit.permission('SPO'), dealerBalModule.GetDealerLicenceByDistCodeUserTypePacs);

router.get('/FILLFINYR', permit.permission('SPO'), dealerBalModule.FILLFINYR);
router.get('/FILLSEASSION', permit.permission('SPO'), dealerBalModule.FILLSEASSION);
router.get('/FILL_GODOWN', permit.permission('SPO'), dealerBalModule.FILL_GODOWN);
router.get('/FILL_CROPCATAGORY', permit.permission('SPO'), dealerBalModule.FILL_CROPCATAGORY);
router.get('/FILLCROPNAME', permit.permission('SPO'), dealerBalModule.FILLCROPNAME);
router.get('/FILLCROPVARIETY', permit.permission('SPO'), dealerBalModule.FILLCROPVARIETY);
router.get('/prebookingDetailsOfDealer', permit.permission('SPO'), dealerBalModule.prebookingDetailsOfDealer);
router.get('/fillAvailableStockDetails', permit.permission('SPO'), dealerBalModule.fillAvailableStockDetails);
router.get('/getSupplyType', permit.permission('SPO'), dealerBalModule.getSupplyType);
router.post('/fillDealerSaleDeatils', permit.permission('SPO'), dealerBalModule.fillDealerSaleDeatils);
router.get('/cashmemodetails', permit.permission('SPO'), dealerBalModule.cashmemodetails);
router.get('/FillLots', permit.permission('SPO'), dealerBalModule.FillLots);
router.get('/FillCrop', permit.permission('SPO'), dealerBalModule.FillCrop);
router.get('/FillVariety', permit.permission('SPO'), dealerBalModule.FillVariety);
router.post('/addinClass', permit.permission('SPO'), dealerBalModule.addinClass);
router.get('/allFillFinYr', permit.permission('SPO'), dealerBalModule.allFillFinYr);
router.get('/FillCropCategory', permit.permission('SPO'), dealerBalModule.FillCropCategory);
router.get('/FillCropByCategoryId', permit.permission('SPO'), dealerBalModule.FillCropByCategoryId);
router.get('/fillGodownwisestock', permit.permission('SPO'), dealerBalModule.fillGodownwisestock);
router.post('/receivedetails', permit.permission('SPO'), dealerBalModule.receivedetails);
router.post('/dateWiseSaleDetails', permit.permission('SPO'), dealerBalModule.dateWiseSaleDetails);
router.post('/dateWiseGodownTransferDetails', permit.permission('SPO'), dealerBalModule.dateWiseGodownTransferDetails);
router.post('/saledetails', permit.permission('SPO'), dealerBalModule.saledetails);
router.get('/getGodownmaster', permit.permission('SPO'), dealerBalModule.getGodownmaster);
router.get('/GetDistCodeFromDist', permit.permission('SPO'), dealerBalModule.GetDistCodeFromDist);
router.post('/paymentStatusByFarmeId', permit.permission('SPO'), dealerBalModule.paymentStatusByFarmeId);
router.post('/submitSeedSubsidyOfGrountnut', permit.permission('SPO'), dealerBalModule.submitSeedSubsidyOfGrountnut);
router.post('/ddutrnocheack', permit.permission('SPO'), dealerBalModule.ddutrnocheack);
router.get('/FillGoDownByDistCodeUserType', permit.permission('SPO'), dealerBalModule.FillGoDownByDistCodeUserType);
router.get('/FillAgencyByOSSC', permit.permission('SPO'), dealerBalModule.FillAgencyByOSSC);
router.get('/FillSourceByAgencyIdUserTypeValues', permit.permission('SPO'), dealerBalModule.FillSourceByAgencyIdUserTypeValues);
router.get('/FillGovtFarmByDistCode', permit.permission('SPO'), dealerBalModule.FillGovtFarmByDistCode);
router.get('/agencyNameReload', permit.permission('SPO'), dealerBalModule.agencyNameReload);
router.get('/FillCropVarietyByOutsideAgencies', permit.permission('SPO'), dealerBalModule.FillCropVarietyByOutsideAgencies);
router.get('/FillCropVarietyByGovtFarm', permit.permission('SPO'), dealerBalModule.FillCropVarietyByGovtFarm);
router.get('/FillCropVarietyByOUAT', permit.permission('SPO'), dealerBalModule.FillCropVarietyByOUAT);
router.get('/FillCropVarietyByMOUAgency', permit.permission('SPO'), dealerBalModule.FillCropVarietyByMOUAgency);
router.get('/FillCropVarietyByCropIdScheme', permit.permission('SPO'), dealerBalModule.FillCropVarietyByCropIdScheme);
router.get('/FillLotByGovtFarm', permit.permission('SPO'), dealerBalModule.FillLotByGovtFarm);
router.get('/fillBagExpiryDate', permit.permission('SPO'), dealerBalModule.fillBagExpiryDate);
router.get('/fillBagsFromStockStockDetails', permit.permission('SPO'), dealerBalModule.fillBagsFromStockStockDetails);
router.post('/Stock_Sp_InsReceiveDetails', permit.permission('SPO'), dealerBalModule.Stock_Sp_InsReceiveDetails);













router.get('/osscinventory', async (req, res) => {
    try {
        
        res.setHeader('Access-Control-Allow-Origin', '*');
        res.setHeader('Access-Control-Allow-Methods', 'GET, POST, OPTIONS, PUT, PATCH, DELETE'); // If needed
        res.setHeader('Access-Control-Allow-Headers', 'X-Requested-With,content-type'); // If needed
        res.setHeader('Access-Control-Allow-Credentials', true);
        res.get('X-Frame-Options');
        const obj = {}
        obj.spo_code = req.session.distCode_1;
        obj.spo_name = req.session.fullname.replace(/-/g, ' - ');
        obj.token=sha512(obj.spo_code+obj.spo_name+'Sikun@1234')
        request({
            url: "https://osscpayment.nic.in/auth/sisSpoLogin",
            method: "POST",
            json: true,   // <--Very important!!!
            body: obj
        }, async function (error, response, body) {
            if (error) {
                console.log(error);
            }
            else {
                console.log(body);
                res.send({body})
            }
        });















        // const ENC_TOKEN = "60e594191a4daa699217c40810c24fbdb0e37d1c27b5ed632fc977ccb4c84c8a";
        // res.setHeader('Access-Control-Allow-Origin', '*');
        // res.setHeader('Access-Control-Allow-Methods', 'GET, POST, OPTIONS, PUT, PATCH, DELETE'); // If needed
        // res.setHeader('Access-Control-Allow-Headers', 'X-Requested-With,content-type'); // If needed
        // res.setHeader('Access-Control-Allow-Credentials', true);
        // res.get('X-Frame-Options');


        // if (ENC_TOKEN == signedData) {
        //     homeBAL.insertSugamRequestData(ENC_TOKEN, Farmer_ID, applicationId, chkSum, signedData, req.connection.remoteAddress, req.device.type.toUpperCase(), 'ENC TOKEN Match');
        //     // res.redirect('http://localhost:3000/#/farmerBooking?Farmer_ID=' + Farmer_ID + '&applicationId=' + applicationId);
        //     res.redirect('https://dbtmbdodisha.nic.in/#/farmerBooking?Farmer_ID=' + Farmer_ID + '&applicationId=' + applicationId);
        // }
        // else {
        //     homeBAL.insertSugamRequestData(ENC_TOKEN, Farmer_ID, applicationId, chkSum, signedData, req.connection.remoteAddress, req.device.type.toUpperCase(), 'ENC TOKEN MisMatch');

        //     console.log('else');
        // }
    } catch (e) {
        console.log(e);
        res.status(500).send(e);
        throw e;
    }
});

















module.exports = router;