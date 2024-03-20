const pool = require('../config/dbConfig');
var dbConfig = require('../config/dbSqlConnection');
var sqlstock = dbConfig.sqlstock;
var sequelizeSeed = dbConfig.sequelizeSeed;
var sequelizeStock = dbConfig.sequelizeStock;
const format = require('pg-format');

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
exports.successPrebookingDtl = (date) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const year = date.substring(date.length - 4);
        const Month = date.substring(date.length - 5, 3)
        const Date = date.substring(date.length - 10, 2)
        const entrydate = year + '-' + Month + '-' + Date + ' 00:00:00.000'
        const lastdate = year + '-' + Month + '-' + Date + ' 23:59:59.000'
        const query = `SELECT a."applicationID",b."UPDATED_ON" "date",b."UPDATED_BY" "delerid",a."TRANSACTION_ID" as "txnid" from "prebookinglist" a
        inner join "STOCK_FARMER" b on a."applicationID"= b."PREBOOKING_APPLICATIONID" and "UPDATED_ON" between $1 and $2 order by "UPDATED_ON"`;
        const values = [entrydate, lastdate];
        const response = await client.query(query, values);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.seedavailablity = (DIST_CODE, CROP_CODE, VARIETY_CODE) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `SELECT  a."LICENCE_NO" as "LICENCE_NUMBER",k."LGDistrict" "DISTRICT", D."Category_Code" "CATEGORY", E."Crop_Code" "CROP", F."Variety_Code" "VARIETY", A."BAG_SIZE_IN_KG" "BAG_SIZE", A."AVL_NO_OF_BAGS" "AVAILABLE", A."LOT_NO" "LOT_NUMBER", A."EXPIRY_DATE" "EXPIRY_DATE", A."RECEIVE_UNITCD", I."PRICE_RECEIVE_UNITCD",CASE WHEN J."Receive_Unitname"='GOVT' THEN 'OSSC' ELSE J."Receive_Unitname" END AS "SOURCE" FROM "STOCK_DEALERSTOCK" A 
        inner join "Stock_District" k on (SUBSTRING(a."LICENCE_NO",3,3)=SUBSTRING(k."Dist_Name",1,3)) 
        LEFT JOIN "mCropCategory" D ON A."CROPCATG_ID" = D."Category_Code"       
        LEFT JOIN "prebooking_mCrop" E ON A."CROP_ID" = E."Crop_Code"       
        LEFT JOIN "prebooking_mCropVariety" F ON A."CROP_VERID" = F."Variety_Code"       
        LEFT JOIN "Price_SourceMapping" I ON A."RECEIVE_UNITCD" = I."RECEIVE_UNITCD" AND I."FIN_YR" = '2023-24' AND I."SEASSION" = 'R'
        INNER JOIN "Stock_Receive_Unit_Master" J ON I."PRICE_RECEIVE_UNITCD" = J."Receive_Unitcd" WHERE "AVL_NO_OF_BAGS">0 AND "EXPIRY_DATE" >= now()  AND
        ('${DIST_CODE}' IS NULL OR '${DIST_CODE}' = '' OR K."LGDistrict" = '${DIST_CODE}') AND ('${CROP_CODE}' IS NULL OR '${CROP_CODE}' = '' OR E."Crop_Code" = '${CROP_CODE}') AND ( '${VARIETY_CODE}' IS NULL OR '${VARIETY_CODE}' = '' OR F."Variety_Code" = '${VARIETY_CODE}')
        and "LICENCE_NO"='ODPUR1/2015-16/0009'  ORDER BY K."Dist_Name" `;
        // DIST_CODE, CROP_CODE, VARIETY_CODE
        const response = await client.query(query);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.getDealerData = (LICENCE_NUMBER) => new Promise(async (resolve, reject) => {
    try {
       
        const totalcollectorder = await sequelizeStock.query(`select A.LIC_NO1 'LICENCE_NUMBER', CASE WHEN a.APP_TYPE='Secretary PACS' THEN 'PACS' ELSE 'DEALER' END 'DEALER_TYPE', a.APPMOB_NO 'CONTACT_NUMBER', a.APP_FIRMNAME 'FARM_NAME',c.LGBLOCK_CODE 'BLOCK', b.ADDRESS 'ADDRESS' from  [DAFPSEED].[DBO].[SEED_LIC_DIST] a
        INNER JOIN [DAFPSEED].[DBO].[SEED_LIC_BUS_DIST] b ON a.SEED_LIC_DIST_ID = b.SEED_LIC_DIST_ID AND b.IS_ACTIVE=1   
        LEFT JOIN  [DAFPSEED].[DBO].[block] c on b.BLOCK_ID = c.block_code  
        where LIC_NO=:LICENCE_NUMBER`, {
            replacements: {LICENCE_NUMBER:LICENCE_NUMBER}, type: sequelizeStock.QueryTypes.SELECT
        });
        resolve(totalcollectorder);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally { // Close the connection after the promise is resolved or rejected
    }
});

