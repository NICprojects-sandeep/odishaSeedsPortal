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
//       console.log(body, 'jjjj');
//       return body
//       // Handle the response body here
//     }
//   });
// };

// Example usage:

// getLicenseData(inputData);



// const getlicencedata = (data) => {
//   // console.log(data);
//   // const url = `https://odishaagrilicense.nic.in/user/seedLicenseCheck?userid=${data.userID}&password=${data.password}&password1=${data.password}`
//   // const fetchdata= axios.get(url);
//   // console.log(fetchdata,'jjjjjjjj');
//   // return fetchdata



//  const data1= request('https://odishaagrilicense.nic.in/user/seedLicenseCheck?userid=' + data.userID + '&password=' + data.password + '&password1=' + data.password, {
//     json: true
//   });
//   console.log(data1,'jjjj');
//   return(data1)
// };

const generateRandomNumber = () => {
  const buf = crypto.randomBytes(16);
  return buf.toString('hex');
};

exports.generateCaptchaAndSalt = (req, res) => {
  let code = '';
  try {
    console.log("authIP", reqip.getClientIp(req), req.params.type);
    switch (req.params.type) {
      case '1': {
        const char = Math.random().toString(24).substring(2, req.params.length) + Math.random().toString(24).substring(2, 4);
        req.session.captcha = char.toUpperCase();
        code = char.toUpperCase();
        break;
      }
      case '2': {
        let captcha = new Array();
        for (q = 0; q < 6; q++) {
          if (q % 2 == 0) {
            captcha[q] = String.fromCharCode(Math.floor(Math.random() * 26 + 65));
          }
          else {
            captcha[q] = Math.floor(Math.random() * 10 + 0);
          }
        }
        captcha[0] = captcha[0].toLowerCase();
        captcha[2] = captcha[4].toLowerCase();
        captcha[4] = captcha[4].toLowerCase();
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
    // if (req.body.captcha === req.session.captcha) {
    // console.log('hiiii');
    // const result = await authDAL.CheckLogIn(req.body);
    // console.log(result);
    var Is_SalePoint = 0;
    const Is_Dealer = await authDAL.Is_Dealer(req.body);
    if (Is_Dealer.length > 0) {
      var CheckLogInOSSC = await authDAL.CheckLogInOSSC(req.body);
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




          req.session.cookie.maxAge = 1800000;
          // req.session.cookie.maxAge =60000;
          req.session.salt = generateRandomNumber();

          const tempSession = req.session;
          req.session.regenerate((err) => {
            Object.assign(req.session, tempSession);
          });
          res.send({
            username: req.session.username, role: req.session.role, fullname: req.session.fullname, message: true
          });
        }
        else {
          console.log('inserrr');
          // const datafetch = await getlicencedata(req.body);


         

          request.get(url, { json: true }, (error, response, body) => {
            if (error) {
              console.error('Error:', error);
              // Handle the error as needed
            } else {
              console.log(body, 'jjjj');
              if (body.length > 1) {

              }
              else {
                req.session.role = 'Dealer';
                req.session.userID = Is_Dealer[0].APPEMAIL_ID;
                req.session.username = Is_Dealer[0].APP_FIRMNAME;
                req.session.fullname = Is_Dealer[0].APP_FIRMNAME;
                req.session.LIC_NO1 = Is_Dealer[0].LIC_NO1;
                req.session.LIC_NO = Is_Dealer[0].LIC_NO;
                req.session.distCode = Is_Dealer[0].DIST_CODE
    
                res.send({
                  username: req.session.username, role: req.session.role, fullname: req.session.fullname, message: true
                });
              }
              // Handle the response body here
            }
          });


          console.log(body,'bbbb');
         
          console.log('120');
          console.log('dealer');
          // const url = `https://odishaagrilicense.nic.in/user/seedLicenseCheck?userid=${req.body.userID}&password=${req.body.password}&password1=${req.body.password}`
          // axios.get(url).then(async res => {


          // })
          //   .catch(error => {
          //     console.log(error);
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
        console.log('dealer');
        console.log('136');
        // const datafetch = await getlicencedata(req.body)
        // const url = `https://odishaagrilicense.nic.in/user/seedLicenseCheck?userid=${data.userID}&password=${data.password}&password1=${data.password}`;

        request.get(url, { json: true }, async(error, response, body) => {
          if (error) {
            console.error('Error:', error);
            // Handle the error as needed
          } else {
            console.log(body, 'jjjj');
            if (body.length > 1) {
              console.log(body.data, 'fffffffffffffffffff');
              var licdetails = await authDAL.licdetails(body);
              console.log(licdetails);
              res.send({
                data: licdetails, message: 'doubleIdPresent'
              });
            }
            else {
              req.session.role = 'Dealer';
              req.session.userID = Is_Dealer[0].APPEMAIL_ID;
              req.session.username = Is_Dealer[0].APP_FIRMNAME;
              req.session.fullname = Is_Dealer[0].APP_FIRMNAME;
              req.session.LIC_NO1 = Is_Dealer[0].LIC_NO1;
              req.session.LIC_NO = Is_Dealer[0].LIC_NO;
              req.session.distCode = Is_Dealer[0].DIST_CODE
              res.send({
                username: req.session.username, role: req.session.role, fullname: req.session.fullname, message: true
              });
            }
            // Handle the response body here
          }
        });
       
        // const url = `https://odishaagrilicense.nic.in/user/seedLicenseCheck?userid=${req.body.userID}&password=${req.body.password}&password1=${req.body.password}`
        // axios.get(url).then(async res => {
        //   console.log('j', url, res.data.length);
        //   console.log(res.data);
        //   elicencedata = res.data


        // }).catch(error => {
        //   console.log(error);
        // });
        // console.log(elicencedata, elicencedata.length > 1,elicencedata.length);
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
      console.log('else');
      const ValidUserIdOrNot = await authDAL.ValidUserIdOrNot(req.body);
      console.log(ValidUserIdOrNot);
      if (ValidUserIdOrNot.length > 0) {
        const getUserPassword = await authDAL.getUserPassword(req.body);
        if (sha512(getUserPassword[0].Password + req.session.salt) === req.body.password) {

          req.session.role = ValidUserIdOrNot[0].User_Type;
          req.session.userID = ValidUserIdOrNot[0].UserID;
          req.session.username = ValidUserIdOrNot[0].Name;
          req.session.fullname = ValidUserIdOrNot[0].fullname;

          req.session.cookie.maxAge = 1800000;
          // req.session.cookie.maxAge =60000;
          req.session.salt = generateRandomNumber();

          const tempSession = req.session;
          req.session.regenerate((err) => {
            Object.assign(req.session, tempSession);
          });
          res.send({
            username: req.session.username, role: req.session.role, fullname: req.session.fullname, message: true
          });
        }
        else {
          res.send({
            message: 'Invalid Username or Password.'
          });
        }

      }
      else {
        res.send({
          message: 'Invalid Username or Password.'
        });
        console.log('invalid userid');
      }
    }

    // } else {
    //   res.send({
    //     message: 'Invalid Captcha.'
    //   });
    // }
  } catch (e) {
    res.status(500).send(e);
    throw e;
  }
};

exports.getUserDetails = (req, res) => {
  try {
    if (req.session.username === req.params.username && req.session.role === req.params.role) {
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
    console.log(result);
    req.session.role = 'Dealer';
    req.session.userID = result[0].APPEMAIL_ID;
    req.session.username = result[0].APP_FIRMNAME;
    req.session.fullname = result[0].APP_FIRMNAME;
    req.session.LIC_NO1 = result[0].LIC_NO1;
    req.session.LIC_NO = result[0].LIC_NO;
    res.send({
      username: req.session.username, role: req.session.role, fullname: req.session.fullname, message: true
    });
  } catch (e) {
    res.status(500).send(e);
    throw e;
  }
};