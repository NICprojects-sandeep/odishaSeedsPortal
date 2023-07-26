const ip = require('ip');
const UAParser = require('ua-parser-js');
const crypto = require('crypto');
const sha512 = require('js-sha512');
const authDAL = require('../DAL/authDAL');
const reqip = require('request-ip')
const parser = new UAParser();
const axios = require('axios')

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

const getlicencedata = (data) => {
  console.log(data);
  const url = `https://odishaagrilicense.nic.in/user/seedLicenseCheck?userid=${data.userID}&password=${data.password}&password1=${data.password}`
  const fetchdata= axios.get(url);
  return fetchdata

};

const generateRandomNumber = () => {
  const buf = crypto.randomBytes(16);
  return buf.toString('hex');
};

exports.generateCaptchaAndSalt = (req, res) => {
  let code = '';
  try {
    console.log("authIP", reqip.getClientIp(req), req.params.type);
    // console.log(theCaptcha);
    // activeCaptcha.innerHTML = `${theCaptcha}`;
    switch (req.params.type) {
      case '1': {
        const char = Math.random().toString(24).substring(2, req.params.length) + Math.random().toString(24).substring(2, 4);
        req.session.captcha = char.toUpperCase();
        code = char.toUpperCase();
        break;
      }
      case '2': {
        // const num = Math.floor(Math.random() * (max - min + 1)) + min; // Returns an integer random number between min (included) and max (included)
        //changed
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
        captcha[4] = captcha[4].toLowerCase();
        code = captcha.join("");
        req.session.captcha = code;
        //end
        // const num1 = Math.floor(Math.random() * 90) + 10;
        // const num2 = Math.floor(Math.random() * 10);
        // const operators = ['+', '-'];
        // const operator = operators[(Math.floor(Math.random() * operators.length))];
        // code = `${num1 + operator + num2}=?`;
        // req.session.captcha = (operator === '+') ? (num1 + num2) : (num1 - num2);
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
    var elicencedata = [];
    if (req.body.captcha === req.session.captcha) {
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
            const datafetch = await getlicencedata(req.body)
            if(datafetch.data.length > 1){

            }
            else{
              req.session.role = 'Dealer';
              req.session.userID = Is_Dealer[0].APPEMAIL_ID;
              req.session.username = Is_Dealer[0].APP_FIRMNAME;
              req.session.fullname = Is_Dealer[0].APP_FIRMNAME;
              req.session.LIC_NO1 = Is_Dealer[0].LIC_NO1;
              req.session.LIC_NO = Is_Dealer[0].LIC_NO;

              res.send({
                username: req.session.username, role: req.session.role, fullname: req.session.fullname, message: true
              });
            }
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
          const datafetch = await getlicencedata(req.body)
          console.log(datafetch.data.length, 'data');

          if(datafetch.data.length > 1){
            console.log(datafetch.data,'fffffffffffffffffff');
            var licdetails = await authDAL.licdetails(datafetch.data);
            console.log(licdetails);
            res.send({
              data: licdetails, message: 'doubleIdPresent'
            });
          }
          else{
            req.session.role = 'Dealer';
            req.session.userID = Is_Dealer[0].APPEMAIL_ID;
            req.session.username = Is_Dealer[0].APP_FIRMNAME;
            req.session.fullname = Is_Dealer[0].APP_FIRMNAME;
            req.session.LIC_NO1 = Is_Dealer[0].LIC_NO1;
            req.session.LIC_NO = Is_Dealer[0].LIC_NO;

            res.send({
              username: req.session.username, role: req.session.role, fullname: req.session.fullname, message: true
            });
          }
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
            req.session.username = ValidUserIdOrNot[0].fullname;
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

      // if (result.length > 0) {
      //   if (sha512(result[0].Password + req.session.salt) === req.body.password) {
      //     req.session.role = result[0].User_Type;
      //     req.session.userID = req.body.userID;
      //     req.session.username = result[0].FullName;
      //     req.session.cookie.maxAge = 1800000;
      //     req.session.salt = generateRandomNumber();

      //     const tempSession = req.session;
      //     req.session.regenerate((err) => {
      //       Object.assign(req.session, tempSession);
      //     });
      //     req.session.save((err) => {
      //       // const cookieOption = {
      //       //   path: '/',
      //       //   httpOnly: false,
      //       //   secure: 'auto',
      //       //   sameSite: true,
      //       //   maxAge: 1800000,
      //       //   signed: true
      //       // };
      //       // res.cookie('auth.cookie', req.session.username + req.session.role, cookieOption);
      //       res.send({
      //         username: req.session.username, role: req.session.role, message: true
      //       });
      //     });
      //   } else {
      //     res.send({
      //       message: 'Invalid Username or Password1.'
      //     });
      //   }
      // } else {
      //   res.send({
      //     message: 'Invalid Username or Password2.'
      //   });
      // }
    } else {
      res.send({
        message: 'Invalid Captcha.'
      });
    }
  } catch (e) {
    res.status(500).send(e);
    throw e;
  }
};

exports.getUserDetails = (req, res) => {
  try {
    console.log(req.session.username, req.params.username, req.session.role, req.params.role);
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