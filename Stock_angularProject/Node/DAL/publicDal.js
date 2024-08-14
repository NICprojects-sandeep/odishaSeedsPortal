const pool = require('../config/dbConfig');
var dbConfig = require('../config/dbSqlConnection');
var sqlstock = dbConfig.sqlstock;
var sequelizeSeed = dbConfig.sequelizeSeed;

var locConfigstock = dbConfig.locConfigStock;
var locConfigStockLive = dbConfig.locConfigStockLive;
var locConfigAuth = dbConfig.locConfigAuth;
var sequelizeStock = dbConfig.sequelizeStock;

exports.addActivityLog = async (action, attack, mode, userID, ipAddress, url, deviceType, os, browser, Message) => {
    const client = await pool.connect().catch((err) => { console.log(`Unable to connect to the database: ${err}`); });
    try {
        const query = `insert into "ActivityLog" ("IPAddress", "UserID", "URL", "DeviceType", "OS", "Browser", "DateTime", "Action", "Attack", "Mode","Message") values ($1, $2, $3, $4, $5, $6, $7, $8, $9, $10,$11)`;
        const values = [ipAddress, userID, url, deviceType, os, browser, 'now()', action, attack, mode, Message];
        await client.query(query, values);
    } catch (e) {
        console.log(`Oops! An error occurred: ${e}`);
    } finally {
        client.release();
    }
};
// exports.getStockPricelist = () => new Promise(async (resolve, reject) => {
//     const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
//     try {
//         const query = `select distinct a."Crop_Code",b."Crop_Name","All_in_cost_Price",c."Receive_Unitname" from "Stock_Pricelist" a
//         inner join "mCrop" b on a."Crop_Code" = b."Crop_Code"
// 		inner join "Stock_Receive_Unit_Master" c on a."RECEIVE_UNITCD"= c."Receive_Unitcd"
//         where "F_Year"=(select "FIN_YR" from public."mFINYR" where "IS_ACTIVE"=1)
//         group by a."Crop_Code","All_in_cost_Price","VARIETY_AFTER_10YEAR",b."Crop_Name",c."Receive_Unitname" order by "Crop_Name"`;
//         const values = [];
//         const response = await client.query(query, values);
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
        const query = `select distinct a."Crop_Code",b."Crop_Name","All_in_cost_Price",c."Receive_Unitname","TOT_SUBSIDY","F_Year","seasons" from "Stock_Pricelist" a
        inner join "mCrop" b on a."Crop_Code" = b."Crop_Code"
inner join "Stock_Receive_Unit_Master" c on a."RECEIVE_UNITCD"= c."Receive_Unitcd"
        where "F_Year"=(select "FIN_YR" from public."mFINYR" where "IS_ACTIVE"=1) and "VARIETY_AFTER_10YEAR"=0 
		and seasons=(select "SHORT_NAME" from public."mSEASSION" where "IS_ACTIVE"=1)
        group by a."Crop_Code","All_in_cost_Price","VARIETY_AFTER_10YEAR",b."Crop_Name",c."Receive_Unitname","TOT_SUBSIDY","F_Year","seasons"   order by "Crop_Name"`;
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
        throw e;
    } finally {
        client.release();
    }
});
exports.getDealerDetails = (DistrictCode) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
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
    } finally {
        client.release();
    }
});
exports.getblockWiseDealer = (data) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        if (data.BlockCode == 'undefined') {
            data.BlockCode = 0;
        }
        const result = await sequelizeSeed.query(`SELECT distinct LIC_NO1,APP_FIRMNAME,APPADDRESS,e.block_name,e.* FROM [dafpseed].[dbo].[SEED_LIC_DIST] A 
          INNER JOIN [dafpseed].[dbo].[SEED_LIC_APP_DIST] B ON A.SEED_LIC_DIST_ID = B.SEED_LIC_DIST_ID 
          INNER JOIN [dafpseed].[dbo].[SEED_LIC_COMP_DIST] C ON A.SEED_LIC_DIST_ID = C.SEED_LIC_DIST_ID 
          inner join [dafpseed].[dbo].[dist] d on a.DIST_CODE = d.dist_code
          inner join [dafpseed].[dbo].[block] e on b.APPBLOCK_ID= e.block_code
          WHERE CONVERT(DATE, DATEADD(MONTH,1,A.APR_UPTO),103) >= CONVERT(DATE, GETDATE(), 103) AND A.LIC_ACTIVE = 1 AND A.IS_ACTIVE = 1 AND A.APP_STATUS = 'A' AND C.COMP_TYPE = 1 AND C.COMP_NAME = 'OSSC' and d.LGDistrict='${data.DistrictCode}' and (${data.BlockCode} = 0 or e.block_code='${data.BlockCode}')  order by e.block_name,APP_FIRMNAME`, {
            replacements: {}, type: sequelizeStock.QueryTypes.SELECT
        });
        resolve(result);

    } catch (e) {
        console.log('An error occurred...', e);
        resolve([]);

        throw e
    } finally {
        client.release();
    }
});
exports.dealerwisedata = (data) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        // if (data.season == 'K') {
        //     const result = await sequelizeStock.query(`select distinct LICENCE_NO,APP_FIRMNAME,Variety_Name,Variety_Code,sum(STOCK_QUANTITY) rcvnoofbags,sum(AVL_QUANTITY)avlnoofbags from STOCK_DEALERSTOCK a
        //     inner join mCropVariety b on a.CROP_VERID=b.Variety_Code
        //     inner join dafpSeed.dbo.SEED_LIC_DIST c on a.LICENCE_NO=c.LIC_NO
        //     where FIN_YR='${data.year}' and  SEASSION='${data.season}'   and DIST_CODE='${data.district}' and a.CROP_ID='${data.crop}'  group by LICENCE_NO,Variety_Name,Variety_Code,APP_FIRMNAME order by APP_FIRMNAME`, {
        //         replacements: {}, type: sequelizeStock.QueryTypes.SELECT
        //     });
        //     resolve(result);
        // }
        // else {
        const query = `select distinct a."LICENCE_NO","Variety_Name","Variety_Code",sum("STOCK_QUANTITY") rcvnoofbags,sum("AVL_QUANTITY")avlnoofbags,'' "APP_FIRMNAME" from "STOCK_DEALERSTOCK" a
                            inner join "mCropVariety" b on a."CROP_VERID"=b."Variety_Code"
                            inner join "Stock_District" c on (SUBSTRING(a."LICENCE_NO",3,3)=SUBSTRING(c."Dist_Name",1,3)) 
                            where "FIN_YR"=$1 and  "SEASSION"=$2   and "Dist_Code"=$3 and a."CROP_ID"=$4  group by "LICENCE_NO","Variety_Name","Variety_Code"`;
        const values = [data.year, data.season, data.district, data.crop];
        const response = await client.query(query, values);
        resolve(response.rows);
        // }

    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));

    } finally {
        client.release();
    }
});
exports.dealerwisedataWithFarmName = (data) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const promises = data.map(async (e) => {
            const result = await sequelizeStock.query(`select * from  [dafpseed].[dbo].[SEED_LIC_DIST] where lic_no=:LICENCE_NO`, {
                replacements: { LICENCE_NO: e.LICENCE_NO },
                type: sequelizeStock.QueryTypes.SELECT
            });
            if (result[0] != undefined) {
                e.APP_FIRMNAME = result[0].APP_FIRMNAME;
            }

            return e;
        });
        Promise.all(promises)
            .then((saledetails) => {
                resolve(saledetails);
            })
            .catch((error) => {

                console.error("An error occurred:", error);
                reject(error);
            });
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));

    } finally {
        client.release();
    }
});
exports.allfinYr = () => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const result = await sequelizeStock.query(`select FIN_YR from mFINYR`, {
            replacements: {}, type: sequelizeStock.QueryTypes.SELECT
        });
        resolve(result);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));

    } finally {
        client.release();
    }
});
exports.getSeason = (year) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const result = await sequelizeStock.query(`select SHORT_NAME,SEASSION_NAME from mSEASSION where FIN_YR='${year}'`, {
            replacements: {}, type: sequelizeStock.QueryTypes.SELECT
        });
        resolve(result);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));

    } finally {
        client.release();
    }
});
exports.loadAllCrop = () => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const result = await sequelizeStock.query(`select Crop_Name,Crop_Code from mCrop where is_active=1 order by Crop_Name`, {
            replacements: {}, type: sequelizeStock.QueryTypes.SELECT
        });
        resolve(result);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));

    } finally {
        client.release();
    }
});
exports.loadAllDistrict = () => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const result = await sequelizeStock.query(`select dist_code,dist_name from [DAFPSEED].[DBO].dist order by dist_name`, {
            replacements: {}, type: sequelizeStock.QueryTypes.SELECT
        });
        resolve(result);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));

    } finally {
        client.release();
    }
});

