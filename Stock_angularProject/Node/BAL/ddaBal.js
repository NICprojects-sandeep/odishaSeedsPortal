const ip = require('ip');
const UAParser = require('ua-parser-js');
const crypto = require('crypto');
const sha512 = require('js-sha512');
const ddaDal = require('../DAL/ddaDal');
const reqip = require('request-ip');
const request = require('request');
var http = require('http');
var https = require('https');
const parser = new UAParser();
const moment = require('moment');

const getURL = (req) => {
    const fullURL = `${req.protocol}://${req.get('host')}${req.originalUrl}`;
    return fullURL;
};
exports.allFillFinYr = async (req, res) => {
    try {
        const result = await ddaDal.allFillFinYr();
        ddaDal.addActivityLog('/allFillFinYr', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        ddaDal.addActivityLog('/allFillFinYr', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.FillCropCategory = async (req, res) => {
    try {
        const result = await ddaDal.FillCropCategory();
        ddaDal.addActivityLog('/FillCropCategory', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        ddaDal.addActivityLog('/FillCropCategory', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.FillCropByCategoryId = async (req, res) => {
    try {
        const result = await ddaDal.FillCropByCategoryId(req.query.SelectedCropCatagory);
        ddaDal.addActivityLog('/FillCropByCategoryId', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        ddaDal.addActivityLog('/FillCropByCategoryId', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.fillCurrentstockPosition = async (req, res) => {
    try {
        console.log(req.session);
        req.query.lgdistrict = req.session.nicdistCode
        const result = await ddaDal.fillCurrentstockPosition(req.query);
        ddaDal.addActivityLog('/fillCurrentstockPosition', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        ddaDal.addActivityLog('/fillCurrentstockPosition', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.fillGodownWiseStock = async (req, res) => {
    try {
        const result = await ddaDal.fillGodownWiseStock(req.query);
        ddaDal.addActivityLog('/fillGodownWiseStock', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        ddaDal.addActivityLog('/fillGodownWiseStock', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.FillDistrict = async (req, res) => {
    try {
        const result = await ddaDal.FillDistrict();
        ddaDal.addActivityLog('/FillDistrict', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        ddaDal.addActivityLog('/FillDistrict', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.fillStateStockPosition = async (req, res) => {
    try {
        req.body.SelectedDistrict = req.session.distCode;
        console.log(req.body);
        const result = await ddaDal.fillStateStockPosition(req.body);
        ddaDal.addActivityLog('/fillStateStockPosition', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        ddaDal.addActivityLog('/fillStateStockPosition', 'Error', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.FillCategoryId = async (req, res) => {
    try {
        const result = await ddaDal.FillCategoryId();
        ddaDal.addActivityLog('/FillCategoryId', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        ddaDal.addActivityLog('/FillCategoryId', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.blockwisetransctiondetails = async (req, res) => {
    try {
        const fullname = req.session.fullname;
        const extractedValue = fullname.split('-')[1].substring(0, 3);
        let result = await ddaDal.blockwisetransctiondetails(req.query.SelectedFinancialYear, req.query.SelectedSeason, req.query.SelectedCrop, req.session.distCode);
        let result1 = await ddaDal.blockwisetransctiondetails1(req.query.SelectedFinancialYear, req.query.SelectedSeason, req.query.SelectedCrop, extractedValue);
        const licNumbers = result1.map(item => item.LICENCE_NO);
        let result2 = await ddaDal.blockwisetransctiondetails2(licNumbers);
        result1 = result1.filter(itemA => {
            const matchB = result2.find(itemB => itemB.LIC_NO === itemA.LICENCE_NO);
            if (matchB) {
                itemA.BLOCK_ID = matchB.BLOCK_ID;

                return true;
            }
            return false;
        });

        const result3 = result1.reduce((acc, curr) => {
            if (curr.BLOCK_ID) {
                if (!acc[curr.BLOCK_ID]) {
                    acc[curr.BLOCK_ID] = { BLOCK_ID: curr.BLOCK_ID, rcv: 0, sale: 0 };
                }
                acc[curr.BLOCK_ID].rcv += parseFloat(curr.rcv);
                acc[curr.BLOCK_ID].sale += parseFloat(curr.sale);
            }
            return acc;
        }, {});

        const output = Object.values(result3);

        result.forEach(itemA => {
            const matchB = output.find(itemB => itemB.BLOCK_ID === itemA.BLK_CODE);
            if (matchB) {
                itemA.ACTUAL_RCV = matchB.rcv;
                itemA.ACTUAL_SALE = matchB.sale;

            } else {
                itemA.rcv = 0; // If no match is found, set rcv to 0
                itemA.sale = 0; // If no match is found, set rcv to 0

            }
            console.log(itemA);
        });



        ddaDal.addActivityLog('/blockwisetransctiondetails', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        ddaDal.addActivityLog('/blockwisetransctiondetails', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.blockwiseStockDetails = async (req, res) => {
    try {
        let result = await ddaDal.blockwiseStockDetails(req.query.BlockCode, req, res);
        const licNumbers = result.map(item => item.LIC_NO);
        let result2 = await ddaDal.blockwiseStockDetails2(licNumbers, req.query.SelectedFinancialYear, req.query.SelectedSeason, req.query.SelectedCrop, req, res);
        result = result.filter(itemA => {
            const matchB = result2.find(itemB => itemB.LICENCE_NO === itemA.LIC_NO);
            if (matchB) {
                itemA.ACTUAL_RECEIVE = matchB.ACTUAL_RECEIVE;
                itemA.ACTUAL_SALE = matchB.ACTUAL_SALE;
                itemA.SaleQty = matchB.SaleQty;
                itemA.CROP_ID = matchB.CROP_ID;
                itemA.FIN_YR = matchB.FIN_YR;
                itemA.SEASSION = matchB.SEASSION;

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
exports.delaeListDistWise = async (req, res) => {
    try {
        let result = await ddaDal.delaeListDistWise(req.session.distCode, req, res);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.dealerwisePaymentDetails = async (req, res) => {
    try {
        let result = await ddaDal.dealerwisePaymentDetails(req.query.LIC_NO, req, res);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.paymentStatusByFarmeId = async (req, res) => {
    try {
        const result = await ddaDal.paymentStatusByFarmeId(req.body, req, res);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.GetDistCodeFromDist = async (req, res) => {
    try {
        console.log(req.session.nicdistCode);
        req.query.distCode = req.session.nicdistCode
        const result = await ddaDal.GetDistCodeFromDist(req.query);
        ddaDal.addActivityLog('/GetDistCodeFromDist', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        ddaDal.addActivityLog('/GetDistCodeFromDist', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.GetDealerSaleDetails = async (req, res) => {
    try {
        let result = await ddaDal.GetDealerSaleDetails('01', req, res);
        const licNumbers = result.map(item => item.LIC_NO);
        let result1 = await ddaDal.GetDealerSaleDetails1(licNumbers, '2024-25', 'K', 'C002', req, res);
        let result2 = await ddaDal.GetDealerSaleDetails2(licNumbers, '2024-25', 'K', 'C002', req, res);


        res.send(result2);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.GetFarmerSaleDetails = async (req, res) => {
    try {
        let result = await ddaDal.GetFarmerSaleDetails();
        let result1 = await ddaDal.GetFarmerSaleDetails1();
        res.send({ saledetails: result, farmerdetails: result1 });
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.getMPRDistrict = async (req, res) => {
    try {
        const url = `https://odishaagrilicense.nic.in/user/getDistricts?appKey=JHR788DD42EWD5S5SD45XP5Y`;
        request.get(url, { json: true, strictSSL: false }, (error, response, body) => {
            if (error) {
                console.log(error);
                ddaDal.addActivityLog('/getMPRDistrict', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, error);

            } else {                
                    res.send( {mprDistrict:body,logindistrict:req.session.nicdistCode});
                // Handle the response body here
            }
        });
    } catch (error) {
        console.log(error, 'error');
        // Handle the error properly, log or throw it again
    }
};
exports.FillDistrictWiseBlock = async (req, res) => {
    try {
        const url = `https://odishaagrilicense.nic.in/user/getBlocksOfDistrict?districtCode=${req.query.districtCode}&appKey=JHR788DD42EWD5S5SD45XP5Y`;
        request.get(url, { json: true, strictSSL: false }, (error, response, body) => {
            if (error) {
                console.log(error);
                ddaDal.addActivityLog('/FillDistrictWiseBlock', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, error);

            } else {
                    res.send(body);
                // Handle the response body here
            }
        });
    } catch (error) {
        console.log(error, 'error');
        // Handle the error properly, log or throw it again
    }
};
exports.FillMPRYear = async (req, res) => {
    try {
        const url = `https://odishaagrilicense.nic.in/user/getYears?appKey=JHR788DD42EWD5S5SD45XP5Y`;
        request.get(url, { json: true, strictSSL: false }, (error, response, body) => {
            if (error) {
                console.log(error);
                ddaDal.addActivityLog('/FillDistrictWiseBlock', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, error);

            } else {                
                    res.send(body);
                // Handle the response body here
            }
        });
    } catch (error) {
        console.log(error, 'error');
        // Handle the error properly, log or throw it again
    }
};
exports.selectedDistrictMPR = async (req, res) => {
    try {
        const url = `https://odishaagrilicense.nic.in/user/getSeedDlrMpr?year=${req.body.SelectedYear}&month=${req.body.SelectedMonth}&blockCode=${req.body.SelectedBlock}&districtName=${req.body.SelectedDistrict}&appKey=JHR788DD42EWD5S5SD45XP5Y`;
        request.get(url, { json: true, strictSSL: false }, (error, response, body) => {
            if (error) {
                console.log(error);
                ddaDal.addActivityLog('/selectedDistrictMPR', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, error);

            } else {                
                    res.send(body);
                // Handle the response body here
            }
        });
    } catch (error) {
        console.log(error, 'error');
        // Handle the error properly, log or throw it again
    }
};
exports.selectedDistrictMPR2 = async (req, res) => {
    try {
        const url = `https://odishaagrilicense.nic.in/user/getSeedDlrMprAmount?year=${req.body.SelectedYear}&month=${req.body.SelectedMonth}&tempNo=${req.body.tempNo}&appKey=JHR788DD42EWD5S5SD45XP5Y`;
        request.get(url, { json: true, strictSSL: false }, (error, response, body) => {
            if (error) {
                console.log(error);
                ddaDal.addActivityLog('/selectedDistrictMPR', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, error);

            } else {                
                    res.send(body);
                // Handle the response body here
            }
        });
    } catch (error) {
        console.log(error, 'error');
        // Handle the error properly, log or throw it again
    }
};