const pool = require('../config/dbConfig');
var dbConfig = require('../config/dbSqlConnection');
var sqlstock = dbConfig.sqlstock;
var sequelizeSeed = dbConfig.sequelizeSeed;

var locConfigstock = dbConfig.locConfigStock;
var locConfigStockLive = dbConfig.locConfigStockLive;
var locConfigAuth = dbConfig.locConfigAuth;
var sequelizeStock = dbConfig.sequelizeStock;


// exports.getStockPricelist = () => new Promise(async (resolve, reject) => {
//     const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
//     try {
//         const query = `select distinct a."Crop_Code",b."Crop_Name","All_in_cost_Price",c."Receive_Unitname" from "Stock_Pricelist" a
//         inner join "mCrop" b on a."Crop_Code" = b."Crop_Code"
// 		inner join "Stock_Receive_Unit_Master" c on a."RECEIVE_UNITCD"= c."Receive_Unitcd"
//         where "F_Year"=(select "FIN_YR" from public."mFINYR" where "IS_ACTIVE"=1)
//         group by a."Crop_Code","All_in_cost_Price","VARIETY_AFTER_10YEAR",b."Crop_Name",c."Receive_Unitname" order by "Crop_Name"`;
//         const values = [];
//         // console.log(query);
//         const response = await client.query(query, values);
//         // console.log('response', response);
//         resolve(response.rows);
//     } catch (e) {
//         reject(new Error(`Oops! An error occurred: ${e}`));
//     } finally {
//         client.release();
//     }
// });
exports.getStockPricelist = () => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `select distinct a."Crop_Code",b."Crop_Name","All_in_cost_Price",c."Receive_Unitname","TOT_SUBSIDY" from "Stock_Pricelist" a
        inner join "mCrop" b on a."Crop_Code" = b."Crop_Code"
inner join "Stock_Receive_Unit_Master" c on a."RECEIVE_UNITCD"= c."Receive_Unitcd"
        where "F_Year"=(select "FIN_YR" from public."mFINYR" where "IS_ACTIVE"=1) and "VARIETY_AFTER_10YEAR"=0
        group by a."Crop_Code","All_in_cost_Price","VARIETY_AFTER_10YEAR",b."Crop_Name",c."Receive_Unitname","TOT_SUBSIDY" order by "Crop_Name"`;
        const values = [];
        // console.log(query);
        const response = await client.query(query, values);
        // console.log('response', response);
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
exports.getBlock = (DistrictCode) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const result = await sequelizeSeed.query(`select * from [DAFPSEED].[DBO].[same_as_block_table_onlyULBCase] where LGDIST_CODE='${DistrictCode}' order by block_name`, {
            replacements: {}, type: sequelizeStock.QueryTypes.SELECT
        });
        resolve(result);
    } catch (e) {
        console.log('An error occurred...', e);
        resolve([]);
        throw e
    }
});
exports.getDealerDetails = (DistrictCode) => new Promise(async (resolve, reject) => {
    try {
        const result = await sequelizeSeed.query(`SELECT distinct LIC_NO1,APP_FIRMNAME,APPADDRESS,e.block_name,e.* FROM [dafpseed].[dbo].[SEED_LIC_DIST] A 
      INNER JOIN [dafpseed].[dbo].[SEED_LIC_APP_DIST] B ON A.SEED_LIC_DIST_ID = B.SEED_LIC_DIST_ID 
      INNER JOIN [dafpseed].[dbo].[SEED_LIC_COMP_DIST] C ON A.SEED_LIC_DIST_ID = C.SEED_LIC_DIST_ID 
      inner join [dafpseed].[dbo].[dist] d on a.DIST_CODE = d.dist_code
      inner join [dafpseed].[dbo].[block] e on b.APPBLOCK_ID= e.block_code
      WHERE CONVERT(DATE, DATEADD(MONTH,1,A.APR_UPTO),103) >= CONVERT(DATE, GETDATE(), 103) AND A.LIC_ACTIVE = 1 AND A.IS_ACTIVE = 1 AND A.APP_STATUS = 'A' AND C.COMP_TYPE = 1 AND C.COMP_NAME = 'OSSC' and d.LGDistrict='${DistrictCode}' order by e.block_name,APP_FIRMNAME`, {
            replacements: {}, type: sequelizeStock.QueryTypes.SELECT
        });
        resolve(result);

    } catch (e) {
        console.log('An error occurred...', e);
        resolve([]);
        throw e
    }
});
exports.getblockWiseDealer = (BlockCode) => new Promise(async (resolve, reject) => {
    console.log('data', BlockCode);
    try {
        const result = await sequelizeSeed.query(`SELECT distinct LIC_NO1,APP_FIRMNAME,APPADDRESS,e.block_name,e.* FROM [dafpseed].[dbo].[SEED_LIC_DIST] A 
          INNER JOIN [dafpseed].[dbo].[SEED_LIC_APP_DIST] B ON A.SEED_LIC_DIST_ID = B.SEED_LIC_DIST_ID 
          INNER JOIN [dafpseed].[dbo].[SEED_LIC_COMP_DIST] C ON A.SEED_LIC_DIST_ID = C.SEED_LIC_DIST_ID 
          inner join [dafpseed].[dbo].[dist] d on a.DIST_CODE = d.dist_code
          inner join [dafpseed].[dbo].[block] e on b.APPBLOCK_ID= e.block_code
          WHERE CONVERT(DATE, DATEADD(MONTH,1,A.APR_UPTO),103) >= CONVERT(DATE, GETDATE(), 103) AND A.LIC_ACTIVE = 1 AND A.IS_ACTIVE = 1 AND A.APP_STATUS = 'A' AND C.COMP_TYPE = 1 AND C.COMP_NAME = 'OSSC' and e.block_code='${BlockCode}' order by e.block_name,APP_FIRMNAME`, {
            replacements: {}, type: sequelizeStock.QueryTypes.SELECT
        });
        resolve(result);

    } catch (e) {
        console.log('An error occurred...', e);
        resolve([]);
        throw e
    }
});
exports.dealerwisedata = (data) => new Promise(async (resolve, reject) => {
    try {
        const result = await sequelizeStock.query(`select distinct LICENCE_NO,APP_FIRMNAME,Variety_Name,Variety_Code,sum(STOCK_QUANTITY) rcvnoofbags,sum(AVL_QUANTITY)avlnoofbags from STOCK_DEALERSTOCK a
        inner join mCropVariety b on a.CROP_VERID=b.Variety_Code
        inner join dafpSeed.dbo.SEED_LIC_DIST c on a.LICENCE_NO=c.LIC_NO
        where FIN_YR='${data.year}' and  SEASSION='${data.season}'   and DIST_CODE='${data.district}' and a.CROP_ID='${data.crop}'  group by LICENCE_NO,Variety_Name,Variety_Code,APP_FIRMNAME order by APP_FIRMNAME`, {
            replacements: {}, type: sequelizeStock.QueryTypes.SELECT
        });
        resolve(result);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    }
});
exports.allfinYr = () => new Promise(async (resolve, reject) => {
    try {
        const result = await sequelizeStock.query(`select FIN_YR from mFINYR`, {
            replacements: {}, type: sequelizeStock.QueryTypes.SELECT
        });
        resolve(result);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    }
});
exports.getSeason = (year) => new Promise(async (resolve, reject) => {
    try {
        const result = await sequelizeStock.query(`select SHORT_NAME,SEASSION_NAME from mSEASSION where FIN_YR='${year}'`, {
            replacements: {}, type: sequelizeStock.QueryTypes.SELECT
        });
        resolve(result);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    }
});
exports.loadAllCrop = () => new Promise(async (resolve, reject) => {
    try {
        const result = await sequelizeStock.query(`select Crop_Name,Crop_Code from mCrop where is_active=1 order by Crop_Name`, {
            replacements: {}, type: sequelizeStock.QueryTypes.SELECT
        });
        resolve(result);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    }
});
exports.loadAllDistrict = () => new Promise(async (resolve, reject) => {
    try {
        const result = await sequelizeStock.query(`select dist_code,dist_name from [DAFPSEED].[DBO].dist order by dist_name`, {
            replacements: {}, type: sequelizeStock.QueryTypes.SELECT
        });
        resolve(result);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    }
});

