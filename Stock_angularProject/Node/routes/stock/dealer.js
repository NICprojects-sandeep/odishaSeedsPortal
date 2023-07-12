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

module.exports = router;