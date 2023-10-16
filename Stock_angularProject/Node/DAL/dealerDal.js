var dbConfig = require('../config/dbSqlConnection');
var sqlstock = dbConfig.sqlstock;
var sequelizeSeed = dbConfig.sequelizeSeed;
var locConfigdafpSeeds = dbConfig.locConfigdafpSeeds;

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
exports.FILL_GODOWN = (DIST_CODE, prebookedsale) => new Promise(async (resolve, reject) => {
    if (prebookedsale == 'true') {
        prebookedsale = 'Y'
    }
    else {
        prebookedsale = 'N'

    }
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        let query = ``;
        let values = []
        if (prebookedsale == 'N') {
            query = `SELECT "Godown_ID","Godown_Name" FROM public."Stock_Godown_Master" a inner join "Stock_District" b on a."Dist_Code" = b."Dist_Code" WHERE b."LGDistrict" = $1 AND a."User_Type"= 'OSSC' AND a."IsActive" = 'Y' order by "Godown_Name"`;
            values = [DIST_CODE];

        }
        else {
            query = `SELECT "Godown_ID","Godown_Name" FROM public."Stock_Godown_Master" a inner join "Stock_District" b on a."Dist_Code" = b."Dist_Code" WHERE b."LGDistrict" = $1 AND a."User_Type"= 'OSSC' AND a."IsActive" = 'Y' and "PrebookingGodown"=$2 order by "Godown_Name"`;
            values = [DIST_CODE, prebookedsale];
        }
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
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `select "applicationID",b."Crop_Name",b."Crop_Code",b."Category_Code",c."Variety_Name",c."Variety_Code",Round((CAST ("bagSize" AS decimal)* CAST ("noOfBag" AS decimal))/100,2) as qtyinqtl,a."bagSize",a."noOfBag","totalCost","preBookingAmt" from prebookinglist a
        inner join "mCrop" b on a."cropCode" = b."Crop_Code"
        inner join "mCropVariety" c on a."varietyCode" = c."Variety_Code"
        where "beneficiaryType"='D' and cast ("distID" as Integer)=$1 and a."IS_ACTIVE"=1 and "dealerId"=$2 order by c."Variety_Name" `;
        const values = [distCode, SelectedDealerOrPacs];
        const response = await client.query(query, values);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    }
});
exports.fillAvailableStockDetails = (data) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `select "Stock_ID",b."Receive_Unitcd",c."REF_NO","Receive_Unitname","Lot_No","Bag_Size_In_kg",CAST(A."Avl_Quantity"/CAST(A."Bag_Size_In_kg" as INT)*100 AS INT) "RECV_NO_OF_BAGS","Avl_Quantity","Crop_Verid",a."Godown_ID",a."Class",d."All_in_cost_Price" from public."Stock_StockDetails" a
        left outer join public."Stock_Receive_Unit_Master" b on a."Receive_Unitcd"= b."Receive_Unitcd"
        left join "mMouData" c on a."MOU_REFNO"= c."REF_NO"
        left join "Stock_Pricelist" d on a."Crop_Verid" = d."Crop_Vcode" and d."F_Year"=(select "FIN_YR" from "mFINYR" where "IS_ACTIVE"=1) and d.seasons=(select "SHORT_NAME" from "mSEASSION" where "IS_ACTIVE"=1)
        where a."Godown_ID"=$4 and a."CropCatg_ID"=$3 and a."Crop_ID"=$2 and a."Crop_Verid"=$1 and a."User_Type"='OSSC' and a."AVL_NO_OF_BAGS">0 and a."VALIDITY"='True' and "EXPIRY_DATE"> CURRENT_TIMESTAMP and "Class" in('Certified','TL')`;
        const values = [data.selectedVariety, data.selectedCrop, data.selectedCategory, data.selectedGodown];
        const response = await client.query(query, values);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    }
});
exports.getSupplyType = () => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `select "SUPPLY_ID","SUPPLY_NAME" from public."Stock_SupplyType" where "USER_TYPE"='OSSC' and "ISACTIVE" = 'Y' ORDER BY "ORDER_NO"`;
        const values = [];
        const response = await client.query(query, values);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.fillDealerSaleDeatils = (data) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        var DDAMOUNT = data.AMOUNT;
        var tDATE = data.SALE_DATE;
        let STATUS = '';
        var mCONFIRM_STATUS = '';
        var IS_OSSC = '';
        var DIST_NAME = '';
        var MAXTRAN_NO = '';
        var MAXSALETRAN_NO = '';
        var SALETRANSID = '';


        var CASH_MEMO_NO = ''
        if (tDATE == '1900-01-01') {
            tDATE = null;
        }
        if (data.AMOUNT == '') {
            data.AMOUNT = null;
            DDAMOUNT = null;
        }

        if (data.CONFIRM_STATUS == 'Y') {
            if (data.SUPPLY_TYPE == '1' || data.SUPPLY_TYPE == '6' || data.SUPPLY_TYPE == '9' || data.SUPPLY_TYPE == '12' || data.SUPPLY_TYPE == '2' || data.SUPPLY_TYPE == '7') {
                STATUS = 'S';
                mCONFIRM_STATUS = 1;
            }
            else if (data.SUPPLY_TYPE == '3' || data.SUPPLY_TYPE == '8') {
                STATUS = 'T';
                mCONFIRM_STATUS = 0;
            }
            if (data.SUPPLY_TYPE = 6) {
                IS_OSSC = await sequelizeSeed.query(`SELECT IS_OSSC FROM [DAFPSEED].[DBO].[SEED_LIC_DIST] WHERE LIC_NO = :SALE_TO`, {
                    replacements: { SALE_TO: data.SALE_TO }, type: sequelizeSeed.QueryTypes.SELECT
                })

                if (IS_OSSC[0].IS_OSSC == true) {
                    data.AMOUNT = 0;
                    data.DD_NUMBER = '';
                    DDAMOUNT = 0;
                }
            }

            DIST_NAME = await client.query(`SELECT SUBSTRING("Dist_Name",1,4) as "DIST_NAME" FROM "Stock_District" WHERE "LGDistrict"=${data.distCode}`);

            MAXTRAN_NO = await client.query(`SELECT COALESCE(MAX(cast(SUBSTRING("CASH_MEMO_NO", 21, 10) as int) ), 0)+1 AS max_value FROM "Stock_SaleDetails" WHERE SUBSTRING("CASH_MEMO_NO",1,4) ='${DIST_NAME.rows[0].DIST_NAME}' AND SUBSTRING("CASH_MEMO_NO",6,4) = '${data.GODOWN_ID}' AND SUBSTRING("CASH_MEMO_NO",11,7) =  '${data.FIN_YR}' AND SUBSTRING("CASH_MEMO_NO",19,1) =  '${data.SEASSION}'`)

            CASH_MEMO_NO = DIST_NAME.rows[0].DIST_NAME + '/' + data.GODOWN_ID + '/' + data.FIN_YR + '/' + data.SEASSION + '/' + MAXTRAN_NO.rows[0].max_value;

            MAXSALETRAN_NO = await client.query(` SELECT CAST(LEFT(SUBSTRING("SALETRANSID", 16, LENGTH("SALETRANSID")), POSITION('-' IN SUBSTRING("SALETRANSID", 16, LENGTH("SALETRANSID"))) - 1) AS INTEGER) + 1 as max FROM "Stock_SaleDetails" WHERE  SUBSTRING("SALETRANSID", 8, 7) =  '${data.FIN_YR}' AND SUBSTRING("SALETRANSID", 3, 4) = '${DIST_NAME.rows[0].DIST_NAME}' ORDER BY  CAST(LEFT(SUBSTRING("SALETRANSID" FROM 16 FOR POSITION('-' IN SUBSTRING("SALETRANSID" FROM 16))), POSITION('-' IN SUBSTRING("SALETRANSID" FROM 16)) - 1) AS INTEGER) DESC LIMIT 1;`)

            SALETRANSID = MAXSALETRAN_NO == null ? 'S/' + DIST_NAME.rows[0].DIST_NAME + '/' + data.FIN_YR + '/' + 1 : 'S/' + DIST_NAME.rows[0].DIST_NAME + '/' + data.FIN_YR + '/' + MAXSALETRAN_NO.rows[0].max;
            var count = 0
            for (const e of data.VALUES) {
                var PRICE_RECEIVE_UNITCD = '';
                var Class_BagSize = '';
                var mCROP_CLASS = '';
                var mBAG_SIZE = '';
                var m_AMOUNT = '';
                var mAMOUNT = '';
                var AVL_NOofBags_Quantity = '';
                var AVL_NO_OF_BAGS = 0;
                var AVL_QUANTITY = 0;
                var OSSC_PACS_OSSC_DEALER = '';
                var OSSC_PACS = '';
                var OSSC_DEALER = '';
                var mALINCOST = '';
                var mSALETRANSID = '';
                var PREBOOKING_AMT = 0.00;
                var insertintostocksaledetails = ''
                var All_in_cost_Price_check = '';
                var mTOT_SUB_AMT = 0;
                var mTOT_QTY = '';
                var mTOT_AMOUNT = 0;
                var updateSTOCK_DEALERSTOCK = '';
                var testingandexpirydate = ''
                PRICE_RECEIVE_UNITCD = await client.query(`SELECT "PRICE_RECEIVE_UNITCD" FROM "Price_SourceMapping" WHERE "RECEIVE_UNITCD" = '${e.Receive_Unitcd}' AND "SEASSION" =  '${data.SEASSION}' AND "FIN_YR" = '${data.FIN_YR}';`)

                Class_BagSize = await client.query(`SELECT "Class","Bag_Size_In_kg" FROM "Stock_StockDetails" WHERE "Lot_No" = '${e.LOT_NO}' AND "Crop_ID" =  '${e.CROP_ID}' AND "Crop_Verid" ='${e.CROP_VERID}' `)
                mCROP_CLASS = Class_BagSize.rows[0].Class;
                mBAG_SIZE = Class_BagSize.rows[0].Bag_Size_In_kg;
                m_AMOUNT = await client.query(`SELECT "All_in_cost_Price" FROM "Stock_Pricelist" WHERE "Crop_class" = '${mCROP_CLASS}' AND "RECEIVE_UNITCD" = '${PRICE_RECEIVE_UNITCD.rows[0].PRICE_RECEIVE_UNITCD}' AND "Crop_Vcode" = '${e.CROP_VERID}' AND "Crop_Code" = '${e.CROP_ID}' AND "seasons" = '${data.SEASSION}' AND "F_Year" = '${data.FIN_YR}'`);
                mAMOUNT = m_AMOUNT.rows[0].All_in_cost_Price;
                AVL_NOofBags_Quantity = await client.query(`SELECT "AVL_NO_OF_BAGS","Avl_Quantity" FROM "Stock_StockDetails" WHERE "Crop_Verid" ='${e.CROP_VERID}' AND "Class" = '${mCROP_CLASS}' AND "Receive_Unitcd" = '${e.Receive_Unitcd}' AND "Lot_No" = '${e.LOT_NO}' AND "Bag_Size_In_kg" = '${mBAG_SIZE}' AND "User_Type" = 'OSSC' AND "Godown_ID" = '${data.GODOWN_ID}' AND "VALIDITY" = 'true'`)
                AVL_NO_OF_BAGS = AVL_NOofBags_Quantity.rows[0].AVL_NO_OF_BAGS;
                AVL_QUANTITY = AVL_NOofBags_Quantity.rows[0].Avl_Quantity;
                if (data.PrebookingorNot) {
                    PREBOOKING_AMT = ((parseInt(mAMOUNT) * parseFloat(data.TotalNoOfQuantity)) * 10) / 100
                }
                if (AVL_NO_OF_BAGS >= e.NO_OF_BAGS) {
                    count += 1
                    if (data.SUPPLY_TYPE == '1' || data.SUPPLY_TYPE == '6' || data.SUPPLY_TYPE == '9' || data.SUPPLY_TYPE == '12') {
                        if (data.IS_PACS == 'true') {
                            OSSC_PACS_OSSC_DEALER = await client.query(`SELECT "OSSC_PACS","OSSC_DEALER" FROM "mPACS_DISCOUNT" WHERE "CROP_CODE" = '${e.CROP_ID}' AND "SEASSION" = '${data.SEASSION}' AND "FIN_YR" = '${data.FIN_YR}' AND "IS_ACTIVE" = 1`)
                            OSSC_PACS = OSSC_PACS_OSSC_DEALER.rows[0].OSSC_PACS;
                            OSSC_DEALER = OSSC_PACS_OSSC_DEALER.rows[0].OSSC_DEALER;
                            if (data.PACSRebate == 1) {
                                mALINCOST = mAMOUNT - OSSC_DEALER;
                            }
                            else if (data.PACSRebate == 2) {
                                mALINCOST = mAMOUNT - OSSC_PACS;
                            }
                        }
                        else {
                            OSSC_PACS_OSSC_DEALER = await client.query(`SELECT "OSSC_DEALER" FROM "mPACS_DISCOUNT" WHERE "CROP_CODE" = '${e.CROP_ID}' AND "SEASSION" = '${data.SEASSION}' AND "FIN_YR" = '${data.FIN_YR}' AND "IS_ACTIVE" = 1`)
                            OSSC_DEALER = OSSC_PACS_OSSC_DEALER.rows[0].OSSC_DEALER;
                            mALINCOST = mAMOUNT - OSSC_DEALER;
                        }
                    }
                    else {
                        mALINCOST = 0;
                        mAMOUNT = 0;
                    }
                    mSALETRANSID = (SALETRANSID + '-' + count);
                    const query = `INSERT INTO public."Stock_SaleDetails"("SALETRANSID", "SUPPLY_TYPE", "CREDIT_BILL_NO", "mDATE", "DEPT_TYPE", "GODOWN_ID", "SALE_DATE", "SALE_TO", "DD_NUMBER", "DD_AMOUNT", "CASH_MEMO_NO",
                     "PRICE_QTL", "AMOUNT", "CROPCATG_ID", "CROP_ID", "CROP_VERID", "CLASS", "Receive_Unitcd", "MOU_REFNO", "LOT_NUMBER", "BAG_SIZE_KG", "SALE_NO_OF_BAG", "CONFIRM_STATUS", "STATUS", "SEASONS", "F_YEAR", 
                     "UPDATED_BY", "UPDATED_ON", "USER_TYPE", "USERIP", "IS_ACTIVE", "PREBOOKING_AMT", "PREBOOKING_APPLICATIONID") values ($1, $2, $3, $4, $5, $6, $7, $8, $9, $10,$11, $12, $13, $14, $15, $16, $17, $18, $19, $20,$21, $22, $23, $24, $25, $26, $27, $28, $29, $30, $31, $32, $33)`;
                    const values = [mSALETRANSID, data.SUPPLY_TYPE, data.CREDIT_BILL_NO, null, data.DEPT_TYPE, e.Godown_ID, data.SALE_DATE, data.SALE_TO, data.DD_NUMBER, DDAMOUNT, CASH_MEMO_NO,
                        mALINCOST, mALINCOST * mBAG_SIZE * e.NO_OF_BAGS / 100, e.CATEGORY_ID, e.CROP_ID, e.CROP_VERID, e.Class, e.Receive_Unitcd, data.MOU_REFNO, e.LOT_NO, e.BAG_SIZE_KG, e.NO_OF_BAGS, mCONFIRM_STATUS, STATUS, data.SEASSION, data.FIN_YR,
                        data.UPDATED_BY, 'now()', 'OSSC', data.ipAdress, 'Y', PREBOOKING_AMT, data.applicationId];
                    insertintostocksaledetails = await client.query(query, values);
                    if (insertintostocksaledetails.rowCount == 1) {
                        if (data.PrebookingorNot) {
                            let updateinprebookinglist = await client.query(`update prebookinglist set "TRANSACTION_ID" = '${CASH_MEMO_NO}' ,"IS_ACTIVE"='0',"noofBagSale" = '${e.NO_OF_BAGS}' ,"saleAmount"='${PREBOOKING_AMT}' 
                    where  "applicationID" >= '${data.applicationId}'`);
                        }
                        let updateinStock_StockDetails = await client.query(`update "Stock_StockDetails" set "AVL_NO_OF_BAGS" = "AVL_NO_OF_BAGS" -${e.NO_OF_BAGS} ,"Avl_Quantity"="Avl_Quantity"-${e.QUANTITY} 
                    where "Lot_No"='${e.LOT_NO}'  and "Godown_ID"='${e.Godown_ID}' and "Crop_Verid" = '${e.CROP_VERID}' AND "Class" ='${e.Class}' AND "Receive_Unitcd" = '${e.Receive_Unitcd}' and "AVL_NO_OF_BAGS" >= ${e.NO_OF_BAGS}`);
                        if (data.SUPPLY_TYPE == '1' || data.SUPPLY_TYPE == '6' || data.SUPPLY_TYPE == '9') {
                            All_in_cost_Price_check = await client.query(`SELECT "All_in_cost_Price","TOT_SUBSIDY" FROM "Stock_Pricelist" WHERE "Crop_class" = '${e.Class}' AND "RECEIVE_UNITCD" = '${PRICE_RECEIVE_UNITCD.rows[0].PRICE_RECEIVE_UNITCD}' AND "Crop_Vcode" = '${e.CROP_VERID}' AND "Crop_Code" = '${e.CROP_ID}' AND seasons = '${data.SEASSION}' AND "F_Year" = '${data.FIN_YR}'`);

                            if (All_in_cost_Price_check.rows.length > 0) {
                                mAMOUNT = All_in_cost_Price_check.rows[0].All_in_cost_Price;
                                mTOT_SUB_AMT = All_in_cost_Price_check.rows[0].TOT_SUBSIDY
                            }
                            else {
                                mAMOUNT = 0;
                                mTOT_SUB_AMT = 0;
                            }
                        }
                        mTOT_QTY = (e.BAG_SIZE_KG * e.NO_OF_BAGS) / 100;
                        mTOT_AMOUNT = mTOT_QTY * mAMOUNT
                        if (data.SUPPLY_TYPE == '2' || data.SUPPLY_TYPE == '7') {
                            let updateinAmount = await client.query(`update "STOCK_DEALERSTOCK" set "AMOUNT" = ${mTOT_AMOUNT} where "CREDIT_BILL_NO"='${data.CREDIT_BILL_NO}'  and "mDATE"='${tDATE}'`);
                        }
                        updateSTOCK_DEALERSTOCK = await client.query(`select * from "STOCK_DEALERSTOCK" where "LICENCE_NO"='${data.SALE_TO}' and "CROP_VERID"='${e.CROP_VERID}' and "RECEIVE_UNITCD"='${PRICE_RECEIVE_UNITCD.rows[0].PRICE_RECEIVE_UNITCD}' and "CLASS"='${e.Class}' and "LOT_NO"='${e.LOT_NO}' and "BAG_SIZE_IN_KG"='${e.BAG_SIZE_KG}' and "USER_TYPE"='OSSC'`);
                        const query1 = `INSERT INTO public."Stock_ReceiveDealer"(
                        "LIC_NO", "RECEIVE_DATE", "DD_NUMBER", "CASH_MEMO_NO", "GODOWN_ID", "RECEIVE_UNITCD", "CROP_VARIETY_CODE", "CROP_ID", "CROP_CLASS", "LOT_NO", "BAG_SIZE", "NO_OF_BAGS", "AMOUNT", "SEASSION_NAME", "FIN_YR", "USER_TYPE", "STATUS", "ENTRYDATE", "USERID", "USERIP", "PACSRebate") values ($1, $2, $3, $4, $5, $6, $7, $8, $9, $10,$11, $12, $13, $14, $15, $16, $17, $18, $19, $20,$21)`;
                        const values1 = [data.SALE_TO, data.SALE_DATE, data.DD_NUMBER, CASH_MEMO_NO, e.Godown_ID, e.Receive_Unitcd, e.CROP_VERID, e.CROP_ID, e.Class, e.LOT_NO, e.BAG_SIZE_KG, e.NO_OF_BAGS, mTOT_AMOUNT, data.SEASSION, data.FIN_YR, 'OSSC', 'P', 'now()', data.UPDATED_BY, data.ipAdress, data.PACSRebate];
                        insertintoStock_ReceiveDealer = await client.query(query1, values1);

                        if (updateSTOCK_DEALERSTOCK.rows.length == 0) {
                            if (IS_OSSC[0].IS_OSSC == true) {
                                mTOT_AMOUNT = 0;
                                data.DD_NUMBER = '';
                            }

                            if (insertintoStock_ReceiveDealer.rowCount == 1) {
                                testingandexpirydate = await client.query(`select "TESTING_DATE","EXPIRY_DATE" from public."Stock_StockDetails" where "Lot_No"='${e.LOT_NO}' and "Crop_ID"='${e.CROP_ID}' and "Crop_Verid"='${e.CROP_VERID}' and "CropCatg_ID"='${e.CATEGORY_ID}' and "VALIDITY"= 'true'`);
                                const query2 = `INSERT INTO public."STOCK_DEALERSTOCK"(
                                "LICENCE_NO", "CLASS", "RECEIVE_UNITCD", "MOU_REFNO", "CROPCATG_ID", "CROP_VERID", "CROP_ID", "SEASSION", "FIN_YR", "LOT_NO", "BAG_SIZE_IN_KG", "RECV_NO_OF_BAGS", "AVL_NO_OF_BAGS", "PRICE_QTL", "SUBSIDY_QTL", "STOCK_DATE", "STOCK_QUANTITY", "AVL_QUANTITY", "USER_TYPE", "ENTRYDATE", "USERID", "USERIP",  "TESTING_DATE", "EXPIRY_DATE","VALIDITY") values ($1, $2, $3, $4, $5, $6, $7, $8, $9, $10,$11, $12, $13, $14, $15, $16, $17, $18, $19, $20,$21,$22,$23,$24,$25)`;
                                const values2 = [data.SALE_TO, e.Class, e.Receive_Unitcd, data.MOU_REFNO, e.CATEGORY_ID, e.CROP_VERID, e.CROP_ID, data.SEASSION, data.FIN_YR, e.LOT_NO, e.BAG_SIZE_KG, e.NO_OF_BAGS, e.NO_OF_BAGS, mAMOUNT, mTOT_SUB_AMT, 'now()', mTOT_QTY, mTOT_QTY, 'OSSC', 'now()', data.UPDATED_BY, data.ipAdress, testingandexpirydate.rows[0].TESTING_DATE, testingandexpirydate.rows[0].EXPIRY_DATE, '1'];
                                insertintoStock_ReceiveDealer = await client.query(query2, values2);
                                if (count == data.VALUES.length) {
                                    resolve({ "result": 'True', "CASH_MEMO_NO": CASH_MEMO_NO })
                                }
                            }
                        }
                        else {
                            let updateinAmount = await client.query(`
                        update "STOCK_DEALERSTOCK" set "RECV_NO_OF_BAGS"= "RECV_NO_OF_BAGS"+${e.NO_OF_BAGS} ,"AVL_NO_OF_BAGS"="AVL_NO_OF_BAGS"+${e.NO_OF_BAGS},"STOCK_QUANTITY"="STOCK_QUANTITY"+${mTOT_QTY}, "AVL_QUANTITY"="AVL_QUANTITY"+${mTOT_QTY} where "LICENCE_NO"='${data.SALE_TO}'and "CROP_VERID"='${e.CROP_VERID}' and "RECEIVE_UNITCD"= '${PRICE_RECEIVE_UNITCD.rows[0].PRICE_RECEIVE_UNITCD}'and "CLASS" ='${e.Class}' and "LOT_NO" ='${e.LOT_NO}' and "BAG_SIZE_IN_KG"='${e.BAG_SIZE_KG}' and "USER_TYPE"='OSSC'`);
                            if (count == data.VALUES.VALUES) {
                                resolve({ "result": 'True', "CASH_MEMO_NO": CASH_MEMO_NO })
                            }
                        }
                    }
                }

            }
            // });
        }

    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.updateSaledetails = (CASH_MEMO_NO, LOT_NO) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        let updateinStock_StockDetails = await client.query(`update "Stock_SaleDetails" set "updatedInSale" = 1 where "LOT_NUMBER"='${LOT_NO}'  and "CASH_MEMO_NO"='${CASH_MEMO_NO}' `);
        resolve(true);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.cashmemodetails = (applicationid) => new Promise(async (resolve, reject) => {
    var cashmemeodetails = [];
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `select "SUPPLY_TYPE","CASH_MEMO_NO","SALE_DATE","SALE_TO","DD_NUMBER","AMOUNT","Receive_Unitname","CROP_ID",b."Crop_Name","CROP_VERID",c."Variety_Name","CLASS","SALE_NO_OF_BAG","BAG_SIZE_KG","All_in_cost_Price",f."applicationID",g."Godown_Name","LOT_NUMBER",Round((CAST ("BAG_SIZE_KG" AS decimal)* CAST ("SALE_NO_OF_BAG" AS decimal))/100,2) as "Quantity" from public."Stock_SaleDetails" a
        inner join "mCrop" b on a."CROP_ID"= b."Crop_Code"
        inner join "mCropVariety" c on a."CROP_VERID"=c."Variety_Code"
        left join "Stock_Pricelist" d on a."CROP_VERID" = d."Crop_Vcode" and d."F_Year"=(select "FIN_YR" from "mFINYR" where "IS_ACTIVE"=1) and d.seasons=(select "SHORT_NAME" from "mSEASSION" where "IS_ACTIVE"=1)
        left outer join public."Stock_Receive_Unit_Master" e on a."Receive_Unitcd"= e."Receive_Unitcd"
        left join prebookinglist f on a."PREBOOKING_APPLICATIONID"= f."applicationID"
        inner join "Stock_Godown_Master"  g on a."GODOWN_ID"= g."Godown_ID"
        where "CASH_MEMO_NO"=$1`;
        const values = [applicationid];
        const response = await client.query(query, values);
        if(response.rows.length > 0){
            if(response.rows[0].SUPPLY_TYPE =='1' || response.rows[0].SUPPLY_TYPE =='6' || response.rows[0].SUPPLY_TYPE =='9'){
                for (const e of response.rows) {

                    const result = await sequelizeSeed.query(`select APP_FIRMNAME,LIC_NO from dafpSeed.dbo.[SEED_LIC_DIST] where LIC_NO=:licno`, {
                        replacements: { licno: e.SALE_TO }, type: sequelizeSeed.QueryTypes.SELECT
                    });
                    e.APP_FIRMNAME = result[0].APP_FIRMNAME
                    cashmemeodetails.push(e);
                }
        
                resolve(cashmemeodetails);
            }
            else{
                resolve(response.rows)
            }
           
        }
      
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    }
});
exports.FillLots = (userID) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `SELECT A."SLNO", A."FROM_TYPE", A."TO_TYPE", A."FIN_YEAR", A."SEASSION", A."LOT_NO", A."QTY", A."SOURCE", B."Crop_Name" ,C."Variety_Name",    
        CASE   
        WHEN A."IS_OSSC" = 0 THEN 'NEW'   
        WHEN A."IS_OSSC" = 1 THEN 'APPROVED'   
        WHEN A."IS_OSSC" =  2 THEN 'REJECTED'   
        END "IS_OSSC", A."OSSC_ON",   
        CASE   
        WHEN A."IS_DEPT" = 0 THEN 'NEW'   
        WHEN A."IS_DEPT" = 1 THEN 'APPROVED'   
        WHEN A."IS_DEPT" =  2 THEN 'REJECTED'   
        END "IS_DEPT", A."DEPT_ON",   
        CASE   
        WHEN A."IS_OSSOPCA" = 0 THEN 'PENDING'   
        WHEN A."IS_OSSOPCA" = 1 THEN 'APPROVED'   
        WHEN A."IS_OSSOPCA" =  2 THEN 'REJECTED'   
        END "IS_OSSOPCA", A."OSSOPCA_ON", A."OSSOPCAREASON", A."UPDATED_BY", A."UPDATED_ON", A."IS_ACTIVE" FROM "CLASS_CHANGE" A   
        INNER JOIN "mCrop" B ON A."Crop_Code" = B."Crop_Code"   
        INNER JOIN "mCropVariety" C ON A."Variety_Code" = C."Variety_Code"   
        WHERE ('${userID}' IS NULL OR A."UPDATED_BY" ='${userID}')  AND A."IS_ACTIVE" = 1  
        ORDER BY A."OSSC_ON", B."Crop_Name" ,C."Variety_Name" `;
        const values = [];
        const response = await client.query(query, values);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.FillCrop = () => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `select "Crop_Code","Crop_Name" from "mCrop" where "IS_ACTIVE"=1 order by "Crop_Name"`;
        const values = [];
        const response = await client.query(query, values);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.FillVariety = (cropCode) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `select "Variety_Name","Variety_Code" from "mCropVariety" where "IS_ACTIVE"=1 and "mCropVariety"."Crop_Code"=$1 order by "Variety_Name"`;
        const values = [cropCode];
        const response = await client.query(query, values);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.addinClass = (data) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { console.log(`Unable to connect to the database: ${err}`); });
    try {
        const query = `INSERT INTO "CLASS_CHANGE"  
        ("FROM_TYPE", "TO_TYPE", "Crop_Code", "Variety_Code", "LOT_NO", "QTY", "SOURCE", "IS_OSSC", "OSSC_ON", "UPDATED_BY", "UPDATED_ON","IS_ACTIVE","IS_DEPT","IS_OSSOPCA" ) values ($1, $2, $3, $4, $5, $6, $7, $8, $9, $10,$11,$12,$13,$14)`;
        const values = ['Foundation', 'Certified', data.SelectedCrop, data.SelectedVariety, data.SelectedLot, data.SelectedQty, data.SelectedSource, 1, 'now()', data.UPDATED_BY, 'now()', 1, 0, 0];
        await client.query(query, values);
        resolve(true)
    } catch (e) {
        console.log(`Oops! An error occurred: ${e}`);
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
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.fillGodownwisestock = (data) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = ` SELECT  DISTINCT SD."Dist_Code", "Dist_Name",SSD."Crop_Verid",SCM."Variety_Name",SUM(cast("Avl_Quantity" as decimal)) AS "STOCK",SGM."Godown_Name",SSD."Godown_ID"          
        FROM "Stock_StockDetails" SSD          
        INNER JOIN "Stock_District" SD ON SD."Dist_Code"=SSD."Dist_Code"         
        INNER JOIN  "Stock_Godown_Master" SGM ON SGM."Godown_ID"=SSD."Godown_ID"    AND SGM."Dist_Code"=SSD."Dist_Code"    
        INNER JOIN "mCropVariety" SCM ON SCM."Variety_Code"=SSD."Crop_Verid"          
        WHERE SSD."FIN_YR"=$1       
        AND SSD."User_Type"='OSSC'          
        AND SSD."CropCatg_ID"= $2       
        AND SSD."Crop_ID"=$3       
        AND SD."LGDistrict"=$4    
        AND ($5 ='0' or $5 is null or SSD."SEASSION_NAME"=$5 )      
        GROUP BY SD."Dist_Code", "Dist_Name",SSD."Crop_Verid",SCM."Variety_Name",SGM."Godown_Name" ,SSD."Godown_ID" order by SCM."Variety_Name"`;
        const values = [data.SelectedFinancialYear, data.SelectedCropCatagory, data.SelectedCrop, data.DIST_CODE, data.SelectedSeason];
        const response = await client.query(query, values);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.receivedetails = (data) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `SELECT SR."Godown_ID","RECVTRANSID",  "Challan_No" AS "PR_Chalan", "Lot_No", "Bag_Size_In_kg", "Recv_No_Of_Bags","Recv_Date", "Recv_Quantity",  "EntryDate",sam."AgenciesName",cm."Crop_Name",cv."Variety_Name",sg."Godown_Name"
        from "Stock_ReceiveDetails" SR    
        INNER JOIN "Stock_Agencies_Master" sam on sr."AgenciesID"=sam."AgenciesID"    
        INNER JOIN "mCrop" cm on cm."Crop_Code"=sr."Crop_ID"    
        INNER JOIN "mCropVariety" cv on cv."Variety_Code"=sr."Crop_Verid"    
        INNER JOIN "Stock_Godown_Master" sg on sg."Godown_ID"=sr."Godown_ID"  
        inner join "Stock_District" b on SR."Dist_Code" = b."Dist_Code"
        WHERE b."LGDistrict" ='${data.distCode}'    
        AND SR."User_Type"='OSSC'    
        AND "Recv_Date">= '${data.selectedFromDate}'  
        AND "Recv_Date"<='${data.selectedToDate}'
        AND ('${data.SelectedGodown}'=0 OR SR."Godown_ID"='${data.SelectedGodown}')  
        AND ('${data.SelectedSeason}' is null or SR."SEASSION_NAME"='${data.SelectedSeason}')  
        order by "Recv_Date"   desc,"Challan_No", cm."Crop_Name",cv."Variety_Name"`;
        const values = [];
        const response = await client.query(query, values);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    }
});
exports.dateWiseSaleDetails = (data) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `SELECT "SALETRANSID",  "CASH_MEMO_NO", "LOT_NUMBER", "BAG_SIZE_KG", "SALE_NO_OF_BAG",                      
        "SALE_DATE",cm."Crop_Name",cv."Variety_Name",sg."Godown_Name", 
        (cast("BAG_SIZE_KG" as decimal)* cast("SALE_NO_OF_BAG" as decimal))/100   AS "Sale_Quantity",
        --ISNULL((CONVERT(DECIMAL(10,2),BAG_SIZE_KG) * CONVERT(DECIMAL(10,2),SALE_NO_OF_BAG))/100,0) AS Sale_Quantity,
        sgM."Godown_Name" AS "SALETO" , "SUPPLY_TYPE" ,sr."SALE_TO" ,           
        CASE WHEN  "SUPPLY_TYPE" IN ('8','3') THEN SGM."Godown_Name"         
        --WHEN "SUPPLY_TYPE" IN ('1','6','9','12') THEN D."LIC_NO"+'/DA & FP(O) ( '+D."APP_FIRMNAME"+' )'         
        ELSE   ST."SUPPLY_NAME" END AS "SUPPLY_NAME"              
        FROM "Stock_SaleDetails" sr                          
        LEFT JOIN  "Stock_Godown_Master" sgM ON "SALE_TO"= sgM."Godown_ID"                 
        INNER JOIN "mCrop" cm on cm."Crop_Code"=sr."CROP_ID"                          
        INNER JOIN "mCropVariety" cv on cv."Variety_Code"=sr."CROP_VERID"                         
        INNER JOIN "Stock_Godown_Master" sg on sg."Godown_ID"=sr."GODOWN_ID"                        
        left JOIN  "Stock_SupplyType" ST ON ST."SUPPLY_ID"=cast(sr."SUPPLY_TYPE" as INTEGER) 
        inner join "Stock_District" b on sg."Dist_Code" = b."Dist_Code"
        --LEFT OUTER JOIN [DAFPSEED].[DBO].[SEED_LIC_DIST] D ON SR.SALE_TO = D.LIC_NO  
        WHERE  b."LGDistrict" ='${data.distCode}'                           
        AND sr."USER_TYPE"='OSSC'     
        AND "SALE_DATE" BETWEEN '${data.selectedFromDate}'  AND '${data.selectedToDate}'   
        AND ('${data.SelectedGodown}'=0 OR sr."GODOWN_ID"='${data.SelectedGodown}')     
        AND ('${data.SelectedSeason}' is null or sr."SEASONS"='${data.SelectedSeason}')                     
        order by "SALE_DATE" desc,"CASH_MEMO_NO", cm."Crop_Name",cv."Variety_Name"   `;
        const values = [];
        const response = await client.query(query, values);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    }
});
exports.dateWiseSaleDetailswithdealerdata = (data) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const promises = data.map(async (e) => {
            if (e.SUPPLY_TYPE == '1' || e.SUPPLY_TYPE == '6' || e.SUPPLY_TYPE == '9' || e.SUPPLY_TYPE == '12') {
                const result = await sequelizeSeed.query(`select LIC_NO+'/DA & FP(O) ('+APP_FIRMNAME+')' as "SUPPLY_NAME" from dafpSeed.dbo.SEED_LIC_DIST where LIC_NO=:SALE_TO`, {
                    replacements: { SALE_TO: e.SALE_TO },
                    type: sequelizeSeed.QueryTypes.SELECT
                });
                if(result.length > 0)
                e.SUPPLY_NAME = result[0].SUPPLY_NAME;
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
exports.dateWiseGodownTransferDetails = (data) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `SELECT  A."CASH_MEMO_NO",B."Godown_Name" as "FROM_GODOWN",C."Godown_Name" as "TO_GODOWN" FROM "Stock_SaleDetails" A LEFT OUTER JOIN "Stock_Godown_Master" B ON A."GODOWN_ID" = B."Godown_ID"
        LEFT OUTER JOIN "Stock_Godown_Master" C ON A."SALE_TO" = C."Godown_ID" 
        inner join "Stock_District" d on b."Dist_Code" = d."Dist_Code"
        WHERE d."LGDistrict" = $1
        AND A."SALE_DATE" =$2
        AND A."SUPPLY_TYPE" IN ('3','8') AND A."USER_TYPE" ='OSSC'
        GROUP BY A."CASH_MEMO_NO",B."Godown_Name",C."Godown_Name"`;
        const values = [data.distCode, data.selectedFromDate];
        const response = await client.query(query, values);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    }
});
exports.saledetails = (data) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `SELECT A."CASH_MEMO_NO",A."SALE_TO",A."DD_NUMBER",SUM(A."AMOUNT") as "AMOUNT"  FROM "Stock_SaleDetails" A LEFT OUTER JOIN "Stock_Godown_Master" B ON A."GODOWN_ID" = B."Godown_ID" 
		inner join "Stock_District" d on B."Dist_Code" = d."Dist_Code"
        WHERE d."LGDistrict" = $1 AND A."SALE_DATE" =$2 AND A."SUPPLY_TYPE" IN ('1','6','9') AND A."USER_TYPE" = 'OSSC' GROUP BY A."CASH_MEMO_NO",A."SALE_TO",A."DD_NUMBER"`;
        const values = [data.distCode, data.selectedFromDate];
        const response = await client.query(query, values);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    }
});
exports.saledetailswithdealerdata = (data) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const promises = data.map(async (e) => {
            const result = await sequelizeSeed.query(`select APP_FIRMNAME as "APP_FIRMNAME" from dafpSeed.dbo.SEED_LIC_DIST where LIC_NO=:SALE_TO`, {
                replacements: { SALE_TO: e.SALE_TO },
                type: sequelizeSeed.QueryTypes.SELECT
            });
            e.APP_FIRMNAME = result[0].APP_FIRMNAME;
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
exports.getGodownmaster = (data) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `SELECT "Godown_ID","Godown_Name" FROM public."Stock_Godown_Master" WHERE "Godown_ID" = $1 AND "IsActive"='Y'`;
        const values = [data.SALE_TO];
        const response = await client.query(query, values);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    }
});
exports.GetDistCodeFromDist = (data) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const result = await sequelizeSeed.query(`select Short_Name from [dafpSeed].[DBO].dist where LGDistrict =:userid`, {
                replacements: { userid: data.distCode }, type: sequelizeSeed.QueryTypes.SELECT
            });
            resolve(result[0]);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    }
});
exports.paymentStatusByFarmeId = (data) => new Promise(async (resolve, reject) => { 
    var con = new sqlstock.ConnectionPool(locConfigdafpSeeds);
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
exports.submitSeedSubsidyOfGrountnut = (data) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { console.log(`Unable to connect to the database: ${err}`); });
    try {
        const query = `INSERT INTO public."TblSeedSubsidy"(
            "FarmerId", "BagSize", "noOfBags", "Quintal", "Source", "Amount", "insertedBy", "dateOfInsert", is_active, sceme_status) values ($1, $2, $3, $4, $5, $6, $7, $8, $9, $10)`;
        const values = [data.farmerid,  data.bagSize, data.noOfBags, data.Quintal, data.Source, data.Amount,data.UPDATED_BY, 'now()', 1, 1];
        await client.query(query, values);
        resolve(true)
    } catch (e) {
        console.log(`Oops! An error occurred: ${e}`);
    } finally {
        client.release();
    }
});