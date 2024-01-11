var dbConfig = require('../dbConfig');
var sqlstock = dbConfig.sqlstock;
var sequelizeSeed = dbConfig.sequelizeSeed;

var locConfigstock = dbConfig.locConfigStock;
var locConfigStockLive = dbConfig.locConfigStockLive;

var sequelizeStock = dbConfig.sequelizeStock;

exports.CheckAccessMode = function (Refno, callback) {
    var con = new sqlstock.ConnectionPool(locConfigstock);
    con.connect().then(function success() {
        const request = new sqlstock.Request(con);
        request.input('REF_NO', Refno);
        request.execute('SP_CHECKACCESSMODE', function (err, result) {
            if (err) {
                
            }
            else {

                callback(result.recordset);
            }
            con.close();
        });
    }).catch(function error(err) {
    });
};

exports.GetDistCodeFromLicNo = function (LicNo, callback) {
    var con = new sqlstock.ConnectionPool(locConfigstock);
    con.connect().then(function success() {
        const request = new sqlstock.Request(con);
        request.input('LIC_NO', LicNo);
        request.execute('SP_GETDISTCODEFROMLICNO', function (err, result) {
            if (err) {
                
            }
            else {
                console.log(result.recordset);
                callback(result.recordset);
            }
            con.close();
        });
    }).catch(function error(err) {
        
    });
};

exports.GetCompTypeFromLicNo = function (LicNo, callback) {
    var con = new sqlstock.ConnectionPool(locConfigstock);
    con.connect().then(function success() {
        const request = new sqlstock.Request(con);
        request.input('LIC_NO', LicNo);
        request.execute('SP_GETCOMPTYPEFROMLICNO', function (err, result) {
            if (err) {
                
            }
            else {

                callback(result.recordset);
            }
            con.close();
        });
    }).catch(function error(err) {
    });
};

exports.FillFinYr = function (Status, callback) {
    var con = new sqlstock.ConnectionPool(locConfigstock);
    con.connect().then(function success() {
        const request = new sqlstock.Request(con);
        request.input('STATUS', Status);
        request.execute('STOCK_FILLFINYR', function (err, result) {
            if (err) {
                
            }
            else {

                callback(result.recordset);
            }
            con.close();
        });
    }).catch(function error(err) {
    });
};

exports.FillSeassion = function (FinYr, Status, callback) {
    var con = new sqlstock.ConnectionPool(locConfigstock);
    con.connect().then(function success() {
        const request = new sqlstock.Request(con);
        request.input('FIN_YR', FinYr);
        request.input('STATUS', Status);
        request.execute('STOCK_FILLSEASSION', function (err, result) {
            if (err) {
                
            }
            else {

                callback(result.recordset);
            }
            con.close();
        });
    }).catch(function error(err) {
        
    });
};

exports.FillDealerStockCrop = function (LicNo, FinYr, Seassion, callback) {
    var con = new sqlstock.ConnectionPool(locConfigstock);
    con.connect().then(function success() {
        const request = new sqlstock.Request(con);
        request.input('LIC_NO', LicNo);
        request.input('FIN_YR', FinYr);
        request.input('SEASSION', Seassion);
        request.execute('SP_FILLDEALERSTOCKCROP', function (err, result) {
            if (err) {
                
            }
            else {

                callback(result.recordset);
            }
            con.close();
        });
    }).catch(function error(err) {
        
    });
};

exports.FillDealerStockCropVariety = function (LicNo, FinYr, Seassion, CropCode, callback) {
    var con = new sqlstock.ConnectionPool(locConfigstock);
    con.connect().then(function success() {
        const request = new sqlstock.Request(con);
        request.input('LIC_NO', LicNo);
        request.input('FIN_YR', FinYr);
        request.input('SEASSION', Seassion);
        request.input('CROP_CODE', CropCode);
        request.execute('SP_FILLDEALERSTOCKCROPVARIETY', function (err, result) {
            if (err) {
                
            }
            else {

                callback(result.recordset);
            }
            con.close();
        });
    }).catch(function error(err) {
        
    });
};

