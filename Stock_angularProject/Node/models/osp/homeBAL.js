var dbConfig = require('../dbConfig');
var sequelize = dbConfig.sequelize;
var sql = dbConfig.sql;
var locConfig = dbConfig.locConfig;

const getFinancialYear = () => {
    const today = new Date();
    const financialYear = (today.getMonth() + 1) <= 3 ? `${(today.getFullYear().toString().substr(2, 3)) - 1}-${today.getFullYear().toString().substr(2, 3)}` : `${(today.getFullYear()).toString().substr(2, 3)}-${(today.getFullYear() + 1).toString().substr(2, 3)}`;
    return financialYear;
};
exports.FillLogoDetails = function (LANG_TYPE, callback) {
    var con = new sql.ConnectionPool(locConfig);
    con.connect().then(function success() {
        const request = new sql.Request(con);
        request.input('LANG_TYPE', LANG_TYPE);
        request.execute('GetLogoDetails', function (err, result) {
            if (err) {
                
            }
            else {
                callback(result.recordset);
            }
            con.close();
        });
    }).catch(function error(err) {
        
    });
};

exports.FillMenu = function (LANG_TYPE, ParentMenuId, callback) {
    var con = new sql.ConnectionPool(locConfig);
    con.connect().then(function success() {
        const request = new sql.Request(con);
        request.input('LANG_TYPE', LANG_TYPE);
        request.input('ParentMenuId', ParentMenuId);
        request.execute('GetMenu', function (err, result) {
            if (err) {
                
            }
            else {
                callback(result.recordset);
            }
            con.close();
        });
    }).catch(function error(err) {
        
    });
};

exports.FillSubMenu = function (LANG_TYPE, callback) {
    var con = new sql.ConnectionPool(locConfig);
    con.connect().then(function success() {
        const request = new sql.Request(con);
        request.input('LANG_TYPE', LANG_TYPE);
        request.execute('GetSubMenu', function (err, result) {
            if (err) {
                
            }
            else {
                callback(result.recordset);
            }
            con.close();
        });
    }).catch(function error(err) {
        
    });
};

exports.FillSlider = function (LANG_TYPE, WidgetId, callback) {
    var con = new sql.ConnectionPool(locConfig);
    con.connect().then(function success() {
        const request = new sql.Request(con);
        request.input('LANG_TYPE', LANG_TYPE);
        request.input('WidgetId', WidgetId);
        request.execute('GetSlider', function (err, result) {
            if (err) {
                
            }
            else {
                callback(result.recordset);
            }
            con.close();
        });
    }).catch(function error(err) {
        
    });
};

exports.FillContact = function (LANG_TYPE, callback) {
    var con = new sql.ConnectionPool(locConfig);
    con.connect().then(function success() {
        const request = new sql.Request(con);
        request.input('LANG_TYPE', LANG_TYPE);
        request.execute('GetContact', function (err, result) {
            if (err) {
                
            }
            else {
                callback(result.recordset);
            }
            con.close();
        });
    }).catch(function error(err) {
        
    });
};

exports.FillNews = function (LANG_TYPE, callback) {
    var con = new sql.ConnectionPool(locConfig);
    con.connect().then(function success() {
        const request = new sql.Request(con);
        request.input('LANG_TYPE', LANG_TYPE);
        request.execute('GetNews', function (err, result) {
            if (err) {
                
            }
            else {
                callback(result.recordset);
            }
            con.close();
        });
    }).catch(function error(err) {
        
    });
};

exports.GRAPH10VARIETY = function (callback) {
    var con = new sql.ConnectionPool(locConfig);
    con.connect().then(function success() {
        const request = new sql.Request(con);
        request.execute('SP_GRAPH10VARIETY', function (err, result) {
            if (err) {
                
            }
            else {
                callback(result.recordset);
            }
            con.close();
        });
    }).catch(function error(err) {
        
    });
};

exports.GRAPH10LIC = function (callback) {
    var con = new sql.ConnectionPool(locConfig);
    con.connect().then(function success() {
        const request = new sql.Request(con);
        request.execute('SP_GRAPH10LIC', function (err, result) {
            if (err) {
                
            }
            else {
                callback(result.recordset);
            }
            con.close();
        });
    }).catch(function error(err) {
        
    });
};

exports.FillGraphVarietyDtl = function (VARIETY_NAME, callback) {
    var con = new sql.ConnectionPool(locConfig);
    con.connect().then(function success() {
        const request = new sql.Request(con);
        request.input('VARIETY_NAME', VARIETY_NAME);
        request.execute('SP_GRAPHVARIETYDTL', function (err, result) {
            if (err) {
                
            }
            else {
                callback(result.recordset);
            }
            con.close();
        });
    }).catch(function error(err) {
        
    });
};
exports.preBookingOfseed = (data) => new Promise(async (resolve, reject) => {
    let DistrictName = ''
    try {
        const threeDistrictName = await sequelize.query(`select District_Name from PDS_LG_DistMap where Dist_Code =${data.distID}`);
        DistrictName = threeDistrictName[0][0].District_Name[0] + threeDistrictName[0][0].District_Name[1] + threeDistrictName[0][0].District_Name[2];
        let findMaxQuery = '';
        let preMCode = '';
        findMaxQuery = `select max(cast((right(bookingID,len(bookingID)-20)) as int))  as totalprebookingorder  from prebookinglist where distID=${data.distID} and beneficiaryType='${data.beneficiaryType}' and Season='${data.Season}'`;
        preMCode = `OD${DistrictName}/${data.beneficiaryType}/PRE/${getFinancialYear()}/${data.Season[0]}`;
        let maxResponse = await sequelize.query(findMaxQuery);
        let bookingID = maxResponse[0][0].totalprebookingorder == null ? `${preMCode}/1` : `${preMCode}/${parseInt(maxResponse[0][0].totalprebookingorder) + 1}`;
        data.cropDetails.forEach((e, key) => {
             sequelize.query('insert into "prebookinglist" (bookingID,bookingType,distID,blockID,gpID,beneficiaryType,beneficiaryId,Season,dealerId,cropCode,varietyCode,monthOfPurchase,bagSize,noOfBag,quantity,totalCost,preBookingAmt,totalPaidAmt,dateofPurchase,dateofInsert) values (:bookingID,:bookingType,:distID,:blockID,:gpID,:beneficiaryType,:beneficiaryId,:Season,:dealerId,:cropCode,:varietyCode,:monthOfPurchase,:bagSize,:noOfBag,:quantity,:totalCost,:preBookingAmt,:totalPaidAmt,EOMONTH(getdate(),0),getdate()) select @@rowcount', {
                replacements: { bookingID: bookingID, bookingType: data.bookingType, distID: data.distID, blockID: data.blockID, gpID: data.gpID, beneficiaryType: data.beneficiaryType, beneficiaryId: data.beneficiaryId, Season: data.Season, dealerId: e.dealerId, cropCode: e.cropCode, varietyCode: e.varietyCode, monthOfPurchase: e.monthOfPurchase, bagSize: e.bagSize, noOfBag: e.noOfBag, quantity: e.quantity, totalCost: e.totalCost, preBookingAmt: e.preBookingAmt, totalPaidAmt: data.totalPaidAmt }, type: sequelize.QueryTypes.INSERT
            })
            if(key+1 == data.cropDetails.length){
                resolve(true)
            }
        });


    } catch (e) {
        console.log('An error occurred...', e);
        resolve([]);
        throw e
    }
})