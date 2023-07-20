var express = require('express');
var router = express.Router();
var bodyParser = require('body-parser');
var balModule = require('../../models/osp/homeBAL');
var sha256 = require('js-sha256');
var csrf = require('csurf');
var csrfProtection = csrf();
var parseForm = bodyParser.urlencoded({ extended: false });
var cache = require('cache-headers');
var cors = require('cors');
var publicbalModule = require('../../BAL/publicBal');


var overrideConfig = {
  'maxAge': 2000,
  'setPrivate': true
};

router.get('/', function (req, res, next) {
  res.get('X-Frame-Options');
  res.render('home', { title: 'Home' });
});

router.get('/FillLogoDetails', function (req, res, next) {
  res.get('X-Frame-Options');
  var LANG_TYPE = req.query.LANG_TYPE;
  //console.log(LANG_TYPE);
  balModule.FillLogoDetails(LANG_TYPE, function success(response) {
    res.send(response);
  }, function error(response) {
    console.log(response.status);
  });
});

router.get('/FillMenu', function (req, res, next) {
  res.get('X-Frame-Options');
  var LANG_TYPE = req.query.LANG_TYPE;
  var ParentMenuId = req.query.ParentMenuId;
  balModule.FillMenu(LANG_TYPE, ParentMenuId, function success(response) {
    res.send(response);
  }, function error(response) {
    console.log(response.status);
  });
});

router.get('/FillSubMenu', function (req, res, next) {
  res.get('X-Frame-Options');
  var LANG_TYPE = req.query.LANG_TYPE;
  balModule.FillSubMenu(LANG_TYPE, function success(response) {
    res.send(response);
  }, function error(response) {
    console.log(response.status);
  });
});

router.get('/FillSlider', function (req, res, next) {
  res.get('X-Frame-Options');
  var LANG_TYPE = req.query.LANG_TYPE;
  var WidgetId = req.query.WidgetId;
  balModule.FillSlider(LANG_TYPE, WidgetId, function success(response) {
    res.send(response);
  }, function error(response) {
    console.log(response.status);
  });
});

router.get('/FillNews', function (req, res, next) {
  res.get('X-Frame-Options');
  var LANG_TYPE = req.query.LANG_TYPE;
  balModule.FillNews(LANG_TYPE, function success(response) {
    res.send(response);
  }, function error(response) {
    console.log(response.status);
  });
});

router.get('/FillContact', function (req, res, next) {
  res.get('X-Frame-Options');
  var LANG_TYPE = req.query.LANG_TYPE;
  balModule.FillContact(LANG_TYPE, function success(response) {
    res.send(response);
  }, function error(response) {
    console.log(response.status);
  });
});

router.get('/GRAPH10VARIETY', function (req, res, next) {
  res.get('X-Frame-Options');
  balModule.GRAPH10VARIETY(function success(response) {
    res.send(response);
  }, function error(response) {
    console.log(response.status);
  });
});

router.get('/GRAPH10LIC', function (req, res, next) {
  res.get('X-Frame-Options');
  balModule.GRAPH10LIC(function success(response) {
    res.send(response);
  }, function error(response) {
    console.log(response.status);
  });
});

router.get('/GRAPHVARIETYDTL', function (req, res, next) {
  res.get('X-Frame-Options');
  var VARIETY_NAME = req.query.VARIETY_NAME;
  balModule.FillGraphVarietyDtl(VARIETY_NAME, function success(response) {
    res.send(response);
  }, function error(response) {
    console.log(response.status);
  });
});
// router.post('/preBookingOfseed', function (req, res, next) {
router.post('/preBookingOfseed', async (req, res) => {
  res.setHeader('Access-Control-Allow-Origin', '*');
  res.setHeader('Access-Control-Allow-Methods', 'GET, POST, OPTIONS, PUT, PATCH, DELETE'); // If needed
  res.setHeader('Access-Control-Allow-Headers', 'X-Requested-With,content-type'); // If needed
  res.setHeader('Access-Control-Allow-Credentials', true);
  res.get('X-Frame-Options');
  try {
    const result = await balModule.preBookingOfseed(req.body);
    res.send(result);
  } catch (e) {
    res.status(500).send(e);
    throw e;
  }
});

//public dealerwisedata page
router.get('/page', cache.overrideCacheHeaders(overrideConfig), function (req, res) {
  res.get('X-Frame-Options');
  res.render('dealerwisedata');
});
router.get('/dealerwisedata', publicbalModule.dealerwisedata);

module.exports = router;