exports.getcropList = () => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `select distinct "Crop_ID",b."Crop_Name",sum("Avl_Quantity") as "avlQtyInQTL" from public."Stock_StockDetails" a
        inner join "mCrop" b on a."Crop_ID" = b."Crop_Code"
        group by "Crop_ID",b."Crop_Name" order by "Crop_Name"`;
        const values = [];
        const response = await client.query(query, values);
        console.log('response', response.rows);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});


exports.graphVariety = (CropID) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `select a."Crop_ID",c."Dist_Name",sum(a."Avl_Quantity") as "avlQtyInQTL" from public."Stock_StockDetails" a
        inner join "mCrop" b on a."Crop_ID" = b."Crop_Code"
        inner join "Stock_District" c on BTRIM(a."Dist_Code", ' ') = c."Dist_Code" where a."Crop_ID"='${CropID}'
        group by a."Crop_ID","Dist_Name" ORDER BY SUM(a."Avl_Quantity") DESC`;
        const values = [];
        const response = await client.query(query, values);
        console.log('response', response.rows);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.manojdata = (vcode,updatedby) => new Promise(async (resolve, reject) => {
    try {
        const result = await sequelizeStock.query(`select  distinct LOT_NUMBER,sum(SALE_NO_OF_BAG) as sum from Stock_SaleDetails where  crop_verid='${vcode}' and updated_by='${updatedby}' and F_YEAR='2023-24' and STATUS='s' 
        group by LOT_NUMBER order by LOT_NUMBER desc`, {
            replacements: {vcode,updatedby}, type: sequelizeStock.QueryTypes.SELECT
        });
        const result1 = await sequelizeStock.query(` select distinct LOT_NO,sum(RECV_NO_OF_BAGS) as sum from STOCK_DEALERSTOCK   where FIN_YR='2023-24' and  SEASSION='K' and USERid='${updatedby}' and CROP_VERID='${vcode}'  group by LOT_NO  order by LOT_NO desc`, {
            replacements: {vcode,updatedby}, type: sequelizeStock.QueryTypes.SELECT
        });
        resolve({result2:result,result3:result1});
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } 
});
exports.manojdata1 = (vcode,lotno) => new Promise(async (resolve, reject) => {
    try {
        const result = await sequelizeStock.query(` select  CASH_MEMO_NO,SALE_TO,CROP_VERID,SALE_NO_OF_BAG,BAG_SIZE_KG,SALE_DATE,UPDATED_BY from Stock_SaleDetails where  crop_verid='${vcode}' and STATUS='s' and LOT_NUMBER='${lotno}' order by UPDATED_ON`, {
            replacements: {}, type: sequelizeStock.QueryTypes.SELECT
        });
       
        resolve(result);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } 
});


exports.getStockPricelistAfter = () => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `select distinct a."Crop_Code",b."Crop_Name","All_in_cost_Price",c."Receive_Unitname","TOT_SUBSIDY" from "Stock_Pricelist" a
        inner join "mCrop" b on a."Crop_Code" = b."Crop_Code"
inner join "Stock_Receive_Unit_Master" c on a."RECEIVE_UNITCD"= c."Receive_Unitcd"
        where "F_Year"=(select "FIN_YR" from public."mFINYR" where "IS_ACTIVE"=1) and "VARIETY_AFTER_10YEAR"=1
        group by a."Crop_Code","All_in_cost_Price","VARIETY_AFTER_10YEAR",b."Crop_Name",c."Receive_Unitname","TOT_SUBSIDY" order by "Crop_Name"`;
        const values = [];
        // console.log(query);
        const response = await client.query(query, values);
        // console.log('response', response);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});