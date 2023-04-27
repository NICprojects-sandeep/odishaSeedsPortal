var dbConfig = require('../models/dbConfig');
var sqlstock = dbConfig.sqlstock;
var sequelizeSeed = dbConfig.sequelizeSeed;

var locConfigstock = dbConfig.locConfigStock;
var locConfigStockLive = dbConfig.locConfigStockLive;

var sequelizeStock = dbConfig.sequelizeStock;

exports.getUserDetails = async (LIC_NO, req, res) => {
    try {
        var queryText = `SELECT APP_FIRMNAME FROM [DAFPSEED].[DBO].[SEED_LIC_DIST] WHERE LIC_NO ='${LIC_NO}'`
        const result = await sequelizeStock.query(queryText);
        return result[0]
    } catch (e) {
        console.log(`Oops! An error occurred: ${e}`);
    }
};
exports.GetFarmerInvHdr = (farmerID) => new Promise(async (resolve, reject) => { 
    var con = new sqlstock.ConnectionPool(locConfigstock);
    try {
        // const result = await con.connect().then(function success() {
        //     const request = new sqlstock.Request(con);
        //     request.input('FARMER_ID', farmerID);
        //     request.execute('STOCK_FARMERINVHDR', function (err, result) {
        //         if (err) {
        //             console.log('An error occurred...', err);
        //         }
        //         else {
        //             console.log(result.recordset, 'dfal;a');
        //             return result.recordset[0];
        //         }
        //         con.close();
        //     });
        // }).catch(function error(err) {
        //     console.log('An error occurred...', err);
        // });

        con.connect().then(function success() {
            const request = new sqlstock.Request(con);
            request.input('FARMER_ID', farmerID);
            request.execute('STOCK_FARMERINVHDR', function (err, result) {
                if (err) {
                    console.log('An error occurred...', err);
                }
                else {
                    // callback(result.recordset);
                    resolve(result.recordsets[0])
                }
                con.close();
            });
        }).catch(function error(err) {
            console.log('An error occurred...', err);
        });

        console.log(result, 'gggggggggggggggggggg');
    } catch (e) {
        console.log(`Oops! An error occurred: ${e}`);
    }
});

exports.createOtp = (data) => new Promise(async (resolve, reject) => { 
    var con = new sqlstock.ConnectionPool(locConfigstock);
    try {
        con.connect().then(function success() {
            const request = new sqlstock.Request(con);
            request.input('FARMER_ID', data.FarmerId);
            request.input('OTP', data.otp);
            request.input('MOB_NO', data.MobileNo);
            request.input('UPDATED_BY', data.LicNo);
            request.output('Val');

            request.execute('CreateOTP', function (err, result) {
                if (err) {
                    console.log('An error occurred...', err);
                }
                else {
                    console.log(result.output);
                    resolve(result.output.Val)
                }
                con.close();
            });
        }).catch(function error(err) {
            console.log('An error occurred...', err);
        });

    } catch (e) {
        console.log(`Oops! An error occurred: ${e}`);
    }
});
exports.ValidateOTP = (data) => new Promise(async (resolve, reject) => { 
    var con = new sqlstock.ConnectionPool(locConfigstock);
    try {
        con.connect().then(function success() {
            const request = new sqlstock.Request(con);
            request.input('FARMER_ID', data.FarmerId);
            request.input('OTP', data.enteredOtp);
            request.input('UPDATED_BY', data.LicNo);
            request.output('Val');

            request.execute('CheckOTP', function (err, result) {
                if (err) {
                    console.log('An error occurred...', err);
                }
                else {
                    console.log(result.output);
                    resolve(result.output.Val)
                }
                con.close();
            });
        }).catch(function error(err) {
            console.log('An error occurred...', err);
        });

    } catch (e) {
        console.log(`Oops! An error occurred: ${e}`);
    }
});
exports.GetFarmerInv = (data) => new Promise(async (resolve, reject) => { 
    var con = new sqlstock.ConnectionPool(locConfigstock);
    try {
        con.connect().then(function success() {
            const request = new sqlstock.Request(con);
            request.input('TRANSACTION_ID', data.TRANSACTION_ID);//data.TRANSACTION_ID
//'W1611030041-176'
            request.execute('STOCK_FARMERINVPRICE', function (err, result) {
                if (err) {
                    console.log('An error occurred...', err);
                }
                else {
                    resolve(result.recordsets[0])
                }
                con.close();
            });
        }).catch(function error(err) {
            console.log('An error occurred...', err);
        });

    } catch (e) {
        console.log(`Oops! An error occurred: ${e}`);
    }
});
exports.RptDateWiseSale = (data) => new Promise(async (resolve, reject) => { 
    var con = new sqlstock.ConnectionPool(locConfigstock);
    try {
        console.log(data,'date');
        const abc=data.selectedFromDate.split("-");
        const abc1=data.selectedToDate.split("-");
        const fromdate= abc[1]+'/'+abc[2]+'/'+ abc[0];
        const todate= +abc1[1]+'/'+abc1[2]+'/'+ abc1[0];
        con.connect().then(function success() {
            const request = new sqlstock.Request(con);
            request.input('LIC_NO', data.LicNo);
            request.input('FromDt', fromdate);
            request.input('ToDt', todate);
            request.execute('STOCK_RPT_DatewiseSale', function (err, result) {
                if (err) {
                    console.log('An error occurred...', err);
                }
                else {
                    console.log(result.recordsets);
                    resolve(result.recordsets[0])
                }
                con.close();
            });
        }).catch(function error(err) {
            console.log('An error occurred...', err);
        });

    } catch (e) {
        console.log(`Oops! An error occurred: ${e}`);
    }
});