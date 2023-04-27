const Sequelize = require('sequelize');
// const sequelize = new Sequelize('seedsportal_o', 'seedsportal', 'SeeDSoDiSha!12#$', {
//     host: '10.242.25.15',
//     dialect: 'mssql'
// });

//laptop
const sequelize = new Sequelize('seedsportal_o', 'sa', 'sa@123#', {host: 'localhost', dialect: 'mssql' });
const sequelizeSeedLive = new Sequelize('dafpseed', 'seed', 'Q@W#E$R%T^YU*I(', {host: '164.100.140.101',dialect: 'mssql'});
const sequelizeSeed = new Sequelize('dafpseed', 'sa', 'sa@123#', {host: 'localhost',dialect: 'mssql'});
const sequelizeStockLive = new Sequelize('stock', 'stock', '!@sToCk5$5ST', {host: '164.100.140.101',dialect: 'mssql'});
const sequelizeOssopoca = new Sequelize('OSSOPCA', 'ossopca', 'A*7Pc#90!2A', { host: '164.100.140.101', dialect: 'mssql' });
const sequelizeStock = new Sequelize('stock', 'public_database', 'sa@123#', {host: '10.172.31.116',dialect: 'mssql'});   
// const sequelizeStock = new Sequelize('stock', 'sa', 'sa@123#', {host: 'localhost',dialect: 'mssql'});   
// const sequelizeStock = new Sequelize('stock', 'stock', '!@sToCk5$5ST', {host: '164.100.140.101',dialect: 'mssql'}); 

//desktop
// const sequelize = new Sequelize('seedsportal_o', 'sa', '1234', { host: 'localhost', dialect: 'mssql' });
// const sequelizeSeedLive = new Sequelize('dafpseed', 'seed', 'Q@W#E$R%T^YU*I(', { host: '164.100.140.101', dialect: 'mssql' });
// const sequelizeSeed = new Sequelize('dafpseed', 'sa', '1234', { host: 'localhost', dialect: 'mssql' });
// const sequelizeStockLive = new Sequelize('stock', 'stock', '!@sToCk5$5ST', { host: '164.100.140.101', dialect: 'mssql' });
// const sequelizeStock = new Sequelize('stock', 'sa', '1234', { host: 'localhost', dialect: 'mssql' });
// const sequelizeOssopoca = new Sequelize('OSSOPCA', 'ossopca', 'A*7Pc#90!2A', { host: '164.100.140.101', dialect: 'mssql' });

const sql = require('mssql');
const sqlseed = require('mssql');
const sqlstock = require('mssql');
//  const locConfig = {
//      user: 'seedsportal',
//      password: 'SeeDSoDiSha!12#$',
//      server: '10.242.25.15',
//      database: 'seedsportal_o',
//      requestTimeout: 3600000
//  };
//164.100.140.101
//10.172.0.101

//laptop
const locConfig = { user: 'sa', password: 'sa@123#', server: 'localhost', database: 'seedsportal_o', requestTimeout: 3600000 };
const locConfigSeedLive = { user: 'seed', password: 'Q@W#E$R%T^YU*I(', server: '164.100.140.101', database: 'dafpseed', requestTimeout: 3600000 };
const locConfigSeed = { user: 'sa', password: 'sa@123#', server: 'localhost', database: 'dafpSeed', requestTimeout: 3600000 };
const locConfigStockLive = { user: 'stock', password: '!@sToCk5$5ST', server: '164.100.140.101', database: 'stock', requestTimeout: 3600000 };
const locConfigOssopoca = { user: 'ossopca', password: 'A*7Pc#90!2A', server: '164.100.140.101', database: 'OSSOPCA', requestTimeout: 3600000 };
// const locConfigStock = { user: 'sa', password: 'sa@123#', server: '10.172.31.116', database: 'stock', requestTimeout: 3600000 };
// const locConfigStock = { user: 'sa', password: 'sa@123#', server: 'localhost', database: 'stock', requestTimeout: 3600000 };
const locConfigStock = { user: 'stock', password: '!@sToCk5$5ST', server: '164.100.140.101', database: 'stock', requestTimeout: 3600000 };
//desktop
// const locConfig = { user: 'sa', password: '1234', server: 'localhost', database: 'seedsportal_o', requestTimeout: 3600000 };
// const locConfigSeedLive = { user: 'seed', password: 'Q@W#E$R%T^YU*I(', server: '164.100.140.101', database: 'dafpseed', requestTimeout: 3600000 };
// const locConfigSeed = { user: 'sa', password: '1234', server: 'localhost', database: 'dafpSeed', requestTimeout: 3600000 };
// const locConfigStockLive = { user: 'stock', password: '!@sToCk5$5ST', server: '164.100.140.101', database: 'stock', requestTimeout: 3600000 };
// const locConfigStock = { user: 'sa', password: '1234', server: 'localhost', database: 'stock', requestTimeout: 3600000 };
// const locConfigOssopoca = { user: 'ossopca', password: 'A*7Pc#90!2A', server: '164.100.140.101', database: 'OSSOPCA', requestTimeout: 3600000 };


//const sql = require('mssql');
//const locConfig = {
//    user: 'seedsportal',
//    password: 'SeeDSoDiSha!12#$',
//    server: '10.242.25.15',
//    database: 'seedsportal_o'
//};

sequelize
    .authenticate()
    .then(function success() {
    }).catch(function error(err) {
        console.log('Unable to connect to the database seedsportal_o: ' + err);
    });

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
        console.log('Unable to connect to the database stock: ' + err);
    });
sequelizeOssopoca
    .authenticate()
    .then(function success() {
    }).catch(function error(err) {
        console.log('Unable to connect to the database Ossopoca: ' + err);
    });
exports.sequelize = sequelize;
exports.sequelizeSeed = sequelizeSeed;
exports.sequelizeStock = sequelizeStock;
exports.sequelizeOssopoca = sequelizeOssopoca;

exports.sql = sql;
exports.sqlseed = sqlseed;
exports.sqlstock = sqlstock;
exports.locConfig = locConfig;
exports.locConfigSeed = locConfigSeed;
exports.locConfigStock = locConfigStock;
exports.locConfigStockLive = locConfigStockLive;
exports.locConfigOssopoca = locConfigOssopoca;

