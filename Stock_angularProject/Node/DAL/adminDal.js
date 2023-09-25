var dbConfig = require('../config/dbSqlConnection');
var sqlstock = dbConfig.sqlstock;
var sequelizeSeed = dbConfig.sequelizeSeed;
var locConfigdafpSeeds = dbConfig.locConfigdafpSeeds;

const format = require('pg-format');
const pool = require('../config/dbConfig');


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
        AND ($5 ='0' or $5 is null or SSD."SEASSION_NAME"=$5 )      
        GROUP BY SD."Dist_Code", "Dist_Name",SSD."Crop_Verid",SCM."Variety_Name" order by "Dist_Name",SCM."Variety_Name"`;
        const values = [data.SelectedFinancialYear, data.SelectedCropCatagory, data.SelectedCrop, data.SelectedAgency, data.SelectedSeason]
        const response = await client.query(query, values);
        resolve(response.rows);
    } catch (e) {
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
        console.log(query, values);
        const response = await client.query(query, values);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.FillDistrict = () => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `select "Dist_Code","Dist_Name" from "Stock_District" order by "Dist_Name"`;
        const response = await client.query(query);
        resolve(response.rows);
    } catch (e) {
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
        const query = `SELECT DISTINCT SD."Dist_Code", "Dist_Name",round((COALESCE("OSSC_Recv",0)-COALESCE("OSSC_GtransOwnTr",0)-COALESCE("OSSC_OthrGtransOwnTr" ,0)),2)  "OSSC_Recv",round(COALESCE("OSSC_SaleDealer" ,0),2) "OSSC_SaleDealer",round(COALESCE("OSSC_SalePacks",0),2) "OSSC_SalePacks",                    
    
        round(COALESCE("OSSC_Stock",0),2) "OSSC_Stock",round((COALESCE("OAIC_Recv",0)-COALESCE("OAIC_GtransOwnTr",0)-COALESCE("OAIC_OthrGtransOwnTr",0)),2) "OAIC_Recv",                  
          
        round(COALESCE("OAIC_SalePacks",0),2) "OAIC_SalePacks", round(COALESCE("OAIC_Stock",0),2) "OAIC_Stock"   ,round(COALESCE("OSSC_Recv",0),2) AS   "OSSC_Recv1"     ,round(COALESCE("OSSC_Gtrans",0),2)    "OSSC_Gtrans1"    , round( "OAIC_Recv",2) as "OAIC_Recv1",round("OAIC_Gtrans",2) as "OAIC_Gtrans1"  ,round(COALESCE("OSSC_GtransOwnTr",0),2) "OSSC_GtransOwnTr",round(COALESCE("OSSC_GtransOwnTrPend",0),2) "OSSC_GtransOwnTrPend",  
        round(COALESCE("OSSC_OthrGtransOwnTr",0),2) "OSSC_OthrGtransOwnTr",round(COALESCE("OSSC_OthrGtransOwnTrPend",0),2) "OSSC_OthrGtransOwnTrPend",  
        round(COALESCE("OAIC_GtransOwnTr",0),2) "OAIC_GtransOwnTr",round(COALESCE("OAIC_GtransOwnTrPend",0),2) "OAIC_GtransOwnTrPend",  
        round(COALESCE("OAIC_OthrGtransOwnTr",0),2) "OAIC_OthrGtransOwnTr",round(COALESCE("OAIC_OthrGtransOwnTrPend",0),2) "OAIC_OthrGtransOwnTrPend"  
          
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
        SELECT "Dist_Code",SUM(cast("Bag_Size_In_kg" as decimal)*cast("Recv_No_Of_Bags" as decimal)/100) AS "OAIC_Recv" 
        FROM "Stock_ReceiveDetails"    
        WHERE "FIN_YR"=$1 AND "CropCatg_ID"=$2 AND "Crop_ID"=$3 AND "User_Type"='OAIC' AND ($4::text is null or $4::text='0' or "SEASSION_NAME"=$4::text  )   AND ($5::timestamp IS NULL OR "EntryDate"<=$5::timestamp) 
        GROUP BY "Dist_Code" 
        ) AS TBL21 ON TBL21."Dist_Code"=SD."Dist_Code"                        
          
         LEFT JOIN 
        (    
        SELECT "Dist_Code",SUM(cast("BAG_SIZE_KG" as decimal)*cast("SALE_NO_OF_BAG" as decimal)/100) AS "OAIC_SalePacks"   
        FROM "Stock_SaleDetails"  SS   
        --LEFT OUTER JOIN [DAFPSEED].[DBO].[SEED_LIC_DIST] B ON SS.SALE_TO = B.LIC_NO        
        left join "Stock_UserProfile" b on b."UserId"=ss."UPDATED_BY"   
        WHERE "F_YEAR"=$1 AND "CROPCATG_ID"=$2 AND "CROP_ID"=$3 AND SS."USER_TYPE"='OAIC'-- AND CONFIRM_STATUS=1   AND STATUS='S' AND SUPPLY_TYPE='1'  
        AND ($4::text is null or $4::text='0' or "SEASONS"=$4::text )  AND ($5::timestamp IS NULL OR SS."UPDATED_ON" <=$5::timestamp)   
        GROUP BY "Dist_Code"                        
          
        ) AS TBL22 ON TBL22."Dist_Code"=SD."Dist_Code"                       
          
        LEFT JOIN                        
          
        (
        SELECT GM."Dist_Code",SUM(cast("BAG_SIZE_KG" as decimal)*cast("SALE_NO_OF_BAG" as decimal)/100) AS "OAIC_Gtrans"   
        FROM "Stock_SaleDetails"  SS
        INNER JOIN "Stock_Godown_Master" GM ON SS."GODOWN_ID"=GM."Godown_ID"  
        INNER JOIN "Stock_Godown_Master" GM1 ON SS."SALE_TO"=GM1."Godown_ID" AND GM."Dist_Code"<>GM1."Dist_Code"
        WHERE "F_YEAR"=$1 AND "CROPCATG_ID"=$2 AND "CROP_ID"=$3 AND SS."USER_TYPE"='OAIC' AND "CONFIRM_STATUS"=1  AND "STATUS"='T' 
        AND ($4::text is null or $4::text='0' or "SEASONS"=$4::text  ) AND ($5::timestamp IS NULL OR SS."UPDATED_ON" <=$5::timestamp)  
        GROUP BY GM."Dist_Code"                        
          
        ) AS TBL23 ON TBL23."Dist_Code"=SD."Dist_Code"                       
          
        LEFT JOIN                        
          
        (                        
          
        SELECT "Dist_Code",SUM(cast("Bag_Size_In_kg" as decimal)*cast("AVL_NO_OF_BAGS" as decimal)/100) AS "OAIC_Stock"  
        FROM "Stock_StockDetails"   
        WHERE "FIN_YR"=$1 AND "CropCatg_ID"=$2 AND "Crop_ID"=$3 AND "User_Type"='OAIC'  AND ($4::text is null or $4::text='0' or "SEASSION_NAME"=$4::text)  AND ($5::timestamp IS NULL OR "EntryDate" <=$5::timestamp)   
        GROUP BY "Dist_Code"                        
          
        ) AS TBL2 ON TBL2."Dist_Code"=SD."Dist_Code"    
        -----------------------------------------OAIC  
        LEFT JOIN  
        (     
        SELECT GM."Dist_Code",SUM(cast("BAG_SIZE_KG" as decimal)*cast("SALE_NO_OF_BAG" as decimal)/100) AS "OAIC_GtransOwnTr" 
        FROM "Stock_SaleDetails"  SS  
        INNER JOIN "Stock_Godown_Master" GM ON SS."GODOWN_ID"=GM."Godown_ID"  
        INNER JOIN "Stock_Godown_Master" GM1 ON SS."SALE_TO"=GM1."Godown_ID" AND GM."Dist_Code"=GM1."Dist_Code"   
        WHERE "F_YEAR"=$1 AND "CROPCATG_ID"='01'AND "CROP_ID"=$3 AND SS."USER_TYPE"='OAIC' AND "CONFIRM_STATUS"=1  AND "STATUS"='T' AND "SUPPLY_TYPE"='3' 
        AND ($4::text is null or $4::text='0' or "SEASONS"=$4::text  )  AND ($5::timestamp IS NULL OR SS."UPDATED_ON" <=$5::timestamp)  
        GROUP BY GM."Dist_Code"                        
          
        ) AS TBL234 ON TBL234."Dist_Code"=SD."Dist_Code"                       
         LEFT JOIN 
        (  
        SELECT GM."Dist_Code",SUM(cast("BAG_SIZE_KG" as decimal)*cast("SALE_NO_OF_BAG" as decimal)/100) AS "OAIC_GtransOwnTrPend" 
        FROM "Stock_SaleDetails"  SS   
        INNER JOIN "Stock_Godown_Master" GM ON SS."GODOWN_ID"=GM."Godown_ID"  
        INNER JOIN "Stock_Godown_Master" GM1 ON SS."SALE_TO"=GM1."Godown_ID" AND GM."Dist_Code"=GM1."Dist_Code"  
        WHERE "F_YEAR"=$1 AND "CROPCATG_ID"=$2 AND "CROP_ID"=$3 AND SS."USER_TYPE"='OAIC' AND "CONFIRM_STATUS"<>1  AND "STATUS"='T' AND "SUPPLY_TYPE"='3'  AND ($4::text is null or $4::text='0' or "SEASONS"=$4::text  )  AND ($5::timestamp IS NULL OR SS."UPDATED_ON" <=$5::timestamp) 
        GROUP BY GM."Dist_Code"                 
          
        ) AS TBL235 ON TBL235."Dist_Code"=SD."Dist_Code"      
        LEFT JOIN    
        ( 
        SELECT GM."Dist_Code",SUM(cast("BAG_SIZE_KG" as decimal)*cast("SALE_NO_OF_BAG" as decimal)/100) AS "OAIC_OthrGtransOwnTr"  
        FROM "Stock_SaleDetails"  SS   
        INNER JOIN "Stock_Godown_Master" GM ON SS."GODOWN_ID"=GM."Godown_ID"  
        INNER JOIN "Stock_Godown_Master" GM1 ON SS."SALE_TO"=GM1."Godown_ID" AND GM."Dist_Code"<>GM1."Dist_Code"   
        WHERE "F_YEAR"=$1 AND "CROPCATG_ID"='01'AND "CROP_ID"=$3 AND SS."USER_TYPE"='OAIC' AND "CONFIRM_STATUS"=1  AND "STATUS"='T' AND "SUPPLY_TYPE"='3'  AND ($4::text is null or $4::text='0' or "SEASONS"=$4::text  ) AND ($5::timestamp IS NULL OR SS."UPDATED_ON" <=$5::timestamp)   
        GROUP BY GM."Dist_Code"                        
          
        ) AS TBL2341 ON TBL2341."Dist_Code"=SD."Dist_Code"                       
         LEFT JOIN    
        (    
        SELECT GM."Dist_Code",SUM(cast("BAG_SIZE_KG" as decimal)*cast("SALE_NO_OF_BAG" as decimal)/100) AS "OAIC_OthrGtransOwnTrPend"
        FROM "Stock_SaleDetails"  SS  
        INNER JOIN "Stock_Godown_Master" GM ON SS."GODOWN_ID"=GM."Godown_ID"  
        INNER JOIN "Stock_Godown_Master" GM1 ON SS."SALE_TO"=GM1."Godown_ID" AND GM."Dist_Code"<>GM1."Dist_Code"  
        WHERE "F_YEAR"=$1 AND "CROPCATG_ID"=$2 AND "CROP_ID"=$3 AND SS."USER_TYPE"='OAIC' AND "CONFIRM_STATUS"<>1  AND "STATUS"='T' AND "SUPPLY_TYPE"='3'AND ($4::text is null or $4::text='0' or "SEASONS"=$4::text  )  AND ($5::timestamp IS NULL OR SS."UPDATED_ON" <=$5::timestamp)  
        GROUP BY GM."Dist_Code"  
        ) AS TBL2351 ON TBL2351."Dist_Code"=SD."Dist_Code"     
        -------------------------OSSC  
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
   
        const response = await client.query(query, values);
        resolve(response.rows);
    } catch (e) {
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
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.getVarietywiseLift = (data) => new Promise(async (resolve, reject) => {
   
    if (data.selectedToDate==''){
        data.selectedToDate=null
    }
    if (data.selectedFromDate==''){
        data.selectedFromDate=null
    }
    if(data.SelectedDistrict==0){
        data.SelectedDistrict=null
    }
    if(data.SelectedMonth==0){
        data.SelectedMonth=0
    }
    console.log(data);
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `SELECT "Dist_Code","Dist_Name","CROP_VERID","Variety_Name","Type", 
        round((SUM(cast("BAG_SIZE_KG" as decimal))*SUM(cast("SALE_NO_OF_BAG" as decimal)))/100,2)  AS Sale,"USER_TYPE" FROM    
        ( 
        select  case when "SUPPLY_TYPE" ='6' then 'Dealer' when  "SUPPLY_TYPE" in ('1','9') then 'PACS' end as "Type" ,v."Variety_Name",d."Dist_Name",     
        "BAG_SIZE_KG","SALE_NO_OF_BAG",s."USER_TYPE",g."Dist_Code",s."CROP_VERID"  from "Stock_SaleDetails" s   
        inner join "Stock_Godown_Master" g on g."Godown_ID" =s."GODOWN_ID"   
        inner join "mCropVariety" v on v."Variety_Code"=s."CROP_VERID"  
        inner join "Stock_District" d on d."Dist_Code"=g."Dist_Code"   
        where "SUPPLY_TYPE" in ('1','6','9') and ($4::text = '0' or s."USER_TYPE"=$4::text)   
        and s."CROP_ID"=$2 and s."F_YEAR"=$1 and  s."SEASONS"=$3
        ----------------------------------------------------------  
        AND ($5::text is null OR d."Dist_Code"=$5::text)   
        AND ($6 =0 OR EXTRACT(MONTH FROM s."UPDATED_ON") =$6 )  
        AND ($7::timestamp IS NULL  OR s."UPDATED_ON">=$7::timestamp) 
        AND ($8::timestamp IS NULL  OR s."UPDATED_ON"<=$8::timestamp) 
        ) AS A  
        GROUP BY "Dist_Code","Dist_Name","CROP_VERID","Variety_Name","Type","USER_TYPE"   
        ORDER BY "Dist_Name"`;
        const values = [data.SelectedFinancialYear,data.SelectedCrop,data.SelectedSeason,data.SelectedUserType,data.SelectedDistrict,data.SelectedMonth, data.selectedFromDate,data.selectedToDate];
        // 
        console.log(query, values);
        const response = await client.query(query, values);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
// SELECT DISTINCT SD."Dist_Code", "Dist_Name",(COALESCE("OSSC_Recv",0)-COALESCE("OSSC_GtransOwnTr",0)-COALESCE("OSSC_OthrGtransOwnTr" ,0))  "OSSC_Recv",COALESCE("OSSC_SaleDealer" ,0) "OSSC_SaleDealer",COALESCE("OSSC_SalePacks",0) "OSSC_SalePacks",                    
    
// COALESCE("OSSC_Stock",0) "OSSC_Stock",(COALESCE("OAIC_Recv",0)-COALESCE("OAIC_GtransOwnTr",0)-COALESCE("OAIC_OthrGtransOwnTr",0)) "OAIC_Recv",                  
  
// COALESCE("OAIC_SalePacks",0) "OAIC_SalePacks", COALESCE("OAIC_Stock",0) "OAIC_Stock"   ,COALESCE("OSSC_Recv",0) AS   "OSSC_Recv1"     ,COALESCE("OSSC_Gtrans",0)    "OSSC_Gtrans1"    ,  "OAIC_Recv" as "OAIC_Recv1","OAIC_Gtrans" as "OAIC_Gtrans1"  ,COALESCE("OSSC_GtransOwnTr",0) "OSSC_GtransOwnTr",COALESCE("OSSC_GtransOwnTrPend",0) "OSSC_GtransOwnTrPend",  
// COALESCE("OSSC_OthrGtransOwnTr",0) "OSSC_OthrGtransOwnTr",COALESCE("OSSC_OthrGtransOwnTrPend",0) "OSSC_OthrGtransOwnTrPend",  
// COALESCE("OAIC_GtransOwnTr",0) "OAIC_GtransOwnTr",COALESCE("OAIC_GtransOwnTrPend",0) "OAIC_GtransOwnTrPend",  
// COALESCE("OAIC_OthrGtransOwnTr",0) "OAIC_OthrGtransOwnTr",COALESCE("OAIC_OthrGtransOwnTrPend",0) "OAIC_OthrGtransOwnTrPend"  
  
// FROM "Stock_District" SD   
// LEFT JOIN  
// (   
// SELECT "Dist_Code",SUM(cast("Bag_Size_In_kg" as decimal)*cast("Recv_No_Of_Bags" as decimal)/100)  "OSSC_Recv"                         
  
// FROM "Stock_ReceiveDetails"                         
  
// WHERE "FIN_YR"='2023-24'                           
  
// AND "CropCatg_ID"='01'                          
  
// AND "Crop_ID"='C002'                          
  
// AND "User_Type"='OSSC'    
  
// AND ('K' is null or 'K'='0' or "SEASSION_NAME"='K' )    
   
// -- AND (NULL IS NULL OR "EntryDate"<=@Date)                       
  
// GROUP BY "Dist_Code"                        
  
// ) AS TBL11 ON TBL11."Dist_Code"=SD."Dist_Code"                        
  
                     
  
// LEFT JOIN                        
  
// (                        
  
// SELECT "Dist_Code",SUM(cast("BAG_SIZE_KG" as decimal)*cast("SALE_NO_OF_BAG" as decimal)/100 )AS "OSSC_SaleDealer"                        
  
// FROM "Stock_SaleDetails"  SS                      
  
// INNER JOIN "Stock_Godown_Master" GM ON SS."GODOWN_ID"=GM."Godown_ID"                      
  
// WHERE "F_YEAR"='2023-24'                                 
  
// AND "CROP_ID"='C002'                          
  
// AND SS."USER_TYPE"='OSSC' AND "SUPPLY_TYPE"='6' --and CONFIRM_STATUS=1 AND STATUS='S'      
  
// AND ('K' is null or 'K'='0' or "SEASONS"='K' )    
    
// --AND (@Date IS NULL OR SS."UPDATED_ON"<=@Date)                         
  
// GROUP BY "Dist_Code"                        
  
// ) AS TBL12 ON TBL12."Dist_Code"=SD."Dist_Code"                       
  
// LEFT JOIN                        
  
// (                        
  
// SELECT "Dist_Code",SUM(cast("BAG_SIZE_KG" as decimal)*cast("SALE_NO_OF_BAG" as decimal)/100) AS "OSSC_SalePacks"                        
  
// FROM "Stock_SaleDetails"  SS                      
//   left join "Stock_UserProfile" b on b."UserId"=ss."UPDATED_BY"
// --LEFT OUTER JOIN [DAFPSEED].[DBO].[SEED_LIC_DIST] B ON SS.SALE_TO = B.LIC_NO        
  
// WHERE "CROP_ID"='C002'    and "F_YEAR"='2023-24'        
  
// AND SS."USER_TYPE"='OSSC' AND "SUPPLY_TYPE"='9'     
  
// AND ('K' is null or 'K'='0' or "SEASONS"='K'  )      
   
// -- AND (@Date IS NULL OR SS."UPDATED_ON"<=@Date)               
  
// GROUP BY "Dist_Code"                        
  
// ) AS TBL121 ON TBL121."Dist_Code"=SD."Dist_Code"       
// LEFT JOIN                        
// (                        
// SELECT GM1."Dist_Code",SUM(cast("BAG_SIZE_KG" as decimal)*cast("SALE_NO_OF_BAG" as decimal)/100) AS "OSSC_Salegodwn"                        
// FROM "Stock_SaleDetails"  SS                      
// INNER JOIN "Stock_Godown_Master" GM ON SS."GODOWN_ID"=GM."Godown_ID"               
// INNER JOIN "Stock_Godown_Master" GM1 ON SS."SALE_TO"=GM1."Godown_ID" AND GM."Dist_Code"=GM1."Dist_Code"       
// WHERE "CROP_ID"='C002'    and "F_YEAR"='2023-24'    
// AND SS."USER_TYPE"='OSSC' AND "SUPPLY_TYPE"='8'-- and LOT_NUMBER='Dec/15-18-150-02G13952-1'       
// AND ('K' is null or "SEASONS"='K'  )     
// -- AND (@Date IS NULL OR SS."UPDATED_ON"<=@Date)      
     
// GROUP BY GM1."Dist_Code"                   
// ) AS TBL3 ON TBL3."Dist_Code"=SD."Dist_Code"  
// LEFT JOIN     
// (    
// SELECT GM."Dist_Code",SUM(cast("BAG_SIZE_KG" as decimal)*cast("SALE_NO_OF_BAG" as decimal)/100) AS "OSSC_Gtrans"   
// FROM "Stock_SaleDetails"   SS   
// INNER JOIN "Stock_Godown_Master" GM ON SS."GODOWN_ID"=GM."Godown_ID"  
// INNER JOIN "Stock_Godown_Master" GM1 ON SS."SALE_TO"=GM1."Godown_ID" AND GM."Dist_Code"<>GM1."Dist_Code" 
// WHERE "F_YEAR"='2023-24'    
// AND "CROPCATG_ID"='01'   
// AND "CROP_ID"='C002'  
// AND SS."USER_TYPE"='OSSC' AND "CONFIRM_STATUS"=1 AND "STATUS"='T'  
// AND ('K' is null or 'K'='0' or "SEASONS"='K' ) 
// -- AND (@Date IS NULL OR SS."UPDATED_ON"<=@Date)                    
  
// GROUP BY GM."Dist_Code"
// ) AS TBL13 ON TBL13."Dist_Code"=SD."Dist_Code"                     
  
// LEFT JOIN  
// (  
// SELECT "Dist_Code",SUM(cast("Bag_Size_In_kg" as decimal)*cast("AVL_NO_OF_BAGS" as decimal)/100) AS "OSSC_Stock"  
// FROM "Stock_StockDetails"  
// WHERE "FIN_YR"='2023-24' AND "CropCatg_ID"='01' AND "Crop_ID"='C002' AND "User_Type"='OSSC' AND ('K' is null or 'K'='0' or  "SEASSION_NAME"='K' ) --AND (@Date IS NULL OR "EntryDate"<=@Date) 
// GROUP BY "Dist_Code" 
// ) AS TBL1 ON TBL1."Dist_Code"=SD."Dist_Code"                        
 
// LEFT JOIN
// (  
// SELECT "Dist_Code",SUM(cast("Bag_Size_In_kg" as decimal)*cast("Recv_No_Of_Bags" as decimal)/100) AS "OAIC_Recv" 
// FROM "Stock_ReceiveDetails"    
// WHERE "FIN_YR"='2023-24'AND "CropCatg_ID"='01' AND "Crop_ID"='C002' AND "User_Type"='OAIC' AND ('K' is null or 'K'='0' or "SEASSION_NAME"='K'  )   -- AND (@Date IS NULL OR "EntryDate"<=@Date) 
// GROUP BY "Dist_Code" 
// ) AS TBL21 ON TBL21."Dist_Code"=SD."Dist_Code"                        
  
//  LEFT JOIN 
// (    
// SELECT "Dist_Code",SUM(cast("BAG_SIZE_KG" as decimal)*cast("SALE_NO_OF_BAG" as decimal)/100) AS "OAIC_SalePacks"   
// FROM "Stock_SaleDetails"  SS   
// --LEFT OUTER JOIN [DAFPSEED].[DBO].[SEED_LIC_DIST] B ON SS.SALE_TO = B.LIC_NO        
// left join "Stock_UserProfile" b on b."UserId"=ss."UPDATED_BY"   
// WHERE "F_YEAR"='2023-24' AND "CROPCATG_ID"='01' AND "CROP_ID"='C002' AND SS."USER_TYPE"='OAIC'-- AND CONFIRM_STATUS=1   AND STATUS='S' AND SUPPLY_TYPE='1'  
// AND ('K' is null or 'K'='0' or "SEASONS"='K' )  
// --AND (@Date IS NULL OR SS."UPDATED_ON" <=@Date)   
// GROUP BY "Dist_Code"                        
  
// ) AS TBL22 ON TBL22."Dist_Code"=SD."Dist_Code"                       
  
// LEFT JOIN                        
  
// (
// SELECT GM."Dist_Code",SUM(cast("BAG_SIZE_KG" as decimal)*cast("SALE_NO_OF_BAG" as decimal)/100) AS "OAIC_Gtrans"   
// FROM "Stock_SaleDetails"  SS
// INNER JOIN "Stock_Godown_Master" GM ON SS."GODOWN_ID"=GM."Godown_ID"  
// INNER JOIN "Stock_Godown_Master" GM1 ON SS."SALE_TO"=GM1."Godown_ID" AND GM."Dist_Code"<>GM1."Dist_Code"
// WHERE "F_YEAR"='2023-24' AND "CROPCATG_ID"='01'  AND "CROP_ID"='C002' AND SS."USER_TYPE"='OAIC' AND "CONFIRM_STATUS"=1  AND "STATUS"='T' 
// AND ('K' is null or 'K'='0' or "SEASONS"='K'  )  
// --AND (@Date IS NULL OR SS."UPDATED_ON" <=@Date)  
// GROUP BY GM."Dist_Code"                        
  
// ) AS TBL23 ON TBL23."Dist_Code"=SD."Dist_Code"                       
  
// LEFT JOIN                        
  
// (                        
  
// SELECT "Dist_Code",SUM(cast("Bag_Size_In_kg" as decimal)*cast("AVL_NO_OF_BAGS" as decimal)/100) AS "OAIC_Stock"  
// FROM "Stock_StockDetails"   
// WHERE "FIN_YR"='2023-24' AND "CropCatg_ID"='01' AND "Crop_ID"='C002'AND "User_Type"='OAIC'  AND ('K' is null or 'K'='0' or "SEASSION_NAME"='K') 
// -- AND (@Date IS NULL OR "EntryDate" <=@Date)   
// GROUP BY "Dist_Code"                        
  
// ) AS TBL2 ON TBL2."Dist_Code"=SD."Dist_Code"    
// -----------------------------------------OAIC  
// LEFT JOIN  
// (     
// SELECT GM."Dist_Code",SUM(cast("BAG_SIZE_KG" as decimal)*cast("SALE_NO_OF_BAG" as decimal)/100) AS "OAIC_GtransOwnTr" 
// FROM "Stock_SaleDetails"  SS  
// INNER JOIN "Stock_Godown_Master" GM ON SS."GODOWN_ID"=GM."Godown_ID"  
// INNER JOIN "Stock_Godown_Master" GM1 ON SS."SALE_TO"=GM1."Godown_ID" AND GM."Dist_Code"=GM1."Dist_Code"   
// WHERE "F_YEAR"='2023-24' AND "CROPCATG_ID"='01'AND "CROP_ID"='C002'AND SS."USER_TYPE"='OAIC' AND "CONFIRM_STATUS"=1  AND "STATUS"='T' AND "SUPPLY_TYPE"='3' 
// AND ('K' is null or 'K'='0' or "SEASONS"='K'  )  
// -- AND (@Date IS NULL OR SS."UPDATED_ON" <=@Date)  
// GROUP BY GM."Dist_Code"                        
  
// ) AS TBL234 ON TBL234."Dist_Code"=SD."Dist_Code"                       
//  LEFT JOIN 
// (  
// SELECT GM."Dist_Code",SUM(cast("BAG_SIZE_KG" as decimal)*cast("SALE_NO_OF_BAG" as decimal)/100) AS "OAIC_GtransOwnTrPend" 
// FROM "Stock_SaleDetails"  SS   
// INNER JOIN "Stock_Godown_Master" GM ON SS."GODOWN_ID"=GM."Godown_ID"  
// INNER JOIN "Stock_Godown_Master" GM1 ON SS."SALE_TO"=GM1."Godown_ID" AND GM."Dist_Code"=GM1."Dist_Code"  
// WHERE "F_YEAR"='2023-24' AND "CROPCATG_ID"='01' AND "CROP_ID"='C002' AND SS."USER_TYPE"='OAIC' AND "CONFIRM_STATUS"<>1  AND "STATUS"='T' AND "SUPPLY_TYPE"='3'  AND ('K' is null or 'K'='0' or "SEASONS"='K'  )  
// -- AND (@Date IS NULL OR SS."UPDATED_ON" <=@Date) 
// GROUP BY GM."Dist_Code"                 
  
// ) AS TBL235 ON TBL235."Dist_Code"=SD."Dist_Code"      
// LEFT JOIN                        
  
// ( 
// SELECT GM."Dist_Code",SUM(cast("BAG_SIZE_KG" as decimal)*cast("SALE_NO_OF_BAG" as decimal)/100) AS "OAIC_OthrGtransOwnTr"  
// FROM "Stock_SaleDetails"  SS   
// INNER JOIN "Stock_Godown_Master" GM ON SS."GODOWN_ID"=GM."Godown_ID"  
// INNER JOIN "Stock_Godown_Master" GM1 ON SS."SALE_TO"=GM1."Godown_ID" AND GM."Dist_Code"<>GM1."Dist_Code"   
// WHERE "F_YEAR"='2023-24' AND "CROPCATG_ID"='01'AND "CROP_ID"='C002' AND SS."USER_TYPE"='OAIC' AND "CONFIRM_STATUS"=1  AND "STATUS"='T' AND "SUPPLY_TYPE"='3'  AND ('K' is null or 'K'='0' or "SEASONS"='K'  ) 
// --AND (@Date IS NULL OR SS."UPDATED_ON" <=@Date)   
// GROUP BY GM."Dist_Code"                        
  
// ) AS TBL2341 ON TBL2341."Dist_Code"=SD."Dist_Code"                       
//  LEFT JOIN    
// (    
// SELECT GM."Dist_Code",SUM(cast("BAG_SIZE_KG" as decimal)*cast("SALE_NO_OF_BAG" as decimal)/100) AS "OAIC_OthrGtransOwnTrPend"
// FROM "Stock_SaleDetails"  SS  
// INNER JOIN "Stock_Godown_Master" GM ON SS."GODOWN_ID"=GM."Godown_ID"  
// INNER JOIN "Stock_Godown_Master" GM1 ON SS."SALE_TO"=GM1."Godown_ID" AND GM."Dist_Code"<>GM1."Dist_Code"  
// WHERE "F_YEAR"='2023-24' AND "CROPCATG_ID"='01'  AND "CROP_ID"='C002' AND SS."USER_TYPE"='OAIC' AND "CONFIRM_STATUS"<>1  AND "STATUS"='T' AND "SUPPLY_TYPE"='3'AND ('K' is null or 'K'='0' or "SEASONS"='K'  ) 
// -- AND (@Date IS NULL OR SS."UPDATED_ON" <=@Date)  
// GROUP BY GM."Dist_Code"                 
  
// ) AS TBL2351 ON TBL2351."Dist_Code"=SD."Dist_Code"     
// -------------------------OSSC  
// LEFT JOIN                        
  
// (  
// SELECT GM."Dist_Code",SUM(cast("BAG_SIZE_KG" as decimal)*cast("SALE_NO_OF_BAG" as decimal)/100) AS "OSSC_GtransOwnTr" 
// FROM "Stock_SaleDetails"  SS 
// INNER JOIN "Stock_Godown_Master" GM ON SS."GODOWN_ID"=GM."Godown_ID" 
// INNER JOIN "Stock_Godown_Master" GM1 ON SS."SALE_TO"=GM1."Godown_ID" AND GM."Dist_Code"=GM1."Dist_Code"  
// WHERE "F_YEAR"='2023-24'  AND "CROPCATG_ID"='01'  AND "CROP_ID"='C002' AND SS."USER_TYPE"='OSSC' AND "CONFIRM_STATUS"=1  AND "STATUS"='T' AND "SUPPLY_TYPE"='8'  
// AND ('K' is null or 'K'='0' or "SEASONS"='K'  )   
// --  AND (@Date IS NULL OR SS."UPDATED_ON" <=@Date) 
// GROUP BY GM."Dist_Code" 
// ) AS TBL23411 ON TBL23411."Dist_Code"=SD."Dist_Code"                       
//  LEFT JOIN    
// (                        
  
// SELECT GM."Dist_Code",SUM(cast("BAG_SIZE_KG" as decimal)*cast("SALE_NO_OF_BAG" as decimal)/100) AS "OSSC_GtransOwnTrPend"  
// FROM "Stock_SaleDetails"  SS 
// INNER JOIN "Stock_Godown_Master" GM ON SS."GODOWN_ID"=GM."Godown_ID"
// INNER JOIN "Stock_Godown_Master" GM1 ON SS."SALE_TO"=GM1."Godown_ID" AND GM."Dist_Code"=GM1."Dist_Code"   
// WHERE "F_YEAR"='2023-24' AND "CROPCATG_ID"='01'    AND "CROP_ID"='C002' AND SS."USER_TYPE"='OSSC' AND "CONFIRM_STATUS"<>1  AND "STATUS"='T' AND "SUPPLY_TYPE"='8'  AND ('K' is null or 'K'='0' or "SEASONS"='K'  )  
// --  AND (@Date IS NULL OR SS."UPDATED_ON" <=@Date)    
// GROUP BY GM."Dist_Code"                 
  
// ) AS TBL23511 ON TBL23511."Dist_Code"=SD."Dist_Code"      
// LEFT JOIN                        
  
// (                        
  
// SELECT GM."Dist_Code",SUM(cast("BAG_SIZE_KG" as decimal)*cast("SALE_NO_OF_BAG" as decimal)/100) AS "OSSC_OthrGtransOwnTr" 
// FROM "Stock_SaleDetails"  SS    
// INNER JOIN "Stock_Godown_Master" GM ON SS."GODOWN_ID"=GM."Godown_ID"    
// INNER JOIN "Stock_Godown_Master" GM1 ON SS."SALE_TO"=GM1."Godown_ID" AND GM."Dist_Code"<>GM1."Dist_Code" 
// WHERE "F_YEAR"='2023-24'   AND "CROPCATG_ID"='01'   AND "CROP_ID"='C002'   AND SS."USER_TYPE"='OSSC' AND "CONFIRM_STATUS"=1  AND "STATUS"='T' AND "SUPPLY_TYPE"='8'    
// AND ('K' is null or 'K'='0' or "SEASONS"='K'  )    
// -- AND (@Date IS NULL OR SS."UPDATED_ON" <=@Date) 
// GROUP BY GM."Dist_Code"                        
  
// ) AS TBL234111 ON TBL234111."Dist_Code"=SD."Dist_Code"                       
//  LEFT JOIN                        
  
// (                        
  
// SELECT GM."Dist_Code",SUM(cast("BAG_SIZE_KG" as decimal)*cast("SALE_NO_OF_BAG" as decimal)/100) AS "OSSC_OthrGtransOwnTrPend"   FROM "Stock_SaleDetails"  SS 
// INNER JOIN "Stock_Godown_Master" GM ON SS."GODOWN_ID"=GM."Godown_ID"  
// INNER JOIN "Stock_Godown_Master" GM1 ON SS."SALE_TO"=GM1."Godown_ID" AND GM."Dist_Code"<>GM1."Dist_Code"  
// WHERE "F_YEAR"='2023-24' AND "CROPCATG_ID"='01' AND "CROP_ID"='C002' AND SS."USER_TYPE"='OSSC' AND "CONFIRM_STATUS"<>1  AND "STATUS"='T' AND "SUPPLY_TYPE"='8'   AND ('K' is null or 'K'='0' or "SEASONS"='K'  )   
// --AND (@Date IS NULL OR SS."UPDATED_ON" <=@Date)   
// GROUP BY GM."Dist_Code"                 
  
// ) AS TBL235111 ON TBL235111."Dist_Code"=SD."Dist_Code"     
 
  
//   --WHERE (@DistCode is null or SD.Dist_Code=@DistCode  )        
 
//  order by "Dist_Name"  
  