const express = require('express');

const router = express.Router();
const authBAL = require('../BAL/authBal');
router.get('/generateCaptchaAndSalt/:type/:length', authBAL.generateCaptchaAndSalt);
router.post('/CheckLogIn', authBAL.CheckLogIn);
router.post('/getUserDetails', authBAL.getUserDetails);
router.post('/signOut', authBAL.signOut);
router.get('/getmarqueData', authBAL.getmarqueData);

router.post('/OneDealerLogin', authBAL.OneDealerLogin);
// router.get('/mobileAosignin/:userID/:password', authBAL.mobileAosignin);
module.exports = router;