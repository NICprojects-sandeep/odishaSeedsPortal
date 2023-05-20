const ip = require('ip');
const UAParser = require('ua-parser-js');
const crypto = require('crypto');
const sha512 = require('js-sha512');
const authDAL = require('../DAL/authDAL');
const reqip = require('request-ip')
const parser = new UAParser();

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

const generateRandomNumber = () => {
  const buf = crypto.randomBytes(16);
  return buf.toString('hex');
};

exports.generateCaptchaAndSalt = (req, res) => {
  let code = '';
  try {
    console.log("authIP",reqip.getClientIp(req),req.params.type);
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
       captcha[0] =captcha[0].toLowerCase();
       captcha[4] =captcha[4].toLowerCase();
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
    console.log(reqip.getClientIp(req),'remoteAddress',req.body.captcha === req.session.captcha,req.body.captcha , req.session.captcha);
    if (req.body.captcha === req.session.captcha) {
      const result = await authDAL.CheckLogIn(req.body);
      if (result.length > 0) {
        if (sha512(result[0].Password + req.session.salt) === req.body.password) {
          req.session.role = result[0].User_Type;
          req.session.userID = req.body.userID;
          req.session.username = result[0].FullName;
          req.session.cookie.maxAge = 1800000;
          // req.session.cookie.maxAge =60000;
          req.session.salt = generateRandomNumber();

          const tempSession = req.session;
          req.session.regenerate((err) => {
            Object.assign(req.session, tempSession);
          });
          // authDAL.addActivityLog('/signIn', 'SELECT', 'POST', req.session.userID, ip.address(), getURL(req), req.device.type.toUpperCase(), `${parser.setUA(req.headers['user-agent']).getOS().name} ${parser.setUA(req.headers['user-agent']).getOS().version}`, `${parser.setUA(req.headers['user-agent']).getBrowser().name} ${parser.setUA(req.headers['user-agent']).getBrowser().version}`);
          req.session.save((err) => {
            // const cookieOption = {
            //   path: '/',
            //   httpOnly: false,
            //   secure: 'auto',
            //   sameSite: true,
            //   maxAge: 1800000,
            //   signed: true
            // };
            // res.cookie('auth.cookie', req.session.username + req.session.role, cookieOption);
            res.send({
              username: req.session.username, role: req.session.role, message: true
            });
          });
        } else {
          res.send({
            message: 'Invalid Username or Password1.'
          });
        }
      } else {
        res.send({
          message: 'Invalid Username or Password2.'
        });
      }
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