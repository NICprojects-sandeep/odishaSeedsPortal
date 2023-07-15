const ip = require('ip');
const UAParser = require('ua-parser-js');
const crypto = require('crypto');
const sha512 = require('js-sha512');
const dealerDal = require('../DAL/dealerDal');
const reqip = require('request-ip');
const  request = require('request');
var http = require('http');
var https = require('https');
const parser = new UAParser();

exports.GetDealerLicenceByDistCodeUserType = async (req, res) => {
    try {
        req.session.DIST_CODE= 371;
        const result = await dealerDal.GetDealerLicenceByDistCodeUserType(req.session.DIST_CODE);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.GetDealerLicenceByDistCodeUserTypePacs = async (req, res) => {
    try {
        req.session.DIST_CODE= 371;
        const result = await dealerDal.GetDealerLicenceByDistCodeUserTypePacs(req.session.DIST_CODE);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.FILLFINYR = async (req, res) => {
    try {
        const result = await dealerDal.FILLFINYR();
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.FILLSEASSION = async (req, res) => {
    try {
        const result = await dealerDal.FILLSEASSION(req.query);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};