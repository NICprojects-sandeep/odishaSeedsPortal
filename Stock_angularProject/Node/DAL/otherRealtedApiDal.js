const pool = require('../config/dbConfig');
var dbConfig = require('../config/dbSqlConnection');
var sqlstock = dbConfig.sqlstock;
var sequelizeSeed = dbConfig.sequelizeSeed;
var sequelizeStock = dbConfig.sequelizeStock;
const format = require('pg-format');
const request = require('request');

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
        const query = `SELECT distinct a."applicationID", case when b."UPDATED_ON" is not null then b."UPDATED_ON" else c."UPDATED_ON" end as "date",
        case when b."UPDATED_BY" is not null then b."UPDATED_BY" else c."UPDATED_BY" end as "delerid",a."TRANSACTION_ID" as "txnid" from "prebookinglist" a
                left join "STOCK_FARMER" b on a."applicationID"= b."PREBOOKING_APPLICATIONID"
                left join "Stock_SaleDetails" c on a."applicationID"= c."PREBOOKING_APPLICATIONID" 
        where a."TRANSACTION_ID" is not null AND (b."UPDATED_ON" BETWEEN $1 AND $2 OR c."UPDATED_ON" BETWEEN $1 AND $2)
        order by date`;
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
            replacements: { LICENCE_NUMBER: LICENCE_NUMBER }, type: sequelizeStock.QueryTypes.SELECT
        });
        resolve(totalcollectorder);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally { // Close the connection after the promise is resolved or rejected
    }
});
exports.NotupdatedeRUPIVoucher = (time) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        let timeinterval = time + ' minutes';
        const query = `select "voucherId","TRANSACTION_ID" from public."eRUPIDetails" WHERE status='200' and current_timestamp - updatedon >= interval '${timeinterval}' and  ("redeemStatus" != '200' OR "redeemstatusId" !='5' or "redeemStatus" is null) and  rejected is null and ( rejectedmsg is null or rejectedmsg ='Voucher is not initiation mode')`;
        const response = await client.query(query);
        resolve(response.rows);
    } catch (e) {
        console.log(e);
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.updateeRUPIVoucher = async (data) => {
    const client = await pool.connect().catch((err) => { console.log(`Unable to connect to the database: ${err}`); });
    try {
        const query = `UPDATE "eRUPIDetails" SET "redeemStatus"=$3, "redeemstatusId"=$4, "redeemstatusMsg"=$5, redeemmsg=$6, "redeemInsertedOn"=$7  where "TRANSACTION_ID"=$2 and "voucherId"=$1;`;
        const values = [data.voucherId, data.TRANSACTION_ID, data.status, data.result.statusId, data.result.statusMsg, data.msg, 'now()'];
        const response = await client.query(query, values);
    } catch (e) {
        console.log(`Oops! An error occurred: ${e}`);
    } finally {
        client.release();
    }
};
exports.anotherTimeeRUPIVoucherCreated = (time) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        let timeinterval = time + ' minutes';

        const query = `select id,"TRANSACTION_ID","districtId","dealerCode","olddealerCode","dealerName","dealerMobileNo","farmerName","farmerMobileNo","farmerId","payableAmtFarmer","voucherId","count" from public."eRUPIDetails" WHERE status='200' and current_timestamp - updatedon >= interval '${timeinterval}' and rejected is null and  ("redeemStatus" != '200' OR "redeemstatusId" != '5' and  "redeemstatusMsg" !='Voucher Revoked' and rejectedmsg is null)`;
        const response = await client.query(query);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.inserteRUPIVoucher = async (data) => {
    const client = await pool.connect().catch((err) => { console.log(`Unable to connect to the database: ${err}`); });
    try {
        const query1 = `UPDATE "eRUPIDetails" SET rejectedon=$1,rejected=0,rejectedmsg=$5  where "TRANSACTION_ID"=$2 and "voucherId"=$3 and id= $4;`;
        const values1 = ['now()', data.TRANSACTION_ID, data.oldVoucherId, data.id, data.revokemsg];
        const response = await client.query(query1, values1);



        const FIN_YR = `select "FIN_YR" from "mFINYR" where "IS_ACTIVE"=1`;
        const response_FIN_YR = await client.query(FIN_YR);
        const SEASSION = `select "SHORT_NAME" from "mSEASSION" where "IS_ACTIVE"=1`;
        const response_SEASSION = await client.query(SEASSION);

        const count = `select count,rejected from "eRUPIDetails" WHERE  "TRANSACTION_ID"=$1 order by insertedon desc limit 1`;
        const values_count = [data.TRANSACTION_ID];
        const response_count = await client.query(count, values_count);
        if (parseInt(response_count.rows[0].count) < 3) {
            console.log(response_count.rows[0].rejected,response_count.rows[0]);
            if (response_count.rows[0].rejected == 0) {
                const query = `insert into "eRUPIDetails" ( "TRANSACTION_ID", "districtId", "dealerCode", "olddealerCode", "dealerName", "dealerMobileNo", "farmerName", "farmerMobileNo", "farmerId", "payableAmtFarmer",  status, "FinYear", season, insertedon,"voucherId",updatedon, msg,count ) values ($1, $2, $3, $4, $5, $6, $7, $8, $9, $10,$11,$12,$13,$14,$15,$16,$17,$18)`;
                const values = [data.TRANSACTION_ID, data.districtId, data.dealerCode, data.olddealerCode, data.dealerName, data.dealerMobileNo, data.farmerName, data.farmerMobileNo, data.farmerId, data.payableAmtFarmer.toFixed(2), data.status, response_FIN_YR.rows[0].FIN_YR, response_SEASSION.rows[0].SHORT_NAME, 'now()', data.voucherId, 'now()', data.msg, parseInt(response_count.rows[0].count) + 1];
                await client.query(query, values);
            }

        }
        else {
            const query = `select b.* from public."STOCK_DEALERSALEDTL" a
            inner join "STOCK_FARMER" b on a."DTL_TRANSACTION_ID"= b."TRANSACTION_ID"
            inner join "mCropVariety" c on a."CROP_VERID" = c."Variety_Code"
            where a."TRANSACTION_ID"='${data.TRANSACTION_ID}'`;

            const values = [];

            // cooment mytask function in farmersaledal
            let response = await client.query(query, values);
            response.rows.forEach(async element => {
                const result = await sequelizeSeed.query(`select * from [STOCK_FARMER_2021-22_R] where TRANSACTION_ID='${element.TRANSACTION_ID}'`, {
                    replacements: {}, type: sequelizeSeed.QueryTypes.SELECT
                });
                if (result.length == 0) {
                    console.log('msg sent to farmer');
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
                    //send sms
                    var otp = Math.floor(100000 + Math.random() * 900000)
                    const date = new Date(element.UPDATED_ON);
                    const options = { day: '2-digit', month: '2-digit', year: 'numeric' };
                    const formattedDate = date.toLocaleDateString('en-IN', options).replace(/\//g, '-');
                    var sms = `Dear Dealer, The subsidy amount against the transaction ${element.TRANSACTION_ID}, sale of ${element.Variety_Name} to farmer ${element.FARMER_ID} on date ${formattedDate} has been switched to DBT mode from e-rupee mode due to technical issues. The DBT amount will be deposited in your bank amount within 3 to 5 working days so rest assured about the payment. From : SEED DIVISION, Govt of Odisha. DAFP AGRIOR`;

                    var mobileNo = 8093274461;
                    //data.dealerMobileNo;
                    // req.query.otp = otp;
                    console.log(sms);
                    console.log(data);
                    request({
                        url: `https://mkuy.apicol.nic.in/Registration/EPestSMSNew?template_id=1107171343262303503&type=OTP&mobileNo=${mobileNo}&sms=${sms}`,
                        json: true,
                        strictSSL: false
                    }, (err, resp, body) => {
                        if (err) {
                            console.log(err);
                        } else {
                        }
                    });

                }

            })

        }
    } catch (e) {
        console.log(`Oops! An error occurred: ${e}`);
    } finally {
        client.release();
    }
};
exports.revokeVoucherCreatedForeRUPI = (data) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query1 = `UPDATE "eRUPIDetails" SET rejectedmsg='Successfully REVOKE'  where "TRANSACTION_ID"=$1 and "voucherId"=$2 and id= $3;`;
        const values1 = [ data.TRANSACTION_ID, data.oldVoucherId, data.id];
        console.log(query1, values1);
        const response = await client.query(query1, values1);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.revokeVoucherDetails = () => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query1 = `select * from "eRUPIDetails" where rejectedmsg='Successfully REVOKE' and "redeemstatusId" !='6' order by "voucherId"`;
        const response = await client.query(query1);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});

exports.revokeVoucherList = (voucherId) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `select * from "eRUPIDetails" where "voucherId"=$1 and "redeemstatusMsg"='Voucher Revoked'`;
        const values = [voucherId]
        const response = await client.query(query,values);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.dummycheck = () => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `select * from "eRUPIDetails" where "voucherId" is not null order by id `;
        const response = await client.query(query);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});

// INSERT INTO public."eRUPIDetails"(
// 	id, "TRANSACTION_ID", "districtId", "dealerCode", "olddealerCode", "dealerName", "dealerMobileNo", "farmerName", "farmerMobileNo", "farmerId", "payableAmtFarmer",status, "FinYear", season, insertedon, count )
// 	VALUES (73, 'WK24260215160076-23', '369','e-Lic/SEED/2020D-1629', 'ODPUR2/2015-16/0076', 'UMAKANTA TRIPATHY', '9861780715', 'SUBRAT NAYAK', '8093274461', 'PUR/67', '500.00','0','2024-25','K','now()',1)
	
// 	select * from "eRUPIDetails" where "voucherId" is null and current_timestamp >= "voucherCreatedOnTime" 
