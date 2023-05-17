var dbConfig = require('../models/dbConfig');
var sqlstock = dbConfig.sqlstock;
var sequelizeSeed = dbConfig.sequelizeSeed;

var locConfigstock = dbConfig.locConfigStock;
var locConfigStockLive = dbConfig.locConfigStockLive;

var sequelizeStock = dbConfig.sequelizeStock;

exports.paymentStatusByFarmeId = async (data, req, res) => {
    console.log(data);
    // try {
    //     var queryText = `SELECT APP_FIRMNAME FROM [DAFPSEED].[DBO].[SEED_LIC_DIST] WHERE LIC_NO ='${LIC_NO}'`
    //     const result = await sequelizeStock.query(queryText);
    //     return result[0]
    // } catch (e) {
    //     console.log(`Oops! An error occurred: ${e}`);
    // }
};