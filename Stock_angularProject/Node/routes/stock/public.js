var express = require('express');
var router = express.Router();
var bodyParser = require('body-parser');
var publicModule = require('../../models/stock/publicBal');
var sha256 = require('js-sha256');
var csrf = require('csurf');
var csrfProtection = csrf();
var parseForm = bodyParser.urlencoded({ extended: false });
var cache = require('cache-headers');
var cors = require('cors');
var publicBal = require('../../BAL/publicBal');


var overrideConfig = {
    'maxAge': 2000,
    'setPrivate': true
};
// router.post('/submitSeedSubsidyOfGrountnut', publicModule.submitSeedSubsidyOfGrountnut);
router.post('/submitSeedSubsidyOfGrountnut', async (req, res, next) => {//ODSON1/2014-15/0001/E
    res.get('X-Frame-Options');
    const result = await publicModule.submitSeedSubsidyOfGrountnut(req.body);
    res.send(result)
});
router.get('/TransforOfSeedSubsidyForGroundnut', function (req, res) {
    res.setHeader('Access-Control-Allow-Origin', '*');
    res.setHeader('Access-Control-Allow-Methods', 'GET, POST, OPTIONS, PUT, PATCH, DELETE'); // If needed
    res.setHeader('Access-Control-Allow-Headers', 'X-Requested-With,content-type'); // If needed
    res.setHeader('Access-Control-Allow-Credentials', true);
    // res.render('acknowledgementsugam')
    const userID = req.query.userID
    res.redirect('https://odishaseedsportal.nic.in/public/transferOfSeedSubsidy?userID=' + userID)
});
router.get('/getUserDetails', async (req, res) => {
    const result = await publicModule.getUserDetails(req.query.userID);
    res.send(result)
});
router.get('/getSeedsubsidyforgroundnut', async (req, res) => {
    const result = await publicModule.getSeedsubsidyforgroundnut(req.query.userID);
    res.send(result)
});
router.post('/seedLicDataInserted', async (req, res) => {
    res.setHeader('Access-Control-Allow-Origin', '*');
    res.setHeader('Access-Control-Allow-Methods', 'GET, POST, OPTIONS, PUT, PATCH, DELETE'); // If needed
    res.setHeader('Access-Control-Allow-Headers', 'X-Requested-With,content-type'); // If needed
    res.setHeader('Access-Control-Allow-Credentials', true);
    const result4 = await publicModule.getSEEDLICDIST(req.body.Licdetails);
    const result1 = await publicModule.getSEEDLICAPPDIST(req.body.userdetails);
    const result2 = await publicModule.getSEEDLICCOMPDIST(req.body.listofseed);
    const result3 = await publicModule.getdata();
    res.send(true)

});
router.post('/getTranscationDtl', async (req, res) => {
    const result = await publicModule.getTranscationDtl(req.body);
    res.send(result)
});
router.post('/deleteTranscationDtl', async (req, res) => {
    const result = await publicModule.deleteTranscationDtl(req.body);
    res.send(result)
});
router.get('/getprebookingDtl', async (req, res) => {
    const result = await publicModule.getprebookingDtl();
    res.send(result)
});
router.post('/getSearchprebookingDtl', async (req, res) => {
    const result = await publicModule.getSearchprebookingDtl(req.body);
    res.send(result)
});
router.post('/getprebookingDtlDelerWise', async (req, res) => {
    const result = await publicModule.getprebookingDtlDelerWise(req.body);
    res.send(result)
});
router.post('/dataDeletePrebookingDtl', async (req, res) => {
    const result = await publicModule.dataDeletePrebookingDtl(req.body);
    res.send(result)
});
router.get('/canceledPrebookingDtl', async (req, res) => {
    res.setHeader('Access-Control-Allow-Origin', '*');
    res.setHeader('Access-Control-Allow-Methods', 'GET, POST, OPTIONS, PUT, PATCH, DELETE'); // If needed
    res.setHeader('Access-Control-Allow-Headers', 'X-Requested-With,content-type'); // If needed
    res.setHeader('Access-Control-Allow-Credentials', true);
    // res.render('acknowledgementsugam')
    const result = await publicModule.canceledPrebookingDtl(req.query.date);
    res.send(result)
});
router.get('/successPrebookingDtl', async (req, res) => {
    res.setHeader('Access-Control-Allow-Origin', '*');
    res.setHeader('Access-Control-Allow-Methods', 'GET, POST, OPTIONS, PUT, PATCH, DELETE'); // If needed
    res.setHeader('Access-Control-Allow-Headers', 'X-Requested-With,content-type'); // If needed
    res.setHeader('Access-Control-Allow-Credentials', true);
    const result = await publicModule.successPrebookingDtl(req.query.date);
    res.send(result)
});
router.post('/getCashMemoNoDtl', async (req, res) => {
    const result = await publicModule.getCashMemoNoDtl(req.body);
    res.send(result)
});
router.post('/FillSaleTransDtls', async (req, res) => {
    const result = await publicModule.FillSaleTransDtls(req.body);
    res.send(result)
});
router.post('/CancelCashMemo', async (req, res) => {
    const result = await publicModule.CancelCashMemo(req.body);
    res.send(result)
});
router.get('/getPrebookingDistrict', async (req, res) => {
    const result = await publicModule.getPrebookingDistrict(req.query);
    res.send(result)
});
router.get('/getCrop', async (req, res) => {
    const result = await publicModule.getCrop();
    res.send(result)
});
router.get('/getVariey', async (req, res) => {
    const result = await publicModule.getVariey(req.query);
    res.send(result)
});
router.get('/getLivedata', async (req, res) => {
    const result = await publicModule.getLivedata();
    res.send(result)
});
router.get('/GetIncentiveOilSeed', async (req, res) => {
    const result = await publicModule.GetIncentiveOilSeed();
    res.send(result)
});
router.post('/InsertIncentiveOilSeed', async (req, res) => {
    const result = await publicModule.InsertIncentiveOilSeed(req.body);
    res.send(result)
});
router.get('/FillIncentiveOilSeed', async (req, res) => {
    const result = await publicModule.FillIncentiveOilSeed(req.query);
    res.send(result)
});
router.get('/FillPendingIncentiveOilSeed', async (req, res) => {
    const result = await publicModule.FillPendingIncentiveOilSeed(req.query);
    res.send(result)
});
router.post('/OsscAprvOilSeed', async (req, res) => {
    const result = await publicModule.OsscAprvOilSeed(req.body);
    res.send(result)
});
//all

