const ip = require('ip');
const UAParser = require('ua-parser-js');
const crypto = require('crypto');
const sha512 = require('js-sha512');
const dealerDal = require('../DAL/dealerDal');
const reqip = require('request-ip');
const request = require('request');
var http = require('http');
var https = require('https');
const parser = new UAParser();
// const axios = require('axios');
const moment = require('moment');

const getURL = (req) => {
    const fullURL = `${req.protocol}://${req.get('host')}${req.originalUrl}`;
    return fullURL;
};
exports.GetDealerLicenceByDistCodeUserType = async (req, res) => {
    try {
        const result = await dealerDal.GetDealerLicenceByDistCodeUserType(req.session.distCode);
        dealerDal.addActivityLog('/GetDealerLicenceByDistCodeUserType', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        dealerDal.addActivityLog('/GetDealerLicenceByDistCodeUserType', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.GetDealerLicenceByDistCodeUserTypePacs = async (req, res) => {
    try {
        const result = await dealerDal.GetDealerLicenceByDistCodeUserTypePacs(req.session.distCode);
        dealerDal.addActivityLog('/GetDealerLicenceByDistCodeUserTypePacs', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        dealerDal.addActivityLog('/GetDealerLicenceByDistCodeUserTypePacs', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.FILLFINYR = async (req, res) => {
    try {
        const result = await dealerDal.FILLFINYR();
        dealerDal.addActivityLog('/FILLFINYR', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        dealerDal.addActivityLog('/FILLFINYR', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.FILLSEASSION = async (req, res) => {
    try {
        const result = await dealerDal.FILLSEASSION(req.query);
        dealerDal.addActivityLog('/FILLSEASSION', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        dealerDal.addActivityLog('/FILLSEASSION', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.FILL_GODOWN = async (req, res) => {
    try {
        const result = await dealerDal.FILL_GODOWN(req.session.distCode, req.query.prebookedsale);
        dealerDal.addActivityLog('/FILL_GODOWN', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');

        res.send(result);
    } catch (e) {
        dealerDal.addActivityLog('/FILL_GODOWN', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.FILL_CROPCATAGORY = async (req, res) => {
    try {
        const result = await dealerDal.FILL_CROPCATAGORY(req.query.selectedGodown);
        dealerDal.addActivityLog('/FILL_CROPCATAGORY', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        dealerDal.addActivityLog('/FILL_CROPCATAGORY', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.FILLCROPNAME = async (req, res) => {
    try {
        const result = await dealerDal.FILLCROPNAME(req.query.selectedCategory, req.query.selectedGodown);
        dealerDal.addActivityLog('/FILLCROPNAME', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        dealerDal.addActivityLog('/FILLCROPNAME', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.FILLCROPVARIETY = async (req, res) => {
    try {
        const result = await dealerDal.FILLCROPVARIETY(req.query.selectedCrop, req.query.selectedCategory, req.query.selectedGodown);
        dealerDal.addActivityLog('/FILLCROPVARIETY', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        dealerDal.addActivityLog('/FILLCROPVARIETY', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.prebookingDetailsOfDealer = async (req, res) => {
    try {
        const result = await dealerDal.prebookingDetailsOfDealer(req.query.SelectedDealerOrPacs, req.session.distCode);
        dealerDal.addActivityLog('/prebookingDetailsOfDealer', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        dealerDal.addActivityLog('/prebookingDetailsOfDealer', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.fillAvailableStockDetails = async (req, res) => {
    try {
        const result = await dealerDal.fillAvailableStockDetails(req.query);
        dealerDal.addActivityLog('/fillAvailableStockDetails', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        dealerDal.addActivityLog('/fillAvailableStockDetails', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.getSupplyType = async (req, res) => {
    try {
        const result = await dealerDal.getSupplyType();
        dealerDal.addActivityLog('/fillAvailableStockDetails', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        dealerDal.addActivityLog('/fillAvailableStockDetails', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.fillDealerSaleDeatils = async (req, res) => {
    try {
        req.body.distCode = req.session.DIST_CODE;
        req.body.distCode = req.session.distCode_1;
        req.body.nicdistCode = req.session.nicdistCode;
        req.body.UPDATED_BY = req.session.userID;
        req.body.ipAdress = reqip.getClientIp(req);

        const result = await dealerDal.fillDealerSaleDeatils(req.body);
        dealerDal.addActivityLog('/fillDealerSaleDeatils', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');



        // res.send(result);




        // let objUserBel1 = {};
        // objUserBel1.dist_Code = (req.body.nicdistCode).toString();
        // objUserBel1.APIKEY = "key01001";
        // const apiUrl = 'https://osscpayment.nic.in/addSellStockbySIS';

        // for (const e of req.body.VALUES) {
        //     objUserBel1.lot_Number = e.LOT_NO;
        //     objUserBel1.source_ID = e.Godown_ID;
        //     objUserBel1.sourceType = "GoDown";
        //     objUserBel1.receiver_ID = req.body.SALE_TO;
        //     objUserBel1.receiverType = req.body.IS_PACS == 'true' ? 'PACS' : 'Dealer';
        //     objUserBel1.partyName = req.body.APP_FIRMNAME;
        //     objUserBel1.qty_Per_Bag_Kg = e.BAG_SIZE_KG;
        //     objUserBel1.no_of_Bag = e.NO_OF_BAGS;
        //     objUserBel1.date_sale = moment(req.body.SALE_DATE).format('DD/MM/YYYY');
        //     objUserBel1.year = req.body.FIN_YR;
        //     objUserBel1.season = req.body.SEASSION;
        //     objUserBel1.ch_Number = req.body.DD_NUMBER;
        //     objUserBel1.ch_Date = moment(req.body.SALE_DATE).format('DD/MM/YYYY');
        //     objUserBel1.crop = e.CROP_ID;
        //     objUserBel1.cropName = e.Crop_Name;
        //     objUserBel1.variety = e.CROP_VERID;
        //     objUserBel1.varietyName = e.Crop_VerName;
        //     objUserBel1.Class = e.Class;
        //     objUserBel1.UserID = req.body.UPDATED_BY;
        //     objUserBel1.UserIP = req.body.ipAdress;
        //     objUserBel1.CropCatg_ID = e.CATEGORY_ID;
        //     objUserBel1.CASH_MEMO_NO = result.CASH_MEMO_NO;
        //     try {
        //         request({
        //             url: "https://osscpayment.nic.in/addSellStockbySIS",
        //             method: "POST",
        //             json: true,   // <--Very important!!!
        //             body: objUserBel1
        //         }, async function (error, response, body) {
        //             if (error) {
        //                 console.log(error);
        //             }
        //             else {
        //                 const result1 = await dealerDal.updateSaledetails(result.CASH_MEMO_NO, e.LOT_NO);
        //             }
        //         });
        //         // const response = await axios.post(apiUrl, objUserBel1);


        //         // Handle the response accordingly
        //     } catch (error) {
        //         console.log(error, 'error');
        //         // Handle the error properly, log or throw it again
        //     }
        //     // try {
        //     //     const response = await axios.post(apiUrl, objUserBel1);
        //     //     const result1 = await dealerDal.updateSaledetails(result.CASH_MEMO_NO,e.LOT_NO);

        //     //     // Handle the response accordingly
        //     // } catch (error) {
        //     //     console.log(error,'error');
        //     //     // Handle the error properly, log or throw it again
        //     // }
        // }
        setTimeout(() => {
            res.send(result);
        }, 5000);

    } catch (e) {
        dealerDal.addActivityLog('/fillDealerSaleDeatils', 'Error', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        // console.log(e);
        // throw e;
    }
};
exports.cashmemodetails = async (req, res) => {
    try {
        const result = await dealerDal.cashmemodetails(req.query.applicationid, req.session.userID);
        dealerDal.addActivityLog('/cashmemodetails', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        dealerDal.addActivityLog('/cashmemodetails', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.FillLots = async (req, res) => {
    try {
        const result = await dealerDal.FillLots(req.session.userID);
        dealerDal.addActivityLog('/FillLots', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        dealerDal.addActivityLog('/FillLots', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.FillCrop = async (req, res) => {
    try {
        const result = await dealerDal.FillCrop();
        dealerDal.addActivityLog('/FillCrop', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        dealerDal.addActivityLog('/FillCrop', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.FillVariety = async (req, res) => {
    try {
        const result = await dealerDal.FillVariety(req.query.selectedCrop);
        dealerDal.addActivityLog('/getVarietywiseLift', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        dealerDal.addActivityLog('/getVarietywiseLift', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.addinClass = async (req, res) => {
    try {
        req.body.UPDATED_BY = req.session.userID;
        req.body.ipAdress = reqip.getClientIp(req);

        const result = await dealerDal.addinClass(req.body);
        dealerDal.addActivityLog('/addinClass', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');

        res.send(result);

    } catch (e) {
        dealerDal.addActivityLog('/addinClass', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        // throw e;
    }
};
exports.allFillFinYr = async (req, res) => {
    try {
        const result = await dealerDal.allFillFinYr();
        dealerDal.addActivityLog('/allFillFinYr', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        dealerDal.addActivityLog('/allFillFinYr', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.FillCropCategory = async (req, res) => {
    try {
        const result = await dealerDal.FillCropCategory();
        dealerDal.addActivityLog('/FillCropCategory', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        dealerDal.addActivityLog('/FillCropCategory', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.FillCropByCategoryId = async (req, res) => {
    try {
        const result = await dealerDal.FillCropByCategoryId(req.query.SelectedCropCatagory);
        dealerDal.addActivityLog('/FillCropByCategoryId', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        dealerDal.addActivityLog('/FillCropByCategoryId', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.fillGodownwisestock = async (req, res) => {
    try {
        req.query.DIST_CODE = req.session.distCode;
        const result = await dealerDal.fillGodownwisestock(req.query);
        dealerDal.addActivityLog('/fillGodownwisestock', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        dealerDal.addActivityLog('/fillGodownwisestock', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.receivedetails = async (req, res) => {
    try {
        req.body.distCode = req.session.distCode
        const result = await dealerDal.receivedetails(req.body);
        dealerDal.addActivityLog('/receivedetails', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        dealerDal.addActivityLog('/receivedetails', 'Error', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.dateWiseSaleDetails = async (req, res) => {
    try {
        req.body.distCode = req.session.distCode
        const result = await dealerDal.dateWiseSaleDetails(req.body);
        dealerDal.addActivityLog('/dateWiseSaleDetails', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        if (result.length > 0) {
            const result1 = await dealerDal.dateWiseSaleDetailswithdealerdata(result);
            dealerDal.addActivityLog('/dateWiseSaleDetailswithdealerdata', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
            res.send(result1);
        }
        else {
            res.send(result);
        }

    } catch (e) {
        dealerDal.addActivityLog('/dateWiseSaleDetails', 'Error', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.dateWiseGodownTransferDetails = async (req, res) => {
    try {
        req.body.distCode = req.session.distCode
        const result = await dealerDal.dateWiseGodownTransferDetails(req.body);
        dealerDal.addActivityLog('/dateWiseGodownTransferDetails', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        dealerDal.addActivityLog('/dateWiseGodownTransferDetails', 'Error', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.saledetails = async (req, res) => {
    try {
        req.body.distCode = req.session.distCode
        const result = await dealerDal.saledetails(req.body);
        dealerDal.addActivityLog('/saledetails', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        if (result.length > 0) {
            const result1 = await dealerDal.saledetailswithdealerdata(result);
            dealerDal.addActivityLog('/saledetailswithdealerdata', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
            res.send(result1);
        }
        else {
            res.send(result);
        }

    } catch (e) {
        dealerDal.addActivityLog('/saledetails', 'Error', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.getGodownmaster = async (req, res) => {
    try {
        const result = await dealerDal.getGodownmaster(req.query);
        dealerDal.addActivityLog('/getGodownmaster', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        dealerDal.addActivityLog('/getGodownmaster', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.GetDistCodeFromDist = async (req, res) => {
    try {
        req.query.distCode = req.session.distCode
        const result = await dealerDal.GetDistCodeFromDist(req.query);
        dealerDal.addActivityLog('/GetDistCodeFromDist', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        dealerDal.addActivityLog('/GetDistCodeFromDist', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.paymentStatusByFarmeId = async (req, res) => {
    try {
        const result = await dealerDal.paymentStatusByFarmeId(req.body);
        dealerDal.addActivityLog('/paymentStatusByFarmeId', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);

    } catch (e) {
        dealerDal.addActivityLog('/paymentStatusByFarmeId', 'Error', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.submitSeedSubsidyOfGrountnut = async (req, res) => {
    try {
        req.body.UPDATED_BY = req.session.userID;
        const result = await dealerDal.submitSeedSubsidyOfGrountnut(req.body);
        dealerDal.addActivityLog('/submitSeedSubsidyOfGrountnut', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);

    } catch (e) {
        dealerDal.addActivityLog('/submitSeedSubsidyOfGrountnut', 'Error', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.ddutrnocheack = async (req, res) => {
    try {
        const result = await dealerDal.ddutrnocheack(req.body);
        dealerDal.addActivityLog('/ddutrnocheack', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        dealerDal.addActivityLog('/ddutrnocheack', 'Error', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.FillGoDownByDistCodeUserType = async (req, res) => {
    try {
        const result = await dealerDal.FillGoDownByDistCodeUserType(req.session.distCode_1);
        dealerDal.addActivityLog('/FillGoDownByDistCodeUserType', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        dealerDal.addActivityLog('/FillGoDownByDistCodeUserType', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.FillAgencyByOSSC = async (req, res) => {
    try {
        const result = await dealerDal.FillAgencyByOSSC();
        dealerDal.addActivityLog('/FillAgencyByOSSC', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        dealerDal.addActivityLog('/FillAgencyByOSSC', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.FillSourceByAgencyIdUserTypeValues = async (req, res) => {
    try {
        const result = await dealerDal.FillSourceByAgencyIdUserTypeValues(req.query.AgenciesID);
        dealerDal.addActivityLog('/FillSourceByAgencyIdUserTypeValues', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        dealerDal.addActivityLog('/FillSourceByAgencyIdUserTypeValues', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.FillGovtFarmByDistCode = async (req, res) => {
    try {
        const result = await dealerDal.FillGovtFarmByDistCode(req.session.distCode_1, req.query.AgenciesID);
        dealerDal.addActivityLog('/FillGovtFarmByDistCode', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        dealerDal.addActivityLog('/FillGovtFarmByDistCode', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.agencyNameReload = async (req, res) => {
    try {
        const result = await dealerDal.agencyNameReload(req.session.distCode_1, req.query.selectedScheme);
        dealerDal.addActivityLog('/agencyNameReload', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        dealerDal.addActivityLog('/agencyNameReload', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.FillCropVarietyByOutsideAgencies = async (req, res) => {
    try {
        const result = await dealerDal.FillCropVarietyByOutsideAgencies(req.query.Crop_Code);
        dealerDal.addActivityLog('/FillCropVarietyByOutsideAgencies', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        dealerDal.addActivityLog('/FillCropVarietyByOutsideAgencies', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.FillCropVarietyByGovtFarm = async (req, res) => {
    try {
        req.query.distcode = req.session.distCode_1;
        const result = await dealerDal.FillCropVarietyByGovtFarm(req.query);
        dealerDal.addActivityLog('/FillCropVarietyByGovtFarm', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        dealerDal.addActivityLog('/FillCropVarietyByGovtFarm', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.FillCropVarietyByOUAT = async (req, res) => {
    try {
        req.query.distcode = req.session.distCode_1;
        const result = await dealerDal.FillCropVarietyByOUAT(req.query);
        dealerDal.addActivityLog('/FillCropVarietyByOUAT', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        dealerDal.addActivityLog('/FillCropVarietyByOUAT', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.FillCropVarietyByMOUAgency = async (req, res) => {
    try {
        req.query.distcode = req.session.distCode_1;
        const result = await dealerDal.FillCropVarietyByMOUAgency(req.query);
        dealerDal.addActivityLog('/FillCropVarietyByMOUAgency', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        dealerDal.addActivityLog('/FillCropVarietyByMOUAgency', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.FillCropVarietyByCropIdScheme = async (req, res) => {
    try {
        req.query.distcode = req.session.distCode_1;
        const result = await dealerDal.FillCropVarietyByCropIdScheme(req.query);
        dealerDal.addActivityLog('/FillCropVarietyByCropIdScheme', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        dealerDal.addActivityLog('/FillCropVarietyByCropIdScheme', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.FillLotByGovtFarm = async (req, res) => {
    try {
        const result = await dealerDal.FillLotByGovtFarm(req.query);
        dealerDal.addActivityLog('/FillLotByGovtFarm', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        dealerDal.addActivityLog('/FillLotByGovtFarm', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.fillBagExpiryDate = async (req, res) => {
    try {
        const result = await dealerDal.fillBagExpiryDate(req.query);
        dealerDal.addActivityLog('/fillBagExpiryDate', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        dealerDal.addActivityLog('/fillBagExpiryDate', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.fillBagsFromStockStockDetails = async (req, res) => {
    try {
        const result = await dealerDal.fillBagsFromStockStockDetails(req.query);
        dealerDal.addActivityLog('/fillBagsFromStockStockDetails', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        dealerDal.addActivityLog('/fillBagsFromStockStockDetails', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.Stock_Sp_InsReceiveDetails = async (req, res) => {
    try {
        req.body.USERID = req.session.userID;
        req.body.USERIP = reqip.getClientIp(req);
        const result = await dealerDal.Stock_Sp_InsReceiveDetails(req.body);
        dealerDal.addActivityLog('/Stock_Sp_InsReceiveDetails', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        // res.send(result);

        let objUserBEL1 = {};
        objUserBEL1.dist_Code = req.session.nicdistCode.toString();
        objUserBEL1.year = req.body.FIN_YR;
        if (req.body.SESSION == 'R') {
            objUserBEL1.season = 'Rabi'
        }
        else {
            objUserBEL1.season = 'Kharif'
        }
        objUserBEL1.pr_Number = req.body.Challan_No;

        objUserBEL1.source_ID = req.body.Receive_Unitcd;
        objUserBEL1.receiver_ID = req.body.Godown_ID;
        objUserBEL1.receiverType = "godown";
        objUserBEL1.date_Intake = req.body.mReceive_Date;
        objUserBEL1.agencyName = req.body.AgName;
        objUserBEL1.SourceType = req.body.SourceType;

        objUserBEL1.UserID = req.body.USERID;
        objUserBEL1.UserIP = req.body.USERIP;
        for (const e of req.body.XML_Value) {
            objUserBEL1.CropCatg_ID = e.CATEGORY_ID;
            objUserBEL1.lot_Number = e.enteredLotno;
            objUserBEL1.qty_Per_Bag_Kg = e.enteredBagsize;
            objUserBEL1.no_of_Bag = e.enteredRecvNoOfBags;
            objUserBEL1.crop = e.CROP_ID;
            objUserBEL1.variety = e.Variety_Code;
            objUserBEL1.cropName = e.Crop_Name;
            objUserBEL1.varietyName = e.Variety_Name;
            objUserBEL1.Class = e.CROP_CLASS;
            try {
                request({
                    url: "https://osscpayment.nic.in/addStockbySIS",
                    method: "POST",
                    json: true,   // <--Very important!!!
                    body: objUserBEL1
                }, async function (error, response, body) {
                    if (error) {
                        console.log(error);
                    }
                    else {
                        // const result1 = await dealerDal.updateSaledetails(result.CASH_MEMO_NO, e.LOT_NO);
                    }
                });
                // const response = await axios.post(apiUrl, objUserBel1);


                // Handle the response accordingly
            } catch (error) {
                console.log(error, 'error');
                // Handle the error properly, log or throw it again
            }
            // try {
            //     const response = await axios.post(apiUrl, objUserBel1);
            //     const result1 = await dealerDal.updateSaledetails(result.CASH_MEMO_NO,e.LOT_NO);

            //     // Handle the response accordingly
            // } catch (error) {
            //     console.log(error,'error');
            //     // Handle the error properly, log or throw it again
            // }
        }
        setTimeout(() => {
            res.send(result);
        }, 3000);

    } catch (e) {
        dealerDal.addActivityLog('/Stock_Sp_InsReceiveDetails', 'Error', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};