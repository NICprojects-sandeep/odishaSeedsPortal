const pool = require('../config/dbConfig');
var dbConfig = require('../config/dbSqlConnection');
var sqlstock = dbConfig.sqlstock;
var sequelizeSeed = dbConfig.sequelizeSeed;

var locConfigstock = dbConfig.locConfigStock;
var locConfigStockLive = dbConfig.locConfigStockLive;
var locConfigAuth = dbConfig.locConfigAuth;


var sequelizeStock = dbConfig.sequelizeStock;

exports.addActivityLog = async (action, attack, mode, userID, ipAddress, url, deviceType, os, browser) => {
  const client = await pool.connect().catch((err) => { console.log(`Unable to connect to the database: ${err}`); });
  try {
    const query = `insert into "ActivityLog" ("IPAddress", "UserID", "URL", "DeviceType", "OS", "Browser", "DateTime", "Action", "Attack", "Mode") values ($1, $2, $3, $4, $5, $6, $7, $8, $9, $10)`;
    const values = [ipAddress, userID, url, deviceType, os, browser, 'now()', action, attack, mode];
    await client.query(query, values);
  } catch (e) {
    await client.query('rollback');
    reject(new Error(`Oops! An error occurred: ${e}`));
} finally {
    client.release();
}
};

exports.getUserDetails = (userID) => new Promise(async (resolve, reject) => {
  const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
  try {
    const partUserID = userID.replace(/[^A-Za-z_]/g, '');
    let query = ``;
    switch (partUserID) {

      case 'AAE_':
        query = `select "UserID", (coalesce(substring("UserID", 1, 4), '') || coalesce(initcap("BlockName"), '')) :: character varying "Username", "PasswordHash", "RoleName", "AccessFailedCount", "IsLoggedIn", "Status" from "UserLogin" a inner join "UserRole" b on a."RoleID" = b."RoleID" inner join "LGDBlock" c on substring("UserID", 5, 4) = c."BlockCode" :: character varying where "UserID" = $1`;
        break;
      case 'AEE_':
        query = `select "UserID", (coalesce(substring("UserID", 1, 4), '') || coalesce(initcap("SubDivisionName"), '')) :: character varying "Username", "PasswordHash", "RoleName", "AccessFailedCount", "IsLoggedIn", "Status" from "UserLogin" a inner join "UserRole" b on a."RoleID" = b."RoleID" inner join "LGDSubDivision" c on substring("UserID", 5, 7) = c."SubDivisionCode" :: character varying where "UserID" = $1`;
        break;
      case 'AO_':
        query = `select "UserID", (coalesce(substring("UserID", 1, 3), '') || coalesce(initcap("BlockName"), '')) :: character varying "Username", "PasswordHash", "RoleName", "AccessFailedCount", "IsLoggedIn", "Status" from "UserLogin" a inner join "UserRole" b on a."RoleID" = b."RoleID" inner join "LGDBlock" c on substring("UserID", 4, 4) = c."BlockCode" :: character varying where "UserID" = $1`;
        break;
      case 'EE_':
        query = `select "UserID", (coalesce(substring("UserID", 1, 3), '') || coalesce(initcap("DistrictName"), '')) :: character varying "Username", "PasswordHash", "RoleName", "AccessFailedCount", "IsLoggedIn", "Status" from "UserLogin" a inner join "UserRole" b on a."RoleID" = b."RoleID" inner join "LGDDistrict" c on substring("UserID", 4, 3) = c."DistrictCode" :: character varying where "UserID" = $1`;
        break;
      case 'OAIC_DM_':
        query = `select "UserID", (coalesce(substring("UserID", 1, 8), '') || coalesce(initcap("DistrictName"), '')) :: character varying "Username", "PasswordHash", "RoleName", "AccessFailedCount", "IsLoggedIn", "Status" from "UserLogin" a inner join "UserRole" b on a."RoleID" = b."RoleID" inner join "LGDDistrict" c on substring("UserID", 9, 3) = c."DistrictCode" :: character varying where "UserID" = $1`;
        break;
      case 'MSakti_':
        query = `select "UserID", (coalesce(substring("UserID", 1, 7), '') || coalesce(initcap("BlockName"), '')) :: character varying "Username", "PasswordHash", "RoleName", "AccessFailedCount", "IsLoggedIn", "Status" from "UserLogin" a
        inner join "UserRole" b on a."RoleID" = b."RoleID" 
        inner join "LGDBlock" c on substring("UserID", 8, 4) = c."BlockCode" :: character varying where "UserID" = $1`;
        break;
      case 'DIR_':
        query = `select "UserID", "UserID" "Username", "PasswordHash", "RoleName", "AccessFailedCount", "IsLoggedIn", "Status" from "UserLogin" a inner join "UserRole" b on a."RoleID" = b."RoleID" where "UserID"= $1`;
        break;
      case 'MD_':
        query = `select "UserID", "UserID" "Username", "PasswordHash", "RoleName", "AccessFailedCount", "IsLoggedIn", "Status" from "UserLogin" a inner join "UserRole" b on a."RoleID" = b."RoleID" where "UserID"= $1`;
        break;
      default:
        query = `select "UserID", "UserID" "Username", "PasswordHash", "RoleName", "AccessFailedCount", "IsLoggedIn", "Status" from "UserLogin" a inner join "UserRole" b on a."RoleID" = b."RoleID" where "UserID" = $1`;
    }
    const values = [userID];
    const response = await client.query(query, values);
    resolve(response.rows);
  }catch (e) {
    await client.query('rollback');
    reject(new Error(`Oops! An error occurred: ${e}`));
} finally {
    client.release();
}
});

