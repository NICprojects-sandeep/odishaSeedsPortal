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






module.exports = router;