const ip = require('ip');
const UAParser = require('ua-parser-js');
const crypto = require('crypto');
const sha512 = require('js-sha512');
const farmersaleDal = require('../DAL/farmersaleDal');
const reqip = require('request-ip')
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
        const result = await farmersaleDal.GetFarmerInvHdr(req.query.farmerID, req, res);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.sendOtp = async (req, resp) => {
    try {
        console.log('send otpppppppppppppppppp');
        var uNameOtp = "agriodisha.otp";
        var pwdO = "Ku%40%23gh883";
        var senderid = "AGRIOR";
        var entity_id = "1101606950000043751";
        var TempId = "1107165150759207123";
        var sRandomOTP=904014
        var sms = "Your Transaction OTP for Seed Purchase is " + 909900 + " . DAFP AGRIOR";
        // var mobileNo = e.MobileNo
        var mobileNo = 8093274461;
        console.log(`https://smsgw.sms.gov.in/failsafe/HttpLink?username=${uNameOtp}&pin=${pwdO}&message=${sms}&mnumber=${mobileNo}&signature=${senderid}&dlt_entity_id=${entity_id}&dlt_template_id=${TempId}`);
        request(`https://smsgw.sms.gov.in/failsafe/HttpLink?username=${uNameOtp}&pin=${pwdO}&message=${sms}&mnumber=${mobileNo}&signature=${senderid}&dlt_entity_id=${entity_id}&dlt_template_id=${TempId}`, {
            json: true
        }, (err, resp, body) => {
            if (err) {
                console.log(err);
            }
        });
        resp.send(result);
    }catch(err){

    }
    //  catch (e) {
    //     resp.status(500).send(e);
    //     throw e;
    // }
};