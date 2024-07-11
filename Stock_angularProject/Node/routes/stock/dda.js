var express = require('express');
var router = express.Router();
var bodyParser = require('body-parser');
var ddaBalModule = require('../../BAL/ddaBal');
const permit = require('../../BAL/permission');


router.get('/allFillFinYr', permit.permission('DIST'), ddaBalModule.allFillFinYr);
router.get('/FillCropCategory', permit.permission('DIST'), ddaBalModule.FillCropCategory);
router.get('/FillCropByCategoryId', permit.permission('DIST'), ddaBalModule.FillCropByCategoryId);
router.get('/fillCurrentstockPosition', permit.permission('DIST'), ddaBalModule.fillCurrentstockPosition);
router.get('/fillGodownWiseStock', permit.permission('DIST'), ddaBalModule.fillGodownWiseStock);
router.get('/FillDistrict', permit.permission('DIST'), ddaBalModule.FillDistrict);
router.post('/fillStateStockPosition', permit.permission('DIST'), ddaBalModule.fillStateStockPosition);
router.get('/FillCategoryId', permit.permission('DIST'), ddaBalModule.FillCategoryId);
router.get('/blockwisetransctiondetails', permit.permission('DIST'), ddaBalModule.blockwisetransctiondetails);
router.get('/blockwiseStockDetails', permit.permission('DIST'), ddaBalModule.blockwiseStockDetails);
router.get('/delaeListDistWise', permit.permission('DIST'), ddaBalModule.delaeListDistWise);
router.get('/dealerwisePaymentDetails', permit.permission('DIST'), ddaBalModule.dealerwisePaymentDetails);
router.post('/paymentStatusByFarmeId', permit.permission('DIST'), ddaBalModule.paymentStatusByFarmeId);
router.get('/GetDistCodeFromDist', permit.permission('DIST'), ddaBalModule.GetDistCodeFromDist);
router.get('/GetDealerSaleDetails', permit.permission('DIST'),  ddaBalModule.GetDealerSaleDetails);
router.get('/GetFarmerSaleDetails', permit.permission('DIST'),   ddaBalModule.GetFarmerSaleDetails);
router.get('/getMPRDistrict', permit.permission('DIST'),   ddaBalModule.getMPRDistrict);
router.get('/FillDistrictWiseBlock', permit.permission('DIST'),   ddaBalModule.FillDistrictWiseBlock);
router.get('/FillMPRYear', permit.permission('DIST'),   ddaBalModule.FillMPRYear);
router.post('/selectedDistrictMPR', permit.permission('DIST'),   ddaBalModule.selectedDistrictMPR);
router.post('/selectedDistrictMPR2', permit.permission('DIST'),   ddaBalModule.selectedDistrictMPR2);










module.exports = router;