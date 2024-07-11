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

const getURL = (req) => {
    const fullURL = `${req.protocol}://${req.get('host')}${req.originalUrl}`;
    return fullURL;
};
exports.allFillFinYr = async (req, res) => {
    try {
        const result = await adminDal.allFillFinYr();
        adminDal.addActivityLog('/allFillFinYr', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        adminDal.addActivityLog('/allFillFinYr', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.FillCropCategory = async (req, res) => {
    try {
        const result = await adminDal.FillCropCategory();
        adminDal.addActivityLog('/FillCropCategory', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        adminDal.addActivityLog('/FillCropCategory', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.FillCropByCategoryId = async (req, res) => {
    try {
        const result = await adminDal.FillCropByCategoryId(req.query.SelectedCropCatagory);
        adminDal.addActivityLog('/FillCropByCategoryId', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        adminDal.addActivityLog('/FillCropByCategoryId', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.fillCurrentstockPosition = async (req, res) => {
    try {
        const result = await adminDal.fillCurrentstockPosition(req.query);
        adminDal.addActivityLog('/fillCurrentstockPosition', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        adminDal.addActivityLog('/fillCurrentstockPosition', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.fillGodownWiseStock = async (req, res) => {
    try {
        const result = await adminDal.fillGodownWiseStock(req.query);
        adminDal.addActivityLog('/fillGodownWiseStock', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        adminDal.addActivityLog('/fillGodownWiseStock', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.FillDistrict = async (req, res) => {
    try {
        const result = await adminDal.FillDistrict();
        adminDal.addActivityLog('/FillDistrict', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        adminDal.addActivityLog('/FillDistrict', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.fillStateStockPosition = async (req, res) => {
    try {
        const result = await adminDal.fillStateStockPosition(req.body);
        adminDal.addActivityLog('/fillStateStockPosition', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        adminDal.addActivityLog('/fillStateStockPosition', 'Error', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.FillCategoryId = async (req, res) => {
    try {
        const result = await adminDal.FillCategoryId();
        adminDal.addActivityLog('/FillCategoryId', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        adminDal.addActivityLog('/FillCategoryId', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.getVarietywiseLift = async (req, res) => {
    try {
        const data = await adminDal.getVarietywiseLift(req.body);
        adminDal.addActivityLog('/getVarietywiseLift', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
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
        adminDal.addActivityLog('/getVarietywiseLift', 'Error', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.FillCropByStock_Farmer = async (req, res) => {
    try {
        console.log(req.query);
        const result = await adminDal.FillCropByStock_Farmer(req.query.SelectedFinancialYear);
        adminDal.addActivityLog('/FillCropByStock_Farmer', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        adminDal.addActivityLog('/FillCropByStock_Farmer', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.dealerPacsSale = async (req, res) => {
    try {
        // console.log(req.query);
        const data = await adminDal.dealerPacsSale(req.body);
        adminDal.addActivityLog('/dealerPacsSale', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');



        const result = [];
        const uniqueCombinationSet = new Set();
        for (const item of data.alldata) {
            const key = `${item.Dist_Code}-${item.CROP_VERID}`;
            if (true) {
                uniqueCombinationSet.add(key);
                result.push(item);
            }
        }

        for (const key of uniqueCombinationSet) {
            const hasPACS = result.some((item) => item.DealerPacks === "PACS" && `${item.Dist_Code}-${item.CROP_VERID}` === key);
            const hasDealer = result.some((item) => item.DealerPacks === "Dealer" && `${item.Dist_Code}-${item.CROP_VERID}` === key);
            if (!hasPACS) {
                result.push({
                    ...result.find((item) => `${item.Dist_Code}-${item.CROP_VERID}` === key),
                    DealerPacks: "PACS",
                    sale: "0.00"
                });
            }

            if (!hasDealer) {
                result.push({
                    ...result.find((item) => `${item.Dist_Code}-${item.CROP_VERID}` === key),
                    DealerPacks: "Dealer",
                    sale: "0.00"
                });
            }
        }
        const result1 = [];
        const uniqueCombinationSet1 = new Set();
        for (const item of data.noofdealerpacs) {
            const key = `${item.Dist_Code}-${item.Dist_Name}`;
            if (true) {
                uniqueCombinationSet1.add(key);
                result1.push(item);
            }
        }
        for (const key of uniqueCombinationSet1) {
            const hasPACS = result1.some((item) => item.DealerPacks === "PACS" && `${item.Dist_Code}-${item.Dist_Name}` === key);
            const hasDealer = result1.some((item) => item.DealerPacks === "Dealer" && `${item.Dist_Code}-${item.Dist_Name}` === key);
            if (!hasPACS) {
                result1.push({
                    ...result1.find((item) => `${item.Dist_Code}-${item.Dist_Name}` === key),
                    DealerPacks: "PACS",
                    noofd: "0"
                });
            }

            if (!hasDealer) {
                result1.push({
                    ...result1.find((item) => `${item.Dist_Code}-${item.Dist_Name}` === key),
                    DealerPacks: "Dealer",
                    noofd: "0"
                });
            }
        }


        res.send({ alldata: result, nooffarmer: data.nooffarmer, noofdealerpacs: result1 });
    } catch (e) {
        adminDal.addActivityLog('/dealerPacsSale', 'Error', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        console.log(e);
        // res.status(500).send(e);
        // throw e;
    }
};

exports.dailyProgressReport = async (req, res) => {
    try {
        let data = {}
        const result = await adminDal.dailyProgressReport(data);
        adminDal.addActivityLog('/dailyProgressReport', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        adminDal.addActivityLog('/dailyProgressReport', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.previousYeardailyProgressReport = async (req, res) => {
    try {
        let data = {}
        const result = await adminDal.previousYeardailyProgressReport(data);
        adminDal.addActivityLog('/previousYeardailyProgressReport', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        adminDal.addActivityLog('/previousYeardailyProgressReport', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.getPFMSStatus = async (req, res) => {
    try {
        const result = await adminDal.getPFMSStatus();
        adminDal.addActivityLog('/getPFMSStatus', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        adminDal.addActivityLog('/getPFMSStatus', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.distwisestockdetails = async (req, res) => {
    try {
        const result = await adminDal.distwisestockdetails(req.query);
        adminDal.addActivityLog('/distwisestockdetails', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        const result1 = result.pgdata.map(item1 => {
            const matchingItem = result.sqlData.find(item2 => item1.DIST_CODE === item2.DIST_CODE);
            return { ...item1, ...matchingItem };
        });
        res.send(result1);

    } catch (e) {
        adminDal.addActivityLog('/distwisestockdetails', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.blockwisestockdetails = async (req, res) => {
    try {
        const result = await adminDal.blockwisestockdetails(req.query);
        adminDal.addActivityLog('/blockwisestockdetails', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        const SaleQty = await adminDal.blockwiseSaleQtydetails(req.query);
        adminDal.addActivityLog('/blockwiseSaleQtydetails', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        const result1 = result.pgdata.map(item1 => {
            const matchingItem = result.sqlData.find(item2 => item1.LIC_NO === item2.LICENCE_NO);
            return { ...item1, ...matchingItem };
        });
        const filteredArray = result1.filter(obj => obj.LICENCE_NO !== undefined);
        const sumMap = {};
        filteredArray.forEach(item => {
            const blockId = item.BLOCK_ID;
            if (sumMap[blockId] === undefined) {
                sumMap[blockId] = { RCV: 0, SALE: 0, AAO_CODE: item.AAO_CODE };
            }
            sumMap[blockId].RCV += parseFloat(item.RCV);
            sumMap[blockId].SALE += parseFloat(item.SALE);
            sumMap[blockId].AAO_CODE = item.AAO_CODE;

        });
        // Convert the sumMap object back to an array of objects
        const summedArray = Object.entries(sumMap).map(([blockId, sums]) => ({
            BLOCK_ID: blockId,
            AAO_CODE: sums.AAO_CODE,
            RCV: sums.RCV,
            SALE: sums.SALE,
        }));
        const result2 = [];

        console.log(summedArray, 'summedArray');
        // Group objects by AAO_CODE
        const groupedData = summedArray.reduce((acc, obj) => {
            console.log(obj, 'obj');
            const key = obj.AAO_CODE;
            if (!acc[key]) {
                acc[key] = { ...obj };
            } else {
                acc[key].RCV += obj.RCV;
                acc[key].SALE += obj.SALE;
            }
            return acc;
        }, {});
        // Convert the grouped data back to an array
        for (const key in groupedData) {
            result2.push(groupedData[key]);
        }
        result2.sort((a, b) => a.BLOCK_ID.localeCompare(b.BLOCK_ID));
        const result3 = SaleQty.map(item1 => {
            const matchingItem = result2.find(item2 => item1.BLK_CODE === item2.BLOCK_ID);
            return { ...item1, ...matchingItem };
        });
        res.send(result3);
    } catch (e) {
        adminDal.addActivityLog('/blockwisestockdetails', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.dealerwisestockdetails = async (req, res) => {
    try {
        let result2 = [];
        const result = await adminDal.dealerwisestockdetails(req.query);
        adminDal.addActivityLog('/dealerwisestockdetails', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        for (const key in result) {
            result2.push("'" + result[key].LIC_NO + "'");
        }
        const result1 = await adminDal.dealerwise_stockdetails(result2, req.query);
        adminDal.addActivityLog('/dealerwise_stockdetails', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        for (let i = 0; i < result1.length; i++) {
            for (let j = 0; j < result.length; j++) {

                if (result1[i].LICENCE_NO === result[j].LIC_NO) {
                    // Merge properties from array b into array a
                    if (result[j].SaleQty == null) {
                        result[j].SaleQty = 0
                    }
                    else {
                        result[j].balance = result[j].SaleQty - result1[i].ACTUAL_SALE
                    }
                    console.log(result1[i].LICENCE_NO, result[j].LIC_NO);
                    result1[i] = { ...result1[i], ...result[j] };
                    break; // Once matched, no need to check further
                }
            }
        }
        res.send(result1);

    } catch (e) {
        adminDal.addActivityLog('/dealerwisestockdetails', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.schemewisedetails = async (req, res) => {
    try {
        const result = await adminDal.schemewisedetails(req.query);
        adminDal.addActivityLog('/schemewisedetails', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        adminDal.addActivityLog('/schemewisedetails', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.dealerwisewisesaledetails = async (req, res) => {
    try {
        const result = await adminDal.dealerwisewisesaledetails(req.body);
        adminDal.addActivityLog('/dealerwisewisesaledetails', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        adminDal.addActivityLog('/dealerwisewisesaledetails', 'Error', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.getPFSMTransctionDetails = async (req, res) => {
    try {
        let DEALER_CODE = [];
        const result = await adminDal.getPFSMTransctionDetails(req.query.enteredCPAID);
        adminDal.addActivityLog('/getPFSMTransctionDetails', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        if (result.length > 0) {
            for (let index = 0; index < result.length; index++) {
                DEALER_CODE.push(result[index].DEALER_CODE);
            }
            const result1 = await adminDal.getPFSMTransctionDetailsAllDealerCodeWise(req.query.enteredCPAID, DEALER_CODE);
            adminDal.addActivityLog('/getPFSMTransctionDetailsAllDealerCodeWise', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
            const dealerCodeWiseArray = Object.values(result1.reduce((acc, obj) => {
                const { DEALER_CODE } = obj;
                if (!acc[DEALER_CODE]) {
                    acc[DEALER_CODE] = [];
                }
                acc[DEALER_CODE].push(obj);
                return acc;
            }, {}));
            res.send({ pfms: result, dealerdata: dealerCodeWiseArray });
        }
        else {
            res.send({ pfms: result });
        }

    } catch (e) {
        adminDal.addActivityLog('/getPFSMTransctionDetails', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.getPFSMTransctionDetailsDealerCodeWise = async (req, res) => {
    try {
        const result = await adminDal.getPFSMTransctionDetailsDealerCodeWise(req.query);
        adminDal.addActivityLog('/getPFSMTransctionDetailsDealerCodeWise', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        adminDal.addActivityLog('/getPFSMTransctionDetailsDealerCodeWise', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.dealerPacsPaymentdetails = async (req, res) => {
    try {
        const result = await adminDal.dealerPacsPaymentdetails(req.query);
        adminDal.addActivityLog('/dealerPacsPaymentdetails', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        adminDal.addActivityLog('/dealerPacsPaymentdetails', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.subsidyInvolovementdetails = async (req, res) => {
    try {
        let data = {}
        const result = await adminDal.subsidyInvolovementdetails(req.query);
        adminDal.addActivityLog('/subsidyInvolovementdetails', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        data.fyear = req.query.SelectedFinancialYear;
        if (req.query.SelectedSeason == 'K') {
            data.season = 'Kharif';
        }
        else {
            data.season = 'Rabi';
        }
        data.currentdate = new Date();
        data.result = result
        res.send(data);
    } catch (e) {
        adminDal.addActivityLog('/subsidyInvolovementdetails', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.getAllUserType = async (req, res) => {
    try {
        const result = await adminDal.getAllUserType();
        adminDal.addActivityLog('/getAllUserType', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        adminDal.addActivityLog('/getAllUserType', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.getUserId = async (req, res) => {
    try {
        const result = await adminDal.getUserId(req.query);
        adminDal.addActivityLog('/getUserId', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        adminDal.addActivityLog('/getUserId', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.resetPassword = async (req, res) => {
    try {
        const result = await adminDal.resetPassword(req.body);
        adminDal.addActivityLog('/resetPassword', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        adminDal.addActivityLog('/resetPassword', 'Error', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.getprebookingDtl = async (req, res) => {
    try {
        const result = await adminDal.getprebookingDtl();
        if (result.totalprebookingdtl.length > 0) {
            for (let index = 0; index < result.totalprebookingdtl.length; index++) {
                const result1 = await adminDal.getAppFirmName(result.totalprebookingdtl[index].dealerId);
                if (result1.length > 0) {
                    result.totalprebookingdtl[index].APP_FIRMNAME = result1[0].APP_FIRMNAME;
                }
            }
        }
        adminDal.addActivityLog('/getprebookingDtl', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        adminDal.addActivityLog('/getprebookingDtl', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.getSearchprebookingDtl = async (req, res) => {
    try {
        const result = await adminDal.getSearchprebookingDtl(req.body);
        if (result.totalprebookingdtl.length > 0) {
            for (let index = 0; index < result.totalprebookingdtl.length; index++) {
                const result1 = await adminDal.getAppFirmName(result.totalprebookingdtl[index].dealerId);
                if (result1.length > 0) {
                    result.totalprebookingdtl[index].APP_FIRMNAME = result1[0].APP_FIRMNAME;
                }
            }

        }
        adminDal.addActivityLog('/getSearchprebookingDtl', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        adminDal.addActivityLog('/getSearchprebookingDtl', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.getPrebookingDistrict = async (req, res) => {
    try {
        const result = await adminDal.getPrebookingDistrict();
        adminDal.addActivityLog('/getPrebookingDistrict', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        adminDal.addActivityLog('/getPrebookingDistrict', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.getVariey = async (req, res) => {
    try {
        const result = await adminDal.getVariey(req.query);
        adminDal.addActivityLog('/getVariey', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        adminDal.addActivityLog('/getVariey', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.getSearchErupidata = async (req, res) => {
    try {
        const result = await adminDal.getSearchErupidata(req.query);
        if (result.length > 0) {
            for (let index = 0; index < result.length; index++) {
                const result1 = await adminDal.getdealerCodeWiseblockname(result[index].dealerCode);
                if (result1.length > 0) {
                    result[index].blockName = result1[0].block_name;
                    result[index].distName = result1[0].dist_name;
                }
            }
        }
        adminDal.addActivityLog('/getSearchErupidata', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        adminDal.addActivityLog('/getSearchErupidata', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.getErupiDistrict = async (req, res) => {
    try {
        const result = await adminDal.getErupiDistrict();
        adminDal.addActivityLog('/getErupiDistrict', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        adminDal.addActivityLog('/getErupiDistrict', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.getErupiBlock = async (req, res) => {
    try {
        const result = await adminDal.getErupiBlock(req.query);
        adminDal.addActivityLog('/getErupiBlock', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        adminDal.addActivityLog('/getErupiBlock', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.getParticularSearchErupidata = async (req, res) => {
    try {
        let dealerlist = [];
        if (req.body.selectedBlock == '0') {
            if (req.body.selectedDistrict == '0') {
            }
            else {
                dealerlist = await adminDal.getDistrictWiseDealerList(req.body.selectedDistrict);
            }

        }
        else {
            dealerlist = await adminDal.getBlockWiseDealerList(req.body.selectedBlock);
        }
        req.body.dealerlist = dealerlist;
        const result = await adminDal.getParticularSearchErupidata(req.body);
        if (result.length > 0) {
            for (let index = 0; index < result.length; index++) {
                console.log(result.length);
                const result1 = await adminDal.getdealerCodeWiseblockname(result[index].dealerCode);
                if (result1.length > 0) {
                    result[index].blockName = result1[0].block_name;
                    result[index].distName = result1[0].dist_name;
                }
            }
        }
        adminDal.addActivityLog('/getParticularSearchErupidata', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        adminDal.addActivityLog('/getParticularSearchErupidata', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.geterupiStockDetails = async (req, res) => {
    try {
        const result = await adminDal.geterupiStockDetails(req.body);
        adminDal.addActivityLog('/geterupiStockDetails', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        adminDal.addActivityLog('/geterupiStockDetails', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};