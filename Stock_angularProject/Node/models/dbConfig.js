const Sequelize = require('sequelize');
const atob = require('atob');

const { dbCredentialofStock } = require('../config/dbSqlCredentials');
const { dbCredentialofseedsportal } = require('../config/dbSqlCredentials');
const { dbCredentialofSeed } = require('../config/dbSqlCredentials');
const { dbCredentialofOssopoca } = require('../config/dbSqlCredentials');
const { dbCredentialofFarmerDB } = require('../config/dbSqlCredentials');


const stock = dbCredentialofStock();
const seedsportal = dbCredentialofseedsportal();
const dafpseed = dbCredentialofSeed();
const Ossopoca = dbCredentialofOssopoca();
const FARMERDB = dbCredentialofFarmerDB();


const sequelize = new Sequelize(atob(seedsportal.enCodedDatabase), atob(seedsportal.encodedUser), atob(seedsportal.encodedPassword), { host: atob(seedsportal.encodedHost), dialect: 'mssql', logging: false, pool: { max: 500, min: 0, acquire: 90000, idle: 10000 } });
const sequelizeSeed = new Sequelize(atob(dafpseed.enCodedDatabase), atob(dafpseed.encodedUser), atob(dafpseed.encodedPassword), { host: atob(stock.encodedHost), dialect: 'mssql', logging: false, pool: { max: 500, min: 0, acquire: 90000, idle: 10000 } });
const sequelizeOssopoca = new Sequelize(atob(Ossopoca.enCodedDatabase), atob(Ossopoca.encodedUser), atob(Ossopoca.encodedPassword), { host: atob(Ossopoca.encodedHost), dialect: 'mssql', logging: false, pool: { max: 500, min: 0, acquire: 90000, idle: 10000 } });
const sequelizeFarmerDB = new Sequelize(atob(FARMERDB.enCodedDatabase), atob(FARMERDB.encodedUser), atob(FARMERDB.encodedPassword), { host: atob(FARMERDB.encodedHost), dialect: 'mssql', logging: false, pool: { max: 500, min: 0, acquire: 90000, idle: 10000 } });
const sequelizeStock = new Sequelize(atob(stock.enCodedDatabase), atob(stock.encodedUser), atob(stock.encodedPassword), { host: atob(stock.encodedHost), dialect: 'mssql', logging: false, pool: { max: 500, min: 0, acquire: 90000, idle: 10000 } });

const sql = require('mssql');
const sqlseed = require('mssql');
const sqlstock = require('mssql');

const locConfig = {
    user: atob(seedsportal.encodedUser), password: atob(seedsportal.encodedPassword), server: atob(seedsportal.encodedHost), database: atob(seedsportal.enCodedDatabase), requestTimeout: 3600000,
    options: {
        trustServerCertificate: true,
        enableArithAbort: true
    }
};

const locConfigSeed = {
    user: atob(dafpseed.encodedUser), password: atob(dafpseed.encodedPassword), server: atob(dafpseed.encodedHost), database: atob(dafpseed.enCodedDatabase), requestTimeout: 3600000,
    options: {
        trustServerCertificate: true,
        enableArithAbort: true
    }
};
const locConfigStockLive = {
    user: atob(stock.encodedUser), password: atob(stock.encodedPassword), server: atob(stock.encodedHost), database: atob(stock.enCodedDatabase), requestTimeout: 3600000,
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
const locConfigFarmerDB = {
    user: atob(FARMERDB.encodedUser), password: atob(FARMERDB.encodedPassword), server: atob(FARMERDB.encodedHost), database: atob(FARMERDB.enCodedDatabase), requestTimeout: 3600000,
    options: {
        trustServerCertificate: true,
        enableArithAbort: true
    }
};
const locConfigStock = {
    user: atob(stock.encodedUser), password: atob(stock.encodedPassword), server: atob(stock.encodedHost), database: atob(stock.enCodedDatabase), requestTimeout: 3600000,
    options: {
        trustServerCertificate: true,
        enableArithAbort: true
    }
};


sequelize
    .authenticate()
    .then(function success() {
    }).catch(function error(err) {
    });

sequelizeSeed
    .authenticate()
    .then(function success() {
    }).catch(function error(err) {
    });

sequelizeStock
    .authenticate()
    .then(function success() {
    }).catch(function error(err) {
    });
sequelizeOssopoca
    .authenticate()
    .then(function success() {
    }).catch(function error(err) {
    });

sequelizeFarmerDB
    .authenticate()
    .then(function success() {
    }).catch(function error(err) {
    });
exports.sequelize = sequelize;
exports.sequelizeSeed = sequelizeSeed;
exports.sequelizeStock = sequelizeStock;
exports.sequelizeOssopoca = sequelizeOssopoca;
exports.sequelizeFarmerDB = sequelizeFarmerDB;

exports.sql = sql;
exports.sqlseed = sqlseed;
exports.sqlstock = sqlstock;
exports.locConfig = locConfig;
exports.locConfigSeed = locConfigSeed;
exports.locConfigStock = locConfigStock;
exports.locConfigStockLive = locConfigStockLive;
exports.locConfigOssopoca = locConfigOssopoca;
exports.locConfigFarmerDB = locConfigFarmerDB;