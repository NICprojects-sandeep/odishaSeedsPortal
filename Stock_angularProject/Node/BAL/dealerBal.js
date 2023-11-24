const ip = require('ip');
const UAParser = require('ua-parser-js');
const crypto = require('crypto');
const sha512 = require('js-sha512');
const dealerDal = require('../DAL/dealerDal');
const reqip = require('request-ip');
const request = require('request');
var http = require('http');
var https = require('https');
const parser = new UAParser();
// const axios = require('axios');
const moment = require('moment');

exports.GetDealerLicenceByDistCodeUserType = async (req, res) => {
    try {
        const result = await dealerDal.GetDealerLicenceByDistCodeUserType(req.session.distCode);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.GetDealerLicenceByDistCodeUserTypePacs = async (req, res) => {
    try {
        const result = await dealerDal.GetDealerLicenceByDistCodeUserTypePacs(req.session.distCode);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.FILLFINYR = async (req, res) => {
    try {
        console.log(req.session);
        const result = await dealerDal.FILLFINYR();
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.FILLSEASSION = async (req, res) => {
    try {
        const result = await dealerDal.FILLSEASSION(req.query);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.FILL_GODOWN = async (req, res) => {
    try {
        const result = await dealerDal.FILL_GODOWN(req.session.distCode, req.query.prebookedsale);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.FILL_CROPCATAGORY = async (req, res) => {
    try {
        const result = await dealerDal.FILL_CROPCATAGORY(req.query.selectedGodown);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.FILLCROPNAME = async (req, res) => {
    try {
        const result = await dealerDal.FILLCROPNAME(req.query.selectedCategory, req.query.selectedGodown);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.FILLCROPVARIETY = async (req, res) => {
    try {
        const result = await dealerDal.FILLCROPVARIETY(req.query.selectedCrop, req.query.selectedCategory, req.query.selectedGodown);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.prebookingDetailsOfDealer = async (req, res) => {
    try {
        const result = await dealerDal.prebookingDetailsOfDealer(req.query.SelectedDealerOrPacs, req.session.distCode);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.fillAvailableStockDetails = async (req, res) => {
    try {
        const result = await dealerDal.fillAvailableStockDetails(req.query);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.getSupplyType = async (req, res) => {
    try {
        const result = await dealerDal.getSupplyType();
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.fillDealerSaleDeatils = async (req, res) => {
    try {
        req.body.distCode = req.session.DIST_CODE;
        req.body.distCode = req.session.distCode_1;
        req.body.nicdistCode = req.session.nicdistCode;
        req.body.UPDATED_BY = req.session.userID;
        req.body.ipAdress = reqip.getClientIp(req);

        const result = await dealerDal.fillDealerSaleDeatils(req.body);


        // res.send(result);




        let objUserBel1 = {};
        objUserBel1.dist_Code = (req.body.nicdistCode).toString();
        objUserBel1.APIKEY = "key01001";
        const apiUrl = 'https://osscpayment.nic.in/addSellStockbySIS';

        for (const e of req.body.VALUES) {
            objUserBel1.lot_Number = e.LOT_NO;
            objUserBel1.source_ID = e.Godown_ID;
            objUserBel1.sourceType = "GoDown";
            objUserBel1.receiver_ID = req.body.SALE_TO;
            objUserBel1.receiverType = req.body.IS_PACS == 'true' ? 'PACS' : 'Dealer';
            objUserBel1.partyName = req.body.APP_FIRMNAME;
            objUserBel1.qty_Per_Bag_Kg = e.BAG_SIZE_KG;
            objUserBel1.no_of_Bag = e.NO_OF_BAGS;
            objUserBel1.date_sale = moment(req.body.SALE_DATE).format('DD/MM/YYYY');
            objUserBel1.year = req.body.FIN_YR;
            objUserBel1.season = req.body.SEASSION;
            objUserBel1.ch_Number = req.body.DD_NUMBER;
            objUserBel1.ch_Date = moment(req.body.SALE_DATE).format('DD/MM/YYYY');
            objUserBel1.crop = e.CROP_ID;
            objUserBel1.cropName = e.Crop_Name;
            objUserBel1.variety = e.CROP_VERID;
            objUserBel1.varietyName = e.Crop_VerName;
            objUserBel1.Class = e.Class;
            objUserBel1.UserID = req.body.UPDATED_BY;
            objUserBel1.UserIP = req.body.ipAdress;
            objUserBel1.CropCatg_ID = e.CATEGORY_ID;
            objUserBel1.CASH_MEMO_NO = result.CASH_MEMO_NO;
            try {
                request({
                    url: "https://osscpayment.nic.in/addSellStockbySIS",
                    method: "POST",
                    json: true,   // <--Very important!!!
                    body: objUserBel1
                }, async function (error, response, body) {
                    if (error) {
                        console.log(error);
                    }
                    else {
                        const result1 = await dealerDal.updateSaledetails(result.CASH_MEMO_NO, e.LOT_NO);
                    }
                });
                // const response = await axios.post(apiUrl, objUserBel1);


                // Handle the response accordingly
            } catch (error) {
                console.log(error, 'error');
                // Handle the error properly, log or throw it again
            }
            // try {
            //     const response = await axios.post(apiUrl, objUserBel1);
            //     const result1 = await dealerDal.updateSaledetails(result.CASH_MEMO_NO,e.LOT_NO);

            //     // Handle the response accordingly
            // } catch (error) {
            //     console.log(error,'error');
            //     // Handle the error properly, log or throw it again
            // }
        }
        setTimeout(() => {
            res.send(result);
        }, 5000);

    } catch (e) {
        res.status(500).send(e);
        // console.log(e);
        // throw e;
    }
};
exports.cashmemodetails = async (req, res) => {
    try {
        const result = await dealerDal.cashmemodetails(req.query.applicationid, req.session.userID);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.FillLots = async (req, res) => {
    try {
        const result = await dealerDal.FillLots(req.session.userID);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.FillCrop = async (req, res) => {
    try {
        const result = await dealerDal.FillCrop();
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.FillVariety = async (req, res) => {
    try {
        const result = await dealerDal.FillVariety(req.query.selectedCrop);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.addinClass = async (req, res) => {
    try {
        req.body.UPDATED_BY = req.session.userID;
        req.body.ipAdress = reqip.getClientIp(req);

        const result = await dealerDal.addinClass(req.body);

        res.send(result);

    } catch (e) {
        res.status(500).send(e);
        // throw e;
    }
};
exports.allFillFinYr = async (req, res) => {
    try {
        const result = await dealerDal.allFillFinYr();
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.FillCropCategory = async (req, res) => {
    try {
        const result = await dealerDal.FillCropCategory();
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.FillCropByCategoryId = async (req, res) => {
    try {
        const result = await dealerDal.FillCropByCategoryId(req.query.SelectedCropCatagory);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.fillGodownwisestock = async (req, res) => {
    try {
        req.query.DIST_CODE = req.session.distCode;
        const result = await dealerDal.fillGodownwisestock(req.query);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.receivedetails = async (req, res) => {
    try {
        req.body.distCode = req.session.distCode
        const result = await dealerDal.receivedetails(req.body);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.dateWiseSaleDetails = async (req, res) => {
    try {
        req.body.distCode = req.session.distCode
        const result = await dealerDal.dateWiseSaleDetails(req.body);
        if (result.length > 0) {
            const result1 = await dealerDal.dateWiseSaleDetailswithdealerdata(result);
            res.send(result1);
        }
        else {
            res.send(result);
        }

    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.dateWiseGodownTransferDetails = async (req, res) => {
    try {
        req.body.distCode = req.session.distCode
        const result = await dealerDal.dateWiseGodownTransferDetails(req.body);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.saledetails = async (req, res) => {
    try {
        req.body.distCode = req.session.distCode
        const result = await dealerDal.saledetails(req.body);
        if (result.length > 0) {
            const result1 = await dealerDal.saledetailswithdealerdata(result);
            res.send(result1);
        }
        else {
            res.send(result);
        }

    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.getGodownmaster = async (req, res) => {
    try {
        const result = await dealerDal.getGodownmaster(req.query);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.GetDistCodeFromDist = async (req, res) => {
    try {
        req.query.distCode = req.session.distCode
        const result = await dealerDal.GetDistCodeFromDist(req.query);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.paymentStatusByFarmeId = async (req, res) => {
    try {
        const result = await dealerDal.paymentStatusByFarmeId(req.body);
        res.send(result);

    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.submitSeedSubsidyOfGrountnut = async (req, res) => {
    try {
        req.body.UPDATED_BY = req.session.userID;
        const result = await dealerDal.submitSeedSubsidyOfGrountnut(req.body);
        res.send(result);

    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.ddutrnocheack = async (req, res) => {
    try {
        const result = await dealerDal.ddutrnocheack(req.body);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.FillGoDownByDistCodeUserType = async (req, res) => {
    try {
        const result = await dealerDal.FillGoDownByDistCodeUserType(req.session.distCode_1);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.FillAgencyByOSSC = async (req, res) => {
    try {
        const result = await dealerDal.FillAgencyByOSSC();
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.FillSourceByAgencyIdUserTypeValues = async (req, res) => {
    try {
        const result = await dealerDal.FillSourceByAgencyIdUserTypeValues(req.query.AgenciesID);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.FillGovtFarmByDistCode = async (req, res) => {
    try {
        const result = await dealerDal.FillGovtFarmByDistCode(req.session.distCode_1, req.query.AgenciesID);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.agencyNameReload = async (req, res) => {
    try {
        const result = await dealerDal.agencyNameReload(req.session.distCode_1, req.query.selectedScheme);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.FillCropVarietyByOutsideAgencies = async (req, res) => {
    try {
        const result = await dealerDal.FillCropVarietyByOutsideAgencies(req.query.Crop_Code);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.FillCropVarietyByGovtFarm = async (req, res) => {
    try {
        req.query.distcode = req.session.distCode_1;
        const result = await dealerDal.FillCropVarietyByGovtFarm(req.query);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.FillCropVarietyByOUAT = async (req, res) => {
    try {
        req.query.distcode = req.session.distCode_1;
        const result = await dealerDal.FillCropVarietyByOUAT(req.query);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.FillCropVarietyByMOUAgency = async (req, res) => {
    try {
        req.query.distcode = req.session.distCode_1;
        const result = await dealerDal.FillCropVarietyByMOUAgency(req.query);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.FillCropVarietyByCropIdScheme = async (req, res) => {
    try {
        req.query.distcode = req.session.distCode_1;
        const result = await dealerDal.FillCropVarietyByCropIdScheme(req.query);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.FillLotByGovtFarm = async (req, res) => {
    try {
        const result = await dealerDal.FillLotByGovtFarm(req.query);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.fillBagExpiryDate = async (req, res) => {
    try {
        const result = await dealerDal.fillBagExpiryDate(req.query);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.fillBagsFromStockStockDetails = async (req, res) => {
    try {
        const result = await dealerDal.fillBagsFromStockStockDetails(req.query);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.Stock_Sp_InsReceiveDetails = async (req, res) => {
    try {
        req.body.USERID = req.session.userID;
        req.body.USERIP = reqip.getClientIp(req);
        const result = await dealerDal.Stock_Sp_InsReceiveDetails(req.body);
        // res.send(result);

        let objUserBEL1 = {};
        objUserBEL1.dist_Code = req.session.nicdistCode.toString();
        objUserBEL1.year = req.body.FIN_YR;
        if(req.body.SESSION=='R'){
            objUserBEL1.season ='Rabi' 
        }
        else{
            objUserBEL1.season ='Kharif'
        }
        objUserBEL1.pr_Number = req.body.Challan_No;

        objUserBEL1.source_ID = req.body.Receive_Unitcd;
        objUserBEL1.receiver_ID = req.body.Godown_ID;
        objUserBEL1.receiverType = "godown";
        objUserBEL1.date_Intake = req.body.mReceive_Date;
        objUserBEL1.agencyName = req.body.AgName;
        objUserBEL1.SourceType = req.body.SourceType;

        objUserBEL1.UserID = req.body.USERID;
        objUserBEL1.UserIP = req.body.USERIP;
        for (const e of req.body.XML_Value) {
            objUserBEL1.CropCatg_ID = e.CATEGORY_ID;
            objUserBEL1.lot_Number = e.enteredLotno;
            objUserBEL1.qty_Per_Bag_Kg = e.enteredBagsize;
            objUserBEL1.no_of_Bag = e.enteredRecvNoOfBags;
            objUserBEL1.crop = e.CROP_ID;
            objUserBEL1.variety = e.Variety_Code;
            objUserBEL1.cropName = e.Crop_Name;
            objUserBEL1.varietyName = e.Variety_Name;
            objUserBEL1.Class = e.CROP_CLASS;
            try {
                request({
                    url: "https://osscpayment.nic.in/addStockbySIS",
                    method: "POST",
                    json: true,   // <--Very important!!!
                    body: objUserBEL1
                }, async function (error, response, body) {
                    if (error) {
                        console.log(error);
                    }
                    else {
                        // const result1 = await dealerDal.updateSaledetails(result.CASH_MEMO_NO, e.LOT_NO);
                    }
                });
                // const response = await axios.post(apiUrl, objUserBel1);


                // Handle the response accordingly
            } catch (error) {
                console.log(error, 'error');
                // Handle the error properly, log or throw it again
            }
            // try {
            //     const response = await axios.post(apiUrl, objUserBel1);
            //     const result1 = await dealerDal.updateSaledetails(result.CASH_MEMO_NO,e.LOT_NO);

            //     // Handle the response accordingly
            // } catch (error) {
            //     console.log(error,'error');
            //     // Handle the error properly, log or throw it again
            // }
        }
        setTimeout(() => {
            res.send(result);
        }, 3000);

    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};