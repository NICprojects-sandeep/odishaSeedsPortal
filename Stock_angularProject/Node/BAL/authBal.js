const ip = require('ip');
const UAParser = require('ua-parser-js');
const crypto = require('crypto');
const sha512 = require('js-sha512');
const authDAL = require('../DAL/authDAL');
const reqip = require('request-ip')
const parser = new UAParser();
const request = require('request');

const { signAccessToken, verifyAccessToken } = require('../helpers/jwt.helper');

const getFinancialYear = () => {
  const today = new Date();
  const financialYear = (today.getMonth() + 1) <= 3 ? `${today.getFullYear() - 1}-${today.getFullYear().toString().substr(2, 3)}` : `${today.getFullYear()}-${(today.getFullYear() + 1).toString().substr(2, 3)}`;
  return financialYear;
};

const getURL = (req) => {
  const fullURL = `${req.protocol}://${req.get('host')}${req.originalUrl}`;
  return fullURL;
};


// const getlicencedata = (data) => {
//   const url = `https://odishaagrilicense.nic.in/user/seedLicenseCheck?userid=${data.userID}&password=${data.password}&password1=${data.password}`;

//   request.get(url, { json: true }, (error, response, body) => {
//     if (error) {
//       console.error('Error:', error);
//       // Handle the error as needed
//     } else {
//       return body
//       // Handle the response body here
//     }
//   });
// };

// Example usage:

// getLicenseData(inputData);



// const getlicencedata = (data) => {
//   // const url = `https://odishaagrilicense.nic.in/user/seedLicenseCheck?userid=${data.userID}&password=${data.password}&password1=${data.password}`
//   // const fetchdata= axios.get(url);
//   // return fetchdata



//  const data1= request('https://odishaagrilicense.nic.in/user/seedLicenseCheck?userid=' + data.userID + '&password=' + data.password + '&password1=' + data.password, {
//     json: true
//   });
//   return(data1)
// };

const generateRandomNumber = () => {
  const buf = crypto.randomBytes(16);
  return buf.toString('hex');
};

exports.generateCaptchaAndSalt = (req, res) => {
  let code = '';
  try {
    switch (req.params.type) {
      case '1': {
        const char = Math.random().toString(24).substring(2, req.params.length) + Math.random().toString(24).substring(2, 4);
        req.session.captcha = char.toUpperCase();
        code = char.toUpperCase();
        break;
      }
      case '2': {
        let captcha = new Array();
        for (q = 0; q < 5; q++) {
          // if (q % 2 == 0) {
          captcha[q] = String.fromCharCode(Math.floor(Math.random() * 26 + 65)).toUpperCase();
          // }
          // else {
          //   captcha[q] = Math.floor(Math.random() * 10 + 0);
          // }
        }
        // captcha[0] = captcha[0].toLowerCase();
        // captcha[2] = captcha[4].toLowerCase();
        // captcha[4] = captcha[4].toLowerCase();
        code = captcha.join("");
        req.session.captcha = code;
        break;
      }
      default: {
        // nothing happens here
      }
    }
    req.session.salt = !req.session.salt ? generateRandomNumber() : req.session.salt;
    res.send({
      code, resultCode: req.session.captcha, salt: req.session.salt
    });
  } catch (e) {
    res.status(500).send(e);
    throw e;
  }
};

