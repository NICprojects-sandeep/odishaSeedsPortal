const ip = require('ip');
const UAParser = require('ua-parser-js');
const crypto = require('crypto');
const sha512 = require('js-sha512');
const publicDal = require('../DAL/publicDal');
const reqip = require('request-ip');
const request = require('request');
var http = require('http');
var https = require('https');
const parser = new UAParser();

exports.getStockPricelist = async (req, res) => {
    try {
        const result = await publicDal.getStockPricelist();
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.getDistrict = async (req, res) => {
    try {
        const result = await publicDal.getDistrict();
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.getDealerDetails = async (req, res) => {
    try {
        const result = await publicDal.getDealerDetails(req.query);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};