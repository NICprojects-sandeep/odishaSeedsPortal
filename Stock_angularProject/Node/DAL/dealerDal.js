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
        let query = ``;
        let values = []
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
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `select "Stock_ID",b."Receive_Unitcd",c."REF_NO","Receive_Unitname","Lot_No","Bag_Size_In_kg",CAST(A."Avl_Quantity"/CAST(A."Bag_Size_In_kg" as INT)*100 AS INT) "RECV_NO_OF_BAGS","Avl_Quantity","Crop_Verid",a."Godown_ID",a."Class",d."All_in_cost_Price" from public."Stock_StockDetails" a
        left outer join public."Stock_Receive_Unit_Master" b on a."Receive_Unitcd"= b."Receive_Unitcd"
        left join "mMouData" c on a."MOU_REFNO"= c."REF_NO"
        left join "Stock_Pricelist" d on a."Crop_Verid" = d."Crop_Vcode" and d."F_Year"=(select "FIN_YR" from "mFINYR" where "IS_ACTIVE"=1) and d.seasons=(select "SHORT_NAME" from "mSEASSION" where "IS_ACTIVE"=1)
        where a."Godown_ID"=$4 and a."CropCatg_ID"=$3 and a."Crop_ID"=$2 and a."Crop_Verid"=$1 and a."User_Type"='OSSC' and a."AVL_NO_OF_BAGS">0 and a."VALIDITY"='True' and "EXPIRY_DATE"> CURRENT_TIMESTAMP and "Class" in('Certified','TL')`;
        const values = [data.selectedVariety, data.selectedCrop, data.selectedCategory, data.selectedGodown];
        console.log(query, values);
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

        // data.VALUES.forEach(e => {
        var count = 1
        for (const e of data.VALUES) {
            var PRICE_RECEIVE_UNITCD = '';
            var Class_BagSize = '';
            var mCROP_CLASS = '';
            var mBAG_SIZE = '';
            var m_AMOUNT = '';
            var mAMOUNT = '';
            var AVL_NOofBags_Quantity = '';
            var AVL_NO_OF_BAGS = 0;
            var AVL_QUANTITY = 0.00;
            var OSSC_PACS_OSSC_DEALER = '';
            var OSSC_PACS = '';
            var OSSC_DEALER = '';
            var mALINCOST = '';
            var mSALETRANSID = '';
            var PREBOOKING_AMT = 0.00;
            var insertintostocksaledetails = ''
            var All_in_cost_Price_check = '';
            var mTOT_SUB_AMT = 0.00;
            PRICE_RECEIVE_UNITCD = await client.query(`SELECT "PRICE_RECEIVE_UNITCD" FROM "Price_SourceMapping" WHERE "RECEIVE_UNITCD" = '${e.Receive_Unitcd}' AND "SEASSION" =  '${data.SEASSION}' AND "FIN_YR" = '${data.FIN_YR}';`)

            Class_BagSize = await client.query(`SELECT "Class","Bag_Size_In_kg" FROM "Stock_StockDetails" WHERE "Lot_No" = '${e.LOT_NO}' AND "Crop_ID" =  '${e.CROP_ID}' AND "Crop_Verid" ='${e.CROP_VERID}' `)
            mCROP_CLASS = Class_BagSize.rows[0].Class;
            mBAG_SIZE = Class_BagSize.rows[0].Bag_Size_In_kg;
            m_AMOUNT = await client.query(`SELECT "All_in_cost_Price" FROM "Stock_Pricelist" WHERE "Crop_class" = 'Certified' AND "RECEIVE_UNITCD" = '${PRICE_RECEIVE_UNITCD.rows[0].PRICE_RECEIVE_UNITCD}' AND "Crop_Vcode" = '${e.CROP_VERID}' AND "Crop_Code" = '${e.CROP_ID}' AND "seasons" = '${data.SEASSION}' AND "F_Year" = '${data.FIN_YR}'`);
            mAMOUNT = m_AMOUNT.rows[0].All_in_cost_Price;
            AVL_NOofBags_Quantity = await client.query(`SELECT "AVL_NO_OF_BAGS","Avl_Quantity" FROM "Stock_StockDetails" WHERE "Crop_Verid" ='${e.CROP_VERID}' AND "Class" = '${mCROP_CLASS}' AND "Receive_Unitcd" = '${e.Receive_Unitcd}' AND "Lot_No" = '${e.LOT_NO}' AND "Bag_Size_In_kg" = '${mBAG_SIZE}' AND "User_Type" = 'OSSC' AND "Godown_ID" = '${data.GODOWN_ID}' AND "VALIDITY" = 'true'`)
            AVL_NO_OF_BAGS = AVL_NOofBags_Quantity.rows[0].AVL_NO_OF_BAGS;
            AVL_QUANTITY = AVL_NOofBags_Quantity.rows[0].Avl_Quantity;
            if (data.PrebookingorNot) {
                PREBOOKING_AMT = ((parseInt(mAMOUNT) * parseFloat(TotalNoOfQuantity)) * 10) / 100
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
                    data.UPDATED_BY, 'now()', 'OSSC', data.ipAdress, 'Y', PREBOOKING_AMT, data.applicationId
                ];
                insertintostocksaledetails = await client.query(query, values);
                if (insertintostocksaledetails.rowCount == 1) {
                    if (data.PrebookingorNot) {
                        let updateinprebookinglist = await client.query(`update prebookinglist set "TRANSACTION_ID" = ${CASH_MEMO_NO} ,"IS_ACTIVE"='0',"noofBagSale" = ${TotalNoOfBags} ,"saleAmount"=${PREBOOKING_AMT} 
                    where  "applicationID" >= ${data.applicationId}`);
                    }
                    let updateinStock_StockDetails = await client.query(`update "Stock_StockDetails" set "AVL_NO_OF_BAGS" = "AVL_NO_OF_BAGS" -${e.NO_OF_BAGS} ,"Avl_Quantity"="Avl_Quantity"-${e.QUANTITY} 
                    where "Lot_No"='${e.LOT_NO}'  and "Godown_ID"='${e.Godown_ID}' and "Crop_Verid" = '${e.CROP_VERID}' AND "Class" ='${e.Class}' AND "Receive_Unitcd" = '${e.Receive_Unitcd}' and "AVL_NO_OF_BAGS" >= ${e.NO_OF_BAGS}`);
                    if (data.SUPPLY_TYPE == '1' || data.SUPPLY_TYPE == '6' || data.SUPPLY_TYPE == '9') {
                        All_in_cost_Price_check = await client.query(`SELECT "All_in_cost_Price" FROM "Stock_Pricelist" WHERE "Crop_class" = '${e.Class}' AND "RECEIVE_UNITCD" = '${PRICE_RECEIVE_UNITCD.rows[0].PRICE_RECEIVE_UNITCD}' AND "Crop_Vcode" = '${e.CROP_VERID}' AND "Crop_Code" = '${e.CROP_ID}' AND seasons = '${data.SEASSION}' AND "F_Year" = '${data.FIN_YR}'`);
                        console.log(All_in_cost_Price_check.rows);
                        if (All_in_cost_Price_check.rows.length > 0) {
                            mAMOUNT = All_in_cost_Price_check.rows[0].All_in_cost_Price
                        }
                        else {
                            mAMOUNT = 0;
                            mTOT_SUB_AMT = 0;
                        }
                    }
                }
            }
        }

        // });
    }
    // const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    // try {
    //     const query = `select "SUPPLY_ID","SUPPLY_NAME" from public."Stock_SupplyType" where "USER_TYPE"='OSSC' and "ISACTIVE" = 'Y' ORDER BY "ORDER_NO"`;
    //     const values = [];
    //     const response = await client.query(query, values);
    //     resolve(response.rows);
    // } catch (e) {
    //     reject(new Error(`Oops! An error occurred: ${e}`));
    // } finally {
    //     client.release();
    // }
});