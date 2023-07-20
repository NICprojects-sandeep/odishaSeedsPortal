const pool = require('../config/dbConfig');
var dbConfig = require('../config/dbSqlConnection');
var sqlstock = dbConfig.sqlstock;
var sequelizeSeed = dbConfig.sequelizeSeed;

var locConfigstock = dbConfig.locConfigStock;
var locConfigStockLive = dbConfig.locConfigStockLive;
var locConfigAuth = dbConfig.locConfigAuth;
var sequelizeStock = dbConfig.sequelizeStock;

exports.getStockPricelist = () => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `select distinct a."Crop_Code",b."Crop_Name","All_in_cost_Price" from "Stock_Pricelist" a
        inner join "mCrop" b on a."Crop_Code" = b."Crop_Code"
        where "F_Year"=(select "FIN_YR" from public."mFINYR" where "IS_ACTIVE"=1)
        group by a."Crop_Code","All_in_cost_Price","VARIETY_AFTER_10YEAR",b."Crop_Name" order by "Crop_Name"`;
        const values = [];
        const response = await client.query(query, values);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.getDistrict = () => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `select "LGDistrict","Dist_Name" from public."Stock_District" order by "Dist_Name"`;
        const values = [];
        const response = await client.query(query, values);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.getDealerDetails = (data) => new Promise(async (resolve, reject) => {

    try {
        const result = await sequelizeSeed.query(`SELECT distinct LIC_NO1,APP_FIRMNAME,APPADDRESS,block_name FROM [dafpseed].[dbo].[SEED_LIC_DIST] A 
      INNER JOIN [dafpseed].[dbo].[SEED_LIC_APP_DIST] B ON A.SEED_LIC_DIST_ID = B.SEED_LIC_DIST_ID 
      INNER JOIN [dafpseed].[dbo].[SEED_LIC_COMP_DIST] C ON A.SEED_LIC_DIST_ID = C.SEED_LIC_DIST_ID 
      inner join [dafpseed].[dbo].[dist] d on a.DIST_CODE = d.dist_code
      inner join [dafpseed].[dbo].[block] e on b.APPBLOCK_ID= e.block_code
      WHERE CONVERT(DATE, DATEADD(MONTH,1,A.APR_UPTO),103) >= CONVERT(DATE, GETDATE(), 103) AND A.LIC_ACTIVE = 1 AND A.IS_ACTIVE = 1 AND A.APP_STATUS = 'A' AND C.COMP_TYPE = 1 AND C.COMP_NAME = 'OSSC' and d.LGDistrict='${data.DistrictCode}' order by APP_FIRMNAME,block_name`, {
            replacements: {}, type: sequelizeStock.QueryTypes.SELECT
        });
        resolve(result);

    } catch (e) {
        console.log('An error occurred...', e);
        resolve([]);
        throw e
    }
});