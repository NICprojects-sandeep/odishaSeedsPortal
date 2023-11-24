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
exports.FillCategoryId = async (req, res) => {
    try {
        const result = await adminDal.FillCategoryId();
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.getVarietywiseLift = async (req, res) => {
    try {
        const data = await adminDal.getVarietywiseLift(req.body);
        // console.log(result);

        const result = [];

        const uniqueCombinationSet = new Set();
        
        for (const item of data) {
          const key = `${item.Dist_Code}-${item.CROP_VERID}`;
        
          if (true) {
              uniqueCombinationSet.add(key);
              result.push(item);
          }
        }
        
        for (const key of uniqueCombinationSet) {
          const hasPACS = result.some((item) => item.Type === "PACS" && `${item.Dist_Code}-${item.CROP_VERID}` === key);
          const hasDealer = result.some((item) => item.Type === "Dealer" && `${item.Dist_Code}-${item.CROP_VERID}` === key);
        
          if (!hasPACS) {
              result.push({
                  ...result.find((item) => `${item.Dist_Code}-${item.CROP_VERID}` === key),
                  Type: "PACS",
                  sale: "0.00"
              });
          }
        
          if (!hasDealer) {
              result.push({
                  ...result.find((item) => `${item.Dist_Code}-${item.CROP_VERID}` === key),
                  Type: "Dealer",
                  sale: "0.00"
              });
          }
        }
        


        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};