exports.getcropList = () => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `select distinct "Crop_ID",b."Crop_Name",sum("Avl_Quantity") as "avlQtyInQTL" from public."Stock_StockDetails" a
        inner join "mCrop" b on a."Crop_ID" = b."Crop_Code"
        where "FIN_YR"=(select "FIN_YR" from public."mFINYR" where "IS_ACTIVE"=1) 
        and "SEASSION_NAME"=(select "SHORT_NAME" from public."mSEASSION" where "IS_ACTIVE"=1)
        group by "Crop_ID",b."Crop_Name" order by "Crop_Name"`;
        const values = [];
        const response = await client.query(query, values);
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
        and "FIN_YR"=(select "FIN_YR" from public."mFINYR" where "IS_ACTIVE"=1) 
        and "SEASSION_NAME"=(select "SHORT_NAME" from public."mSEASSION" where "IS_ACTIVE"=1)
        group by a."Crop_ID","Dist_Name" ORDER BY SUM(a."Avl_Quantity") DESC`;
        const values = [];
        const response = await client.query(query, values);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.manojdata = (vcode, updatedby) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const result = await sequelizeStock.query(`select  distinct LOT_NUMBER,sum(SALE_NO_OF_BAG) as sum from Stock_SaleDetails where  crop_verid='${vcode}' and updated_by='${updatedby}' and F_YEAR='2023-24' and STATUS='s' 
        group by LOT_NUMBER order by LOT_NUMBER desc`, {
            replacements: { vcode, updatedby }, type: sequelizeStock.QueryTypes.SELECT
        });
        const result1 = await sequelizeStock.query(` select distinct LOT_NO,sum(RECV_NO_OF_BAGS) as sum from STOCK_DEALERSTOCK   where FIN_YR='2023-24' and  SEASSION='K' and USERid='${updatedby}' and CROP_VERID='${vcode}'  group by LOT_NO  order by LOT_NO desc`, {
            replacements: { vcode, updatedby }, type: sequelizeStock.QueryTypes.SELECT
        });
        resolve({ result2: result, result3: result1 });
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));

    } finally {
        client.release();
    }
});
exports.manojdata1 = (vcode, lotno) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const result = await sequelizeStock.query(` select  CASH_MEMO_NO,SALE_TO,CROP_VERID,SALE_NO_OF_BAG,BAG_SIZE_KG,SALE_DATE,UPDATED_BY from Stock_SaleDetails where  crop_verid='${vcode}' and STATUS='s' and LOT_NUMBER='${lotno}' order by UPDATED_ON`, {
            replacements: {}, type: sequelizeStock.QueryTypes.SELECT
        });

        resolve(result);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));

    } finally {
        client.release();
    }
});


exports.getStockPricelistAfter = () => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `select distinct a."Crop_Code",b."Crop_Name","All_in_cost_Price",c."Receive_Unitname","TOT_SUBSIDY" from "Stock_Pricelist" a
        inner join "mCrop" b on a."Crop_Code" = b."Crop_Code"