exports.CheckLogIn = (data) => new Promise(async (resolve, reject) => {
  try {
    const partUserID = data.userID.replace(/[^A-Za-z_]/g, '');
    switch (partUserID) {
      case 'AAO_':

        var queryText = `select b.User_Type,b.UserID,a.Password,c.Name,c.UID,d.LGDistrict,  
        CONVERT(VARCHAR(10),Datediff(MINUTE,b.Date_Create,b.Last_Pwd_Change)) time_diff,c.Dist_Code,d.Dist_Name,UPPER(c.FullName) AS FullName from [AuthenticationDB].[dbo].[Auth_User] a
       inner join stock.dbo.Stock_Users b on a.Username=b.UserID
       inner join Stock_UserProfile c on a.Username =c.UserId
       inner join Stock_District d on c.Dist_Code= d.Dist_Code 
       where [Username]='${data.userID}'  `
        const result = await sequelizeStock.query(queryText);
        resolve(result[0]);

    }

  } catch (e) {
    await client.query('rollback');
    reject(new Error(`Oops! An error occurred: ${e}`));
} finally {
    client.release();
}
})
exports.getmarqueData = async (req, res) => {
  return new Promise(async resolve => {
    try {
      const result = await sequelizeSeed.query(`SELECT NEWS_ID,NEWS FROM mLATESTNEWS WHERE IS_ACTIVE = 1 ORDER BY NEWS_ID DESC`, {
        replacements: {}, type: sequelizeStock.QueryTypes.SELECT
      });
      resolve(result);

    } catch (e) {
      await client.query('rollback');
      reject(new Error(`Oops! An error occurred: ${e}`));
  } finally {
      client.release();
  }
  });
};

