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
    res.redirect('http://164.100.140.77/public/transferOfSeedSubsidy?userID=' + userID)
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
    const result = await publicModule.GetIncentiveOilSeedAll(req.query);
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

router.get('/getcropList',publicBal.getcropList)
router.get('/manojdata', publicBal.manojdata);
router.get('/manojdata1', publicBal.manojdata1);

router.get('/manoj', async (req, res) => {
    res.setHeader('Access-Control-Allow-Origin', '*');
    res.setHeader('Access-Control-Allow-Methods', 'GET, POST, OPTIONS, PUT, PATCH, DELETE'); // If needed
    res.setHeader('Access-Control-Allow-Headers', 'X-Requested-With,content-type'); // If needed
    res.setHeader('Access-Control-Allow-Credentials', true);
    res.render('monoj')
  
});



// http://localhost:3000/publicR/seedLicDataInserted
// {"Licdetails":[{"SN":"9398","APP_FIRMNAME":"BHISMARATHI SAHU","APP_TYPE":"Proprietor","APR_UPTO":"2027-09-25T09:25:40.413Z","LIC_NO":"e-Lic/SEED/2022D-12168","APPMOB_NO":"9668473766","AADHAR_NO":"363170223020","userId":"bhismarathi123@rediffmail.com","SALTED_PASSWORD":"e744e28f9e56b2fff36b8f0b283d36cf981b282ac2d67effe72c18f17482b14f38f10cb3a0d8519cb467c8368456c79aad851a30169e802c714df259f6a66821","DIST_CODE":"358","DIST_NAME":"KALAHANDI","DAO_CD":"ADO_44","DAO_NAME":"NARALA","APR_DATE":"2022-09-26T09:25:40.555Z","PHOTO":"../../public/uploads/seed/DPP1650176646386.jpeg"}],
// "userdetails":[{"LIC_NO":"e-Lic/SEED/2022D-12168","APPNAME":"Bhismarathi Sahu",	"APPADDRESS":"854",	"DIST_NAME":"KALAHANDI","APPDIST_ID":"358","BLOCK_NAME":"MADANPUR RAMPUR","APPBLOCK_ID":"3419",	"APPPIN":"766102","APPEMAIL_ID":"bhismarathi123@rediffmail.com","APPICARD_TYPE":"AADHAR",	"APPCARD_NO":"363170223020","APPPHOTO":"../../public/uploads/seed/DPP1650176646386.jpeg","APPMOB_NO":"9668473766"}],
// "listofseed":[{"LIC_NO":"e-Lic/SEED/2022D-12168","COMP_NAME":"Pan Seeds Pvt Ltd"}]}
module.exports = router;




