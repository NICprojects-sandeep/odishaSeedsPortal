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
        const abc = data.selectedFromDate.split("-");
        const abc1 = data.selectedToDate.split("-");
        const fromdate = abc[1] + '/' + abc[2] + '/' + abc[0];
        const todate = +abc1[1] + '/' + abc1[2] + '/' + abc1[0];
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




////////////////////////////////postgress//////////////////////////////////

const format = require('pg-format');
const pool = require('../config/dbConfig');

var dbConfigsql = require('../config/dbSqlConnection');
var sql_stock = dbConfigsql.sqlstock;
var sequelize_Seed = dbConfigsql.sequelizeSeed;
var locConfig_stock = dbConfigsql.locConfigStock;
var locConfig_dafpSeeds = dbConfigsql.locConfigdafpSeeds;

exports.GETDISTCODEFROMLICNO = (LicNo) => new Promise(async (resolve, reject) => {
    var con = new sqlstock.ConnectionPool(locConfig_stock);
    try {
        con.connect().then(function success() {
            const request = new sqlstock.Request(con);
            request.input('LIC_NO', LicNo);
            request.execute('SP_GETDISTCODEFROMLICNO', function (err, result) {
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
    } catch (e) {
        console.log(`Oops! An error occurred: ${e}`);
    }
});
exports.getStockReceivedData = (data) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query1 = `SELECT B."Crop_Name","Variety_Name",SUM("TOT_QTL") "TOT_QTL" FROM "STOCK_FARMER" A 
      LEFT OUTER JOIN public."mCrop" B ON A."CROP_ID" = B."Crop_Code"
      inner join public."mCropVariety" c on a."CROP_VERID" = c."Variety_Code"
      where "FIN_YEAR"=$1 and "SEASON"=$2 and b."IS_ACTIVE"=1 and c."IS_ACTIVE"=1 and "FARMER_ID"=$3
      GROUP BY B."Crop_Name",c."Variety_Name"`;
        const values1 = [data.FIN_YR, data.SEASSION, data.FarmerId];
        const response = await client.query(query1, values1);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.getPreBookingDetails = (data) => new Promise(async (resolve, reject) => {
    if (data.SEASSION = 'K') {
        data.SEASSION = 'Kharif'
    }
    else {
        data.SEASSION = 'Rabi'
    }
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query1 = `select "beneficiaryId",b."Crop_Code",b."Crop_Name",c."Variety_Code",c."Variety_Name",a."preBookingAmt","bagSize",
      "noOfBag","totalCost",sum(CAST (A.quantity AS DOUBLE PRECISION))/100 as "QUANTITY","applicationID"
      from public.prebookinglist a
      inner join public."mCrop" b on a."cropCode" =b."Crop_Code" 
      inner join public."mCropVariety" c on a."varietyCode" = c."Variety_Code"
      where  fin_year=$1 and "Season"=$2 and b."IS_ACTIVE"=1 and c."IS_ACTIVE"=1 and a."IS_ACTIVE"=1 and
      "beneficiaryId"=$3 and "TRANSACTION_ID" is null and cancelstatus is null 
      group by "beneficiaryId",b."Crop_Code",b."Crop_Name",c."Variety_Code",c."Variety_Name",a."preBookingAmt","noOfBag","totalCost"
      ,"bagSize","applicationID"`;
        const values1 = [data.FIN_YR, data.SEASSION, data.FarmerId];
        const response = await client.query(query1, values1);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});

exports.createOtp = (data) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `UPDATE "Transaction_OTP" SET "IS_ACTIVE"=false WHERE "FARMER_ID"=$1 ;`;
        const values = [data.FarmerId];
        const response = await client.query(query, values);

        const query1 = `INSERT INTO "Transaction_OTP" ("FARMER_ID", "VCHMOBNO", "OTP_NO", "CREATED_DATE", "EXPIRED_DATE","UPDATED_IP") 
        VALUES ($1, $2, $3, $4, now() + interval '10 minutes', $5)`;
        const values1 = [data.FarmerId, data.MobileNo, data.otp, 'now()', '10.172.0.78'];
        const response1 = await client.query(query1, values1);
        resolve(response1.rows);


    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }


});
exports.ValidateOTP = (data) => new Promise(async (resolve, reject) => {
    // var con = new sqlstock.ConnectionPool(locConfigstock);
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const sendresult = (value) => {
            resolve(value)
        }
        const query = `SELECT * FROM "Transaction_OTP" WHERE  "FARMER_ID" =$1 AND "OTP_NO" = $2 AND "IS_ACTIVE" = true and EXTRACT(MINUTE FROM AGE(NOW(), "CREATED_DATE")) < 10;`
        const values = [data.FarmerId, data.enteredOtp];
        const response = await client.query(query, values);
        const FIN_YR = await client.query(`select "FIN_YR" from public."mFINYR" where "IS_ACTIVE"=1`);
        const SEASON = await client.query(`select "SHORT_NAME" from public."mSEASSION" where "IS_ACTIVE"=1`);
        if (response.rows.length > 0) {
            const query1 = `SELECT "VCHMOBNO" from "Transaction_OTP"  WHERE "FARMER_ID"=$1 AND "OTP_NO" = $2 AND "IS_ACTIVE" = true;`
            const values1 = [data.FarmerId, data.enteredOtp];
            const response1 = await client.query(query1, values1);

            const query2 = `UPDATE "Transaction_OTP" SET "IS_ACTIVE"=false,"OTP_RESPONSE"='U'  WHERE "FARMER_ID"=$1 AND "OTP_NO" = $2 AND "IS_ACTIVE" = true;`
            const values2 = [data.FarmerId, data.enteredOtp];
            const response2 = await client.query(query2, values2);

            const query3 = `SELECT * FROM "TOTPFARMERSALE" WHERE "FARMER_ID" = $1 AND "VCHMOBNO" = $2`
            const values3 = [data.FarmerId, response1.rows[0].VCHMOBNO];
            const response3 = await client.query(query3, values3);
            if (response3.rows.length > 0) {
                const query2 = `UPDATE "TOTPFARMERSALE" SET "CNT" = "CNT"+1,"OTP_RESPONSE"='U'  WHERE "FARMER_ID"=$1 AND "OTP_NO" = $2 AND "IS_ACTIVE" = true;`
                const values2 = [data.FarmerId, data.enteredOtp];
                const response2 = await client.query(query2, values2);
                sendresult(true);
            }
            else {
                const query1 = `INSERT INTO "TOTPFARMERSALE" ("FARMER_ID", "VCHMOBNO", "UPDATED_ON", "FIN_YEAR", "SEASON", "CNT","UPDATED_IP") 
                VALUES ($1, $2, $3, $4,$5,$6,$7)`;
                const values1 = [data.FarmerId, data.MobileNo, 'now()', FIN_YR.rows[0].FIN_YR, SEASON.rows[0].SHORT_NAME, 1, '10.172.0.78'];
                const response1 = await client.query(query1, values1);
                sendresult(true);
            }

        }
        else {
            sendresult(false);
        }

    } catch (e) {
        console.log(`Oops! An error occurred: ${e}`);
    }
});
exports.FillCrops = (data) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query1 = `SELECT C."Crop_Code" AS "CROP_CODE",C."Crop_Name" AS "CROP_NAME" FROM "STOCK_DEALERSTOCK" A 
        LEFT OUTER JOIN public."mCropCategory"  B ON A."CROPCATG_ID" = B."Category_Code"
        LEFT OUTER JOIN public."mCrop" C ON A."CROP_ID" = C."Crop_Code"
        WHERE A."LICENCE_NO" = $3 AND A."FIN_YR" = $1 AND A."SEASSION" = $2 AND C."IS_ACTIVE" = 1 
        AND A."VALIDITY" = 1  
        GROUP BY C."Crop_Code",C."Crop_Name" ORDER BY C."Crop_Code",C."Crop_Name"`;
        const values1 = [data.FIN_YR, data.Seasons, data.LicNo];
        console.log(values1);
        const response = await client.query(query1, values1);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.FillVariety = (data) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query1 = `SELECT D."Variety_Code" as "VARIETY_CODE",D."Variety_Name" as "VARIETY_NAME" FROM "STOCK_DEALERSTOCK" A 
        LEFT OUTER JOIN "mCropCategory" B ON A."CROPCATG_ID" = B."Category_Code"
        LEFT OUTER JOIN "mCrop" C ON A."CROP_ID" = C."Crop_Code" 
        LEFT OUTER JOIN "mCropVariety" D ON A."CROP_VERID" = D."Variety_Code"
        WHERE A."LICENCE_NO" = $3 AND A."CROP_ID" = $4 AND A."FIN_YR" = $1 AND A."SEASSION" = $2 
        AND A."AVL_NO_OF_BAGS" > 0 AND D."IS_ACTIVE" = 1 AND A."VALIDITY" = 1 
        GROUP BY D."Variety_Code",D."Variety_Name" ORDER BY D."Variety_Code",D."Variety_Name"`;
        const values1 = [data.FIN_YR, data.Seasons, data.LicNo, data.Crop];
        const response = await client.query(query1, values1);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.FILLFINYR = () => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query1 = `SELECT "FIN_YR" FROM "mFINYR" WHERE "IS_ACTIVE" = 1 `;
        const values1 = [];
        const response = await client.query(query1, values1);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.FILLSEASSION = (data) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query1 = ` SELECT "SHORT_NAME","SEASSION_NAME" FROM "mSEASSION" WHERE "FIN_YR" = $1 AND "IS_ACTIVE" = 1  `;
        const values1 = [data.FIN_YR];
        const response = await client.query(query1, values1);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.FILLDEALERSTOCK = (data) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query1 = `SELECT A."STOCK_ID",A."CROP_VERID",A."LICENCE_NO",A."STOCK_ID",A."LOT_NO",A."RECEIVE_UNITCD",A."BAG_SIZE_IN_KG", "AVL_NO_OF_BAGS" as "AVL_BAGS",A."AVL_QUANTITY",
        D."All_in_cost_Price",D."TOT_SUBSIDY","USER_TYPE" AS "Receive_Unitname"                
        FROM "STOCK_DEALERSTOCK" A                 
        LEFT OUTER JOIN "Stock_Receive_Unit_Master" B ON A."RECEIVE_UNITCD" = B."Receive_Unitcd" AND B."IS_ACTIVE" = 1                
        LEFT OUTER JOIN "Price_SourceMapping" C ON B."Receive_Unitcd" = C."RECEIVE_UNITCD" AND C."SEASSION" =  $3 AND C."FIN_YR" = $2               
        LEFT OUTER JOIN "Stock_Pricelist" D ON C."PRICE_RECEIVE_UNITCD" = D."RECEIVE_UNITCD" AND D."Crop_Vcode" =  $5 AND D."Crop_Code" =  $4 AND D."seasons" = $3
        AND D."F_Year" =  $2                
        WHERE A."LICENCE_NO" = $1 AND A."AVL_QUANTITY" > 0 AND A."CROP_ID" =  $4 AND A."CROP_VERID" = $5 AND A."VALIDITY" = 1  AND A."FIN_YR" =  $2
        AND A."SEASSION" = $3 --AND A.EXPIRY_DATE >GETDATE()      
        ORDER BY A."AVL_NO_OF_BAGS"`;
        const values1 = [data.LIC_NO, data.FIN_YR, data.SEASSION, data.CROP_CODE, data.CROP_VERID];
        const response = await client.query(query1, values1);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.GetDistCodeByLicNo = (data) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const result = await sequelizeSeed.query(`SELECT DIST_CODE FROM SEED_LIC_DIST WHERE LIC_NO = :LICENCE_NO`, {//GAN/141088
            replacements: { LICENCE_NO: data.LICENCE_NO }, type: sequelizeStock.QueryTypes.SELECT
        });
        resolve(result[0].DIST_CODE);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.GetDAOCodeByLicNo = (data) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const result = await sequelizeSeed.query(`SELECT RIGHT(DAO_CD,2) as daocode FROM SEED_LIC_DIST WHERE LIC_NO = :LICENCE_NO`, {//GAN/141088
            replacements: { LICENCE_NO: data.LICENCE_NO }, type: sequelizeStock.QueryTypes.SELECT
        });
        resolve(result[0].daocode);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.GETFARMERINFO = (FarmerId) => new Promise(async (resolve, reject) => {
    var con = new sqlstock.ConnectionPool(locConfig_stock);
    try {
        con.connect().then(function success() {
            const request = new sqlstock.Request(con);
            request.input('FARMER_ID', FarmerId);
            request.execute('SP_GETFARMERINFO', function (err, result) {
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
    } catch (e) {
        console.log(`Oops! An error occurred: ${e}`);
    }
});
exports.InsertSaleDealer = (data) => new Promise(async (resolve, reject) => {

    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        if (data.SEASON = 'Kharif') {
            data.SEASON = 'K'
        }
        else {
            data.SEASON = 'R'
        }
        var SALE_QUANTITY = 0.00;
        var MAXTRAN_NO = 0;
        var CNT = 0;
        var mAPP_DATE = '';
        var mMON = '';
        var mYR = '';
        var YR = 0;
        var SCHEME_CODE = '';
        var USER_TYPE = '';
        var VARIETYAGE = '';

        var mTOT_SUB_AMOUNT_GOI = 0.00;
        var mTOT_SUB_AMOUNT_SP = 0.00;
        var aTOTQTL_TAKEN = 0.00;
        var aTOTQTL_SUBSIDY = 0.00;
        var pTOT_SUB_AMOUNT_GOI = 0.00;
        var pTOT_SUB_AMOUNT_SP = 0.00;
        var aTOTSUBSIDY_TAKEN_GOI = 0.00;
        var aTOTSUBSIDY_TAKEN_SP = 0.00;
        var PREBOOKING_AMT = 0.00;
        var applicationID = '';
        var IS_PREBOOKING = 0;
        var PREBOOKING_AMT1 = 0.00;
        var NO_OF_BAGS = 0;

        let YR_ = await client.query(`select SUBSTRING("FIN_YR",1,4) as yr from "mFINYR" where "IS_ACTIVE"=1`);
        YR = YR_.rows[0].yr
        let USER_TYPE_ = await client.query(`SELECT "USER_TYPE" FROM "STOCK_DEALERSTOCK" WHERE "LICENCE_NO" = ${data.LIC_NO} limit 1`);
        USER_TYPE = USER_TYPE_.rows[0].USER_TYPE;

        MAXTRAN_NO = await client.query(`SELECT COALESCE(MAX(cast(SUBSTRING("TRANSACTION_ID", 18, 5) as int) ), 0)+1 AS max_value FROM "STOCK_DEALERSALEHDR" WHERE SUBSTRING("TRANSACTION_ID",1,2) = 'W'${data.SEASON} AND SUBSTRING("TRANSACTION_ID",3,2) = SUBSTRING(${data.FINYR},3,2) AND SUBSTRING("TRANSACTION_ID",5,2) = ${data.distCode} AND SUBSTRING("TRANSACTION_ID",7,2) = ${data.DAO_CD} AND SUBSTRING("TRANSACTION_ID",9,2) = SUBSTRING(${data.LIC_NO},10,2) AND SUBSTRING("TRANSACTION_ID",11,2) = SUBSTRING(${data.LIC_NO},13,2)  AND SUBSTRING("TRANSACTION_ID",13,4) = SUBSTRING(${data.LIC_NO},16,4)`);
        TRANSACTION_ID = 'W' + data.SEASON + SUBSTRING(YR, 3, 2) + data.distCode + data.DAO_CD + SUBSTRING(data.LIC_NO, 10, 2) + SUBSTRING(data.LIC_NO, 13, 2) + SUBSTRING(data.LIC_NO, 16, 4) + '-' + CONVERT(VARCHAR(10), MAXTRAN_NO.rows[0].max_value);

        let mCROPCATG_ID = '';
        let mCROP_ID = '';
        let mCROP_VERID = '';
        let mCROP_CLASS = '';
        let mLOT_NO = '';
        let mReceive_Unitcd = '';
        let mBAG_SIZE_KG = 0;
        let mNO_OF_BAGS = 0;
        let mQUANTITY = 0.00;
        let mPRICE_QTL = 0.00;
        let mSUBSIDY_QTL = 0.00;
        let mAMOUNT = 0.00;
        let ALL_IN_COST_AMOUNT = 0.00;
        let SUBSIDY_AMOUNT = 0.00;
        let TOT_SALE_AMOUNT = 0.00;
        let TOT_SUB_AMOUNT = 0.00;
        let AVL_NO_OF_BAGS = 0;
        let AVL_QUANTITY = 0.00;
        let DTL_ID = 0;
        let TOT_SUB_AMOUNT_GOI = 0.00;
        let TOT_SUB_AMOUNT_SP = 0.00;
        let MAP_CODE = 0;
        let PRICE_RECEIVE_UNITCD = '';
        let SCHEME_CODE_GOI = '';;
        let SCHEME_CODE_SP = '';;
        let VARIETY_AFTER_10YEAR = 0;
        let MAX_SUBSIDY = 0.00;
        let ADMISSIBLE_SUBSIDY = 0.00;
        let TOT_QTL = 0.00;
        let fFARMERID = 0;
        let STATUS = '';
        let FARMERLEN = 0;
        let AVL_BAGS = 0;

        let GOIQTY = 0;
        let GOISUBSIDYTAKENQTY = 0.00;
        let aTOT_SUB_AMOUNT_GOI = 0.00;
        let aTOT_SUB_AMOUNT_SP = 0.00;

        fFARMERID = await client.query(`select SUBSTRING(${data.FARMER_ID},5,10) as farmerid`);
        FARMERLEN = await client.query(`SELECT length (${data.FARMER_ID}) as farmeridlength`);
        if (FARMERLEN.rows[0].farmeridlength == 5 && fFARMERID == 0) {
            // 130-368code here
        }
        else {
            const query = `INSERT INTO public."STOCK_DEALERSALEHDR"(
                "SALE_DATE", "FARMER_ID", "LIC_NO", "TRANSACTION_ID", "TOT_SALE_AMOUNT", "TOT_SUB_AMOUNT_GOI", "TOT_SUB_AMOUNT_SP", "SEASON", "FIN_YEAR", "IS_ACTIVE", "UPDATED_BY", "UPDATED_ON", "USER_TYPE", "USERIP", "TRN_TYPE") values ($1, $2, $3, $4, $5, $6, $7, $8, $9, $10,$11, $12, $13, $14, $15)`;
            const values = ['now()', data.FARMER_ID, data.LIC_NO, TRANSACTION_ID, 0, 0, 0, data.SEASON, data.FINYR, data.LIC_NO, 'now()', 'OSSC', data.ipAdress, 'W'];
            console.log(query, values);
            let insertintoSTOCKDEALERSALEHDR = await client.query(query, values);
            const query1 = `INSERT INTO public."Test1"(
                "TRANSACTION_ID", "value") values ($1, $2)`;
            const values1 = [TRANSACTION_ID, data];
            insertintoTest1 = await client.query(query1, values1);
            var count = 0
            if (insertintoSTOCKDEALERSALEHDR.rowCount == 1) {
                count += 1
                for (const e of data.VALUES) {
                    USER_TYPE = await client.query(`SELECT "USER_TYPE" FROM "STOCK_DEALERSTOCK" WHERE "LICENCE_NO" = '${data.LIC_NO}' AND "LOT_NO" = '${e.LOT_NO}' AND "AVL_NO_OF_BAGS" > 0 limit 1`);
                    let cropandclass = await client.query(`select "CropCatg_ID","Class" from "Stock_StockDetails" where "Lot_No" = '${e.LOT_NO}'  AND  "Crop_ID" = '${e.CROP_ID}'  AND "Crop_Verid" ='${e.CROP_VERID}' `);
                    mCROPCATG_ID = cropandclass.rows[0].CropCatg_ID
                    mCROP_CLASS = cropandclass.rows[0].Class;
                    MAX_SUBSIDY = await client.query(`select "MAX_SUBSIDY" from "mMAX_SUBSIDY" where "CROP_CODE"='${e.LOT_NO}' and "FIN_YEAR"='${data.FINYR}' and "SEASON"='${data.SEASON}' and "IS_ACTIVE"=1`);
                    if (fFARMERID == 0)
                        MAX_SUBSIDY = 0;
                    MAP_CODE = await client.query(`SELECT "MAP_CODE" FROM "Dist_CropMapping" WHERE "DIST_CODE" = '${data.distCode}' AND "CROP_CODE" =  '${e.CROP_ID}'  AND "SEASSION" ='${data.SEASON}' AND "FIN_YEAR" ='${data.FINYR}'`);
                    PRICE_RECEIVE_UNITCD = await client.query(`select "PRICE_RECEIVE_UNITCD" from public."Price_SourceMapping" where "RECEIVE_UNITCD"='${e.Receive_Unitcd}' and "SEASSION"='${data.SEASON}' and "FIN_YR"='${data.FINYR}'`);
                    if (MAP_CODE.rows[0].MAP_CODE == 1) {
                        SCHEME_CODE_GOI = 'OR7';
                    }
                    else if (MAP_CODE.rows[0].MAP_CODE == 2) {
                        SCHEME_CODE_GOI = 'OR1';
                    } else if (MAP_CODE.rows[0].MAP_CODE == 3) {
                        SCHEME_CODE_GOI = 'OR43';
                    }
                    else if (MAP_CODE.rows[0].MAP_CODE == 4) {
                        SCHEME_CODE_GOI = '0';
                    }
                    SCHEME_CODE_SP = 'OR119'
                    let all_data = await client.query(`select "All_in_cost_Price","GOI_Subsidy","STATEPLAN_Subsidy","VARIETY_AFTER_10YEAR" from public."Stock_Pricelist" where "Crop_class"='${mCROP_CLASS}' and "RECEIVE_UNITCD"='${PRICE_RECEIVE_UNITCD.rows[0].Price_SourceMapping}' and "Crop_Vcode"='${e.CROP_VERID}' and "Crop_Code"='${e.CROP_ID}' and seasons='${data.SEASON}' and "F_Year"='${data.FINYR}' and "IS_ACTIVE"=1`);
                    ALL_IN_COST_AMOUNT = all_data.rows[0].All_in_cost_Price;
                    TOT_SUB_AMOUNT_GOI = all_data.rows[0].GOI_Subsidy;
                    TOT_SUB_AMOUNT_SP = all_data.rows[0].STATEPLAN_Subsidy;
                    VARIETY_AFTER_10YEAR = all_data.rows[0].VARIETY_AFTER_10YEAR;
                    AVL_BAGS = await client.query(`SELECT "AVL_NO_OF_BAGS" FROM "STOCK_DEALERSTOCK" WHERE "LICENCE_NO" = '${data.LIC_NO}' AND "CLASS" = '${mCROP_CLASS}' AND "CROP_ID" = '${e.CROP_ID}' AND "CROP_VERID" = '${e.CROP_VERID}' AND "LOT_NO" = '${e.LOT_NO}'  AND "VALIDITY" = 1`);
                    if (AVL_BAGS.rows[0].AVL_NO_OF_BAGS >= e.NO_OF_BAGS) {
                        let STOCK_FARMERSTOCK = await client.query(`SELECT * FROM "STOCK_FARMERSTOCK" WHERE "FARMER_ID" = '${data.LIC_NO}' AND "Crop_Code" = '${e.CROP_ID}' AND "SEASON" = '${data.SEASON}' AND "FIN_YEAR" = '${data.FINYR}'`);
                        if (STOCK_FARMERSTOCK.rows[0].length == 0) {
                            if (e.QUANTITY < MAX_SUBSIDY.rows[0].MAX_SUBSIDY) {
                                ADMISSIBLE_SUBSIDY = e.QUANTITY;
                            }
                            else {
                                ADMISSIBLE_SUBSIDY = MAX_SUBSIDY.rows[0].MAX_SUBSIDY;
                            }
                            const query2 = `INSERT INTO public."STOCK_FARMERSTOCK"(
                                "FARMER_ID", "Crop_Code", "BAG_SIZE_KG", "NO_OF_BAGS", "TOT_QTL", "SEASON", "FIN_YEAR", "UPDATED_ON") values ($1, $2, $3, $4, $5, $6, $7, $8)`;
                            const values2 = [data.FARMER_ID, e.CROP_ID, e.BAG_SIZE_KG, e.NO_OF_BAGS, e.QUANTITY, data.SEASON, data.FINYR, 'now()'];
                            let insertintoSTOCK_FARMERSTOCK = await client.query(query2, values2);
                        }
                        else {
                            let updateinSTOCK_FARMERSTOCK = await client.query(`update "STOCK_FARMERSTOCK" set "NO_OF_BAGS" = "NO_OF_BAGS" +${e.NO_OF_BAGS} ,"TOT_QTL"="TOT_QTL"+${e.QUANTITY} 
                            where "FARMER_ID"='${data.FARMER_ID}'  and "Crop_Code"='${e.CROP_ID}' and "SEASON" = '${data.SEASON}' and "FIN_YEAR" = '${data.FINYR}'`);

                        }
                    }
                    let totsubsidytaken = await client.query(`select COALESCE(sum("TOT_SUB_AMOUNT_GOI"),0) as "TOT_SUB_AMOUNT_GOI",COALESCE(sum("TOT_SUB_AMOUNT_SP"),0) as "TOT_SUB_AMOUNT_SP" from public."STOCK_FARMER" where "FARMER_ID"='${data.FARMER_ID}' and "SEASON"='${data.SEASON}' and "FIN_YEAR"='${data.FINYR}'`);
                    let totqtyandsubsidy = await client.query(`select COALESCE(sum("TOT_QTL"),0) as "TOT_QTL" ,COALESCE(sum("ADMISSIBLE_SUBSIDY"),0) as "ADMISSIBLE_SUBSIDY" from public."STOCK_FARMER" where "FARMER_ID"='${data.FARMER_ID}' and "CROP_ID"='${e.CROP_ID}' and "SEASON"='${data.SEASON}' and "FIN_YEAR"='${data.FINYR}'`);
                    aTOTSUBSIDY_TAKEN_GOI = totsubsidytaken.rows[0].TOT_SUB_AMOUNT_GOI;
                    aTOTSUBSIDY_TAKEN_SP = totsubsidytaken.rows[0].TOT_SUB_AMOUNT_SP
                    aTOTQTL_TAKEN = totqtyandsubsidy.rows[0].TOT_QTL
                    aTOTQTL_SUBSIDY = totqtyandsubsidy.rows[0].ADMISSIBLE_SUBSIDY;

                    if (aTOTQTL_TAKEN <= MAX_SUBSIDY.rows[0].MAX_SUBSIDY) {
                        if (aTOTQTL_TAKEN == aTOTQTL_SUBSIDY) {
                            if ((e.QUANTITY + aTOTQTL_TAKEN) <= MAX_SUBSIDY.rows[0].MAX_SUBSIDY) {
                                ADMISSIBLE_SUBSIDY = e.QUANTITY;
                            }
                            else {
                                ADMISSIBLE_SUBSIDY = MAX_SUBSIDY.rows[0].MAX_SUBSIDY - aTOTQTL_TAKEN;
                            }
                        }
                        else {
                            if ((e.QUANTITY + aTOTQTL_TAKEN) <= MAX_SUBSIDY.rows[0].MAX_SUBSIDY) {
                                ADMISSIBLE_SUBSIDY = e.QUANTITY + (aTOTQTL_TAKEN - aTOTQTL_SUBSIDY)
                            }
                            else {
                                ADMISSIBLE_SUBSIDY = MAX_SUBSIDY.rows[0].MAX_SUBSIDY - aTOTQTL_SUBSIDY
                            }
                        }
                    }
                    else {
                        ADMISSIBLE_SUBSIDY = 0
                    }
                    mTOT_SUB_AMOUNT_GOI = (ADMISSIBLE_SUBSIDY * TOT_SUB_AMOUNT_GOI);
                    mTOT_SUB_AMOUNT_SP = (ADMISSIBLE_SUBSIDY * TOT_SUB_AMOUNT_SP);

                    var VARIETY_AFTER10 = '';
                    var SUBSIDY_AMT = 0.00;
                    var TOTSUBSIDY_AMT = 0.00;
                    var GOI_AMT = 0.00;
                    var SP_AMT = 0.00;
                    var pGOI_QTL = '';
                    var GOI_QTL = 0.00;
                    var SPQTY = 0;
                    if (mCROPCATG_ID == '01') {
                        if (VARIETY_AFTER_10YEAR == 0) {
                            let GETSUBSIDYVALUE = await exports.GETSUBSIDYVALUE(data.FINYR, data.SEASON, data.FARMER_ID, VARIETY_AFTER_10YEAR, SCHEME_CODE_GOI, PRICE_RECEIVE_UNITCD.rows[0].PRICE_RECEIVE_UNITCD, aTOTSUBSIDY_TAKEN_GOI, ADMISSIBLE_SUBSIDY, TOT_SUB_AMOUNT_GOI, TOT_SUB_AMOUNT_SP);
                        }
                        else {
                            GOISUBSIDYTAKENQTY = await client.query(`select COALESCE(sum("ADMISSIBLE_SUBSIDY"),0) as "ADMISSIBLE_SUBSIDY" from public."STOCK_FARMER" 
                            where "FIN_YEAR"='${data.FINYR}' and "SEASON"='${data.SEASON}' and "CROP_ID"='C002' and "FARMER_ID"='${data.FARMER_ID}'`);
                            if ((GOISUBSIDYTAKENQTY.rows[0].ADMISSIBLE_SUBSIDY + ADMISSIBLE_SUBSIDY) > 1) {
                                if (ADMISSIBLE_SUBSIDY >= 1 && GOISUBSIDYTAKENQTY.rows[0].ADMISSIBLE_SUBSIDY == 0) {
                                    GOIQTY = 1;
                                }
                                else if (((GOISUBSIDYTAKENQTY.rows[0].ADMISSIBLE_SUBSIDY + ADMISSIBLE_SUBSIDY) > 1) && (GOISUBSIDYTAKENQTY.rows[0].ADMISSIBLE_SUBSIDY > 0) && (GOISUBSIDYTAKENQTY.rows[0].ADMISSIBLE_SUBSIDY < 1)) {
                                    GOIQTY = 1 - GOISUBSIDYTAKENQTY.rows[0].ADMISSIBLE_SUBSIDY;
                                }
                            }
                            else {
                                GOIQTY = ADMISSIBLE_SUBSIDY;
                            }
                            aTOT_SUB_AMOUNT_GOI = GOIQTY * TOT_SUB_AMOUNT_GOI;
                            aTOT_SUB_AMOUNT_SP = mTOT_SUB_AMOUNT_SP + (mTOT_SUB_AMOUNT_GOI - aTOT_SUB_AMOUNT_GOI)

                            mTOT_SUB_AMOUNT_GOI = aTOT_SUB_AMOUNT_GOI;
                            mTOT_SUB_AMOUNT_SP = aTOT_SUB_AMOUNT_SP;

                        }
                        var VARIETY_AFTER10_TotSubsidy = await client.query(`SELECT "VARIETY_AFTER_10YEAR","TOT_SUBSIDY" FROM "Stock_Pricelist" WHERE "F_Year" = '${data.FINYR}' AND seasons = '${data.SEASON}' AND "Crop_Vcode" = '${e.CROP_VERID}' limit 1`);
                        VARIETY_AFTER10 = VARIETY_AFTER10_TotSubsidy.rows[0].VARIETY_AFTER_10YEAR;
                        SUBSIDY_AMT = VARIETY_AFTER10_TotSubsidy.rows[0].TOT_SUBSIDY;
                        pGOI_QTL = await client.query(`SELECT COALESCE(SUM("GOI_QTY"),0) as "pGOI_QTL" FROM "STOCK_FARMER" WHERE "FIN_YEAR" = '${data.FINYR}' AND "CROP_ID" = 'C002' AND "FARMER_ID" = '${data.FARMER_ID}'`)
                        if (pGOI_QTL.rows[0].pGOI_QTL < 1) {
                            if ((pGOI_QTL.rows[0].pGOI_QTL + ADMISSIBLE_SUBSIDY) <= 1) {
                                GOI_QTL = ADMISSIBLE_SUBSIDY;
                            }
                            else {
                                GOI_QTL = (1 - pGOI_QTL.rows[0].pGOI_QTL)
                            }
                            TOTSUBSIDY_AMT = (ADMISSIBLE_SUBSIDY * SUBSIDY_AMT);
                            GOI_AMT = (GOI_QTL * SUBSIDY_AMT)
                            SP_AMT = (TOTSUBSIDY_AMT - GOI_AMT)
                        }
                        else {
                            GOI_QTL = 0;
                            TOTSUBSIDY_AMT = (ADMISSIBLE_SUBSIDY * SUBSIDY_AMT);
                            GOI_AMT = (0)
                            SP_AMT = (TOTSUBSIDY_AMT)
                        }
                        GOIQTY = GOI_QTL;
                        mTOT_SUB_AMOUNT_GOI = GOI_AMT;
                        mTOT_SUB_AMOUNT_SP = SP_AMT;
                    }
                    else if (mCROPCATG_ID != '01') {
                        let GETSUBSIDYVALUE_ = await exports.GETSUBSIDYVALUE_(data.FINYR, data.SEASON, data.FARMER_ID, e.CROP_VERID, e.CROP_ID, PRICE_RECEIVE_UNITCD.rows[0].PRICE_RECEIVE_UNITCD, e.QUANTITY);
                        mTOT_SUB_AMOUNT_GOI = GETSUBSIDYVALUE_.rows[0].mTOT_SUB_AMOUNT_GOI;
                        mTOT_SUB_AMOUNT_SP = GETSUBSIDYVALUE_.rows[0].mTOT_SUB_AMOUNT_SP;
                        GOIQTY = GETSUBSIDYVALUE_.rows[0].GOIQTY;
                        SPQTY = GETSUBSIDYVALUE_.rows[0].SPQTY;
                        VARIETYAGE = GETSUBSIDYVALUE_.rows[0].VARIETYAGE;
                    }
                    if (SCHEME_CODE_GOI == 'OR7') {
                        mTOT_SUB_AMOUNT_SP = mTOT_SUB_AMOUNT_SP + mTOT_SUB_AMOUNT_GOI;
                        mTOT_SUB_AMOUNT_GOI = 0;
                        GOIQTY = 0;
                    }
                    //preboking or not
                    // if (data.PrebookingorNot) {
                    //     PREBOOKING_AMT = (ALL_IN_COST_AMOUNT * mQUANTITY);
                    //     PREBOOKING_AMT1 += PREBOOKING_AMT;
                    //     NO_OF_BAGS += e.NO_OF_BAGS;
                    // select * from prebookinglist  where TRANSACTION_ID='WK23170315160038-57-2'

                    // select * from [STOCK_FARMER_2021-22_R] where TRANSACTION_ID like 'WK23170315160038-57-%'

                    // --select "All_in_cost_Price","GOI_Subsidy","STATEPLAN_Subsidy","VARIETY_AFTER_10YEAR" from "Stock_Pricelist" where  "Crop_Vcode"='V372'  and seasons='K' and "F_Year"='2023-24' and "IS_ACTIVE"=1

                    // select * from STOCK_DEALERSALEDTL where DTL_TRANSACTION_ID='WK23190219200005-1-1'

                    // select * from STOCK_DEALERSALEHDR where TRANSACTION_ID='WK23190219200005-1'
                    // }
                    const query3 = `INSERT INTO public."STOCK_DEALERSALEDTL"(
                        "TRANSACTION_ID", "DTL_TRANSACTION_ID", "CROPCATG_ID", "CROP_ID", "CROP_VERID", "CROP_CLASS", "LOT_NUMBER", "Receive_Unitcd", "PRICE_QTL", "BAG_SIZE_KG", "NO_OF_BAGS", "TOT_QTL", "ADMISSIBLE_SUBSIDY", "ALL_IN_COST_AMOUNT", "SCHEME_CODE_GOI", "TOT_SUB_AMOUNT_GOI", "SCHEME_CODE_SP", "TOT_SUB_AMOUNT_SP", "SUBSIDY_AMOUNT", "RELEASE_STATUS") values ($1, $2, $3, $4, $5, $6, $7, $8, $9, $10,$11, $12, $13, $14, $15, $16, $17, $18, $19, $20)`;
                   const values3 = [TRANSACTION_ID, TRANSACTION_ID+'-'+count, mCROPCATG_ID,e.CROP_ID,e.CROP_VERID,mCROP_CLASS,e.LOT_NO,e.Receive_Unitcd,e.PRICE_QTL,e.BAG_SIZE_KG,e.NO_OF_BAGS,e.QUANTITY,ADMISSIBLE_SUBSIDY,ALL_IN_COST_AMOUNT*e.QUANTITY,SCHEME_CODE_GOI,mTOT_SUB_AMOUNT_GOI,SCHEME_CODE_SP,mTOT_SUB_AMOUNT_SP,mTOT_SUB_AMOUNT_GOI+mTOT_SUB_AMOUNT_SP,'P'];
                       console.log(query, values);
                   let insertintoSTOCK_DEALERSALEDTL = await client.query(query3, values3);
                }
            }
        }
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.GETSUBSIDYVALUE = (FINYR, Season, FARMER_ID, VARIETY_AFTER_10YEAR, SCHEME_CODE_GOI, PRICE_RECEIVE_UNITCD, aTOTSUBSIDY_TAKEN_GOI, ADMISSIBLE_SUBSIDY, TOT_SUB_AMOUNT_GOI, TOT_SUB_AMOUNT_SP) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        var GOISUBSIDYTAKENQTY = '';
        var aTOT_SUB_AMOUNT_GOI = 0.00;
        var aTOT_SUB_AMOUNT_SP = 0.00;
        var FIN_YR = FINYR;
        var SEASON = Season;
        var GOIQTY = 0.00;
        var mTOT_SUB_AMOUNT_GOI = 0;
        var mTOT_SUB_AMOUNT_SP = 0;

        GOISUBSIDYTAKENQTY = await client.query(`select COALESCE(sum("ADMISSIBLE_SUBSIDY"),0) as "ADMISSIBLE_SUBSIDY" from public."STOCK_FARMER" 
        where "FIN_YEAR"='${FIN_YR}' and "SEASON"='${SEASON}' and "CROP_ID"='C002' and "FARMER_ID"='${FARMER_ID}'`);
        if ((GOISUBSIDYTAKENQTY.rows[0].ADMISSIBLE_SUBSIDY + ADMISSIBLE_SUBSIDY) > 1) {
            if (ADMISSIBLE_SUBSIDY >= 1 && GOISUBSIDYTAKENQTY.rows[0].ADMISSIBLE_SUBSIDY == 0) {
                GOIQTY = 1
            }
            else if (((GOISUBSIDYTAKENQTY.rows[0].ADMISSIBLE_SUBSIDY + ADMISSIBLE_SUBSIDY) > 1) && (GOISUBSIDYTAKENQTY.rows[0].ADMISSIBLE_SUBSIDY > 0) && (GOISUBSIDYTAKENQTY.rows[0].ADMISSIBLE_SUBSIDY < 1)) {
                GOIQTY = 1 - GOISUBSIDYTAKENQTY.rows[0].ADMISSIBLE_SUBSIDY;
            }
        }
        else {
            GOIQTY = ADMISSIBLE_SUBSIDY;
        }
        if (VARIETY_AFTER_10YEAR == 0) {
            if (PRICE_RECEIVE_UNITCD == '0000') //ossc
            {
                if ((aTOTSUBSIDY_TAKEN_GOI + (ADMISSIBLE_SUBSIDY * TOT_SUB_AMOUNT_GOI)) > 1493) {
                    if (aTOTSUBSIDY_TAKEN_GOI >= 1493) {
                        mTOT_SUB_AMOUNT_GOI = 0;
                        mTOT_SUB_AMOUNT_SP = mTOT_SUB_AMOUNT_SP + ((ADMISSIBLE_SUBSIDY * TOT_SUB_AMOUNT_GOI) - mTOT_SUB_AMOUNT_GOI);
                    }
                    else {
                        mTOT_SUB_AMOUNT_GOI = 1493 - aTOTSUBSIDY_TAKEN_GOI
                        mTOT_SUB_AMOUNT_SP = mTOT_SUB_AMOUNT_SP + ((ADMISSIBLE_SUBSIDY * TOT_SUB_AMOUNT_GOI) - mTOT_SUB_AMOUNT_GOI)
                    }
                }
                else {
                    mTOT_SUB_AMOUNT_GOI = (ADMISSIBLE_SUBSIDY * TOT_SUB_AMOUNT_GOI);
                    mTOT_SUB_AMOUNT_SP = (ADMISSIBLE_SUBSIDY * TOT_SUB_AMOUNT_SP);
                }
            }
            else if (PRICE_RECEIVE_UNITCD == '0103') {// NSC
                if ((aTOTSUBSIDY_TAKEN_GOI + (ADMISSIBLE_SUBSIDY * TOT_SUB_AMOUNT_GOI)) > 2000) {
                    if (aTOTSUBSIDY_TAKEN_GOI >= 2000) {
                        mTOT_SUB_AMOUNT_GOI = 0
                        mTOT_SUB_AMOUNT_SP = mTOT_SUB_AMOUNT_SP + ((ADMISSIBLE_SUBSIDY * TOT_SUB_AMOUNT_GOI) - mTOT_SUB_AMOUNT_GOI)
                    }
                    else {
                        mTOT_SUB_AMOUNT_GOI = 2000 - aTOTSUBSIDY_TAKEN_GOI
                        mTOT_SUB_AMOUNT_SP = mTOT_SUB_AMOUNT_SP + ((ADMISSIBLE_SUBSIDY * TOT_SUB_AMOUNT_GOI) - mTOT_SUB_AMOUNT_GOI)
                    }
                }
                else {
                    mTOT_SUB_AMOUNT_GOI = (ADMISSIBLE_SUBSIDY * TOT_SUB_AMOUNT_GOI);
                    mTOT_SUB_AMOUNT_SP = (ADMISSIBLE_SUBSIDY * TOT_SUB_AMOUNT_SP);
                }
            }
        }
        else {
            if (PRICE_RECEIVE_UNITCD == '0000') //ossc
            {
                if ((aTOTSUBSIDY_TAKEN_GOI + (ADMISSIBLE_SUBSIDY * TOT_SUB_AMOUNT_GOI)) > 1000) {
                    if (aTOTSUBSIDY_TAKEN_GOI >= 1000) {
                        mTOT_SUB_AMOUNT_GOI = 0
                        mTOT_SUB_AMOUNT_SP = mTOT_SUB_AMOUNT_SP + ((ADMISSIBLE_SUBSIDY * TOT_SUB_AMOUNT_GOI) - mTOT_SUB_AMOUNT_GOI)
                    }
                    else {
                        mTOT_SUB_AMOUNT_GOI = 1000 - aTOTSUBSIDY_TAKEN_GOI
                        mTOT_SUB_AMOUNT_SP = mTOT_SUB_AMOUNT_SP + ((ADMISSIBLE_SUBSIDY * TOT_SUB_AMOUNT_GOI) - mTOT_SUB_AMOUNT_GOI)
                    }
                }
                else {
                    mTOT_SUB_AMOUNT_GOI = (ADMISSIBLE_SUBSIDY * TOT_SUB_AMOUNT_GOI);
                    mTOT_SUB_AMOUNT_SP = (ADMISSIBLE_SUBSIDY * TOT_SUB_AMOUNT_SP);
                }
            }
            else if (PRICE_RECEIVE_UNITCD == '0103')//NSC
            {
                if ((aTOTSUBSIDY_TAKEN_GOI + (ADMISSIBLE_SUBSIDY * TOT_SUB_AMOUNT_GOI)) > 1000) {
                    if (aTOTSUBSIDY_TAKEN_GOI >= 1000) {
                        mTOT_SUB_AMOUNT_GOI = 0
                        mTOT_SUB_AMOUNT_SP = mTOT_SUB_AMOUNT_SP + ((ADMISSIBLE_SUBSIDY * TOT_SUB_AMOUNT_GOI) - mTOT_SUB_AMOUNT_GOI)
                    }
                    else {
                        mTOT_SUB_AMOUNT_GOI = 1000 - aTOTSUBSIDY_TAKEN_GOI
                        mTOT_SUB_AMOUNT_SP = mTOT_SUB_AMOUNT_SP + ((ADMISSIBLE_SUBSIDY * TOT_SUB_AMOUNT_GOI) - mTOT_SUB_AMOUNT_GOI)
                    }
                }
                else {
                    mTOT_SUB_AMOUNT_GOI = (ADMISSIBLE_SUBSIDY * TOT_SUB_AMOUNT_GOI);
                    mTOT_SUB_AMOUNT_SP = (ADMISSIBLE_SUBSIDY * TOT_SUB_AMOUNT_SP);
                }
            }
        }
        resolve({ mTOT_SUB_AMOUNT_GOI: aTOT_SUB_AMOUNT_GOI, mTOT_SUB_AMOUNT_SP: aTOT_SUB_AMOUNT_SP, GOIQTY: GOIQTY });
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }

});
exports.GETSUBSIDYVALUE_ = (FINYR, Season, FARMER_ID, VARIETY_CODE, CROP_CODE, SOURCE_CODE, QTY) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        var MAXTOT_SUBSIDY = '';
        var MAXGOI_SUBSIDY = '';
        var MAXSP_SUBSIDY = '';
        var TOT_SUBSIDY = '';
        var GOI_SUBSIDY = '';
        var SP_SUBSIDY = '';
        var TOT_SUBSIDY_TAKEN = '';
        var GOI_SUBSIDY_TAKEN = '';
        var SP_SUBSIDY_TAKEN = '';

        var GOI_QTL = '';
        var SP_QTL = '';
        var GOIQTY = '';
        var SPQTY = '';

        let varietyageGOI_QTLSP_QTL = await client.query(`SELECT "VARIETY_AFTER_10YEAR","GOI_Subsidy","STATEPLAN_Subsidy" FROM "Stock_Pricelist" WHERE "Crop_Vcode" ='${VARIETY_CODE}' AND "F_Year" = '${FINYR}' AND seasons = '${Season}' AND "RECEIVE_UNITCD" = '${SOURCE_CODE}' `);
        let TOTSUBSIDYTAKEN = await client.query(`SELECT COALESCE(SUM("ADMISSIBLE_SUBSIDY"),0) as "ADMISSIBLE_SUBSIDY",COALESCE(SUM("GOI_QTY"),0) as "GOI_QTY" ,COALESCE(SUM("SP_QTY"),0) as "SP_QTY" FROM "STOCK_FARMER" WHERE "CROP_ID" = '${CROP_CODE}' AND "FIN_YEAR" = '${FINYR}' AND "SEASON" = '${Season}' AND "Receive_Unitcd" = '${SOURCE_CODE}' AND "FARMER_ID" = '${FARMER_ID}' `);
        let maxsubsidy = await client.query(`SELECT "MAX_SUBSIDY","GOI_SUBSIDY","SP_SUBSIDY" FROM "mMAX_SUBSIDY" WHERE "CROP_CODE" = '${CROP_CODE}' AND "FIN_YEAR" = '${FINYR}' AND "SEASON" = '${Season}'`);

        let VARIETYAGE = varietyageGOI_QTLSP_QTL.rows[0].VARIETY_AFTER_10YEAR
        GOI_QTL = varietyageGOI_QTLSP_QTL.rows[0].GOI_Subsidy
        SP_QTL = varietyageGOI_QTLSP_QTL.rows[0].STATEPLAN_Subsidy
        TOT_SUBSIDY_TAKEN = TOTSUBSIDYTAKEN.rows[0].ADMISSIBLE_SUBSIDY
        GOI_SUBSIDY_TAKEN = TOTSUBSIDYTAKEN.rows[0].GOI_QTY
        SP_SUBSIDY_TAKEN = TOTSUBSIDYTAKEN.rows[0].SP_QTY
        MAXTOT_SUBSIDY = maxsubsidy.rows[0].MAX_SUBSIDY
        MAXGOI_SUBSIDY = maxsubsidy.rows[0].GOI_SUBSIDY
        MAXSP_SUBSIDY = maxsubsidy.rows[0].SP_SUBSIDY
        if (TOT_SUBSIDY_TAKEN < MAXTOT_SUBSIDY) {
            TOT_SUBSIDY = (MAXTOT_SUBSIDY - TOT_SUBSIDY_TAKEN);
            GOI_SUBSIDY = (MAXGOI_SUBSIDY - GOI_SUBSIDY_TAKEN);
            SP_SUBSIDY = (MAXSP_SUBSIDY - SP_SUBSIDY_TAKEN);
            if (GOI_SUBSIDY > 0) {
                if (QTY >= GOI_SUBSIDY) {
                    GOIQTY = GOI_SUBSIDY;
                    QTY = QTY - GOIQTY;
                    if (QTY >= SP_SUBSIDY) {
                        SPQTY = SP_SUBSIDY
                    }
                    else {
                        SPQTY = 0;
                    }
                }
                else {
                    GOIQTY = QTY;
                    SPQTY = 0;
                }
            }
            else if (SP_SUBSIDY > 0) {
                if (QTY >= SP_SUBSIDY) {
                    GOIQTY = 0;
                    SPQTY = SP_SUBSIDY;
                }
                else {
                    SPQTY = QTY;
                    GOIQTY = 0;
                }
            }
            resolve({ mTOT_SUB_AMOUNT_GOI: 0, mTOT_SUB_AMOUNT_SP: 0, GOIQTY: GOIQTY, SPQTY: SPQTY, VARIETYAGE: VARIETYAGE });
        }

    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }

});