exports.FillDealerStock = function (LicNo, FinYr, Seassion, CropCode, VarietyCode, UserType, callback) {
    var con = new sqlstock.ConnectionPool(locConfigstock);
    con.connect().then(function success() {
        const request = new sqlstock.Request(con);
        request.input('LICENCENO', LicNo);
        request.input('F_Year', FinYr);
        request.input('SEASONS', Seassion);
        request.input('CROP_ID', CropCode);
        request.input('CROP_VERID', VarietyCode);
        request.input('USER_TYPE', UserType);
        request.execute('sp_FILLSALEDEALERSALE', function (err, result) {
            if (err) {
                
            }
            else {
                console.log(result.recordset);
                callback(result.recordset);
            }
            con.close();
        });
    }).catch(function error(err) {
        
    });
};

exports.GetFarmerStatus = function (FarmerId, callback) {
    var con = new sqlstock.ConnectionPool(locConfigstock);
    con.connect().then(function success() {
        const request = new sqlstock.Request(con);
        request.input('FARMER_ID', FarmerId);
        request.execute('SP_GETFARMERSTATUS', function (err, result) {
            if (err) {
                
            }
            else {

                callback(result.recordset);
            }
            con.close();
        });
    }).catch(function error(err) {
        
    });
};

exports.GetFarmerInfo = function (FarmerId, callback) {
    var con = new sqlstock.ConnectionPool(locConfigstock);
    con.connect().then(function success() {
        const request = new sqlstock.Request(con);
        request.input('FARMER_ID', FarmerId);
        request.execute('SP_GETFARMERINFO', function (err, result) {
            if (err) {
                
            }
            else {
                callback(result.recordset);
            }
            con.close();
        });
    }).catch(function error(err) {
        
    });
};

exports.GetFarmerRecvCrop = function (FarmerId, FinYear, Season, callback) {
    var con = new sqlstock.ConnectionPool(locConfigstock);
    con.connect().then(function success() {
        const request = new sqlstock.Request(con);
        request.input('FARMER_ID', FarmerId);
        request.input('FIN_YR', FinYear);
        request.input('SEASON', Season);
        request.execute('SP_GETFARMERRECEIVECROP', function (err, result) {
            if (err) {
                
            }
            else {

                callback(result.recordset);
            }
            con.close();
        });
    }).catch(function error(err) {
        
    });
};

exports.GetDealerStockCrop = function (LicNo, FinYear, Season, callback) {
    var con = new sqlstock.ConnectionPool(locConfigstock);
    con.connect().then(function success() {
        const request = new sqlstock.Request(con);
        request.input('LICENCE_NO', LicNo);
        request.input('FIN_YR', FinYear);
        request.input('SEASSION', Season);
        request.execute('GetDealerStockCrop', function (err, result) {
            if (err) {
                
            }
            else {

                callback(result.recordset);
            }
            con.close();
        });
    }).catch(function error(err) {
    });
};

exports.GetDealerStockVariety = function (LicNo, FinYear, Season, CropId, callback) {
    var con = new sqlstock.ConnectionPool(locConfigstock);
    con.connect().then(function success() {
        const request = new sqlstock.Request(con);
        request.input('LICENCE_NO', LicNo);
        request.input('FIN_YR', FinYear);
        request.input('SEASSION', Season);
        request.input('CROP_ID', CropId);
        request.execute('GetDealerStockVariety', function (err, result) {
            if (err) {
                
            }
            else {

                callback(result.recordset);
            }
            con.close();
        });
    }).catch(function error(err) {
        
    });
};

