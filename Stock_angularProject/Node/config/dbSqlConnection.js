const Sequelize = require('sequelize');
const sqlstock = require('mssql');
// const sequelizeSeed = new Sequelize('dafpseed', 'seed', 'Q@W#E$R%T^YU*I(', {host: '10.172.0.101',dialect: 'mssql'});
// const sequelizeStock = new Sequelize('stock', 'stock', '!@sToCk5$5ST', {host: '10.172.0.101',dialect: 'mssql'}); 
// const sequelizeFarmerDB = new Sequelize('FARMERDB', 'FARMER', 'faRmeR@DB@13', { host: '10.172.0.101', dialect: 'mssql' });
// const locConfigStock = { user: 'stock', password: '!@sToCk5$5ST', server: '10.172.0.101', database: 'stock', requestTimeout: 3600000 };
// const locConfigdafpSeeds = { user: 'seed', password: 'Q@W#E$R%T^YU*I(', server: '10.172.0.101', database: 'dafpseed', requestTimeout: 3600000 };
// const locConfigAuth = { user: 'authdb', password: 'Auth@Db#123', server: '10.172.0.101', database: 'AuthenticationDB', requestTimeout: 3600000 };
// const locConfigfarmerDB = { user: 'FARMER', password: 'faRmeR@DB@13', server: '10.172.0.101', database: 'FARMERDB', requestTimeout: 3600000 };



const sequelizeSeed = new Sequelize('dafpseed', 'sa', 'sa@123#', { host: 'localhost', dialect: 'mssql' });
const sequelizeStock = new Sequelize('stock', 'sa', 'sa@123#', { host: 'localhost', dialect: 'mssql' });
const sequelizeFarmerDB = new Sequelize('FARMERDB', 'sa', 'sa@123#', { host: 'localhost', dialect: 'mssql' });
// const sequelizeFarmerDB = new Sequelize('FARMERDB', 'FARMER', 'faRmeR@DB@13', { host: '10.172.0.101', dialect: 'mssql' });
const sequelizeOssopoca = new Sequelize('OSSOPCA', 'ossopca', 'A*7Pc#90!2A', { host: '164.100.140.101', dialect: 'mssql' });

const locConfigStock = {
    user: 'sa', password: 'sa@123#', server: 'localhost', database: 'stock', requestTimeout: 3600000,
    options: {
        trustServerCertificate: true,
        enableArithAbort: true // or `false`. Just set it explicitly to silence the warning message
    }
};
const locConfigdafpSeeds = {
    user: 'sa', password: 'sa@123#', server: 'localhost', database: 'dafpseed', requestTimeout: 3600000,
    options: {
        trustServerCertificate: true,
        enableArithAbort: true // or `false`. Just set it explicitly to silence the warning message
    }
};
const locConfigAuth = {
    user: 'sa', password: 'sa@123#', server: 'localhost', database: 'AuthenticationDB', requestTimeout: 3600000,
    options: {
        trustServerCertificate: true,
        enableArithAbort: true // or `false`. Just set it explicitly to silence the warning message
    }
};
const locConfigfarmerDB = {
    user: 'sa', password: 'sa@123#', server: 'localhost', database: 'FARMERDB', requestTimeout: 3600000,
    options: {
        trustServerCertificate: true,
        enableArithAbort: true // or `false`. Just set it explicitly to silence the warning message
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