router.get('/GetIncentiveOilSeedAll', async (req, res) => {
    const result = await publicModule.GetIncentiveOilSeed(req.query);
    res.send(result)
});
router.post('/InsertIncentiveOilSeedAll', async (req, res) => {
    const result = await publicModule.InsertIncentiveOilSeedAll(req.body);
    res.send(result)
});
router.get('/FillIncentiveOilSeedAll', async (req, res) => {
    const result = await publicModule.FillIncentiveOilSeedAll(req.query);
    res.send(result)
});
router.get('/FillPendingIncentiveOilSeedAll', async (req, res) => {
    const result = await publicModule.FillPendingIncentiveOilSeedAll(req.query);
    res.send(result)
});
router.post('/OsscAprvOilSeedAll', async (req, res) => {
    const result = await publicModule.OsscAprvOilSeedAll(req.body);
    res.send(result)
});
//route-->bal-->dal
router.get('/getStockPricelist', publicBal.getStockPricelist);
router.get('/getDistrict', publicBal.getDistrict);
router.get('/getDealerDetails', publicBal.getDealerDetails);
router.get('/getBlock', publicBal.getBlock);
router.get('/getblockWiseDealer', publicBal.getblockWiseDealer);
router.get('/getcropList',publicBal.getcropList);
router.get('/graphVariety',publicBal.graphVariety);
router.get('/manojdata', publicBal.manojdata);
router.get('/manojdata1', publicBal.manojdata1);
router.get('/manoj', async (req, res) => {
    res.setHeader('Access-Control-Allow-Origin', '*');
    res.setHeader('Access-Control-Allow-Methods', 'GET, POST, OPTIONS, PUT, PATCH, DELETE'); // If needed
    res.setHeader('Access-Control-Allow-Headers', 'X-Requested-With,content-type'); // If needed
    res.setHeader('Access-Control-Allow-Credentials', true);
    res.render('monoj')
  
});
router.get('/getStockPricelistAfter', publicBal.getStockPricelistAfter);
router.post('/AddGodwns', publicBal.AddGodwns);
router.post('/AddGodwn', publicBal.AddGodwn);
router.post('/AddSeed', publicBal.AddSeed);
router.get('/AadhaarVaultEncryption', publicBal.AadhaarVaultEncryption);
router.get('/GetIncentiveOilSeedOuat', async (req, res) => {
    const result = await publicModule.GetIncentiveOilSeedOuat(req.query);
    res.send(result)
});
router.post('/InsertIncentiveOilSeedOuat', async (req, res) => {
    const result = await publicModule.InsertIncentiveOilSeedOuat(req.body);
    res.send(result)
});
router.get('/FillIncentiveOilSeedOUAT', async (req, res) => {
    const result = await publicModule.FillIncentiveOilSeedOUAT(req.query);
    res.send(result)
});
router.post('/OsscAprvOilSeedOUAT', async (req, res) => {
    const result = await publicModule.OsscAprvOilSeedOUAT(req.body);
    res.send(result)
});
module.exports = router;