exports.Is_Dealer = (data) => new Promise(async (resolve, reject) => {
  const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
  try {
    const result = await sequelizeSeed.query(`SELECT APP_FIRMNAME,LIC_NO1,APPEMAIL_ID,LIC_NO,DIST_CODE FROM [dafpseed].[dbo].[SEED_LIC_DIST] A 
    INNER JOIN [dafpseed].[dbo].[SEED_LIC_APP_DIST] B ON A.SEED_LIC_DIST_ID = B.SEED_LIC_DIST_ID 
    INNER JOIN [dafpseed].[dbo].[SEED_LIC_COMP_DIST] C ON A.SEED_LIC_DIST_ID = C.SEED_LIC_DIST_ID 
    WHERE B.APPEMAIL_ID = :APPEMAIL_ID AND CONVERT(DATE, DATEADD(MONTH,1,A.APR_UPTO),103) >= CONVERT(DATE, GETDATE(), 103) AND A.LIC_ACTIVE = 1 AND A.IS_ACTIVE = 1 AND A.APP_STATUS = 'A' AND C.COMP_TYPE = 1 AND C.COMP_NAME = 'OSSC'`, {
      replacements: { APPEMAIL_ID: data.userID }, type: sequelizeStock.QueryTypes.SELECT
    });
    resolve(result);
  } catch (e) {
    await client.query('rollback');
    reject(new Error(`Oops! An error occurred: ${e}`));
} finally {
    client.release();
}
});
exports.ValidUserIdOrNot = (data) => new Promise(async (resolve, reject) => {
  const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
  try {
    const query1 =
      `SELECT a."User_Type",a."UserID",a."Passwd",b."Name",b."UID",c."LGDistrict",
      cast(Extract(epoch FROM ( a."Last_Pwd_Change"- a."Date_Create"))/60 as INTEGER) AS "time_diff",
      b."Dist_Code",c."Dist_Name",UPPER(b."FullName") AS FullName 
      FROM "Stock_Users" a,"Stock_UserProfile" b,"Stock_District" c  
      WHERE a."UserID"=b."UserId" and b."Dist_Code"=c."Dist_Code" and a."UserID" =$1 `;
    const values1 = [data.userID];
    const response = await client.query(query1, values1);
    resolve(response.rows);
  } catch (e) {
    await client.query('rollback');
    reject(new Error(`Oops! An error occurred: ${e}`));
} finally {
    client.release();
}
});
exports.getUserPassword = (data) => new Promise(async (resolve, reject) => {
  var con = new sqlstock.ConnectionPool(locConfigAuth);
  try {
    con.connect().then(function success() {
      const request = new sqlstock.Request(con);
      request.input('Username', data.userID);
      request.execute('NicAutht_UserLogin', function (err, result) {
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
    await client.query('rollback');
    reject(new Error(`Oops! An error occurred: ${e}`));
} finally {
    client.release();
}
});
exports.ChkValidLic = (data) => new Promise(async (resolve, reject) => {
  return new Promise(async resolve => {
    try {
      const result = await sequelizeSeed.query(`SELECT SEED_LIC_DIST_ID,REF_NO,LIC_NO,LIC_NO1,APP_FIRMNAME,PASSWORD,PASSWORD_SALT,SALTED_PASSWORD,ISLOCKEDOUT,APP_TYPE FROM [dafpseed].[dbo].[SEED_LIC_DIST] WHERE LIC_ACTIVE = 1 AND IS_ACTIVE = 1 AND APP_STATUS = 'A' AND CONVERT(DATE,DATEADD(MON
        TH,1,APR_UPTO),103) >= CONVERT(DATE,GETDATE(),103) AND LIC_NO1 = '${data.userID}'`, {
        replacements: {}, type: sequelizeStock.QueryTypes.SELECT
      });
      resolve(result);

    } catch (e) {
      await client.query('rollback');
      reject(new Error(`Oops! An error occurred: ${e}`));
  } finally {
      client.release();
  }
  });
});
exports.CheckLic = (data) => new Promise(async (resolve, reject) => {
  return new Promise(async resolve => {
    try {
      const result = await sequelizeSeed.query(`SELECT SEED_LIC_DIST_ID,REF_NO,LIC_NO,LIC_NO1,APP_FIRMNAME,PASSWORD,PASSWORD_SALT,SALTED_PASSWORD,ISLOCKEDOUT,APP_TYPE FROM [dafpseed].[dbo].[SEED_LIC_DIST] WHERE LIC_ACTIVE = 1 AND IS_ACTIVE = 1 AND APP_STATUS = 'A' AND CONVERT(DATE,DATEADD(MONTH,1,APR_UPTO),103) >= CONVERT(DATE,GETDATE(),103) AND LIC_NO1= '${data.userID}'`, {
        replacements: {}, type: sequelizeStock.QueryTypes.SELECT
      });
      resolve(result);

    } 
    // catch (e) {
    //   console.log('An error occurred...', e);
    //   resolve([]);
    //   throw e
    // }
    catch (e) {
      await client.query('rollback');
      reject(new Error(`Oops! An error occurred: ${e}`));
  } finally {
      client.release();
  }
  });
});
exports.GetBlockCode = (data) => new Promise(async (resolve, reject) => {
  return new Promise(async resolve => {
    try {
      const result = await sequelizeSeed.query(`SELECT SEED_LIC_DIST_ID,REF_NO,LIC_NO,LIC_NO1,APP_FIRMNAME,PASSWORD,PASSWORD_SALT,SALTED_PASSWORD,ISLOCKEDOUT,APP_TYPE FROM [dafpseed].[dbo].[SEED_LIC_DIST] WHERE LIC_ACTIVE = 1 AND IS_ACTIVE = 1 AND APP_STATUS = 'A' AND CONVERT(DATE,DATEADD(MONTH,1,APR_UPTO),103) >= CONVERT(DATE,GETDATE(),103) AND LIC_NO1= '${data.userID}'`, {
        replacements: {}, type: sequelizeStock.QueryTypes.SELECT
      });
      resolve(result);

    } catch (e) {
      await client.query('rollback');
      reject(new Error(`Oops! An error occurred: ${e}`));
  } finally {
      client.release();
  }
  });
});
exports.CheckLogInOSSC = (data) => new Promise(async (resolve, reject) => {
  const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
  try {
    const result = await sequelizeStock.query(` SELECT APP_FIRMNAME,LIC_NO1,APPEMAIL_ID,d.Password,LIC_NO,LGDistrict,e.dist_code FROM [dafpseed].[dbo].[SEED_LIC_DIST] A 
      INNER JOIN [dafpseed].[dbo].[SEED_LIC_APP_DIST] B ON A.SEED_LIC_DIST_ID = B.SEED_LIC_DIST_ID 
      INNER JOIN [dafpseed].[dbo].[SEED_LIC_COMP_DIST] C ON A.SEED_LIC_DIST_ID = C.SEED_LIC_DIST_ID 
      inner join [AuthenticationDB].dbo.Auth_User  d on b.APPEMAIL_ID= d.Username
      inner join [dafpseed].[dbo].[dist] e on e.dist_code= b.APPDIST_ID
      WHERE B.APPEMAIL_ID = '${data.userID}' AND CONVERT(DATE, DATEADD(MONTH,1,A.APR_UPTO),103) >= CONVERT(DATE, GETDATE(), 103) AND A.LIC_ACTIVE = 1 AND A.IS_ACTIVE = 1 AND A.APP_STATUS = 'A' AND C.COMP_TYPE = 1 AND C.COMP_NAME = 'OSSC' AND A.IS_OSSC = 1`, {
      replacements: {}, type: sequelizeStock.QueryTypes.SELECT
    });
    resolve(result);

  } catch (e) {
    await client.query('rollback');
    reject(new Error(`Oops! An error occurred: ${e}`));
} finally {
    client.release();
}
});
exports.licdetails = (data) => new Promise(async (resolve, reject) => {
  var licdetails = [];

    for (let i = 0; i < data.length; i++) {
      var result = await sequelizeSeed.query(` SELECT APP_FIRMNAME,LIC_NO1,APPEMAIL_ID,LIC_NO FROM [dafpseed].[dbo].[SEED_LIC_DIST] A 
      INNER JOIN [dafpseed].[dbo].[SEED_LIC_APP_DIST] B ON A.SEED_LIC_DIST_ID = B.SEED_LIC_DIST_ID 
      INNER JOIN [dafpseed].[dbo].[SEED_LIC_COMP_DIST] C ON A.SEED_LIC_DIST_ID = C.SEED_LIC_DIST_ID 
      WHERE LIC_NO1 = '${data[i].licenceNo}' AND CONVERT(DATE, DATEADD(MONTH,1,A.APR_UPTO),103) >= CONVERT(DATE, GETDATE(), 103) AND A.LIC_ACTIVE = 1 AND A.IS_ACTIVE = 1 AND A.APP_STATUS = 'A' AND C.COMP_TYPE = 1 AND C.COMP_NAME = 'OSSC'`, {
        replacements: {}, type: sequelizeStock.QueryTypes.SELECT
      });
      licdetails.push(result[0]);
      if (i + 1 == data.length) {
  
        resolve( licdetails);
      }
    }


  // for (let index = 0; index <= data.length; index++) {
  //   console.log(data[index].licenceNo);
  //   var result = await sequelizeSeed.query(` SELECT APP_FIRMNAME,LIC_NO1,APPEMAIL_ID,d.Password,LIC_NO FROM [dafpseed].[dbo].[SEED_LIC_DIST] A 
  //   INNER JOIN [dafpseed].[dbo].[SEED_LIC_APP_DIST] B ON A.SEED_LIC_DIST_ID = B.SEED_LIC_DIST_ID 
  //   INNER JOIN [dafpseed].[dbo].[SEED_LIC_COMP_DIST] C ON A.SEED_LIC_DIST_ID = C.SEED_LIC_DIST_ID 
  //   inner join [AuthenticationDB].dbo.Auth_User  d on b.APPEMAIL_ID= d.Username
  //   WHERE B.APPEMAIL_ID = '${data[index].licenceNo}' AND CONVERT(DATE, DATEADD(MONTH,1,A.APR_UPTO),103) >= CONVERT(DATE, GETDATE(), 103) AND A.LIC_ACTIVE = 1 AND A.IS_ACTIVE = 1 AND A.APP_STATUS = 'A' AND C.COMP_TYPE = 1 AND C.COMP_NAME = 'OSSC' AND A.IS_OSSC = 1`, {
  //     replacements: {}, type: sequelizeStock.QueryTypes.SELECT
  //   });
  //   console.log(result);

  //   licdetails.push(result);
  //   if (key + 1 == data.length) {
  //     res.send({ result: licdetails });
  //   }
  //   licdetails.push(result);
  //   console.log(licdetails);
  // }


});
exports.OneDealerLogin = (data) => new Promise(async (resolve, reject) => {
  const client = await pool.connect().catch((err) => { reject(new Error(`Unable to connect to the database: ${err}`)); });
  try {
    const result = await sequelizeSeed.query(`SELECT APP_FIRMNAME,LIC_NO1,APPEMAIL_ID,LIC_NO FROM [dafpseed].[dbo].[SEED_LIC_DIST] A 
    INNER JOIN [dafpseed].[dbo].[SEED_LIC_APP_DIST] B ON A.SEED_LIC_DIST_ID = B.SEED_LIC_DIST_ID 
    INNER JOIN [dafpseed].[dbo].[SEED_LIC_COMP_DIST] C ON A.SEED_LIC_DIST_ID = C.SEED_LIC_DIST_ID 
    WHERE LIC_NO1 = :licno AND CONVERT(DATE, DATEADD(MONTH,1,A.APR_UPTO),103) >= CONVERT(DATE, GETDATE(), 103) AND A.LIC_ACTIVE = 1 AND A.IS_ACTIVE = 1 AND A.APP_STATUS = 'A' AND C.COMP_TYPE = 1 AND C.COMP_NAME = 'OSSC'`, {
      replacements: { licno: data.licNumber }, type: sequelizeStock.QueryTypes.SELECT
    });
    resolve(result);
  } catch (e) {
    await client.query('rollback');
    reject(new Error(`Oops! An error occurred: ${e}`));
} finally {
    client.release();
}
});