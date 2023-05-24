var dbConfig = require('../models/dbConfig');
var sqlstock = dbConfig.sqlstock;
var sequelizeSeed = dbConfig.sequelizeSeed;

var locConfigstock = dbConfig.locConfigStock;
var locConfigStockLive = dbConfig.locConfigStockLive;
var locConfigFarmerDB = dbConfig.locConfigFarmerDB;

var sequelizeStock = dbConfig.sequelizeStock;
var sequelizeFarmerDB = dbConfig.sequelizeFarmerDB;


exports.paymentStatusByFarmeId = (data) => new Promise(async (resolve, reject) => { 
    var con = new sqlstock.ConnectionPool(locConfigstock);
    try {
        con.connect().then(function success() {
            const request = new sqlstock.Request(con);
            request.input('FARMERID', data.selectedFarmerId);
            request.input('FIN_Yr', data.selectedFinancialYear);
            request.input('Season', data.selectedSeasons);
            request.execute('GetFarmerpaymentDtlsbyfinyear', function (err, result) {
                if (err) {
                    console.log('An error occurred...', err);
                }
                else {
                    resolve(result.recordset)
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
exports.GetDistCodeFromAOO = async (userid, req, res) => {
    const partUserID = userid.split('_', 6);
    return new Promise(async resolve => {
        try {
            const result = await sequelizeStock.query(`select b.Short_Name from [dafpSeed].[DBO].[same_as_block_table_onlyULBCase] a
            inner join [dafpSeed].[DBO].[DIST] B on a.LGDIST_CODE = b.LGDistrict where a.block_code=:userid`, {
                replacements: { userid: partUserID[1] }, type: sequelizeStock.QueryTypes.SELECT
            });
            resolve(result[0]);

        } catch (e) {
            console.log('An error occurred...', e);
            resolve([]);
            throw e
        }
    });
};

exports.fillfARMERiD = (data) => new Promise(async (resolve, reject) => { 
    const blockcode = data.split('_');
    console.log(blockcode[1]);

    var con = new sqlstock.ConnectionPool(locConfigFarmerDB);
    try {
        con.connect().then(function success() {
            const request = new sqlstock.Request(con);
            request.input('BlockCode', blockcode[1]);
            request.execute('sp_GetFarmerId', function (err, result) {
                if (err) {
                    console.log('An error occurred...', err);
                }
                else {
                    resolve(result.recordset)
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