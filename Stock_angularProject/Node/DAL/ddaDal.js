var dbConfig = require('../config/dbSqlConnection');
var sqlstock = dbConfig.sqlstock;
var sequelizeSeed = dbConfig.sequelizeSeed;
var locConfigdafpSeeds = dbConfig.locConfigdafpSeeds;
var locConfigfarmerDB = dbConfig.locConfigFarmerDB;
var sequelizeStock = dbConfig.sequelizeStock;
var locConfigstock = dbConfig.locConfigStock;
const format = require('pg-format');
const pool = require('../config/dbConfig');



exports.addActivityLog = async (action, attack, mode, userID, ipAddress, url, deviceType, os, browser,Message) => {
    const client = await pool.connect().catch((err) => { console.log(`Unable to connect to the database: ${err}`); });
    try {
      const query = `insert into "ActivityLog" ("IPAddress", "UserID", "URL", "DeviceType", "OS", "Browser", "DateTime", "Action", "Attack", "Mode","Message") values ($1, $2, $3, $4, $5, $6, $7, $8, $9, $10,$11)`;
      const values = [ipAddress, userID, url, deviceType, os, browser, 'now()', action, attack, mode,Message];
      await client.query(query, values);
    } catch (e) {
      console.log(`Oops! An error occurred: ${e}`);
    } finally {
      client.release();
    }
  };
  
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
exports.fillCurrentstockPosition = (data) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = ` SELECT  DISTINCT SD."Dist_Code", "Dist_Name",SSD."Crop_Verid",SCM."Variety_Name",SUM(cast("Avl_Quantity" as decimal)) AS "STOCK"    
        FROM "Stock_StockDetails" SSD           
        INNER JOIN "Stock_District" SD ON SD."Dist_Code"=SSD."Dist_Code"         
        INNER JOIN  "Stock_Godown_Master" SGM ON SGM."Godown_ID"=SSD."Godown_ID"    AND SGM."Dist_Code"=SSD."Dist_Code"    
        INNER JOIN "mCropVariety" SCM ON SCM."Variety_Code"=SSD."Crop_Verid"          
        WHERE SSD."FIN_YR"=$1      
        AND SSD."User_Type"=$4          
        AND SSD."CropCatg_ID"= $2       
        AND SSD."Crop_ID"=$3   
        AND ($5 ='0' or $5 is null or SSD."SEASSION_NAME"=$5 )  and "LGDistrict"=$6     
        GROUP BY SD."Dist_Code", "Dist_Name",SSD."Crop_Verid",SCM."Variety_Name" order by "Dist_Name",SCM."Variety_Name"`;
        const values = [data.SelectedFinancialYear, data.SelectedCropCatagory, data.SelectedCrop, data.SelectedAgency, data.SelectedSeason,data.lgdistrict]
        const response = await client.query(query, values);
        resolve(response.rows);
    } catch (e) {
        await client.query('rollback');
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.fillGodownWiseStock = (data) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = ` SELECT  DISTINCT SD."Dist_Code", "Dist_Name",SSD."Crop_Verid",SCM."Variety_Name",SUM(cast("Avl_Quantity" as decimal)) AS "STOCK",SGM."Godown_Name",SSD."Godown_ID"  ,SUM(cast("Avl_Quantity" as decimal)) AS STOCK
        FROM "Stock_StockDetails" SSD              
        INNER JOIN "Stock_District" SD ON SD."Dist_Code"=SSD."Dist_Code"         
        INNER JOIN  "Stock_Godown_Master" SGM ON SGM."Godown_ID"=SSD."Godown_ID"    AND SGM."Dist_Code"=SSD."Dist_Code"    
        INNER JOIN "mCropVariety" SCM ON SCM."Variety_Code"=SSD."Crop_Verid"          
        WHERE SSD."FIN_YR"=$1      
        AND SSD."User_Type"=$4          
        AND SSD."CropCatg_ID"= $2       
        AND SSD."Crop_ID"=$3   
        AND ($5 ='0' or $5 is null or SSD."SEASSION_NAME"=$5 )      
        and SD."Dist_Code"=$6
        GROUP BY SD."Dist_Code", "Dist_Name",SSD."Crop_Verid",SCM."Variety_Name",SGM."Godown_Name",SSD."Godown_ID"  order by "Dist_Name",SCM."Variety_Name"`;
       
        const values = [data.SelectedFinancialYear, data.SelectedCropCatagory, data.SelectedCrop, data.SelectedAgency, data.SelectedSeason,data.DistCode]
        const response = await client.query(query, values);
        resolve(response.rows);
    } catch (e) {
        await client.query('rollback');
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.FillDistrict = () => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `select "Dist_Code","Dist_Name","LGDistrict" from "Stock_District" order by "Dist_Name"`;
        const response = await client.query(query);
        resolve(response.rows);
    } catch (e) {
        await client.query('rollback');
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.fillStateStockPosition = (data) => new Promise(async (resolve, reject) => {
   
    if (data.selectedToDate==''){
        data.selectedToDate=null
    }
    if(data.SelectedDistrict==0){
        data.SelectedDistrict=null
    }
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `SELECT DISTINCT SD."Dist_Code", "Dist_Name",
        CAST(
            ROUND(
                (COALESCE("OSSC_Recv", 0) - COALESCE("OSSC_GtransOwnTr", 0) - COALESCE("OSSC_OthrGtransOwnTr", 0)),
                2
            ) AS DECIMAL(10, 2)
        ) AS "OSSC_Recv",
        CAST(  round(COALESCE("OSSC_SaleDealer" ,0),2)AS DECIMAL(10, 2)) "OSSC_SaleDealer",
        CAST(round(COALESCE("OSSC_SalePacks",0),2)AS DECIMAL(10, 2)) "OSSC_SalePacks",   
        CAST(round(COALESCE("OSSC_Stock",0),2)AS DECIMAL(10, 2)) "OSSC_Stock",   
		CAST(round(COALESCE("OSSC_Recv",0),2)AS DECIMAL(10, 2)) AS   "OSSC_Recv1",
        CAST( round(COALESCE("OSSC_Gtrans",0),2)AS DECIMAL(10, 2))    "OSSC_Gtrans1",
         CAST(round(COALESCE("OSSC_GtransOwnTr",0),2)AS DECIMAL(10, 2)) "OSSC_GtransOwnTr",
         CAST(round(COALESCE("OSSC_GtransOwnTrPend",0),2)AS DECIMAL(10, 2)) "OSSC_GtransOwnTrPend",  
       CAST( round(COALESCE("OSSC_OthrGtransOwnTr",0),2)AS DECIMAL(10, 2)) "OSSC_OthrGtransOwnTr",
       CAST( round(COALESCE("OSSC_OthrGtransOwnTrPend",0),2)AS DECIMAL(10, 2)) "OSSC_OthrGtransOwnTrPend"
          
        
        
        FROM "Stock_District" SD   
        LEFT JOIN  
        (   
        SELECT "Dist_Code",SUM(cast("Bag_Size_In_kg" as decimal)*cast("Recv_No_Of_Bags" as decimal)/100)  "OSSC_Recv"  
        FROM "Stock_ReceiveDetails"  WHERE "FIN_YR"=$1  AND "CropCatg_ID"=$2   AND "Crop_ID"=$3 AND "User_Type"='OSSC'  AND ($4::text is null or $4::text='0' or "SEASSION_NAME"=$4::text )    AND ($5::timestamp IS NULL OR "EntryDate"<=$5::timestamp)  GROUP BY "Dist_Code"                        
          
        ) AS TBL11 ON TBL11."Dist_Code"=SD."Dist_Code"       
        LEFT JOIN 
        (                        
          
        SELECT "Dist_Code",SUM(cast("BAG_SIZE_KG" as decimal)*cast("SALE_NO_OF_BAG" as decimal)/100 )AS "OSSC_SaleDealer"                        
          
        FROM "Stock_SaleDetails"  SS 
        INNER JOIN "Stock_Godown_Master" GM ON SS."GODOWN_ID"=GM."Godown_ID"   
        WHERE "F_YEAR"=$1  AND "CROP_ID"=$3  
        AND SS."USER_TYPE"='OSSC' AND "SUPPLY_TYPE"='6' --and CONFIRM_STATUS=1 AND STATUS='S'      
        AND ($4::text is null or $4::text='0' or "SEASONS"=$4::text )    
        AND ($5::timestamp IS NULL OR SS."UPDATED_ON"<=$5::timestamp)                         
        GROUP BY "Dist_Code"                        
          
        ) AS TBL12 ON TBL12."Dist_Code"=SD."Dist_Code"                       
          
        LEFT JOIN                        
          
        (                        
          
        SELECT "Dist_Code",SUM(cast("BAG_SIZE_KG" as decimal)*cast("SALE_NO_OF_BAG" as decimal)/100) AS "OSSC_SalePacks"                        
          
        FROM "Stock_SaleDetails"  SS                      
          left join "Stock_UserProfile" b on b."UserId"=ss."UPDATED_BY"
        --LEFT OUTER JOIN [DAFPSEED].[DBO].[SEED_LIC_DIST] B ON SS.SALE_TO = B.LIC_NO        
          
        WHERE "CROP_ID"=$3    and "F_YEAR"=$1  AND SS."USER_TYPE"='OSSC' AND "SUPPLY_TYPE"='9'  AND ($4::text is null or $4::text='0' or "SEASONS"=$4::text  )  AND ($5::timestamp IS NULL OR SS."UPDATED_ON"<=$5::timestamp)  GROUP BY "Dist_Code"                        
          
        ) AS TBL121 ON TBL121."Dist_Code"=SD."Dist_Code"       
        LEFT JOIN                        
        (                        
        SELECT GM1."Dist_Code",SUM(cast("BAG_SIZE_KG" as decimal)*cast("SALE_NO_OF_BAG" as decimal)/100) AS "OSSC_Salegodwn"                        
        FROM "Stock_SaleDetails"  SS                      
        INNER JOIN "Stock_Godown_Master" GM ON SS."GODOWN_ID"=GM."Godown_ID"               
        INNER JOIN "Stock_Godown_Master" GM1 ON SS."SALE_TO"=GM1."Godown_ID" AND GM."Dist_Code"=GM1."Dist_Code"       
        WHERE "CROP_ID"=$3    and "F_YEAR"=$1    
        AND SS."USER_TYPE"='OSSC' AND "SUPPLY_TYPE"='8'-- and LOT_NUMBER='Dec/15-18-150-02G13952-1'       
        AND ($4::text is null or "SEASONS"=$4::text  )     
        AND ($5::timestamp IS NULL OR SS."UPDATED_ON"<=$5::timestamp)      
             
        GROUP BY GM1."Dist_Code"                   
        ) AS TBL3 ON TBL3."Dist_Code"=SD."Dist_Code"  
        LEFT JOIN     
        (    
        SELECT GM."Dist_Code",SUM(cast("BAG_SIZE_KG" as decimal)*cast("SALE_NO_OF_BAG" as decimal)/100) AS "OSSC_Gtrans"   
        FROM "Stock_SaleDetails"   SS   
        INNER JOIN "Stock_Godown_Master" GM ON SS."GODOWN_ID"=GM."Godown_ID"  
        INNER JOIN "Stock_Godown_Master" GM1 ON SS."SALE_TO"=GM1."Godown_ID" AND GM."Dist_Code"<>GM1."Dist_Code" 
        WHERE "F_YEAR"=$1    
        AND "CROPCATG_ID"=$2  
        AND "CROP_ID"=$3  
        AND SS."USER_TYPE"='OSSC' AND "CONFIRM_STATUS"=1 AND "STATUS"='T'  
        AND ($4::text is null or $4::text='0' or "SEASONS"=$4::text ) 
        AND ($5::timestamp IS NULL OR SS."UPDATED_ON"<=$5::timestamp)                    
          
        GROUP BY GM."Dist_Code"
        ) AS TBL13 ON TBL13."Dist_Code"=SD."Dist_Code"                     
          
        LEFT JOIN  
        (  
        SELECT "Dist_Code",SUM(cast("Bag_Size_In_kg" as decimal)*cast("AVL_NO_OF_BAGS" as decimal)/100) AS "OSSC_Stock"  
        FROM "Stock_StockDetails"  
        WHERE "FIN_YR"=$1 AND "CropCatg_ID"=$2 AND "Crop_ID"=$3 AND "User_Type"='OSSC' AND ($4::text is null or $4::text='0' or  "SEASSION_NAME"=$4::text ) AND ($5::timestamp IS NULL OR "EntryDate"<=$5::timestamp) 
        GROUP BY "Dist_Code" 
        ) AS TBL1 ON TBL1."Dist_Code"=SD."Dist_Code"   
        LEFT JOIN                        
          
        (  
        SELECT GM."Dist_Code",SUM(cast("BAG_SIZE_KG" as decimal)*cast("SALE_NO_OF_BAG" as decimal)/100) AS "OSSC_GtransOwnTr" 
        FROM "Stock_SaleDetails"  SS 
        INNER JOIN "Stock_Godown_Master" GM ON SS."GODOWN_ID"=GM."Godown_ID" 
        INNER JOIN "Stock_Godown_Master" GM1 ON SS."SALE_TO"=GM1."Godown_ID" AND GM."Dist_Code"=GM1."Dist_Code"  
        WHERE "F_YEAR"=$1  AND "CROPCATG_ID"=$2 AND "CROP_ID"=$3 AND SS."USER_TYPE"='OSSC' AND "CONFIRM_STATUS"=1  AND "STATUS"='T' AND "SUPPLY_TYPE"='8'  
        AND ($4::text is null or $4::text='0' or "SEASONS"=$4::text  )   AND ($5::timestamp IS NULL OR SS."UPDATED_ON" <=$5::timestamp) 
        GROUP BY GM."Dist_Code" 
        ) AS TBL23411 ON TBL23411."Dist_Code"=SD."Dist_Code"                       
         LEFT JOIN    
        (
        SELECT GM."Dist_Code",SUM(cast("BAG_SIZE_KG" as decimal)*cast("SALE_NO_OF_BAG" as decimal)/100) AS "OSSC_GtransOwnTrPend"  
        FROM "Stock_SaleDetails"  SS 
        INNER JOIN "Stock_Godown_Master" GM ON SS."GODOWN_ID"=GM."Godown_ID"
        INNER JOIN "Stock_Godown_Master" GM1 ON SS."SALE_TO"=GM1."Godown_ID" AND GM."Dist_Code"=GM1."Dist_Code"   
        WHERE "F_YEAR"=$1 AND "CROPCATG_ID"=$2   AND "CROP_ID"=$3 AND SS."USER_TYPE"='OSSC' AND "CONFIRM_STATUS"<>1  AND "STATUS"='T' AND "SUPPLY_TYPE"='8'  AND ($4::text is null or $4::text='0' or "SEASONS"=$4::text  )  AND ($5::timestamp IS NULL OR SS."UPDATED_ON" <=$5::timestamp)    
        GROUP BY GM."Dist_Code"                 
          
        ) AS TBL23511 ON TBL23511."Dist_Code"=SD."Dist_Code"      
        LEFT JOIN  
        ( 
        SELECT GM."Dist_Code",SUM(cast("BAG_SIZE_KG" as decimal)*cast("SALE_NO_OF_BAG" as decimal)/100) AS "OSSC_OthrGtransOwnTr" 
        FROM "Stock_SaleDetails"  SS    
        INNER JOIN "Stock_Godown_Master" GM ON SS."GODOWN_ID"=GM."Godown_ID"    
        INNER JOIN "Stock_Godown_Master" GM1 ON SS."SALE_TO"=GM1."Godown_ID" AND GM."Dist_Code"<>GM1."Dist_Code" 
        WHERE "F_YEAR"=$1   AND "CROPCATG_ID"=$2  AND "CROP_ID"=$3   AND SS."USER_TYPE"='OSSC' AND "CONFIRM_STATUS"=1  AND "STATUS"='T' AND "SUPPLY_TYPE"='8'    
        AND ($4::text is null or $4::text='0' or "SEASONS"=$4::text  )  AND ($5::timestamp IS NULL OR SS."UPDATED_ON" <=$5::timestamp) 
        GROUP BY GM."Dist_Code"                        
          
        ) AS TBL234111 ON TBL234111."Dist_Code"=SD."Dist_Code"                       
         LEFT JOIN  
        (  
        SELECT GM."Dist_Code",SUM(cast("BAG_SIZE_KG" as decimal)*cast("SALE_NO_OF_BAG" as decimal)/100) AS "OSSC_OthrGtransOwnTrPend"   FROM "Stock_SaleDetails"  SS 
        INNER JOIN "Stock_Godown_Master" GM ON SS."GODOWN_ID"=GM."Godown_ID"  
        INNER JOIN "Stock_Godown_Master" GM1 ON SS."SALE_TO"=GM1."Godown_ID" AND GM."Dist_Code"<>GM1."Dist_Code"  
        WHERE "F_YEAR"=$1 AND "CROPCATG_ID"=$2 AND "CROP_ID"=$3 AND SS."USER_TYPE"='OSSC' AND "CONFIRM_STATUS"<>1  AND "STATUS"='T' AND "SUPPLY_TYPE"='8'   AND ($4::text is null or $4::text='0' or "SEASONS"=$4::text  )   AND ($5::timestamp IS NULL OR SS."UPDATED_ON" <=$5::timestamp)   
        GROUP BY GM."Dist_Code" 
        ) AS TBL235111 ON TBL235111."Dist_Code"=SD."Dist_Code"     
         
          
        WHERE ($6::text is null or SD."Dist_Code"= $6::text  )        
         
         order by "Dist_Name" `;
      
        const values = [data.SelectedFinancialYear,data.SelectedCropCatagory,data.SelectedCrop,data.SelectedSeason,data.selectedToDate,data.SelectedDistrict];
   console.log(values);
        const response = await client.query(query, values);
        resolve(response.rows);
    } catch (e) {
        await client.query('rollback');
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
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
exports.blockwisetransctiondetails = (SelectedFinancialYear,SelectedSeason,SelectedCrop,distCode) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `select j."DDA_CODE",j."DDA_NAME",j."BLK_NAME",COALESCE(a."SaleQty", 0) AS "SaleQty",j."BLK_CODE",j."AAO_CODE"                 
        from "JALANIDHI_DAO_AAO" j                      
        left join                      
        (                      
        SELECT j."DDA_CODE",j."DDA_NAME", SUM(CAST("SaleQty" AS DECIMAL(10,2))) AS "SaleQty",J."BLK_NAME","BLK_CODE" FROM "DealerwiseStock" S                        
        INNER JOIN "JALANIDHI_DAO_AAO" J ON J."AAO_CODE"=RIGHT(S."UpdatedBy",6)                      
        WHERE "PP"='1' AND ('${SelectedCrop}' is null or  "Crop_Code"='${SelectedCrop}' )       
                       and ('${SelectedFinancialYear}' is null or S."FIN_YR"='${SelectedFinancialYear}') and ('${SelectedSeason}' is null or S."SEASSION"='${SelectedSeason}'  )  
                       
        group by "BLK_NAME","BLK_CODE" ,"DDA_CODE","DDA_NAME"                     
        ) a on j."BLK_CODE"=a."BLK_CODE"   
        where ('${distCode}' is null or  j."DDA_CODE"='${distCode}') and "PP"='1' order by j."BLK_NAME"  `;
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
exports.blockwisetransctiondetails1 = (SelectedFinancialYear,SelectedSeason,SelectedCrop,extractedValue) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `SELECT  "LICENCE_NO",  SUM(CAST("STOCK_QUANTITY" AS DECIMAL(10,2))) AS RCV,
        SUM(CAST("STOCK_QUANTITY" AS DECIMAL(10,2)) - CAST("AVL_QUANTITY" AS DECIMAL(10,2))) AS SALE
    FROM "STOCK_DEALERSTOCK" A          
    WHERE (SUBSTRING(a."LICENCE_NO",3,3))='${extractedValue}'                
    AND ('${SelectedCrop}' is null or "CROP_ID" = '${SelectedCrop}' )        
    AND ('${SelectedFinancialYear}' is null or  A."FIN_YR"='${SelectedFinancialYear}')      
    AND ('${SelectedSeason}' is null or A."SEASSION"='${SelectedSeason}')                       
    GROUP BY "LICENCE_NO" `;
        const values = [];
        console.log(query);
        const response = await client.query(query, values);
        resolve(response.rows);
    } catch (e) {
        await client.query('rollback');
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.blockwisetransctiondetails2 = async (licNumbers, req, res) => {
    return new Promise(async resolve => {
        try {
            const result = await sequelizeStock.query(`select BLOCK_ID,LIC_NO from [DAFPSEED].[DBO].[SEED_LIC_DIST] a

            inner join [DAFPSEED].[DBO].[SEED_LIC_BUS_DIST] b on a.SEED_LIC_DIST_ID = b.SEED_LIC_DIST_ID
            
            where LIC_NO in (:licNumbers)  `, {
                replacements: { licNumbers: licNumbers }, type: sequelizeStock.QueryTypes.SELECT
            });
            resolve(result);

        } catch (e) {
            console.log('An error occurred...', e);
            resolve([]);
            throw e
        }
    });
};
exports.blockwiseStockDetails = async (blockcode, req, res) => {
    return new Promise(async resolve => {
        try {
            const result = await sequelizeStock.query(`SELECT A.APP_FIRMNAME,A.LIC_NO,CASE WHEN A.APPMOB_NO <>'' THEN APPMOB_NO ELSE '' END AS APPMOB_NO,CASE A.APP_TYPE WHEN 'Secretary PACS' THEN 'PACS' ELSE 'DEALER' END APP_TYPE, p.pan_name     
            FROM [DAFPSEED].[DBO].[SEED_LIC_DIST] A                                  
            LEFT OUTER JOIN [DAFPSEED].[DBO].[SEED_LIC_BUS_DIST] B ON A.SEED_LIC_DIST_ID = B.SEED_LIC_DIST_ID     
            LEFT JOIN  [STOCK].dbo.panchayat P ON P.pan_code=B.GP_ID                     
            WHERE  B.BLOCK_ID IN (select BLK_CODE from [STOCK].dbo.JALANIDHI_DAO_AAO where aao_code= :blockcode)  and LIC_NO is not null    AND B.IS_ACTIVE = 1  `, {
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

exports.blockwiseStockDetails2 = (data, selectedFinancialYear, selectedSeasons, selectedCrop) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    const licNoListString = data.map(licNo => `'${licNo}'`).join(',');
    try {
        const query = `SELECT a."LICENCE_NO", a."CROP_ID", a."FIN_YR", a."SEASSION", 
        a."ACTUAL_RECEIVE", a."ACTUAL_SALE", 
        COALESCE(b."SaleQty", 0) as "SaleQty" 
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
exports.delaeListDistWise = async (distCode, req, res) => {
    return new Promise(async resolve => {
        try {
            const result = await sequelizeStock.query(`select upper(APP_FIRMNAME)  as APP_FIRMNAME,LIC_NO,appmob_no, case when APP_TYPE='Secretary PACS' then 'PACS' ELSE 'Dealer' end as type  FROM [dafpSeed].[dbo].[SEED_LIC_DIST] where DIST_CODE=:distCode AND APP_STATUS='A' AND APR_UPTO>GETDATE() AND IS_ACTIVE=1 AND LIC_ACTIVE=1 ORDER BY APP_FIRMNAME`, {
                replacements: { distCode: distCode }, type: sequelizeStock.QueryTypes.SELECT
            });
            resolve(result);

        } catch (e) {
            console.log('An error occurred...', e);
            resolve([]);
            throw e
        }
    });
};
exports.dealerwisePaymentDetails = (LIC_NO) => new Promise(async (resolve, reject) => {
    var con = new sqlstock.ConnectionPool(locConfigstock);
    try {
        con.connect().then(function success() {
            const request = new sqlstock.Request(con);
            request.input('LIC_NO', LIC_NO);
            request.input('FrmDt', '');
            request.input('Todate', '');
            request.execute('DatewiseSale', function (err, result) {
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
exports.GetDistCodeFromDist = (data) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const result = await sequelizeSeed.query(`select Short_Name,dist_name,dist_code from [dafpSeed].[DBO].dist where LGDistrict =:userid`, {
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
exports.GetDealerSaleDetails = async (distCode, req, res) => {
    return new Promise(async resolve => {
        try {
            const result = await sequelizeStock.query(`select upper(APP_FIRMNAME)  as APP_FIRMNAME,LIC_NO  FROM [dafpSeed].[dbo].[SEED_LIC_DIST] where APP_TYPE<>'Secretary PACS' AND DIST_CODE=:distCode  ORDER BY APP_FIRMNAME`, {
                replacements: { distCode: distCode }, type: sequelizeStock.QueryTypes.SELECT
            });
            resolve(result);

        } catch (e) {
            console.log('An error occurred...', e);
            resolve([]);
            throw e
        }
    });
};

exports.GetDealerSaleDetails1 = (data, selectedFinancialYear, selectedSeasons, selectedCrop) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    const licNoListString = data.map(licNo => `'${licNo}'`).join(',');
    try {
        const query = `SELECT a."LICENCE_NO",A."CROP_VERID",V."Variety_Name"
        ,SUM("STOCK_QUANTITY") "RECEIVE_QTY",COALESCE(SUM("STOCK_QUANTITY"), 0) -COALESCE(SUM("AVL_QUANTITY"), 0) AS "SALE_QTY"
        --SUM((ISNULL("STOCK_QUANTITY",0)-ISNULL("AVL_QUANTITY",0)))  AS "SALE_QTY" --,COUNT(*) AS FARMER          
        FROM "STOCK_DEALERSTOCK" A            
               
        INNER JOIN "mCropVariety" V ON V."Variety_Code"=A."CROP_VERID"          
        WHERE A."CROP_ID"='${selectedCrop}'    
        AND  ('${selectedFinancialYear}' is null or A."FIN_YR"='${selectedFinancialYear}')  AND ('${selectedSeasons}' is null or A."SEASSION"='${selectedSeasons}') and a."LICENCE_NO" in (${licNoListString})         
        GROUP BY  a."LICENCE_NO",A."CROP_VERID",V."Variety_Name"            
        ORDER BY a."LICENCE_NO","Variety_Name" `;
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
exports.GetDealerSaleDetails2 = (data, selectedFinancialYear, selectedSeasons, selectedCrop) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    const licNoListString = data.map(licNo => `'${licNo}'`).join(',');
    try {
        const query = `select "UPDATED_BY","CROP_VERID" ,count("FARMER_ID")  from "STOCK_FARMER" a
        INNER JOIN "mCropVariety" V ON V."Variety_Code"=A."CROP_VERID"          
        WHERE A."CROP_ID"='${selectedCrop}'      
        AND  ('${selectedFinancialYear}' is null or A."FIN_YEAR"='${selectedFinancialYear}')  AND ('${selectedSeasons}' is null or A."SEASON"='${selectedSeasons}') and "UPDATED_BY" in (${licNoListString})  
        group by "UPDATED_BY","CROP_VERID" `;
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
exports.GetFarmerSaleDetails = () => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `SELECT "Dist_Code","Dist_Name","CROPCATG_ID" ,"Category_Name" ,"Crop_Code","Crop_Name","CROP_VERID","Variety_Name",SUM("SALE") AS "SALE"  FROM (  
    SELECT "Dist_Code",SD."Dist_Name",A."CROPCATG_ID",CC."Category_Name",CM."Crop_Code",CM."Crop_Name", A."CROP_VERID",CV."Variety_Name",                          
   (SUM(CAST(A."BAG_SIZE_KG" AS DECIMAL(10,2)) * CAST(A."NO_OF_BAGS" AS DECIMAL(10,2))) / 100) AS "SALE" FROM(
   SELECT "CROPCATG_ID", "CROP_ID", "CROP_VERID", "BAG_SIZE_KG", "NO_OF_BAGS", "UPDATED_BY", "FIN_YEAR", "SEASON", "USER_TYPE", "UPDATED_ON", "FARMER_ID" FROM "STOCK_FARMER")  A 
   INNER JOIN "Stock_District" SD ON  left(A."FARMER_ID",3)= left(SD."Dist_Name",3)                           
   INNER JOIN "mCropCategory" CC ON CC."Category_Code"=A."CROPCATG_ID"                            
   INNER JOIN "mCrop" CM ON CM."Crop_Code"=A."CROP_ID"                           
   INNER JOIN  "mCropVariety" CV ON CV."Variety_Code"=A."CROP_VERID"  
   WHERE A."CROP_ID"='C002'    AND (null IS NULL OR SD."Dist_Code"=null) AND  ('2024-25' is null or  A."FIN_YEAR"='2024-25') AND ('K' is null or  A."SEASON"='K') 
   GROUP BY "Dist_Code",SD."Dist_Name",A."CROPCATG_ID" ,CC."Category_Name" ,CM."Crop_Code",CM."Crop_Name",A. "CROP_VERID",CV."Variety_Name" ) AS TBL                     
   GROUP BY "Dist_Code","Dist_Name","CROPCATG_ID" ,"Category_Name" ,"Crop_Code","Crop_Name","CROP_VERID","Variety_Name" order by "Dist_Name","Variety_Name"`;
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
exports.GetFarmerSaleDetails1 = () => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `select d."Dist_Code",d."Dist_Name",COUNT(distinct A."FARMER_ID") AS NoofFarmer    
from (SELECT "CROP_ID", "FARMER_ID", "FIN_YEAR", "SEASON", "USER_TYPE", "UPDATED_ON", "UPDATED_BY" FROM "STOCK_FARMER") A
 	INNER join "Stock_District" d on (SUBSTRING(A."UPDATED_BY",3,3)=SUBSTRING(d."Dist_Name",1,3)) 
 WHERE A."CROP_ID"='C002'  AND (null IS NULL OR d."Dist_Code"=null)       
 AND  ('2024-25' is null or  A."FIN_YEAR"='2024-25') AND ('K' is null or A."SEASON"='K') AND (null IS NULL OR  d."Dist_Code"=null)    GROUP BY d."Dist_Code" ,d."Dist_Name"  order by d."Dist_Name" `;
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
// SELECT a."LICENCE_NO",A."CROP_VERID",V."Variety_Name"
// ,SUM("STOCK_QUANTITY") "RECEIVE_QTY",COALESCE(SUM("STOCK_QUANTITY"), 0) -COALESCE(SUM("AVL_QUANTITY"), 0) AS "SALE_QTY"
// --SUM((ISNULL("STOCK_QUANTITY",0)-ISNULL("AVL_QUANTITY",0)))  AS "SALE_QTY" --,COUNT(*) AS FARMER          
// FROM "STOCK_DEALERSTOCK" A            
       
// INNER JOIN "mCropVariety" V ON V."Variety_Code"=A."CROP_VERID"          
// WHERE A."CROP_ID"='C002'    
// AND  ('2024-25' is null or A."FIN_YR"='2024-25')  AND ('K' is null or A."SEASSION"='K')          
// GROUP BY  a."LICENCE_NO",A."CROP_VERID",V."Variety_Name"            
// ORDER BY a."LICENCE_NO","Variety_Name"    
// "ODBAL1/2014-15/0016"	"V372"	"Kalachampa "	43.00	17.90

// select "UPDATED_BY","CROP_VERID" ,count("FARMER_ID")  from "STOCK_FARMER" a
       
// INNER JOIN "mCropVariety" V ON V."Variety_Code"=A."CROP_VERID"          
// WHERE A."CROP_ID"='C002'    
// AND  ('2024-25' is null or A."FIN_YEAR"='2024-25')  AND ('K' is null or A."SEASON"='K') 

// group by "UPDATED_BY","CROP_VERID" 

// select d."Dist_Code",d."Dist_Name",COUNT(distinct A."FARMER_ID") AS NoofFarmer    
// from (SELECT "CROP_ID", "FARMER_ID", "FIN_YEAR", "SEASON", "USER_TYPE", "UPDATED_ON", "UPDATED_BY" FROM "STOCK_FARMER") A                            
// --left outer join [dafpseed].[dbo].[SEED_LIC_DIST] B ON A.UPDATED_BY=B.LIC_NO                            
//  --INNER JOIN dbo.Stock_District SD ON SD.Dist_Code=B.DIST_CODE  
//  	INNER join "Stock_District" d on (SUBSTRING(A."UPDATED_BY",3,3)=SUBSTRING(d."Dist_Name",1,3)) 
//  WHERE A."CROP_ID"='C002'  AND (null IS NULL OR d."Dist_Code"=null)       
//  AND  ('2024-25' is null or  A."FIN_YEAR"='2024-25') AND ('K' is null or A."SEASON"='K')        
       
//    GROUP BY d."Dist_Code" ,d."Dist_Name"  order by d."Dist_Name"
   