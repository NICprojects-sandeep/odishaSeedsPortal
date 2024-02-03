// var dbConfig = require('../models/dbConfig');
var dbConfig = require('../config/dbSqlConnection');

var sqlstock = dbConfig.sqlstock;
var sequelizeSeed = dbConfig.sequelizeSeed;
var sequelizeFarmerDB = dbConfig.sequelizeFarmerDB;


var locConfigstock = dbConfig.locConfigStock;
var locConfigStockLive = dbConfig.locConfigStockLive;
var locConfigFarmerDB = dbConfig.locConfigFarmerDB;


var sequelizeStock = dbConfig.sequelizeStock;

exports.getUserDetails = async (LIC_NO, req, res) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        var queryText = `SELECT APP_FIRMNAME,LIC_NO FROM [DAFPSEED].[DBO].[SEED_LIC_DIST] WHERE LIC_NO ='${LIC_NO}'`
        const result = await sequelizeStock.query(queryText);
        return result[0]
    } catch (e) {
        console.log(`Oops! An error occurred: ${e}`);
    } finally {
        client.release();
    }
};
exports.GetFarmerInvHdr = (farmerID) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
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
    } finally {
        client.release();
    }
});
exports.GetFarmerInv = (data) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query1 = `SELECT "TOT_SALE_AMOUNT" as "TOT_AMT","TOT_SUB_AMOUNT_GOI"+"TOT_SUB_AMOUNT_SP" as "SUB_AMT","PREBOOKING_AMT" as "totalAmountPrebookingTime","SALE_DATE","FARMER_ID"
        FROM "STOCK_DEALERSALEHDR" A       
        WHERE A."TRANSACTION_ID" = $1 `;
        const values1 = [data.TRANSACTION_ID];
        const response = await client.query(query1, values1);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }



    // var con = new sqlstock.ConnectionPool(locConfigstock);
    // try {
    //     con.connect().then(function success() {
    //         const request = new sqlstock.Request(con);
    //         request.input('TRANSACTION_ID', data.TRANSACTION_ID);//data.TRANSACTION_ID
    //         //'W1611030041-176'
    //         request.execute('STOCK_FARMERINVPRICE', function (err, result) {
    //             if (err) {
    //                 console.log('An error occurred...', err);
    //             }
    //             else {
    //                 resolve(result.recordsets[0])
    //             }
    //             con.close();
    //         });
    //     }).catch(function error(err) {
    //         console.log('An error occurred...', err);
    //     });

    // } catch (e) {
    //     console.log(`Oops! An error occurred: ${e}`);
    // }
});
exports.GetFarmerDtl = (data) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query1 = `SELECT "SALE_DATE",D."Category_Name",E."Crop_Name",F."Variety_Name",B."CROP_CLASS",B."LOT_NUMBER",B."Receive_Unitcd",A."USER_TYPE" AS "Receive_Unitname",B."NO_OF_BAGS",      
        B."BAG_SIZE_KG",B."TOT_QTL",B."PRICE_QTL",B."ALL_IN_COST_AMOUNT",B."SUBSIDY_AMOUNT"       
        FROM "STOCK_DEALERSALEHDR" A       
        LEFT OUTER JOIN "STOCK_DEALERSALEDTL" B ON A."TRANSACTION_ID" = B."TRANSACTION_ID"       
        LEFT OUTER JOIN "mCropCategory" D ON B."CROPCATG_ID" = D."Category_Code"       
        LEFT OUTER JOIN "mCrop" E ON B."CROP_ID" = E."Crop_Code"       
        LEFT OUTER JOIN "mCropVariety" F ON B."CROP_VERID" = F."Variety_Code"         
        WHERE   A."TRANSACTION_ID" = $1`;
        const values1 = [data.TRANSACTION_ID];
        const response = await client.query(query1, values1);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }

});
exports.RptDateWiseSale = (data) => new Promise(async (resolve, reject) => {
    var saledetails = [];
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query1 = `SELECT "UPDATED_ON", "TRANSACTION_ID", "FARMER_ID", d."Category_Name", b."Crop_Name", c."Variety_Name", a."BAG_SIZE_KG", "NO_OF_BAGS", "TOT_QTL", "SUBSIDY_AMOUNT" FROM "STOCK_FARMER" a INNER JOIN "mCrop" b ON a."CROP_ID" = b."Crop_Code" INNER JOIN public."mCropVariety" c ON a."CROP_VERID" = c."Variety_Code" INNER JOIN public."mCropCategory" d ON a."CROPCATG_ID" = d."Category_Code" WHERE "UPDATED_BY" = $1 AND  "UPDATED_ON" >= $2  and  "UPDATED_ON" <= $3 order by "UPDATED_ON" ;`;
        const values1 = [data.LicNo, data.selectedFromDate, data.selectedToDate + ' ' + '23:59:59'];
        const response = await client.query(query1, values1);
        resolve(response.rows);
        // for (const e of response.rows) {
        // response.rows.forEach(async (e, key) => {
        //     const result = await sequelizeFarmerDB.query(`select VCHFARMERNAME from [dbo].[M_FARMER_REGISTRATION_API] where NICFARMERID=:FARMER_ID`, {
        //         replacements: { FARMER_ID: e.FARMER_ID }, type: sequelizeFarmerDB.QueryTypes.SELECT
        //     });
        //     // console.log(e);
        //     // console.log(result[0]);
        //     e.VCHFARMERNAME = result[0].VCHFARMERNAME;
        //     saledetails.push(e);
        //     // console.log(saledetails);
        //     if (key + 1 == response.rows.length) {
        //         resolve(saledetails);

        //     }
        // });
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.RptDateWiseSalewithFarmerdata = (data) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const promises = data.map(async (e) => {
            const result = await sequelizeStock.query(`SELECT VCHFARMERNAME FROM [FARMERDB].[dbo].[M_FARMER_REGISTRATION_API] WHERE NICFARMERID = :FARMER_ID`, {
                replacements: { FARMER_ID: e.FARMER_ID },
                type: sequelizeStock.QueryTypes.SELECT
            });
            e.VCHFARMERNAME = result[0].VCHFARMERNAME;

            return e;
        });
        Promise.all(promises)
            .then((saledetails) => {
                resolve(saledetails);
            })
            .catch((error) => {
                sequelizeStock.close();
                console.error("An error occurred:", error);
                reject(error);
            });
    } catch (e) {
        sequelizeStock.close();
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
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
var locConfigfarmerDB = dbConfigsql.locConfigfarmerDB;

exports.GETDISTCODEFROMLICNO = (LicNo) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
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
    } finally {
        client.release();
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
        await client.query('rollback');
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.getPreBookingDetails = (data) => new Promise(async (resolve, reject) => {
    if (data.SEASSION == 'K') {
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
        await client.query('rollback');
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
        await client.query('rollback');
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
        await client.query('rollback');
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.FillCrops = (data) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query1 = `SELECT C."Crop_Code" AS "CROP_CODE",C."Crop_Name" AS "CROP_NAME" FROM "STOCK_DEALERSTOCK" A 
        LEFT OUTER JOIN public."mCropCategory"  B ON A."CROPCATG_ID" = B."Category_Code"
        LEFT OUTER JOIN public."mCrop" C ON A."CROP_ID" = C."Crop_Code"
        WHERE A."LICENCE_NO" = $3 AND A."FIN_YR" = $1 AND A."SEASSION" = $2 AND C."IS_ACTIVE" = 1 
        AND A."VALIDITY" = 1 AND A."AVL_NO_OF_BAGS" > 0 
        GROUP BY C."Crop_Code",C."Crop_Name" ORDER BY C."Crop_Code",C."Crop_Name"`;
        const values1 = [data.FIN_YR, data.Seasons, data.LicNo];
        const response = await client.query(query1, values1);
        resolve(response.rows);
    } catch (e) {
        await client.query('rollback');
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
        console.log(query1, values1);
        resolve(response.rows);
    } catch (e) {
        await client.query('rollback');
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
        await client.query('rollback');
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
        await client.query('rollback');
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
        await client.query('rollback');
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.GetDistCodeByLicNo = (data) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const result = await sequelizeSeed.query(`SELECT DIST_CODE FROM [DAFPSEED].[DBO].SEED_LIC_DIST WHERE LIC_NO = :LICENCE_NO`, {//GAN/141088
            replacements: { LICENCE_NO: data.LICENCE_NO }, type: sequelizeStock.QueryTypes.SELECT
        });
        resolve(result[0].DIST_CODE);
    } catch (e) {
        await client.query('rollback');
        sequelizeSeed.close();
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.GetDAOCodeByLicNo = (data) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const result = await sequelizeSeed.query(`SELECT RIGHT(DAO_CD,2) as daocode FROM [DAFPSEED].[DBO].SEED_LIC_DIST WHERE LIC_NO = :LICENCE_NO`, {//GAN/141088
            replacements: { LICENCE_NO: data.LICENCE_NO }, type: sequelizeStock.QueryTypes.SELECT
        });
        resolve(result[0].daocode);
    } catch (e) {
        await client.query('rollback');
        sequelizeSeed.close();
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.GETFARMERINFO = (FarmerId) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
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
        await client.query('rollback');
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.InsertSaleDealer = (data) => new Promise(async (resolve, reject) => {

    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        console.log(data.SEASON);
        if (data.SEASON == 'Kharif') {
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
        var VARIETYAGE = 0;

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

        fFARMERID = await client.query(`select SUBSTRING('${data.FARMER_ID}',5,10) as farmerid`);
        FARMERLEN = await client.query(`SELECT length ('${data.FARMER_ID}') as farmeridlength`);
        if (FARMERLEN.rows[0].farmeridlength == 5 && fFARMERID == 0) {
            // 130-368code here
        }
        else {
            let YR_ = await client.query(`select SUBSTRING("FIN_YR",1,4) as yr from "mFINYR" where "IS_ACTIVE"=1`);
            YR = YR_.rows[0].yr
            let USER_TYPE_ = await client.query(`SELECT "USER_TYPE" FROM "STOCK_DEALERSTOCK" WHERE "LICENCE_NO" = '${data.LIC_NO}' limit 1`);
            USER_TYPE = USER_TYPE_.rows[0].USER_TYPE;
            MAXTRAN_NO = await client.query(`SELECT COALESCE(MAX(cast(SUBSTRING("TRANSACTION_ID", 18, 5) as int) ), 0)+1 AS max_value FROM "STOCK_DEALERSALEHDR" WHERE SUBSTRING("TRANSACTION_ID",1,2) = 'W${data.SEASON}' AND SUBSTRING("TRANSACTION_ID",3,2) = SUBSTRING('${data.FINYR}',3,2) AND SUBSTRING("TRANSACTION_ID",5,2) = '${data.DIST_CODE}' AND SUBSTRING("TRANSACTION_ID",7,2) = '${data.DAO_CD}' AND SUBSTRING("TRANSACTION_ID",9,2) = SUBSTRING('${data.LIC_NO}',10,2) AND SUBSTRING("TRANSACTION_ID",11,2) = SUBSTRING('${data.LIC_NO}',13,2)  AND SUBSTRING("TRANSACTION_ID",13,4) = SUBSTRING('${data.LIC_NO}',16,4)`);

            TRANSACTION_ID = 'W' + data.SEASON + YR.substring(2, 4) + data.DIST_CODE + data.DAO_CD + data.LIC_NO.substring(9, 11) + data.LIC_NO.substring(12, 14) + data.LIC_NO.substring(15, 19) + '-' + MAXTRAN_NO.rows[0].max_value.toString()
            // TRANSACTION_ID='WR23030217180006-14
            const query = `INSERT INTO public."STOCK_DEALERSALEHDR"(
                "SALE_DATE", "FARMER_ID", "LIC_NO", "TRANSACTION_ID", "TOT_SALE_AMOUNT", "TOT_SUB_AMOUNT_GOI", "TOT_SUB_AMOUNT_SP", "SEASON", "FIN_YEAR",  "UPDATED_BY", "UPDATED_ON", "USER_TYPE", "USERIP", "TRN_TYPE") values ($1, $2, $3, $4, $5, $6, $7, $8, $9, $10,$11, $12, $13, $14)`;
            const values = ['now()', data.FARMER_ID, data.LIC_NO, TRANSACTION_ID, 0, 0, 0, data.SEASON, data.FINYR, data.LIC_NO, 'now()', 'OSSC', data.ipAdress, 'W'];
            let insertintoSTOCKDEALERSALEHDR = await client.query(query, values);
            const query1 = `INSERT INTO public."Test1"("TRANSACTION_ID", "value") values ($1, $2)`;
            const values1 = [TRANSACTION_ID, data];
            insertintoTest1 = await client.query(query1, values1);
            var count = 0;
            if (insertintoSTOCKDEALERSALEHDR.rowCount == 1) {
                for (const e of data.VALUES) {
                    // data.VALUES.forEach(async (e, key) => {
                    count += 1
                    USER_TYPE = await client.query(`SELECT "USER_TYPE" FROM "STOCK_DEALERSTOCK" WHERE "LICENCE_NO" = '${data.LIC_NO}' AND "LOT_NO" = '${e.LOT_NO}' AND "AVL_NO_OF_BAGS" > 0 limit 1`);
                    let cropandclass = await client.query(`select "CropCatg_ID","Class" from "Stock_StockDetails" where "Lot_No" = '${e.LOT_NO}'  AND  "Crop_ID" = '${e.CROP_ID}'  AND "Crop_Verid" ='${e.CROP_VERID}' `);
                    mCROPCATG_ID = cropandclass.rows[0].CropCatg_ID
                    mCROP_CLASS = cropandclass.rows[0].Class;
                    MAX_SUBSIDY = await client.query(`select "MAX_SUBSIDY" from "mMAX_SUBSIDY" where "CROP_CODE"='${e.CROP_ID}' and "FIN_YEAR"='${data.FINYR}' and "SEASON"='${data.SEASON}' and "IS_ACTIVE"=1`);
                    if (fFARMERID == 0)
                        MAX_SUBSIDY = 0;
                    MAP_CODE = await client.query(`SELECT "MAP_CODE" FROM "Dist_CropMapping" WHERE "DIST_CODE" = '${data.DIST_CODE}' AND "CROP_CODE" =  '${e.CROP_ID}'  AND "SEASSION" ='${data.SEASON}' AND "FIN_YEAR" ='${data.FINYR}'`);
                    console.log(`SELECT "MAP_CODE" FROM "Dist_CropMapping" WHERE "DIST_CODE" = '${data.DIST_CODE}' AND "CROP_CODE" =  '${e.CROP_ID}'  AND "SEASSION" ='${data.SEASON}' AND "FIN_YEAR" ='${data.FINYR}'`);
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
                    let all_data = await client.query(`select "All_in_cost_Price","GOI_Subsidy","STATEPLAN_Subsidy","VARIETY_AFTER_10YEAR" from public."Stock_Pricelist" where "Crop_class"='${mCROP_CLASS}' and "RECEIVE_UNITCD"='${PRICE_RECEIVE_UNITCD.rows[0].PRICE_RECEIVE_UNITCD}' and "Crop_Vcode"='${e.CROP_VERID}' and "Crop_Code"='${e.CROP_ID}' and seasons='${data.SEASON}' and "F_Year"='${data.FINYR}' and "IS_ACTIVE"=1`);
                    ALL_IN_COST_AMOUNT = all_data.rows[0].All_in_cost_Price;
                    TOT_SUB_AMOUNT_GOI = all_data.rows[0].GOI_Subsidy;
                    TOT_SUB_AMOUNT_SP = all_data.rows[0].STATEPLAN_Subsidy;
                    VARIETY_AFTER_10YEAR = all_data.rows[0].VARIETY_AFTER_10YEAR;
                    AVL_BAGS = await client.query(`SELECT "AVL_NO_OF_BAGS" FROM "STOCK_DEALERSTOCK" WHERE "LICENCE_NO" = '${data.LIC_NO}' AND "CLASS" = '${mCROP_CLASS}' AND "CROP_ID" = '${e.CROP_ID}' AND "CROP_VERID" = '${e.CROP_VERID}' AND "LOT_NO" = '${e.LOT_NO}'  AND "VALIDITY" = 1`);
                    if (AVL_BAGS.rows[0].AVL_NO_OF_BAGS >= e.NO_OF_BAGS) {
                        let STOCK_FARMERSTOCK = await client.query(`SELECT * FROM "STOCK_FARMERSTOCK" WHERE "FARMER_ID" = '${data.FARMER_ID}' AND "Crop_Code" = '${e.CROP_ID}' AND "SEASON" = '${data.SEASON}' AND "FIN_YEAR" = '${data.FINYR}'`);
                        if (STOCK_FARMERSTOCK.rows.length == 0) {
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
                    console.log(ADMISSIBLE_SUBSIDY * TOT_SUB_AMOUNT_GOI, '1');
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
                        // mTOT_SUB_AMOUNT_GOI = GETSUBSIDYVALUE_.mTOT_SUB_AMOUNT_GOI;
                        // mTOT_SUB_AMOUNT_SP = GETSUBSIDYVALUE_.mTOT_SUB_AMOUNT_SP;
                        GOIQTY = GETSUBSIDYVALUE_.GOIQTY;
                        SPQTY = GETSUBSIDYVALUE_.SPQTY;
                        VARIETYAGE = parseInt(GETSUBSIDYVALUE_.VARIETYAGE);
                    }
                    if (SCHEME_CODE_GOI == 'OR7') {
                        mTOT_SUB_AMOUNT_SP = mTOT_SUB_AMOUNT_SP + mTOT_SUB_AMOUNT_GOI;
                        mTOT_SUB_AMOUNT_GOI = 0;
                        GOIQTY = 0;
                    }
                    //preboking or not

                    // select * from prebookinglist  where TRANSACTION_ID='WK23170315160038-57-2'

                    // select * from [STOCK_FARMER_2021-22_R] where TRANSACTION_ID like 'WK23170315160038-57-%'

                    // --select "All_in_cost_Price","GOI_Subsidy","STATEPLAN_Subsidy","VARIETY_AFTER_10YEAR" from "Stock_Pricelist" where  "Crop_Vcode"='V372'  and seasons='K' and "F_Year"='2023-24' and "IS_ACTIVE"=1

                    // select * from STOCK_DEALERSALEDTL where DTL_TRANSACTION_ID='WK23190219200005-1-1'

                    // select * from STOCK_DEALERSALEHDR where TRANSACTION_ID='WK23190219200005-1'
                    // }
                    const query3 = `INSERT INTO public."STOCK_DEALERSALEDTL"(
                        "TRANSACTION_ID", "DTL_TRANSACTION_ID", "CROPCATG_ID", "CROP_ID", "CROP_VERID", "CROP_CLASS", "LOT_NUMBER", "Receive_Unitcd", "PRICE_QTL", "BAG_SIZE_KG", "NO_OF_BAGS", "TOT_QTL", "ADMISSIBLE_SUBSIDY", "ALL_IN_COST_AMOUNT", "SCHEME_CODE_GOI", "TOT_SUB_AMOUNT_GOI", "SCHEME_CODE_SP", "TOT_SUB_AMOUNT_SP", "SUBSIDY_AMOUNT", "RELEASE_STATUS") values ($1, $2, $3, $4, $5, $6, $7, $8, $9, $10,$11, $12, $13, $14, $15, $16, $17, $18, $19, $20)`;
                    const values3 = [TRANSACTION_ID, TRANSACTION_ID + '-' + count, mCROPCATG_ID, e.CROP_ID, e.CROP_VERID, mCROP_CLASS, e.LOT_NO, e.Receive_Unitcd, e.PRICE_QTL, e.BAG_SIZE_KG, e.NO_OF_BAGS, e.QUANTITY, ADMISSIBLE_SUBSIDY, ALL_IN_COST_AMOUNT * e.QUANTITY, SCHEME_CODE_GOI, mTOT_SUB_AMOUNT_GOI, SCHEME_CODE_SP, mTOT_SUB_AMOUNT_SP, mTOT_SUB_AMOUNT_GOI + mTOT_SUB_AMOUNT_SP, 'P'];
                    let insertintoSTOCK_DEALERSALEDTL = await client.query(query3, values3);

                    const query4 = `INSERT INTO public."STOCK_FARMER"(
                        "FARMER_ID", "TRANSACTION_ID", "CROPCATG_ID", "CROP_ID", "CROP_VERID", "CROP_CLASS", "Receive_Unitcd", "LOT_NUMBER", "BAG_SIZE_KG", "NO_OF_BAGS", "TOT_QTL", "ADMISSIBLE_SUBSIDY",
                         "PRICE_QTL", "ALL_IN_COST_AMOUNT", "SCHEME_CODE_GOI","TOT_SUB_AMOUNT_GOI", "SCHEME_CODE_SP", "TOT_SUB_AMOUNT_SP", "SUBSIDY_AMOUNT", "SEASON", "FIN_YEAR", "UPDATED_BY", "UPDATED_ON", "USER_TYPE", "USERIP", "TRN_TYPE", "RECOVERY_AMT", "RECOVERY_DATE", "RECOVERY_STATUS", "GOI_QTY", "SP_QTY", "VARIETY_AGE") values ($1, $2, $3, $4, $5, $6, $7, $8, $9, $10,$11, $12, $13, $14, $15, $16, $17, $18, $19, $20,$21, $22, $23, $24, $25, $26, $27, $28, $29, $30,$31, $32)`;
                    const values4 = [data.FARMER_ID, TRANSACTION_ID + '-' + count, mCROPCATG_ID, e.CROP_ID, e.CROP_VERID, mCROP_CLASS, e.Receive_Unitcd, e.LOT_NO, e.BAG_SIZE_KG, e.NO_OF_BAGS, e.QUANTITY, ADMISSIBLE_SUBSIDY,
                    e.PRICE_QTL, ALL_IN_COST_AMOUNT * e.QUANTITY, SCHEME_CODE_GOI, mTOT_SUB_AMOUNT_GOI, SCHEME_CODE_SP, mTOT_SUB_AMOUNT_SP, mTOT_SUB_AMOUNT_GOI + mTOT_SUB_AMOUNT_SP, data.SEASON, data.FINYR, data.LIC_NO, 'now()', 'OSSC', data.ipAdress, 'W', mTOT_SUB_AMOUNT_GOI + mTOT_SUB_AMOUNT_SP, 'now()', '0', GOIQTY, SPQTY, VARIETYAGE];

                    let insertintoSTOCK_FARMER = await client.query(query4, values4);

                    let updateinSTOCK_FARMERSTOCK = await client.query(`update "STOCK_DEALERSTOCK" set "AVL_NO_OF_BAGS"="AVL_NO_OF_BAGS"-${e.NO_OF_BAGS},"AVL_QUANTITY"="AVL_QUANTITY"-${e.QUANTITY} where "LICENCE_NO"= '${data.LIC_NO}' and "CLASS"='${mCROP_CLASS}' and "CROP_VERID"='${e.CROP_VERID}' and "LOT_NO"='${e.LOT_NO}' and "VALIDITY"=1`);
                    if (data.PrebookingorNot) {
                        PREBOOKING_AMT = (ALL_IN_COST_AMOUNT * e.QUANTITY);
                        PREBOOKING_AMT1 += PREBOOKING_AMT;
                        NO_OF_BAGS += e.NO_OF_BAGS;
                        let updateinSTOCK_FARMER = await client.query(`UPDATE "STOCK_FARMER" SET "PREBOOKING_AMT"=${PREBOOKING_AMT1}/10, "PREBOOKING_APPLICATIONID"='${data.applicationId}'  WHERE "TRANSACTION_ID" ='${TRANSACTION_ID + '-' + count}' `);
                    }
                    if (count == data.VALUES.length) {
                        let alldata = await client.query(`select COALESCE(sum("ALL_IN_COST_AMOUNT"),0) as "TOT_SALE_AMOUNT" ,COALESCE(sum("TOT_SUB_AMOUNT_GOI"),0) as "TOT_SUB_AMOUNT_GOI", COALESCE(sum("TOT_SUB_AMOUNT_SP"),0) as "TOT_SUB_AMOUNT_SP" from "STOCK_DEALERSALEDTL" WHERE "TRANSACTION_ID" = '${TRANSACTION_ID}'`);

                        let updateinSTOCK_FARMERSTOCK = await client.query(`UPDATE "STOCK_DEALERSALEHDR" SET "TOT_SALE_AMOUNT" ='${alldata.rows[0].TOT_SALE_AMOUNT}' ,"TOT_SUB_AMOUNT_GOI" = '${alldata.rows[0].TOT_SUB_AMOUNT_GOI}',"TOT_SUB_AMOUNT_SP" = '${alldata.rows[0].TOT_SUB_AMOUNT_SP}' WHERE "TRANSACTION_ID" ='${TRANSACTION_ID}' `);
                        if (data.PrebookingorNot) {
                            let updateprebookinglist = await client.query(`update prebookinglist set "TRANSACTION_ID"='${TRANSACTION_ID}',"noofBagSale"='${NO_OF_BAGS}',"saleAmount"=${PREBOOKING_AMT1}/10,"IS_ACTIVE"=0 where "applicationID"='${data.applicationId}' `);
                            let updateinSTOCK_FARMERSTOCK_forfarmerbooking = await client.query(`UPDATE "STOCK_DEALERSALEHDR" SET "PREBOOKING_AMT"=${PREBOOKING_AMT1}/10, "PREBOOKING_APPLICATIONID"='${data.applicationId}'  WHERE "TRANSACTION_ID" ='${TRANSACTION_ID}' `);
                        }
                        resolve({ "result": 'True', "TRANSACTION_ID": TRANSACTION_ID });
                    }

                }
                // });
            }
        }
    } catch (e) {
        await client.query('rollback');
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
        await client.query('rollback');
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
        var GOIQTY = 0;
        var SPQTY = 0;

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
        await client.query('rollback');
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }

});
exports.getCurrentstockDetails = (LIC_NO) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query1 = `SELECT d."Category_Code",d."Category_Name",c."Crop_Code",c."Crop_Name",B."Variety_Code",B."Variety_Name","CLASS",SUM("STOCK_QUANTITY")"STOCK_QUANTITY",SUM("AVL_QUANTITY")"AVL_QUANTITY" FROM "STOCK_DEALERSTOCK" A 
        LEFT OUTER JOIN "mCropVariety" B ON A."CROP_VERID" = B."Variety_Code" 
        LEFT OUTER JOIN "mCrop" C ON B."Crop_Code" = C."Crop_Code" 
        LEFT OUTER JOIN "mCropCategory" D ON C."Category_Code" = D."Category_Code" 
        INNER JOIN "mFINYR" E ON A."FIN_YR" = E."FIN_YR" AND E."IS_ACTIVE" = 1 
        INNER JOIN "mSEASSION" F ON A."SEASSION" = F."SHORT_NAME" AND F."IS_ACTIVE" = 1 
        WHERE A."LICENCE_NO" = $1
                    GROUP BY d."Category_Code",d."Category_Name",c."Crop_Code",c."Crop_Name",B."Variety_Code",B."Variety_Code",B."Variety_Name","CLASS" order by d."Category_Name",c."Crop_Name"`;
        const values1 = [LIC_NO];
        const response = await client.query(query1, values1);
        resolve(response.rows);
    } catch (e) {
        await client.query('rollback');
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.getPaymentResponse = (data) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `SELECT A."FARMER_ID",A."TRANSACTION_ID",A."UPDATED_ON",l."Category_Name",E."Crop_Name",F."Variety_Name",a."BAG_SIZE_KG",a."NO_OF_BAGS",A."TOT_QTL",A."TOT_SUB_AMOUNT_GOI",A."TOT_SUB_AMOUNT_SP",A."SUBSIDY_AMOUNT"  FROM "STOCK_FARMER" A        
        INNER JOIN public."mCropCategory" l ON A."CROPCATG_ID" = l."Category_Code"    
        INNER JOIN public."mCrop" E ON A."CROP_ID" = E."Crop_Code"        
        INNER JOIN public."mCropVariety" F ON A."CROP_VERID" = F."Variety_Code"   
            where ( a."UPDATED_ON">=$1) and (a."UPDATED_ON"<=$2)  
          ORDER BY a."UPDATED_ON",A."FARMER_ID" limit 50  `;
        const values = [data.selectedFromDate, data.selectedToDate];
        const response = await client.query(query, values);
        resolve(response.rows);
    } catch (e) {
        await client.query('rollback');
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.getpaymentResponseWithPgFarmerID = (data) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const promises = data.map(async (e) => {
            const result = await sequelizeSeed.query(`select G.VCHFARMERNAME,h.[vch_DistrictName],i.vch_blockname,j.vch_gpname,k.vch_villagename,REPLICATE('*',LEN(B.[BankPost_Office_Account_number])-4)+RIGHT(B.[BankPost_Office_Account_number],4) as BankPost_Office_Account_number,B.Bank_Post_Office_Name,B.Bank_Post_Office_Branch,
                 CASE WHEN (C.record_status='ACCP') THEN 'PAID' WHEN (C.record_status='RJCT')  THEN 'NOT PAID' Else 'Pending'   END  as Status ,CASE WHEN (C.record_status='ACCP') THEN '' WHEN (C.record_status='RJCT') THEN c.rejection_narration   END  as Reject_Reason,CASE WHEN b.unique_credit_transaction_id IS NULL THEN 'Payment File is Under Process' when c.Unique_Credit_Transaction_Id  is null then 'Sent to Bank' when n.Original_End_to_End_Id is null then 'Payment File Pending at PFMS' end as pendingat  from [FARMERDB].dbo.M_FARMER_REGISTRATION  g   
                inner join farmerdb.[dbo].[PDS_DISTRICTMASTER] h on h.[int_DistrictID]=g.vchdistid  collate Latin1_General_CI_AI        
                inner join farmerdb.[dbo].[PDS_BLOCKMASTER] i on i.[int_blockID]=g.vchblockid   collate Latin1_General_CI_AI      
                inner join farmerdb.[dbo].[PDS_GPMASTER] j on j.[int_gpid]=g.vchgpid collate Latin1_General_CI_AI      
                inner join farmerdb.[dbo].[PDS_VILLAGEMASTER] k on k.[int_villageid]=g.vchvillageid collate Latin1_General_CI_AI  
                LEFT join farmerdb.[dbo].[Request_tbl_Payment_List_Rabi] b on :TRANSACTION_ID=left(b.unique_credit_transaction_id,CHARINDEX('O', b.unique_credit_transaction_id)-1) collate Latin1_General_CI_AI and b.unique_credit_transaction_id like '%O%'        
                LEFT JOIN farmerdb.dbo.Response_tbl_Paymemt_Ack_Message_Rabi n on n.Original_End_to_End_Id=b.Unique_Credit_Transaction_Id      
                LEFT join farmerdb.[dbo].[Response_tbl_Payment_Authorization_Message_Rabi] c on b.Unique_Credit_Transaction_Id=c.Unique_Credit_Transaction_Id        
                LEFT join farmerdb.[dbo].[Request_tbl_Payment_Message_Rabi] d on b.Unique_Message_Id=d.Unique_Message_Id  collate Latin1_General_CI_AI  
                where  NICFARMERID = :FARMER_ID`, {
                replacements: { FARMER_ID: e.FARMER_ID, TRANSACTION_ID: e.TRANSACTION_ID },
                type: sequelizeSeed.QueryTypes.SELECT
            });
            e.VCHFARMERNAME = result[0].VCHFARMERNAME;
            e.vch_DistrictName = result[0].vch_DistrictName;
            e.vch_blockname = result[0].vch_blockname;
            e.vch_gpname = result[0].vch_gpname;
            e.vch_villagename = result[0].vch_villagename;
            e.BankPost_Office_Account_number = result[0].BankPost_Office_Account_number;
            e.Bank_Post_Office_Name = result[0].Bank_Post_Office_Name;
            e.Bank_Post_Office_Branch = result[0].Bank_Post_Office_Branch;
            e.pendingat = result[0].pendingat;
            e.status = result[0].Status;
            return e;
        });

        Promise.all(promises)
            .then((saledetails) => {
                resolve(saledetails);
            })
            .catch((error) => {
                console.error("22An error occurred:", error);
                sequelizeSeed.close();
                reject(error);
            });
    } catch (e) {
        await client.query('rollback');
        sequelizeSeed.close();
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.GetDistCodeFromDist = (data) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const result = await sequelizeSeed.query(`select Short_Name from [dafpSeed].[DBO].dist where dist_code =:userid`, {
            replacements: { userid: data.distCode }, type: sequelizeSeed.QueryTypes.SELECT
        });
        resolve(result[0]);
    } catch (e) {
        await client.query('rollback');
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.paymentStatusByFarmeId = (data) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    var con = new sqlstock.ConnectionPool(locConfig_stock);
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
        await client.query('rollback');
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.allFillFinYr = () => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query1 = `SELECT "FIN_YR" FROM "mFINYR"`;
        const values1 = [];
        const response = await client.query(query1, values1);
        resolve(response.rows);
    } catch (e) {
        await client.query('rollback');
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});

exports.FillCropCategory = () => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query1 = `SELECT "Category_Code","Category_Name","IS_ACTIVE" FROM "mCropCategory" WHERE "IS_ACTIVE" = 'true' ORDER BY "Category_Name"`;
        const values1 = [];
        const response = await client.query(query1, values1);
        resolve(response.rows);
    } catch (e) {
        await client.query('rollback');
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.FillCropByCategoryId = (SelectedCropCatagory) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `SELECT "Crop_Code","Crop_Name" FROM "mCrop" WHERE "Category_Code" = $1 AND "IS_ACTIVE" = '1' ORDER BY "Crop_Name" ASC`;
        const values = [SelectedCropCatagory];
        const response = await client.query(query, values);
        resolve(response.rows);
    } catch (e) {
        await client.query('rollback');
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.fillGodownwisestock = (data) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = ` SELECT DISTINCT SD."Dist_Code", "Dist_Name",SSD."Crop_Verid",SCM."Variety_Name",SUM("Avl_Quantity") AS "STOCK"      
        FROM "Stock_District" SD      
        inner JOIN "Stock_StockDetails" SSD ON SD."Dist_Code"=SSD."Dist_Code"      
        LEFT JOIN "mCropVariety" SCM ON SCM."Variety_Code"=SSD."Crop_Verid"      
        WHERE SSD."FIN_YR"=$1    
        AND SSD."User_Type"='OSSC'     
       AND SSD."CropCatg_ID"=$2      
       AND SSD."Crop_ID"=$3     
       and  SD."Dist_Code" ::text=$4 and ssd."SEASSION_NAME" ::text=$5   
        GROUP BY SD."Dist_Code", "Dist_Name",SSD."Crop_Verid",SCM."Variety_Name"   
        ORDER BY SCM."Variety_Name"`;
        const values = [data.SelectedFinancialYear, data.SelectedCropCatagory, data.SelectedCrop, data.DIST_CODE, data.SelectedSeason];
        const response = await client.query(query, values);
        resolve(response.rows);
    } catch (e) {
        await client.query('rollback');
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.GetDealerInfo = (LIC_NO) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    var con = new sqlstock.ConnectionPool(locConfig_dafpSeeds);
    try {
        con.connect().then(function success() {
            const request = new sqlstock.Request(con);
            request.input('LIC_NO', LIC_NO);
            request.execute('sp_GETDEALERINFO', function (err, result) {
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
        await client.query('rollback');
        sqlstock.close();
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.CntLic = (LIC_NO) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const result = await sequelizeSeed.query(`SELECT COUNT(*)Cnt FROM [DAFPSEED].[DBO].SEED_LIC_DIST WHERE LIC_NO = :LIC_NO AND ACC_NO IS NULL`, {
            replacements: { LIC_NO: LIC_NO },
            type: sequelizeSeed.QueryTypes.SELECT
        });
        if (result[0].Cnt == 0) {
            console.log('entry');
            const result1 = await sequelizeSeed.query(`select COUNT(*) Cnt from dafpseed.dbo.seed_lic_dist a inner join farmerdb.dbo.M_FARMER_REGISTRATION b on a.dealer_code=b.vchfarmercode collate SQL_Latin1_General_CP1_CI_AS left join farmerdb.[dbo].[Response_tbl_Beneficiary_Ack_Message] c on b.vchfarmercode=c.scheme_specific_id  where  b.chapfmsstatus is not null and c.scheme_specific_id not in(select  Farmer_Code from farmerdb.dbo.Tbl_FarmerApprove) and (null IS NULL OR A.DIST_CODE=  null) AND (:LIC_NO IS NULL OR A.LIC_NO = :LIC_NO)`, {
                replacements: { LIC_NO: LIC_NO },
                type: sequelizeSeed.QueryTypes.SELECT
            });
            resolve(result1[0]);
        }
        else {
            resolve(result[0]);
        }
    } catch (e) {
        await client.query('rollback');
        sequelizeSeed.close();
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.FillBank = () => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    var con = new sqlstock.ConnectionPool(locConfig_dafpSeeds);
    try {
        con.connect().then(function success() {
            const request = new sqlstock.Request(con);
            request.execute('sp_FILLBANK', function (err, result) {
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
        await client.query('rollback');
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.FillBranchName = (BnkCode) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    var con = new sqlstock.ConnectionPool(locConfig_dafpSeeds);
    try {
        con.connect().then(function success() {
            const request = new sqlstock.Request(con);
            request.input('BnkCode', BnkCode);
            request.execute('sp_FILLBRANCH', function (err, result) {
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
        await client.query('rollback');
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.FillIFSC = (BnchId) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    var con = new sqlstock.ConnectionPool(locConfig_dafpSeeds);
    try {
        con.connect().then(function success() {
            const request = new sqlstock.Request(con);
            request.input('BnchId', BnchId);
            request.execute('sp_FILLIFSC', function (err, result) {
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
        await client.query('rollback');
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.UpdateDealerBankDetails = (data) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    var con = new sqlstock.ConnectionPool(locConfig_dafpSeeds);
    try {
        con.connect().then(function success() {
            const request = new sqlstock.Request(con);
            request.input('LIC_NO', data.userid);
            request.input('AADHAAR_NO', data.AADHAAR_NO);
            request.input('ACC_HOLDERNAME', data.ACC_HOLDERNAME);
            request.input('ACC_NO', data.ACC_NO);
            request.input('BANK_ID', data.BANK_ID);
            request.input('BRANCH_ID', data.BRANCH_ID);
            request.input('IFSC_CODE', data.IFSC_CODE);
            request.input('BANK_UPDATED_BY', data.userid);
            request.output('VAL');
            request.execute('sp_UPDBANKDETAILS', function (err, result) {
                if (err) {
                    console.log('An error occurred...', err);
                }
                else {
                    resolve(result.output)
                }
                con.close();
            });
        }).catch(function error(err) {
            sqlstock.close();
            console.log('An error occurred...', err);
        });

    } catch (e) {
        await client.query('rollback');
        sqlstock.close();
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.FillPrebooking = (beneficiaryType, LIC_NO1) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `SELECT C."Crop_Name",B."Variety_Name" ,sum(CAST (A.quantity AS DOUBLE PRECISION))/100 as "QUANTITY"
        FROM "prebookinglist" A  
        INNER JOIN "mCropVariety" B ON A."varietyCode" = B."Variety_Code"  
        INNER JOIN "mCrop" C ON B."Crop_Code" = C."Crop_Code"  
        WHERE A."dealerId"=$1 AND  A."beneficiaryType"=$2
        AND A."IS_ACTIVE"=1 GROUP BY C."Crop_Name",B."Variety_Name"`;
        const values = [LIC_NO1, beneficiaryType];
        const response = await client.query(query, values);
        resolve(response.rows);
    } catch (e) {
        await client.query('rollback');
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});

async function myTask() {
    try {
        // Wait for the connection to resolve
        const client = await pool.connect();

        const query = `select * from "STOCK_FARMER" where "UPDATED_ON"  <= CURRENT_TIMESTAMP - INTERVAL '60 minutes' and "updatedInSql" is null order by "UPDATED_ON" limit 10`;

        const values = [];

        // Use the resolved connection object to execute the query
        const response = await client.query(query, values);

        if (response.rows.length > 0) {
            response.rows.forEach(async element => {
                const result = await sequelizeSeed.query(`select * from [STOCK_FARMER_2021-22_R] where TRANSACTION_ID='${element.TRANSACTION_ID}'`, {
                    replacements: {}, type: sequelizeSeed.QueryTypes.SELECT
                });
                if (result.length == 0) {
                    const insertintoStock_farmer_sql = await sequelizeSeed.query(`INSERT INTO [dbo].[STOCK_FARMER_2021-22_R]
                    ([FARMER_ID],[TRANSACTION_ID],[CROPCATG_ID],[CROP_ID]
                    ,[CROP_VERID],[CROP_CLASS],[Receive_Unitcd],[LOT_NUMBER],[BAG_SIZE_KG],[NO_OF_BAGS],[TOT_QTL],[ADMISSIBLE_SUBSIDY]
                    ,[PRICE_QTL],[ALL_IN_COST_AMOUNT],[SCHEME_CODE_GOI],[TOT_SUB_AMOUNT_GOI],[SCHEME_CODE_SP],[TOT_SUB_AMOUNT_SP]
                    ,[SEASON],[FIN_YEAR],[UPDATED_BY],[UPDATED_ON],[USER_TYPE],[USERIP],[TRN_TYPE],[XML_Status],[RECOVERY_AMT]
                    ,[RECOVERY_DATE],[RECOVERY_STATUS],[GOI_QTY],[SP_QTY],[VARIETY_AGE],[PREBOOKING_AMT],[PREBOOKING_APPLICATIONID])
                    VALUES(:FARMER_ID,:TRANSACTION_ID,:CROPCATG_ID
                    ,:CROP_ID,:CROP_VERID,:CROP_CLASS,:Receive_Unitcd,:LOT_NUMBER
                    ,:BAG_SIZE_KG,:NO_OF_BAGS,:TOT_QTL,:ADMISSIBLE_SUBSIDY,:PRICE_QTL
                    ,:ALL_IN_COST_AMOUNT,:SCHEME_CODE_GOI,:TOT_SUB_AMOUNT_GOI,:SCHEME_CODE_SP,:TOT_SUB_AMOUNT_SP
                    ,:SEASON,:FIN_YEAR,:UPDATED_BY,:UPDATED_ON,:USER_TYPE 
                    ,:USERIP,:TRN_TYPE,:XML_Status,:RECOVERY_AMT,:RECOVERY_DATE,:RECOVERY_STATUS
                    ,:GOI_QTY,:SP_QTY,:VARIETY_AGE,:PREBOOKING_AMT,:PREBOOKING_APPLICATIONID)`, {
                        replacements: {
                            FARMER_ID: element.FARMER_ID, TRANSACTION_ID: element.TRANSACTION_ID, CROPCATG_ID: element.CROPCATG_ID, CROP_ID: element.CROP_ID, CROP_VERID: element.CROP_VERID, CROP_CLASS: element.CROP_CLASS, Receive_Unitcd: element.Receive_Unitcd,
                            LOT_NUMBER: element.LOT_NUMBER, BAG_SIZE_KG: element.BAG_SIZE_KG, NO_OF_BAGS: element.NO_OF_BAGS, TOT_QTL: element.TOT_QTL, ADMISSIBLE_SUBSIDY: element.ADMISSIBLE_SUBSIDY, PRICE_QTL: element.PRICE_QTL, ALL_IN_COST_AMOUNT: element.ALL_IN_COST_AMOUNT, SCHEME_CODE_GOI: element.SCHEME_CODE_GOI, TOT_SUB_AMOUNT_GOI: element.TOT_SUB_AMOUNT_GOI, SCHEME_CODE_SP: element.SCHEME_CODE_SP,
                            TOT_SUB_AMOUNT_SP: element.TOT_SUB_AMOUNT_SP, SEASON: element.SEASON, FIN_YEAR: element.FIN_YEAR, UPDATED_BY: element.UPDATED_BY, UPDATED_ON: element.UPDATED_ON.toISOString(),
                            USER_TYPE: element.USER_TYPE, USERIP: element.USERIP, TRN_TYPE: element.TRN_TYPE, XML_Status: element.XML_Status, RECOVERY_AMT: element.RECOVERY_AMT, RECOVERY_DATE: element.RECOVERY_DATE.toISOString(),
                            RECOVERY_STATUS: element.RECOVERY_STATUS, GOI_QTY: element.GOI_QTY, SP_QTY: element.SP_QTY, VARIETY_AGE: element.VARIETY_AGE, PREBOOKING_AMT: element.PREBOOKING_AMT, PREBOOKING_APPLICATIONID: element.PREBOOKING_APPLICATIONID
                        }, type: sequelizeSeed.QueryTypes.SELECT
                    });

                    const update__STOCK_FARMER_Query = `UPDATE "STOCK_FARMER" SET "updatedInSql" = 1  where "TRANSACTION_ID"='${element.TRANSACTION_ID}'`;

                    const update__STOCK_FARMER_Values = [];

                    const response = await client.query(update__STOCK_FARMER_Query, update__STOCK_FARMER_Values);
                }
            });
        }


        // Release the connection back to the pool
        client.release();

    } catch (error) {
        console.error('Error:', error);
        // Handle the error as needed
    }
}
async function update_xmlstatus() {
    try {
        const client = await pool.connect();
        const result = await sequelizeSeed.query(`select  TRANSACTION_ID from [STOCK_FARMER_2021-22_R] where "FIN_YEAR"='2023-24' and SEASON='R' and XML_Status is not null  and updatedinpgsql is null AND DATEDIFF(DAY, UPDATED_ON, GETDATE()) <= 135;`, {
            replacements: {}, type: sequelizeSeed.QueryTypes.SELECT
        });
        let transactionIds = result.map(pair => pair.TRANSACTION_ID);
        if (transactionIds.length > 0) {
            const query = ` update "STOCK_FARMER" set "XML_Status"=1 WHERE "TRANSACTION_ID" IN (${transactionIds.map(id => `'${id}'`).join(', ')});`;
            const values = [];
            const response = await client.query(query, values);
            console.log(`update  [STOCK_FARMER_2021-22_R] set updatedinpgsql=1 WHERE updatedinpgsql is null and  "TRANSACTION_ID" IN (${transactionIds.map(id => `'${id}'`).join(', ')});`);
            const result1 = await sequelizeSeed.query(`update  [STOCK_FARMER_2021-22_R] set updatedinpgsql=1 WHERE updatedinpgsql is null and  "TRANSACTION_ID" IN (${transactionIds.map(id => `'${id}'`).join(', ')});`, {
                replacements: {}, type: sequelizeSeed.QueryTypes.SELECT
            });
        }

        client.release();

    } catch (error) {
        console.error('Error:', error);
    }
}
// Set up a scheduler to run the task every 5 seconds (5000 milliseconds)
const intervalId = setInterval(myTask, 5000);
const updatexmlstatus = setInterval(update_xmlstatus, 5000);
exports.rejectedBankDetails = (LIC_NO) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {

        const result1 = await sequelizeSeed.query(`select  a.lic_no, a.ACC_HOLDERNAME,BANK_ID,BRANCH_ID,IFSC_CODE, DIST_CODE, CASE APP_TYPE WHEN 'Secretary PACS' THEN 'PACS' ELSE 'Dealer' END APP_TYPE, b.VCHACCOUNTNO , c.bank_account_number , c.bank_post_office_branch,a.AADHAAR_NO, case when c.beneficiary_status = 'RJCT' then 
        c.rejection_reason_narration when c.Scheme_Specific_ID is null then 'Invalid Bank Details' else 'account holder name mismatch' end RJCT_REASON from dafpseed.dbo.seed_lic_dist a inner join farmerdb.dbo.M_FARMER_REGISTRATION b on a.dealer_code=b.vchfarmercode collate SQL_Latin1_General_CP1_CI_AS left join farmerdb.[dbo].[Response_tbl_Beneficiary_Ack_Message] c on b.vchfarmercode=c.scheme_specific_id  where  b.chapfmsstatus is not null and c.scheme_specific_id not in(select  Farmer_Code from farmerdb.dbo.Tbl_FarmerApprove) and (null IS NULL OR A.DIST_CODE=  null) AND (:LIC_NO IS NULL OR A.LIC_NO = :LIC_NO)`, {
            replacements: { LIC_NO: LIC_NO },
            type: sequelizeSeed.QueryTypes.SELECT
        });
        resolve(result1[0]);


    } catch (e) {
        await client.query('rollback');
        sequelizeSeed.close();
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.UpdatetheBankDetails = (data) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    var con = new sqlstock.ConnectionPool(locConfig_dafpSeeds);
    try {
        con.connect().then(function success() {
            const request = new sqlstock.Request(con);
            request.input('LIC_NO', data.userid);
            request.input('ACC_HOLDERNAME', data.ACC_HOLDERNAME);
            request.input('ACC_NO', data.ACC_NO);
            request.input('BANK_ID', data.BANK_ID);
            request.input('BRANCH_ID', data.BRANCH_ID);
            request.input('IFSC_CODE', data.IFSC_CODE);
            request.input('BANK_UPDATED_BY', data.userid);
            request.output('VAL');
            request.execute('sp_UPDBANKDETAILS1', function (err, result) {
                if (err) {
                    console.log('An error occurred...', err);
                }
                else {
                    resolve(result.output)
                }
                con.close();
            });
        }).catch(function error(err) {
            sqlstock.close();
            console.log('An error occurred...', err);
        });

    } catch (e) {
        await client.query('rollback');
        sqlstock.close();
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
//scedular end
// select * from "STOCK_DEALERSALEHDR"

// select * from "STOCK_FARMERSTOCK"

// select * from "STOCK_DEALERSALEDTL"

// select * from "STOCK_FARMER" where "UPDATED_ON" >'2023-07-27 10:10:05.277+05:30'

// delete from "STOCK_DEALERSALEHDR"

// delete from "STOCK_FARMERSTOCK"

// delete from "STOCK_DEALERSALEDTL"

// delete from "STOCK_FARMER" where "UPDATED_ON" >'2023-07-27 10:10:05.277+05:30'