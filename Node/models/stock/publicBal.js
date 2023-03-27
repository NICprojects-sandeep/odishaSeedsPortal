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
        // const totalprebookingdtl = await sequelizeOssopoca.query(`SELECT SG_Name,Father_husband_name,villg_name,Dist_name,LotNo,Ref_No,Varity_Code,Varity_Name,Class_code,ProcessedArea,Dist_code,Farmerid,Inspected_Area,(ProcessedArea/Inspected_Area)Qtl_Ha FROM dbo.VwIncentiveOilseed WHERE Farmerid IS NOT NULL and crop_code in ('C026','C027','C033','C029')  ORDER BY Farmerid,(ProcessedArea/Inspected_Area)`, {
        //     replacements: {}, type: sequelizeOssopoca.QueryTypes.SELECT
        // });
        const totalprebookingdtl = [
    {"SG_Name":"GADADHAR JENA","Father_husband_name":"BIRABHADRA JENA","villg_name":"Akandi","Dist_name":"BALASORE","LotNo":"Mar/22-18-315-08G77823","Ref_No":"BAL/08/2021-22/R/85157","Varity_Code":"V406","Varity_Name":"Pusa Mustard 28","Class_code":"C03","ProcessedArea":15,"Dist_code":"02","Farmerid":"BAL/104172","Inspected_Area":2,"Qtl_Ha":7.5},{"SG_Name":"UMAKANTA DAS","Father_husband_name":"RADHAKRUSHNA DAS","villg_name":"Sonpur","Dist_name":"BALASORE","LotNo":"May/22-18-315-08G78569","Ref_No":"BAL/08/2021-22/R/87391","Varity_Code":"V386","Varity_Name":"SMARAK","Class_code":"C03","ProcessedArea":11,"Dist_code":"02","Farmerid":"BAL/106135","Inspected_Area":2,"Qtl_Ha":5.5},{"SG_Name":"KARTTICK CHANDRA GIRI","Father_husband_name":"BANAMALI GIRI","villg_name":"Karua","Dist_name":"BALASORE","LotNo":"May/22-18-315-08G78571","Ref_No":"BAL/08/2021-22/R/87389","Varity_Code":"V386","Varity_Name":"SMARAK","Class_code":"C03","ProcessedArea":8,"Dist_code":"02","Farmerid":"BAL/126379","Inspected_Area":2,"Qtl_Ha":4},{"SG_Name":"ARJUN CHARAN SAHOO","Father_husband_name":"NARENDRA","villg_name":"Jamsuli(Balisahi)","Dist_name":"BALASORE","LotNo":"May/22-18-315-08G78573","Ref_No":"BAL/08/2021-22/R/87388","Varity_Code":"V386","Varity_Name":"SMARAK","Class_code":"C03","ProcessedArea":8,"Dist_code":"02","Farmerid":"BAL/129779","Inspected_Area":2,"Qtl_Ha":4},{"SG_Name":"ASHIS KUMAR MOHANTY","Father_husband_name":"NITYANANDA MOHANTY","villg_name":"Santoshpur","Dist_name":"BALASORE","LotNo":"May/22-18-315-08G78566","Ref_No":"BAL/08/2021-22/R/87386","Varity_Code":"V386","Varity_Name":"SMARAK","Class_code":"C03","ProcessedArea":4.2,"Dist_code":"02","Farmerid":"BAL/132222","Inspected_Area":2,"Qtl_Ha":2.1},{"SG_Name":"DIBYARANJAN GIRI","Father_husband_name":"SURENDRA NATH GIRI","villg_name":"Patrapada","Dist_name":"BALASORE","LotNo":"May/22-18-315-08G78698","Ref_No":"BAL/08/2021-22/R/87905","Varity_Code":"V386","Varity_Name":"SMARAK","Class_code":"C03","ProcessedArea":5.8,"Dist_code":"02","Farmerid":"BAL/136241","Inspected_Area":2,"Qtl_Ha":2.9},{"SG_Name":"PADMA LOCHAN SAHU","Father_husband_name":"BIDYADHAR SAHU","villg_name":"Gilajodi","Dist_name":"BALASORE","LotNo":"May/22-18-315-08G78565","Ref_No":"BAL/08/2021-22/R/87382","Varity_Code":"V386","Varity_Name":"SMARAK","Class_code":"C03","ProcessedArea":10,"Dist_code":"02","Farmerid":"BAL/138993","Inspected_Area":2,"Qtl_Ha":5},{"SG_Name":"Nara hari Dey","Father_husband_name":"Baburam Dey","villg_name":"Srirampur","Dist_name":"BALASORE","LotNo":"Mar/22-18-315-08G77386","Ref_No":"BAL/08/2021-22/R/85183","Varity_Code":"V406","Varity_Name":"Pusa Mustard 28","Class_code":"C03","ProcessedArea":17,"Dist_code":"02","Farmerid":"BAL/145999","Inspected_Area":2,"Qtl_Ha":8.5},{"SG_Name":"Ajit Kumar Dey","Father_husband_name":"Santosh","villg_name":"Srirampur","Dist_name":"BALASORE","LotNo":"Mar/22-18-315-08G77379","Ref_No":"BAL/08/2021-22/R/85163","Varity_Code":"V406","Varity_Name":"Pusa Mustard 28","Class_code":"C03","ProcessedArea":17,"Dist_code":"02","Farmerid":"BAL/146657","Inspected_Area":2,"Qtl_Ha":8.5},{"SG_Name":"Karttik  Chandra  Nayak","Father_husband_name":"Purnachandra  Nayak","villg_name":"Chhachina","Dist_name":"BALASORE","LotNo":"Feb/22-18-315-08G77591","Ref_No":"BAL/08/2021-22/R/85161","Varity_Code":"V406","Varity_Name":"Pusa Mustard 28","Class_code":"C03","ProcessedArea":17.6,"Dist_code":"02","Farmerid":"BAL/146940","Inspected_Area":3,"Qtl_Ha":5.866666},{"SG_Name":"Babita Dey","Father_husband_name":"Bansidhar Dey","villg_name":"Nepura","Dist_name":"BALASORE","LotNo":"Mar/22-18-315-08G77381","Ref_No":"BAL/08/2021-22/R/85164","Varity_Code":"V406","Varity_Name":"Pusa Mustard 28","Class_code":"C03","ProcessedArea":17,"Dist_code":"02","Farmerid":"BAL/146979","Inspected_Area":2,"Qtl_Ha":8.5},{"SG_Name":"Sumati Dey","Father_husband_name":"Santosh Dey","villg_name":"Srirampur","Dist_name":"BALASORE","LotNo":"Mar/22-18-315-08G77388","Ref_No":"BAL/08/2021-22/R/85165","Varity_Code":"V406","Varity_Name":"Pusa Mustard 28","Class_code":"C03","ProcessedArea":17,"Dist_code":"02","Farmerid":"BAL/146980","Inspected_Area":2,"Qtl_Ha":8.5},{"SG_Name":"Mr SUKUMAR JENA","Father_husband_name":"ARUN KU ","villg_name":"Guhalipada","Dist_name":"BALASORE","LotNo":"May/22-18-315-08G78574","Ref_No":"BAL/08/2021-22/R/87387","Varity_Code":"V386","Varity_Name":"SMARAK","Class_code":"C03","ProcessedArea":9,"Dist_code":"02","Farmerid":"BAL/155292","Inspected_Area":2,"Qtl_Ha":4.5},
     {"SG_Name":"Mr BHAGIRATH PATRA","Father_husband_name":"PURNA CHANDRA PATRA","villg_name":"Ramchak","Dist_name":"BALASORE","LotNo":"Mar/22-18-315-08G77157","Ref_No":"BAL/08/2021-22/R/85154","Varity_Code":"V406","Varity_Name":"Pusa Mustard 28","Class_code":"C03","ProcessedArea":7,"Dist_code":"02","Farmerid":"BAL/155684","Inspected_Area":2,"Qtl_Ha":3.5},{"SG_Name":"RADHA GOBINDA MOHANTY","Father_husband_name":"RAGHUNATH MOHANTY","villg_name":"JADIBALI","Dist_name":"BALASORE","LotNo":"May/22-18-175-08G78643","Ref_No":"BAL/08/2021-22/R/85399","Varity_Code":"V370","Varity_Name":"CO-7","Class_code":"C01","ProcessedArea":26.8,"Dist_code":"02","Farmerid":"BAL/16088","Inspected_Area":2,"Qtl_Ha":13.4},{"SG_Name":"jashaswee Sahoo","Father_husband_name":"","villg_name":"Sartha","Dist_name":"BALASORE","LotNo":"May/22-18-315-08G78567","Ref_No":"BAL/08/2021-22/R/87384","Varity_Code":"V386","Varity_Name":"SMARAK","Class_code":"C03","ProcessedArea":10.8,"Dist_code":"02","Farmerid":"BAL/161504","Inspected_Area":2,"Qtl_Ha":5.4},{"SG_Name":"Bharati dey","Father_husband_name":"Chimnmay dey","villg_name":"Srirampur","Dist_name":"BALASORE","LotNo":"Mar/22-18-315-08G77383","Ref_No":"BAL/08/2021-22/R/85186","Varity_Code":"V406","Varity_Name":"Pusa Mustard 28","Class_code":"C03","ProcessedArea":17,"Dist_code":"02","Farmerid":"BAL/169185","Inspected_Area":2,"Qtl_Ha":8.5},{"SG_Name":"MRS MAMINA SAHOO","Father_husband_name":"RAGHUNATH SAHOO","villg_name":"Gilajodi","Dist_name":"BALASORE","LotNo":"May/22-18-315-08G78697","Ref_No":"BAL/08/2021-22/R/87898","Varity_Code":"V386","Varity_Name":"SMARAK","Class_code":"C03","ProcessedArea":14,"Dist_code":"02","Farmerid":"BAL/171262","Inspected_Area":3,"Qtl_Ha":4.666666},{"SG_Name":"DIPAK KUMAR DAS","Father_husband_name":"SUMANTA KUMAR DAS","villg_name":"Sitadiha","Dist_name":"BALASORE","LotNo":"May/22-18-175-08G78663","Ref_No":"BAL/08/2021-22/R/85511","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C01","ProcessedArea":12.8,"Dist_code":"02","Farmerid":"BAL/204526","Inspected_Area":2,"Qtl_Ha":6.4},{"SG_Name":"Mr GOPINATH SAHOO","Father_husband_name":"SHYAMA SAHOO","villg_name":"Baliapal","Dist_name":"BALASORE","LotNo":"May/22-18-315-08G78572","Ref_No":"BAL/08/2021-22/R/87390","Varity_Code":"V386","Varity_Name":"SMARAK","Class_code":"C03","ProcessedArea":8,"Dist_code":"02","Farmerid":"BAL/206120","Inspected_Area":2,"Qtl_Ha":4},{"SG_Name":"GIRISH CHANDRA MOHANTY","Father_husband_name":"GANANATH MOHANTY","villg_name":"JADIBALI","Dist_name":"BALASORE","LotNo":"May/22-18-175-08G78642","Ref_No":"BAL/08/2021-22/R/85398","Varity_Code":"V370","Varity_Name":"CO-7","Class_code":"C01","ProcessedArea":26,"Dist_code":"02","Farmerid":"BAL/21065","Inspected_Area":2,"Qtl_Ha":13},{"SG_Name":"JAGATJIT SAHOO","Father_husband_name":"CHAITANYA SAHOO","villg_name":"Sartha","Dist_name":"BALASORE","LotNo":"May/22-18-315-08G78564","Ref_No":"BAL/08/2021-22/R/87385","Varity_Code":"V386","Varity_Name":"SMARAK","Class_code":"C03","ProcessedArea":9.4,"Dist_code":"02","Farmerid":"BAL/212504","Inspected_Area":2,"Qtl_Ha":4.7},{"SG_Name":"SUMATI  CHAND","Father_husband_name":"AJAYA CHAND","villg_name":"Nabara","Dist_name":"BALASORE","LotNo":"Mar/22-18-315-08G77821","Ref_No":"BAL/08/2021-22/R/85158","Varity_Code":"V406","Varity_Name":"Pusa Mustard 28","Class_code":"C03","ProcessedArea":15,"Dist_code":"02","Farmerid":"BAL/216176","Inspected_Area":2,"Qtl_Ha":7.5},{"SG_Name":"Mrs SANTILATA SENAPATI","Father_husband_name":"GAYARAM","villg_name":"Akandi","Dist_name":"BALASORE","LotNo":"Mar/22-18-315-08G77822","Ref_No":"BAL/08/2021-22/R/85159","Varity_Code":"V406","Varity_Name":"Pusa Mustard 28","Class_code":"C03","ProcessedArea":15,"Dist_code":"02","Farmerid":"BAL/217177","Inspected_Area":2,"Qtl_Ha":7.5},{"SG_Name":"Mrs SANGITA BARIK","Father_husband_name":"AMARENDRA BARIK","villg_name":"Panasa","Dist_name":"BALASORE","LotNo":"May/22-18-315-08G78605","Ref_No":"BAL/08/2021-22/R/86852","Varity_Code":"V386","Varity_Name":"SMARAK","Class_code":"C03","ProcessedArea":12.8,"Dist_code":"02","Farmerid":"Bal/219421","Inspected_Area":2,"Qtl_Ha":6.4},
 {"SG_Name":"PRADYUMNA GIRI","Father_husband_name":"SANJAY GIRI","villg_name":"Panasa","Dist_name":"BALASORE","LotNo":"May/22-18-315-08G78606","Ref_No":"BAL/08/2021-22/R/86853","Varity_Code":"V386","Varity_Name":"SMARAK","Class_code":"C03","ProcessedArea":10.4,"Dist_code":"02","Farmerid":"Bal/45559","Inspected_Area":2,"Qtl_Ha":5.2},{"SG_Name":"TAPAN KU GIRI","Father_husband_name":"SANJAY GIRI","villg_name":"Panasa","Dist_name":"BALASORE","LotNo":"Mar/22-18-315-08G77156","Ref_No":"BAL/08/2021-22/R/85155","Varity_Code":"V406","Varity_Name":"Pusa Mustard 28","Class_code":"C03","ProcessedArea":9,"Dist_code":"02","Farmerid":"BAL/45576","Inspected_Area":2,"Qtl_Ha":4.5},{"SG_Name":"TAPAN KU GIRI","Father_husband_name":"SANJAY GIRI","villg_name":"Panasa","Dist_name":"BALASORE","LotNo":"May/22-18-315-08G78601","Ref_No":"BAL/08/2021-22/R/86851","Varity_Code":"V386","Varity_Name":"SMARAK","Class_code":"C03","ProcessedArea":12.4,"Dist_code":"02","Farmerid":"Bal/45576","Inspected_Area":2,"Qtl_Ha":6.2},{"SG_Name":"RANJAN PATRA","Father_husband_name":"UPENDRA PATRA","villg_name":"Dhobachakurai","Dist_name":"BALASORE","LotNo":"May/22-18-315-08G78603","Ref_No":"BAL/08/2021-22/R/86854","Varity_Code":"V386","Varity_Name":"SMARAK","Class_code":"C03","ProcessedArea":12.4,"Dist_code":"02","Farmerid":"Bal/48731","Inspected_Area":2,"Qtl_Ha":6.2},{"SG_Name":"RAGHUNATH SAHOO","Father_husband_name":"MADHUSUDAN SAHOO","villg_name":"Gilajodi","Dist_name":"BALASORE","LotNo":"May/22-18-315-08G78696","Ref_No":"BAL/08/2021-22/R/87897","Varity_Code":"V386","Varity_Name":"SMARAK","Class_code":"C03","ProcessedArea":4.2,"Dist_code":"02","Farmerid":"BAL/61986","Inspected_Area":2,"Qtl_Ha":2.1},{"SG_Name":"CHINMAY KUMAR DEY","Father_husband_name":"NARAHARI DEY","villg_name":"Srirampur","Dist_name":"BALASORE","LotNo":"Mar/22-18-315-08G77385","Ref_No":"BAL/08/2021-22/R/85184","Varity_Code":"V406","Varity_Name":"Pusa Mustard 28","Class_code":"C03","ProcessedArea":17,"Dist_code":"02","Farmerid":"BAL/75790","Inspected_Area":2,"Qtl_Ha":8.5},{"SG_Name":"BHAGIRATHA GIRI","Father_husband_name":"RAMHARI GIRI","villg_name":"Chalanti","Dist_name":"BALASORE","LotNo":"May/22-18-175-08G78644","Ref_No":"BAL/08/2021-22/R/85417","Varity_Code":"V370","Varity_Name":"CO-7","Class_code":"C01","ProcessedArea":52,"Dist_code":"02","Farmerid":"BAL/91517","Inspected_Area":4,"Qtl_Ha":13},{"SG_Name":"SANDITA SAHOO","Father_husband_name":"PADMALOCHAN SAHU","villg_name":"Gilajodi","Dist_name":"BALASORE","LotNo":"May/22-18-315-08G78570","Ref_No":"BAL/08/2021-22/R/87383","Varity_Code":"V386","Varity_Name":"SMARAK","Class_code":"C03","ProcessedArea":10.8,"Dist_code":"02","Farmerid":"BAL/92612","Inspected_Area":2,"Qtl_Ha":5.4},{"SG_Name":"BIDYADHAR SAHOO","Father_husband_name":"KHETRAMOHAN SAHOO","villg_name":"Gilajodi","Dist_name":"BALASORE","LotNo":"May/22-18-315-08G78568","Ref_No":"BAL/08/2021-22/R/87517","Varity_Code":"V386","Varity_Name":"SMARAK","Class_code":"C01","ProcessedArea":5.8,"Dist_code":"02","Farmerid":"BAL/92929","Inspected_Area":1.5,"Qtl_Ha":3.866666},{"SG_Name":"SOUKILAL NAIK","Father_husband_name":"CHANDRABHANU NAIK","villg_name":"pikrijharan","Dist_name":"BARGARH","LotNo":"May/22-18-173-01G77874","Ref_No":"BAR/01/2021-22/R/85245","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C03","ProcessedArea":45.6,"Dist_code":"03","Farmerid":"BAR/41015","Inspected_Area":3,"Qtl_Ha":15.2},{"SG_Name":"JHASAKETAN NAYAK","Father_husband_name":"BHAGABAN NAYAK","villg_name":"pikrijharan","Dist_name":"BARGARH","LotNo":"May/22-18-173-01G77873","Ref_No":"BAR/01/2021-22/R/85244","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C03","ProcessedArea":48,"Dist_code":"03","Farmerid":"bar/44026","Inspected_Area":4,"Qtl_Ha":12},{"SG_Name":"JUDHISTIRA SETH","Father_husband_name":"BHAGABATIA SETH","villg_name":"Chikhili","Dist_name":"BARGARH","LotNo":"May/22-18-173-01G78204","Ref_No":"BAR/01/2021-22/R/85248","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C03","ProcessedArea":8.8,"Dist_code":"03","Farmerid":"BAR/45327","Inspected_Area":2,"Qtl_Ha":4.4},{"SG_Name":"PRAFULA SAHU","Father_husband_name":"PURANDAR SAHU","villg_name":"Cherengamunda","Dist_name":"BARGARH","LotNo":"Apr/22-18-1-01G77840","Ref_No":"BAR/01/2021-22/R/85226","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C03","ProcessedArea":22.5,"Dist_code":"03","Farmerid":"BAR/48021","Inspected_Area":1.8,"Qtl_Ha":12.5},{"SG_Name":"BHOLANATH SAHU","Father_husband_name":"DAITARI","villg_name":"Ghess","Dist_name":"BARGARH","LotNo":"Mar/22-18-221-01G77872","Ref_No":"BAR/01/2021-22/R/85195","Varity_Code":"V406","Varity_Name":"Pusa Mustard 28","Class_code":"C03","ProcessedArea":0.52,"Dist_code":"03","Farmerid":"BAR/76475","Inspected_Area":2.9,"Qtl_Ha":0.17931},{"SG_Name":"BHOLANATH SAHU","Father_husband_name":"DAITARI","villg_name":"Ghess","Dist_name":"BARGARH","LotNo":"May/22-18-1-01G77981","Ref_No":"BAR/01/2021-22/R/85196","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C03","ProcessedArea":14,"Dist_code":"03","Farmerid":"BAR/76475","Inspected_Area":1,"Qtl_Ha":14},{"SG_Name":"KRUSHNA SAHU","Father_husband_name":"UDENATH SAHU","villg_name":"Laumunda","Dist_name":"BARGARH","LotNo":"Apr/22-18-1-01G77813","Ref_No":"BAR/01/2021-22/R/85227","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C03","ProcessedArea":22.5,"Dist_code":"03","Farmerid":"BAR/8676","Inspected_Area":1.8,"Qtl_Ha":12.5},{"SG_Name":"PRADYUMNA NAYAK","Father_husband_name":"PURNACHANDRA NAYAK","villg_name":"Kalmi","Dist_name":"BARGARH","LotNo":"May/22-18-173-01G77875","Ref_No":"BAR/01/2021-22/R/85246","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C03","ProcessedArea":43.6,"Dist_code":"03","Farmerid":"BAR/88396","Inspected_Area":3,"Qtl_Ha":14.533333},
 {"SG_Name":"RAMAKRUSHNA NAHAK","Father_husband_name":"MAHANTAR","villg_name":"Sunathara","Dist_name":"GANJAM","LotNo":"APR/22-18-268-06G77262","Ref_No":"GAN/06/2021-22/R/85119","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C01","ProcessedArea":54,"Dist_code":"11","Farmerid":"GAN/142386","Inspected_Area":4,"Qtl_Ha":13.5},{"SG_Name":"SUDARSANA PANIGRAHI","Father_husband_name":"RUPACHANDRA PANIGRAHI","villg_name":"Badabaranga","Dist_name":"GANJAM","LotNo":"apr/22-18-268-06g77504","Ref_No":"GAN/06/2021-22/R/85109","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C01","ProcessedArea":33,"Dist_code":"11","Farmerid":"GAN/148600","Inspected_Area":4,"Qtl_Ha":8.25},{"SG_Name":"RUPACHANDRA PANIGRAHI","Father_husband_name":"HARI PANIGRAHI","villg_name":"Badabaranga","Dist_name":"GANJAM","LotNo":"apr/22-18-268-06g77502","Ref_No":"GAN/06/2021-22/R/85118","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C01","ProcessedArea":19.2,"Dist_code":"11","Farmerid":"GAN/148603","Inspected_Area":2,"Qtl_Ha":9.6},{"SG_Name":"SURANJITA PANIGRAHI","Father_husband_name":"JYOTIRAJ PANIGRAHI","villg_name":"Badabaranga","Dist_name":"GANJAM","LotNo":"apr/22-18-268-06G77506","Ref_No":"GAN/06/2021-22/R/85115","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C01","ProcessedArea":13.5,"Dist_code":"11","Farmerid":"GAN/148607","Inspected_Area":2,"Qtl_Ha":6.75},{"SG_Name":"JYOSHNA PANIGRAHI","Father_husband_name":"SUDARSANA PANIGRAHI","villg_name":"Badabaranga","Dist_name":"GANJAM","LotNo":"apr/22-18-268-06G77507","Ref_No":"GAN/06/2021-22/R/85114","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C01","ProcessedArea":39,"Dist_code":"11","Farmerid":"GAN/148610","Inspected_Area":4,"Qtl_Ha":9.75},{"SG_Name":"M KURAMANI","Father_husband_name":"RAGHU","villg_name":"Badabaranga","Dist_name":"GANJAM","LotNo":"apr/22-18-268-06g77503","Ref_No":"GAN/06/2021-22/R/85117","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C01","ProcessedArea":12.8,"Dist_code":"11","Farmerid":"GAN/166272","Inspected_Area":2,"Qtl_Ha":6.4},{"SG_Name":"SUMAN KUMAR PANIGRAHY","Father_husband_name":"SUDARSAN PANIGRAHY","villg_name":"Badabaranga","Dist_name":"GANJAM","LotNo":"apr/22-18-268-06G77508","Ref_No":"GAN/06/2021-22/R/85116","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C01","ProcessedArea":10.2,"Dist_code":"11","Farmerid":"GAN/196578","Inspected_Area":2,"Qtl_Ha":5.1},{"SG_Name":"MEENA PATEL","Father_husband_name":"BINAYA KUMAR PATEL","villg_name":"Mundrajore","Dist_name":"JHARSUGUDA","LotNo":"Mar/22-18-223-01G78326","Ref_No":"SAM/01/2021-22/R/85170","Varity_Code":"V406","Varity_Name":"Pusa Mustard 28","Class_code":"C03","ProcessedArea":24.92,"Dist_code":"14","Farmerid":"jha/21627","Inspected_Area":5,"Qtl_Ha":4.984},{"SG_Name":"FALINDRA PATEL","Father_husband_name":"SAHEBRAM PATEL","villg_name":"Mundrajore","Dist_name":"JHARSUGUDA","LotNo":"Feb/22-18-223-01G78319","Ref_No":"JHA/01/2021-22/R/85177","Varity_Code":"V406","Varity_Name":"Pusa Mustard 28","Class_code":"C03","ProcessedArea":24.64,"Dist_code":"14","Farmerid":"jha/3666","Inspected_Area":5,"Qtl_Ha":4.928},{"SG_Name":"BINAYA KUMAR PATEL","Father_husband_name":"SUNDARMANI PATEL","villg_name":"Mundrajore","Dist_name":"JHARSUGUDA","LotNo":"Mar/22-18-223-01G78321","Ref_No":"JHA/01/2021-22/R/85179","Varity_Code":"V406","Varity_Name":"Pusa Mustard 28","Class_code":"C03","ProcessedArea":24.92,"Dist_code":"14","Farmerid":"JHA/3667","Inspected_Area":5,"Qtl_Ha":4.984},{"SG_Name":"FAKIR MOHAN PATEL","Father_husband_name":"SAHEBRAM PATEL","villg_name":"Mundrajore","Dist_name":"JHARSUGUDA","LotNo":"Mar/22-18-223-01G78334","Ref_No":"SAM/01/2021-22/R/85176","Varity_Code":"V406","Varity_Name":"Pusa Mustard 28","Class_code":"C03","ProcessedArea":24.64,"Dist_code":"14","Farmerid":"jha/5961","Inspected_Area":5,"Qtl_Ha":4.928},{"SG_Name":"TAPAS KUMAR PATEL","Father_husband_name":"FAKIRMOHAN PATEL","villg_name":"Mundrajore","Dist_name":"JHARSUGUDA","LotNo":"Mar/22-18-223-01G78320","Ref_No":"JHA/01/2021-22/R/85178","Varity_Code":"V406","Varity_Name":"Pusa Mustard 28","Class_code":"C03","ProcessedArea":24.92,"Dist_code":"14","Farmerid":"jha/5962","Inspected_Area":5,"Qtl_Ha":4.984},{"SG_Name":"HRUDANANDA SWAIN","Father_husband_name":"GOBINDA SWAIN","villg_name":"Badalasasan","Dist_name":"KHURDA","LotNo":"Mar/22-18-59-02G77005","Ref_No":"PUR/02/2021-22/R/85162","Varity_Code":"V370","Varity_Name":"CO-7","Class_code":"C01","ProcessedArea":28,"Dist_code":"18","Farmerid":"KHU/18873","Inspected_Area":2,"Qtl_Ha":14},{"SG_Name":"KEDAR NATH SWAIN","Father_husband_name":"LAXMIDHAR SWAIN","villg_name":"Badalasasan","Dist_name":"KHURDA","LotNo":"Apr/22-18-59-02G77334","Ref_No":"PUR/02/2021-22/R/85153","Varity_Code":"V370","Varity_Name":"CO-7","Class_code":"C01","ProcessedArea":20,"Dist_code":"18","Farmerid":"KHU/45380","Inspected_Area":2,"Qtl_Ha":10},{"SG_Name":"CHAITANY MALI","Father_husband_name":"PADLAM MALI","villg_name":"BODAPUT","Dist_name":"KORAPUT","LotNo":"MAY/22-18-84-04G78484","Ref_No":"KOR/04/2021-22/R/85989","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C01","ProcessedArea":24,"Dist_code":"19","Farmerid":"KOR/1231","Inspected_Area":2,"Qtl_Ha":12},{"SG_Name":"MANGALA MALI","Father_husband_name":"GURU MALI","villg_name":"BODAPUT","Dist_name":"KORAPUT","LotNo":"MAY/22-18-84-04G78483","Ref_No":"KOR/04/2021-22/R/85983","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C01","ProcessedArea":23,"Dist_code":"19","Farmerid":"KOR/1881","Inspected_Area":2,"Qtl_Ha":11.5},{"SG_Name":"GUPTA MALI","Father_husband_name":"MANGALA MALI","villg_name":"BODAPUT","Dist_name":"KORAPUT","LotNo":"MAY/22-18-84-04G78482","Ref_No":"KOR/04/2021-22/R/85986","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C01","ProcessedArea":23.8,"Dist_code":"19","Farmerid":"KOR/2248","Inspected_Area":2,"Qtl_Ha":11.9},{"SG_Name":"JAGABANDHU SAMRATH","Father_husband_name":"DHABLU SAMRATH","villg_name":"Kumarput","Dist_name":"MALKANGIRI","LotNo":"May/22-18-84-04G78124","Ref_No":"MAL/04/2021-22/R/85300","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C03","ProcessedArea":10,"Dist_code":"20","Farmerid":"MAL/14","Inspected_Area":1,"Qtl_Ha":10},{"SG_Name":"DEBENDRA PUHAN","Father_husband_name":"KANDA KISHOR PUHAN","villg_name":"Daiguda","Dist_name":"MALKANGIRI","LotNo":"May/22-18-84-04G78125","Ref_No":"MAL/04/2021-22/R/86076","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C03","ProcessedArea":8.2,"Dist_code":"20","Farmerid":"mal/3432","Inspected_Area":3,"Qtl_Ha":2.733333},{"SG_Name":"SAMARA KOPE","Father_husband_name":"CHAKRADHAR","villg_name":"Chadeiguda","Dist_name":"MALKANGIRI","LotNo":"May/22-18-84-04G78122","Ref_No":"MAL/04/2021-22/R/85299","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C03","ProcessedArea":6.2,"Dist_code":"20","Farmerid":"MAL/3572","Inspected_Area":1,"Qtl_Ha":6.2},{"SG_Name":"MANGALA KIRSANI","Father_husband_name":"gourang","villg_name":"Dhangadaguda","Dist_name":"MALKANGIRI","LotNo":"May/22-18-84-04G78121","Ref_No":"MAL/04/2021-22/R/85298","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C03","ProcessedArea":14.6,"Dist_code":"20","Farmerid":"MAL/48084","Inspected_Area":1,"Qtl_Ha":14.6},
 {"SG_Name":"PRASANTA KU NAYAK","Father_husband_name":"BIDYADHAR NAYAK","villg_name":"Sarbanaghati","Dist_name":"MAYURBHANJ","LotNo":"May/22-18-180-05G77993","Ref_No":"MAY/05/2021-22/R/85395","Varity_Code":"V507","Varity_Name":"Dheeraj(TCGS-1073)","Class_code":"C01","ProcessedArea":24.2,"Dist_code":"21","Farmerid":"MAY/20885","Inspected_Area":2,"Qtl_Ha":12.1},
{"SG_Name":"DEBENDRA LENKA","Father_husband_name":"LAXMIDHAR LENKA","villg_name":"Kukurdima","Dist_name":"MAYURBHANJ","LotNo":"May/22-18-180-05G77989","Ref_No":"MAY/05/2021-22/R/85390","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C01","ProcessedArea":21,"Dist_code":"21","Farmerid":"MAY/47170","Inspected_Area":3,"Qtl_Ha":7},{"SG_Name":"BISHNU CHANDRA PRADHAN","Father_husband_name":"SIBAPRASAD  PRADHAN","villg_name":"Sarbanaghati","Dist_name":"MAYURBHANJ","LotNo":"May/22-18-180-05G77991","Ref_No":"MAY/05/2021-22/R/85392","Varity_Code":"V507","Varity_Name":"Dheeraj(TCGS-1073)","Class_code":"C01","ProcessedArea":40,"Dist_code":"21","Farmerid":"MAY/54706","Inspected_Area":4,"Qtl_Ha":10},{"SG_Name":"BHIMA MURMU","Father_husband_name":"PITHA MURMU","villg_name":"Kutling","Dist_name":"MAYURBHANJ","LotNo":"May/22-18-180-05G77987","Ref_No":"MAY/05/2021-22/R/85388","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C01","ProcessedArea":46,"Dist_code":"21","Farmerid":"MAY/56885","Inspected_Area":3.8,"Qtl_Ha":12.105263},{"SG_Name":"ASHIS MOHAPATRA","Father_husband_name":"PURNACHANDRA","villg_name":"Kutling","Dist_name":"MAYURBHANJ","LotNo":"May/22-18-180-05G77983","Ref_No":"MAY/05/2021-22/R/85384","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C01","ProcessedArea":36,"Dist_code":"21","Farmerid":"MAY/56891","Inspected_Area":3,"Qtl_Ha":12},{"SG_Name":"RANJEET  KHANDEI","Father_husband_name":"GANGADHARA  KHANDEI","villg_name":"Kutling","Dist_name":"MAYURBHANJ","LotNo":"May/22-18-180-05G77988","Ref_No":"MAY/05/2021-22/R/85389","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C01","ProcessedArea":38.8,"Dist_code":"21","Farmerid":"MAY/57576","Inspected_Area":3.8,"Qtl_Ha":10.210526},{"SG_Name":"GAJENDRA MANIKA","Father_husband_name":"KAMINI MANIKA","villg_name":"Kutling","Dist_name":"MAYURBHANJ","LotNo":"May/22-18-180-05G77984","Ref_No":"MAY/05/2021-22/R/85385","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C01","ProcessedArea":36,"Dist_code":"21","Farmerid":"MAY/57819","Inspected_Area":3,"Qtl_Ha":12},{"SG_Name":"HARIMOHAN BASKE","Father_husband_name":"BHIMA BASKE","villg_name":"Kutling","Dist_name":"MAYURBHANJ","LotNo":"May/22-18-180-05G77985","Ref_No":"MAY/05/2021-22/R/85386","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C01","ProcessedArea":49,"Dist_code":"21","Farmerid":"MAY/59139","Inspected_Area":4,"Qtl_Ha":12.25},{"SG_Name":"NIROD JENA","Father_husband_name":"SUDAM JENA","villg_name":"Kutling","Dist_name":"MAYURBHANJ","LotNo":"May/22-18-180-05G77986","Ref_No":"MAY/05/2021-22/R/85387","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C01","ProcessedArea":48.4,"Dist_code":"21","Farmerid":"MAY/59755","Inspected_Area":4,"Qtl_Ha":12.1},{"SG_Name":"Bibhutibhusana Nayak","Father_husband_name":"Surendra Nayak","villg_name":"Sarbanaghati","Dist_name":"MAYURBHANJ","LotNo":"Mar/22-18-249-05G79090","Ref_No":"MAY/05/2021-22/R/85232","Varity_Code":"V476","Varity_Name":"Pant Sweta (PYS 2007-10)","Class_code":"C03","ProcessedArea":7.48,"Dist_code":"21","Farmerid":"may/82396","Inspected_Area":2.4,"Qtl_Ha":3.116666},{"SG_Name":"Bibhutibhusana Nayak","Father_husband_name":"Surendra Nayak","villg_name":"Sarbanaghati","Dist_name":"MAYURBHANJ","LotNo":"May/22-18-180-05G77990","Ref_No":"MAY/05/2021-22/R/85391","Varity_Code":"V507","Varity_Name":"Dheeraj(TCGS-1073)","Class_code":"C01","ProcessedArea":48.4,"Dist_code":"21","Farmerid":"MAY/82396","Inspected_Area":4,"Qtl_Ha":12.1},{"SG_Name":"DHARANIDHAR NAYAK","Father_husband_name":"BIBHUTI BHUSHAN NAYAK","villg_name":"Sarbanaghati","Dist_name":"MAYURBHANJ","LotNo":"Jun/22-18-180-05G77992","Ref_No":"MAY/05/2021-22/R/85394","Varity_Code":"V507","Varity_Name":"Dheeraj(TCGS-1073)","Class_code":"C01","ProcessedArea":48.6,"Dist_code":"21","Farmerid":"MAY/96313","Inspected_Area":4,"Qtl_Ha":12.15},{"SG_Name":"KALANDI PANDA","Father_husband_name":"GOURANGA PANDA","villg_name":"Chhenua","Dist_name":"PURI","LotNo":"Apr/22-18-59-02G76931","Ref_No":"PUR/02/2021-22/R/85198","Varity_Code":"V370","Varity_Name":"CO-7","Class_code":"C01","ProcessedArea":41,"Dist_code":"26","Farmerid":"PUR/102855","Inspected_Area":3,"Qtl_Ha":13.666666},{"SG_Name":"BHRUGARAJA PANDA","Father_husband_name":"GAURANGA PANDA","villg_name":"Chhenua","Dist_name":"PURI","LotNo":"Apr/22-18-59-02G76932","Ref_No":"PUR/02/2021-22/R/85199","Varity_Code":"V370","Varity_Name":"CO-7","Class_code":"C01","ProcessedArea":41.4,"Dist_code":"26","Farmerid":"PUR/102889","Inspected_Area":3,"Qtl_Ha":13.8},{"SG_Name":"SADANANDA PRADHAN ","Father_husband_name":"Lingaraj Pradhan ","villg_name":"Arisal","Dist_name":"PURI","LotNo":"Apr/22-18-59-02G77458","Ref_No":"PUR/02/2021-22/R/85256","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C01","ProcessedArea":28.4,"Dist_code":"26","Farmerid":"PUR/121768","Inspected_Area":2,"Qtl_Ha":14.2},{"SG_Name":"KAILASH CH KHATEI","Father_husband_name":"SAURI KHATEI","villg_name":"Bindha","Dist_name":"PURI","LotNo":"Apr/22-18-59-02G77453","Ref_No":"PUR/02/2021-22/R/85254","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C01","ProcessedArea":35.2,"Dist_code":"26","Farmerid":"pur/5540","Inspected_Area":3,"Qtl_Ha":11.733333},{"SG_Name":"SARBESWAR DAS","Father_husband_name":"LATE GOBIND CHANDRA DAS","villg_name":"Inchala","Dist_name":"PURI","LotNo":"May/22-18-59-02G77827","Ref_No":"PUR/02/2021-22/R/85497","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C01","ProcessedArea":28,"Dist_code":"26","Farmerid":"PUR/72735","Inspected_Area":2,"Qtl_Ha":14},{"SG_Name":"GUNANIDHI SAHOO","Father_husband_name":"SANATAN SAHOO","villg_name":"Arjungada","Dist_name":"PURI","LotNo":"Apr/22-18-59-02G77457","Ref_No":"PUR/02/2021-22/R/85348","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C01","ProcessedArea":28,"Dist_code":"26","Farmerid":"PUR/74663","Inspected_Area":2,"Qtl_Ha":14},{"SG_Name":"RAMESHCHANDRA PRADHAN","Father_husband_name":"DHOBEI PRADHAN ","villg_name":"Bindha","Dist_name":"PURI","LotNo":"Apr/22-18-59-02G76937","Ref_No":"PUR/02/2021-22/R/85377","Varity_Code":"V507","Varity_Name":"Dheeraj(TCGS-1073)","Class_code":"C01","ProcessedArea":35.2,"Dist_code":"26","Farmerid":"PUR/88095","Inspected_Area":3,"Qtl_Ha":11.733333},{"SG_Name":"AGANI SWAIN","Father_husband_name":"LINGARAJ SWAIN","villg_name":"Bindha","Dist_name":"PURI","LotNo":"Apr/22-18-59-02G76936","Ref_No":"PUR/02/2021-22/R/85376","Varity_Code":"V507","Varity_Name":"Dheeraj(TCGS-1073)","Class_code":"C01","ProcessedArea":45,"Dist_code":"26","Farmerid":"pur/88643","Inspected_Area":3,"Qtl_Ha":15},{"SG_Name":"PRASANTA KUMAR PRADHAN","Father_husband_name":"NARENDRA PRADHAN","villg_name":"Chhenua","Dist_name":"PURI","LotNo":"Apr/22-18-59-02G77335","Ref_No":"PUR/02/2021-22/R/85140","Varity_Code":"V370","Varity_Name":"CO-7","Class_code":"C01","ProcessedArea":12.6,"Dist_code":"26","Farmerid":"PUR/92686","Inspected_Area":2,"Qtl_Ha":6.3},{"SG_Name":"KULAMANI PATEL","Father_husband_name":"JUDHISTHIR PATEL","villg_name":"dangajore","Dist_name":"SAMBALPUR","LotNo":"Mar/22-18-223-01G78322","Ref_No":"SAM/01/2021-22/R/85166","Varity_Code":"V406","Varity_Name":"Pusa Mustard 28","Class_code":"C03","ProcessedArea":24.92,"Dist_code":"28","Farmerid":"sam/56695","Inspected_Area":5,"Qtl_Ha":4.984},{"SG_Name":"SABITRI PATEL","Father_husband_name":"KULAMANI PATEL","villg_name":"dangajore","Dist_name":"SAMBALPUR","LotNo":"Mar/22-18-223-01G78324","Ref_No":"SAM/01/2021-22/R/85168","Varity_Code":"V406","Varity_Name":"Pusa Mustard 28","Class_code":"C03","ProcessedArea":24.92,"Dist_code":"28","Farmerid":"sam/56703","Inspected_Area":5,"Qtl_Ha":4.984},{"SG_Name":"SUBRAT PATEL","Father_husband_name":"ABHIRAM PATEL","villg_name":"Bhaluchuan(SAN)","Dist_name":"SAMBALPUR","LotNo":"Mar/22-18-223-01G78332","Ref_No":"SAM/01/2021-22/R/85180","Varity_Code":"V406","Varity_Name":"Pusa Mustard 28","Class_code":"C03","ProcessedArea":24.64,"Dist_code":"28","Farmerid":"sam/59266","Inspected_Area":5,"Qtl_Ha":4.928},{"SG_Name":"NABIN BIHARI NAYAK","Father_husband_name":"JUDHISTHIR NAYAK","villg_name":"KUCHINDA NAC","Dist_name":"SAMBALPUR","LotNo":"Mar/22-18-223-01G78330","Ref_No":"SAM/01/2021-22/R/85174","Varity_Code":"V406","Varity_Name":"Pusa Mustard 28","Class_code":"C03","ProcessedArea":24.64,"Dist_code":"28","Farmerid":"SAM/80560","Inspected_Area":5,"Qtl_Ha":4.928}
]
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
            console.log(i,data.length);
          
           
            CROP_CODE = await sequelizeStock.query(`SELECT TOP(1)Crop_Code FROM mCropVariety WHERE Variety_Code = :Varity_Code`, {
                replacements: { Varity_Code: data[i].Varity_Code }, type: sequelizeStock.QueryTypes.SELECT
            });
            console.log(CROP_CODE[0]);
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
            }); if (PREV_SUBSIDISED_AREA[0].PREV_SUBSIDISED_AREA < 5) {
                if ((PREV_SUBSIDISED_AREA[0].PREV_SUBSIDISED_AREA + data[i].Inspected_Area) <= 5) {
                    SUBSIDISED_AREA = data[i].Inspected_Area;
                }
                else {
                    SUBSIDISED_AREA = (5 - PREV_SUBSIDISED_AREA[0].PREV_SUBSIDISED_AREA);
                }
                console.log(CROP_CODE[0].Crop_Code);
                if (CROP_CODE[0].Crop_Code == 'C026') {
                    console.log(SUBSIDISED_AREA,data[i].ProcessedArea,(SUBSIDISED_AREA * 20) <= data[i].ProcessedArea);
                    if ((SUBSIDISED_AREA * 20) <= data[i].ProcessedArea) {
                        console.log(SUBSIDISED_AREA * 20, 'SUBSIDISED_AREA * 20');
                        SUBSIDISED_QTY = (SUBSIDISED_AREA * 20);
                    }
                    else {
                        console.log(data[i].ProcessedArea, 'data[i].ProcessedArea');
                        SUBSIDISED_QTY = data[i].ProcessedArea;
                    }
                    if (data[i].Qtl_Ha < 20)
                        console.log(SUBSIDISED_AREA * data[i].Qtl_Ha,'SUBSIDISED_AREA * data[i].Qtl_Ha',SUBSIDISED_AREA , data[i].Qtl_Ha);
                    SUBSIDISED_QTY = (SUBSIDISED_AREA * data[i].Qtl_Ha);
                }
                if (CROP_CODE[0].Crop_Code == 'C033') {
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

                if (CROP_CODE[0].Crop_Code == 'C029') {
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


                if (CROP_CODE[0].Crop_Code == 'C027') {
                    console.log(CROP_CODE[0].Crop_Code);
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
                if (CROP_CODE[0].Crop_Code == 'C028') {
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


                if (CROP_CODE[0].Crop_Code == 'C036') {
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
            console.log(SUBSIDISED_QTY, 'SUBSIDISED_QTY');
            var result = await sequelizeStock.query(` INSERT INTO mINCENTIVE2 (TRANSACTION_ID,FARMER_ID, FARMER_NAME, FATHER_HUSBAND_NAME, Category_Code, Crop_Code, VARIETY_CODE, 
                VARIETY_NAME,DIST_CODE, DIST_NAME, REF_NO, LOT_NO, CLASS_CODE, VILLG_NAME, PROCESSED_QTY, SUBSIDISED_QTY, INSPECTED_AREA, SUBSIDISED_AREA,
                 GOI_SUBSIDY_CODE, GOI_SUBSIDY, SP_SUBSIDY_CODE, SP_SUBSIDY, FARMER_SUBSIDY, OSSC_SUBSIDY, FIN_YR, SEASON, UPDATED_ON,QTL_HA)   
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
        // let response = await sequelizeStock.query(` UPDATE A SET A.ACC_HOLDERNAME = B.VCHACCHOLDERNM,A.ACC_NO = B.VCHACCOUNTNO,A.BANK_ID = B.INTBANKID, A.BRANCH_ID = B.INTBRANCHID, A.IFSC_CODE = B.VCHIFSCCODE FROM mINCENTIVE2 A   
        // INNER JOIN [FARMERDB].[DBO].[M_FARMER_REGISTRATION] B ON A.FARMER_ID = B.NICFARMERID COLLATE Latin1_General_CI_AI  
        // WHERE A.ACC_NO IS NULL`);
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

//all
exports.GetIncentiveOilSeedAll = (data) => new Promise(async (resolve, reject) => {
    var con = new sqlstock.ConnectionPool(locConfigOssopoca);
    // var con = new sqlstock.ConnectionPool(locConfigstock);
    console.log(data);
    try {
        // const totalprebookingdtl = await sequelizeOssopoca.query(`SELECT SG_Name, Father_husband_name, villg_name, Dist_name, LotNo, Ref_No, Varity_Code, Varity_Name, Class_code, ProcessedArea, Dist_code, Farmerid, Inspected_Area, (ProcessedArea/Inspected_Area)Qtl_Ha FROM dbo.VwIncentiveOilseedAll WHERE Farmerid IS NOT NULL and crop_code in ('C026') AND SUBSTRING(REF_NO,8,7) = :FIN_YR ORDER BY Farmerid,(ProcessedArea/Inspected_Area)`, {
        //     replacements: {FIN_YR: data.selectedFinancialYear}, type: sequelizeOssopoca.QueryTypes.SELECT
        // });
        const totalprebookingdtl = [{"SG_Name":"RADHA GOBINDA MOHANTY","Father_husband_name":"RAGHUNATH MOHANTY","villg_name":"JADIBALI","Dist_name":"BALASORE","LotNo":"May/22-18-175-08G78643","Ref_No":"BAL/08/2021-22/R/85399","Varity_Code":"V370","Varity_Name":"CO-7","Class_code":"C01","ProcessedArea":26.8,"Dist_code":"02","Farmerid":"BAL/16088","Inspected_Area":2,"Qtl_Ha":13.4},{"SG_Name":"DIPAK KUMAR DAS","Father_husband_name":"SUMANTA KUMAR DAS","villg_name":"Sitadiha","Dist_name":"BALASORE","LotNo":"May/22-18-175-08G78663","Ref_No":"BAL/08/2021-22/R/85511","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C01","ProcessedArea":12.8,"Dist_code":"02","Farmerid":"BAL/204526","Inspected_Area":2,"Qtl_Ha":6.4},{"SG_Name":"GIRISH CHANDRA MOHANTY","Father_husband_name":"GANANATH MOHANTY","villg_name":"JADIBALI","Dist_name":"BALASORE","LotNo":"May/22-18-175-08G78642","Ref_No":"BAL/08/2021-22/R/85398","Varity_Code":"V370","Varity_Name":"CO-7","Class_code":"C01","ProcessedArea":26,"Dist_code":"02","Farmerid":"BAL/21065","Inspected_Area":2,"Qtl_Ha":13},{"SG_Name":"BHAGIRATHA GIRI","Father_husband_name":"RAMHARI GIRI","villg_name":"Chalanti","Dist_name":"BALASORE","LotNo":"May/22-18-175-08G78644","Ref_No":"BAL/08/2021-22/R/85417","Varity_Code":"V370","Varity_Name":"CO-7","Class_code":"C01","ProcessedArea":52,"Dist_code":"02","Farmerid":"BAL/91517","Inspected_Area":4,"Qtl_Ha":13},{"SG_Name":"SOUKILAL NAIK","Father_husband_name":"CHANDRABHANU NAIK","villg_name":"pikrijharan","Dist_name":"BARGARH","LotNo":"May/22-18-173-01G77874","Ref_No":"BAR/01/2021-22/R/85245","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C03","ProcessedArea":45.6,"Dist_code":"03","Farmerid":"BAR/41015","Inspected_Area":3,"Qtl_Ha":15.2},{"SG_Name":"JHASAKETAN NAYAK","Father_husband_name":"BHAGABAN NAYAK","villg_name":"pikrijharan","Dist_name":"BARGARH","LotNo":"May/22-18-173-01G77873","Ref_No":"BAR/01/2021-22/R/85244","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C03","ProcessedArea":48,"Dist_code":"03","Farmerid":"bar/44026","Inspected_Area":4,"Qtl_Ha":12},{"SG_Name":"JUDHISTIRA SETH","Father_husband_name":"BHAGABATIA SETH","villg_name":"Chikhili","Dist_name":"BARGARH","LotNo":"May/22-18-173-01G78204","Ref_No":"BAR/01/2021-22/R/85248","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C03","ProcessedArea":8.8,"Dist_code":"03","Farmerid":"BAR/45327","Inspected_Area":2,"Qtl_Ha":4.4},{"SG_Name":"PRAFULA SAHU","Father_husband_name":"PURANDAR SAHU","villg_name":"Cherengamunda","Dist_name":"BARGARH","LotNo":"Apr/22-18-1-01G77840","Ref_No":"BAR/01/2021-22/R/85226","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C03","ProcessedArea":22.5,"Dist_code":"03","Farmerid":"BAR/48021","Inspected_Area":1.8,"Qtl_Ha":12.5},{"SG_Name":"BHOLANATH SAHU","Father_husband_name":"DAITARI","villg_name":"Ghess","Dist_name":"BARGARH","LotNo":"May/22-18-1-01G77981","Ref_No":"BAR/01/2021-22/R/85196","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C03","ProcessedArea":14,"Dist_code":"03","Farmerid":"BAR/76475","Inspected_Area":1,"Qtl_Ha":14},{"SG_Name":"KRUSHNA SAHU","Father_husband_name":"UDENATH SAHU","villg_name":"Laumunda","Dist_name":"BARGARH","LotNo":"Apr/22-18-1-01G77813","Ref_No":"BAR/01/2021-22/R/85227","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C03","ProcessedArea":22.5,"Dist_code":"03","Farmerid":"BAR/8676","Inspected_Area":1.8,"Qtl_Ha":12.5},{"SG_Name":"PRADYUMNA NAYAK","Father_husband_name":"PURNACHANDRA NAYAK","villg_name":"Kalmi","Dist_name":"BARGARH","LotNo":"May/22-18-173-01G77875","Ref_No":"BAR/01/2021-22/R/85246","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C03","ProcessedArea":43.6,"Dist_code":"03","Farmerid":"BAR/88396","Inspected_Area":3,"Qtl_Ha":14.533333},{"SG_Name":"RAMAKRUSHNA NAHAK","Father_husband_name":"MAHANTAR","villg_name":"Sunathara","Dist_name":"GANJAM","LotNo":"APR/22-18-268-06G77262","Ref_No":"GAN/06/2021-22/R/85119","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C01","ProcessedArea":54,"Dist_code":"11","Farmerid":"GAN/142386","Inspected_Area":4,"Qtl_Ha":13.5},{"SG_Name":"SUDARSANA PANIGRAHI","Father_husband_name":"RUPACHANDRA PANIGRAHI","villg_name":"Badabaranga","Dist_name":"GANJAM","LotNo":"apr/22-18-268-06g77504","Ref_No":"GAN/06/2021-22/R/85109","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C01","ProcessedArea":33,"Dist_code":"11","Farmerid":"GAN/148600","Inspected_Area":4,"Qtl_Ha":8.25},{"SG_Name":"RUPACHANDRA PANIGRAHI","Father_husband_name":"HARI PANIGRAHI","villg_name":"Badabaranga","Dist_name":"GANJAM","LotNo":"apr/22-18-268-06g77502","Ref_No":"GAN/06/2021-22/R/85118","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C01","ProcessedArea":19.2,"Dist_code":"11","Farmerid":"GAN/148603","Inspected_Area":2,"Qtl_Ha":9.6},{"SG_Name":"SURANJITA PANIGRAHI","Father_husband_name":"JYOTIRAJ PANIGRAHI","villg_name":"Badabaranga","Dist_name":"GANJAM","LotNo":"apr/22-18-268-06G77506","Ref_No":"GAN/06/2021-22/R/85115","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C01","ProcessedArea":13.5,"Dist_code":"11","Farmerid":"GAN/148607","Inspected_Area":2,"Qtl_Ha":6.75},{"SG_Name":"JYOSHNA PANIGRAHI","Father_husband_name":"SUDARSANA PANIGRAHI","villg_name":"Badabaranga","Dist_name":"GANJAM","LotNo":"apr/22-18-268-06G77507","Ref_No":"GAN/06/2021-22/R/85114","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C01","ProcessedArea":39,"Dist_code":"11","Farmerid":"GAN/148610","Inspected_Area":4,"Qtl_Ha":9.75},{"SG_Name":"M KURAMANI","Father_husband_name":"RAGHU","villg_name":"Badabaranga","Dist_name":"GANJAM","LotNo":"apr/22-18-268-06g77503","Ref_No":"GAN/06/2021-22/R/85117","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C01","ProcessedArea":12.8,"Dist_code":"11","Farmerid":"GAN/166272","Inspected_Area":2,"Qtl_Ha":6.4},{"SG_Name":"SUMAN KUMAR PANIGRAHY","Father_husband_name":"SUDARSAN PANIGRAHY","villg_name":"Badabaranga","Dist_name":"GANJAM","LotNo":"apr/22-18-268-06G77508","Ref_No":"GAN/06/2021-22/R/85116","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C01","ProcessedArea":10.2,"Dist_code":"11","Farmerid":"GAN/196578","Inspected_Area":2,"Qtl_Ha":5.1},{"SG_Name":"HRUDANANDA SWAIN","Father_husband_name":"GOBINDA SWAIN","villg_name":"Badalasasan","Dist_name":"KHURDA","LotNo":"Mar/22-18-59-02G77005","Ref_No":"PUR/02/2021-22/R/85162","Varity_Code":"V370","Varity_Name":"CO-7","Class_code":"C01","ProcessedArea":28,"Dist_code":"18","Farmerid":"KHU/18873","Inspected_Area":2,"Qtl_Ha":14},{"SG_Name":"KEDAR NATH SWAIN","Father_husband_name":"LAXMIDHAR SWAIN","villg_name":"Badalasasan","Dist_name":"KHURDA","LotNo":"Apr/22-18-59-02G77334","Ref_No":"PUR/02/2021-22/R/85153","Varity_Code":"V370","Varity_Name":"CO-7","Class_code":"C01","ProcessedArea":20,"Dist_code":"18","Farmerid":"KHU/45380","Inspected_Area":2,"Qtl_Ha":10},{"SG_Name":"CHAITANY MALI","Father_husband_name":"PADLAM MALI","villg_name":"BODAPUT","Dist_name":"KORAPUT","LotNo":"MAY/22-18-84-04G78484","Ref_No":"KOR/04/2021-22/R/85989","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C01","ProcessedArea":24,"Dist_code":"19","Farmerid":"KOR/1231","Inspected_Area":2,"Qtl_Ha":12},{"SG_Name":"MANGALA MALI","Father_husband_name":"GURU MALI","villg_name":"BODAPUT","Dist_name":"KORAPUT","LotNo":"MAY/22-18-84-04G78483","Ref_No":"KOR/04/2021-22/R/85983","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C01","ProcessedArea":23,"Dist_code":"19","Farmerid":"KOR/1881","Inspected_Area":2,"Qtl_Ha":11.5},{"SG_Name":"GUPTA MALI","Father_husband_name":"MANGALA MALI","villg_name":"BODAPUT","Dist_name":"KORAPUT","LotNo":"MAY/22-18-84-04G78482","Ref_No":"KOR/04/2021-22/R/85986","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C01","ProcessedArea":23.8,"Dist_code":"19","Farmerid":"KOR/2248","Inspected_Area":2,"Qtl_Ha":11.9},{"SG_Name":"JAGABANDHU SAMRATH","Father_husband_name":"DHABLU SAMRATH","villg_name":"Kumarput","Dist_name":"MALKANGIRI","LotNo":"May/22-18-84-04G78124","Ref_No":"MAL/04/2021-22/R/85300","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C03","ProcessedArea":10,"Dist_code":"20","Farmerid":"MAL/14","Inspected_Area":1,"Qtl_Ha":10},{"SG_Name":"DEBENDRA PUHAN","Father_husband_name":"KANDA KISHOR PUHAN","villg_name":"Daiguda","Dist_name":"MALKANGIRI","LotNo":"May/22-18-84-04G78125","Ref_No":"MAL/04/2021-22/R/86076","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C03","ProcessedArea":8.2,"Dist_code":"20","Farmerid":"mal/3432","Inspected_Area":3,"Qtl_Ha":2.733333},{"SG_Name":"SAMARA KOPE","Father_husband_name":"CHAKRADHAR","villg_name":"Chadeiguda","Dist_name":"MALKANGIRI","LotNo":"May/22-18-84-04G78122","Ref_No":"MAL/04/2021-22/R/85299","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C03","ProcessedArea":6.2,"Dist_code":"20","Farmerid":"MAL/3572","Inspected_Area":1,"Qtl_Ha":6.2},{"SG_Name":"MANGALA KIRSANI","Father_husband_name":"gourang","villg_name":"Dhangadaguda","Dist_name":"MALKANGIRI","LotNo":"May/22-18-84-04G78121","Ref_No":"MAL/04/2021-22/R/85298","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C03","ProcessedArea":14.6,"Dist_code":"20","Farmerid":"MAL/48084","Inspected_Area":1,"Qtl_Ha":14.6},{"SG_Name":"PRASANTA KU NAYAK","Father_husband_name":"BIDYADHAR NAYAK","villg_name":"Sarbanaghati","Dist_name":"MAYURBHANJ","LotNo":"May/22-18-180-05G77993","Ref_No":"MAY/05/2021-22/R/85395","Varity_Code":"V507","Varity_Name":"Dheeraj(TCGS-1073)","Class_code":"C01","ProcessedArea":24.2,"Dist_code":"21","Farmerid":"MAY/20885","Inspected_Area":2,"Qtl_Ha":12.1},
        {"SG_Name":"DEBENDRA LENKA","Father_husband_name":"LAXMIDHAR LENKA","villg_name":"Kukurdima","Dist_name":"MAYURBHANJ","LotNo":"May/22-18-180-05G77989","Ref_No":"MAY/05/2021-22/R/85390","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C01","ProcessedArea":21,"Dist_code":"21","Farmerid":"MAY/47170","Inspected_Area":3,"Qtl_Ha":7},{"SG_Name":"BISHNU CHANDRA PRADHAN","Father_husband_name":"SIBAPRASAD  PRADHAN","villg_name":"Sarbanaghati","Dist_name":"MAYURBHANJ","LotNo":"May/22-18-180-05G77991","Ref_No":"MAY/05/2021-22/R/85392","Varity_Code":"V507","Varity_Name":"Dheeraj(TCGS-1073)","Class_code":"C01","ProcessedArea":40,"Dist_code":"21","Farmerid":"MAY/54706","Inspected_Area":4,"Qtl_Ha":10},{"SG_Name":"BHIMA MURMU","Father_husband_name":"PITHA MURMU","villg_name":"Kutling","Dist_name":"MAYURBHANJ","LotNo":"May/22-18-180-05G77987","Ref_No":"MAY/05/2021-22/R/85388","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C01","ProcessedArea":46,"Dist_code":"21","Farmerid":"MAY/56885","Inspected_Area":3.8,"Qtl_Ha":12.105263},{"SG_Name":"ASHIS MOHAPATRA","Father_husband_name":"PURNACHANDRA","villg_name":"Kutling","Dist_name":"MAYURBHANJ","LotNo":"May/22-18-180-05G77983","Ref_No":"MAY/05/2021-22/R/85384","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C01","ProcessedArea":36,"Dist_code":"21","Farmerid":"MAY/56891","Inspected_Area":3,"Qtl_Ha":12},{"SG_Name":"RANJEET  KHANDEI","Father_husband_name":"GANGADHARA  KHANDEI","villg_name":"Kutling","Dist_name":"MAYURBHANJ","LotNo":"May/22-18-180-05G77988","Ref_No":"MAY/05/2021-22/R/85389","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C01","ProcessedArea":38.8,"Dist_code":"21","Farmerid":"MAY/57576","Inspected_Area":3.8,"Qtl_Ha":10.210526},{"SG_Name":"GAJENDRA MANIKA","Father_husband_name":"KAMINI MANIKA","villg_name":"Kutling","Dist_name":"MAYURBHANJ","LotNo":"May/22-18-180-05G77984","Ref_No":"MAY/05/2021-22/R/85385","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C01","ProcessedArea":36,"Dist_code":"21","Farmerid":"MAY/57819","Inspected_Area":3,"Qtl_Ha":12},{"SG_Name":"HARIMOHAN BASKE","Father_husband_name":"BHIMA BASKE","villg_name":"Kutling","Dist_name":"MAYURBHANJ","LotNo":"May/22-18-180-05G77985","Ref_No":"MAY/05/2021-22/R/85386","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C01","ProcessedArea":49,"Dist_code":"21","Farmerid":"MAY/59139","Inspected_Area":4,"Qtl_Ha":12.25},{"SG_Name":"NIROD JENA","Father_husband_name":"SUDAM JENA","villg_name":"Kutling","Dist_name":"MAYURBHANJ","LotNo":"May/22-18-180-05G77986","Ref_No":"MAY/05/2021-22/R/85387","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C01","ProcessedArea":48.4,"Dist_code":"21","Farmerid":"MAY/59755","Inspected_Area":4,"Qtl_Ha":12.1},{"SG_Name":"Bibhutibhusana Nayak","Father_husband_name":"Surendra Nayak","villg_name":"Sarbanaghati","Dist_name":"MAYURBHANJ","LotNo":"May/22-18-180-05G77990","Ref_No":"MAY/05/2021-22/R/85391","Varity_Code":"V507","Varity_Name":"Dheeraj(TCGS-1073)","Class_code":"C01","ProcessedArea":48.4,"Dist_code":"21","Farmerid":"MAY/82396","Inspected_Area":4,"Qtl_Ha":12.1},{"SG_Name":"DHARANIDHAR NAYAK","Father_husband_name":"BIBHUTI BHUSHAN NAYAK","villg_name":"Sarbanaghati","Dist_name":"MAYURBHANJ","LotNo":"Jun/22-18-180-05G77992","Ref_No":"MAY/05/2021-22/R/85394","Varity_Code":"V507","Varity_Name":"Dheeraj(TCGS-1073)","Class_code":"C01","ProcessedArea":48.6,"Dist_code":"21","Farmerid":"MAY/96313","Inspected_Area":4,"Qtl_Ha":12.15},{"SG_Name":"KALANDI PANDA","Father_husband_name":"GOURANGA PANDA","villg_name":"Chhenua","Dist_name":"PURI","LotNo":"Apr/22-18-59-02G76931","Ref_No":"PUR/02/2021-22/R/85198","Varity_Code":"V370","Varity_Name":"CO-7","Class_code":"C01","ProcessedArea":41,"Dist_code":"26","Farmerid":"PUR/102855","Inspected_Area":3,"Qtl_Ha":13.666666},{"SG_Name":"BHRUGARAJA PANDA","Father_husband_name":"GAURANGA PANDA","villg_name":"Chhenua","Dist_name":"PURI","LotNo":"Apr/22-18-59-02G76932","Ref_No":"PUR/02/2021-22/R/85199","Varity_Code":"V370","Varity_Name":"CO-7","Class_code":"C01","ProcessedArea":41.4,"Dist_code":"26","Farmerid":"PUR/102889","Inspected_Area":3,"Qtl_Ha":13.8},{"SG_Name":"SADANANDA PRADHAN ","Father_husband_name":"Lingaraj Pradhan ","villg_name":"Arisal","Dist_name":"PURI","LotNo":"Apr/22-18-59-02G77458","Ref_No":"PUR/02/2021-22/R/85256","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C01","ProcessedArea":28.4,"Dist_code":"26","Farmerid":"PUR/121768","Inspected_Area":2,"Qtl_Ha":14.2},{"SG_Name":"KAILASH CH KHATEI","Father_husband_name":"SAURI KHATEI","villg_name":"Bindha","Dist_name":"PURI","LotNo":"Apr/22-18-59-02G77453","Ref_No":"PUR/02/2021-22/R/85254","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C01","ProcessedArea":35.2,"Dist_code":"26","Farmerid":"pur/5540","Inspected_Area":3,"Qtl_Ha":11.733333},{"SG_Name":"SARBESWAR DAS","Father_husband_name":"LATE GOBIND CHANDRA DAS","villg_name":"Inchala","Dist_name":"PURI","LotNo":"May/22-18-59-02G77827","Ref_No":"PUR/02/2021-22/R/85497","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C01","ProcessedArea":28,"Dist_code":"26","Farmerid":"PUR/72735","Inspected_Area":2,"Qtl_Ha":14},{"SG_Name":"GUNANIDHI SAHOO","Father_husband_name":"SANATAN SAHOO","villg_name":"Arjungada","Dist_name":"PURI","LotNo":"Apr/22-18-59-02G77457","Ref_No":"PUR/02/2021-22/R/85348","Varity_Code":"V387","Varity_Name":"Dharani (TCGS 1043)","Class_code":"C01","ProcessedArea":28,"Dist_code":"26","Farmerid":"PUR/74663","Inspected_Area":2,"Qtl_Ha":14},{"SG_Name":"RAMESHCHANDRA PRADHAN","Father_husband_name":"DHOBEI PRADHAN ","villg_name":"Bindha","Dist_name":"PURI","LotNo":"Apr/22-18-59-02G76937","Ref_No":"PUR/02/2021-22/R/85377","Varity_Code":"V507","Varity_Name":"Dheeraj(TCGS-1073)","Class_code":"C01","ProcessedArea":35.2,"Dist_code":"26","Farmerid":"PUR/88095","Inspected_Area":3,"Qtl_Ha":11.733333},{"SG_Name":"AGANI SWAIN","Father_husband_name":"LINGARAJ SWAIN","villg_name":"Bindha","Dist_name":"PURI","LotNo":"Apr/22-18-59-02G76936","Ref_No":"PUR/02/2021-22/R/85376","Varity_Code":"V507","Varity_Name":"Dheeraj(TCGS-1073)","Class_code":"C01","ProcessedArea":45,"Dist_code":"26","Farmerid":"pur/88643","Inspected_Area":3,"Qtl_Ha":15},{"SG_Name":"PRASANTA KUMAR PRADHAN","Father_husband_name":"NARENDRA PRADHAN","villg_name":"Chhenua","Dist_name":"PURI","LotNo":"Apr/22-18-59-02G77335","Ref_No":"PUR/02/2021-22/R/85140","Varity_Code":"V370","Varity_Name":"CO-7","Class_code":"C01","ProcessedArea":12.6,"Dist_code":"26","Farmerid":"PUR/92686","Inspected_Area":2,"Qtl_Ha":6.3}]
            resolve(totalprebookingdtl);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        con.release();
    }

});
exports.InsertIncentiveOilSeedAll = (data) => new Promise(async (resolve, reject) => {
    var con = new sqlstock.ConnectionPool(locConfigstock);
    try {
        var SCHEME_CODE_GOI = 9145;
        var SCHEME_CODE_SP = 9145;
        var CROP_CODE = '';
        for (let i = 0; i < data.length; i++) {
            console.log(i,data.length);
          
           
            CROP_CODE = await sequelizeStock.query(`SELECT TOP(1)Crop_Code FROM mCropVariety WHERE Variety_Code = :Varity_Code`, {
                replacements: { Varity_Code: data[i].Varity_Code }, type: sequelizeStock.QueryTypes.SELECT
            });
            console.log(CROP_CODE[0]);
            var Category_Code = await sequelizeStock.query(`SELECT TOP(1)Category_Code FROM mCrop WHERE CROP_CODE =:CROP_CODE`, {
                replacements: { CROP_CODE: CROP_CODE[0].Crop_Code }, type: sequelizeStock.QueryTypes.SELECT
            });
            var SUBSIDISED_QTY = 0;
            var SUBSIDISED_AREA = 0;
            var PREV_SUBSIDY_QTY = await sequelizeStock.query(`SELECT ISNULL(SUM(SUBSIDISED_QTY),0) as PREV_SUBSIDY_QTY FROM mINCENTIVE3 WHERE FIN_YR = :selectedFinancialYear AND FARMER_ID = :FARMER_ID`, {
                replacements: { selectedFinancialYear: data[0].selectedFinancialYear, FARMER_ID: data[i].Farmerid }, type: sequelizeStock.QueryTypes.SELECT
            });
            var PREV_SUBSIDISED_AREA = await sequelizeStock.query(`SELECT ISNULL(SUM(SUBSIDISED_AREA),0) as PREV_SUBSIDISED_AREA FROM mINCENTIVE3 WHERE FIN_YR = :selectedFinancialYear AND FARMER_ID = :FARMER_ID`, {
                replacements: { selectedFinancialYear: data[0].selectedFinancialYear, FARMER_ID: data[i].Farmerid }, type: sequelizeStock.QueryTypes.SELECT
            }); if (PREV_SUBSIDISED_AREA[0].PREV_SUBSIDISED_AREA < 5) {
                if ((PREV_SUBSIDISED_AREA[0].PREV_SUBSIDISED_AREA + data[i].Inspected_Area) <= 5) {
                    SUBSIDISED_AREA = data[i].Inspected_Area;
                }
                else {
                    SUBSIDISED_AREA = (5 - PREV_SUBSIDISED_AREA[0].PREV_SUBSIDISED_AREA);
                }
                console.log(CROP_CODE[0].Crop_Code);
                if (CROP_CODE[0].Crop_Code == 'C026') {
                    console.log(SUBSIDISED_AREA,data[i].ProcessedArea,(SUBSIDISED_AREA * 20) <= data[i].ProcessedArea);
                    if ((SUBSIDISED_AREA * 20) <= data[i].ProcessedArea) {
                        console.log(SUBSIDISED_AREA * 20, 'SUBSIDISED_AREA * 20');
                        SUBSIDISED_QTY = (SUBSIDISED_AREA * 20);
                    }
                    else {
                        console.log(data[i].ProcessedArea, 'data[i].ProcessedArea');
                        SUBSIDISED_QTY = data[i].ProcessedArea;
                    }
                    if (data[i].Qtl_Ha < 20)
                        console.log(SUBSIDISED_AREA * data[i].Qtl_Ha,'SUBSIDISED_AREA * data[i].Qtl_Ha',SUBSIDISED_AREA , data[i].Qtl_Ha);
                    SUBSIDISED_QTY = (SUBSIDISED_AREA * data[i].Qtl_Ha);
                }
            }


            var MAXTRAN_NO = await sequelizeStock.query(`SELECT ISNULL(MAX(convert(INT,SUBSTRING(TRANSACTION_ID,11,5))),0)+1 as MAXTRAN_NO  FROM mINCENTIVE3 WHERE SUBSTRING(TRANSACTION_ID,1,7) =:selectedFinancialYear`, {
                replacements: { selectedFinancialYear: data[0].selectedFinancialYear }, type: sequelizeStock.QueryTypes.SELECT
            });
            //    (SELECT ISNULL(MAX(CONVERT(INT,SUBSTRING(TRANSACTION_ID,11,5))),0)+1 FROM mINCENTIVE2 WHERE SUBSTRING(TRANSACTION_ID,1,7) = @FIN_YR)  
            var TRANSACTION_ID = data[0].selectedFinancialYear + '/S/' + MAXTRAN_NO[0].MAXTRAN_NO;
            var SEASON = await sequelizeStock.query(`SELECT substring(:Ref_No,16,1) as SEASON `, {
                replacements: { Ref_No: data[i].Ref_No }, type: sequelizeStock.QueryTypes.SELECT
            });
            console.log(SUBSIDISED_QTY, 'SUBSIDISED_QTY');
            var result = await sequelizeStock.query(` INSERT INTO mINCENTIVE3 (TRANSACTION_ID,FARMER_ID, FARMER_NAME, FATHER_HUSBAND_NAME, Category_Code, Crop_Code, VARIETY_CODE, 
                VARIETY_NAME,DIST_CODE, DIST_NAME, REF_NO, LOT_NO, CLASS_CODE, VILLG_NAME, PROCESSED_QTY, SUBSIDISED_QTY, INSPECTED_AREA, SUBSIDISED_AREA,
                 GOI_SUBSIDY_CODE, GOI_SUBSIDY, SP_SUBSIDY_CODE, SP_SUBSIDY, FARMER_SUBSIDY, OSSC_SUBSIDY, FIN_YR, SEASON, UPDATED_ON,QTL_HA)   
            VALUES    (:TRANSACTION_ID, :FARMER_ID, :FARMER_NAME, :FATHER_HUSBAND_NAME, :Category_Code, :Crop_Code, :VARIETY_CODE,
                 :VARIETY_NAME, :DIST_CODE, :DIST_NAME, :REF_NO, :LOT_NO, :CLASS_CODE, :VILLG_NAME, :PROCESSED_QTY, :SUBSIDISED_QTY, :INSPECTED_AREA,
                 :SUBSIDISED_AREA, :SCHEME_CODE_GOI, (:SUBSIDISED_QTY * 750), :SCHEME_CODE_SP, (:SUBSIDISED_QTY * 0), (:SUBSIDISED_QTY * 750),
                  (:SUBSIDISED_QTY * 0),:FIN_YR,:SEASON,getdate(),:Qtl_Ha) select @@rowcount `, {
                replacements: {
                    TRANSACTION_ID: TRANSACTION_ID, FARMER_ID: data[i].Farmerid, FARMER_NAME: data[i].SG_Name, FATHER_HUSBAND_NAME: data[i].Father_husband_name, Category_Code: Category_Code[0].Category_Code, Crop_Code: CROP_CODE[0].Crop_Code, VARIETY_CODE: data[i].Varity_Code,
                    VARIETY_NAME: data[i].Varity_Name, DIST_CODE: data[i].Dist_code, DIST_NAME: data[i].Dist_name, REF_NO: data[i].Ref_No, LOT_NO: data[i].LotNo, CLASS_CODE: data[i].Class_code, VILLG_NAME: data[i].villg_name, PROCESSED_QTY: data[i].ProcessedArea, SUBSIDISED_QTY: SUBSIDISED_QTY, INSPECTED_AREA: data[i].Inspected_Area,
                    SUBSIDISED_AREA: SUBSIDISED_AREA, SCHEME_CODE_GOI: SCHEME_CODE_GOI, SCHEME_CODE_SP: SCHEME_CODE_SP, FIN_YR: data[0].selectedFinancialYear, SEASON: SEASON[0].SEASON, Qtl_Ha: data[i].Qtl_Ha
                }, type: sequelizeStock.QueryTypes.SELECT
            });


        }
        // let response = await sequelizeStock.query(` UPDATE A SET A.ACC_HOLDERNAME = B.VCHACCHOLDERNM,A.ACC_NO = B.VCHACCOUNTNO,A.BANK_ID = B.INTBANKID, A.BRANCH_ID = B.INTBRANCHID, A.IFSC_CODE = B.VCHIFSCCODE FROM mINCENTIVE2 A   
        // INNER JOIN [FARMERDB].[DBO].[M_FARMER_REGISTRATION] B ON A.FARMER_ID = B.NICFARMERID COLLATE Latin1_General_CI_AI  
        // WHERE A.ACC_NO IS NULL`);
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
exports.FillIncentiveOilSeedAll = (data) => new Promise(async (resolve, reject) => {
    var con = new sqlstock.ConnectionPool(locConfigstock);
    try {
        con.connect().then(function success() {
            const request = new sql.Request(con);
            request.input('FIN_YR', data.selectedFinancialYear);
            request.input('SEASON', data.selectedSeason);
            request.execute('FillIncentiveOilSeedAll', function (err, result) {
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
exports.FillPendingIncentiveOilSeedAll = (data) => new Promise(async (resolve, reject) => {
    var con = new sqlstock.ConnectionPool(locConfigstock);
    try {
        const totalprebookingdtl = await sequelizeStock.query(`SELECT COUNT(*) as count FROM mINCENTIVE3 WHERE FIN_YR = :FIN_YR AND OSSC_STATUS = 'Pending'`, {
            replacements: { FIN_YR: data.selectedFinancialYear }, type: sequelizeStock.QueryTypes.SELECT
        });
        resolve(totalprebookingdtl);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        con.release();
    }

});
exports.OsscAprvOilSeedAll = (data) => new Promise(async (resolve, reject) => {
    var con = new sqlstock.ConnectionPool(locConfigstock);
    try {
        const totalprebookingdtl = await sequelizeStock.query(`UPDATE mINCENTIVE3 SET OSSC_STATUS = 'Approved', OSSC_STATUS_UPDATEDBY = :OSSC_STATUS_UPDATEDBY, OSSC_STATUS_UPDATEDON = GETDATE() WHERE FIN_YR = :FIN_YR AND OSSC_STATUS = 'Pending'`, {
            replacements: { FIN_YR: data.FIN_YR, OSSC_STATUS_UPDATEDBY: data.UPDATED_BY }, type: sequelizeStock.QueryTypes.SELECT
        });
        resolve(totalprebookingdtl);
    } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
    } finally {
        con.release();
    }

});