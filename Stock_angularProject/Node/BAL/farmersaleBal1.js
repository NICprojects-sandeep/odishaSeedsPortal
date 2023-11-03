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

exports.GetFirmName = async (req, res) => {
    try {
        
        const result = await farmersaleDal.getUserDetails(req.session.LIC_NO, req, res);
        res.send({ result });
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.GetFarmerInvHdr = async (req, res) => {
    try {
        const today =  new Date();
        const result = await farmersaleDal.GetFarmerInvHdr(req.query.farmerID, req, res);
        result.today = new Date();
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};

exports.GetFarmerInv = async (req, res) => {
    try {
        const result = await farmersaleDal.GetFarmerInv(req.query, req, res);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.GetFarmerDtl = async (req, res) => {
    try {
        const result = await farmersaleDal.GetFarmerDtl(req.query, req, res);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.RptDateWiseSale = async (req, res) => {
    try {
        req.query.LicNo=req.session.LIC_NO
        const result = await farmersaleDal.RptDateWiseSale(req.query, req, res);
        const result1 = await farmersaleDal.RptDateWiseSalewithFarmerdata(result);
        res.send(result1);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
//////////////////////////////////////////////////////////////////// postgressql///////////////////////////////
exports.GETDISTCODEFROMLICNO = async (req, res) => {
    try {
        console.log('call');
        const result = await farmersaleDal.GETDISTCODEFROMLICNO(req.session.LIC_NO);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.getStockReceivedData = async (req, res) => {
    try {
        const result = await farmersaleDal.getStockReceivedData(req.query, req, res);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.getPreBookingDetails = async (req, res) => {
    try {
        const result = await farmersaleDal.getPreBookingDetails(req.query, req, res);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};


exports.sendOtp = async (req, res, next) => {
    console.log("dddd");
    try {
        var otp = Math.floor(100000 + Math.random() * 900000)
        var sms = `Your Transaction OTP for Seed Purchase is ${otp}.DAFP AGRIOR`;
        var mobileNo = req.query.MobileNo;
        req.query.otp = otp;

        request(`http://mkuy.apicol.nic.in/Registration/EPestSMSNew?template_id=1107165150759207123&type=SMS&mobileNo=${mobileNo}&sms=${sms}`, {
            json: true,strictSSL:false
        }, (err, resp, body) => {
            if (err) {
                console.log(err);
            }
        });
        const result = await farmersaleDal.createOtp(req.query);
        res.send(true);
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
        console.error(e);
    }
};
exports.ValidateOTP = async (req, res) => {
    try {
        const result = await farmersaleDal.ValidateOTP(req.query);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.FillCrops = async (req, res) => {
    try {
        req.query.LicNo=req.session.LIC_NO
        const result = await farmersaleDal.FillCrops(req.query);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.FillVariety = async (req, res) => {
    try {
        req.query.LicNo=req.session.LIC_NO
        const result = await farmersaleDal.FillVariety(req.query);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.FILLFINYR = async (req, res) => {
    try {
        const result = await farmersaleDal.FILLFINYR();
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.FILLSEASSION = async (req, res) => {
    try {
        const result = await farmersaleDal.FILLSEASSION(req.query);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.FILLDEALERSTOCK = async (req, res) => {
    try {
        req.query.LIC_NO=req.session.LIC_NO
        const result = await farmersaleDal.FILLDEALERSTOCK(req.query);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};

exports.GETFARMERINFO = async (req, res) => {
    try {
        const result = await farmersaleDal.GETFARMERINFO(req.query.FARMER_ID);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.InsertSaleDealer = async (req, res) => {
    try {
        req.body.LICENCE_NO=req.session.LIC_NO
        req.body.DIST_CODE = await farmersaleDal.GetDistCodeByLicNo(req.body)//session
        req.body.DAO_CD = await farmersaleDal.GetDAOCodeByLicNo(req.body)//session
        req.body.LIC_NO = req.session.LIC_NO //session
        req.body.USERIP = reqip.getClientIp(req);
        req.body.distCode=req.session.distCode;
        req.body.ipAdress = reqip.getClientIp(req);
        const result = await farmersaleDal.InsertSaleDealer(req.body);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.getCurrentstockDetails = async (req, res) => {
    try {
        const result = await farmersaleDal.getCurrentstockDetails(req.session.LIC_NO);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.getPaymentResponse = async (req, res) => {
    try {
        // req.body.distCode = req.session.distCode
        const result = await farmersaleDal.getPaymentResponse(req.query);
        if (result.length > 0) {
            const result1 = await farmersaleDal.getpaymentResponseWithPgFarmerID(result);  
            res.send(result1);
        }
        else {
            res.send(result);
        }

    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.GetDistCodeFromDist = async (req, res) => {
    try {
        req.query.distCode = req.session.distCode
        const result = await farmersaleDal.GetDistCodeFromDist(req.query);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.paymentStatusByFarmeId = async (req, res) => {
    try {
        const result = await farmersaleDal.paymentStatusByFarmeId(req.body);
        res.send(result);

    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.allFillFinYr = async (req, res) => {
    try {
        const result = await farmersaleDal.allFillFinYr();
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.FillCropCategory = async (req, res) => {
    try {
        const result = await farmersaleDal.FillCropCategory();
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.FillCropByCategoryId = async (req, res) => {
    try {
        const result = await farmersaleDal.FillCropByCategoryId(req.query.SelectedCropCatagory);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.fillGodownwisestock = async (req, res) => {
    try {
        req.query.DIST_CODE = req.session.distCode;
        const result = await farmersaleDal.fillGodownwisestock(req.query);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};

