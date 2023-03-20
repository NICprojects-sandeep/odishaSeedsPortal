var dbConfig = require('../dbConfig');
var sql = dbConfig.sql;
var sqlstock = dbConfig.sqlstock;
var locConfigstock = dbConfig.locConfigStock;
var sequelizeStock = dbConfig.sequelizeStock;
var locConfigSeed = dbConfig.locConfigSeed;
var sequelizeSeed = dbConfig.sequelizeSeed;
var locConfigOssopoca = dbConfig.locConfigOssopoca;
var sequelizeOssopoca = dbConfig.sequelizeOssopoca;



exports.submitSeedSubsidyOfGrountnut = (data) => new Promise(async (resolve, reject) => {
    // resolve(true)
    var con = new sqlstock.ConnectionPool(locConfigstock);
    try {
        sequelizeStock.query(`insert into "TblSeedSubsidy" (FarmerId,BagSize,noOfBags,Quintal,Source,Amount,insertedBy,dateOfInsert) values(:farmerid,:bagSize,:noOfBags,:Quintal,:Source,:Amount,:insertedBy,getdate()) select @@rowcount`, {
            replacements: { farmerid: data.farmerid, bagSize: data.bagSize, noOfBags: data.noOfBags, Quintal: data.Quintal, Source: data.Source, Amount: data.Amount, insertedBy: data.insertedBy }, type: sequelizeStock.QueryTypes.INSERT
        }).then(function success(data1) {
            resolve(true);
        })
    } catch (e) {
        console.log(`Oops! An error occurred: ${e}`);
    }
})
exports.getUserDetails = (userID) => new Promise(async (resolve, reject) => {
    try {
        return sequelizeStock.query(`select c.Short_Name from [dafpSeed].[dbo].[SEED_LIC_DIST] A
        inner join [dafpSeed].[dbo].[SEED_LIC_APP_DIST] B on A.SEED_LIC_DIST_ID = B.SEED_LIC_DIST_ID
        inner join [dafpSeed].[dbo].[DIST] C on A.DIST_CODE = C.dist_code
        where A.IS_OSSC=1 and B.APPEMAIL_ID=:userID`, {
            replacements: { userID: userID }, type: sequelizeStock.QueryTypes.SELECT
        }).then(function success(data) {
            resolve(data);
        }).catch(function error(err) {
            console.log('An error occurred...', err);
        });
    } catch (e) {
        console.log(`Oops! An error occurred: ${e}`);
    }
});
exports.getSeedsubsidyforgroundnut = (userID) => new Promise(async (resolve, reject) => {
    try {
        return sequelizeStock.query(`select * from TblSeedSubsidy where insertedBy=:userID and is_active=1`, {
            replacements: { userID: userID }, type: sequelizeStock.QueryTypes.SELECT
        }).then(function success(data) {
            resolve(data);
        }).catch(function error(err) {
            console.log('An error occurred...', err);
        });
    } catch (e) {
        console.log(`Oops! An error occurred: ${e}`);
    }
});
exports.getSEEDLICDIST = (arr) => new Promise(async (resolve, reject) => {
    try {
        var con = new sql.ConnectionPool(locConfigSeed);
        con.connect().then(function success() {
            const tableSEEDLICDIST = new sql.Table();
            tableSEEDLICDIST.create = true;
            tableSEEDLICDIST.columns.add('APP_FIRMNAME', sql.VarChar(5000), { nullable: false });
            tableSEEDLICDIST.columns.add('APP_TYPE', sql.VarChar(50), { nullable: false });
            tableSEEDLICDIST.columns.add('APR_UPTO', sql.DateTime, { nullable: true });
            tableSEEDLICDIST.columns.add('LIC_NO', sql.VarChar(50), { nullable: false });
            tableSEEDLICDIST.columns.add('APPMOB_NO', sql.VarChar(50), { nullable: false });
            tableSEEDLICDIST.columns.add('AADHAR_NO', sql.VarChar(50), { nullable: false });
            tableSEEDLICDIST.columns.add('userId', sql.VarChar(50), { nullable: false });
            tableSEEDLICDIST.columns.add('SALTED_PASSWORD', sql.VarChar(5000), { nullable: false });
            tableSEEDLICDIST.columns.add('DIST_CODE', sql.VarChar(50), { nullable: false });
            tableSEEDLICDIST.columns.add('DIST_NAME', sql.VarChar(50), { nullable: false });
            tableSEEDLICDIST.columns.add('DAO_CD', sql.VarChar(50), { nullable: false });
            tableSEEDLICDIST.columns.add('DAO_NAME', sql.VarChar(50), { nullable: false });
            tableSEEDLICDIST.columns.add('APR_DATE', sql.DateTime, { nullable: false });
            tableSEEDLICDIST.columns.add('PHOTO', sql.VarChar(5000), { nullable: false });

            for (var i = 0; i < arr.length; i++) {
                tableSEEDLICDIST.rows.add(arr[i].APP_FIRMNAME, arr[i].APP_TYPE, new Date(arr[i].APR_UPTO), arr[i].LIC_NO, arr[i].APPMOB_NO, arr[i].AADHAR_NO, arr[i].userId, arr[i].SALTED_PASSWORD, arr[i].DIST_CODE, arr[i].DIST_NAME, arr[i].DAO_CD, arr[i].DAO_NAME, new Date(arr[i].APR_DATE), arr[i].PHOTO);
            }
            const request = new sql.Request(con);
            request.input('tableSEEDLICDIST', tableSEEDLICDIST);
            request.execute('spSubmitSEEDLICDIST', function (err, result) {
                if (err) {
                    console.log('An error occurred...', err);
                }
                else {
                    resolve(result.returnValue);
                }
                con.close();
            });
        }).catch(function error(err) {
            console.log('An error occurred...', err);
        });
    } catch (e) {
        console.log(`Oops! An error occurred: ${e}`);
    }
});
exports.getSEEDLICAPPDIST = (arr) => new Promise(async (resolve, reject) => {
    try {
        var con = new sql.ConnectionPool(locConfigSeed);
        con.connect().then(function success() {
            const tableSEEDLICAPPDIST = new sql.Table();
            tableSEEDLICAPPDIST.create = true;
            tableSEEDLICAPPDIST.columns.add('LIC_NO', sql.VarChar(50), { nullable: false });
            tableSEEDLICAPPDIST.columns.add('APPNAME', sql.VarChar(5000), { nullable: false });
            tableSEEDLICAPPDIST.columns.add('APPADDRESS', sql.VarChar(5000), { nullable: true });
            tableSEEDLICAPPDIST.columns.add('DIST_NAME', sql.VarChar(50), { nullable: false });
            tableSEEDLICAPPDIST.columns.add('APPDIST_ID', sql.VarChar(50), { nullable: false });
            tableSEEDLICAPPDIST.columns.add('BLOCK_NAME', sql.VarChar(50), { nullable: false });
            tableSEEDLICAPPDIST.columns.add('APPBLOCK_ID', sql.VarChar(50), { nullable: false });
            tableSEEDLICAPPDIST.columns.add('APPPIN', sql.VarChar(50), { nullable: false });
            tableSEEDLICAPPDIST.columns.add('APPEMAIL_ID', sql.VarChar(50), { nullable: false });
            tableSEEDLICAPPDIST.columns.add('APPICARD_TYPE', sql.VarChar(50), { nullable: false });
            tableSEEDLICAPPDIST.columns.add('APPCARD_NO', sql.VarChar(50), { nullable: false });
            tableSEEDLICAPPDIST.columns.add('APPPHOTO', sql.VarChar(5000), { nullable: false });
            tableSEEDLICAPPDIST.columns.add('APPMOB_NO', sql.VarChar(50), { nullable: false });

            for (var i = 0; i < arr.length; i++) {
                tableSEEDLICAPPDIST.rows.add(arr[i].LIC_NO, arr[i].APPNAME, arr[i].APPADDRESS, arr[i].DIST_NAME, arr[i].APPDIST_ID, arr[i].BLOCK_NAME, arr[i].APPBLOCK_ID, arr[i].APPPIN, arr[i].APPEMAIL_ID, arr[i].APPICARD_TYPE, arr[i].APPCARD_NO, arr[i].APPPHOTO, arr[i].APPMOB_NO);
            }
            const request = new sql.Request(con);

            request.input('tableSEEDLICAPPDIST', tableSEEDLICAPPDIST);
            request.execute('spSubmitSEEDLICAPPDIST', function (err, result) {
                if (err) {
                    console.log('An error occurred...', err);
                }
                else {
                    resolve(result.returnValue);
                }
                con.close();
            });
        }).catch(function error(err) {
            console.log('An error occurred...', err);
        });
    } catch (e) {
        console.log(`Oops! An error occurred: ${e}`);
    }
});
exports.getSEEDLICCOMPDIST = (arr) => new Promise(async (resolve, reject) => {
    try {
        var con = new sql.ConnectionPool(locConfigSeed);
        con.connect().then(function success() {
            const tableSEEDLICCOMPDIST = new sql.Table();
            tableSEEDLICCOMPDIST.create = true;
            tableSEEDLICCOMPDIST.columns.add('LIC_NO', sql.VarChar(50), { nullable: false });
            tableSEEDLICCOMPDIST.columns.add('COMP_NAME', sql.VarChar(5000), { nullable: false });

            for (var i = 0; i < arr.length; i++) {
                tableSEEDLICCOMPDIST.rows.add(arr[i].LIC_NO, arr[i].COMP_NAME);
            }
            const request = new sql.Request(con);

            request.input('tableSEEDLICCOMPDIST', tableSEEDLICCOMPDIST);
            request.execute('spSubmitSEEDLICCOMPDIST', function (err, result) {
                if (err) {
                    console.log('An error occurred...', err);
                }
                else {
                    resolve(result.returnValue);
                }
                con.close();
            });
        }).catch(function error(err) {
            console.log('An error occurred...', err);
        });
    } catch (e) {
        console.log(`Oops! An error occurred: ${e}`);
    }
});
exports.getdata = () => new Promise(async (resolve, reject) => {
    try {
        var con = new sql.ConnectionPool(locConfigSeed);
        con.connect().then(function success() {
            const request = new sql.Request(con);
            request.execute('spSeedLicDataInserted', function (err, result) {
                if (err) {
                    console.log('An error occurred...', err);
                }
                else {
                    resolve(result.returnValue);
                }
                con.close();
            });
        }).catch(function error(err) {
            console.log('An error occurred...', err);
        });
    } catch (e) {
        console.log(`Oops! An error occurred: ${e}`);
    }
});
// exports.getTranscationDtl = () => new Promise(async (resolve, reject) => {
//     try {
//         var con = new sql.ConnectionPool(locConfigSeed);
//         con.connect().then(function success() {     
//             const request = new sql.Request(con);
//             request.execute('spSeedLicDataInserted', function (err, result) {
//                 if (err) {
//                     console.log('An error occurred...', err);
//                 }
//                 else {
//                     resolve(result.returnValue);
//                 }
//                 con.close();
//             });
//         }).catch(function error(err) {
//             console.log('An error occurred...', err);
//         });
//     } catch (e) {
//         console.log(`Oops! An error occurred: ${e}`);
//     }
// });
exports.getTranscationDtl = (data) => new Promise(async (resolve, reject) => {
    var con = new sqlstock.ConnectionPool(locConfigstock);
    try {
        con.connect().then(function success() {
            const request = new sql.Request(con);
            request.input('transactionid', data.transactionId);
            request.execute('SP_showtransactiondetails', function (err, result) {
                if (err) {
                    console.log('An error occurred...', err);
                }
                else {
                    // callback(result.recordset);
                    resolve(result.recordset)
                }
                con.close();
            });
        }).catch(function error(err) {
            console.log('An error occurred...', err);
        });
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        con.release();
    }
});
exports.deleteTranscationDtl = (data) => new Promise(async (resolve, reject) => {
    var con = new sqlstock.ConnectionPool(locConfigstock);
    try {
        con.connect().then(function success() {
            const request = new sql.Request(con);
            request.input('transactionid', data.transactionId);
            request.input('CANCEL_BY', 'ADMIN');
            request.input('CANCEL_IP', '10.172.0.78');
            request.output('VAL');
            request.execute('SP_deletetransactionpRACTISE', function (err, result) {
                if (err) {
                    console.log('An error occurred...', err);
                }
                else {
                    // callback(result.recordset);
                    resolve(result.output)
                }
                con.close();
            });
        }).catch(function error(err) {
            console.log('An error occurred...', err);
        });
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        con.release();
    }
});
exports.getprebookingDtl = () => new Promise(async (resolve, reject) => {
    var con = new sqlstock.ConnectionPool(locConfigstock);
    try {
        const totalprebookedorder = await sequelizeStock.query(`select count (bookingID) as totalprebookedorder from prebookinglist where IS_ACTIVE=1 ;`, {
            replacements: {}, type: sequelizeStock.QueryTypes.SELECT
        });
        const totalprebookedquantity = await sequelizeStock.query(`select  sum( cast (quantity as decimal(10,2)))/100 as totalprebookedquantity from prebookinglist where IS_ACTIVE=1 ;`, {
            replacements: {}, type: sequelizeStock.QueryTypes.SELECT
        });

        const totalcollectorder = await sequelizeStock.query(`select a.dealerId  from prebookinglist a
        left join [dafpSeed].[dbo].SEED_LIC_DIST g on a.dealerId = g.LIC_NO1 left join STOCK_DEALERSTOCK h on g.LIC_NO=h.LICENCE_NO and h.FIN_YR='2022-23' and h.SEASSION='R'
        where a.IS_ACTIVE=1  group by a.dealerId,a.beneficiaryType,a.distID,a.cropCode,a.varietyCode,a.monthOfPurchase,APP_FIRMNAME
		having sum(AVL_QUANTITY) >= cast((sum( cast (bagSize as decimal(10,2)))*sum( cast (noOfBag as decimal(10,2))))/100 as decimal(10,2)) `, {
            replacements: {}, type: sequelizeStock.QueryTypes.SELECT
        });
        const totalcollectorderquantity = await sequelizeStock.query(`select cast((sum( cast (bagSize as decimal(10,2)))*sum( cast (noOfBag as decimal(10,2))))/100 as decimal(10,2)) as collectqty
        from prebookinglist a left join [dafpSeed].[dbo].SEED_LIC_DIST g on a.dealerId = g.LIC_NO1
        left join STOCK_DEALERSTOCK h on g.LIC_NO=h.LICENCE_NO and h.FIN_YR='2022-23' and h.SEASSION='R'
        where a.IS_ACTIVE=1  group by a.dealerId,a.beneficiaryType,a.distID,a.cropCode,a.varietyCode,a.monthOfPurchase,APP_FIRMNAME
		having sum(AVL_QUANTITY) >= cast((sum( cast (bagSize as decimal(10,2)))*sum( cast (noOfBag as decimal(10,2))))/100 as decimal(10,2)) `, {
            replacements: {}, type: sequelizeStock.QueryTypes.SELECT
        });

        const totalpendingorder = await sequelizeStock.query(`select count (bookingID) as totalpendingorder from prebookinglist where IS_ACTIVE=1 ;`, {
            replacements: {}, type: sequelizeStock.QueryTypes.SELECT
        });
        const totalpendingorderquantity = await sequelizeStock.query(`select  sum( cast (quantity as int)) as totalpendingorderquantity from prebookinglist  where TRANSACTION_ID is  null and cancelstatus is null;`, {
            replacements: {}, type: sequelizeStock.QueryTypes.SELECT
        });
        // select distinct a.dealerId,b.beneficiaryType,c.beneficiaryType ,prebookedquanitybydealer, prebookedquanitybyfarmer,a.distID,District_Name,Crop_Name,a.cropCode,a.monthOfPurchase,a.TRANSACTION_ID, isnull(prebookedquanitybydealer,0)+isnull(prebookedquanitybyfarmer,0) as sum1,Variety_Name,APP_FIRMNAME,sum(AVL_QUANTITY) as liftedqty
        // from prebookinglist a
        // left join (select sum(cast(quantity as int)) prebookedquanitybydealer,dealerId,beneficiaryType,cropCode,distID,monthOfPurchase,varietyCode from prebookinglist where beneficiaryType='D' and cancelstatus is null group by dealerId,beneficiaryType,cropCode,distID,monthOfPurchase,varietyCode) b 
        // on a.dealerId=b.dealerId and a.cropCode=b.cropCode and a.distID=b.distID and a.monthOfPurchase= b.monthOfPurchase and a.varietyCode= b.varietyCode
        // left join (select sum(cast(quantity as int)) prebookedquanitybyfarmer,dealerId,beneficiaryType,cropCode,distID,monthOfPurchase,varietyCode from prebookinglist where beneficiaryType='F' and cancelstatus is null group by dealerId,beneficiaryType,cropCode,distID,monthOfPurchase,varietyCode) c 
        // on a.dealerId=c.dealerId  and a.cropCode=c.cropCode and a.distID=c.distID and a.monthOfPurchase= c.monthOfPurchase and a.varietyCode= c.varietyCode
        // inner join [PDS_LG_DistMap] d on a.distID = d.[Dist_Code]
        // inner join mCrop e on a.cropCode = e.Crop_Code
        // inner join mCropVariety f on a.varietyCode = f.Variety_Code
        // left join [dafpSeed].[dbo].SEEDLICDIST g on a.dealerId = g.lic_no
        // left join STOCK_DEALERSTOCK h on g.LICNO1=h.LICENCE_NO and h.FIN_YR='2022-23' and h.SEASSION='R'
        // where cancelstatus is null 
        // group by a.dealerId,b.beneficiaryType,c.beneficiaryType ,prebookedquanitybydealer, prebookedquanitybyfarmer,a.distID,District_Name,Crop_Name,a.cropCode,a.monthOfPurchase,a.TRANSACTION_ID,Variety_Name,APP_FIRMNAME
        // order by  District_Name,APP_FIRMNAME,Crop_Name,Variety_Name,monthOfPurchase,c.beneficiaryType
        // cast((sum( cast (bagSize as decimal(10,2)))*sum( cast (noOfBag as decimal(10,2))))/100 as decimal(10,2))

        // cast((sum( cast (bagSize as decimal(10,2)))*sum( cast (noOfBag as decimal(10,2))))/100 as decimal(10,2))
        const totalprebookingdtl = await sequelizeStock.query(`
        select distinct a.dealerId,a.beneficiaryType ,a.distID,District_Name,Crop_Name,a.cropCode,a.varietyCode,Variety_Name,a.monthOfPurchase,APP_FIRMNAME,
        sum(cast(a.quantity as decimal(10,2)))/100 as prebookingquanity,sum(AVL_QUANTITY) as availableQuanity
        from prebookinglist a
        inner join [PDS_LG_DistMap] d on a.distID = d.[Dist_Code]
        inner join mCrop e on a.cropCode = e.Crop_Code
        inner join mCropVariety f on a.varietyCode = f.Variety_Code
        left join [dafpSeed].[dbo].SEED_LIC_DIST g on a.dealerId = g.LIC_NO1
		left join STOCK_DEALERSTOCK h on g.LIC_NO=h.LICENCE_NO and h.FIN_YR='2022-23' and h.SEASSION='R'
		where a.IS_ACTIVE=1
		group by a.dealerId,a.beneficiaryType,a.distID,District_Name,Crop_Name,a.cropCode,a.monthOfPurchase,Variety_Name,APP_FIRMNAME,a.varietyCode
		order by  District_Name,APP_FIRMNAME,Crop_Name,Variety_Name,monthOfPurchase,a.beneficiaryType
        `, {
            replacements: {}, type: sequelizeStock.QueryTypes.SELECT
        });

        const data = {
            totalprebookedorder: totalprebookedorder,
            totalprebookedquantity: totalprebookedquantity,
            totalcollectorder: totalcollectorder,
            totalcollectorderquantity: totalcollectorderquantity,
            totalpendingorder: totalpendingorder,
            totalpendingorderquantity: totalpendingorderquantity,
            totalprebookingdtl: totalprebookingdtl,
        }
        resolve(data);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        con.release();
    }
});
exports.getSearchprebookingDtl = (data) => new Promise(async (resolve, reject) => {
    var con = new sqlstock.ConnectionPool(locConfigstock);
    try {
        const totalprebookedorder = await sequelizeStock.query(`select count (bookingID) as totalprebookedorder from prebookinglist where IS_ACTIVE=1 and (Season = :selectedSeason or :selectedSeason = '0') and (monthOfPurchase = :selectedDemandMonth or :selectedDemandMonth = '0') and (distID = :selectedDistrict or :selectedDistrict = '0') and (cropCode = :selectedCrop or :selectedCrop = '0') and  (varietyCode = :selectedVariety or :selectedVariety = '0');`, {
            replacements: { selectedSeason: data.selectedSeason, selectedDistrict: data.selectedDistrict, selectedCrop: data.selectedCrop, selectedVariety: data.selectedVariety, selectedDemandMonth: data.selectedDemandMonth }, type: sequelizeStock.QueryTypes.SELECT
        });
        const totalprebookedquantity = await sequelizeStock.query(`select  sum( cast (quantity as decimal(10,2)))/100 as totalprebookedquantity from prebookinglist where IS_ACTIVE=1 and (Season = :selectedSeason or :selectedSeason = '0') and (monthOfPurchase = :selectedDemandMonth or :selectedDemandMonth = '0') and (distID = :selectedDistrict or :selectedDistrict = '0') and (cropCode = :selectedCrop or :selectedCrop = '0') and  (varietyCode = :selectedVariety or :selectedVariety = '0') ;`, {
            replacements: { selectedSeason: data.selectedSeason, selectedDistrict: data.selectedDistrict, selectedCrop: data.selectedCrop, selectedVariety: data.selectedVariety, selectedDemandMonth: data.selectedDemandMonth }, type: sequelizeStock.QueryTypes.SELECT
        });

        const totalcollectorder = await sequelizeStock.query(`select a.dealerId  from prebookinglist a
        left join [dafpSeed].[dbo].SEED_LIC_DIST g on a.dealerId = g.LIC_NO1 left join STOCK_DEALERSTOCK h on g.LIC_NO=h.LICENCE_NO and h.FIN_YR='2022-23' and h.SEASSION='R'
        where a.IS_ACTIVE=1 and (a.Season = :selectedSeason or :selectedSeason = '0') and (a.monthOfPurchase = :selectedDemandMonth or :selectedDemandMonth = '0') and (a.distID = :selectedDistrict or :selectedDistrict = '0') and (a.cropCode = :selectedCrop or :selectedCrop = '0') and  (a.varietyCode = :selectedVariety or :selectedVariety = '0')  group by a.dealerId,a.beneficiaryType,a.distID,a.cropCode,a.varietyCode,a.monthOfPurchase,APP_FIRMNAME
		having sum(AVL_QUANTITY) >= cast((sum( cast (bagSize as decimal(10,2)))*sum( cast (noOfBag as decimal(10,2))))/100 as decimal(10,2)) `, {
            replacements: { selectedSeason: data.selectedSeason, selectedDistrict: data.selectedDistrict, selectedCrop: data.selectedCrop, selectedVariety: data.selectedVariety, selectedDemandMonth: data.selectedDemandMonth }, type: sequelizeStock.QueryTypes.SELECT
        });
        const totalcollectorderquantity = await sequelizeStock.query(`select cast((sum( cast (bagSize as decimal(10,2)))*sum( cast (noOfBag as decimal(10,2))))/100 as decimal(10,2)) as collectqty
        from prebookinglist a left join [dafpSeed].[dbo].SEED_LIC_DIST g on a.dealerId = g.LIC_NO1
        left join STOCK_DEALERSTOCK h on g.LIC_NO=h.LICENCE_NO and h.FIN_YR='2022-23' and h.SEASSION='R'
        where a.IS_ACTIVE=1 and (a.Season = :selectedSeason or :selectedSeason = '0') and (a.monthOfPurchase = :selectedDemandMonth or :selectedDemandMonth = '0') and (a.distID = :selectedDistrict or :selectedDistrict = '0') and (a.cropCode = :selectedCrop or :selectedCrop = '0') and  (a.varietyCode = :selectedVariety or :selectedVariety = '0')  group by a.dealerId,a.beneficiaryType,a.distID,a.cropCode,a.varietyCode,a.monthOfPurchase,APP_FIRMNAME
		having sum(AVL_QUANTITY) >= cast((sum( cast (bagSize as decimal(10,2)))*sum( cast (noOfBag as decimal(10,2))))/100 as decimal(10,2)) `, {
            replacements: { selectedSeason: data.selectedSeason, selectedDistrict: data.selectedDistrict, selectedCrop: data.selectedCrop, selectedVariety: data.selectedVariety, selectedDemandMonth: data.selectedDemandMonth }, type: sequelizeStock.QueryTypes.SELECT
        });

        const totalpendingorder = await sequelizeStock.query(`select count (bookingID) as totalpendingorder from prebookinglist where IS_ACTIVE=1 and (Season = :selectedSeason or :selectedSeason = '0') and (monthOfPurchase = :selectedDemandMonth or :selectedDemandMonth = '0') and (distID = :selectedDistrict or :selectedDistrict = '0') and (cropCode = :selectedCrop or :selectedCrop = '0') and  (varietyCode = :selectedVariety or :selectedVariety = '0') ;`, {
            replacements: { selectedSeason: data.selectedSeason, selectedDistrict: data.selectedDistrict, selectedCrop: data.selectedCrop, selectedVariety: data.selectedVariety, selectedDemandMonth: data.selectedDemandMonth }, type: sequelizeStock.QueryTypes.SELECT
        });
        const totalpendingorderquantity = await sequelizeStock.query(`select  sum( cast (quantity as int)) as totalpendingorderquantity from prebookinglist  where TRANSACTION_ID is  null and cancelstatus is null;`, {
            replacements: { selectedSeason: data.selectedSeason, selectedDistrict: data.selectedDistrict, selectedCrop: data.selectedCrop, selectedVariety: data.selectedVariety, selectedDemandMonth: data.selectedDemandMonth }, type: sequelizeStock.QueryTypes.SELECT
        });

        // cast((sum( cast (bagSize as decimal(10,2)))*sum( cast (noOfBag as decimal(10,2))))/100 as decimal(10,2)) 
        const totalprebookingdtl = await sequelizeStock.query(`select distinct a.dealerId,a.beneficiaryType ,a.distID,District_Name,Crop_Name,a.cropCode,a.varietyCode,Variety_Name,a.monthOfPurchase,APP_FIRMNAME,
         sum(cast(a.quantity as decimal(10,2)))/100
         as prebookingquanity,sum(AVL_QUANTITY) as availableQuanity
        from prebookinglist a
        inner join [PDS_LG_DistMap] d on a.distID = d.[Dist_Code]
        inner join mCrop e on a.cropCode = e.Crop_Code
        inner join mCropVariety f on a.varietyCode = f.Variety_Code
        left join [dafpSeed].[dbo].SEED_LIC_DIST g on a.dealerId = g.LIC_NO1
		left join STOCK_DEALERSTOCK h on g.LIC_NO=h.LICENCE_NO and h.FIN_YR='2022-23' and h.SEASSION='R'
		where a.IS_ACTIVE=1 and (a.Season = :selectedSeason or :selectedSeason = '0') and (a.monthOfPurchase = :selectedDemandMonth or :selectedDemandMonth = '0') and (a.distID = :selectedDistrict or :selectedDistrict = '0') and (a.cropCode = :selectedCrop or :selectedCrop = '0') and  (a.varietyCode = :selectedVariety or :selectedVariety = '0') 
		group by a.dealerId,a.beneficiaryType,a.distID,District_Name,Crop_Name,a.cropCode,a.monthOfPurchase,Variety_Name,APP_FIRMNAME,a.varietyCode
		order by  District_Name,APP_FIRMNAME,Crop_Name,Variety_Name,monthOfPurchase,a.beneficiaryType`, {
            replacements: { selectedSeason: data.selectedSeason, selectedDistrict: data.selectedDistrict, selectedCrop: data.selectedCrop, selectedVariety: data.selectedVariety, selectedDemandMonth: data.selectedDemandMonth }, type: sequelizeStock.QueryTypes.SELECT
        });
        const data1 = {
            totalprebookedorder: totalprebookedorder,
            totalprebookedquantity: totalprebookedquantity,
            totalcollectorder: totalcollectorder,
            totalcollectorderquantity: totalcollectorderquantity,
            totalpendingorder: totalpendingorder,
            totalpendingorderquantity: totalpendingorderquantity,
            totalprebookingdtl: totalprebookingdtl,
        }
        resolve(data1);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        con.release();
    }
});
exports.getprebookingDtlDelerWise = (data) => new Promise(async (resolve, reject) => {
    var con = new sqlstock.ConnectionPool(locConfigstock);
    try {
        const delerLicenceNo = await sequelizeStock.query(`select lic_no1  from [dafpSeed].dbo.seed_lic_dist where lic_no=:dealerId`, {
            replacements: { dealerId: data.dealerId }, type: sequelizeStock.QueryTypes.SELECT
        });
        const prebookingdtldelerwise = await sequelizeStock.query(`select applicationID,bookingID,District_Name,distID,beneficiaryType,beneficiaryId,cropCode,Crop_Name,Variety_Code,Variety_Name,bagSize,noOfBag,quantity,monthOfPurchase,totalPaidAmt from prebookinglist a
        inner join [PDS_LG_DistMap] d on a.distID = d.[Dist_Code]
        inner join mCrop e on a.cropCode = e.Crop_Code
        inner join mCropVariety f on a.varietyCode = f.Variety_Code
        where a.IS_ACTIVE=1 and a.dealerId= :dealerId and cancelstatus is null 
        order by dealerId, dateofInsert;`, {
            replacements: { dealerId: delerLicenceNo[0].lic_no1 }, type: sequelizeStock.QueryTypes.SELECT
        });

        resolve(prebookingdtldelerwise);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        con.release();
    }
});
exports.dataDeletePrebookingDtl = (data) => new Promise(async (resolve, reject) => {
    var con = new sqlstock.ConnectionPool(locConfigstock);
    try {
        const prebookingdtldelerwise = await sequelizeStock.query(` update [prebookinglist] set cancelstatus = '0' ,
        dateofcancel=getdate(),canceledby=:dealerId,remarkbydeler=:remark where applicationID=:applicationID`, {
            replacements: { applicationID: data.selectedApplicationID, dealerId: data.delerID, remark: data.remark }, type: sequelizeStock.QueryTypes.SELECT
        });
        resolve(true);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        con.release();
    }
});
exports.canceledPrebookingDtl = (date) => new Promise(async (resolve, reject) => {
    var con = new sqlstock.ConnectionPool(locConfigstock);
    try {
        const year = date.substring(date.length - 4);
        const Month = date.substring(date.length - 5, 3)
        const Date = date.substring(date.length - 10, 2)
        const entrydate = year + '-' + Month + '-' + Date + ' 00:00:00.000'
        const lastdate = year + '-' + Month + '-' + Date + ' 23:59:59.000'
        const prebookingdtldelerwise = await sequelizeStock.query(`select applicationID,dateofcancel,canceledby,remarkbydeler from  prebookinglist where cancelstatus='0'
         and dateofcancel between :entrydate and :lastdate  order by dateofcancel;`, {
            replacements: { entrydate: entrydate, lastdate: lastdate }, type: sequelizeStock.QueryTypes.SELECT
        });

        resolve(prebookingdtldelerwise);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        con.release();
    }
});
exports.successPrebookingDtl = (date) => new Promise(async (resolve, reject) => {
    var con = new sqlstock.ConnectionPool(locConfigstock);
    try {
        const year = date.substring(date.length - 4);
        const Month = date.substring(date.length - 5, 3)
        const Date = date.substring(date.length - 10, 2)
        const entrydate = year + '-' + Month + '-' + Date + ' 00:00:00.000'
        const lastdate = year + '-' + Month + '-' + Date + ' 23:59:59.000'
        const prebookingdtldelerwise = await sequelizeStock.query(`SELECT a.applicationID,a.TRANSACTION_ID,b.UPDATED_BY,b.UPDATED_ON from [prebookinglist] a
        inner join [STOCK_FARMER_2021-22_R] b on a.applicationID= b.PREBOOKING_APPLICATIONID and UPDATED_ON between :entrydate and :lastdate order by UPDATED_ON`, {
            replacements: { entrydate: entrydate, lastdate: lastdate }, type: sequelizeStock.QueryTypes.SELECT
        });
        resolve(prebookingdtldelerwise);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        con.release();
    }
});
exports.getCashMemoNoDtl = (data) => new Promise(async (resolve, reject) => {
    try {
        const result = await sequelizeStock.query(`SELECT A.SALETRANSID,A.GODOWN_ID,E.GODOWN_NAME,CONVERT(VARCHAR(10),A.SALE_DATE,103)SALE_DATE,B.LIC_NO,B.LIC_NO,B.APP_FIRMNAME,APP_FIRMNAME,C.Crop_Name,D.Variety_Name,A.LOT_NUMBER,A.BAG_SIZE_KG,A.SALE_NO_OF_BAG,CONVERT(DECIMAL(10,2),CONVERT(DECIMAL(10,2),BAG_SIZE_KG )*CONVERT(DECIMAL(10,2),SALE_NO_OF_BAG )/100) AS BAGS_QTL,A.UPDATED_BY,F.SUPPLY_NAME,A.USER_TYPE  FROM Stock_SaleDetails A LEFT OUTER JOIN [DAFPSEED].[DBO].[SEED_LIC_DIST] B ON A.SALE_TO = B.LIC_NO
        LEFT OUTER JOIN mCROP C ON A.CROP_ID = C.Crop_Code 
        LEFT OUTER JOIN mCROPVARIETY D ON A.CROP_VERID = D.Variety_Code  
        LEFT OUTER JOIN Stock_Godown_Master E ON A.GODOWN_ID = E.GODOWN_ID 
        LEFT OUTER JOIN Stock_SupplyType F ON A.SUPPLY_TYPE = F.SUPPLY_ID 
        WHERE A.CASH_MEMO_NO =:transactionId ORDER BY A.SALETRANSID`, {
            replacements: { transactionId: data.transactionId }, type: sequelizeStock.QueryTypes.SELECT
        });
        resolve(result);

    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {

    }
});
exports.FillSaleTransDtls = (data) => new Promise(async (resolve, reject) => {
    var con = new sqlstock.ConnectionPool(locConfigstock);
    try {
        con.connect().then(function success() {
            const request = new sql.Request(con);
            request.input('SALETRANSID', data.SALETRANSID);
            request.execute('Stock_SP_SELCASHMEMO', function (err, result) {
                if (err) {
                    console.log('An error occurred...', err);
                }
                else {
                    // callback(result.recordset);
                    resolve(result.recordset)
                }
                con.close();
            });
        }).catch(function error(err) {
            console.log('An error occurred...', err);
        });
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        con.release();
    }
});
exports.CancelCashMemo = (data) => new Promise(async (resolve, reject) => {
    var con = new sqlstock.ConnectionPool(locConfigstock);
    try {
        con.connect().then(function success() {
            const request = new sql.Request(con);
            request.input('SALETRANSID', data.SALETRANSID);
            request.input('CANCEL_BY', 'ADMIN');
            request.input('CANCEL_IP', '10.172.0.78');
            request.output('VAL');
            request.execute('Stock_SP_CANCELCASHMEMO', function (err, result) {
                if (err) {
                    console.log('An error occurred...', err);
                }
                else {
                    // callback(result.recordset);
                    resolve(result.output)
                }
                con.close();
            });
        }).catch(function error(err) {
            console.log('An error occurred...', err);
        });
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        con.release();
    }
});
exports.getPrebookingDistrict = (data) => new Promise(async (resolve, reject) => {
    try {
        return sequelizeStock.query(`select Dist_Code,District_Name from [PDS_LG_DistMap] `, {
            replacements: { Season: data.selectedSeason }, type: sequelizeStock.QueryTypes.SELECT
        }).then(function success(data) {
            resolve(data);
        }).catch(function error(err) {
            console.log('An error occurred...', err);
        });
    } catch (e) {
        console.log(`Oops! An error occurred: ${e}`);
    }

});
exports.getCrop = () => new Promise(async (resolve, reject) => {
    try {
        return sequelizeStock.query(`select Crop_Code,Crop_Name from mCrop where IS_ACTIVE=1 order by Crop_Name `, {
            replacements: {}, type: sequelizeStock.QueryTypes.SELECT
        }).then(function success(data) {
            resolve(data);
        }).catch(function error(err) {
            console.log('An error occurred...', err);
        });
    } catch (e) {
        console.log(`Oops! An error occurred: ${e}`);
    }

});
exports.getVariey = (data) => new Promise(async (resolve, reject) => {
    try {
        return sequelizeStock.query(`select Variety_Code,Variety_Name from mCropVariety where IS_ACTIVE=1 and Crop_Code=:Crop_Code order by Variety_Name `, {
            replacements: { Crop_Code: data.selectedCrop }, type: sequelizeStock.QueryTypes.SELECT
        }).then(function success(data) {
            resolve(data);
        }).catch(function error(err) {
            console.log('An error occurred...', err);
        });
    } catch (e) {
        console.log(`Oops! An error occurred: ${e}`);
    }

});
exports.getLivedata = () => new Promise(async (resolve, reject) => {
    try {
        return sequelizeStock.query(`select Crop_Name,Variety_Name,availableData from TargetOfPrebooking a
        inner join mCrop b on a.cropCode=b.Crop_Code
        inner join mCropVariety c on a.varietyCode=c.Variety_Code
        where finYear='2022-23' and season='Rabi' order by Crop_Name,Variety_Name `, {
            replacements: {}, type: sequelizeStock.QueryTypes.SELECT
        }).then(function success(data) {
            resolve(data);
        }).catch(function error(err) {
            console.log('An error occurred...', err);
        });
    } catch (e) {
        console.log(`Oops! An error occurred: ${e}`);
    }

});
//incentive
exports.GetIncentiveOilSeed = () => new Promise(async (resolve, reject) => {
    var con = new sqlstock.ConnectionPool(locConfigOssopoca);
    // var con = new sqlstock.ConnectionPool(locConfigstock);
    try {
        const totalprebookingdtl = await sequelizeOssopoca.query(`SELECT SG_Name,Father_husband_name,villg_name,Dist_name,LotNo,Ref_No,Varity_Code,Varity_Name,Class_code,ProcessedArea,Dist_code,Farmerid,Inspected_Area,(ProcessedArea/Inspected_Area)Qtl_Ha FROM dbo.VwIncentiveOilseed WHERE Farmerid IS NOT NULL and crop_code in ('C026','C027','C033','C029')  ORDER BY Farmerid,(ProcessedArea/Inspected_Area)`, {
            replacements: {}, type: sequelizeOssopoca.QueryTypes.SELECT
        });
        // const totalprebookingdtl = [{ "SG_Name": "MAHESWAR GIRI", "Father_husband_name": "RAMHARI", "villg_name": "Chalanti", "Dist_name": "BALASORE", "LotNo": "Apr/21-18-180-08G67220", "Ref_No": "BAL/08/2020-21/R/74921", "Varity_Code": "V387", "Varity_Name": "Dharani (TCGS 1043)", "Class_code": "C01", "ProcessedArea": 28.2, "Dist_code": "02", "Farmerid": "BAL/132648", "Inspected_Area": 2, "Qtl_Ha": 14.1 }, { "SG_Name": "MAHESWAR GIRI", "Father_husband_name": "RAMHARI", "villg_name": "Chalanti", "Dist_name": "BALASORE", "LotNo": "Apr/21-18-180-08G67221", "Ref_No": "BAL/08/2020-21/R/74922", "Varity_Code": "V387", "Varity_Name": "Dharani (TCGS 1043)", "Class_code": "C01", "ProcessedArea": 28.2, "Dist_code": "02", "Farmerid": "BAL/132648", "Inspected_Area": 2, "Qtl_Ha": 14.1 }, { "SG_Name": "SUNIL KUMAR MOHANTY", "Father_husband_name": "GIRISH", "villg_name": "JADIBALI", "Dist_name": "BALASORE", "LotNo": "Apr/21-18-180-08G67222", "Ref_No": "BAL/08/2020-21/R/74995", "Varity_Code": "V387", "Varity_Name": "Dharani (TCGS 1043)", "Class_code": "C01", "ProcessedArea": 57, "Dist_code": "02", "Farmerid": "Bal/149474", "Inspected_Area": 4, "Qtl_Ha": 14.25 }, { "SG_Name": "GIRISH CHANDRA MOHANTY", "Father_husband_name": "GANANATH MOHANTY", "villg_name": "JADIBALI", "Dist_name": "BALASORE", "LotNo": "Apr/21-18-180-08G67223", "Ref_No": "BAL/08/2020-21/R/74997", "Varity_Code": "V387", "Varity_Name": "Dharani (TCGS 1043)", "Class_code": "C01", "ProcessedArea": 56, "Dist_code": "02", "Farmerid": "Bal/21065", "Inspected_Area": 4, "Qtl_Ha": 14 }, { "SG_Name": "KAMALAKANTA BEHERA", "Father_husband_name": "SURENDRA BEHERA", "villg_name": "Rambindha", "Dist_name": "BALASORE", "LotNo": "Mar/21-18-315-08G67001", "Ref_No": "BAL/08/2020-21/R/74612", "Varity_Code": "V406", "Varity_Name": "Pusa Mustard 28", "Class_code": "C03", "ProcessedArea": 5.6, "Dist_code": "02", "Farmerid": "BAL/88512", "Inspected_Area": 3.2, "Qtl_Ha": 1.75 }, { "SG_Name": "SANJAY KU BEHERA", "Father_husband_name": "ARJUNA CH BEHERA", "villg_name": "Rambindha", "Dist_name": "BALASORE", "LotNo": "Feb/21-18-315-08G67003", "Ref_No": "BAL/08/2020-21/R/74613", "Varity_Code": "V406", "Varity_Name": "Pusa Mustard 28", "Class_code": "C03", "ProcessedArea": 9.8, "Dist_code": "02", "Farmerid": "BAL/88525", "Inspected_Area": 3, "Qtl_Ha": 3.266666 },
        // { "SG_Name": "BHAGIRATHA GIRI", "Father_husband_name": "RAMHARI GIRI", "villg_name": "Chalanti", "Dist_name": "BALASORE", "LotNo": "Apr/21-18-180-08G67219", "Ref_No": "BAL/08/2020-21/R/74923", "Varity_Code": "V387", "Varity_Name": "Dharani (TCGS 1043)", "Class_code": "C01", "ProcessedArea": 28.2, "Dist_code": "02", "Farmerid": "BAL/91517", "Inspected_Area": 2, "Qtl_Ha": 14.1 }, { "SG_Name": "BHAGIRATHA GIRI", "Father_husband_name": "RAMHARI GIRI", "villg_name": "Chalanti", "Dist_name": "BALASORE", "LotNo": "Apr/21-18-180-08G67218", "Ref_No": "BAL/08/2020-21/R/74924", "Varity_Code": "V387", "Varity_Name": "Dharani (TCGS 1043)", "Class_code": "C01", "ProcessedArea": 28.2, "Dist_code": "02", "Farmerid": "BAL/91517", "Inspected_Area": 2, "Qtl_Ha": 14.1 }, { "SG_Name": "DEBASIS SAHU", "Father_husband_name": "Saratchandrasahu", "villg_name": "Barikel", "Dist_name": "BARGARH", "LotNo": "May/21-18-1-01G67287", "Ref_No": "BAR/01/2020-21/R/74691", "Varity_Code": "V387", "Varity_Name": "Dharani (TCGS 1043)", "Class_code": "C01", "ProcessedArea": 32.4, "Dist_code": "03", "Farmerid": "BAR/77834", "Inspected_Area": 3, "Qtl_Ha": 10.8 }, { "SG_Name": "Sudam meher", "Father_husband_name": "Dhaneswar meher", "villg_name": "Badimal", "Dist_name": "BARGARH", "LotNo": "May/21-18-1-01G67288", "Ref_No": "BAR/01/2020-21/R/74690", "Varity_Code": "V387", "Varity_Name": "Dharani (TCGS 1043)", "Class_code": "C01", "ProcessedArea": 28.8, "Dist_code": "03", "Farmerid": "BAR/81535", "Inspected_Area": 3, "Qtl_Ha": 9.6 }, { "SG_Name": "BHIKARI CHARAN PATRA", "Father_husband_name": "BRUNDABAN", "villg_name": "Tumba Thengapalli", "Dist_name": "GANJAM", "LotNo": "APR/21-18-268-06G67342", "Ref_No": "GAN/06/2020-21/R/74711", "Varity_Code": "V387", "Varity_Name": "Dharani (TCGS 1043)", "Class_code": "C01", "ProcessedArea": 11.6, "Dist_code": "11", "Farmerid": "GAN/140038", "Inspected_Area": 4, "Qtl_Ha": 2.9 }, { "SG_Name": "RAMAKRUSHNA NAHAK", "Father_husband_name": "MAHANTAR", "villg_name": "Sunathara", "Dist_name": "GANJAM", "LotNo": "Apr/21-18-268-06G67152", "Ref_No": "GAN/06/2020-21/R/74648", "Varity_Code": "V387", "Varity_Name": "Dharani (TCGS 1043)", "Class_code": "C01", "ProcessedArea": 56, "Dist_code": "11", "Farmerid": "GAN/142386", "Inspected_Area": 4, "Qtl_Ha": 14 }, { "SG_Name": "MEENA PATEL", "Father_husband_name": "BINAYA KUMAR PATEL", "villg_name": "Mundrajore", "Dist_name": "JHARSUGUDA", "LotNo": "Mar/21-18-221-01G69054", "Ref_No": "SAM/01/2020-21/R/74687", "Varity_Code": "V406", "Varity_Name": "Pusa Mustard 28", "Class_code": "C03", "ProcessedArea": 14.08, "Dist_code": "14", "Farmerid": "JHA/21627", "Inspected_Area": 4, "Qtl_Ha": 3.52 },
        // { "SG_Name": "MANGARAJ SAHOO", "Father_husband_name": "NIRANJAN SAHOO", "villg_name": "Sadangi", "Dist_name": "KEONJHAR", "LotNo": "APR/21-18-305-05G68048", "Ref_No": "KEO/05/2020-21/R/74609", "Varity_Code": "V406", "Varity_Name": "Pusa Mustard 28", "Class_code": "C03", "ProcessedArea": 4.2, "Dist_code": "17", "Farmerid": "KEO/29781", "Inspected_Area": 2.4, "Qtl_Ha": 1.75 }, { "SG_Name": "MOTILAL SAHOO", "Father_husband_name": "KAMALAKANTA SAHOO", "villg_name": "Sadangi", "Dist_name": "KEONJHAR", "LotNo": "APR/21-18-305-05G68047", "Ref_No": "KEO/05/2020-21/R/74608", "Varity_Code": "V406", "Varity_Name": "Pusa Mustard 28", "Class_code": "C03", "ProcessedArea": 2.2, "Dist_code": "17", "Farmerid": "keo/38644", "Inspected_Area": 2.5, "Qtl_Ha": 0.88 }, { "SG_Name": "HRUDANANDA SWAIN", "Father_husband_name": "GOBINDA SWAIN", "villg_name": "Badalasasan", "Dist_name": "KHURDA", "LotNo": "Apr/21-18-59-02G66966", "Ref_No": "KHU/02/2020-21/R/74839", "Varity_Code": "V370", "Varity_Name": "CO-7", "Class_code": "C01", "ProcessedArea": 15.3, "Dist_code": "18", "Farmerid": "KHU/18873", "Inspected_Area": 1, "Qtl_Ha": 15.3 }, { "SG_Name": "AJAYA BHATTA", "Father_husband_name": "BASUDEB BHATTA", "villg_name": "Badalasasan", "Dist_name": "KHURDA", "LotNo": "Apr/21-18-59-02G66967", "Ref_No": "KHU/02/2020-21/R/74840", "Varity_Code": "V370", "Varity_Name": "CO-7", "Class_code": "C01", "ProcessedArea": 14.1, "Dist_code": "18", "Farmerid": "KHU/18967", "Inspected_Area": 1, "Qtl_Ha": 14.1 }, { "SG_Name": "BAIRAGI MALLIK", "Father_husband_name": "ANA MALLIK", "villg_name": "Panikata", "Dist_name": "KHURDA", "LotNo": "Apr/21-18-30-02G67499", "Ref_No": "KHU/02/2020-21/R/75944", "Varity_Code": "V386", "Varity_Name": "SMARAK", "Class_code": "C01", "ProcessedArea": 5.68, "Dist_code": "18", "Farmerid": "KHU/61965", "Inspected_Area": 2, "Qtl_Ha": 2.84 }, { "SG_Name": "Santosh Kumar Saha", "Father_husband_name": "Brajanandan Prasad Saha", "villg_name": "BARIPADA MUN", "Dist_name": "MAYURBHANJ", "LotNo": "May/21-18-180-05G67156", "Ref_No": "MAY/05/2020-21/R/74976", "Varity_Code": "V387", "Varity_Name": "Dharani (TCGS 1043)", "Class_code": "C01", "ProcessedArea": 41.6, "Dist_code": "21", "Farmerid": "may/100720", "Inspected_Area": 4, "Qtl_Ha": 10.4 }, { "SG_Name": "GHANASHYAM NAYAK", "Father_husband_name": "SRIDHAR NAYAK", "villg_name": "Sarbanaghati", "Dist_name": "MAYURBHANJ", "LotNo": "Apr/21-18-180-05G67164", "Ref_No": "MAY/05/2020-21/R/74991", "Varity_Code": "V370", "Varity_Name": "CO-7", "Class_code": "C01", "ProcessedArea": 19.8, "Dist_code": "21", "Farmerid": "MAY/101212", "Inspected_Area": 2.24, "Qtl_Ha": 8.839285 },
        // { "SG_Name": "AJAY KUMAR NAYAK", "Father_husband_name": "KHETRA MOHAN NAYAK", "villg_name": "Budamara", "Dist_name": "MAYURBHANJ", "LotNo": "Apr/21-18-180-05G67158", "Ref_No": "MAY/05/2020-21/R/74919", "Varity_Code": "V387", "Varity_Name": "Dharani (TCGS 1043)", "Class_code": "C01", "ProcessedArea": 49, "Dist_code": "21", "Farmerid": "MAY/11826", "Inspected_Area": 4, "Qtl_Ha": 12.25 }, { "SG_Name": "GAJENDRA KU RAJ", "Father_husband_name": "JADUNATH RAJ", "villg_name": "Sarbanaghati", "Dist_name": "MAYURBHANJ", "LotNo": "May/21-18-180-05G67163", "Ref_No": "MAY/05/2020-21/R/74989", "Varity_Code": "V370", "Varity_Name": "CO-7", "Class_code": "C01", "ProcessedArea": 37, "Dist_code": "21", "Farmerid": "MAY/124770", "Inspected_Area": 3, "Qtl_Ha": 12.333333 }, { "SG_Name": "MANOJ KUMAR KUANR", "Father_husband_name": "NARAHARI", "villg_name": "Kuldiha", "Dist_name": "MAYURBHANJ", "LotNo": "Apr/21-18-180-05G67166", "Ref_No": "MAY/05/2020-21/R/74980", "Varity_Code": "V387", "Varity_Name": "Dharani (TCGS 1043)", "Class_code": "C01", "ProcessedArea": 25.4, "Dist_code": "21", "Farmerid": "may/134915", "Inspected_Area": 2, "Qtl_Ha": 12.7 }, { "SG_Name": "PRASANTA KU NAYAK", "Father_husband_name": "BIDYADHAR NAYAK", "villg_name": "Sarbanaghati", "Dist_name": "MAYURBHANJ", "LotNo": "Apr/21-18-180-05G67167", "Ref_No": "MAY/05/2020-21/R/74970", "Varity_Code": "V387", "Varity_Name": "Dharani (TCGS 1043)", "Class_code": "C01", "ProcessedArea": 25.4, "Dist_code": "21", "Farmerid": "MAY/20885", "Inspected_Area": 2, "Qtl_Ha": 12.7 }, { "SG_Name": "SAMIR KUMAR MOHANTY", "Father_husband_name": "BANSHIDHAR MOHANTY", "villg_name": "Jambhirapal", "Dist_name": "MAYURBHANJ", "LotNo": "Apr/21-18-180-05G67089", "Ref_No": "MAY/05/2020-21/R/74983", "Varity_Code": "V387", "Varity_Name": "Dharani (TCGS 1043)", "Class_code": "C01", "ProcessedArea": 20.8, "Dist_code": "21", "Farmerid": "MAY/3901", "Inspected_Area": 2, "Qtl_Ha": 10.4 }, { "SG_Name": "BISHNU CHANDRA PRADHAN", "Father_husband_name": "SIBAPRASAD  PRADHAN", "villg_name": "Kukurdima", "Dist_name": "MAYURBHANJ", "LotNo": "Apr/21-18-180-05G67160", "Ref_No": "MAY/05/2020-21/R/74969", "Varity_Code": "V387", "Varity_Name": "Dharani (TCGS 1043)", "Class_code": "C01", "ProcessedArea": 19, "Dist_code": "21", "Farmerid": "MAY/54706", "Inspected_Area": 1.5, "Qtl_Ha": 12.666666 }, { "SG_Name": "RAJENDRA PRASAD JENA", "Father_husband_name": "CHINTAMANI JENA", "villg_name": "Chuinpur", "Dist_name": "MAYURBHANJ", "LotNo": "May/21-18-249-05G68619", "Ref_No": "MAY/05/2020-21/R/77563", "Varity_Code": "V386", "Varity_Name": "SMARAK", "Class_code": "C01", "ProcessedArea": 2, "Dist_code": "21", "Farmerid": "may/56384", "Inspected_Area": 3, "Qtl_Ha": 0.666666 }, { "SG_Name": "Bibhutibhusana Nayak", "Father_husband_name": "Surendra Nayak", "villg_name": "Sarbanaghati", "Dist_name": "MAYURBHANJ", "LotNo": "May/21-18-180-05G67159", "Ref_No": "MAY/05/2020-21/R/74968", "Varity_Code": "V387", "Varity_Name": "Dharani (TCGS 1043)", "Class_code": "C01", "ProcessedArea": 51, "Dist_code": "21", "Farmerid": "MAY/82396", "Inspected_Area": 3, "Qtl_Ha": 17 }, { "SG_Name": "SAMIR KUMAR BARIK", "Father_husband_name": "KAILASH CH BARIK", "villg_name": "Bathudibandha", "Dist_name": "MAYURBHANJ", "LotNo": "Apr/21-18-180-05G67168", "Ref_No": "MAY/05/2020-21/R/75003", "Varity_Code": "V387", "Varity_Name": "Dharani (TCGS 1043)", "Class_code": "C01", "ProcessedArea": 49, "Dist_code": "21", "Farmerid": "May/83398", "Inspected_Area": 4, "Qtl_Ha": 12.25 }, { "SG_Name": "Kartikeswar Nayak", "Father_husband_name": "Ghanashyam Nayak", "villg_name": "Sarbanaghati", "Dist_name": "MAYURBHANJ", "LotNo": "Apr/21-18-180-05G67165", "Ref_No": "MAY/05/2020-21/R/74943", "Varity_Code": "V473", "Varity_Name": "Nitya Haritha", "Class_code": "C01", "ProcessedArea": 21, "Dist_code": "21", "Farmerid": "MAY/87234", "Inspected_Area": 2, "Qtl_Ha": 10.5 },
        // { "SG_Name": "Sanjukta Bhol", "Father_husband_name": "Ajay", "villg_name": "Budamara", "Dist_name": "MAYURBHANJ", "LotNo": "Apr/21-18-180-05G67157", "Ref_No": "MAY/05/2020-21/R/74920", "Varity_Code": "V387", "Varity_Name": "Dharani (TCGS 1043)", "Class_code": "C01", "ProcessedArea": 49, "Dist_code": "21", "Farmerid": "MAY/93495", "Inspected_Area": 4, "Qtl_Ha": 12.25 }, { "SG_Name": "DHARANIDHAR NAYAK", "Father_husband_name": "BIBHUTI BHUSHAN NAYAK", "villg_name": "Sarbanaghati", "Dist_name": "MAYURBHANJ", "LotNo": "Apr/21-18-180-05G67162", "Ref_No": "MAY/05/2020-21/R/74985", "Varity_Code": "V370", "Varity_Name": "CO-7", "Class_code": "C01", "ProcessedArea": 25.6, "Dist_code": "21", "Farmerid": "MAY/96313", "Inspected_Area": 3, "Qtl_Ha": 8.533333 }, { "SG_Name": "CHITTARANJAN NAYAK", "Father_husband_name": "GHANASHYAM NAYAK", "villg_name": "Sarbanaghati", "Dist_name": "MAYURBHANJ", "LotNo": "May/21-18-180-05G67161", "Ref_No": "MAY/05/2020-21/R/74940", "Varity_Code": "V387", "Varity_Name": "Dharani (TCGS 1043)", "Class_code": "C01", "ProcessedArea": 31, "Dist_code": "21", "Farmerid": "MAY/96320", "Inspected_Area": 2.5, "Qtl_Ha": 12.4 }, { "SG_Name": "SADANANDA PRADHAN ", "Father_husband_name": "Lingaraj Pradhan ", "villg_name": "Arisal", "Dist_name": "PURI", "LotNo": "Apr/21-18-59-02G66971", "Ref_No": "PUR/02/2020-21/R/74886", "Varity_Code": "V387", "Varity_Name": "Dharani (TCGS 1043)", "Class_code": "C01", "ProcessedArea": 74.1, "Dist_code": "26", "Farmerid": "pur/121768", "Inspected_Area": 4, "Qtl_Ha": 18.525 }, { "SG_Name": "BALAKRUSHNA PRADHAN", "Father_husband_name": "BHAGIRATHI PRADHAN", "villg_name": "Hakapada", "Dist_name": "PURI", "LotNo": "Apr/21-18-59-02G66875", "Ref_No": "PUR/02/2020-21/R/74945", "Varity_Code": "V387", "Varity_Name": "Dharani (TCGS 1043)", "Class_code": "C01", "ProcessedArea": 68.4, "Dist_code": "26", "Farmerid": "pur/125749", "Inspected_Area": 4, "Qtl_Ha": 17.1 }, { "SG_Name": "DAITARI PRADHAN", "Father_husband_name": "DIGAMBAR PRADHAN", "villg_name": "Jarakani", "Dist_name": "PURI", "LotNo": "Apr/21-18-59-02G66874", "Ref_No": "PUR/02/2020-21/R/74944", "Varity_Code": "V370", "Varity_Name": "CO-7", "Class_code": "C01", "ProcessedArea": 36.9, "Dist_code": "26", "Farmerid": "pur/37380", "Inspected_Area": 2, "Qtl_Ha": 18.45 }, { "SG_Name": "BIPIN KUMAR KHATAI", "Father_husband_name": "KAILASH CH KHATAI", "villg_name": "Bindha", "Dist_name": "PURI", "LotNo": "Apr/21-18-59-02G66970", "Ref_No": "PUR/02/2020-21/R/74870", "Varity_Code": "V387", "Varity_Name": "Dharani (TCGS 1043)", "Class_code": "C01", "ProcessedArea": 60, "Dist_code": "26", "Farmerid": "pur/58460", "Inspected_Area": 4, "Qtl_Ha": 15 }, { "SG_Name": "PRADIP KUMAR NAYAK", "Father_husband_name": "JUDHISTHIR", "villg_name": "Jagannathpur", "Dist_name": "PURI", "LotNo": "Apr/21-18-59-02G66974", "Ref_No": "PUR/02/2020-21/R/74907", "Varity_Code": "V370", "Varity_Name": "CO-7", "Class_code": "C01", "ProcessedArea": 57.2, "Dist_code": "26", "Farmerid": "PUR/77910", "Inspected_Area": 4, "Qtl_Ha": 14.3 }, { "SG_Name": "LAXMIDHAR NAYAK", "Father_husband_name": "BHRAMAR", "villg_name": "Jagannathpur", "Dist_name": "PURI", "LotNo": "Apr/21-18-59-02G66973", "Ref_No": "PUR/02/2020-21/R/74906", "Varity_Code": "V370", "Varity_Name": "CO-7", "Class_code": "C01", "ProcessedArea": 60, "Dist_code": "26", "Farmerid": "PUR/77960", "Inspected_Area": 4, "Qtl_Ha": 15 }, { "SG_Name": "ASHOK KUMAR NAYAK", "Father_husband_name": "JUDHISTHIR", "villg_name": "Jagannathpur", "Dist_name": "PURI", "LotNo": "Apr/21-18-59-02G66972", "Ref_No": "PUR/02/2020-21/R/74908", "Varity_Code": "V370", "Varity_Name": "CO-7", "Class_code": "C01", "ProcessedArea": 45, "Dist_code": "26", "Farmerid": "PUR/77961", "Inspected_Area": 4, "Qtl_Ha": 11.25 }, { "SG_Name": "KULAMANI PATEL", "Father_husband_name": "JUDHISTHIR PATEL", "villg_name": "dangajore", "Dist_name": "SAMBALPUR", "LotNo": "Mar/21-18-221-01G69053", "Ref_No": "SAM/01/2020-21/R/74688", "Varity_Code": "V406", "Varity_Name": "Pusa Mustard 28", "Class_code": "C03", "ProcessedArea": 14.28, "Dist_code": "28", "Farmerid": "SAM/56695", "Inspected_Area": 4, "Qtl_Ha": 3.57 },
        // { "SG_Name": "SABITRI PATEL", "Father_husband_name": "KULAMANI PATEL", "villg_name": "dangajore", "Dist_name": "SAMBALPUR", "LotNo": "Mar/21-18-221-01G69055", "Ref_No": "SAM/01/2020-21/R/74689", "Varity_Code": "V406", "Varity_Name": "Pusa Mustard 28", "Class_code": "C03", "ProcessedArea": 14, "Dist_code": "28", "Farmerid": "SAM/56703", "Inspected_Area": 4, "Qtl_Ha": 3.5 }, { "SG_Name": "DIBYALOCHAN PATEL", "Father_husband_name": "PREMANANDA PATEL", "villg_name": "GIRINGKELA", "Dist_name": "SUNDERGARH", "LotNo": "Jun/21-18-177-01G67363", "Ref_No": "SUN/01/2020-21/R/74828", "Varity_Code": "V387", "Varity_Name": "Dharani (TCGS 1043)", "Class_code": "C01", "ProcessedArea": 11.7, "Dist_code": "30", "Farmerid": "SUN/10650", "Inspected_Area": 1, "Qtl_Ha": 11.7 }, { "SG_Name": "SANTOSH PRUSETH", "Father_husband_name": "MANBODH PRUSETH", "villg_name": "GIRINGKELA", "Dist_name": "SUNDERGARH", "LotNo": "JUN/21-18-177-01G67374", "Ref_No": "SUN/01/2020-21/R/74833", "Varity_Code": "V387", "Varity_Name": "Dharani (TCGS 1043)", "Class_code": "C01", "ProcessedArea": 6.9, "Dist_code": "30", "Farmerid": "SUN/11937", "Inspected_Area": 1, "Qtl_Ha": 6.9 }, { "SG_Name": "NILAMANI MAGAR", "Father_husband_name": "KHAGESWAR MAGAR", "villg_name": "GIRINGKELA", "Dist_name": "SUNDERGARH", "LotNo": "Jun/21-18-177-01G67365", "Ref_No": "SUN/01/2020-21/R/74817", "Varity_Code": "V387", "Varity_Name": "Dharani (TCGS 1043)", "Class_code": "C01", "ProcessedArea": 13.2, "Dist_code": "30", "Farmerid": "SUN/11946", "Inspected_Area": 0.8, "Qtl_Ha": 16.5 }, { "SG_Name": "TULARAM NAIK", "Father_husband_name": "PARAMANANDA NAIK", "villg_name": "GIRINGKELA", "Dist_name": "SUNDERGARH", "LotNo": "Jun/21-18-177-01G67347", "Ref_No": "SUN/01/2020-21/R/74825", "Varity_Code": "V387", "Varity_Name": "Dharani (TCGS 1043)", "Class_code": "C01", "ProcessedArea": 10.5, "Dist_code": "30", "Farmerid": "SUN/30845", "Inspected_Area": 0.6, "Qtl_Ha": 17.5 }, { "SG_Name": "DESHRANJAN PATEL", "Father_husband_name": "RUDRA NARAYAN PATEL", "villg_name": "GIRINGKELA", "Dist_name": "SUNDERGARH", "LotNo": "Jun/21-18-177-01G67314", "Ref_No": "SUN/01/2020-21/R/74819", "Varity_Code": "V387", "Varity_Name": "Dharani (TCGS 1043)", "Class_code": "C01", "ProcessedArea": 17.1, "Dist_code": "30", "Farmerid": "SUN/35876", "Inspected_Area": 1, "Qtl_Ha": 17.1 }, { "SG_Name": "TANKADHAR PATEL", "Father_husband_name": "BHAGABANA PATEL", "villg_name": "GIRINGKELA", "Dist_name": "SUNDERGARH", "LotNo": "Jun/21-18-177-01G67346", "Ref_No": "SUN/01/2020-21/R/74830", "Varity_Code": "V387", "Varity_Name": "Dharani (TCGS 1043)", "Class_code": "C01", "ProcessedArea": 15, "Dist_code": "30", "Farmerid": "SUN/37244", "Inspected_Area": 1, "Qtl_Ha": 15 }, { "SG_Name": "BENU DHAR BEHERA", "Father_husband_name": "NAROTTAMA BEHERA", "villg_name": "GIRINGKELA", "Dist_name": "SUNDERGARH", "LotNo": "Jun/21-18-177-01G67359", "Ref_No": "SUN/01/2020-21/R/74824", "Varity_Code": "V387", "Varity_Name": "Dharani (TCGS 1043)", "Class_code": "C01", "ProcessedArea": 17.1, "Dist_code": "30", "Farmerid": "SUN/46314", "Inspected_Area": 1, "Qtl_Ha": 17.1 }, { "SG_Name": "SANKAR SHAN PATEL", "Father_husband_name": "NILA MBER PATEL", "villg_name": "GIRINGKELA", "Dist_name": "SUNDERGARH", "LotNo": "Jun/21-18-177-01G67360", "Ref_No": "SUN/01/2020-21/R/74834", "Varity_Code": "V387", "Varity_Name": "Dharani (TCGS 1043)", "Class_code": "C01", "ProcessedArea": 14.1, "Dist_code": "30", "Farmerid": "SUN/48546", "Inspected_Area": 0.8, "Qtl_Ha": 17.625 }, { "SG_Name": "RAJESH SORENGE", "Father_husband_name": "RATI SORENG", "villg_name": "GIRINGKELA", "Dist_name": "SUNDERGARH", "LotNo": "Jun/21-18-177-01G67313", "Ref_No": "SUN/01/2020-21/R/74823", "Varity_Code": "V387", "Varity_Name": "Dharani (TCGS 1043)", "Class_code": "C01", "ProcessedArea": 25.5, "Dist_code": "30", "Farmerid": "SUN/52979", "Inspected_Area": 1.4, "Qtl_Ha": 18.214285 }, { "SG_Name": "TAYONIDHI PANDEY", "Father_husband_name": "ANANDRAM PANDEY", "villg_name": "GIRINGKELA", "Dist_name": "SUNDERGARH", "LotNo": "Jun/21-18-177-01G67348", "Ref_No": "SUN/01/2020-21/R/74822", "Varity_Code": "V387", "Varity_Name": "Dharani (TCGS 1043)", "Class_code": "C01", "ProcessedArea": 24.6, "Dist_code": "30", "Farmerid": "SUN/53069", "Inspected_Area": 1.8, "Qtl_Ha": 13.666666 },
        // { "SG_Name": "KISHOR GURLIA", "Father_husband_name": "MANGAL GURLIA", "villg_name": "GIRINGKELA", "Dist_name": "SUNDERGARH", "LotNo": "Jun/21-18-177-01G67364", "Ref_No": "SUN/01/2020-21/R/74829", "Varity_Code": "V387", "Varity_Name": "Dharani (TCGS 1043)", "Class_code": "C01", "ProcessedArea": 17.1, "Dist_code": "30", "Farmerid": "SUN/53751", "Inspected_Area": 1, "Qtl_Ha": 17.1 }, { "SG_Name": "RABIRATNA  PATEL", "Father_husband_name": "DUBRAJ PATEL", "villg_name": "GIRINGKELA", "Dist_name": "SUNDERGARH", "LotNo": "Jun/21-18-177-01G67376", "Ref_No": "SUN/01/2020-21/R/74831", "Varity_Code": "V387", "Varity_Name": "Dharani (TCGS 1043)", "Class_code": "C01", "ProcessedArea": 30.6, "Dist_code": "30", "Farmerid": "SUN/53753", "Inspected_Area": 1.8, "Qtl_Ha": 17 }, { "SG_Name": "ABHIN CHANDRA PANDEY", "Father_husband_name": "RUKMAN PABDEY", "villg_name": "GIRINGKELA", "Dist_name": "SUNDERGARH", "LotNo": "Jun/21-18-177-01G67373", "Ref_No": "SUN/01/2020-21/R/74821", "Varity_Code": "V387", "Varity_Name": "Dharani (TCGS 1043)", "Class_code": "C01", "ProcessedArea": 9.6, "Dist_code": "30", "Farmerid": "SUN/53757", "Inspected_Area": 1, "Qtl_Ha": 9.6 }, { "SG_Name": "MADAN BEHERA", "Father_husband_name": "MANGALU BEHERA", "villg_name": "GIRINGKELA", "Dist_name": "SUNDERGARH", "LotNo": "Jun/21-18-177-01G67366", "Ref_No": "SUN/01/2020-21/R/74815", "Varity_Code": "V387", "Varity_Name": "Dharani (TCGS 1043)", "Class_code": "C01", "ProcessedArea": 17.1, "Dist_code": "30", "Farmerid": "SUN/53764", "Inspected_Area": 1, "Qtl_Ha": 17.1 }, { "SG_Name": "LABA BARIHA", "Father_husband_name": "KUNDI BARIHA", "villg_name": "GIRINGKELA", "Dist_name": "SUNDERGARH", "LotNo": "Jun/21-18-177-01G67372", "Ref_No": "SUN/01/2020-21/R/74818", "Varity_Code": "V387", "Varity_Name": "Dharani (TCGS 1043)", "Class_code": "C01", "ProcessedArea": 11.4, "Dist_code": "30", "Farmerid": "SUN/53772", "Inspected_Area": 1, "Qtl_Ha": 11.4 }, { "SG_Name": "NIBAS PATEL", "Father_husband_name": "DHARMARAJ PATEL", "villg_name": "GIRINGKELA", "Dist_name": "SUNDERGARH", "LotNo": "Jun/21-18-177-01G67371", "Ref_No": "SUN/01/2020-21/R/74820", "Varity_Code": "V387", "Varity_Name": "Dharani (TCGS 1043)", "Class_code": "C01", "ProcessedArea": 14.1, "Dist_code": "30", "Farmerid": "SUN/53773", "Inspected_Area": 1, "Qtl_Ha": 14.1 }, { "SG_Name": "DURGA MADHAB PATEL", "Father_husband_name": "KARTIKA PATEL", "villg_name": "GIRINGKELA", "Dist_name": "SUNDERGARH", "LotNo": "Jun/21-18-177-01G67369", "Ref_No": "SUN/01/2020-21/R/74832", "Varity_Code": "V387", "Varity_Name": "Dharani (TCGS 1043)", "Class_code": "C01", "ProcessedArea": 12, "Dist_code": "30", "Farmerid": "SUN/53970", "Inspected_Area": 1, "Qtl_Ha": 12 }, { "SG_Name": "LAXMI DHURUA", "Father_husband_name": "ANAMA DHURUA", "villg_name": "GIRINGKELA", "Dist_name": "SUNDERGARH", "LotNo": "Jun/21-18-177-01G67358", "Ref_No": "SUN/01/2020-21/R/74816", "Varity_Code": "V387", "Varity_Name": "Dharani (TCGS 1043)", "Class_code": "C01", "ProcessedArea": 16.2, "Dist_code": "30", "Farmerid": "SUN/67140", "Inspected_Area": 1, "Qtl_Ha": 16.2 }, { "SG_Name": "DULAMANI KERKETTA", "Father_husband_name": "ETA KERKETTA", "villg_name": "GIRINGKELA", "Dist_name": "SUNDERGARH", "LotNo": "Jun/21-18-177-01G67357", "Ref_No": "SUN/01/2020-21/R/74827", "Varity_Code": "V387", "Varity_Name": "Dharani (TCGS 1043)", "Class_code": "C01", "ProcessedArea": 18.9, "Dist_code": "30", "Farmerid": "SUN/96206", "Inspected_Area": 1, "Qtl_Ha": 18.9 }, { "SG_Name": "TANKADHAR PATEL", "Father_husband_name": "BUDDHADEV PATEL", "villg_name": "GIRINGKELA", "Dist_name": "SUNDERGARH", "LotNo": "JUN/21-18-177-01G67375", "Ref_No": "SUN/01/2020-21/R/74826", "Varity_Code": "V387", "Varity_Name": "Dharani (TCGS 1043)", "Class_code": "C01", "ProcessedArea": 9, "Dist_code": "30", "Farmerid": "SUN/97243", "Inspected_Area": 0.8, "Qtl_Ha": 11.25 }]
        resolve(totalprebookingdtl);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        con.release();
    }

});
exports.InsertIncentiveOilSeed = (data) => new Promise(async (resolve, reject) => {
    var con = new sqlstock.ConnectionPool(locConfigstock);
    try {
        var SCHEME_CODE_GOI = 9145;
        var SCHEME_CODE_SP = 9145;
        var CROP_CODE = '';
        for (let i = 0; i < data.length; i++) {
            CROP_CODE = await sequelizeStock.query(`SELECT TOP(1)Crop_Code FROM mCropVariety WHERE Variety_Code = :Varity_Code`, {
                replacements: { Varity_Code: data[i].Varity_Code }, type: sequelizeStock.QueryTypes.SELECT
            });
            var Category_Code = await sequelizeStock.query(`SELECT TOP(1)Category_Code FROM mCrop WHERE CROP_CODE =:CROP_CODE`, {
                replacements: { CROP_CODE: CROP_CODE[0].Crop_Code }, type: sequelizeStock.QueryTypes.SELECT
            });
            var SUBSIDISED_QTY = 0;
            var SUBSIDISED_AREA = 0;
            var PREV_SUBSIDY_QTY = await sequelizeStock.query(`SELECT ISNULL(SUM(SUBSIDISED_QTY),0) as PREV_SUBSIDY_QTY FROM mINCENTIVE2 WHERE FIN_YR = :selectedFinancialYear AND FARMER_ID = :FARMER_ID`, {
                replacements: { selectedFinancialYear: data[0].selectedFinancialYear, FARMER_ID: data[i].Farmerid }, type: sequelizeStock.QueryTypes.SELECT
            });
            var PREV_SUBSIDISED_AREA = await sequelizeStock.query(`SELECT ISNULL(SUM(SUBSIDISED_AREA),0) as PREV_SUBSIDISED_AREA FROM mINCENTIVE2 WHERE FIN_YR = :selectedFinancialYear AND FARMER_ID = :FARMER_ID`, {
                replacements: { selectedFinancialYear: data[0].selectedFinancialYear, FARMER_ID: data[i].Farmerid }, type: sequelizeStock.QueryTypes.SELECT
            });
            if (PREV_SUBSIDISED_AREA[0].PREV_SUBSIDISED_AREA < 5) {
                if ((PREV_SUBSIDISED_AREA + data[i].INSPECTED_AREA) <= 5) {
                    SUBSIDISED_AREA = data[i].INSPECTED_AREA;
                }
                else {
                    SUBSIDISED_AREA = (5 - PREV_SUBSIDISED_AREA[0].PREV_SUBSIDISED_AREA);
                }
                if (CROP_CODE[0].CROP_CODE = 'C026') {
                    if ((SUBSIDISED_AREA * 20) <= data[i].ProcessedArea) {
                        SUBSIDISED_QTY = (SUBSIDISED_AREA * 20);
                    }
                    else {
                        SUBSIDISED_QTY = data[i].ProcessedArea;
                    }
                    if (data[i].Qtl_Ha < 20)
                        SUBSIDISED_QTY = (SUBSIDISED_AREA * data[i].Qtl_Ha);
                }
                if (CROP_CODE[0].CROP_CODE = 'C027') {
                    if ((SUBSIDISED_AREA * 10) <= data[i].ProcessedArea) {
                        SUBSIDISED_QTY = (SUBSIDISED_AREA * 10);
                    }
                    else {
                        SUBSIDISED_QTY = data[i].ProcessedArea;
                    }
                    if (data[i].Qtl_Ha < 10) {
                        SUBSIDISED_QTY = (SUBSIDISED_AREA * data[i].Qtl_Ha);
                    }
                }

                if (CROP_CODE[0].CROP_CODE = 'C029') {
                    if ((SUBSIDISED_AREA * 5) <= data[i].ProcessedArea) {
                        SUBSIDISED_QTY = (SUBSIDISED_AREA * 5);
                    }
                    else {
                        SUBSIDISED_QTY = data[i].ProcessedArea;
                    }
                    if (data[i].Qtl_Ha < 5) {
                        SUBSIDISED_QTY = (SUBSIDISED_AREA * data[i].Qtl_Ha);
                    }
                }


                if (CROP_CODE[0].CROP_CODE = 'C033') {
                    if ((SUBSIDISED_AREA * 6) <= data[i].ProcessedArea) {
                        SUBSIDISED_QTY = (SUBSIDISED_AREA * 6);
                    }
                    else {
                        SUBSIDISED_QTY = data[i].ProcessedArea;
                    }
                    if (data[i].Qtl_Ha < 6) {
                        SUBSIDISED_QTY = (SUBSIDISED_AREA * data[i].Qtl_Ha);
                    }
                }

                if (CROP_CODE[0].CROP_CODE = 'C028') {
                    if ((SUBSIDISED_AREA * 6) <= data[i].ProcessedArea) {
                        SUBSIDISED_QTY = (SUBSIDISED_AREA * 6);
                    }
                    else {
                        SUBSIDISED_QTY = data[i].ProcessedArea;
                    }
                    if (data[i].Qtl_Ha < 6) {
                        SUBSIDISED_QTY = (SUBSIDISED_AREA * data[i].Qtl_Ha);
                    }
                }


                if (CROP_CODE[0].CROP_CODE = 'C036') {
                    if ((SUBSIDISED_AREA * 10) <= data[i].ProcessedArea) {
                        SUBSIDISED_QTY = (SUBSIDISED_AREA * 10);
                    }
                    else {
                        SUBSIDISED_QTY = data[i].ProcessedArea;
                    }
                    if (data[i].Qtl_Ha < 10) {
                        SUBSIDISED_QTY = (SUBSIDISED_AREA * data[i].Qtl_Ha);
                    }
                }
            }
            var MAXTRAN_NO = await sequelizeStock.query(`SELECT ISNULL(MAX(convert(INT,SUBSTRING(TRANSACTION_ID,11,5))),0)+1 as MAXTRAN_NO  FROM mINCENTIVE2 WHERE SUBSTRING(TRANSACTION_ID,1,7) =:selectedFinancialYear`, {
                replacements: { selectedFinancialYear: data[0].selectedFinancialYear }, type: sequelizeStock.QueryTypes.SELECT
            });
            //    (SELECT ISNULL(MAX(CONVERT(INT,SUBSTRING(TRANSACTION_ID,11,5))),0)+1 FROM mINCENTIVE2 WHERE SUBSTRING(TRANSACTION_ID,1,7) = @FIN_YR)  
            var TRANSACTION_ID = data[0].selectedFinancialYear + '/S/' + MAXTRAN_NO[0].MAXTRAN_NO;
            var SEASON = await sequelizeStock.query(`SELECT substring(:Ref_No,16,1) as SEASON `, {
                replacements: { Ref_No: data[i].Ref_No }, type: sequelizeStock.QueryTypes.SELECT
            });
            // if (SEASON[0].SEASON == 'K') {
            //     SEASON[0].SEASON = 'Kharif';
            // }
            // else {
            //     SEASON[0].SEASON = 'Rabi';
            // }
            var result = await sequelizeStock.query(` INSERT INTO mINCENTIVE2 (TRANSACTION_ID,FARMER_ID, FARMER_NAME, FATHER_HUSBAND_NAME, Category_Code, Crop_Code, VARIETY_CODE, VARIETY_NAME,DIST_CODE, DIST_NAME, REF_NO, LOT_NO, CLASS_CODE, VILLG_NAME, PROCESSED_QTY, SUBSIDISED_QTY, INSPECTED_AREA, SUBSIDISED_AREA, GOI_SUBSIDY_CODE, GOI_SUBSIDY, SP_SUBSIDY_CODE, SP_SUBSIDY, FARMER_SUBSIDY, OSSC_SUBSIDY, FIN_YR, SEASON, UPDATED_ON,QTL_HA)   
            VALUES    (:TRANSACTION_ID, :FARMER_ID, :FARMER_NAME, :FATHER_HUSBAND_NAME, :Category_Code, :Crop_Code, :VARIETY_CODE,
                 :VARIETY_NAME, :DIST_CODE, :DIST_NAME, :REF_NO, :LOT_NO, :CLASS_CODE, :VILLG_NAME, :PROCESSED_QTY, :SUBSIDISED_QTY, :INSPECTED_AREA,
                 :SUBSIDISED_AREA, :SCHEME_CODE_GOI, (:SUBSIDISED_QTY * 1875), :SCHEME_CODE_SP, (:SUBSIDISED_QTY * 625), (:SUBSIDISED_QTY * 1875),
                  (:SUBSIDISED_QTY * 625),:FIN_YR,:SEASON,getdate(),:Qtl_Ha) select @@rowcount `, {
                replacements: {
                    TRANSACTION_ID: TRANSACTION_ID, FARMER_ID: data[i].Farmerid, FARMER_NAME: data[i].SG_Name, FATHER_HUSBAND_NAME: data[i].Father_husband_name, Category_Code: Category_Code[0].Category_Code, Crop_Code: CROP_CODE[0].Crop_Code, VARIETY_CODE: data[i].Varity_Code,
                    VARIETY_NAME: data[i].Varity_Name, DIST_CODE: data[i].Dist_code, DIST_NAME: data[i].Dist_name, REF_NO: data[i].Ref_No, LOT_NO: data[i].LotNo, CLASS_CODE: data[i].Class_code, VILLG_NAME: data[i].villg_name, PROCESSED_QTY: data[i].ProcessedArea, SUBSIDISED_QTY: SUBSIDISED_QTY, INSPECTED_AREA: data[i].Inspected_Area,
                    SUBSIDISED_AREA: SUBSIDISED_AREA, SCHEME_CODE_GOI: SCHEME_CODE_GOI, SCHEME_CODE_SP: SCHEME_CODE_SP, FIN_YR: data[0].selectedFinancialYear, SEASON: SEASON[0].SEASON, Qtl_Ha: data[i].Qtl_Ha
                }, type: sequelizeStock.QueryTypes.SELECT
            });




        }
        let response = await sequelizeStock.query(` UPDATE A SET A.ACC_HOLDERNAME = B.VCHACCHOLDERNM,A.ACC_NO = B.VCHACCOUNTNO,A.BANK_ID = B.INTBANKID, A.BRANCH_ID = B.INTBRANCHID, A.IFSC_CODE = B.VCHIFSCCODE FROM mINCENTIVE2 A   
        INNER JOIN [FARMERDB].[DBO].[M_FARMER_REGISTRATION] B ON A.FARMER_ID = B.NICFARMERID COLLATE Latin1_General_CI_AI  
        WHERE A.ACC_NO IS NULL`);
        resolve(true);
        // con.connect().then(function success() {
        //     const request = new sql.Request(con);
        //     //data.selectedFinancialYear
        //     request.input('FIN_YR', data[0].selectedFinancialYear);
        //     request.input('VALUES', data);
        //     request.output('VAL');
        //     request.execute('Stock_SP_IncentiveOilSeed', function (err, result) {
        //         if (err) {
        //             console.log('An error occurred...', err);
        //         }
        //         else {
        //             console.log(result.recordsets,'result');
        //             // callback(result.recordset);
        //             resolve(result.output)
        //         }
        //         con.close();
        //     });
        // }).catch(function error(err) {
        //     console.log('An error occurred...', err);
        // });
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        con.release();
    }
});
exports.FillIncentiveOilSeed = (data) => new Promise(async (resolve, reject) => {
    var con = new sqlstock.ConnectionPool(locConfigstock);
    try {
        con.connect().then(function success() {
            const request = new sql.Request(con);
            request.input('FIN_YR', data.selectedFinancialYear);
            request.input('SEASON', data.selectedSeason);
            request.execute('FillIncentiveOilSeed', function (err, result) {
                if (err) {
                    console.log('An error occurred...', err);
                }
                else {
                    // callback(result.recordset);
                    resolve(result.recordset)
                }
                con.close();
            });
        }).catch(function error(err) {
            console.log('An error occurred...', err);
        });
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        con.release();
    }

});
exports.FillPendingIncentiveOilSeed = (data) => new Promise(async (resolve, reject) => {
    var con = new sqlstock.ConnectionPool(locConfigstock);
    try {
        const totalprebookingdtl = await sequelizeStock.query(`SELECT COUNT(*) as count FROM mINCENTIVE2 WHERE FIN_YR = :FIN_YR AND OSSC_STATUS = 'Pending'`, {
            replacements: { FIN_YR: data.selectedFinancialYear }, type: sequelizeStock.QueryTypes.SELECT
        });
        resolve(totalprebookingdtl);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        con.release();
    }

});
exports.OsscAprvOilSeed = (data) => new Promise(async (resolve, reject) => {
    var con = new sqlstock.ConnectionPool(locConfigstock);
    try {
        const totalprebookingdtl = await sequelizeStock.query(`UPDATE mINCENTIVE2 SET OSSC_STATUS = 'Approved', OSSC_STATUS_UPDATEDBY = :OSSC_STATUS_UPDATEDBY, OSSC_STATUS_UPDATEDON = GETDATE() WHERE FIN_YR = :FIN_YR AND OSSC_STATUS = 'Pending'`, {
            replacements: { FIN_YR: data.FIN_YR, OSSC_STATUS_UPDATEDBY: data.UPDATED_BY }, type: sequelizeStock.QueryTypes.SELECT
        });
        resolve(totalprebookingdtl);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        con.release();
    }

});
// };