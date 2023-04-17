const ip = require('ip');
const UAParser = require('ua-parser-js');
const crypto = require('crypto');
const sha512 = require('js-sha512');
const farmersaleDal = require('../DAL/farmersaleDal');
const reqip = require('request-ip')
const parser = new UAParser();

exports.GetFirmName = async (req, res) => {
    try {
        const result = await farmersaleDal.getUserDetails(req.query.LIC_NO, req, res);
        res.send({ result });
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.GetFarmerInvHdr = async (req, res) => {
    try {
        const result = await farmersaleDal.GetFarmerInvHdr(req.query.farmerID, req, res);
        console.log(result,'jhjhjhj');
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};