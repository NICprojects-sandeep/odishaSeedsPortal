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
    console.log(data);
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
                    console.log(result);
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
    try {
        const totalprebookingdtl = await sequelizeOssopoca.query(`SELECT SG_Name,Father_husband_name,villg_name,Dist_name,LotNo,Ref_No,Varity_Code,Varity_Name,Class_code,ProcessedArea,Dist_code,Farmerid,Inspected_Area,(ProcessedArea/Inspected_Area)Qtl_Ha FROM dbo.VwIncentiveOilseed WHERE Farmerid IS NOT NULL and crop_code in ('C026','C027','C033','C029')  ORDER BY Farmerid,(ProcessedArea/Inspected_Area)`, {
            replacements: {}, type: sequelizeOssopoca.QueryTypes.SELECT
        });
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
        con.connect().then(function success() {
            const request = new sql.Request(con);
            request.input('FIN_YR', data.selectedFinancialYear);
            request.input('VALUES', data);
            request.output('VAL');
            request.execute('Stock_SP_IncentiveOilSeed', function (err, result) {
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
exports.FillPendingIncentiveOilSeed = (data) => new Promise(async (resolve, reject) => {
    var con = new sqlstock.ConnectionPool(locConfigStock);
    try {
        const totalprebookingdtl = await sequelizeStock.query(`SELECT COUNT(*) FROM mINCENTIVE2 WHERE FIN_YR = :FIN_YR AND OSSC_STATUS = 'Pending'`, {
            replacements: {FIN_YR: data.selectedFinancialYear}, type: sequelizeStock.QueryTypes.SELECT
        });
        resolve(totalprebookingdtl);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        con.release();
    }

});
exports.FillPendingIncentiveOilSeed = (data) => new Promise(async (resolve, reject) => {
    var con = new sqlstock.ConnectionPool(locConfigStock);
    try {
        const totalprebookingdtl = await sequelizeStock.query(`UPDATE mINCENTIVE2 SET OSSC_STATUS = 'Approved', OSSC_STATUS_UPDATEDBY = :OSSC_STATUS_UPDATEDBY, OSSC_STATUS_UPDATEDON = GETDATE() WHERE FIN_YR = :FIN_YR AND OSSC_STATUS = 'Pending'`, {
            replacements: {FIN_YR: data.selectedFinancialYear,OSSC_STATUS_UPDATEDBY:data.OSSC_STATUS_UPDATEDBY}, type: sequelizeStock.QueryTypes.SELECT
        });
        resolve(totalprebookingdtl);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        con.release();
    }

});
// };