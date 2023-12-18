const pool = require('../config/dbConfig');
var dbConfig = require('../config/dbSqlConnection');
var sqlstock = dbConfig.sqlstock;
var sequelizeSeed = dbConfig.sequelizeSeed;

const format = require('pg-format');


exports.successPrebookingDtl = (date) => new Promise(async (resolve, reject) => {
    const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
    try {
        const year = date.substring(date.length - 4);
        const Month = date.substring(date.length - 5, 3)
        const Date = date.substring(date.length - 10, 2)
        const entrydate = year + '-' + Month + '-' + Date + ' 00:00:00.000'
        const lastdate = year + '-' + Month + '-' + Date + ' 23:59:59.000'
        const query = `SELECT a."applicationID",b."UPDATED_ON" "date",b."UPDATED_BY" "delerid",a."TRANSACTION_ID" as "txnid" from "prebookinglist" a
        inner join "STOCK_FARMER" b on a."applicationID"= b."PREBOOKING_APPLICATIONID" and "UPDATED_ON" between $1 and $2 order by "UPDATED_ON"`;
        const values = [entrydate,lastdate];
        const response = await client.query(query, values);
        resolve(response.rows);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        client.release();
    }
});


