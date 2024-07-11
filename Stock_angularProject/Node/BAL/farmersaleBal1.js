const ip = require('ip');
const UAParser = require('ua-parser-js');
const crypto = require('crypto');
const sha512 = require('js-sha512');
const farmersaleDal = require('../DAL/farmersaleDal');
const reqip = require('request-ip');
const request = require('request');
var http = require('http');
var https = require('https');
const parser = new UAParser();

const getURL = (req) => {
    const fullURL = `${req.protocol}://${req.get('host')}${req.originalUrl}`;
    return fullURL;
};
exports.GetFirmName = async (req, res) => {
    try {

        const result = await farmersaleDal.getUserDetails(req.session.LIC_NO, req, res);
        farmersaleDal.addActivityLog('/getUserDetails', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send({ result });
    } catch (e) {
        farmersaleDal.addActivityLog('/getUserDetails', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.GetFarmerInvHdr = async (req, res) => {
    try {
        const today = new Date();
        const result = await farmersaleDal.GetFarmerInvHdr(req.query.farmerID, req, res);
        farmersaleDal.addActivityLog('/GetFarmerInvHdr', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        result.today = new Date();
        res.send(result);
    } catch (e) {
        farmersaleDal.addActivityLog('/GetFarmerInvHdr', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};

exports.GetFarmerInv = async (req, res) => {
    try {
        const result = await farmersaleDal.GetFarmerInv(req.query, req, res);
        farmersaleDal.addActivityLog('/GetFarmerInv', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        farmersaleDal.addActivityLog('/GetFarmerInv', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.GetFarmerDtl = async (req, res) => {
    try {
        const result = await farmersaleDal.GetFarmerDtl(req.query, req, res);
        farmersaleDal.addActivityLog('/GetFarmerDtl', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        farmersaleDal.addActivityLog('/GetFarmerDtl', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.RptDateWiseSale = async (req, res) => {
    try {
        req.query.LicNo = req.session.LIC_NO
        const result = await farmersaleDal.RptDateWiseSale(req.query, req, res);
        farmersaleDal.addActivityLog('/RptDateWiseSale', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        const result1 = await farmersaleDal.RptDateWiseSalewithFarmerdata(result);
        farmersaleDal.addActivityLog('/RptDateWiseSalewithFarmerdata', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result1);
    } catch (e) {
        farmersaleDal.addActivityLog('/RptDateWiseSale', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
//////////////////////////////////////////////////////////////////// postgressql///////////////////////////////
exports.GETDISTCODEFROMLICNO = async (req, res) => {
    try {
        console.log(req.session.userID, req.session.mobileNo);
        let result = await farmersaleDal.GETDISTCODEFROMLICNO(req.session.LIC_NO);
        console.log(req.session);
        result[0].emailid = req.session.userID;
        result[0].mobileNo = req.session.mobileNo;
        result[0].LIC_NO = req.session.LIC_NO1;
        result[0].subsidyModeToERUPI = req.session.subsidyModeToERUPI;


        farmersaleDal.addActivityLog('/GETDISTCODEFROMLICNO', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        console.log(result);

        res.send(result);
    } catch (e) {
        farmersaleDal.addActivityLog('/GETDISTCODEFROMLICNO', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.getStockReceivedData = async (req, res) => {
    try {
        const result = await farmersaleDal.getStockReceivedData(req.query, req, res);
        farmersaleDal.addActivityLog('/getStockReceivedData', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        farmersaleDal.addActivityLog('/getStockReceivedData', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.getPreBookingDetails = async (req, res) => {
    try {
        console.log(req.session);
        req.query.LicNo = req.session.LIC_NO1
        const result = await farmersaleDal.getPreBookingDetails(req.query, req, res);
        farmersaleDal.addActivityLog('/getPreBookingDetails', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        farmersaleDal.addActivityLog('/getPreBookingDetails', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};


exports.sendOtp = async (req, res, next) => {
    try {
        var otp = Math.floor(100000 + Math.random() * 900000)
        var sms = `Your Transaction OTP for Seed Purchase is ${otp}.DAFP AGRIOR`;
        var mobileNo = req.query.MobileNo;
        req.query.otp = otp;

        request({
            url: `https://mkuy.apicol.nic.in/Registration/EPestSMSNew?template_id=1107165150759207123&type=OTP&mobileNo=${mobileNo}&sms=${sms}`,
            json: true,
            strictSSL: false
        }, (err, resp, body) => {
            if (err) {
                farmersaleDal.addActivityLog('/sendOtp', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, err);
                console.log(err);
                res.send(false);
            } else {
                const result = farmersaleDal.createOtp(req.query);
                farmersaleDal.addActivityLog('/sendOtp', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
                res.send(true);
            }
        });

        // res.send(true)
        //another

        //     var sms = "Your Transaction OTP for Seed Purchase is " + 909900 + " . DAFP AGRIOR";
        //     var mobileNo = 8093274461;


        //     var url = new URL(`https://mkuy.apicol.nic.in/Registration/EPestSMSNew?template_id=1107165150759207123&type=SMS&mobileNo=${mobileNo}&sms=${sms}`)
        //     var client = http; /* default  client */
        //     // You can use url.protocol as well 
        //     /*if (url.toString().indexOf("https") === 0){
        //                 client = https;
        //     }*/
        //     /* Enhancement : using the  URL.protocol  parameter
        //      * the  URL  object ,  provides a  parameter url.protocol that gives you 
        //      * the protocol  value  ( determined  by the  protocol ID  before 
        //      * the ":" in the  url. 
        //      * This makes it easier to  determine the protocol, and to  support other  
        //      * protocols like ftp ,  file  etc) 
        //      */
        //    client = (url.protocol == "https:") ? https : client; 
        //     var req = client.get(url, function(res){
        //         console.log(res);
        //     });

        // console.log(`http://mkuy.apicol.nic.in/Registration/EPestSMSNew?template_id=1107165150759207123&type=SMS&mobileNo=${mobileNo}&sms=${sms}    `);



    }
    catch (e) {
        farmersaleDal.addActivityLog('/sendOtp', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        console.error(e);
    }
};
exports.ValidateOTP = async (req, res) => {
    try {
        req.query.ipAdress = reqip.getClientIp(req);
        const result = await farmersaleDal.ValidateOTP(req.query);
        farmersaleDal.addActivityLog('/ValidateOTP', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        farmersaleDal.addActivityLog('/ValidateOTP', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.FillCrops = async (req, res) => {
    try {
        req.query.LicNo = req.session.LIC_NO
        const result = await farmersaleDal.FillCrops(req.query);
        farmersaleDal.addActivityLog('/FillCrops', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        farmersaleDal.addActivityLog('/FillCrops', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.FillVariety = async (req, res) => {
    try {
        req.query.LicNo = req.session.LIC_NO
        const result = await farmersaleDal.FillVariety(req.query);
        farmersaleDal.addActivityLog('/FillVariety', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        farmersaleDal.addActivityLog('/FillVariety', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.FILLFINYR = async (req, res) => {
    try {
        const result = await farmersaleDal.FILLFINYR();
        farmersaleDal.addActivityLog('/FILLFINYR', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        farmersaleDal.addActivityLog('/FILLFINYR', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.FILLSEASSION = async (req, res) => {
    try {
        const result = await farmersaleDal.FILLSEASSION(req.query);
        farmersaleDal.addActivityLog('/FILLSEASSION', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        farmersaleDal.addActivityLog('/FILLSEASSION', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.FILLDEALERSTOCK = async (req, res) => {
    try {
        req.query.LIC_NO = req.session.LIC_NO
        const result = await farmersaleDal.FILLDEALERSTOCK(req.query);
        farmersaleDal.addActivityLog('/FILLDEALERSTOCK', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        farmersaleDal.addActivityLog('/FILLDEALERSTOCK', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};

exports.GETFARMERINFO = async (req, res) => {
    try {
        const url = `https://dbtmbdodisha.nic.in/dafp/getFarmerDetails?NICFARMERID=${req.query.FARMER_ID}&appKey=JHR768BJSDFDS789SDNGK8GH4DFGF2RFTG`;

        request.get(url, { json: true, strictSSL: false }, async (error, response, body) => {
            if (error) {
                authDAL.addActivityLog('/GETFARMERINFO', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, error);
                console.error('Error:', error);
            } else {
                if(body.length >0){
                    if (body[0].Status == 'ACCP') {
                        const result = await farmersaleDal.GETFARMERINFO(req.query.FARMER_ID);
                        farmersaleDal.addActivityLog('/GETFARMERINFO', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
                        result.STATUS = body[0].Status;
                        res.send(result);
                    }
                }else{
                    res.send();
                }
                
            }
        });
    } catch (e) {
        farmersaleDal.addActivityLog('/GETFARMERINFO', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};

exports.InsertSaleDealer = async (req, res) => {
    try {
        let url = 'https://stgsugam.csm.co.in/admin/integration/voucherDetails'
        req.body.LICENCE_NO = req.session.LIC_NO
        req.body.DIST_CODE = await farmersaleDal.GetDistCodeByLicNo(req.body)//session
        farmersaleDal.addActivityLog('/GetDistCodeByLicNo', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        req.body.DAO_CD = await farmersaleDal.GetDAOCodeByLicNo(req.body)//session
        farmersaleDal.addActivityLog('/GetDAOCodeByLicNo', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        req.body.LIC_NO = req.session.LIC_NO //session
        req.body.USERIP = reqip.getClientIp(req);
        req.body.distCode = req.session.distCode;
        req.body.ipAdress = reqip.getClientIp(req);
        req.body.subsidyModeToERUPI = req.session.subsidyModeToERUPI;
        req.body.subsidyModeToERUPI == '0' ? req.body.subsidyModeToERUPI = 'PFMS' : req.body.subsidyModeToERUPI = 'eRupi';
        const result = await farmersaleDal.InsertSaleDealer(req.body);
        if (result.result == 'True') {
            // console.log(parseFloat(result.totalamount - result.totalsubsidyamount), parseFloat((result.totalamount - result.totalsubsidyamount).toFixed(2)));
           
           if(req.session.subsidyModeToERUPI == '1'){           
            let erupiDetails = {
                "TRANSACTION_ID": result.TRANSACTION_ID,
                "districtId": req.session.nicdistCode,
                "dealerCode": req.session.LIC_NO1,
                "olddealerCode": req.session.LIC_NO,
                "dealerName": req.session.username,
                "dealerMobileNo": req.session.mobileNo,
                "farmerName": result.farmerName,
                "farmerMobileNo": result.farmerMobileNo,
                "farmerId": result.farmerId,
                "payableAmtFarmer": result.totalamount

            }
            console.log(erupiDetails, typeof (erupiDetails.payableAmtFarmer));

            erupiDetails.payableAmtFarmer = parseFloat(erupiDetails.payableAmtFarmer);
            let payableAmtFarmerString = erupiDetails.payableAmtFarmer.toString();
            console.log(payableAmtFarmerString);
            if (!payableAmtFarmerString.includes('.')) {
                console.log(typeof (erupiDetails.payableAmtFarmer));
                const stringValue1 = erupiDetails.payableAmtFarmer + '.0000001';
                console.log(typeof (stringValue1), stringValue1);
                erupiDetails.payableAmtFarmer = parseFloat(stringValue1);
            }
            const result1 = await farmersaleDal.InserteRUPIData(erupiDetails);
            try {
                request({
                    url: "https://stgsugam.csm.co.in/admin/integration/voucherDetails",
                    method: "POST",
                    json: true,   // <--Very important!!!
                    headers: {
                        'Content-Type': 'application/json',
                        'Cookie': 'laravel_session_fard=ZwmXL2reFpPdoJKiuqirgZ9iCVIryv1i70kDKHGO'
                    },
                    body: erupiDetails
                }, async function (error, response, body) {
                    if (error) {
                        authDAL.addActivityLog('/erupisendtocsm', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, error);

                        console.log(error);
                    }
                    else {
                        response.body.TRANSACTION_ID = result.TRANSACTION_ID;
                        result.voucherid=response.body.result.voucherId
                        const result1 = await farmersaleDal.updateeRUPIData(response.body);
                        console.log(response.body);
                    }
                });
            } catch (error) {
                console.log(error, 'error');
                authDAL.addActivityLog('/erupisendtocsm', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, error);

                // Handle the error properly, log or throw it again
            }}

        }

        console.log(result);
        farmersaleDal.addActivityLog('/InsertSaleDealer', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        setTimeout(() => {
            res.send(result);
        }, 3000);
    } catch (e) {
        farmersaleDal.addActivityLog('/InsertSaleDealer', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.getCurrentstockDetails = async (req, res) => {
    try {
        const result = await farmersaleDal.getCurrentstockDetails(req.session.LIC_NO);
        farmersaleDal.addActivityLog('/getCurrentstockDetails', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        farmersaleDal.addActivityLog('/getCurrentstockDetails', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.getPaymentResponse = async (req, res) => {
    try {
        // req.body.distCode = req.session.distCode
        const result = await farmersaleDal.getPaymentResponse(req.query);
        farmersaleDal.addActivityLog('/getPaymentResponse', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        if (result.length > 0) {
            const result1 = await farmersaleDal.getpaymentResponseWithPgFarmerID(result);
            farmersaleDal.addActivityLog('/getpaymentResponseWithPgFarmerID', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
            res.send(result1);
        }
        else {
            res.send(result);
        }

    } catch (e) {
        farmersaleDal.addActivityLog('/getPaymentResponse', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.GetDistCodeFromDist = async (req, res) => {
    try {
        req.query.distCode = req.session.distCode
        const result = await farmersaleDal.GetDistCodeFromDist(req.query);
        farmersaleDal.addActivityLog('/GetDistCodeFromDist', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        farmersaleDal.addActivityLog('/GetDistCodeFromDist', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.paymentStatusByFarmeId = async (req, res) => {
    try {
        const result = await farmersaleDal.paymentStatusByFarmeId(req.body);
        farmersaleDal.addActivityLog('/paymentStatusByFarmeId', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);

    } catch (e) {
        farmersaleDal.addActivityLog('/paymentStatusByFarmeId', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.allFillFinYr = async (req, res) => {
    try {
        const result = await farmersaleDal.allFillFinYr();
        farmersaleDal.addActivityLog('/allFillFinYr', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        farmersaleDal.addActivityLog('/allFillFinYr', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.FillCropCategory = async (req, res) => {
    try {
        const result = await farmersaleDal.FillCropCategory();
        farmersaleDal.addActivityLog('/FillCropCategory', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        farmersaleDal.addActivityLog('/FillCropCategory', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.FillCropByCategoryId = async (req, res) => {
    try {
        const result = await farmersaleDal.FillCropByCategoryId(req.query.SelectedCropCatagory);
        farmersaleDal.addActivityLog('/FillCropByCategoryId', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        farmersaleDal.addActivityLog('/FillCropByCategoryId', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.fillGodownwisestock = async (req, res) => {
    try {
        req.query.DIST_CODE = req.session.distCode;
        const result = await farmersaleDal.fillGodownwisestock(req.query);
        farmersaleDal.addActivityLog('/fillGodownwisestock', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        farmersaleDal.addActivityLog('/fillGodownwisestock', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.GetDealerInfo = async (req, res) => {
    try {
        const result = await farmersaleDal.GetDealerInfo(req.session.LIC_NO);
        farmersaleDal.addActivityLog('/GetDealerInfo', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        farmersaleDal.addActivityLog('/GetDealerInfo', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.CntLic = async (req, res) => {
    try {
        const result = await farmersaleDal.CntLic(req.session.LIC_NO);
        farmersaleDal.addActivityLog('/CntLic', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        farmersaleDal.addActivityLog('/CntLic', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.FillBank = async (req, res) => {
    try {
        const result = await farmersaleDal.FillBank();
        farmersaleDal.addActivityLog('/FillBank', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        farmersaleDal.addActivityLog('/FillBank', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.FillBranchName = async (req, res) => {
    try {
        const result = await farmersaleDal.FillBranchName(req.query.selectedBank);
        farmersaleDal.addActivityLog('/FillBranchName', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        farmersaleDal.addActivityLog('/FillBranchName', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.FillIFSC = async (req, res) => {
    try {
        const result = await farmersaleDal.FillIFSC(req.query.selectedBranch);
        farmersaleDal.addActivityLog('/FillIFSC', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        farmersaleDal.addActivityLog('/FillIFSC', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};

exports.UpdateDealerBankDetails = async (req, res) => {
    try {
        req.body.userid = req.session.LIC_NO1;
        const result = await farmersaleDal.UpdateDealerBankDetails(req.body);
        farmersaleDal.addActivityLog('/UpdateDealerBankDetails', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);

    } catch (e) {
        farmersaleDal.addActivityLog('/UpdateDealerBankDetails', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.FillPrebooking = async (req, res) => {
    try {
        const result = await farmersaleDal.FillPrebooking(req.query.beneficiaryType, req.session.LIC_NO1);
        farmersaleDal.addActivityLog('/FillPrebooking', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        farmersaleDal.addActivityLog('/FillPrebooking', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.rejectedBankDetails = async (req, res) => {
    try {
        const result = await farmersaleDal.rejectedBankDetails(req.session.LIC_NO);
        farmersaleDal.addActivityLog('/rejectedBankDetails', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        farmersaleDal.addActivityLog('/rejectedBankDetails', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.UpdatetheBankDetails = async (req, res) => {
    try {
        req.body.userid = req.session.LIC_NO;
        const result = await farmersaleDal.UpdatetheBankDetails(req.body);
        farmersaleDal.addActivityLog('/UpdatetheBankDetails', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);

    } catch (e) {
        farmersaleDal.addActivityLog('/UpdatetheBankDetails', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.CountFarmerMob = async (req, res) => {
    try {
        const result = await farmersaleDal.CountFarmerMob(req.query.MobileNo);
        farmersaleDal.addActivityLog('/CountFarmerMob', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        farmersaleDal.addActivityLog('/CountFarmerMob', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