inner join "Stock_Receive_Unit_Master" c on a."RECEIVE_UNITCD"= c."Receive_Unitcd"
        where "F_Year"=(select "FIN_YR" from public."mFINYR" where "IS_ACTIVE"=1) and "VARIETY_AFTER_10YEAR"=1
        and seasons=(select "SHORT_NAME" from public."mSEASSION" where "IS_ACTIVE"=1)
        group by a."Crop_Code","All_in_cost_Price","VARIETY_AFTER_10YEAR",b."Crop_Name",c."Receive_Unitname","TOT_SUBSIDY" order by "Crop_Name"`;
        const values = [];
        const response = await client.query(query, values);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.AddGodwns = (data) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        let AgenciesID = '08';
        let User_Type = 'OSSC';
        var SUPPLY_TYPE = '';
        let response_DIST_CODE = ''
        let mCONFIRM_STATUS = '';
        let MAXTRAN_NO = 0;
        let SALETRANSID = '';
        var mSALETRANSID = '';
        let RECVTRANSID = '';

        const FIN_YR = `select "FIN_YR" from "mFINYR" where "IS_ACTIVE"=1`;
        const response_FIN_YR = await client.query(FIN_YR);
        const SEASSION = `select "SHORT_NAME" from "mSEASSION" where "IS_ACTIVE"=1`;
        const response_SEASSION = await client.query(SEASSION);
        if (data.CROP_CLASS == 'Certified-II') {
            data.CROP_CLASS = 'Certified'
        }
        const QUANTITY = (parseFloat(data.BAG_SIZE.toFixed(2)) * parseFloat(data.NO_OF_BAGS.toFixed(2))) / 100;
        if (data.APIKEY == 'key12145') {
            SUPPLY_TYPE = 8
        }
        else if (data.APIKEY == 'key12146') {
            SUPPLY_TYPE = 7
        }
        const CASH_MEMO_NO = `SELECT * FROM "Stock_SaleDetails" where "CASH_MEMO_NO"=$1  `;
        const CASH_MEMO_NO_values = [data.CASH_MEMO_NO]
        const response_CASH_MEMO_NO = await client.query(CASH_MEMO_NO, CASH_MEMO_NO_values);
        if (response_CASH_MEMO_NO.rows.length > 0) {
            const DIST_CODE = `SELECT "Dist_Code" FROM "Stock_Godown_Master" WHERE "Godown_ID"=$1  `;
            const DIST_CODE_values = [data.GODOWN_ID]
            response_DIST_CODE = await client.query(DIST_CODE, DIST_CODE_values);
        }
        else {
            const DIST_CODE = `SELECT "Dist_Code" FROM "Stock_Godown_Master" WHERE "Godown_ID"=$1  `;
            const DIST_CODE_values = [data.SALE_TO]
            response_DIST_CODE = await client.query(DIST_CODE, DIST_CODE_values);
        }
        const DIST_NAME = `SELECT SUBSTRING("Dist_Name" FROM 1 FOR 4) AS "Dist_Name" FROM "Stock_District" WHERE "Dist_Code"=$1`;
        const DIST_NAME_values = [response_DIST_CODE.rows[0].Dist_Code];
        const response_DIST_NAME = await client.query(DIST_NAME, DIST_NAME_values);

        const RECEIVE_UNITCD = `SELECT "Receive_Unitcd" FROM "Stock_StockDetails" WHERE "Lot_No" =$1  ORDER BY "EntryDate" limit 1 `;
        const RECEIVE_UNITCD_values = [data.LOT_NO]
        const response_RECEIVE_UNITCD = await client.query(RECEIVE_UNITCD, RECEIVE_UNITCD_values);

        const AVLQTY = `SELECT "Avl_Quantity" FROM "Stock_StockDetails" WHERE "Lot_No" =  $1 AND "Godown_ID" = $2`;
        const AVLQTY_values = [data.LOT_NO, data.GODOWN_ID]
        const response_AVLQTY = await client.query(AVLQTY, AVLQTY_values);

        const insertintoapi08 = `INSERT INTO public."API_08"(
            "DIST_CODE", "GODOWN_ID", "TRANSFER_DATE", "SALE_TO", "SEASSION", "FIN_YR", "USERID", "USERIP", "CATEGORY_ID", "CROP_ID", "CROP_CLASS", "VARIETY_ID", "LOT_NO", "BAG_SIZE", "NO_OF_BAGS", "QUANTITY", "CASH_MEMO_NO", "APIKEY", "UPDATED_ON")
         values ($1, $2, $3, $4, $5, $6, $7, $8, $9, $10,$11, $12, $13, $14, $15, $16, $17, $18, $19)`;
        const insertintoapi08values = [response_DIST_CODE.rows[0].Dist_Code, data.GODOWN_ID, data.TRANSFER_DATE, data.SALE_TO, response_SEASSION.rows[0].SHORT_NAME, response_FIN_YR.rows[0].FIN_YR, data.USERID, data.USERIP, data.CATEGORY_ID, data.CROP_ID, data.CROP_CLASS, data.VARIETY_ID, data.LOT_NO, data.BAG_SIZE, data.NO_OF_BAGS, QUANTITY, data.CASH_MEMO_NO, data.APIKEY, 'now()'];
        await client.query(insertintoapi08, insertintoapi08values);


        const length = `SELECT * FROM "Stock_SaleDetails" WHERE "CASH_MEMO_NO"= $1`;
        const lengthValues = [data.CASH_MEMO_NO]
        const response_length = await client.query(length, lengthValues);

        // const AVLQTY = `SELECT "Avl_Quantity" FROM "Stock_StockDetails" WHERE "Godown_ID" = $1 AND "Lot_No" = $2`;
        // const AVLQTYValues = [data.GODOWN_ID,data.LOT_NO]
        // const response_AVLQTY = await client.query(AVLQTY, AVLQTYValues);
        if (response_length.rows.length == 0) {
            mCONFIRM_STATUS = 0
            if (QUANTITY <= response_AVLQTY.rows[0].Avl_Quantity) {
                const MAXTRAN_NO_Calculate = `SELECT NULLIF( CAST(LEFT(SUBSTRING("SALETRANSID" FROM 16 FOR LENGTH("SALETRANSID")),POSITION('-' IN SUBSTRING("SALETRANSID" FROM 16 FOR LENGTH("SALETRANSID"))) - 1) AS INTEGER) + 1,0) as "Maxno" FROM "Stock_SaleDetails" WHERE SUBSTRING("SALETRANSID" FROM 8 FOR 7) = $2  AND SUBSTRING("SALETRANSID" FROM 3 FOR 4) = $1 ORDER BY "Maxno" DESC limit 1;`;
                const MAXTRAN_NO_CalculateValues = [response_DIST_NAME.rows[0].Dist_Name, response_FIN_YR.rows[0].FIN_YR]
                const response_MAXTRAN_NO_Calculate = await client.query(MAXTRAN_NO_Calculate, MAXTRAN_NO_CalculateValues);
                if (response_MAXTRAN_NO_Calculate.rows.length == 0) {
                    MAXTRAN_NO = 1
                }
                else {
                    MAXTRAN_NO = response_MAXTRAN_NO_Calculate.rows[0].Maxno
                }
                SALETRANSID = 'S/' + response_DIST_NAME.rows[0].Dist_Name + '/' + response_FIN_YR.rows[0].FIN_YR + '/' + MAXTRAN_NO;
                mSALETRANSID = SALETRANSID + '-1';
                const insertintoStock_SaleDetails = `INSERT INTO public."Stock_SaleDetails"(
                    "SALETRANSID", "SUPPLY_TYPE","GODOWN_ID","SALE_DATE", "SALE_TO","CASH_MEMO_NO", "CROPCATG_ID", "CROP_ID", "CROP_VERID", "CLASS", "Receive_Unitcd", "LOT_NUMBER", "BAG_SIZE_KG", "SALE_NO_OF_BAG", "CONFIRM_STATUS", "STATUS", "SEASONS", "F_YEAR", "UPDATED_BY", "UPDATED_ON", "USER_TYPE", "USERIP", "IS_ACTIVE")
                    values ($1, $2, $3, $4, $5, $6, $7, $8, $9, $10,$11, $12, $13, $14, $15, $16, $17, $18, $19,$20,$21,$22,$23)`;
                const insertintoStock_SaleDetailsvalues = [mSALETRANSID, SUPPLY_TYPE, data.GODOWN_ID, data.TRANSFER_DATE, data.SALE_TO, data.CASH_MEMO_NO, data.CATEGORY_ID, data.CROP_ID, data.VARIETY_ID, data.CROP_CLASS, response_RECEIVE_UNITCD.rows[0].Receive_Unitcd, data.LOT_NO, data.BAG_SIZE, data.NO_OF_BAGS, mCONFIRM_STATUS, 'T', response_SEASSION.rows[0].SHORT_NAME, response_FIN_YR.rows[0].FIN_YR, data.USERID, 'now()', 'OSSC', data.USERIP, 'Y'];
                await client.query(insertintoStock_SaleDetails, insertintoStock_SaleDetailsvalues);


                const UPDATE_Stock_StockDetails1 = `UPDATE "Stock_StockDetails" SET "AVL_NO_OF_BAGS" = "AVL_NO_OF_BAGS" - $1,"Avl_Quantity" = "Avl_Quantity" - $2                 
                WHERE "Crop_Verid" = $3 AND "Class" = $4 AND "Receive_Unitcd" = $5 AND "Lot_No" = $6 AND "Bag_Size_In_kg" = $7 AND "User_Type" = 'OSSC' AND "Godown_ID" = $8 AND "VALIDITY" = true;`;
                const UPDATE_Stock_StockDetails1_values = [data.NO_OF_BAGS, QUANTITY, data.VARIETY_ID, data.CROP_CLASS, response_RECEIVE_UNITCD.rows[0].Receive_Unitcd, data.LOT_NO, data.BAG_SIZE, data.GODOWN_ID];
                const response_UPDATE_Stock_StockDetails1 = await client.query(UPDATE_Stock_StockDetails1, UPDATE_Stock_StockDetails1_values);
                resolve(true);

            }
        }
        else {
            const MAXTRAN_NO_Calculate = `SELECT "RECVTRANSID",CAST( LEFT( SUBSTRING("RECVTRANSID" FROM 16 FOR LENGTH("RECVTRANSID")),POSITION('-' IN SUBSTRING("RECVTRANSID" FROM 16 FOR LENGTH("RECVTRANSID"))) - 1) AS INTEGER) + 1  as "Maxno" FROM "Stock_ReceiveDetails" WHERE SUBSTRING("RECVTRANSID" FROM 8 FOR 7)= $2  AND SUBSTRING("RECVTRANSID" FROM 3 FOR 4) = $1 ORDER BY "Maxno" DESC limit 1;`;
            const MAXTRAN_NO_CalculateValues = [response_DIST_NAME.rows[0].Dist_Name, response_FIN_YR.rows[0].FIN_YR]
            const response_MAXTRAN_NO_Calculate = await client.query(MAXTRAN_NO_Calculate, MAXTRAN_NO_CalculateValues);
            if (response_MAXTRAN_NO_Calculate.rows.length == 0) {
                MAXTRAN_NO = 1
            }
            else {
                MAXTRAN_NO = response_MAXTRAN_NO_Calculate.rows[0].Maxno
            }
            RECVTRANSID = 'R/' + response_DIST_NAME.rows[0].Dist_Name + '/' + response_FIN_YR.rows[0].FIN_YR + '/' + MAXTRAN_NO;

            const TESTING_DATE_EXPIRY_DATE = `SELECT "TESTING_DATE","EXPIRY_DATE" FROM "Stock_StockDetails" WHERE "Lot_No" = $1 AND "Crop_ID" = $2 AND "Crop_Verid" = $3 AND "CropCatg_ID" = $4 AND "TESTING_DATE" IS NOT NULL;`;
            const TESTING_DATE_EXPIRY_DATE_Values = [data.LOT_NO, data.CROP_ID, data.VARIETY_ID, data.CATEGORY_ID]
            const response_TESTING_DATE_EXPIRY_DATE = await client.query(TESTING_DATE_EXPIRY_DATE, TESTING_DATE_EXPIRY_DATE_Values);

            const TRNS_NO = `SELECT "SALE_NO_OF_BAG" FROM "Stock_SaleDetails" WHERE "CASH_MEMO_NO" = $1 AND "LOT_NUMBER" = $2 AND "CONFIRM_STATUS" = 0;`;
            const TRNS_NO_Values = [data.CASH_MEMO_NO, data.LOT_NO]
            const response_TRNS_NO = await client.query(TRNS_NO, TRNS_NO_Values);

            let TRNR_NO = data.NO_OF_BAGS;
            let TRNS_QTY = parseFloat((parseInt(data.BAG_SIZE) * parseInt(response_TRNS_NO.rows[0].SALE_NO_OF_BAG)) / 100).toFixed(2);
            let TRNR_QTY = QUANTITY;
            if (response_TRNS_NO.rows[0].SALE_NO_OF_BAG == TRNR_NO) {
                const UPDATE_Stock_StockDetails2 = `UPDATE "Stock_SaleDetails" SET "CONFIRM_STATUS" = 1 WHERE "CASH_MEMO_NO" = $1 AND "LOT_NUMBER" =$2;`;
                const UPDATE_Stock_StockDetails2_values = [data.CASH_MEMO_NO, data.LOT_NO];
                const response_UPDATE_Stock_StockDetails2 = await client.query(UPDATE_Stock_StockDetails2, UPDATE_Stock_StockDetails2_values);

                const insertintoStock_ReceiveDetails = `INSERT INTO public."Stock_ReceiveDetails"("RECVTRANSID", "Dist_Code", "Godown_ID", "AgenciesID", "Receive_Unitcd","Challan_No", "CropCatg_ID","Crop_ID", "Crop_Verid", "Class","Lot_No", "Bag_Size_In_kg", "Recv_No_Of_Bags", "Recv_Date","Recv_Quantity","SEASSION_NAME", "FIN_YR", "User_Type", "EntryDate", "UserID","UserIP", "TESTING_DATE", "EXPIRY_DATE")
                    values ($1, $2, $3, $4, $5, $6, $7, $8, $9, $10,$11, $12, $13, $14, $15, $16, $17, $18, $19,$20,$21,$22,$23)`;
                const insertintoStock_ReceiveDetailsvalues = [RECVTRANSID + '-1', response_DIST_CODE.rows[0].Dist_Code, data.SALE_TO, AgenciesID, response_RECEIVE_UNITCD.rows[0].Receive_Unitcd, data.CASH_MEMO_NO, data.CATEGORY_ID, data.CROP_ID, data.VARIETY_ID, data.CROP_CLASS, data.LOT_NO, data.BAG_SIZE, data.NO_OF_BAGS, data.TRANSFER_DATE, QUANTITY, response_SEASSION.rows[0].SHORT_NAME, response_FIN_YR.rows[0].FIN_YR, 'OSSC', 'now()', data.USERID, data.USERIP, response_TESTING_DATE_EXPIRY_DATE.rows[0].TESTING_DATE, response_TESTING_DATE_EXPIRY_DATE.rows[0].EXPIRY_DATE];
                await client.query(insertintoStock_ReceiveDetails, insertintoStock_ReceiveDetailsvalues);
            }
            else {
                const mSALETRANSID = `SELECT SUBSTRING("SALETRANSID" FROM 1 FOR LENGTH("SALETRANSID") - 1) FROM "Stock_SaleDetails" WHERE "CASH_MEMO_NO" = $2 AND "LOT_NUMBER" = $1;`;
                const mSALETRANSID_Values = [data.LOT_NO, data.CASH_MEMO_NO]
                const response_mSALETRANSID = await client.query(mSALETRANSID, mSALETRANSID_Values);

                const UPDATE_Stock_StockDetails3 = `UPDATE "Stock_SaleDetails" SET "CONFIRM_STATUS" = 1 WHERE "CASH_MEMO_NO" = $1 AND "LOT_NUMBER" =$2;`;
                const UPDATE_Stock_StockDetails3_values = [data.CASH_MEMO_NO, data.LOT_NO];
                const response_UPDATE_Stock_StockDetails3 = await client.query(UPDATE_Stock_StockDetails3, UPDATE_Stock_StockDetails3_values);

                const insertintoStock_SaleDetails = `INSERT INTO public."Stock_SaleDetails"(
                    "SALETRANSID", "SUPPLY_TYPE","GODOWN_ID","SALE_DATE", "SALE_TO","CASH_MEMO_NO", "CROPCATG_ID", "CROP_ID", "CROP_VERID", "CLASS", "Receive_Unitcd", "LOT_NUMBER", "BAG_SIZE_KG", "SALE_NO_OF_BAG", "CONFIRM_STATUS", "STATUS", "SEASONS", "F_YEAR", "UPDATED_BY", "UPDATED_ON", "USER_TYPE", "USERIP", "IS_ACTIVE")
                   values ($1, $2, $3, $4, $5, $6, $7, $8, $9, $10,$11, $12, $13, $14, $15, $16, $17, $18, $19,$20,$21,$22,$23)`;
                const insertintoStock_SaleDetailsvalues = [mSALETRANSID + '2', 7, data.GODOWN_ID, data.TRANSFER_DATE, data.SALE_TO, data.CASH_MEMO_NO, data.CATEGORY_ID, data.CROP_ID, data.VARIETY_ID, data.CROP_CLASS, response_RECEIVE_UNITCD.rows[0].Receive_Unitcd, data.LOT_NO, data.BAG_SIZE, response_TRNS_NO.rows[0].SALE_NO_OF_BAG - TRNR_NO, 1, 'M', response_SEASSION.rows[0].SHORT_NAME, response_FIN_YR.rows[0].FIN_YR, data.USERID, 'now()', 'OSSC', data.USERIP, 'Y'];
                await client.query(insertintoStock_SaleDetails, insertintoStock_SaleDetailsvalues);

                const insertintoStock_ReceiveDetails1 = `INSERT INTO public."Stock_ReceiveDetails"(
                    "RECVTRANSID", "Dist_Code", "Godown_ID", "AgenciesID", "Receive_Unitcd", "Challan_No", "CropCatg_ID","Crop_ID", "Crop_Verid", "Class","Lot_No", "Bag_Size_In_kg", "Recv_No_Of_Bags", "Recv_Date", "Recv_Quantity","SEASSION_NAME", "FIN_YR", "User_Type", "EntryDate", "UserID", "UserIP", "TESTING_DATE", "EXPIRY_DATE")
                    values ($1, $2, $3, $4, $5, $6, $7, $8, $9, $10,$11, $12, $13, $14, $15, $16, $17, $18, $19,$20,$21,$22,$23)`;
                const insertintoStock_ReceiveDetailsvalues1 = [RECVTRANSID + '-1', response_DIST_CODE.rows[0].Dist_Code, data.SALE_TO, AgenciesID, response_RECEIVE_UNITCD.rows[0].Receive_Unitcd, data.CASH_MEMO_NO, data.CATEGORY_ID, data.CROP_ID, data.VARIETY_ID, data.CROP_CLASS, data.LOT_NO, data.BAG_SIZE, TRNR_NO, data.TRANSFER_DATE, TRNR_QTY, response_SEASSION.rows[0].SHORT_NAME, response_FIN_YR.rows[0].FIN_YR, 'OSSC', 'now()', data.USERID, data.USERIP, response_TESTING_DATE_EXPIRY_DATE.rows[0].TESTING_DATE, response_TESTING_DATE_EXPIRY_DATE.rows[0].EXPIRY_DATE];
                await client.query(insertintoStock_ReceiveDetails1, insertintoStock_ReceiveDetailsvalues1);

                const insertintoStock_ReceiveDetails2 = `INSERT INTO public."Stock_ReceiveDetails"(
                    "RECVTRANSID", "Dist_Code", "Godown_ID", "AgenciesID", "Receive_Unitcd", "Challan_No", "CropCatg_ID","Crop_ID", "Crop_Verid", "Class","Lot_No", "Bag_Size_In_kg", "Recv_No_Of_Bags","Recv_Date", "Recv_Quantity","SEASSION_NAME", "FIN_YR", "User_Type", "EntryDate", "UserID", "UserIP", "TESTING_DATE", "EXPIRY_DATE")
                    values ($1, $2, $3, $4, $5, $6, $7, $8, $9, $10,$11, $12, $13, $14, $15, $16, $17, $18, $19,$20,$21,$22,$23)`;
                const insertintoStock_ReceiveDetailsvalues2 = [RECVTRANSID + '-1', response_DIST_CODE.rows[0].Dist_Code, data.SALE_TO, AgenciesID, response_RECEIVE_UNITCD.rows[0].Receive_Unitcd, data.CASH_MEMO_NO, data.CATEGORY_ID, data.CROP_ID, data.VARIETY_ID, data.CROP_CLASS, data.LOT_NO, data.BAG_SIZE, response_TRNS_NO.rows[0].SALE_NO_OF_BAG - TRNR_NO, data.TRANSFER_DATE, TRNS_QTY - TRNR_QTY, response_SEASSION.rows[0].SHORT_NAME, response_FIN_YR.rows[0].FIN_YR, 'OSSC', 'now()', data.USERID, data.USERIP, response_TESTING_DATE_EXPIRY_DATE.rows[0].TESTING_DATE, response_TESTING_DATE_EXPIRY_DATE.rows[0].EXPIRY_DATE];
                await client.query(insertintoStock_ReceiveDetails2, insertintoStock_ReceiveDetailsvalues2);

            }
            const checkStock_StockDetails = `SELECT * FROM "Stock_StockDetails" WHERE "Dist_Code" = $1 AND "Godown_ID" = $2 AND "Crop_Verid" = $3 AND "Receive_Unitcd" = $4 AND "Lot_No" = $5 AND "FIN_YR" = $6 AND "SEASSION_NAME" = $7 AND "User_Type" = 'OSSC';`;
            const checkStock_StockDetails_Values = [response_DIST_CODE.rows[0].Dist_Code, data.SALE_TO, data.VARIETY_ID, response_RECEIVE_UNITCD.rows[0].Receive_Unitcd, data.LOT_NO, response_FIN_YR.rows[0].FIN_YR, response_SEASSION.rows[0].SHORT_NAME]
            const response_checkStock_StockDetails = await client.query(checkStock_StockDetails, checkStock_StockDetails_Values);
            if (response_checkStock_StockDetails.rows.length == 0) {
                const stock_id = `SELECT COALESCE(MAX("Stock_ID"), 0) + 1 as "Stock_ID"  FROM "Stock_StockDetails";`;
                const stock_id_Values = []
                const response_stock_id = await client.query(stock_id, stock_id_Values);

                const insertintoStock_ReceiveDetails1 = `INSERT INTO public."Stock_StockDetails"(
                    "Stock_ID", "Dist_Code", "Godown_ID", "CropCatg_ID", "Crop_ID",
                     "Crop_Verid", "Class", "Receive_Unitcd","Lot_No", "Bag_Size_In_kg",
                      "Recv_No_Of_Bags", "AVL_NO_OF_BAGS", "Stock_Date", "Stock_Quantity", "Avl_Quantity",
                       "SEASSION_NAME", "FIN_YR", "User_Type", "EntryDate", "UserID", 
                       "UserIP", "Entry_Status", "TESTING_DATE", "EXPIRY_DATE","VALIDITY")
                    values ($1, $2, $3, $4, $5, $6, $7, $8, $9, $10,$11, $12, $13, $14, $15, $16, $17, $18, $19,$20,$21,$22,$23,$24,$25)`;
                const insertintoStock_ReceiveDetailsvalues1 = [
                    response_stock_id.rows[0].Stock_ID, response_DIST_CODE.rows[0].Dist_Code, data.SALE_TO, data.CATEGORY_ID, data.CROP_ID,
                    data.VARIETY_ID, data.CROP_CLASS, response_RECEIVE_UNITCD.rows[0].Receive_Unitcd, data.LOT_NO, data.BAG_SIZE,
                    data.NO_OF_BAGS, data.NO_OF_BAGS, data.TRANSFER_DATE, QUANTITY, QUANTITY,
                    response_SEASSION.rows[0].SHORT_NAME, response_FIN_YR.rows[0].FIN_YR, 'OSSC', 'now()', data.USERID, data.USERIP,
                    'T', response_TESTING_DATE_EXPIRY_DATE.rows[0].TESTING_DATE, response_TESTING_DATE_EXPIRY_DATE.rows[0].EXPIRY_DATE, 'true'];
                await client.query(insertintoStock_ReceiveDetails1, insertintoStock_ReceiveDetailsvalues1);
                resolve(true);

            }
            else {
                const Stock_StockDetails_data = `SELECT "Recv_No_Of_Bags",  "AVL_NO_OF_BAGS", "Stock_Quantity",  "Avl_Quantity" FROM   "Stock_StockDetails" WHERE  "Dist_Code" = $1 AND "Godown_ID" = $2 AND "Receive_Unitcd" = $4  AND "Crop_Verid" = $3 AND "Lot_No" = $5 AND "FIN_YR" = $6 AND "User_Type" = 'OSSC';`;
                const Stock_StockDetails_data_Values = [response_DIST_CODE.rows[0].Dist_Code, data.SALE_TO, data.VARIETY_ID, response_RECEIVE_UNITCD.rows[0].Receive_Unitcd, data.LOT_NO, response_FIN_YR.rows[0].FIN_YR]
                const response_Stock_StockDetails_data = await client.query(Stock_StockDetails_data, Stock_StockDetails_data_Values);


                const UPDATE_Stock_StockDetails4 = `UPDATE "Stock_StockDetails" SET "Recv_No_Of_Bags" = CAST($12 AS INTEGER) +CAST($1 AS INTEGER), "AVL_NO_OF_BAGS" = CAST($12 AS INTEGER) +CAST($2 AS INTEGER), "Stock_Quantity" = CAST($13 AS DOUBLE PRECISION) + CAST($3 AS DOUBLE PRECISION), "Avl_Quantity" = CAST($13 AS DOUBLE PRECISION) + CAST($4 AS DOUBLE PRECISION) WHERE "Dist_Code" = $5 AND "Godown_ID" = $6 AND "Receive_Unitcd" = $7 AND "Crop_Verid" = $8  AND "Lot_No" = $9 AND "FIN_YR" = $10 AND "SEASSION_NAME" = $11 AND "User_Type" = 'OSSC';`;
                const UPDATE_Stock_StockDetails4_values = [response_Stock_StockDetails_data.rows[0].Recv_No_Of_Bags, response_Stock_StockDetails_data.rows[0].AVL_NO_OF_BAGS, response_Stock_StockDetails_data.rows[0].Stock_Quantity, response_Stock_StockDetails_data.rows[0].Avl_Quantity, response_DIST_CODE.rows[0].Dist_Code, data.SALE_TO, response_RECEIVE_UNITCD.rows[0].Receive_Unitcd, data.VARIETY_ID, data.LOT_NO, response_FIN_YR.rows[0].FIN_YR, response_SEASSION.rows[0].SHORT_NAME, data.NO_OF_BAGS, QUANTITY];
                const response_UPDATE_Stock_StockDetails4 = await client.query(UPDATE_Stock_StockDetails4, UPDATE_Stock_StockDetails4_values);
                resolve(true);
            }
        }
    } catch (e) {
        console.log(new Error(`Oops! An error occurred: ${e}`));
        const UPDATE_API_08 = `update "API_08" set "Error"=1 where "API_08ID" in( select "API_08ID" from "API_08" where "CASH_MEMO_NO"=$1 order by "API_08ID" desc limit 1)`;
        const UPDATE_API_08_values = [data.CASH_MEMO_NO];
        const response_UPDATE_API_08 = await client.query(UPDATE_API_08, UPDATE_API_08_values);
        resolve(false);
    } finally {
        client.release();
    }
});
exports.fillnews = () => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `SELECT * FROM "mLATESTNEWS" WHERE "IS_ACTIVE" = 1 ORDER BY "NEWS" DESC`;
        const values = [];
        const response = await client.query(query, values);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});

exports.AddSeed = (data) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        let Recv_Quantity = (parseFloat(data.Bag_Size_In_kg.toFixed(2)) * parseFloat(data.Recv_No_Of_Bags.toFixed(2))) / 100;
        const FIN_YR = `select "FIN_YR" from "mFINYR" where "IS_ACTIVE"=1`;
        const response_FIN_YR = await client.query(FIN_YR);
        const SEASSION = `select "SHORT_NAME" from "mSEASSION" where "IS_ACTIVE"=1`;
        const response_SEASSION = await client.query(SEASSION);
        if (data.Class == 'Certified-II') {
            data.Class = 'Certified'
        }
        const insertintoapi01 = `INSERT INTO public."API_01"(
            "DIST_CODE", "GODOWN_ID", "Challan_No", "Recv_Date", "FARMER_ID", "SEASSION", "FIN_YR", "USERID", "USERIP",
             "CropCatg_ID", "CROP_ID", "Crop_Verid", "Class", "LOT_NO", "Bag_Size_In_kg", "Recv_No_Of_Bags", "Recv_Quantity", "Testing_Date", "APIKEY", "UPDATED_ON")
         values ($1, $2, $3, $4, $5, $6, $7, $8, $9, $10,$11, $12, $13, $14, $15, $16, $17, $18, $19,$20)`;
        const insertintoapi01values = [data.Dist_Code, data.Godown_ID, data.Challan_No, data.Recv_Date, data.FARMER_ID, response_SEASSION.rows[0].SHORT_NAME, response_FIN_YR.rows[0].FIN_YR, data.UserID, data.UserIP, data.CropCatg_ID, data.Crop_ID, data.Crop_Verid, data.Class, data.Lot_No, data.Bag_Size_In_kg, data.Recv_No_Of_Bags, Recv_Quantity, data.Testing_Date, data.APIKEY, 'now()'];
        await client.query(insertintoapi01, insertintoapi01values);

        let AgenciesID = '01';
        let Receive_Unitcd = '0123';
        let User_Type = 'OSSC';
        let CNT = 0

        const DIST_CODE = `SELECT "Dist_Code" FROM "Stock_District" WHERE "Dist_Code" = $1 OR "LGDistrict" = $1::integer`;
        const DIST_CODE_values = [data.Dist_Code]
        console.log(DIST_CODE, DIST_CODE_values, typeof (data.Dist_Code));
        console.log('iiii');
        let response_DIST_CODE = await client.query(DIST_CODE, DIST_CODE_values);
        console.log(response_DIST_CODE.rows[0].Dist_Code);


        const DIST_NAME = `SELECT SUBSTRING("Dist_Name" FROM 1 FOR 4) AS "Dist_Name" FROM "Stock_District" WHERE "Dist_Code"=$1`;
        const DIST_NAME_values = [response_DIST_CODE.rows[0].Dist_Code];
        const response_DIST_NAME = await client.query(DIST_NAME, DIST_NAME_values);
        console.log(response_DIST_NAME.rows[0].Dist_Name);
        const MAXTRAN_NO_Calculate = `SELECT "RECVTRANSID",CAST( LEFT( SUBSTRING("RECVTRANSID" FROM 16 FOR LENGTH("RECVTRANSID")),POSITION('-' IN SUBSTRING("RECVTRANSID" FROM 16 FOR LENGTH("RECVTRANSID"))) - 1) AS INTEGER) + 1  as "Maxno" FROM "Stock_ReceiveDetails" WHERE SUBSTRING("RECVTRANSID" FROM 8 FOR 7)= $2  AND SUBSTRING("RECVTRANSID" FROM 3 FOR 4) = $1 ORDER BY "Maxno" DESC limit 1;`;
        const MAXTRAN_NO_CalculateValues = [response_DIST_NAME.rows[0].Dist_Name, response_FIN_YR.rows[0].FIN_YR]
        const response_MAXTRAN_NO_Calculate = await client.query(MAXTRAN_NO_Calculate, MAXTRAN_NO_CalculateValues);
        if (response_MAXTRAN_NO_Calculate.rows.length == 0) {
            MAXTRAN_NO = 1
        }
        else {
            MAXTRAN_NO = response_MAXTRAN_NO_Calculate.rows[0].Maxno
        }
        RECVTRANSID = 'R/' + response_DIST_NAME.rows[0].Dist_Name + '/' + response_FIN_YR.rows[0].FIN_YR + '/' + MAXTRAN_NO;
        console.log(RECVTRANSID);

        const originalDate = new Date(data.Testing_Date);
        originalDate.setUTCMonth(originalDate.getUTCMonth() + 9);
        const Expiry_Date = originalDate.toISOString();
        console.log(Expiry_Date);
        CNT += 1
        const insertintoStock_ReceiveDetails = `INSERT INTO public."Stock_ReceiveDetails"("RECVTRANSID", "Dist_Code", "Godown_ID", "AgenciesID", "Receive_Unitcd","Challan_No", "CropCatg_ID","Crop_ID", "Crop_Verid", "Class","Lot_No", "Bag_Size_In_kg", "Recv_No_Of_Bags", "Recv_Date","Recv_Quantity","SEASSION_NAME", "FIN_YR", "User_Type", "EntryDate", "UserID","UserIP", "TESTING_DATE", "EXPIRY_DATE","FARMER_ID","STATUS")
        values ($1, $2, $3, $4, $5, $6, $7, $8, $9, $10,$11, $12, $13, $14, $15, $16, $17, $18, $19,$20,$21,$22,$23,$24,$25)`;
        const insertintoStock_ReceiveDetailsvalues = [RECVTRANSID + '-' + CNT, response_DIST_CODE.rows[0].Dist_Code, data.Godown_ID, AgenciesID, Receive_Unitcd, data.Challan_No, data.CropCatg_ID, data.Crop_ID, data.Crop_Verid, data.Class, data.Lot_No, data.Bag_Size_In_kg, data.Recv_No_Of_Bags, data.Recv_Date, Recv_Quantity, response_SEASSION.rows[0].SHORT_NAME, response_FIN_YR.rows[0].FIN_YR, 'OSSC', 'now()', data.UserID, data.UserIP, data.Testing_Date, Expiry_Date, data.FARMER_ID, 0];
        await client.query(insertintoStock_ReceiveDetails, insertintoStock_ReceiveDetailsvalues);
        console.log('insertintoStock_ReceiveDetails');
        const checkStock_StockDetails = `SELECT * FROM "Stock_StockDetails" WHERE "Dist_Code" = $1 AND "Godown_ID" = $2 AND "Crop_Verid" = $3 AND "Receive_Unitcd" = $4 AND "Lot_No" = $5 AND "FIN_YR" = $6 AND "SEASSION_NAME" = $7 AND "User_Type" = 'OSSC';`;
        const checkStock_StockDetails_Values = [response_DIST_CODE.rows[0].Dist_Code, data.Godown_ID, data.Crop_Verid, Receive_Unitcd, data.Lot_No, response_FIN_YR.rows[0].FIN_YR, response_SEASSION.rows[0].SHORT_NAME]
        const response_checkStock_StockDetails = await client.query(checkStock_StockDetails, checkStock_StockDetails_Values);
        console.log(response_checkStock_StockDetails.rows.length);
        if (response_checkStock_StockDetails.rows.length == 0) {
            console.log('0');
            const stock_id = `SELECT COALESCE(MAX("Stock_ID"), 0) + 1 as "Stock_ID"  FROM "Stock_StockDetails";`;
            const stock_id_Values = []
            const response_stock_id = await client.query(stock_id, stock_id_Values);
            console.log(response_stock_id.rows[0].Stock_ID);
            const insertintoStock_ReceiveDetails1 = `INSERT INTO public."Stock_StockDetails"( "Stock_ID", "Dist_Code", "Godown_ID", "CropCatg_ID", "Crop_ID","Crop_Verid", "Class", "Receive_Unitcd","Lot_No", "Bag_Size_In_kg","Recv_No_Of_Bags", "AVL_NO_OF_BAGS", "Stock_Date", "Stock_Quantity", "Avl_Quantity","SEASSION_NAME", "FIN_YR", "User_Type", "EntryDate", "UserID", "UserIP", "Entry_Status", "TESTING_DATE", "EXPIRY_DATE","VALIDITY")
                    values ($1, $2, $3, $4, $5, $6, $7, $8, $9, $10,$11, $12, $13, $14, $15, $16, $17, $18, $19,$20,$21,$22,$23,$24,$25)`;
            const insertintoStock_ReceiveDetailsvalues1 = [
                response_stock_id.rows[0].Stock_ID, response_DIST_CODE.rows[0].Dist_Code, data.Godown_ID, data.CropCatg_ID, data.Crop_ID,
                data.Crop_Verid, data.Class, Receive_Unitcd, data.Lot_No, data.Bag_Size_In_kg,
                data.Recv_No_Of_Bags, data.Recv_No_Of_Bags, data.Recv_Date, Recv_Quantity, Recv_Quantity,
                response_SEASSION.rows[0].SHORT_NAME, response_FIN_YR.rows[0].FIN_YR, 'OSSC', 'now()', data.UserID, data.UserIP,
                'R', data.Testing_Date, Expiry_Date, 'true'];
            await client.query(insertintoStock_ReceiveDetails1, insertintoStock_ReceiveDetailsvalues1);
            console.log('insertintoStock_ReceiveDetails1');
            resolve(true);
        }
        // else {
        //     console.log('1');
        //     const Stock_StockDetails_data = `SELECT "Recv_No_Of_Bags",  "AVL_NO_OF_BAGS", "Stock_Quantity",  "Avl_Quantity" FROM   "Stock_StockDetails" WHERE  "Dist_Code" = $1 AND "Godown_ID" = $2 AND "Receive_Unitcd" = $4  AND "Crop_Verid" = $3 AND "Lot_No" = $5 AND "FIN_YR" = $6 AND "User_Type" = 'OSSC';`;
        //     const Stock_StockDetails_data_Values = [response_DIST_CODE.rows[0].Dist_Code, data.Godown_ID, data.Crop_Verid, Receive_Unitcd, data.Lot_No, response_FIN_YR.rows[0].FIN_YR]
        //     const response_Stock_StockDetails_data = await client.query(Stock_StockDetails_data, Stock_StockDetails_data_Values);

        //     console.log(response_Stock_StockDetails_data.rows[0]);
        //     const UPDATE_Stock_StockDetails4 = `UPDATE "Stock_StockDetails" SET "Recv_No_Of_Bags" = CAST($12 AS INTEGER) +CAST($1 AS INTEGER), "AVL_NO_OF_BAGS" = CAST($12 AS INTEGER) +CAST($2 AS INTEGER), "Stock_Quantity" = CAST($13 AS DOUBLE PRECISION) + CAST($3 AS DOUBLE PRECISION), "Avl_Quantity" = CAST($13 AS DOUBLE PRECISION) + CAST($4 AS DOUBLE PRECISION) WHERE "Dist_Code" = $5 AND "Godown_ID" = $6 AND "Receive_Unitcd" = $7 AND "Crop_Verid" = $8  AND "Lot_No" = $9 AND "FIN_YR" = $10 AND "SEASSION_NAME" = $11 AND "User_Type" = 'OSSC';`;
        //     const UPDATE_Stock_StockDetails4_values = [response_Stock_StockDetails_data.rows[0].Recv_No_Of_Bags, response_Stock_StockDetails_data.rows[0].AVL_NO_OF_BAGS, response_Stock_StockDetails_data.rows[0].Stock_Quantity, response_Stock_StockDetails_data.rows[0].Avl_Quantity, response_DIST_CODE.rows[0].Dist_Code, data.Godown_ID, Receive_Unitcd, data.Crop_Verid, data.Lot_No, response_FIN_YR.rows[0].FIN_YR, response_SEASSION.rows[0].SHORT_NAME, data.Recv_No_Of_Bags, Recv_Quantity];

        //     console.log(UPDATE_Stock_StockDetails4_values);
        //     const response_UPDATE_Stock_StockDetails4 = await client.query(UPDATE_Stock_StockDetails4, UPDATE_Stock_StockDetails4_values);
        //     resolve(true);
        // }


    } catch (e) {
        console.log(new Error(`Oops! An error occurred: ${e}`));
        const UPDATE_API_08 = `update "API_08" set "Error"=1 where "API_08ID" in( select "API_08ID" from "API_08" where "CASH_MEMO_NO"=$1 order by "API_08ID" desc limit 1)`;
        const UPDATE_API_08_values = [data.CASH_MEMO_NO];
        const response_UPDATE_API_08 = await client.query(UPDATE_API_08, UPDATE_API_08_values);
        resolve(false);
    } finally {
        client.release();
    }
});
exports.updatelicno = () => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const result = await sequelizeSeed.query(`update  [dafpseed].[dbo].[SEED_LIC_COMP_DIST] set subsidyModeToERUPI =1  where SEED_LIC_DIST_ID in (select SEED_LIC_DIST_ID from  [dafpseed].[dbo].[SEED_LIC_DIST] where lic_no1 in (
   'e-Lic/SEED/2020D-1191','e-Lic/SEED/2020D-911','e-Lic/SEED/2022D-11255','e-Lic/SEED/2023D-12753',
   'e-Lic/SEED/2023D-13317','e-Lic/SEED/2020D-7352','e-Lic/SEED/2020D-6275','e-Lic/SEED/2020D-1641',
   'e-Lic/SEED/2020D-8527','e-Lic/SEED/2021D-9292','e-Lic/SEED/2024D-13848','e-Lic/SEED/2020D-3658',
   'e-Lic/SEED/2020D-3744','e-Lic/SEED/2020D-8266','e-Lic/SEED/2022D-10386','e-Lic/SEED/2022D-10444',
   'e-Lic/SEED/2020D-6874','e-Lic/SEED/2020D-6965','e-Lic/SEED/2020D-4033','e-Lic/SEED/2020D-6968',
   'e-Lic/SEED/2020D-7024','e-Lic/SEED/2020D-2473','e-Lic/SEED/2022D-11525','e-Lic/SEED/2020D-2498',
   'e-Lic/SEED/2020D-6936','e-Lic/SEED/2020D-8648','e-Lic/SEED/2020D-8369','e-Lic/SEED/2020D-1124',
   'e-Lic/SEED/2022D-11574','e-Lic/SEED/2020D-6015','e-Lic/SEED/2020D-4031','e-Lic/SEED/2020D-6013',
   'e-Lic/SEED/2020D-3395')) and COMP_TYPE=1`, {
            replacements: {}, type: sequelizeStock.QueryTypes.SELECT
        });
        resolve(result);
    } catch (e) {
        console.log('An error occurred...', e);

        resolve([]);
        throw e;
    } finally {
        client.release();
    }
});
exports.updatedlicno = () => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const result = await sequelizeSeed.query(`SELECT distinct APP_FIRMNAME,LIC_NO1,APPEMAIL_ID,LIC_NO,a.DIST_CODE,a.APPMOB_NO,e.LGDistrict,subsidyModeToERUPI,f.block_name FROM [dafpseed].[dbo].[SEED_LIC_DIST] A 
    INNER JOIN [dafpseed].[dbo].[SEED_LIC_APP_DIST] B ON A.SEED_LIC_DIST_ID = B.SEED_LIC_DIST_ID 
    INNER JOIN [dafpseed].[dbo].[SEED_LIC_COMP_DIST] C ON A.SEED_LIC_DIST_ID = C.SEED_LIC_DIST_ID 
    inner join [dafpseed].[dbo].[dist] e on e.dist_code= b.APPDIST_ID 
	inner join [dafpseed].[dbo].block f on f.block_code= b.APPBLOCK_ID
    WHERE   CONVERT(DATE, DATEADD(MONTH,1,A.APR_UPTO),103) >= CONVERT(DATE, GETDATE(), 103) AND A.LIC_ACTIVE = 1 AND A.IS_ACTIVE = 1 AND A.APP_STATUS = 'A' AND C.COMP_TYPE = 1 and subsidyModeToERUPI=1 order by f.block_name `, {
            replacements: {}, type: sequelizeStock.QueryTypes.SELECT
        });
        resolve(result);
    } catch (e) {
        console.log('An error occurred...', e);

        resolve([]);
        throw e;
    } finally {
        client.release();
    }
});
exports.getTransactionDetails = (txnid) => new Promise(async (resolve, reject) => {
    let txnid1 = txnid + '-%'
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const result = await sequelizeSeed.query(`select TRANSACTION_ID,LOT_NUMBER,NO_OF_BAGS,XML_Status from [dbo].[STOCK_FARMER_2021-22_R] where  XML_Status is null and "TRANSACTION_ID" like :txnid order by UPDATED_ON`, {
            replacements: { txnid: txnid1 }, type: sequelizeStock.QueryTypes.SELECT
        });

        const query = `select "TRANSACTION_ID","LOT_NUMBER","NO_OF_BAGS","XML_Status","SUBSIDY_AMOUNT", "UPDATED_ON" from "STOCK_FARMER" where  "XML_Status" is null and "TRANSACTION_ID" like  $1`;
        const value = [txnid1];
        let response = await client.query(query, value);
        const query1 = `select "TRANSACTION_ID","LOT_NUMBER","NO_OF_BAGS","SUBSIDY_AMOUNT" from "STOCK_DEALERSALEDTL" where "TRANSACTION_ID" like  $1`;
        const value1 = [txnid];
        let response1 = await client.query(query1, value1);
        const query2 = `select "TRANSACTION_ID","TOT_SUB_AMOUNT_GOI","TOT_SUB_AMOUNT_SP", "UPDATED_ON" from "STOCK_DEALERSALEHDR" where "TRANSACTION_ID" like  $1`;
        const value2 = [txnid];
        let response2 = await client.query(query2, value2);
        const query3 = `select a."FARMER_ID",a."BAG_SIZE_KG",a."NO_OF_BAGS" from "STOCK_FARMERSTOCK" a inner join "STOCK_FARMER" b on a."FARMER_ID"= b."FARMER_ID" where b."TRANSACTION_ID" like  $1`;
        const value3 = [txnid1];
        let response3 = await client.query(query3, value3);

        resolve({ sqldata: result, STOCK_FARMER: response.rows, STOCK_DEALERSALEDTL: response1.rows, STOCK_DEALERSALEHDR: response2.rows, STOCK_FARMERSTOCK: response3.rows[0] });
    } catch (e) {
        console.log('An error occurred...', e);

        resolve([]);
        throw e;
    } finally {
        client.release();
    }
});
exports.deleteTransactionDetails = (txnid) => new Promise(async (resolve, reject) => {
    let txnid1 = txnid + '-%'
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `SELECT "DTL_TRANSACTION_ID","LOT_NUMBER","BAG_SIZE_KG","NO_OF_BAGS","TOT_QTL" FROM "STOCK_DEALERSALEDTL" WHERE "TRANSACTION_ID" like  $1`;
        const value = [txnid];
        let response = await client.query(query, value);
        const query1 = `SELECT  "FARMER_ID","LIC_NO","SEASON","FIN_YEAR" FROM "STOCK_DEALERSALEHDR" WHERE "TRANSACTION_ID"  like  $1`;
        const value1 = [txnid];
        let response1 = await client.query(query1, value1);
        const insertintoAUDITLOG_FarmerTransactiondelete = ` INSERT INTO AUDITLOG_FarmerTransactiondelete (FARMER_ID, TRANSACTION_ID, CROPCATG_ID, CROP_ID, CROP_VERID, CROP_CLASS, Receive_Unitcd, LOT_NUMBER, BAG_SIZE_KG, NO_OF_BAGS, TOT_QTL, ADMISSIBLE_SUBSIDY, PRICE_QTL, ALL_IN_COST_AMOUNT, SCHEME_CODE_GOI, TOT_SUB_AMOUNT_GOI, SCHEME_CODE_SP, TOT_SUB_AMOUNT_SP, SUBSIDY_AMOUNT, SEASON, FIN_YEAR, UPDATED_BY, UPDATED_ON, USER_TYPE, USERIP, TRN_TYPE, CANCEL_DATE, CANCEL_BY, CANCEL_IP) 
        SELECT   A."FARMER_ID", A."TRANSACTION_ID", B."CROPCATG_ID", B."CROP_ID", B."CROP_VERID", B."CROP_CLASS", B."Receive_Unitcd", B."LOT_NUMBER", B."BAG_SIZE_KG", B."NO_OF_BAGS", B."TOT_QTL", B."ADMISSIBLE_SUBSIDY", B."PRICE_QTL", B."ALL_IN_COST_AMOUNT", B."SCHEME_CODE_GOI", B."TOT_SUB_AMOUNT_GOI", B."SCHEME_CODE_SP", B."TOT_SUB_AMOUNT_SP",  B."SUBSIDY_AMOUNT", A."SEASON", A."FIN_YEAR", A."UPDATED_BY", A."UPDATED_ON",  A."USER_TYPE", A."USERIP", A."TRN_TYPE", CURRENT_TIMESTAMP, 'admin', '10.172.0.77' FROM  "STOCK_DEALERSALEHDR" A  INNER JOIN "STOCK_DEALERSALEDTL" B  ON A."TRANSACTION_ID" = B."TRANSACTION_ID"   WHERE  A."TRANSACTION_ID"=$1`;
        const insertintoAUDITLOG_FarmerTransactiondeletevalues = [txnid];
        await client.query(insertintoAUDITLOG_FarmerTransactiondelete, insertintoAUDITLOG_FarmerTransactiondeletevalues);
        for (let index = 0; index < response.rows.length; index++) {

            let updateinSTOCK_DEALERSTOCK = await client.query(`UPDATE "STOCK_DEALERSTOCK" SET "AVL_NO_OF_BAGS" = "AVL_NO_OF_BAGS" + ${response.rows[index].NO_OF_BAGS},"AVL_QUANTITY" = "AVL_QUANTITY" +${response.rows[index].TOT_QTL} WHERE "LICENCE_NO" = '${response1.rows[0].LIC_NO}' AND "LOT_NO" =  '${response.rows[index].LOT_NUMBER}'`);
            let updateinSTOCK_FARMERSTOCK = await client.query(`UPDATE "STOCK_FARMERSTOCK" SET "NO_OF_BAGS" = "NO_OF_BAGS" - ${response.rows[index].NO_OF_BAGS},"TOT_QTL" = "TOT_QTL" - ${response.rows[index].TOT_QTL} WHERE "FARMER_ID" = '${response1.rows[0].FARMER_ID}' AND "FIN_YEAR"='${response1.rows[0].FIN_YEAR}' AND "SEASON"='${response1.rows[0].SEASON}'`);

            let deleteSTOCK_DEALERSALEDTL = await client.query(`DELETE FROM "STOCK_DEALERSALEDTL" WHERE "DTL_TRANSACTION_ID" = '${response.rows[index].DTL_TRANSACTION_ID}'`);
            let deleteSTOCK_FARMER = await client.query(`DELETE FROM "STOCK_FARMER" WHERE "TRANSACTION_ID" ='${response.rows[index].DTL_TRANSACTION_ID}'`);

        }
        let deleteSTOCK_DEALERSALEDTL = await client.query(`DELETE FROM "STOCK_DEALERSALEHDR" WHERE "TRANSACTION_ID" = '${txnid}'`);

        const result = await sequelizeSeed.query(`DELETE from [dbo].[STOCK_FARMER_2021-22_R] where  XML_Status is null and "TRANSACTION_ID" like :txnid `, {
            replacements: { txnid: txnid1 }, type: sequelizeStock.QueryTypes.SELECT
        });
        resolve(true);
    } catch (e) {
        console.log('An error occurred...', e);

        resolve([]);
        throw e;
    } finally {
        client.release();
    }
});






exports.getcrop = () => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `SELECT "Crop_Code","Crop_Name" FROM "mCrop" WHERE  "IS_ACTIVE" = '1' ORDER BY "Crop_Name" ASC`;
        const values = [];
        const response = await client.query(query, values);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});


exports.getvariety = (CropID) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query1 = `select "Variety_Code","Variety_Name" from "mCropVariety" where "IS_ACTIVE"=1 and "Crop_Code"=$1 order by "Variety_Name"`;
        const values1 = [CropID];
        const response1 = await client.query(query1, values1);
        resolve(response1.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.getVarietySearch = (data) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `SELECT "LICENCE_NO",SUM("AVL_QUANTITY") AS QTY FROM "STOCK_DEALERSTOCK"        
                WHERE  RIGHT(LEFT("LICENCE_NO",5),3)=left($1,3)         
