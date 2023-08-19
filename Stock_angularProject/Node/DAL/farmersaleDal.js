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
        LEFT OUTER JOIN "Stock_Pricelist" D ON C."PRICE_RECEIVE_UNITCD" = D."RECEIVE_UNITCD" AND D."Crop_Vcode" =  $5 AND D."Crop_Code" =  $4 AND D."seasons" = 'K'
        AND D."F_Year" =  $2                
        WHERE A."LICENCE_NO" = $1 AND A."AVL_QUANTITY" > 0 AND A."CROP_ID" =  $4 AND A."CROP_VERID" = $5 AND A."VALIDITY" = 1  AND A."FIN_YR" =  $2
        AND A."SEASSION" = $3 --AND A.EXPIRY_DATE >GETDATE()      
        ORDER BY A."AVL_NO_OF_BAGS"   `;
        const values1 = [data.LIC_NO, data.FIN_YR, data.SEASSION, data.CROP_CODE, data.CROP_VERID];
        const response = await client.query(query1, values1);
        console.log(response.rows);
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

        let GOIQTY = 0.00;
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
            insertintoSTOCKDEALERSALEHDR = await client.query(query, values);
            const query1 = `INSERT INTO public."Test1"(
                "TRANSACTION_ID", "value") values ($1, $2)`;
            const values1 = [TRANSACTION_ID, data];
            insertintoTest1 = await client.query(query1, values1);

            if (insertintoSTOCKDEALERSALEHDR.rowCount == 1) {
                for (const e of data.VALUES) {
                    USER_TYPE = await client.query(`SELECT "USER_TYPE" FROM "STOCK_DEALERSTOCK" WHERE "LICENCE_NO" = '${data.LIC_NO}' AND "LOT_NO" = '${e.LOT_NO}' AND "AVL_NO_OF_BAGS" > 0 limit 1`);
                    let cropandclass = await client.query(`select "CropCatg_ID","Class" from "Stock_StockDetails" where "Lot_No" = '${e.LOT_NO}'  AND  "Crop_ID" = '${e.CROP_ID}'  AND "Crop_Verid" ='${e.CROP_VERID}' `);
                    mCROPCATG_ID = cropandclass.rows[0].CropCatg_ID
                    mCROP_CLASS = cropandclass.rows[0].Class;
                    MAX_SUBSIDY = await client.query(`select "MAX_SUBSIDY" from "mMAX_SUBSIDY" where "CROP_CODE"='${e.LOT_NO}' and "FIN_YEAR"='${data.FINYR}' and "SEASON"='${data.SEASON}' and "IS_ACTIVE"=1`);

                }
            }
        }
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});