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
        const result = await dealerDal.GetDealerLicenceByDistCodeUserType(req.session.distCode);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.GetDealerLicenceByDistCodeUserTypePacs = async (req, res) => {
    try {
        const result = await dealerDal.GetDealerLicenceByDistCodeUserTypePacs(req.session.distCode);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.FILLFINYR = async (req, res) => {
    try {
        const result = await dealerDal.FILLFINYR();
        console.log(req.session);
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
exports.FILL_GODOWN = async (req, res) => {
    try {
        const result = await dealerDal.FILL_GODOWN(req.session.distCode,req.query.prebookedsale);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.FILL_CROPCATAGORY = async (req, res) => {
    try {
        const result = await dealerDal.FILL_CROPCATAGORY(req.query.selectedGodown);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.FILLCROPNAME = async (req, res) => {
    try {
        const result = await dealerDal.FILLCROPNAME(req.query.selectedCategory,req.query.selectedGodown);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.FILLCROPVARIETY = async (req, res) => {
    try {
        const result = await dealerDal.FILLCROPVARIETY(req.query.selectedCrop,req.query.selectedCategory,req.query.selectedGodown);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.prebookingDetailsOfDealer = async (req, res) => {
    try {
        console.log(req.query);
        const result = await dealerDal.prebookingDetailsOfDealer(req.query.SelectedDealerOrPacs,req.session.distCode);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.fillAvailableStockDetails = async (req, res) => {
    try {
        console.log(req.query);
        const result = await dealerDal.fillAvailableStockDetails(req.query);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.getSupplyType = async (req, res) => {
    try {
        const result = await dealerDal.getSupplyType();
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.fillDealerSaleDeatils = async (req, res) => {
    try {
        console.log(req.session);
        req.body.DIST_CODE=req.session.DIST_CODE;
        req.body.distCode= req.session.distCode;
        req.body.nicdistCode= req.session.nicdistCode;
        req.body.UPDATED_BY= req.session.userID;
        req.body.ipAdress=reqip.getClientIp(req);

        const result = await dealerDal.fillDealerSaleDeatils(req.body);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};