exports.GetDealerStock = function (LicNo, FinYear, Season, CropId, VarietyId, callback) {
    var con = new sqlstock.ConnectionPool(locConfigstock);
    con.connect().then(function success() {
        const request = new sqlstock.Request(con);
        request.input('LICENCENO', LicNo);
        request.input('F_Year', FinYear);
        request.input('SEASONS', Season);
        request.input('CROP_ID', CropId);
        request.input('CROP_VERID', VarietyId);
        request.execute('sp_FILLSALEDEALERSALE', function (err, result) {
            if (err) {
                
            }
            else {

                callback(result.recordset);
            }
            con.close();
        });
    }).catch(function error(err) {
    });
};
// exports.FillCrops = (data) => new Promise(async (resolve, reject) => {
//     try {
//         return sequelizeStock.query(`SELECT C.CROP_CODE,C.CROP_NAME FROM STOCK_DEALERSTOCK A LEFT OUTER JOIN mCROPCATEGORY B ON A.CROPCATG_ID = B.CATEGORY_CODE 
//         LEFT OUTER JOIN mCROP C ON A.CROP_ID = C.CROP_CODE 
//         WHERE A.LICENCE_NO = :LICENCE_NO AND A.FIN_YR = :FIN_YR AND A.SEASSION = 'K' AND C.IS_ACTIVE = 1 AND A.VALIDITY = 1  
//         GROUP BY C.CROP_CODE,C.CROP_NAME 
//         ORDER BY C.CROP_CODE,C.CROP_NAME`,{
//             replacements: {LICENCE_NO: data.LicNo,FIN_YR: data.FIN_YR,SEASSION: data.Seasons}, type: sequelizeStock.QueryTypes.SELECT
//         }).then(function success(data) {
//             return(data.recordset)
//         }).catch(function error(err) {
//             
//         });
//     }  catch (e) {
//         console.log(`Oops! An error occurred: ${e}`);
//     }
// });
exports.FillCrops = function (data) {
    return new Promise(async resolve => {
        try {
            const TargerAchivementReport1 = await sequelizeStock.query(`SELECT C.CROP_CODE,C.CROP_NAME FROM STOCK_DEALERSTOCK A LEFT OUTER JOIN mCROPCATEGORY B ON A.CROPCATG_ID = B.CATEGORY_CODE 
                LEFT OUTER JOIN mCROP C ON A.CROP_ID = C.CROP_CODE 
                WHERE A.LICENCE_NO = :LICENCE_NO AND A.FIN_YR = :FIN_YR AND A.SEASSION = :SEASSION AND C.IS_ACTIVE = 1 AND A.VALIDITY = 1  
                GROUP BY C.CROP_CODE,C.CROP_NAME 
                ORDER BY C.CROP_CODE,C.CROP_NAME`, {
                replacements: { LICENCE_NO: data.LicNo, FIN_YR: data.FIN_YR, SEASSION: data.Seasons }, type: sequelizeStock.QueryTypes.SELECT
            });
            resolve(TargerAchivementReport1);

        } catch (e) {
            console.log('An error occurred...', e);
            resolve([]);
            throw e
        }
    });

};
exports.FillVariety = function (data) {
    return new Promise(async resolve => {
        try {
            const TargerAchivementReport1 = await sequelizeStock.query(`SELECT D.VARIETY_CODE,D.VARIETY_NAME FROM STOCK_DEALERSTOCK A 
                LEFT OUTER JOIN mCROPCATEGORY B ON A.CROPCATG_ID = B.CATEGORY_CODE 
                LEFT OUTER JOIN mCROP C ON A.CROP_ID = C.CROP_CODE 
                LEFT OUTER JOIN mCROPVARIETY D ON A.CROP_VERID = D.VARIETY_CODE 
                WHERE A.LICENCE_NO = :LICENCE_NO AND A.CROP_ID = :CROP_CODE AND A.FIN_YR = :FIN_YR AND A.SEASSION = :SEASSION AND A.AVL_NO_OF_BAGS > 0 AND D.IS_ACTIVE = 1 AND A.VALIDITY = 1 
                GROUP BY D.VARIETY_CODE,D.VARIETY_NAME 
                ORDER BY D.VARIETY_CODE,D.VARIETY_NAME`, {
                replacements: { LICENCE_NO: data.LicNo, FIN_YR: data.FIN_YR, SEASSION: data.Seasons, CROP_CODE: data.Crop }, type: sequelizeStock.QueryTypes.SELECT
            });
            resolve(TargerAchivementReport1);
        } catch (e) {
            console.log('An error occurred...', e);
            resolve([]);
            throw e
        }
    });

};