exports.CheckLogIn = async (req, res) => {
  try {
    const url = `https://odishaagrilicense.nic.in/user/seedLicenseCheck?userid=${req.body.userID}&password=${req.body.password}&password1=${req.body.password}`;
    var elicencedata = [];
    if (req.body.captcha === req.session.captcha) {
      console.log('insert', req.body);
      const partUserID = req.body.userID.replace(/[^A-Za-z_]/g, '');
      console.log(partUserID);
      if (partUserID == 'BAO_') {
        const Is_Dealer = await authDAL.BAOloginCheck(req.body);
      } else {
        // const result = await authDAL.CheckLogIn(req.body);
        // console.log(result);
        var Is_SalePoint = 0;
        const Is_Dealer = await authDAL.Is_Dealer(req.body);
        console.log(Is_Dealer);
        if (Is_Dealer.length > 0) {
          var CheckLogInOSSC = await authDAL.CheckLogInOSSC(req.body);
          console.log(CheckLogInOSSC);
          if (CheckLogInOSSC.length > 0) {
            if (sha512(CheckLogInOSSC[0].Password + req.session.salt) === req.body.password) {
              req.session.role = 'SPO';
              req.session.userID = CheckLogInOSSC[0].APPEMAIL_ID;
              req.session.username = CheckLogInOSSC[0].APP_FIRMNAME;
              req.session.fullname = CheckLogInOSSC[0].APP_FIRMNAME;
              req.session.LIC_NO1 = CheckLogInOSSC[0].LIC_NO1;
              req.session.LIC_NO = CheckLogInOSSC[0].LIC_NO;
              req.session.distCode = CheckLogInOSSC[0].LGDistrict;
              req.session.nicdistCode = CheckLogInOSSC[0].LGDistrict;
              req.session.distCode_1 = CheckLogInOSSC[0].dist_code;
              req.session.mobileNo = CheckLogInOSSC[0].APPMOB_NO;


              req.session.cookie.maxAge = 1800000;
              // req.session.cookie.maxAge =60000;
              req.session.salt = generateRandomNumber();

              const tempSession = req.session;
              req.session.regenerate((err) => {
                Object.assign(req.session, tempSession);
              });
              authDAL.addActivityLog('/CheckLogIn', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Login');
              res.send({
                username: req.session.username, role: req.session.role, fullname: req.session.fullname, message: true
              });
            }
            else {
              // const datafetch = await getlicencedata(req.body);




              request.get(url, { json: true, strictSSL: false }, (error, response, body) => {
                if (error) {
                  authDAL.addActivityLog('/CheckLogIn', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, error);

                  console.error('Error:', error);
                  // Handle the error as needed
                } else {
                  if (body.length > 1) {
                    authDAL.addActivityLog('/CheckLogIn', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, body.length);

                  }
                  else {
                    console.log(Is_Dealer, 1);
                    req.session.role = 'Dealer';//1
                    req.session.userID = Is_Dealer[0].APPEMAIL_ID;
                    req.session.username = Is_Dealer[0].APP_FIRMNAME;
                    req.session.fullname = Is_Dealer[0].APP_FIRMNAME;
                    req.session.LIC_NO1 = Is_Dealer[0].LIC_NO1;
                    req.session.LIC_NO = Is_Dealer[0].LIC_NO;
                    req.session.distCode = Is_Dealer[0].DIST_CODE;
                    req.session.mobileNo = Is_Dealer[0].APPMOB_NO;
                    req.session.nicdistCode = Is_Dealer[0].LGDistrict;
                    req.session.subsidyModeToERUPI = Is_Dealer[0].subsidyModeToERUPI
                    authDAL.addActivityLog('/CheckLogIn', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Login');

                    res.send({
                      username: req.session.username, role: req.session.role, fullname: req.session.fullname, message: true
                    });
                  }
                  // Handle the response body here
                }
              });


              // const url = `https://odishaagrilicense.nic.in/user/seedLicenseCheck?userid=${req.body.userID}&password=${req.body.password}&password1=${req.body.password}`
              // axios.get(url).then(async res => {


              // })
              //   .catch(error => {
              //   });
              // if (elicencedata.length > 1) {
              //   //code
              // }
              // else {
              //   req.session.role = 'Dealer';
              //   req.session.userID = Is_Dealer[0].APPEMAIL_ID;
              //   req.session.username = Is_Dealer[0].APPEMAIL_ID;
              //   req.session.fullname = Is_Dealer[0].APP_FIRMNAME;
              //   req.session.LIC_NO1 = Is_Dealer[0].LIC_NO1;
              //   req.session.LIC_NO = Is_Dealer[0].LIC_NO;

              //   res.send({
              //     username: req.session.username, role: req.session.role, fullname: req.session.fullname, message: true
              //   });
              // }

            }

          }
          else {
            // const datafetch = await getlicencedata(req.body)
            // const url = `https://odishaagrilicense.nic.in/user/seedLicenseCheck?userid=${data.userID}&password=${data.password}&password1=${data.password}`;
            console.log(url);
            request.get(url, { json: true, strictSSL: false }, async (error, response, body) => {
              if (error) {
                console.error('Error:', error);
                authDAL.addActivityLog('/CheckLogIn', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, error);
                // Handle the error as needed
              } else {
                if (body.length > 1) {
                  var licdetails = await authDAL.licdetails(body);
                  authDAL.addActivityLog('/CheckLogIn', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'doubleIdPresent');
                  res.send({
                    data: licdetails, message: 'doubleIdPresent'
                  });
                }
                else {
                  console.log(Is_Dealer, 2);

                  req.session.role = 'Dealer';//3
                  req.session.userID = Is_Dealer[0].APPEMAIL_ID;
                  req.session.username = Is_Dealer[0].APP_FIRMNAME;
                  req.session.fullname = Is_Dealer[0].APP_FIRMNAME;
                  req.session.LIC_NO1 = Is_Dealer[0].LIC_NO1;
                  req.session.LIC_NO = Is_Dealer[0].LIC_NO;
                  req.session.distCode = Is_Dealer[0].DIST_CODE;
                  req.session.mobileNo = Is_Dealer[0].APPMOB_NO;
                  req.session.nicdistCode = Is_Dealer[0].LGDistrict;
                  req.session.subsidyModeToERUPI = Is_Dealer[0].subsidyModeToERUPI
                  authDAL.addActivityLog('/CheckLogIn', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Login');

                  res.send({
                    username: req.session.username, role: req.session.role, fullname: req.session.fullname, message: true
                  });
                }
                // Handle the response body here
              }
            });

            // const url = `https://odishaagrilicense.nic.in/user/seedLicenseCheck?userid=${req.body.userID}&password=${req.body.password}&password1=${req.body.password}`
            // axios.get(url).then(async res => {
            //   elicencedata = res.data


            // }).catch(error => {
            //   console.log(error);
            // });
            // if (elicencedata.length > 1) {
            //   console.log(elicencedata, 'atan6187@gmail.com');

            // }
            // else {
            //   req.session.role = 'Dealer';
            //   req.session.userID = Is_Dealer[0].APPEMAIL_ID;
            //   req.session.username = Is_Dealer[0].APPEMAIL_ID;
            //   req.session.fullname = Is_Dealer[0].APP_FIRMNAME;
            //   req.session.LIC_NO1 = Is_Dealer[0].LIC_NO1;
            //   req.session.LIC_NO = Is_Dealer[0].LIC_NO;

            //   res.send({
            //     username: req.session.username, role: req.session.role, fullname: req.session.fullname, message: true
            //   });
            // }


          }


        }
        else {
          const ValidUserIdOrNot = await authDAL.ValidUserIdOrNot(req.body);
          console.log(ValidUserIdOrNot);
          if (ValidUserIdOrNot.length > 0) {
            const getUserPassword = await authDAL.getUserPassword(req.body);
            console.log(sha512(getUserPassword[0].Password + req.session.salt) === req.body.password);
            console.log(getUserPassword[0].Password, '+', req.body.password);
            if (sha512(getUserPassword[0].Password + req.session.salt) === req.body.password) {
              req.session.role = ValidUserIdOrNot[0].User_Type;
              req.session.userID = ValidUserIdOrNot[0].UserID;
              req.session.username = ValidUserIdOrNot[0].Name;
              req.session.fullname = ValidUserIdOrNot[0].fullname;
              req.session.nicdistCode = ValidUserIdOrNot[0].LGDistrict;
              req.session.distCode = ValidUserIdOrNot[0].Dist_Code;
              req.session.cookie.maxAge = 1800000;
              // req.session.cookie.maxAge =60000;
              req.session.salt = generateRandomNumber();

              const tempSession = req.session;
              req.session.regenerate((err) => {
                Object.assign(req.session, tempSession);
              });
              authDAL.addActivityLog('/CheckLogIn', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Login');
              console.log(req.session);
              res.send({
                username: req.session.username, role: req.session.role, fullname: req.session.fullname, message: true
              });
            }
            else {
              authDAL.addActivityLog('/CheckLogIn', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'Invalid Username or Password.');
              res.send({
                message: 'Invalid Username or Password.'
              });
            }

          }
          else {
            authDAL.addActivityLog('/CheckLogIn', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'Invalid Username or Password.');
            res.send({
              message: 'Invalid Username or Password.'
            });
          }
        }
      }


    } else {
      authDAL.addActivityLog('/CheckLogIn', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'Invalid Captcha.');

      res.send({
        message: 'Invalid Captcha.'
      });
    }
  } catch (e) {
    authDAL.addActivityLog('/CheckLogIn', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, e);
    res.status(500).send(e);
    throw e;
  }
};

exports.getUserDetails = (req, res) => {
  console.log('diptiranjan.parida100@gmail.com');
  try {
   
    // if (req.session.username.charAt(req.session.username.length - 1) === ' ') {
    //   req.session.username = req.session.username.slice(0, -1);
    //   console.log('remove');
    // }
    console.log(req.session.username === req.body.username, req.session.role === req.body.role);
    console.log(req.session.username, req.body.username, req.session.role, req.body.role);
    if (req.session.username === req.body.username && req.session.role === req.body.role) {
      res.send({
        isLoggedIn: true
      });
    } else {
      req.session.destroy();
      res.send({
        isLoggedIn: false
      });
    }
  } catch (e) {
    res.status(500).send(e);
    throw e;
  }
};

exports.signOut = (req, res) => {
  try {
    req.session.destroy();
    res.send({
      message: true
    });
  } catch (e) {
    res.status(500).send(e);
    throw e;
  }
};
exports.getmarqueData = async (req, res) => {
  try {
    const result = await authDAL.getmarqueData(req, res);
    res.send({ result });
  } catch (e) {
    res.status(500).send(e);
    throw e;
  }
};
exports.OneDealerLogin = async (req, res) => {
  try {
    const result = await authDAL.OneDealerLogin(req.body);
    req.session.role = 'Dealer';
    req.session.userID = result[0].APPEMAIL_ID;
    req.session.username = result[0].APP_FIRMNAME;
    req.session.fullname = result[0].APP_FIRMNAME;
    req.session.LIC_NO1 = result[0].LIC_NO1;
    req.session.LIC_NO = result[0].LIC_NO;
    authDAL.addActivityLog('/OneDealerLogin', 'SELECT', 'POST', req.session.username, reqip.getClientIp(req), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`, 'SucessFully Login');
    res.send({
      username: req.session.username, role: req.session.role, fullname: req.session.fullname, message: true
    });
  } catch (e) {
    res.status(500).send(e);
    throw e;
  }
};
// select "Godown_ID","Lot_No",count( "Dist_Code") from "Stock_StockDetails" group by "Godown_ID" ,"Lot_No" having count("Dist_Code") >1 order by "Godown_ID"
// select * from "STOCK_FARMER" where "TRANSACTION_ID"  in (select "DTL_TRANSACTION_ID" from "STOCK_DEALERSALEDTL" where "TOT_SUB_AMOUNT_SP" :: varchar like '-%') 765 nos
//select "TRANSACTION_ID",count("TRANSACTION_ID") from "STOCK_FARMER" group by "TRANSACTION_ID" having count("TRANSACTION_ID") >1 26

// select count(distinct "TRANSACTION_ID"),sum("payableAmtFarmer") from( SELECT "TRANSACTION_ID","payableAmtFarmer",
//   DENSE_RANK() OVER  (PARTITION BY "TRANSACTION_ID" ORDER BY "redeemInsertedOn" desc) AS rank 
//   FROM "eRUPIDetails" where "dealerCode" in ('e-Lic/SEED/2020D-3395','e-Lic/SEED/2020D-1191')  order by id)  where rank =1  --340  168  6

// select count(distinct "TRANSACTION_ID"),sum("payableAmtFarmer") from( SELECT "TRANSACTION_ID","payableAmtFarmer",count,
//   DENSE_RANK() OVER  (PARTITION BY "TRANSACTION_ID" ORDER BY "redeemInsertedOn" desc) AS rank
//   FROM "eRUPIDetails" where "redeemstatusMsg"='Voucher Redeemed'  and "dealerCode" in ('e-Lic/SEED/2020D-3395','e-Lic/SEED/2020D-1191')   order by id)  where rank =1  --168

// select count(distinct "TRANSACTION_ID"),sum("payableAmtFarmer") from( SELECT "TRANSACTION_ID","payableAmtFarmer",count,
//   DENSE_RANK() OVER  (PARTITION BY "TRANSACTION_ID" ORDER BY "redeemInsertedOn" desc) AS rank
//   FROM "eRUPIDetails" where count=3 and "redeemstatusMsg"='Voucher Revoked'  and "dealerCode" in ('e-Lic/SEED/2020D-3395','e-Lic/SEED/2020D-1191')  order by id)  where rank =1 --6

// WITH TransactionsRedeemed AS (SELECT DISTINCT "TRANSACTION_ID" FROM "eRUPIDetails" WHERE "redeemstatusMsg" = 'Voucher Redeemed'),
// TransactionsRevokedCount3 AS (SELECT DISTINCT "TRANSACTION_ID" FROM "eRUPIDetails" WHERE "count" = 3 AND "redeemstatusMsg" = 'Voucher Revoked'),
// RankedTransactions AS (SELECT  "TRANSACTION_ID","payableAmtFarmer",DENSE_RANK() OVER (PARTITION BY "TRANSACTION_ID" ORDER BY "redeemInsertedOn" DESC) AS rank FROM "eRUPIDetails" WHERE ("redeemstatusMsg" != 'Voucher Redeemed' OR "redeemstatusMsg" IS NULL) and "dealerCode" in ('e-Lic/SEED/2020D-3395','e-Lic/SEED/2020D-1191') )
// SELECT COUNT(DISTINCT "TRANSACTION_ID") AS total_transactions, 
//        SUM("payableAmtFarmer") FROM RankedTransactions
// WHERE "TRANSACTION_ID" NOT IN (SELECT "TRANSACTION_ID" FROM TransactionsRedeemed)
//   AND "TRANSACTION_ID" NOT IN (SELECT "TRANSACTION_ID" FROM TransactionsRevokedCount3)
//   AND rank = 1 



// WITH 
// RedeemedTransactions AS ( SELECT distinct "TRANSACTION_ID","payableAmtFarmer", DENSE_RANK() OVER (PARTITION BY "TRANSACTION_ID" ORDER BY "redeemInsertedOn" DESC) AS rank
//     FROM public."eRUPIDetails"  WHERE  "redeemstatusMsg" = 'Voucher Redeemed'), 
// AggregatedRedeemedTransactions AS ( SELECT "TRANSACTION_ID",COUNT(DISTINCT "TRANSACTION_ID") AS redeemed_transaction_count, SUM("payableAmtFarmer") AS total_payable_amt_farmer FROM  
//   RedeemedTransactions WHERE   rank = 1  GROUP BY "TRANSACTION_ID"),

// switchtodbtmode AS ( SELECT distinct "TRANSACTION_ID","payableAmtFarmer", DENSE_RANK() OVER (PARTITION BY "TRANSACTION_ID" ORDER BY "redeemInsertedOn" DESC) AS rank
//     FROM public."eRUPIDetails"  WHERE count=3 and "redeemstatusMsg"='Voucher Revoked'), 
// Aggregatedswitchtodbtmode AS ( SELECT "TRANSACTION_ID",COUNT(DISTINCT "TRANSACTION_ID") AS switchtodbtmode_transaction_count, SUM("payableAmtFarmer") AS total_payable_amt_switchtodbtmode_farmer FROM  switchtodbtmode WHERE   rank = 1  GROUP BY "TRANSACTION_ID"),



// RankedTransactions AS (SELECT  "TRANSACTION_ID","payableAmtFarmer",DENSE_RANK() OVER (PARTITION BY "TRANSACTION_ID" ORDER BY "redeemInsertedOn" DESC) AS rank FROM "eRUPIDetails" WHERE ("redeemstatusMsg" != 'Voucher Redeemed' OR "redeemstatusMsg" IS NULL)  ),
// Aggregatedpendingforrecon as (	SELECT "TRANSACTION_ID",COUNT(DISTINCT "TRANSACTION_ID") as pending_transaction_count ,SUM("payableAmtFarmer") as total_payable_amt_pending_farmer  
// 	FROM RankedTransactions WHERE "TRANSACTION_ID" NOT IN (SELECT "TRANSACTION_ID" FROM RedeemedTransactions)  AND "TRANSACTION_ID" NOT IN (SELECT "TRANSACTION_ID" FROM switchtodbtmode)
//   AND rank = 1 GROUP BY "TRANSACTION_ID" )
	
	
// SELECT  distinct e."dealerCode",  COUNT(DISTINCT e."TRANSACTION_ID") AS total_transactions, COUNT(DISTINCT rt."TRANSACTION_ID") AS "Redeemed Transaction Count", SUM(rt.total_payable_amt_farmer) AS
// 	"Total Payable Amt Farmer",COUNT(DISTINCT std."TRANSACTION_ID") AS "Redeemed Transaction Count", SUM( DISTINCT std.total_payable_amt_switchtodbtmode_farmer) AS
// 	"total_payable_amt_switchtodbtmode_farmer",
// 	COUNT(DISTINCT pending."TRANSACTION_ID") AS "pending Transaction Count", SUM(pending.total_payable_amt_pending_farmer) AS
// 	"total_payable_amt_switchtodbtmode_farmer"
	
// 	FROM   public."eRUPIDetails" e 
// 	LEFT JOIN AggregatedRedeemedTransactions rt ON e."TRANSACTION_ID" = rt."TRANSACTION_ID" and "redeemstatusMsg" = 'Voucher Redeemed'
// 	LEFT JOIN Aggregatedswitchtodbtmode std ON e."TRANSACTION_ID" = std."TRANSACTION_ID" and count = 3
// 	LEFT JOIN Aggregatedpendingforrecon pending ON e."TRANSACTION_ID" = pending."TRANSACTION_ID"  
// GROUP BY 
//  e."dealerCode"
// ORDER BY 
//     e."dealerCode";