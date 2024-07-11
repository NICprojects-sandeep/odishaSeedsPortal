var dbConfig = require('../models/dbConfig');
var sqlstock = dbConfig.sqlstock;
var sequelizeSeed = dbConfig.sequelizeSeed;

var locConfigstock = dbConfig.locConfigStock;
var locConfigStockLive = dbConfig.locConfigStockLive;
var locConfigFarmerDB = dbConfig.locConfigFarmerDB;

var sequelizeStock = dbConfig.sequelizeStock;
var sequelizeFarmerDB = dbConfig.sequelizeFarmerDB;
const pool = require('../config/dbConfig');

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
    console.log(partUserID);
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
exports.gerFarmerDetailsForSamallMarginUpdatation = (NICFARMERID) => new Promise(async (resolve, reject) => {
    var con = new sqlstock.ConnectionPool(locConfigFarmerDB);
    try {
        con.connect().then(function success() {
            const request = new sqlstock.Request(con);
            request.input('FarmerId', NICFARMERID);
            request.execute('sp_SelectFarmerDetails', function (err, result) {
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
exports.farmerTypeUpdate = (data) => new Promise(async (resolve, reject) => {
    var con = new sqlstock.ConnectionPool(locConfigFarmerDB);
    try {
        con.connect().then(function success() {
            const request = new sqlstock.Request(con);
            request.input('FarmerId', data.NICFARMERID);
            request.input('FarmerType', data.farmerType);
            request.execute('sp_FarmerTypeUpdate', function (err, result) {
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
exports.delalerwisestockCheack = (data) => new Promise(async (resolve, reject) => {
    var con = new sqlstock.ConnectionPool(locConfigFarmerDB);
    try {
        con.connect().then(function success() {
            const request = new sqlstock.Request(con);
            request.input('FarmerId', data.NICFARMERID);
            request.input('FarmerType', data.farmerType);
            request.execute('sp_FarmerTypeUpdate', function (err, result) {
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
exports.FillCategoryId = () => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `SELECT "Crop_Code","Crop_Name" FROM "mCrop" WHERE  "IS_ACTIVE" = '1' ORDER BY "Crop_Name" ASC`;
        const values = [];
        const response = await client.query(query, values);
        resolve(response.rows);
    } catch (e) {
        await client.query('rollback');
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.physicalsaleentrydata = async (blockcode, req, res) => {
    return new Promise(async resolve => {
        try {
            const result = await sequelizeStock.query(`SELECT A.APP_FIRMNAME,A.LIC_NO,CASE WHEN A.APPMOB_NO <>'' THEN APPMOB_NO ELSE '' END AS APPMOB_NO,CASE A.APP_TYPE WHEN 'Secretary PACS' THEN 'PACS' ELSE 'DEALER' END APP_TYPE, p.pan_name     
            FROM [DAFPSEED].[DBO].[SEED_LIC_DIST] A                                  
            LEFT OUTER JOIN [DAFPSEED].[DBO].[SEED_LIC_BUS_DIST] B ON A.SEED_LIC_DIST_ID = B.SEED_LIC_DIST_ID     
            LEFT JOIN  [STOCK].dbo.panchayat P ON P.pan_code=B.GP_ID                     
            WHERE  B.BLOCK_ID IN (select BLK_CODE from JALANIDHI_DAO_AAO where aao_code= :blockcode)  and LIC_NO is not null    AND B.IS_ACTIVE = 1  `, {
                replacements: { blockcode: blockcode }, type: sequelizeStock.QueryTypes.SELECT
            });
            resolve(result);

        } catch (e) {
            console.log('An error occurred...', e);
            resolve([]);
            throw e
        }
    });
};

exports.physicalsaleentrydata2 = (data, selectedFinancialYear, selectedSeasons, selectedCrop) => new Promise(async (resolve, reject) => {
    console.log('call');
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    const licNoListString = data.map(licNo => `'${licNo}'`).join(',');
    try {
        const query = `SELECT a."LICENCE_NO", a."CROP_ID", a."FIN_YR", a."SEASSION", 
        a."ACTUAL_RECEIVE", a."ACTUAL_SALE", 
        COALESCE(b."SaleQty", 0) as "SaleQty",case when  COALESCE(b."SaleQty", 0)  =0  THEN '0' ELSE COALESCE(b."SaleQty", 0) - a."ACTUAL_SALE"  END as balance 
        FROM ( SELECT "LICENCE_NO", "CROP_ID", "FIN_YR", "SEASSION", COALESCE(SUM("STOCK_QUANTITY"), 0) AS "ACTUAL_RECEIVE", (COALESCE(SUM("STOCK_QUANTITY"), 0) - COALESCE(SUM("AVL_QUANTITY"), 0)) AS "ACTUAL_SALE" FROM "STOCK_DEALERSTOCK"  
            WHERE ('${selectedCrop}' IS NULL OR "CROP_ID" = '${selectedCrop}') AND ('${selectedSeasons}' IS NULL OR '${selectedSeasons}' = '0' OR "SEASSION" = '${selectedSeasons}')
            AND ('${selectedFinancialYear}' IS NULL OR '${selectedFinancialYear}' = '0' OR "FIN_YR" = '${selectedFinancialYear}') and "LICENCE_NO" in (${licNoListString})  
               GROUP BY "LICENCE_NO", "CROP_ID","FIN_YR","SEASSION") a
               LEFT JOIN (
                   SELECT COALESCE(SUM("SaleQty"), 0) AS "SaleQty", "LIC_NO"
                   FROM "DealerwiseStock" WHERE ('${selectedCrop}' IS NULL OR "Crop_Code" = '${selectedCrop}')
                   AND ('${selectedSeasons}' IS NULL OR '${selectedSeasons}' = '0' OR "SEASSION" = '${selectedSeasons}')
                   AND ('${selectedFinancialYear}' IS NULL OR '${selectedFinancialYear}' = '0' OR "FIN_YR" = '${selectedFinancialYear}') and "LIC_NO" in (${licNoListString})     GROUP BY "LIC_NO"
                ) b ON a."LICENCE_NO" = b."LIC_NO"`;
                console.log(query);
        // const values = [licNoListString];
        const response = await client.query(query);
        resolve(response.rows);
    } catch (e) {
        await client.query('rollback');
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.inserphysicalsaleentrydata = (data) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        var count = 0;
        for (let index = 0; index < data.length; index++) {
            count += 1
            const element = data[index];
            const query = `select COUNT(*) from "DealerwiseStock" where "LIC_NO"='${element.LIC_NO}' and "Crop_Code"='${element.CROP_ID}' and "FIN_YR"='${element.FIN_YR}' and "SEASSION"='${element.SEASSION}'`;
            const response = await client.query(query);
            if (response.rows[0].count == 0) {
                const query = `INSERT INTO public."DealerwiseStock"(
                    "LIC_NO", "Crop_Code", "SaleQty", "UPDATED_ON", "UpdatedBy", "FIN_YR", "SEASSION") values ($1, $2, $3, $4, $5, $6, $7)`;
                const values = [element.LIC_NO, element.CROP_ID, element.SaleQty, 'now()', data.userID, element.FIN_YR, element.SEASSION];
                await client.query(query, values);
            } else {
                const query1 = `UPDATE public."DealerwiseStock" set "SaleQty"='${element.SaleQty}'  where "LIC_NO"='${element.LIC_NO}' and "Crop_Code"='${element.CROP_ID}' and "FIN_YR"='${element.FIN_YR}' and "SEASSION"='${element.SEASSION}' `;
                await client.query(query1);
            }
            if (count == data.length) {
                resolve({ "result": 'True' })
            }

        }
    } catch (e) {
        await client.query('rollback');
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});

// SELECT A.APP_FIRMNAME,A.LIC_NO,CASE WHEN A.APPMOB_NO <>'' THEN '<i style=color:red>'+APPMOB_NO+'</i>' ELSE '' END AS APPMOB_NO,CASE A.APP_TYPE WHEN 'Secretary PACS' THEN 'PACS' ELSE 'DEALER' END APP_TYPE, p.pan_name
// FROM [DAFPSEED].[DBO].[SEED_LIC_DIST] A
// LEFT OUTER JOIN [DAFPSEED].[DBO].[SEED_LIC_BUS_DIST] B ON A.SEED_LIC_DIST_ID = B.SEED_LIC_DIST_ID
// LEFT JOIN  [STOCK].dbo.panchayat P ON P.pan_code=B.GP_ID
// WHERE  B.BLOCK_ID IN (select BLK_CODE from JALANIDHI_DAO_AAO where aao_code= '070301')  and LIC_NO is not null    AND B.IS_ACTIVE = 1

// SELECT "LICENCE_NO",
//        COALESCE(SUM("STOCK_QUANTITY"), 0) AS "ACTUAL_RECEIVE",
//        (COALESCE(SUM("STOCK_QUANTITY"), 0) - COALESCE(SUM("AVL_QUANTITY"), 0)) AS "ACTUAL_SALE"
// FROM "STOCK_DEALERSTOCK"
// WHERE ('C002' IS NULL OR "CROP_ID" = 'C002')
//  AND ('K' IS NULL OR 'K' = '0' OR "SEASSION" = 'K')
//  AND ('2024-25' IS NULL OR '2024-25' = '0' OR "FIN_YR" = '2024-25') and "LICENCE_NO"='ODBAL5/2022-23/0011'
// GROUP BY "LICENCE_NO";

// select sum(SaleQty)SaleQty,LIC_NO from [STOCK].[DBO].[DealerwiseStock] D where  ('C002' is null or  D.Crop_Code = 'C002' )
// AND ('K' is null or 'K'='0' or d.SEASSION='K')
// AND ('2022-23' is null or '2022-23'='0' or d.FIN_YR='2022-23') group by LIC_NO