exports.getPreBookingDetails = function (data) {
    return new Promise(async resolve => {
        try {
            const result = await sequelizeStock.query(`select beneficiaryId,b.Crop_Code,b.Crop_Name,c.Variety_Code,c.Variety_Name,a.preBookingAmt,bagSize,noOfBag,totalCost,CONVERT(DECIMAL(10,2),(SUM(CONVERT(DECIMAL(10,2), A.QUANTITY))/100)) QUANTITY from prebookinglist a
                inner join mCROP b on a.cropCode =b.Crop_Code 
                inner join mCROPVARIETY c on a.varietyCode = c.Variety_Code
                where FIN_YEAR=:FIN_YR and SEASON=:SEASSION and b.IS_ACTIVE=1 and c.IS_ACTIVE=1 and a.IS_ACTIVE=1 and beneficiaryId=:FarmerId and TRANSACTION_ID is null and cancelstatus is null  group by beneficiaryId,b.Crop_Code,b.Crop_Name,c.Variety_Code,c.Variety_Name,a.preBookingAmt,noOfBag,totalCost,bagSize`, {//GAN/141088
                replacements: { FIN_YR: data.FIN_YR, SEASSION: data.SEASSION, FarmerId: data.FarmerId }, type: sequelizeStock.QueryTypes.SELECT
            });
            resolve(result);

        } catch (e) {
            console.log('An error occurred...', e);
            resolve([]);
            throw e
        }
    });

};
exports.GetDistCodeByLicNo = function (data) {
    return new Promise(async resolve => {
        try {
            const result = await sequelizeSeed.query(`SELECT DIST_CODE FROM SEED_LIC_DIST WHERE LIC_NO = :LICENCE_NO`, {//GAN/141088
                replacements: { LICENCE_NO: data.LICENCE_NO }, type: sequelizeStock.QueryTypes.SELECT
            });
            resolve(result[0].DIST_CODE);

        } catch (e) {
            console.log('An error occurred...', e);
            resolve([]);
            throw e
        }
    });

};
exports.GetDAOCodeByLicNo = function (data) {
    return new Promise(async resolve => {
        try {
            const result = await sequelizeSeed.query(`SELECT RIGHT(DAO_CD,2) as daocode FROM SEED_LIC_DIST WHERE LIC_NO = :LICENCE_NO`, {//GAN/141088
                replacements: { LICENCE_NO: data.LICENCE_NO }, type: sequelizeStock.QueryTypes.SELECT
            });
            resolve(result[0].daocode);

        } catch (e) {
            console.log('An error occurred...', e);
            resolve([]);
            throw e
        }
    });

};
exports.InsertSaleDealer = (data) => new Promise(async (resolve, reject) => {
    const toXml = (data) => {
        return data.reduce((result, el) => {
            return result + `<A> 
         <CROP_ID>${el.CROP_ID}</CROP_ID> <Crop_Name>${el.Crop_Name}</Crop_Name>
         <CROP_VERID>${el.CROP_VERID}</CROP_VERID> <Crop_VerName>${el.Crop_VerName}</Crop_VerName>
         <LOT_NO>${el.LOT_NO}</LOT_NO> <Receive_Unitcd>${el.Receive_Unitcd}</Receive_Unitcd>
         <Receive_Unitname>${el.Receive_Unitname}</Receive_Unitname> <BAG_SIZE_KG>${el.BAG_SIZE_KG}</BAG_SIZE_KG>
         <NO_OF_BAGS>${el.NO_OF_BAGS}</NO_OF_BAGS> <QUANTITY>${el.QUANTITY}</QUANTITY>
         <PRICE_QTL>${el.PRICE_QTL}</PRICE_QTL> <SUBSIDY_QTL>${el.SUBSIDY_QTL}</SUBSIDY_QTL>  <AMOUNT>${el.Amount}</AMOUNT>         
         </A>\n`
        }, '');
    }
    var aaa = `<DocumentElement>` + toXml(data.VALUES) + `</DocumentElement>`;
    // console.log(aaa);
    // const toXml =
    //     `<DocumentElement>
    //         <A>
    //             <CROP_ID>C015</CROP_ID>
    //             <Crop_Name>PaddyDhan</Crop_Name>
    //             <CROP_VERID>V217</CROP_VERID>
    //             <Crop_VerName>MTU-7029(SWARNA)</Crop_VerName>
    //             <LOT_NO>Mar/22-18-199-06G77775-1</LOT_NO>
    //             <Receive_Unitcd>124</Receive_Unitcd>
    //             <Receive_Unitname>OSSC</Receive_Unitname>
    //             <BAG_SIZE_KG>4</BAG_SIZE_KG>
    //             <NO_OF_BAGS>1</NO_OF_BAGS>
    //             <QUANTITY>0.04</QUANTITY>
    //             <PRICE_QTL>11197.00</PRICE_QTL>
    //             <SUBSIDY_QTL>2947.00</SUBSIDY_QTL>
    //             <AMOUNT>447.88</AMOUNT>
    //         </A>
    //         <A>
    //         <CROP_ID>C015</CROP_ID>
    //         <Crop_Name>PaddyDhan</Crop_Name>
    //         <CROP_VERID>V217</CROP_VERID>
    //         <Crop_VerName>MTU-7029(SWARNA)</Crop_VerName>
    //         <LOT_NO>Mar/22-18-199-06G77775-1</LOT_NO>
    //         <Receive_Unitcd>124</Receive_Unitcd>
    //         <Receive_Unitname>OSSC</Receive_Unitname>
    //         <BAG_SIZE_KG>4</BAG_SIZE_KG>
    //             <NO_OF_BAGS>2</NO_OF_BAGS>
    //             <QUANTITY>0.08</QUANTITY>
    //             <PRICE_QTL>11197.00</PRICE_QTL>
    //             <SUBSIDY_QTL>2947.00</SUBSIDY_QTL>
    //             <AMOUNT>895.76</AMOUNT>
    //         </A>
    //     </DocumentElement>`
    var con = new sqlstock.ConnectionPool(locConfigstock);
    try {
        con.connect().then(function success() {
            const request = new sqlstock.Request(con);
            request.input('FARMER_ID', data.FARMER_ID);
            request.input('DIST_CODE', parseInt(data.DIST_CODE));
            request.input('DAO_CD', data.DAO_CD);
            request.input('LICENCE_NO', data.LICENCE_NO);
            request.input('SEASON', 'R');
            request.input('FINYR', data.FINYR);
            request.input('VALUES', aaa);
            request.input('UPDATED_BY', data.UPDATED_BY);
            request.input('USERIP', data.USERIP);
            request.output('TRANSACTION_ID');
            request.output('Val');


            request.execute('Stock_SP_InsDealerSale', function (err, result) {
                if (err) {
                    
                }
                else {
                    resolve(result.output);
                }
                con.close();
            });
        }).catch(function error(err) {
            
        });
    } catch (e) {
        console.log(`Oops! An error occurred: ${e}`);
    }
})











