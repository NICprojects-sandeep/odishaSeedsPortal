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
const axios = require('axios');

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
        const result = await dealerDal.FILLFINYR();
        console.log(req.session);
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
        console.log(req.query);
        const result = await dealerDal.prebookingDetailsOfDealer(req.query.SelectedDealerOrPacs, req.session.distCode);
        res.send(result);
    } catch (e) {
        res.status(500).send(e);
        throw e;
    }
};
exports.fillAvailableStockDetails = async (req, res) => {
    try {
        console.log(req.query);
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
        console.log(req.session);
        req.body.DIST_CODE = req.session.DIST_CODE;
        req.body.distCode = req.session.distCode;
        req.body.nicdistCode = req.session.nicdistCode;
        req.body.UPDATED_BY = req.session.userID;
        req.body.ipAdress = reqip.getClientIp(req);

        // const result = await dealerDal.fillDealerSaleDeatils(req.body);
        // for (const e of req.body.VALUES) {
        //     var objUserBEL1 = {};
        //     objUserBEL1.dist_Code = req.body.nicdistCode;
        //     objUserBEL1.lot_Number = e.LOT_NO;
        //     objUserBEL1.source_ID = e.Godown_ID;
        //     objUserBEL1.sourceType = "GoDown";
        //     objUserBEL1.receiver_ID = req.body.SALE_TO;
        //     objUserBEL1.receiverType = req.body.IS_PACS == 'true' ? 'PACS' : 'Dealer';
        //     objUserBEL1.partyName = req.body.APP_FIRMNAME;
        //     objUserBEL1.qty_Per_Bag_Kg = e.BAG_SIZE_KG;
        //     objUserBEL1.no_of_Bag = e.NO_OF_BAGS;
        //     objUserBEL1.date_sale = req.body.SALE_DATE;
        //     objUserBEL1.year = req.body.FIN_YR;
        //     objUserBEL1.season = req.body.SEASSION;
        //     objUserBEL1.ch_Number = req.body.DD_NUMBER;
        //     objUserBEL1.ch_Date = req.body.SALE_DATE;
        //     objUserBEL1.crop = e.CROP_ID;
        //     objUserBEL1.cropName = e.Crop_Name;
        //     objUserBEL1.variety = e.CROP_VERID;
        //     objUserBEL1.varietyName = e.Crop_VerName;
        //     objUserBEL1.Class = 'Certified';
        //     objUserBEL1.UserID = req.body.UPDATED_BY;
        //     objUserBEL1.UserIP = req.body.ipAdress;
        //     objUserBEL1.CropCatg_ID = e.CATEGORY_ID;
        //     objUserBEL1.CASH_MEMO_NO = 'Cashmemeo number';
        //     objUserBEL1.APIKEY = "key01001";
        //     const apiUrl = 'http://164.100.140.107/transferPortal/auth/login';

        //     const response = await axios.post(apiUrl, objUserBEL1);
        // }

        // res.send(result);




        let objUserBel1 = {};
        objUserBel1.distCode = req.body.nicdistCode;
        objUserBel1.APIKEY = "key01001";
        const apiUrl = 'http://164.100.140.107/transferPortal/auth/login';

        for (const e of req.body.VALUES) {
            objUserBel1.lotNumber = e.LOT_NO;
            objUserBel1.sourceID = e.Godown_ID;
            objUserBel1.sourceType = "GoDown";
            objUserBel1.receiverID = req.body.SALE_TO;
            objUserBel1.receiverType = req.body.IS_PACS == 'true' ? 'PACS' : 'Dealer';
            objUserBel1.partyName = req.body.APP_FIRMNAME;
            objUserBel1.qtyPerBagKg = e.BAG_SIZE_KG;
            objUserBel1.noOfBag = e.NO_OF_BAGS;
            objUserBel1.dateSale = req.body.SALE_DATE;
            objUserBel1.year = req.body.FIN_YR;
            objUserBel1.season = req.body.SEASSION;
            objUserBel1.chNumber = req.body.DD_NUMBER;
            objUserBel1.chDate = req.body.SALE_DATE;
            objUserBel1.crop = e.CROP_ID;
            objUserBel1.cropName = e.Crop_Name;
            objUserBel1.variety = e.CROP_VERID;
            objUserBel1.varietyName = e.Crop_VerName;
            objUserBel1.Class = 'Certified';
            objUserBel1.UserID = req.body.UPDATED_BY;
            objUserBel1.UserIP = req.body.ipAdress;
            objUserBel1.cropCatgID = e.CATEGORY_ID;
            objUserBel1.CASH_MEMO_NO = 'Cashmemeo number';

            try {
                const response = await axios.post(apiUrl, objUserBel1);
                console.log(response);
                // Handle the response accordingly
            } catch (error) {
                console.log('error');
                // Handle the error properly, log or throw it again
            }
        }

    } catch (e) {
        res.status(500).send(e);
        // console.log(e);
        // throw e;
    }
};