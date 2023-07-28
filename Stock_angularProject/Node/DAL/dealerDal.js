var dbConfig = require('../config/dbSqlConnection');
var sqlstock = dbConfig.sqlstock;
var sequelizeSeed = dbConfig.sequelizeSeed;
const format = require('pg-format');
const pool = require('../config/dbConfig');

exports.GetDealerLicenceByDistCodeUserType = (DIST_CODE) => new Promise(async (resolve, reject) => {
    try {
        const result = await sequelizeSeed.query(`SELECT DISTINCT CASE WHEN A.LIC_NO1 IS NOT NULL THEN A.LIC_NO1 ELSE A.LIC_NO END + '/DA & FP(O) - ' + A.APP_FIRMNAME +' - '+ A.LIC_NO AS 'Dealer', A.APP_FIRMNAME, A.LIC_NO,a.LIC_NO1 FROM SEED_LIC_DIST A 
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
        const result = await sequelizeSeed.query(`SELECT DISTINCT CASE WHEN A.LIC_NO1 IS NOT NULL THEN A.LIC_NO1 ELSE A.LIC_NO END + '/DA & FP(O) - ' + A.APP_FIRMNAME +' - '+ A.LIC_NO AS 'Dealer', A.APP_FIRMNAME, A.LIC_NO,a.LIC_NO1 FROM SEED_LIC_DIST A 
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
exports.FILL_GODOWN = (DIST_CODE, prebookedsale) => new Promise(async (resolve, reject) => {
    console.log(prebookedsale);
    if (prebookedsale == 'true') {
        prebookedsale = 'Y'
    }
    else {
        prebookedsale = 'N'

    }
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        let query =``;
        let values=[]
        if (prebookedsale == 'N') {
             query = `SELECT "Godown_ID","Godown_Name" FROM public."Stock_Godown_Master" a inner join "Stock_District" b on a."Dist_Code" = b."Dist_Code" WHERE b."LGDistrict" = $1 AND a."User_Type"= 'OSSC' AND a."IsActive" = 'Y'`;
             values = [DIST_CODE];
       
            }
        else {
             query = `SELECT "Godown_ID","Godown_Name" FROM public."Stock_Godown_Master" a inner join "Stock_District" b on a."Dist_Code" = b."Dist_Code" WHERE b."LGDistrict" = $1 AND a."User_Type"= 'OSSC' AND a."IsActive" = 'Y' and "PrebookingGodown"=$2`;
             values = [DIST_CODE, prebookedsale];
            }
        console.log(query, values);
        const response = await client.query(query, values);
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
        const response = await client.query(query, values);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.FILLCROPNAME = (selectedCategory, selectedGodown) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `SELECT B."Crop_Code",B."Crop_Name" FROM "Stock_StockDetails" A 
        LEFT OUTER JOIN public."mCrop" B ON A."Crop_ID" = B."Crop_Code" 
        WHERE  A."Avl_Quantity" > 0 AND "User_Type" = 'OSSC' AND A."CropCatg_ID" = $1 AND A."Godown_ID" = $2
        AND A."AVL_NO_OF_BAGS" > 0 AND A."VALIDITY" = true 
        GROUP BY B."Crop_Code",B."Crop_Name" order by B."Crop_Name" `;
        const values = [selectedCategory, selectedGodown];
        const response = await client.query(query, values);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.FILLCROPVARIETY = (selectedCrop, selectedCategory, selectedGodown) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `SELECT B."Variety_Code",B."Variety_Name" FROM "Stock_StockDetails" A 
        LEFT OUTER JOIN "mCropVariety" B ON A."Crop_Verid" = B."Variety_Code" 
        WHERE  A."Avl_Quantity" > 0 AND "User_Type" = 'OSSC' AND A."CropCatg_ID" = $2 AND A."Crop_ID" = $1 
        AND A."Godown_ID" = $3
        AND A."AVL_NO_OF_BAGS" > 0 AND A."VALIDITY" = true 
        AND (A."EXPIRY_DATE" = NULL OR "EXPIRY_DATE"::date >= CURRENT_TIMESTAMP) AND 
        B."IS_ACTIVE" = 1 
        GROUP BY B."Variety_Code",B."Variety_Name"
        ORDER BY B."Variety_Name"`;
        const values = [selectedCrop, selectedCategory, selectedGodown];
        const response = await client.query(query, values);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.prebookingDetailsOfDealer = (SelectedDealerOrPacs, distCode) => new Promise(async (resolve, reject) => {
    console.log(SelectedDealerOrPacs);
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `select "applicationID",b."Crop_Name",b."Crop_Code",b."Category_Code",c."Variety_Name",c."Variety_Code",Round((CAST ("bagSize" AS decimal)* CAST ("noOfBag" AS decimal))/100,2) as qtyinqtl,a."bagSize",a."noOfBag","totalCost","preBookingAmt" from prebookinglist a
        inner join "mCrop" b on a."cropCode" = b."Crop_Code"
        inner join "mCropVariety" c on a."varietyCode" = c."Variety_Code"
        where "beneficiaryType"='D' and cast ("distID" as Integer)=$1 and a."IS_ACTIVE"=1 and "dealerId"=$2 order by c."Variety_Name" `;
        const values = [distCode, SelectedDealerOrPacs];
        console.log(query, values);
        const response = await client.query(query, values);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    }
});
exports.fillAvailableStockDetails = (data) => new Promise(async (resolve, reject) => {
    console.log();
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `select "Stock_ID",b."Receive_Unitcd",c."REF_NO","Receive_Unitname","Lot_No","Bag_Size_In_kg",CAST(A."Avl_Quantity"/CAST(A."Bag_Size_In_kg" as INT)*100 AS INT) "RECV_NO_OF_BAGS","Avl_Quantity","Crop_Verid" from public."Stock_StockDetails" a
        left outer join public."Stock_Receive_Unit_Master" b on a."Receive_Unitcd"= b."Receive_Unitcd"
        left join "mMouData" c on a."MOU_REFNO"= c."REF_NO"
        where a."Godown_ID"=$4 and a."CropCatg_ID"=$3 and a."Crop_ID"=$2 and a."Crop_Verid"=$1 and a."User_Type"='OSSC' and a."AVL_NO_OF_BAGS">0 and a."VALIDITY"='True' and "EXPIRY_DATE"> CURRENT_TIMESTAMP and "Class" in('Certified','TL')`;
        const values = [data.selectedVariety,data.selectedCrop, data.selectedCategory, data.selectedGodown];
        console.log(query, values);
        const response = await client.query(query, values);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    }
});