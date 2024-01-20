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
exports.FillCropByStock_Farmer = async (req, res) => {
    try {
        console.log(req.query);
        const result = await adminDal.FillCropByStock_Farmer(req.query.SelectedFinancialYear);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.dealerPacsSale = async (req, res) => {
    try {
        // console.log(req.query);
        const data = await adminDal.dealerPacsSale(req.body);



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
        console.log(e);
        // res.status(500).send(e);
        // throw e;
    }
};

exports.dailyProgressReport = async (req, res) => {
    try {
        console.log(req.session);
        let data = {}
        const result = await adminDal.dailyProgressReport(data);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.previousYeardailyProgressReport = async (req, res) => {
    try {
        console.log(req.session);
        let data = {}
        const result = await adminDal.previousYeardailyProgressReport(data);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.getPFMSStatus = async (req, res) => {
    try {
        const result = await adminDal.getPFMSStatus();
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.distwisestockdetails = async (req, res) => {
    try {
        const result = await adminDal.distwisestockdetails(req.query);
        const result1 = result.pgdata.map(item1 => {
            const matchingItem = result.sqlData.find(item2 => item1.DIST_CODE === item2.DIST_CODE);
            return { ...item1, ...matchingItem };
        });
        res.send(result1);

    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.blockwisestockdetails = async (req, res) => {
    try {
        const result = await adminDal.blockwisestockdetails(req.query);
        const SaleQty = await adminDal.blockwiseSaleQtydetails(req.query);
        const result1 = result.pgdata.map(item1 => {
            const matchingItem = result.sqlData.find(item2 => item1.LIC_NO === item2.LICENCE_NO);
            return { ...item1, ...matchingItem };
        });
        const filteredArray = result1.filter(obj => obj.LICENCE_NO !== undefined);
        const sumMap = {};

        console.log(filteredArray, filteredArray.length);
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

        console.log(groupedData);
        // Convert the grouped data back to an array
        for (const key in groupedData) {
            result2.push(groupedData[key]);
        }
        console.log(result2);
        result2.sort((a, b) => a.BLOCK_ID.localeCompare(b.BLOCK_ID));
        const result3 = SaleQty.map(item1 => {
            // console.log(item1);
            const matchingItem = result2.find(item2 => item1.BLK_CODE === item2.BLOCK_ID);
            return { ...item1, ...matchingItem };
        });
        res.send(result3);

    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.dealerwisestockdetails = async (req, res) => {
    try {
        let result2 = [];
        const result = await adminDal.dealerwisestockdetails(req.query);
        for (const key in result) {
            result2.push("'" + result[key].LIC_NO + "'");
        }
        const result1 = await adminDal.dealerwise_stockdetails(result2,req.query);

        // console.log(result);
       
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
        console.log(result1);
        res.send(result1);

    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.schemewisedetails = async (req, res) => {
    try {
        const result = await adminDal.schemewisedetails(req.query);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};