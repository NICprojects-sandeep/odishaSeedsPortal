var express = require('express');
var router = express.Router();
var bodyParser = require('body-parser');
var aaoBalModule = require('../../bal/aaoBal');
var sha256 = require('js-sha256');
var csrf = require('csurf');
var csrfProtection = csrf();
var parseForm = bodyParser.urlencoded({ extended: false });
var cache = require('cache-headers');
var cors=require('cors');
const reqip = require('request-ip');


router.post('/paymentStatusByFarmeId', aaoBalModule.paymentStatusByFarmeId);
module.exports = router;