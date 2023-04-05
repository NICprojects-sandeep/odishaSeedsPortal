const pool = require('../config/dbConfig');

exports.addActivityLog = async (action, attack, mode, userID, ipAddress, url, deviceType, os, browser) => {
  const client = await pool.connect().catch((err) => { console.log(`Unable to connect to the database: ${err}`); });
  try {
    const query = `insert into "ActivityLog" ("IPAddress", "UserID", "URL", "DeviceType", "OS", "Browser", "DateTime", "Action", "Attack", "Mode") values ($1, $2, $3, $4, $5, $6, $7, $8, $9, $10)`;
    const values = [ipAddress, userID, url, deviceType, os, browser, 'now()', action, attack, mode];
    await client.query(query, values);
  } catch (e) {
    console.log(`Oops! An error occurred: ${e}`);
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
  } catch (e) {
    reject(new Error(`Oops! An error occurred: ${e}`));
  } finally {
    client.release();
  }
});