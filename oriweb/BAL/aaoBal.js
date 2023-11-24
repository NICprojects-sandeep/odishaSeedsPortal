const ip = require('ip');
const UAParser = require('ua-parser-js');
const crypto = require('crypto');
const sha512 = require('js-sha512');
const aaoDal = require('../DAL/aaoDal');
const reqip = require('request-ip');
const  request = require('request');
var http = require('http');
var https = require('https');
const parser = new UAParser();

exports.paymentStatusByFarmeId = async (req, res) => {
    try {
        const result = await aaoDal.paymentStatusByFarmeId(req.body, req, res);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.GetDistCodeFromAOO = async (req, res) => {
    try {
        const result = await aaoDal.GetDistCodeFromAOO(req.session.userID, req, res);
        res.send({ result });
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.fillfARMERiD = async (req, res) => {
    try {
        const result = await aaoDal.fillfARMERiD(req.session.userID, req, res);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.gerFarmerDetailsForSamallMarginUpdatation = async (req, res) => {
    try {
        const result = await aaoDal.gerFarmerDetailsForSamallMarginUpdatation(req.query.NICFARMERID, req, res);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.farmerTypeUpdate = async (req, res) => {
    try {
        const result = await aaoDal.farmerTypeUpdate(req.body, req, res);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.delalerwisestockCheack = async (req, res) => {
    try {
        const result = await aaoDal.delalerwisestockCheack(req.body, req, res);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};