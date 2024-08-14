const ip = require('ip');
const UAParser = require('ua-parser-js');
const crypto = require('crypto');
const sha512 = require('js-sha512');
const otherDal = require('../DAL/otherRealtedApiDal');
const reqip = require('request-ip');
const request = require('request');
var http = require('http');
var https = require('https');
const parser = new UAParser();
const cron = require('node-cron');
const dotenv = require('dotenv');
dotenv.config();



// const axios = require('axios');
const moment = require('moment');
var overrideConfig = {
    'maxAge': 2000,
    'setPrivate': true
};
const getURL = (req) => {
    const fullURL = `${req.protocol}://${req.get('host')}${req.originalUrl}`;
    return fullURL;
};
exports.successPrebookingDtl = async (req, res) => {
    try {
        const result = await otherDal.successPrebookingDtl(req.query.date);
        otherDal.addActivityLog('/successPrebookingDtl', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
        res.send(result);
    } catch (e) {
        otherDal.addActivityLog('/successPrebookingDtl', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.seedavailablity = async (req, res) => {
    try {
        var reqApiData = req.query.apiKey;
        if (reqApiData == 'AXkU758hy4g5MUGi3589FR45') {
            var DIST_CODE = req.query.DIST_CODE;
            var CROP_CODE = req.query.CROP_CODE;
            var VARIETY_CODE = req.query.VARIETY_CODE;
            const result = await otherDal.seedavailablity(DIST_CODE, CROP_CODE, VARIETY_CODE);
            otherDal.addActivityLog('/seedavailablity', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
            if (result.length > 0) {
                let a = result.length;
                for (let i = 0; i < result.length; i++) {
                    const result1 = await otherDal.getDealerData(result[i].LICENCE_NUMBER);
                    otherDal.addActivityLog('/getDealerData', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
                    if (result1.length > 0) {
                        result[i].LICENCE_NUMBER = result1[0].LICENCE_NUMBER;
                        result[i].DEALER_TYPE = result1[0].DEALER_TYPE;
                        result[i].CONTACT_NUMBER = result1[0].CONTACT_NUMBER;
                        result[i].FARM_NAME = result1[0].FARM_NAME;
                        result[i].BLOCK = result1[0].BLOCK;
                        result[i].ADDRESS = result1[0].ADDRESS;
                    }
                    if (a == i + 1) {
                        res.send(result);
                    }
                }

            }
        }
        else {
            otherDal.addActivityLog('/getDealerData', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'API key is not valid');
            res.send('API key is not valid')
        }
    } catch (e) {
        otherDal.addActivityLog('/seedavailablity', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};
exports.getSeeddbtfupdata = async (req, res) => {
    try {
        var reqApiData = req.query.apiKey;
        if (reqApiData == 'AXkU758hy4g5MUGi3589FR45') {
            var from = req.query.from;
            var to = req.query.to;

            const result = await otherDal.getSeeddbtfupdata(from,to);
            otherDal.addActivityLog('/getSeeddbtfupdata', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Show');
            res.send(result[0]);
        }
        else {
            otherDal.addActivityLog('/getSeeddbtfupdata', 'SELECT', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'API key is not valid');
            res.send('API key is not valid')
        }
    } catch (e) {
        otherDal.addActivityLog('/getSeeddbtfupdata', 'Error', 'GET', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
        res.status(500).send(e);
        throw e;
    }
};

const fs = require('fs');

// Specify the path to your .txt file
const filePath = 'PullthevoucherstatusfromSUGAMPortastarttime.txt';
const vouchercreatedtimepath = 'vouchercreatedtime.txt';

// Read the contents of the file
// fs.readFile(filePath, 'utf8', (err, data) => {
//   if (err) {
//     console.error('Error reading file:', err);
//     return;
//   }

//   // Log the contents of the file
//   console.log('File content:', data);
//   const sugamcorn = data.trim(); // Assuming the content of corn.txt contains only the value you want to use for cron scheduling

//   // Schedule a task using the content read from the file
//   cron.schedule(`*/${process.env.sugamsidetime} * * * * *`, () => {
//     console.log(process.env.sugamsidetime, sugamcorn);
//     console.log('Running a task every', sugamcorn, 'minutes!');
//   });
// });



//////////////////////////////
let sugamcorn = ''; // Define sugamcorn variable outside the callback function to make it accessible globally
let vouchercreatedtimedata = '';

// Function to read the content of the file and update the cron job
function updateCronJob() {
    // Pull the voucher status from SUGAM Portal start
    fs.readFile(filePath, 'utf8', (err, data) => {
        if (err) {
            console.error('Error reading file:', err);
            return;
        }

        sugamcorn = data.trim(); // Update sugamcorn with new data from the file

        // Schedule a task using the content read from the file
        cron.schedule(`* * * * * *`, () => {
            // Define an asynchronous function to execute your code
            async function executeTask() {
                try {
                    const result = await otherDal.NotupdatedeRUPIVoucher(sugamcorn);

                    for (let i = 0; i < result.length; i++) {
                        const element = result[i];
                        try {
                            request({
                                url: "https://stgsugam.csm.co.in/admin/integration/voucherStatus",
                                method: "POST",
                                json: true,   // <--Very important!!!
                                headers: {
                                    'Content-Type': 'application/json',
                                    'Cookie': 'laravel_session_fard=ZwmXL2reFpPdoJKiuqirgZ9iCVIryv1i70kDKHGO'
                                },
                                body: result[i]
                            }, async function (error, response, body) {
                                if (error) {
                                    otherDal.addActivityLog('/erupisendtocsm', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, error);
                                }
                                else {
                                    if (response.body.status == 404) {
                                        response.body.msg = response.body.message
                                    }

                                    response.body.TRANSACTION_ID = element.TRANSACTION_ID;
                                    response.body.voucherId = element.voucherId;
                                    const result1 = await otherDal.updateeRUPIVoucher(response.body);

                                }
                            });
                        } catch (error) {
                            otherDal.addActivityLog('/erupisendtocsm', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, error);

                            // Handle the error properly, log or throw it again
                        }

                    }

                } catch (error) {
                    console.error('Error occurred:', error);
                }
            }

            // Call the asynchronous function
            executeTask();
        });
    });
    //end

    //if after x min  voucher initited status is not updated then voucher is created another time
    fs.readFile(vouchercreatedtimepath, 'utf8', (err, data) => {
        if (err) {
            console.error('Error reading file:', err);
            return;
        }

        vouchercreatedtimedata = data.trim(); // Update sugamcorn with new data from the file

        // Schedule a task using the content read from the file
        cron.schedule(`* * * * * *`, () => {
            // Define an asynchronous function to execute your code
            async function executeTask1() {
                try {
                    const result = await otherDal.anotherTimeeRUPIVoucherCreated(vouchercreatedtimedata);

                    for (let i = 0; i < result.length; i++) {
                        console.log(result[i], result.length, i);
                        if (result[i].count < 3) {
                            console.log('hhhh');
                            const element = result[i];
                            let payableAmtFarmerString = element.payableAmtFarmer.toString();
                            console.log(payableAmtFarmerString);
                            if (payableAmtFarmerString.includes('.00')) {
                                const stringValue1 = payableAmtFarmerString.replace('.00', '.0000001');
                                // const stringValue1  = element.payableAmtFarmer + '.0000001';
                                element.payableAmtFarmer = parseFloat(stringValue1);
                            }
                            else {
                            }
                            try {
                                //revoke start
                                request({
                                    url: "https://stgsugam.csm.co.in/admin/integration/voucherRevoke",
                                    method: "POST",
                                    json: true,   // <--Very important!!!
                                    headers: {
                                        'Content-Type': 'application/json',
                                        'Cookie': 'laravel_session_fard=SNGOwDrZE9NYFqwjLI0Im5HzB3SRAHWUb3Kmdk28'
                                    },
                                    body: {
                                        "voucherId": result[i].voucherId
                                    }


                                }, async function (error, response1) {
                                    if (error) {
                                        otherDal.addActivityLog('/voucherRevoke', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, error);

                                    }
                                    else {
                                        console.log('revoke time if', response1.body.result.msg);
                                        if (response1.body.status == 404) {
                                            response1.body.msg = response1.body.message
                                        }
                                        if (response1.body.result.msg === undefined) {
                                            response1.body.result.msg = response1.body.result.statusMsg;
                                        }
                                        let data = {
                                            "TRANSACTION_ID": result[i].TRANSACTION_ID,
                                            "oldVoucherId": result[i].voucherId,
                                            "id": result[i].id,
                                            "revokemsg": response1.body.result.msg

                                        }
                                        console.log("rejectedmsg start");
                                        console.log(data);
                                        console.log("rejectedmsg end");

                                        const result1 = await otherDal.revokeVoucherCreatedForeRUPI(data);
                                        //voucher another time created
                                        // request({
                                        //     url: "https://stgsugam.csm.co.in/admin/integration/voucherDetails",
                                        //     method: "POST",
                                        //     json: true,   // <--Very important!!!
                                        //     headers: {
                                        //         'Content-Type': 'application/json',
                                        //         'Cookie': 'laravel_session_fard=ZwmXL2reFpPdoJKiuqirgZ9iCVIryv1i70kDKHGO'
                                        //     },
                                        //     body: result[i]
                                        // }, async function (error, response, body) {
                                        //     if (error) {
                                        //         otherDal.addActivityLog('/erupisendtocsm', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, error);
                                        //             }
                                        //     else {
                                        //         let data = {
                                        //             "id": result[i].id,
                                        //             "TRANSACTION_ID": result[i].TRANSACTION_ID,
                                        //             "districtId": result[i].districtId,
                                        //             "dealerCode": result[i].dealerCode,
                                        //             "olddealerCode": result[i].olddealerCode,
                                        //             "dealerName": result[i].dealerName,
                                        //             "dealerMobileNo": result[i].dealerMobileNo,
                                        //             "farmerName": result[i].farmerName,
                                        //             "farmerMobileNo": result[i].farmerMobileNo,
                                        //             "farmerId": result[i].farmerId,
                                        //             "payableAmtFarmer": result[i].payableAmtFarmer,
                                        //             "voucherId": response.body.result.voucherId,
                                        //             "status": response.body.status,
                                        //             "msg": response.body.msg,
                                        //             "oldVoucherId": result[i].voucherId,
                                        //             "revokemsg": response1.body.result.msg

                                        //         }

                                        //         // response.body.TRANSACTION_ID = element.TRANSACTION_ID;
                                        //         // response.body.voucherId = element.voucherId;
                                        //         const result1 = await otherDal.inserteRUPIVoucher(data);
                                        //     }
                                        // });
                                        //voucher created end 
                                    }
                                });

                                //revoke end





                            } catch (error) {
                                otherDal.addActivityLog('/erupisendtocsm', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, error);

                                // Handle the error properly, log or throw it again
                            }
                        }

                        //else
                        else {
                            request({
                                url: "https://stgsugam.csm.co.in/admin/integration/voucherRevoke",
                                method: "POST",
                                json: true,   // <--Very important!!!
                                headers: {
                                    'Content-Type': 'application/json',
                                    'Cookie': 'laravel_session_fard=SNGOwDrZE9NYFqwjLI0Im5HzB3SRAHWUb3Kmdk28'
                                },
                                body: {
                                    "voucherId": result[i].voucherId
                                }


                            }, async function (error, response) {
                                if (error) {
                                    otherDal.addActivityLog('/voucherRevoke', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, error);

                                }
                                else {
                                    console.log('revoke time else ');
                                    if (response.body.status == 404) {
                                        response.body.msg = response.body.message
                                    }
                                    let data = {
                                        "id": result[i].id,
                                        "TRANSACTION_ID": result[i].TRANSACTION_ID,
                                        "districtId": result[i].districtId,
                                        "dealerCode": result[i].dealerCode,
                                        "olddealerCode": result[i].olddealerCode,
                                        "dealerName": result[i].dealerName,
                                        "dealerMobileNo": result[i].dealerMobileNo,
                                        "farmerName": result[i].farmerName,
                                        "farmerMobileNo": result[i].farmerMobileNo,
                                        "farmerId": result[i].farmerId,
                                        "payableAmtFarmer": result[i].payableAmtFarmer,
                                        "oldVoucherId": result[i].voucherId,
                                        "revokemsg": response.body.result.msg,

                                    }

                                    // response.body.TRANSACTION_ID = element.TRANSACTION_ID;
                                    // response.body.voucherId = element.voucherId;

                                    const result1 = await otherDal.inserteRUPIVoucher(data);
                                }
                            });

                        }

                    }

                } catch (error) {
                    console.error('Error occurred:', error);
                }
            }

            // Call the asynchronous function
            executeTask1();
        });







        //new  function

        function voucherStatuscheck(data) {
            return new Promise((resolve, reject) => {
                request({
                    url: "https://stgsugam.csm.co.in/admin/integration/voucherStatus",
                    method: "POST",
                    json: true,   // <--Very important!!!
                    headers: {
                        'Content-Type': 'application/json',
                        'Cookie': 'laravel_session_fard=ZwmXL2reFpPdoJKiuqirgZ9iCVIryv1i70kDKHGO'
                    },
                    body: data
                }, async function (error, response, body) {
                    if (error) {
                        otherDal.addActivityLog('/voucherStatuscheck', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, error);
                    }
                    else {
                        if (response.body.status == 404) {
                            response.body.msg = response.body.message
                        }
                        console.log('start');
                        console.log(data);
                        console.log(response.body.result.statusMsg, data.redeemstatusMsg, response.body.result.statusMsg != data.redeemstatusMsg);
                        console.log('end');
                        if (response.body.result.statusMsg != data.redeemstatusMsg) {
                            response.body.TRANSACTION_ID = data.TRANSACTION_ID;
                            response.body.voucherId = data.voucherId;
                            const result1 = await otherDal.updateeRUPIVoucher(response.body);
                        }
                        resolve(true);


                    }
                });
            })
        }








        cron.schedule(`* * * * * *`, () => {
            // Define an asynchronous function to execute your code
            async function executeTask2() {
                try {
                    const result = await otherDal.revokeVoucherDetails();

                    for (let i = 0; i < result.length; i++) {


                        let voucherStatus = await voucherStatuscheck(result[i]);
                        console.log(result[i], 'data.voucherIddata.voucherIddata.voucherIddata.voucherIddata.voucherId');
                        const result1 = await otherDal.revokeVoucherList(result[i].voucherId);
                        if (result1.length > 0) {
                            if (result1[0].count < 3) {
                                console.log('hhhh');
                                const element = result1[0];
                                let payableAmtFarmerString = element.payableAmtFarmer.toString();
                                console.log(payableAmtFarmerString);
                                if (payableAmtFarmerString.includes('.00')) {
                                    const stringValue1 = payableAmtFarmerString.replace('.00', '.0000001');
                                    // const stringValue1  = element.payableAmtFarmer + '.0000001';
                                    result1[0].payableAmtFarmer = parseFloat(stringValue1);
                                }
                                else {
                                }
                                try {
                                    request({
                                        url: "https://stgsugam.csm.co.in/admin/integration/voucherDetails",
                                        method: "POST",
                                        json: true,   // <--Very important!!!
                                        headers: {
                                            'Content-Type': 'application/json',
                                            'Cookie': 'laravel_session_fard=ZwmXL2reFpPdoJKiuqirgZ9iCVIryv1i70kDKHGO'
                                        },
                                        body: result1[0]
                                    }, async function (error, response, body) {
                                        if (error) {
                                            otherDal.addActivityLog('/erupisendtocsm', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, error);
                                        }
                                        else {
                                            let data = {
                                                "id": element.id,
                                                "TRANSACTION_ID": element.TRANSACTION_ID,
                                                "districtId": element.districtId,
                                                "dealerCode": element.dealerCode,
                                                "olddealerCode": element.olddealerCode,
                                                "dealerName": element.dealerName,
                                                "dealerMobileNo": element.dealerMobileNo,
                                                "farmerName": element.farmerName,
                                                "farmerMobileNo": element.farmerMobileNo,
                                                "farmerId": element.farmerId,
                                                "payableAmtFarmer": element.payableAmtFarmer,
                                                "voucherId": response.body.result.voucherId,
                                                "status": response.body.status,
                                                "msg": response.body.msg,
                                                "oldVoucherId": element.voucherId,
                                                "revokemsg": element.rejectedmsg,

                                            }
                                            const result1 = await otherDal.inserteRUPIVoucher(data);
                                        }
                                    });

                                } catch (error) {
                                    otherDal.addActivityLog('/erupisendtocsm', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, error);

                                    // Handle the error properly, log or throw it again
                                }
                            }
                        }

                    }

                } catch (error) {
                    console.error('Error occurred:', error);
                }
            }

            // Call the asynchronous function
            executeTask2();
        });

        cron.schedule(`* * * * * *`, () => {
            // Define an asynchronous function to execute your code
            async function executeTask2() {
                try {
                    const result = await otherDal.revokeVoucherDetails();

                    for (let i = 0; i < result.length; i++) {


                        let voucherStatus = await voucherStatuscheck(result[i]);
                        console.log(result[i], 'data.voucherIddata.voucherIddata.voucherIddata.voucherIddata.voucherId');
                        const result1 = await otherDal.revokeVoucherList(result[i].voucherId);
                        if (result1.length > 0) {
                            if (result1[0].count < 3) {
                                console.log('hhhh');
                                const element = result1[0];
                                let payableAmtFarmerString = element.payableAmtFarmer.toString();
                                console.log(payableAmtFarmerString);
                                if (payableAmtFarmerString.includes('.00')) {
                                    const stringValue1 = payableAmtFarmerString.replace('.00', '.0000001');
                                    // const stringValue1  = element.payableAmtFarmer + '.0000001';
                                    result1[0].payableAmtFarmer = parseFloat(stringValue1);
                                }
                                else {
                                    result1[0].payableAmtFarmer = parseFloat(result1[0].payableAmtFarmer);
                                }
                                try {
                                    request({
                                        url: "https://stgsugam.csm.co.in/admin/integration/voucherDetails",
                                        method: "POST",
                                        json: true,   // <--Very important!!!
                                        headers: {
                                            'Content-Type': 'application/json',
                                            'Cookie': 'laravel_session_fard=ZwmXL2reFpPdoJKiuqirgZ9iCVIryv1i70kDKHGO'
                                        },
                                        body: result1[0]
                                    }, async function (error, response, body) {
                                        if (error) {
                                            otherDal.addActivityLog('/erupisendtocsm', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, error);
                                        }
                                        else {
                                            let data = {
                                                "id": element.id,
                                                "TRANSACTION_ID": element.TRANSACTION_ID,
                                                "districtId": element.districtId,
                                                "dealerCode": element.dealerCode,
                                                "olddealerCode": element.olddealerCode,
                                                "dealerName": element.dealerName,
                                                "dealerMobileNo": element.dealerMobileNo,
                                                "farmerName": element.farmerName,
                                                "farmerMobileNo": element.farmerMobileNo,
                                                "farmerId": element.farmerId,
                                                "payableAmtFarmer": element.payableAmtFarmer,
                                                "voucherId": response.body.result.voucherId,
                                                "status": response.body.status,
                                                "msg": response.body.msg,
                                                "oldVoucherId": element.voucherId,
                                                "revokemsg": element.rejectedmsg,

                                            }
                                            const result1 = await otherDal.inserteRUPIVoucher(data);
                                        }
                                    });

                                } catch (error) {
                                    otherDal.addActivityLog('/erupisendtocsm', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, error);

                                    // Handle the error properly, log or throw it again
                                }
                            }
                        }

                    }

                } catch (error) {
                    console.error('Error occurred:', error);
                }
            }

            // Call the asynchronous function
            executeTask2();
        });





















    });

}

// Initial setup
// updateCronJob();

// Watch for changes to the file
fs.watch(filePath, (eventType, filename) => {
    if (eventType === 'change') {
        console.log('File', filename, 'was changed!');
        // Update the cron job
        // updateCronJob();
    }
});
fs.watch(vouchercreatedtimepath, (eventType, filename) => {
    if (eventType === 'change') {
        console.log('File', filename, 'was changed!');
        // Update the cron job
        // updateCronJob();
    }
});

exports.dummycheck = async (req, res) => {
   


    
try {
    const result = await otherDal.dummycheck();
    // console.log(result);
    for (let i = 0; i < result.length; i++) {
        const element = result[i];
        request({
            url: "https://sugam.odisha.gov.in/admin/integration/voucherStatus",
            method: "POST",
            json: true,   // <--Very important!!!
            headers: {
                'Content-Type': 'application/json',
                'Cookie': 'laravel_session_fard=ZwmXL2reFpPdoJKiuqirgZ9iCVIryv1i70kDKHGO'
            },
            body: element
        }, async function (error, response, body) {
            if (error) {
                otherDal.addActivityLog('/erupisendtocsm', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, error);
            }
            else {
                // if (response.body.status == 404) {
                //     response.body.msg = response.body.message
                // }
                if( response.body.result == 'undefined'){
                    console.log(element.redeemstatusMsg)
                }else{
                    if(element.redeemstatusMsg != response.body.result.statusMsg)
                        console.log(element.redeemstatusMsg,element.voucherId,'hhhh',response.body.result.statusMsg,'iiiiiiiiiiiiiiiiiiiiiiiii');
                }
               
                // response.body.TRANSACTION_ID = element.TRANSACTION_ID;
                // response.body.voucherId = element.voucherId;
                // const result1 = await otherDal.updateeRUPIVoucher(response.body);
    
            }
        });
        
    }
   
} catch (error) {
    otherDal.addActivityLog('/erupisendtocsm', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, error);

    // Handle the error properly, log or throw it again
}
};