AND "CROP_ID"=$2 AND "CROP_VERID"=$3     
AND ('2024-25' is null or "FIN_YR"='2024-25' )AND ('K' is null or "SEASSION"='K')      
GROUP BY "LICENCE_NO"  having SUM("AVL_QUANTITY") >0   order by SUM("AVL_QUANTITY") desc`;
        const values = [data.selectedDistrict,data.selectedCrop,data.selectedVariety];
        const response = await client.query(query, values);
        if(response.rows.length >0){
            const licNumbers = response.rows.map(item => item.LICENCE_NO);
            let result = await sequelizeSeed.query(`
                SELECT UPPER(APPNAME)APPNAME,LineItems2.APPMOB_NO,left(LineItems2.APPMOB_NO,2)+'****'+RIGHT(LineItems2.APPMOB_NO,4) maskingmobilenumber, LIC_NO,APP_FIRMNAME,LineItems2.APPADDRESS FROM [dafpSeed].[dbo].[SEED_LIC_DIST] AS SLD        
    CROSS APPLY                                    
    (                                    
    SELECT  TOP 1 SEED_LIC_APP_DIST_ID,APPMOB_NO,APPNAME,APPADDRESS                                    
    FROM    [dafpSeed].[dbo].[SEED_LIC_APP_DIST]                                     
    WHERE   SEED_LIC_APP_DIST.SEED_LIC_DIST_ID = SLD.SEED_LIC_DIST_ID                                    
    ) LineItems2   
    where lic_no in (:licNumbers)
                `, {
                replacements: { licNumbers: licNumbers }, type: sequelizeStock.QueryTypes.SELECT
            });
            result = result.filter(itemA => {
                const matchB = response.rows.find(itemB => itemB.LICENCE_NO === itemA.LIC_NO);
                if (matchB) {
                    itemA.avlqty = matchB.qty;
                    return true;
                }
                return false;
            }).sort((a, b) => {
                if (a.APPNAME < b.APPNAME) return -1;
                if (a.APPNAME > b.APPNAME) return 1;
                return 0;
            });
            // console.log(result);
            resolve(result);
        }
        else{
            resolve(response.rows);
        }
      

    } catch (e) {
        console.log('An error occurred...', e);

        resolve([]);
        throw e;
    } finally {
        client.release();
    }
});
exports.districtWisecropList = (selectedDistrict) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `select distinct "Crop_ID",b."Crop_Name",sum("Avl_Quantity") as "avlQtyInQTL" from public."Stock_StockDetails" a
        inner join "mCrop" b on a."Crop_ID" = b."Crop_Code"
		inner join "Stock_District" c on c."Dist_Code" = a."Dist_Code"
        where "FIN_YR"=(select "FIN_YR" from public."mFINYR" where "IS_ACTIVE"=1) 
        and "SEASSION_NAME"=(select "SHORT_NAME" from public."mSEASSION" where "IS_ACTIVE"=1) and (c."LGDistrict"=$1 or $1=0)
        group by "Crop_ID",b."Crop_Name" order by "Crop_Name"`;
        const values = [selectedDistrict];        
        const response = await client.query(query, values);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});