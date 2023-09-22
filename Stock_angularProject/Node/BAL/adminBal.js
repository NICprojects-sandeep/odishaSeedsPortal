const ip = require('ip');
const UAParser = require('ua-parser-js');
const crypto = require('crypto');
const sha512 = require('js-sha512');
const adminDal = require('../DAL/adminDal');
const reqip = require('request-ip');
const request = require('request');
var http = require('http');
var https = require('https');
const parser = new UAParser();
const axios = require('axios');
const moment = require('moment');


exports.allFillFinYr = async (req, res) => {
    try {
        const result = await adminDal.allFillFinYr();
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.FillCropCategory = async (req, res) => {
    try {
        const result = await adminDal.FillCropCategory();
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.FillCropByCategoryId = async (req, res) => {
    try {
        const result = await adminDal.FillCropByCategoryId(req.query.SelectedCropCatagory);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.fillCurrentstockPosition = async (req, res) => {
    try {
        const result = await adminDal.fillCurrentstockPosition(req.query);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.fillGodownWiseStock = async (req, res) => {
    try {
        const result = await adminDal.fillGodownWiseStock(req.query);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.FillDistrict = async (req, res) => {
    try {
        const result = await adminDal.FillDistrict();
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.fillStateStockPosition = async (req, res) => {
    try {
        const result = await adminDal.fillStateStockPosition(req.body);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};