// select * from mCrop where Crop_Name like '%Gram%'
// select * from mCrop where Crop_Name like '%Black%'

// select * from mCropVariety where Crop_Code like '%C011%'  order by Variety_Name



// select * from [dbo].[STOCK_FARMER_2021-22_R] where CROP_VERID='V385' LOT_NUMBER=''


// select * from mCrop where Crop_Code in ('C015','C011','C021','','C027','C033','C026','C005')


// select * from mCropVariety where Variety_Code in ('V217','V437',   'V941',  'V474',   '',  'V977','V406'  ,'V386',  'V387','V513','V424','V457')




//////////////////////////////////////////////////////////////////// postgressql///////////////////////////////



exports.getStockReceivedDat4543 = function (data) {
    return new Promise(async resolve => {
        try {
            const result = await sequelizeStock.query(`SELECT B.Crop_Name,Variety_Name,CONVERT(VARCHAR(50),SUM(TOT_QTL))+' Qtl'TOT_QTL FROM [STOCK].[DBO].[STOCK_FARMER_2021-22_R] A LEFT OUTER JOIN [STOCK].[DBO].[mCrop] B ON A.CROP_ID = B.Crop_Code
                inner join mCROPVARIETY c on a.CROP_VERID = c.Variety_Code
                where FIN_YEAR=:FIN_YR and SEASON=:SEASSION and b.IS_ACTIVE=1 and c.IS_ACTIVE=1 and FARMER_ID=:FarmerId GROUP BY B.Crop_Name,c.Variety_Name`, {
                //GAN/104345
                replacements: { FIN_YR: data.FIN_YR, SEASSION: data.SEASSION, FarmerId: data.FarmerId }, type: sequelizeStock.QueryTypes.SELECT
            });
            resolve(result);

        } catch (e) {
            console.log('An error occurred...', e);
            resolve([]);
            throw e
        }
    });

};
