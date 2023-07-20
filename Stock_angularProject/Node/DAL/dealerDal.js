var dbConfig = require('../config/dbSqlConnection');
var sqlstock = dbConfig.sqlstock;
var sequelizeSeed = dbConfig.sequelizeSeed;
const format = require('pg-format');
const pool = require('../config/dbConfig');

exports.GetDealerLicenceByDistCodeUserType = (DIST_CODE) => new Promise(async (resolve, reject) => {
    try {
        const result = await sequelizeSeed.query(`SELECT DISTINCT CASE WHEN A.LIC_NO1 IS NOT NULL THEN A.LIC_NO1 ELSE A.LIC_NO END + '/DA & FP(O) - ' + A.APP_FIRMNAME +' - '+ A.LIC_NO AS 'Dealer', A.APP_FIRMNAME, A.LIC_NO FROM SEED_LIC_DIST A 
        LEFT OUTER JOIN SEED_LIC_COMP_DIST B ON A.SEED_LIC_DIST_ID = B.SEED_LIC_DIST_ID 
        LEFT OUTER JOIN SEED_LIC_APP_DIST C ON A.SEED_LIC_DIST_ID = C.SEED_LIC_DIST_ID 
        inner join [dbo].dist d on a.DIST_CODE= d.dist_code
        WHERE d.LGDistrict = :DIST_CODE  AND A.APP_STATUS = 'A' AND A.IS_ACTIVE = 1 AND A.LIC_ACTIVE = 1 AND CONVERT(DATE,GETDATE(),103) <= A.APR_UPTO AND COMP_TYPE = 1 AND COMP_NAME = 'OSSC' AND A.APP_TYPE != 'Secretary PACS'
        ORDER BY A.APP_FIRMNAME`, {//GAN/141088
            replacements: { DIST_CODE: DIST_CODE }, type: sequelizeSeed.QueryTypes.SELECT
        });
        resolve(result);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } 
});
exports.GetDealerLicenceByDistCodeUserTypePacs = (DIST_CODE) => new Promise(async (resolve, reject) => {
      try {
          const result = await sequelizeSeed.query(`SELECT DISTINCT CASE WHEN A.LIC_NO1 IS NOT NULL THEN A.LIC_NO1 ELSE A.LIC_NO END + '/DA & FP(O) - ' + A.APP_FIRMNAME +' - '+ A.LIC_NO AS 'Dealer', A.APP_FIRMNAME, A.LIC_NO FROM SEED_LIC_DIST A 
          LEFT OUTER JOIN SEED_LIC_COMP_DIST B ON A.SEED_LIC_DIST_ID = B.SEED_LIC_DIST_ID 
          LEFT OUTER JOIN SEED_LIC_APP_DIST C ON A.SEED_LIC_DIST_ID = C.SEED_LIC_DIST_ID 
          inner join [dbo].dist d on a.DIST_CODE= d.dist_code
          WHERE d.LGDistrict = :DIST_CODE AND A.APP_STATUS = 'A' AND A.IS_ACTIVE = 1 AND A.LIC_ACTIVE = 1 AND CONVERT(DATE,GETDATE(),103)<= A.APR_UPTO AND COMP_TYPE = 1 AND COMP_NAME = 'OSSC' AND A.APP_TYPE = 'Secretary PACS' ORDER BY A.APP_FIRMNAME`, {
              replacements: { DIST_CODE: DIST_CODE }, type: sequelizeSeed.QueryTypes.SELECT
          });
          resolve(result);
      } catch (e) {
          reject(new Error(`Oops! An error occurred: ${e}`));
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
    console.log(data);
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query1 = ` SELECT "SHORT_NAME","SEASSION_NAME" FROM "mSEASSION" WHERE "FIN_YR" = $1 AND "IS_ACTIVE" = 1  `;
        const values1 = [data.FIN_YR];
        console.log(query1, values1);
        const response = await client.query(query1, values1);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.FILL_GODOWN = (DIST_CODE) => new Promise(async (resolve, reject) => {
    console.log(DIST_CODE);
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `SELECT "Godown_ID","Godown_Name" FROM public."Stock_Godown_Master" a inner join "Stock_District" b on a."Dist_Code" = b."Dist_Code" WHERE b."LGDistrict" = $1 AND a."User_Type"= 'OSSC' AND a."IsActive" = 'Y'`;
        const values = [DIST_CODE];
        const response = await client.query(query,values);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.FILL_CROPCATAGORY = (selectedGodown) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `SELECT B."Category_Code",B."Category_Name" FROM public."Stock_StockDetails" A 
        inner JOIN public."mCropCategory" B ON A."CropCatg_ID" = B."Category_Code" 
        WHERE A."Avl_Quantity" > 0 AND "User_Type" = 'OSSC' AND A."EXPIRY_DATE">now() AND A."Godown_ID" = $1
        AND A."AVL_NO_OF_BAGS" > 0 AND A."VALIDITY" = true 
        GROUP BY B."Category_Code",B."Category_Name"
        ORDER BY B."Category_Name"`;
        const values = [selectedGodown];
        const response = await client.query(query,values);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.FILLCROPNAME = (DIST_CODE) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `SELECT "Godown_ID","Godown_Name" FROM public."Stock_Godown_Master" a inner join "Stock_District" b on a."Dist_Code" = b."Dist_Code" WHERE b."LGDistrict" = $1 AND a."User_Type"= 'OSSC' AND a."IsActive" = 'Y'`;
        const values = [DIST_CODE];
        const response = await client.query(query,values);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.FILLCROPVARIETY = (DIST_CODE) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `SELECT "Godown_ID","Godown_Name" FROM public."Stock_Godown_Master" a inner join "Stock_District" b on a."Dist_Code" = b."Dist_Code" WHERE b."LGDistrict" = $1 AND a."User_Type"= 'OSSC' AND a."IsActive" = 'Y'`;
        const values = [DIST_CODE];
        const response = await client.query(query,values);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});