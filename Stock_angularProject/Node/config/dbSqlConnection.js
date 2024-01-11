const Sequelize = require('sequelize');
const sqlstock = require('mssql');
const atob = require('atob');
const { dbCredentialofStock } = require('./dbSqlCredentials');
const { dbCredentialofSeed } = require('./dbSqlCredentials');
const { dbCredentialofFarmerDB } = require('./dbSqlCredentials');
const { dbCredentialofOssopoca } = require('./dbSqlCredentials');
const { dbCredentialofAuth } = require('./dbSqlCredentials');





const stock = dbCredentialofStock();
const dafpseed =  dbCredentialofSeed();
const FARMERDB = dbCredentialofFarmerDB();
const Ossopoca = dbCredentialofOssopoca();
const Auth = dbCredentialofAuth();





const sequelizeSeed = new Sequelize(atob(stock.enCodedDatabase), atob(stock.encodedUser), atob(stock.encodedPassword), { host: atob(stock.encodedHost), dialect: 'mssql', logging: false, pool: { max: 500, min: 0, acquire: 90000, idle: 10000 } });
const sequelizeStock = new Sequelize(atob(dafpseed.enCodedDatabase), atob(dafpseed.encodedUser), atob(dafpseed.encodedPassword), { host: atob(stock.encodedHost), dialect: 'mssql', logging: false, pool: { max: 500, min: 0, acquire: 90000, idle: 10000 } });
const sequelizeFarmerDB = new Sequelize(atob(FARMERDB.enCodedDatabase), atob(FARMERDB.encodedUser), atob(FARMERDB.encodedPassword), { host: atob(FARMERDB.encodedHost), dialect: 'mssql', logging: false, pool: { max: 500, min: 0, acquire: 90000, idle: 10000 } });
const sequelizeOssopoca = new Sequelize(atob(Ossopoca.enCodedDatabase), atob(Ossopoca.encodedUser), atob(Ossopoca.encodedPassword), { host: atob(Ossopoca.encodedHost), dialect: 'mssql', logging: false, pool: { max: 500, min: 0, acquire: 90000, idle: 10000 } });

const locConfigStock = {
    user: atob(stock.encodedUser), password: atob(stock.encodedPassword), server: atob(stock.encodedHost), database: atob(stock.enCodedDatabase), requestTimeout: 3600000,
    options: {
        trustServerCertificate: true,
        enableArithAbort: true
    }
};
const locConfigdafpSeeds = {
    user: atob(dafpseed.encodedUser), password: atob(dafpseed.encodedPassword), server: atob(dafpseed.encodedHost), database: atob(dafpseed.enCodedDatabase), requestTimeout: 3600000,
    options: {
        trustServerCertificate: true,
        enableArithAbort: true
    }
};
const locConfigAuth = {
    user: atob(Auth.encodedUser), password: atob(Auth.encodedPassword), server: atob(Auth.encodedHost), database: atob(Auth.enCodedDatabase), requestTimeout: 3600000,
    options: {
        trustServerCertificate: true,
        enableArithAbort: true
    }
};
const locConfigfarmerDB = {
    user: atob(FARMERDB.encodedUser), password: atob(FARMERDB.encodedPassword), server: atob(FARMERDB.encodedHost), database: atob(FARMERDB.enCodedDatabase), requestTimeout: 3600000,
    options: {
        trustServerCertificate: true,
        enableArithAbort: true
    }
};
const locConfigOssopoca = {
    user: atob(Ossopoca.encodedUser), password: atob(Ossopoca.encodedPassword), server: atob(Ossopoca.encodedHost), database: atob(Ossopoca.enCodedDatabase), requestTimeout: 3600000,
    options: {
        trustServerCertificate: true,
        enableArithAbort: true
    }
};


sequelizeSeed
    .authenticate()
    .then(function success() {
    }).catch(function error(err) {
        console.log('Unable to connect to the database dafpseed: ' + err);
    });
sequelizeStock
    .authenticate()
    .then(function success() {
    }).catch(function error(err) {
        console.log('Unable to connect to the database sql stock: ' + err);
    });

sequelizeFarmerDB
    .authenticate()
    .then(function success() {
    }).catch(function error(err) {
        console.log('Unable to connect to the database FarmerDB: ' + err);
    });
exports.sqlstock = sqlstock;
exports.sequelizeSeed = sequelizeSeed;
exports.sequelizeStock = sequelizeStock;
exports.locConfigStock = locConfigStock;
exports.locConfigAuth = locConfigAuth;
exports.sequelizeFarmerDB = sequelizeFarmerDB;
exports.sequelizeOssopoca = sequelizeOssopoca;

exports.locConfigdafpSeeds = locConfigdafpSeeds;
exports.locConfigFarmerDB = locConfigfarmerDB;
exports.locConfigOssopoca = locConfigOssopoca;




