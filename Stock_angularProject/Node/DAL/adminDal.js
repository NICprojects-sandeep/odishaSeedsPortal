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
        AND ($5 ='0' or $5 is null or SSD."SEASSION_NAME"=$5 )      
        GROUP BY SD."Dist_Code", "Dist_Name",SSD."Crop_Verid",SCM."Variety_Name" order by "Dist_Name",SCM."Variety_Name"`;
        const values = [data.SelectedFinancialYear, data.SelectedCropCatagory, data.SelectedCrop, data.SelectedAgency, data.SelectedSeason]
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
        const query = `select "Dist_Code","Dist_Name" from "Stock_District" order by "Dist_Name"`;
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
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        // "SUPPLY_TYPE" in ('1','6','9')
        // select  case when "SUPPLY_TYPE" ='6' then 'Dealer' when  "SUPPLY_TYPE" in ('1','9') then 'PACS' end as "Type" ,v."Variety_Name",d."Dist_Name",     
        // "BAG_SIZE_KG","SALE_NO_OF_BAG",s."USER_TYPE",g."Dist_Code",s."CROP_VERID"  from "Stock_SaleDetails" s   
        // inner join "Stock_Godown_Master" g on g."Godown_ID" =s."GODOWN_ID"   
        // inner join "mCropVariety" v on v."Variety_Code"=s."CROP_VERID"  
        // inner join "Stock_District" d on d."Dist_Code"=g."Dist_Code"   
        // where "SUPPLY_TYPE" in ('1','6','9') and ($4::text = '0' or s."USER_TYPE"=$4::text)   
        // and s."CROP_ID"=$2 and s."F_YEAR"=$1 and  s."SEASONS"=$3
        // ----------------------------------------------------------  
        // AND ($5::text is null OR d."Dist_Code"=$5)   
        // AND ($6 =0 OR EXTRACT(MONTH FROM s."UPDATED_ON") =$6 )  
        // AND ($7::timestamp IS NULL  OR s."UPDATED_ON">=$7::timestamp) 
        // AND ($8::timestamp IS NULL  OR s."UPDATED_ON"<=$8::timestamp) 
        const query = `SELECT "Dist_Code","Dist_Name","CROP_VERID","Variety_Name","Type", 
        --round((SUM(cast("BAG_SIZE_KG" as decimal))*SUM(cast("SALE_NO_OF_BAG" as decimal)))/100,2)  AS Sale
        round((SUM(cast("BAG_SIZE_KG" as decimal)*cast("SALE_NO_OF_BAG" as decimal)))/100,2)  AS Sale
        ,"USER_TYPE" FROM    
        ( 
            SELECT case when "SUPPLY_TYPE" ='6' then 'Dealer' when  "SUPPLY_TYPE" in ('1','9') then 'PACS' end as "Type" ,d."Variety_Name",A."Dist_Name",  "BAG_SIZE_KG","SALE_NO_OF_BAG","USER_TYPE",A."Dist_Code",c."CROP_VERID" FROM "Stock_District" A
            INNER JOIN "Stock_Godown_Master" b on b."Dist_Code" = A."Dist_Code"
            left join "Stock_SaleDetails" c on c."GODOWN_ID"= b."Godown_ID"
            left join "mCropVariety" d on d."Variety_Code" = c."CROP_VERID"
            where ("SUPPLY_TYPE" is null or "SUPPLY_TYPE" in ('1','6','9')) and ("USER_TYPE"=$4 or  "USER_TYPE" is null)
            and (c."CROP_ID"=$2 or c."CROP_ID" is null)  and ("F_YEAR"=$1 or "F_YEAR" is null) and  ("SEASONS"=$3 or "SEASONS" is null)
            AND ($5::text is null OR a."Dist_Code"=$5) AND ($6 =0 OR EXTRACT(MONTH FROM c."UPDATED_ON") =$6) AND ($7::timestamp IS NULL  OR c."UPDATED_ON">=$7::timestamp)  AND ($8::timestamp IS NULL  OR c."UPDATED_ON"<=$8::timestamp) 
        ) AS A  
        GROUP BY "Dist_Code","Dist_Name","CROP_VERID","Variety_Name","Type","USER_TYPE"   
        ORDER BY "Dist_Name"`;
        console.log(query);
        const values = [data.SelectedFinancialYear,data.SelectedCrop,data.SelectedSeason,data.SelectedUserType,data.SelectedDistrict,data.SelectedMonth, data.selectedFromDate,data.selectedToDate];
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
exports.FillCropByStock_Farmer = (SelectedFinancialYear) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query = `SELECT DISTINCT B."Crop_Code",B."Crop_Name" FROM "STOCK_FARMER" A INNER JOIN "mCrop" B ON A."CROP_ID" = B."Crop_Code"  where A."FIN_YEAR"=$1  ORDER BY "Crop_Name" ASC`;
        const values = [SelectedFinancialYear];
        console.log(query, values);
        const response = await client.query(query, values);
        resolve(response.rows);
    } catch (e) {
        await client.query('rollback');
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});
exports.dealerPacsSale = (data) => new Promise(async (resolve, reject) => {
    console.log(data);
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query1 = `SELECT "Dist_Code","Dist_Name","CROPCATG_ID" ,"Category_Name" ,"Crop_Code","Crop_Name","CROP_VERID","Variety_Name" ,"DealerPacks",SUM("SALE") AS SALE  FROM(                        
            SELECT sd."Dist_Code",SD."Dist_Name",A."CROPCATG_ID",CC."Category_Name",CM."Crop_Code",CM."Crop_Name", A. "CROP_VERID",CV."Variety_Name",     
            SUM(A."TOT_QTL") AS "SALE",  CASE WHEN "SUPPLY_TYPE" IN ('1', '9') THEN 'PACS' ELSE 'Dealer' END AS "DealerPacks" FROM   
            (SELECT "CROP_ID","CROPCATG_ID","CROP_VERID","TOT_QTL","UPDATED_BY","USER_TYPE","FIN_YEAR","SEASON","FARMER_ID","LOT_NUMBER"  FROM "STOCK_FARMER") A  
            left join(select distinct "SALE_TO","LOT_NUMBER","SUPPLY_TYPE" from "Stock_SaleDetails") b on a."UPDATED_BY" = b."SALE_TO" and b."LOT_NUMBER" = a."LOT_NUMBER"                                     
            INNER JOIN "Stock_District" SD ON left(A."FARMER_ID",3)= left(sd."Dist_Name",3)                                   
            INNER JOIN "mCropCategory" CC ON CC."Category_Code"=A."CROPCATG_ID"                                    
            INNER JOIN "mCrop" CM ON CM."Crop_Code"=A."CROP_ID"                                   
            INNER JOIN  "mCropVariety" CV ON CV."Variety_Code"=A."CROP_VERID"                                    
            WHERE A."CROP_ID"=$3 ::text  AND ('OSSC'='0' OR  A."USER_TYPE"='OSSC' )             
            AND  ($1 ::text is null or A."FIN_YEAR"=$1 ::text) AND ($2 ::text is null or A."SEASON"=$2 ::text)                          
            GROUP BY sd."Dist_Code",SD."Dist_Name",A."CROPCATG_ID" ,CC."Category_Name" ,CM."Crop_Code",CM."Crop_Name",A."CROP_VERID",CV."Variety_Name" ,b."SUPPLY_TYPE" ) AS TBL GROUP BY "Dist_Code","Dist_Name","CROPCATG_ID" ,"Category_Name" ,"Crop_Code","Crop_Name","CROP_VERID","Variety_Name" ,"DealerPacks"       
             ORDER BY "Dist_Name","Variety_Name" ,"DealerPacks"`;
        const values1 = [data.SelectedFinancialYear,data.SelectedSeason,data.SelectedCrop];
        console.log(query1, values1);
        const response1 = await client.query(query1, values1);

        const query2 = `select SD."Dist_Code",COUNT(distinct "FARMER_ID") AS NoofFarmer from  (SELECT "FARMER_ID","CROP_ID","UPDATED_BY","USER_TYPE","FIN_YEAR","SEASON" FROM "STOCK_FARMER"  UNION ALL  SELECT "FARMER_ID","CROP_ID","UPDATED_BY","USER_TYPE","FIN_YEAR","SEASON" FROM "STOCK_FARMER")A  
        left outer join "Stock_SaleDetails" b on a."UPDATED_BY" = b."SALE_TO"                               
         INNER JOIN "Stock_District" SD ON left(A."FARMER_ID",3)= left(sd."Dist_Name",3)                                   
         WHERE A."CROP_ID"=$3::text  AND ('OSSC'='0' OR  A."USER_TYPE"='OSSC' )             
         AND ( $1 ::text is null or A."FIN_YEAR"=$1 ::text) AND ($2 ::text is null or  A."SEASON"=$2::text)
         GROUP BY SD."Dist_Code" order by SD."Dist_Code"`;
        const values2 = [data.SelectedFinancialYear,data.SelectedSeason,data.SelectedCrop];
        console.log(query2, values2);
        const response2 = await client.query(query2, values2);

        const query3 = `SELECT SD."Dist_Code",SD."Dist_Name",COUNT(DISTINCT A."UPDATED_BY") AS NOOFD ,  CASE WHEN "SUPPLY_TYPE" IN ('1', '9') THEN 'PACS' ELSE 'Dealer' END AS "DealerPacks"
        FROM   
        (SELECT "CROP_ID","FIN_YEAR","USER_TYPE","SEASON","UPDATED_BY","FARMER_ID" FROM "STOCK_FARMER")A  
        left outer join "Stock_SaleDetails" b on a."UPDATED_BY" = b."SALE_TO"                               
         INNER JOIN "Stock_District" SD ON left(A."FARMER_ID",3)= left(sd."Dist_Name",3)                           
        WHERE A."CROP_ID"=$3::text  AND ('OSSC'='0' OR  A."USER_TYPE"='OSSC' )            
        AND ($1::text is null or  A."FIN_YEAR"=$1::text) AND ($2::text is null or A."SEASON"=$2::text)                   
         GROUP BY SD."Dist_Code",SD."Dist_Name",B."SUPPLY_TYPE" ORDER BY SD."Dist_Code",B."SUPPLY_TYPE"`;
        const values3 = [data.SelectedFinancialYear,data.SelectedSeason,data.SelectedCrop];
        console.log(query3, values3);
        const response3 = await client.query(query3, values3);
        resolve({alldata:response1.rows,nooffarmer:response2.rows,noofdealerpacs:response3.rows});
    } catch (e) {
        await client.query('rollback');
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});

exports.dailyProgressReport = (data) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const query1 = `SELECT  TBL1."Dist_Code",TBL1."Dist_Name",TBL1."CROP_ID",B."Crop_Name","PACS_RCV","DEALER_RCV",("PACS_RCV"+"DEALER_RCV")"TOT_RCV","PACS_SALE","DEALER_SALE",("PACS_SALE"+"DEALER_SALE")"TOT_SALE",
        COALESCE(TBL2."FARMER_CNT", 0) AS "FARMER_CNT" FROM (          
          SELECT "Dist_Name","Dist_Code","CROP_ID",SUM("PACS_RCV") "PACS_RCV",SUM("DEALER_RCV") "DEALER_RCV",SUM("PACS_SALE") "PACS_SALE",SUM("DEALER_SALE") "DEALER_SALE" FROM          
              (          
             select  case when d."Dist_Name" is null then 'KANDHAMAL' else d."Dist_Name" end as "Dist_Name"  ,
              case when d."Dist_Code" is null then '25' else d."Dist_Code" end as "Dist_Code",A."CROP_ID",
              COALESCE(SUM(CASE WHEN "SUPPLY_TYPE" IN ('1', '9') THEN A."STOCK_QUANTITY" ELSE 0 END), 0) AS "PACS_RCV",
                COALESCE(SUM(CASE WHEN "SUPPLY_TYPE"  IN ('6') THEN A."STOCK_QUANTITY" ELSE 0 END), 0) AS "DEALER_RCV",
                COALESCE(SUM(CASE WHEN "SUPPLY_TYPE" IN ('1', '9') THEN A."STOCK_QUANTITY" - A."AVL_QUANTITY" ELSE 0 END), 0) AS "PACS_SALE",
                COALESCE(SUM(CASE WHEN "SUPPLY_TYPE"  IN ('6') THEN A."STOCK_QUANTITY" - A."AVL_QUANTITY" ELSE 0 END), 0) AS "DEALER_SALE"  from "Stock_District" d
       left JOIN (select distinct "SALE_TO","LOT_NUMBER","SUPPLY_TYPE" from "Stock_SaleDetails") B on (SUBSTRING(b."SALE_TO",3,3)=SUBSTRING(d."Dist_Name",1,3)) 
       left join "STOCK_DEALERSTOCK"  a  ON A."LICENCE_NO" = b."SALE_TO" and a."LOT_NO"= b."LOT_NUMBER" 
       WHERE (A."FIN_YR" = '2023-24' or a."FIN_YR" is null) AND (A."SEASSION" = 'R' or A."SEASSION" is null) AND (null IS NULL OR  A."CROP_ID"=null) 
        GROUP BY d."Dist_Code",A."CROP_ID",B."SUPPLY_TYPE" ,d."Dist_Name"
              ) 
           A GROUP BY "Dist_Name","Dist_Code","CROP_ID"          
       ) TBL1          
       left JOIN "mCrop" B ON TBL1."CROP_ID" = B."Crop_Code"                    
       left JOIN "Stock_District" C ON TBL1."Dist_Code" = C."Dist_Code"          
       LEFT JOIN          
       (SELECT count(distinct A."FARMER_ID") "FARMER_CNT",A."CROP_ID",SUBSTRING(A."UPDATED_BY",3,3) "UPDATED_BY" FROM (  
       SELECT "FARMER_ID","FIN_YEAR","SEASON","CROP_ID","UPDATED_BY" FROM "STOCK_FARMER" 
       )A WHERE A."FIN_YEAR"='2023-24' AND A."SEASON"='R' AND (null IS NULL OR A."CROP_ID" = null) GROUP BY A."CROP_ID",SUBSTRING(A."UPDATED_BY",3,3)) TBL2 ON TBL1."CROP_ID"=TBL2."CROP_ID"  AND  
       "UPDATED_BY"=SUBSTRING(TBL1."Dist_Name",1,3)          
       order BY TBL1."Dist_Code",B."Crop_Name",TBL1."CROP_ID" `;
        const values1 = [];
        const response1 = await client.query(query1, values1);
        resolve(response1.rows);
    } catch (e) {
        await client.query('rollback');
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
  


//daily report 


// SELECT  case when d.dist_name is null then 'KANDHAMAL' else d.dist_name end as dist_name  ,
// case when d.dist_code is null then '25' else d.dist_code end as dist_code,A.CROP_ID,ISNULL(CASE WHEN "SUPPLY_TYPE" IN ('1', '9') THEN SUM(A.STOCK_QUANTITY) ELSE 0 END,0) AS 'PACS_RCV',ISNULL(CASE WHEN "SUPPLY_TYPE" not IN ('1', '9') THEN SUM(A.STOCK_QUANTITY) ELSE 0 END,0) AS 'DEALER_RCV', ISNULL(CASE WHEN "SUPPLY_TYPE" IN ('1', '9') THEN SUM(A.STOCK_QUANTITY-A.AVL_QUANTITY) ELSE 0 END,0) AS 'PACS_SALE',ISNULL(CASE WHEN "SUPPLY_TYPE" not IN ('1', '9')THEN SUM(A.STOCK_QUANTITY-A.AVL_QUANTITY) ELSE 0 END,0) AS 'DEALER_SALE'   
// FROM STOCK_DEALERSTOCK A          
// left JOIN (select distinct "SALE_TO","LOT_NUMBER","SUPPLY_TYPE" from "Stock_SaleDetails") B ON A.LICENCE_NO = b."SALE_TO" and a.LOT_NO= b.LOT_NUMBER 
// left join [Stock_District] d on (SUBSTRING(b.SALE_TO,3,3)=SUBSTRING(d.Dist_Name,1,3)) 
// WHERE A.FIN_YR = '2022-23' AND A.SEASSION = 'K' AND (null IS NULL OR  A.CROP_ID=null) 
// GROUP BY d.dist_code,A.CROP_ID,B."SUPPLY_TYPE" ,d.dist_name 


// select * from [Stock_District] a 
// left JOIN (select distinct "SALE_TO","LOT_NUMBER","SUPPLY_TYPE" from "Stock_SaleDetails") B ON (SUBSTRING(b.SALE_TO,3,3)=SUBSTRING(a.Dist_Name,1,3)) 
// left join STOCK_DEALERSTOCK c on c.LICENCE_NO = b."SALE_TO" and c.LOT_NO= b.LOT_NUMBER 
// WHERE ('2023-24'  is null or c.FIN_YR='2023-24' )  AND (c.SEASSION = 'K' or c.SEASSION is null) AND (null IS NULL OR  c.CROP_ID=null) 


// SELECT TBL1."Dist_Code",TBL1."Dist_Name",TBL1."CROP_ID",B."Crop_Name","PACS_RCV","DEALER_RCV",("PACS_RCV"+"DEALER_RCV")"TOT_RCV","PACS_SALE","DEALER_SALE",("PACS_SALE"+"DEALER_SALE")"TOT_SALE",
// --ISNULL(TBL2."FARMER_CNT",0)"FARMER_CNT"
// COALESCE(TBL2."FARMER_CNT", 0) AS "FARMER_CNT" FROM (          
//    SELECT "Dist_Name","Dist_Code","CROP_ID",SUM("PACS_RCV") "PACS_RCV",SUM("DEALER_RCV") "DEALER_RCV",SUM("PACS_SALE") "PACS_SALE",SUM("DEALER_SALE") "DEALER_SALE" FROM          
//        (          
//        SELECT  case when d."Dist_Name" is null then 'KANDHAMAL' else d."Dist_Name" end as "Dist_Name"  ,
//        case when d."Dist_Code" is null then '25' else d."Dist_Code" end as "Dist_Code",A."CROP_ID",
//        COALESCE(SUM(CASE WHEN "SUPPLY_TYPE" IN ('1', '9') THEN A."STOCK_QUANTITY" ELSE 0 END), 0) AS "PACS_RCV",
//          COALESCE(SUM(CASE WHEN "SUPPLY_TYPE"  IN ('6') THEN A."STOCK_QUANTITY" ELSE 0 END), 0) AS "DEALER_RCV",
//          COALESCE(SUM(CASE WHEN "SUPPLY_TYPE" IN ('1', '9') THEN A."STOCK_QUANTITY" - A."AVL_QUANTITY" ELSE 0 END), 0) AS "PACS_SALE",
//          COALESCE(SUM(CASE WHEN "SUPPLY_TYPE"  IN ('6') THEN A."STOCK_QUANTITY" - A."AVL_QUANTITY" ELSE 0 END), 0) AS "DEALER_SALE" 
//        FROM "STOCK_DEALERSTOCK" A          
//        left JOIN (select distinct "SALE_TO","LOT_NUMBER","SUPPLY_TYPE" from "Stock_SaleDetails") B ON A."LICENCE_NO" = b."SALE_TO" and 				a."LOT_NO"= b."LOT_NUMBER" 
//        left join "Stock_District" d on (SUBSTRING(b."SALE_TO",3,3)=SUBSTRING(d."Dist_Name",1,3)) 
//        WHERE A."FIN_YR" = '2023-24' AND A."SEASSION" = 'R' AND (null IS NULL OR  A."CROP_ID"=null) 
//        GROUP BY d."Dist_Code",A."CROP_ID",B."SUPPLY_TYPE" ,d."Dist_Name" 

//        ) 
//     A GROUP BY "Dist_Name","Dist_Code","CROP_ID"          
// ) TBL1          
// INNER JOIN "mCrop" B ON TBL1."CROP_ID" = B."Crop_Code"                    
// INNER JOIN "Stock_District" C ON TBL1."Dist_Code" = C."Dist_Code"          
// LEFT JOIN          
// (SELECT count(distinct A."FARMER_ID") "FARMER_CNT",A."CROP_ID",SUBSTRING(A."UPDATED_BY",3,3) "UPDATED_BY" FROM (  
// SELECT "FARMER_ID","FIN_YEAR","SEASON","CROP_ID","UPDATED_BY" FROM "STOCK_FARMER" 
// )A WHERE A."FIN_YEAR"='2023-24' AND A."SEASON"='R' AND (null IS NULL OR A."CROP_ID" = null) GROUP BY A."CROP_ID",SUBSTRING(A."UPDATED_BY",3,3)) TBL2 ON TBL1."CROP_ID"=TBL2."CROP_ID"  AND  
// "UPDATED_BY"=SUBSTRING(TBL1."Dist_Name",1,3)          
// order BY TBL1."Dist_Code",TBL1."CROP_ID"  


//updated
// SELECT TBL1."Dist_Code",TBL1."Dist_Name",TBL1."CROP_ID",B."Crop_Name","PACS_RCV","DEALER_RCV",("PACS_RCV"+"DEALER_RCV")"TOT_RCV","PACS_SALE","DEALER_SALE",("PACS_SALE"+"DEALER_SALE")"TOT_SALE",
// COALESCE(TBL2."FARMER_CNT", 0) AS "FARMER_CNT" FROM (          
//    SELECT "Dist_Name","Dist_Code","CROP_ID",SUM("PACS_RCV") "PACS_RCV",SUM("DEALER_RCV") "DEALER_RCV",SUM("PACS_SALE") "PACS_SALE",SUM("DEALER_SALE") "DEALER_SALE" FROM          
//        (          
//        SELECT  case when d."Dist_Name" is null then 'KANDHAMAL' else d."Dist_Name" end as "Dist_Name"  ,
//        case when d."Dist_Code" is null then '25' else d."Dist_Code" end as "Dist_Code",A."CROP_ID",
//        COALESCE(SUM(CASE WHEN "SUPPLY_TYPE" IN ('1', '9') THEN A."STOCK_QUANTITY" ELSE 0 END), 0) AS "PACS_RCV",
//          COALESCE(SUM(CASE WHEN "SUPPLY_TYPE"  IN ('6') THEN A."STOCK_QUANTITY" ELSE 0 END), 0) AS "DEALER_RCV",
//          COALESCE(SUM(CASE WHEN "SUPPLY_TYPE" IN ('1', '9') THEN A."STOCK_QUANTITY" - A."AVL_QUANTITY" ELSE 0 END), 0) AS "PACS_SALE",
//          COALESCE(SUM(CASE WHEN "SUPPLY_TYPE"  IN ('6') THEN A."STOCK_QUANTITY" - A."AVL_QUANTITY" ELSE 0 END), 0) AS "DEALER_SALE" 
//        FROM "STOCK_DEALERSTOCK" A          
//        left JOIN (select distinct "SALE_TO","LOT_NUMBER","SUPPLY_TYPE" from "Stock_SaleDetails") B ON A."LICENCE_NO" = b."SALE_TO" and 				a."LOT_NO"= b."LOT_NUMBER" 
//        left join "Stock_District" d on (SUBSTRING(b."SALE_TO",3,3)=SUBSTRING(d."Dist_Name",1,3)) 
//        WHERE A."FIN_YR" = '2023-24' AND A."SEASSION" = 'R' AND (null IS NULL OR  A."CROP_ID"=null) 
//        GROUP BY d."Dist_Code",A."CROP_ID",B."SUPPLY_TYPE" ,d."Dist_Name" 

//        ) 
//     A GROUP BY "Dist_Name","Dist_Code","CROP_ID"          
// ) TBL1          
// INNER JOIN "mCrop" B ON TBL1."CROP_ID" = B."Crop_Code"                    
// INNER JOIN "Stock_District" C ON TBL1."Dist_Code" = C."Dist_Code"          
// LEFT JOIN          
// (SELECT count(distinct A."FARMER_ID") "FARMER_CNT",A."CROP_ID",SUBSTRING(A."UPDATED_BY",3,3) "UPDATED_BY" FROM (  
// SELECT "FARMER_ID","FIN_YEAR","SEASON","CROP_ID","UPDATED_BY" FROM "STOCK_FARMER" 
// )A WHERE A."FIN_YEAR"='2023-24' AND A."SEASON"='R' AND (null IS NULL OR A."CROP_ID" = null) GROUP BY A."CROP_ID",SUBSTRING(A."UPDATED_BY",3,3)) TBL2 ON TBL1."CROP_ID"=TBL2."CROP_ID"  AND  
// "UPDATED_BY"=SUBSTRING(TBL1."Dist_Name",1,3)          
// order BY TBL1."Dist_Code",TBL1."CROP_ID"  



// SELECT   d."Dist_Name","Dist_Code","CROP_ID","SUPPLY_TYPE",--b."CROP_VERID",
// case  WHEN "SUPPLY_TYPE" in ('1', '9') then round((SUM(cast("BAG_SIZE_KG" as decimal)*cast("SALE_NO_OF_BAG" as decimal)))/100,2) else 0 end AS "PACS_RCV",
// case  WHEN "SUPPLY_TYPE" in ('6') then  round((SUM(cast("BAG_SIZE_KG" as decimal)*cast("SALE_NO_OF_BAG" as decimal)))/100,2) else 0 end AS "DEALER_RCV"

// 		FROM  "Stock_SaleDetails" B 
// 		left join "Stock_District" d on (SUBSTRING(b."SALE_TO",3,3)=SUBSTRING(d."Dist_Name",1,3)) 
// 		WHERE  ("SUPPLY_TYPE" IS NULL OR "SUPPLY_TYPE" IN ('1', '6', '9'))
//         AND ("USER_TYPE" = 'OSSC' OR "USER_TYPE" IS NULL)
//         AND ("CROP_ID" = 'C002' OR "CROP_ID" IS NULL)
//         AND ("F_YEAR" = '2023-24' OR "F_YEAR" IS NULL)
//         AND ("SEASONS" = 'R' OR "SEASONS" IS NULL)
//         AND ('26'::TEXT IS NULL OR d."Dist_Code" = '26')
// 		GROUP BY d."Dist_Code",B."CROP_ID",B."SUPPLY_TYPE" ,d."Dist_Name" ,b."CROP_VERID"
		
		
// 		--------------------------
// 		SELECT d."Dist_Name","Dist_Code","CROP_ID","SUPPLY_TYPE",
// 		case  WHEN "SUPPLY_TYPE" in ('1', '9') then SUM(A."STOCK_QUANTITY"-A."AVL_QUANTITY") else 0 end AS "PACS_SALE",
// case  WHEN "SUPPLY_TYPE" in ('6') then SUM(A."STOCK_QUANTITY"-A."AVL_QUANTITY") else 0 end AS "DEALER_SALE"
// 		--ISNULL(CASE WHEN "SUPPLY_TYPE" IN ('1', '9') THEN SUM(A.STOCK_QUANTITY-A.AVL_QUANTITY) ELSE 0 END,0) AS 'PACS_SALE',
// 		--ISNULL(CASE WHEN "SUPPLY_TYPE" not IN ('1', '9')THEN SUM(A.STOCK_QUANTITY-A.AVL_QUANTITY) ELSE 0 END,0) AS 'DEALER_SALE' 
  		
// 		FROM "STOCK_DEALERSTOCK" A          
// 		left JOIN (select distinct "SALE_TO","LOT_NUMBER","SUPPLY_TYPE" from "Stock_SaleDetails") B ON A."LICENCE_NO" = b."SALE_TO" and 				a."LOT_NO"= b."LOT_NUMBER" 
// 		left join "Stock_District" d on (SUBSTRING(A."LICENCE_NO",3,3)=SUBSTRING(d."Dist_Name",1,3)) 
// 		WHERE A."FIN_YR" = '2023-24' AND A."SEASSION" = 'R' AND (null IS NULL OR  A."CROP_ID"=null) 
		
// 		GROUP BY d."Dist_Code",A."CROP_ID",B."SUPPLY_TYPE" ,d."Dist_Name" 