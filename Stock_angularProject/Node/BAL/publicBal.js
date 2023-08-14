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
exports.getBlock = async (req, res) => {
    try {
        const result = await publicDal.getBlock(req.query.DistrictCode);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.getDealerDetails = async (req, res) => 
{
    try {
        const result = await publicDal.getDealerDetails(req.query.DistrictCode);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};

exports.dealerwisedata = async (req, res) => {
    try {
        res.setHeader('Access-Control-Allow-Origin', '*');
        res.setHeader('Access-Control-Allow-Methods', 'GET, POST, OPTIONS, PUT, PATCH, DELETE'); // If needed
        res.setHeader('Access-Control-Allow-Headers', 'X-Requested-With,content-type'); // If needed
        res.setHeader('Access-Control-Allow-Credentials', true);
        const result = await publicDal.dealerwisedata(req.query);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.allfinYr = async (req, res) => {
    try {
        res.setHeader('Access-Control-Allow-Origin', '*');
        res.setHeader('Access-Control-Allow-Methods', 'GET, POST, OPTIONS, PUT, PATCH, DELETE'); // If needed
        res.setHeader('Access-Control-Allow-Headers', 'X-Requested-With,content-type'); // If needed
        res.setHeader('Access-Control-Allow-Credentials', true);
        const result = await publicDal.allfinYr();
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.loadAllCrop = async (req, res) => {
    try {
        res.setHeader('Access-Control-Allow-Origin', '*');
        res.setHeader('Access-Control-Allow-Methods', 'GET, POST, OPTIONS, PUT, PATCH, DELETE'); // If needed
        res.setHeader('Access-Control-Allow-Headers', 'X-Requested-With,content-type'); // If needed
        res.setHeader('Access-Control-Allow-Credentials', true);
        const result = await publicDal.loadAllCrop();
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.loadAllDistrict = async (req, res) => {
    try {
        res.setHeader('Access-Control-Allow-Origin', '*');
        res.setHeader('Access-Control-Allow-Methods', 'GET, POST, OPTIONS, PUT, PATCH, DELETE'); // If needed
        res.setHeader('Access-Control-Allow-Headers', 'X-Requested-With,content-type'); // If needed
        res.setHeader('Access-Control-Allow-Credentials', true);
        const result = await publicDal.loadAllDistrict();
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.getSeason = async (req, res) => {
    try {
        res.setHeader('Access-Control-Allow-Origin', '*');
        res.setHeader('Access-Control-Allow-Methods', 'GET, POST, OPTIONS, PUT, PATCH, DELETE'); // If needed
        res.setHeader('Access-Control-Allow-Headers', 'X-Requested-With,content-type'); // If needed
        res.setHeader('Access-Control-Allow-Credentials', true);
        const result = await publicDal.getSeason(req.query.year);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};

exports.getblockWiseDealer = async (req, res) => 
{
    try {
        const result = await publicDal.getblockWiseDealer(req.query.BlockCode);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};

exports.getcropList = async (req, res) => 
{
    try {
        const result = await publicDal.getcropList();
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.graphVariety = async (req, res) => 
{
    try {
        const result = await publicDal.graphVariety(req.query.CropID);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};