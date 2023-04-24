const ip = require('ip');
const UAParser = require('ua-parser-js');
const crypto = require('crypto');
const sha512 = require('js-sha512');
const farmersaleDal = require('../dal/farmersaleDal');
const reqip = require('request-ip');
const  request = require('request');
var http = require('http');
var https = require('https');
const parser = new UAParser();

exports.GetFirmName = async (req, res) => {
    try {
        const result = await farmersaleDal.getUserDetails(req.query.LIC_NO, req, res);
        res.send({ result });
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.GetFarmerInvHdr = async (req, res) => {
    try {
        const today = new Date();
        const result = await farmersaleDal.GetFarmerInvHdr(req.query.farmerID, req, res); 
        result[0].today = today;
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.sendOtp = async (req, res, next) => {
    try {
        var otp= Math.floor(100000 + Math.random() * 900000)
        var sms = `Your Transaction OTP for Seed Purchase is ${otp}.DAFP AGRIOR`;
        var mobileNo = req.query.MobileNo;
        req.query.otp= otp;

        request(`http://mkuy.apicol.nic.in/Registration/EPestSMSNew?template_id=1107165150759207123&type=SMS&mobileNo=${mobileNo}&sms=${sms}`, {
            json: true
        }, (err, resp, body) => {
            if (err) {
                console.log(err);
            }
        });
        const result = await farmersaleDal.createOtp(req.query, req, res);
        res.send(result);





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
        const result = await farmersaleDal.ValidateOTP(req.query, req, res);
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