// CREATE OR REPLACE PROCEDURE public.insert_treasurytransaction(
// 	IN myaction text DEFAULT NULL::text,
// 	IN industry_code character varying DEFAULT NULL::character varying,
// 	IN userid character varying DEFAULT NULL::character varying,
// 	IN trs_head character varying DEFAULT NULL::character varying,
// 	IN trsid integer DEFAULT NULL::integer,
// 	IN amount numeric DEFAULT NULL::numeric,
// 	IN paymenttowards character varying DEFAULT NULL::character varying,
// 	IN purpose_cd character varying DEFAULT NULL::character varying,
// 	IN ee_code character varying DEFAULT NULL::character varying,
// 	IN ip_address character varying DEFAULT NULL::character varying)
// LANGUAGE 'plpgsql'
// AS $BODY$
// DECLARE
//     Output int;
// BEGIN
// 	Output := NULL;
//     IF MyAction = 'Ins_Trs_Transaction' THEN
//         IF EXISTS (SELECT Industry_Code FROM Registration WHERE UserID = UserID) THEN
//             -- Fetch the Industry_Code
//             SELECT Industry_Code INTO Industry_Code FROM Registration WHERE UserID = UserID;
            
//             -- Insert into TreasuryTransaction
//             INSERT INTO TreasuryTransaction (Industry_Code, Trs_Head, Amount, PaymentTowards, Purpose_cd, Status, EE_Code, IP_Address)
//             VALUES (Industry_Code, Trs_Head, Amount, PaymentTowards, Purpose_cd, '0', EE_Code, IP_Address);
            
//             -- Check if the insert was successful
//             IF FOUND THEN
//                 SELECT max(TrsID) INTO Output FROM TreasuryTransaction;
//             ELSE
//                 Output = -2;
//             END IF;
//         ELSE
//             Output = -3;
//         END IF;
//     END IF;
// END;
// $BODY$;
// ALTER PROCEDURE public.insert_treasurytransaction(text, character varying, character varying, character varying, integer, numeric, character varying, character varying, character varying, character varying)
//     OWNER TO postgres;

