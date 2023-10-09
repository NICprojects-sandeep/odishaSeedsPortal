var express = require('express');
var router = express.Router();
var bodyParser = require('body-parser');
var aaoBalModule = require('../../BAL/aaoBal');
var sha256 = require('js-sha256');
var csrf = require('csurf');
var csrfProtection = csrf();
var parseForm = bodyParser.urlencoded({ extended: false });
var cache = require('cache-headers');
var cors=require('cors');
const reqip = require('request-ip');


router.post('/paymentStatusByFarmeId', aaoBalModule.paymentStatusByFarmeId);
router.get('/GetDistCodeFromAOO', aaoBalModule.GetDistCodeFromAOO);
router.get('/fillfARMERiD', aaoBalModule.fillfARMERiD);
router.get('/gerFarmerDetailsForSamallMarginUpdatation', aaoBalModule.gerFarmerDetailsForSamallMarginUpdatation);
router.post('/farmerTypeUpdate', aaoBalModule.farmerTypeUpdate);
router.post('/delalerwisestockCheack', aaoBalModule.delalerwisestockCheack);

module.exports = router;