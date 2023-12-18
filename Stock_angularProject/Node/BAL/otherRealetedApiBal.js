const ip = require('ip');
const UAParser = require('ua-parser-js');
const crypto = require('crypto');
const sha512 = require('js-sha512');
const otherDal = require('../DAL/otherRealtedApiDal');
const reqip = require('request-ip');
const request = require('request');
var http = require('http');
var https = require('https');
const parser = new UAParser();
// const axios = require('axios');
const moment = require('moment');
var overrideConfig = {
    'maxAge': 2000,
    'setPrivate': true
};
exports.successPrebookingDtl = async (req, res) => {
    try {
        console.log();
        const result = await otherDal.successPrebookingDtl(req.query.date);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};