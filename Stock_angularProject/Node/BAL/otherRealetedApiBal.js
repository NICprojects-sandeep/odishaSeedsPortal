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
const getURL = (req) => {
    const fullURL = `${req.protocol}://${req.get('host')}${req.originalUrl}`;
    return fullURL;
};
exports.successPrebookingDtl = async (req, res) => {
    try {
        const result = await otherDal.successPrebookingDtl(req.query.date);
        otherDal.addActivityLog('/successPrebookingDtl', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        otherDal.addActivityLog('/successPrebookingDtl', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.seedavailablity = async (req, res) => {
    try {
        var reqApiData = req.query.apiKey;
        if (reqApiData == 'AXkU758hy4g5MUGi3589FR45') {
            var DIST_CODE = req.query.DIST_CODE;
            var CROP_CODE = req.query.CROP_CODE;
            var VARIETY_CODE = req.query.VARIETY_CODE;
            const result = await otherDal.seedavailablity(DIST_CODE, CROP_CODE, VARIETY_CODE);
            otherDal.addActivityLog('/seedavailablity', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
            if (result.length > 0) {
                let a = result.length;
                for (let i = 0; i < result.length; i++) {
                    const result1 = await otherDal.getDealerData(result[i].LICENCE_NUMBER);
                    otherDal.addActivityLog('/getDealerData', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
                    if (result1.length > 0) {
                        console.log(result1.length);
                        result[i].LICENCE_NUMBER = result1[0].LICENCE_NUMBER;
                        result[i].DEALER_TYPE = result1[0].DEALER_TYPE;
                        result[i].CONTACT_NUMBER = result1[0].CONTACT_NUMBER;
                        result[i].FARM_NAME = result1[0].FARM_NAME;
                        result[i].BLOCK = result1[0].BLOCK;
                        result[i].ADDRESS = result1[0].ADDRESS;
                    }
                    // console.log(result);
                    if (a == i + 1) {
                        res.send(result);
                    }
                }

            }
        }
        else {
            otherDal.addActivityLog('/getDealerData', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'API key is not valid');
            res.send('API key is not valid')
        }
    } catch (e) {
        otherDal.addActivityLog('/seedavailablity', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};