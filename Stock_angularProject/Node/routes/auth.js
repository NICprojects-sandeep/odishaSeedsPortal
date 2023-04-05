const express = require('express');

const router = express.Router();
const authBAL = require('../BAL/authBal');

router.get('/generateCaptchaAndSalt/:type/:length', authBAL.generateCaptchaAndSalt);
router.post('/signIn', authBAL.signIn);
router.get('/getUserDetails/:role/:username', authBAL.getUserDetails);
router.post('/signOut', authBAL.signOut);
// router.get('/mobileAosignin/:userID/:password', authBAL.mobileAosignin);
module.exports = router;