const express = require('express');

const router = express.Router();
const authBAL = require('../bal/authBal');
router.get('/generateCaptchaAndSalt/:type/:length', authBAL.generateCaptchaAndSalt);
router.post('/CheckLogIn', authBAL.CheckLogIn);
router.get('/getUserDetails/:role/:username', authBAL.getUserDetails);
router.post('/signOut', authBAL.signOut);
router.get('/getmarqueData', authBAL.getmarqueData);
// router.get('/mobileAosignin/:userID/:password', authBAL.mobileAosignin);
module.exports = router;