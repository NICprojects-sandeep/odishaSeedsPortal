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
module.exports = router;