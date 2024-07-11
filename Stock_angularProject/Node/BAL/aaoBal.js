const ip = require('ip');
const UAParser = require('ua-parser-js');
const crypto = require('crypto');
const sha512 = require('js-sha512');
const aaoDal = require('../DAL/aaoDal');
const reqip = require('request-ip');
const request = require('request');
var http = require('http');
var https = require('https');
const parser = new UAParser();

const getURL = (req) => {
    const fullURL = `${req.protocol}://${req.get('host')}${req.originalUrl}`;
    return fullURL;
};
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
exports.FillCategoryId = async (req, res) => {
    try {
        const result = await aaoDal.FillCategoryId();
        aaoDal.addActivityLog('/FillCategoryId', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        aaoDal.addActivityLog('/FillCategoryId', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
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
exports.physicalsaleentrydata = async (req, res) => {
    try {
        let result = await aaoDal.physicalsaleentrydata(req.session.userID.split('_')[1], req, res);
        const licNumbers = result.map(item => item.LIC_NO);
        let result2 = await aaoDal.physicalsaleentrydata2(licNumbers,req.body.selectedFinancialYear,req.body.selectedSeasons,req.body.selectedCrop, req, res);
        result = result.filter(itemA => {
            const matchB = result2.find(itemB => itemB.LICENCE_NO === itemA.LIC_NO);
            if (matchB) {
              itemA.ACTUAL_RECEIVE = matchB.ACTUAL_RECEIVE;
              itemA.ACTUAL_SALE = matchB.ACTUAL_SALE;
              itemA.SaleQty = matchB.SaleQty;
              itemA.CROP_ID = matchB.CROP_ID;
              itemA.FIN_YR = matchB.FIN_YR;
              itemA.SEASSION = matchB.SEASSION;
              itemA.balance = matchB.balance;
              return true;
            }
            return false;
          });
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.inserphysicalsaleentrydata = async (req, res) => {
    try {
        req.body.userID=req.session.userID
        const result = await aaoDal.inserphysicalsaleentrydata(req.body, req, res);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};


