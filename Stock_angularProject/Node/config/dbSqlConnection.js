const Sequelize = require('sequelize');
const sqlstock = require('mssql');
// const sequelizeSeed = new Sequelize('dafpseed', 'seed', 'Q@W#E$R%T^YU*I(', {host: '10.172.0.101',dialect: 'mssql'});
// const sequelizeStock = new Sequelize('stock', 'stock', '!@sToCk5$5ST', {host: '10.172.0.101',dialect: 'mssql'}); 

const sequelizeSeed = new Sequelize('dafpseed', 'sa', 'sa@123#', { host: 'localhost', dialect: 'mssql' });
const sequelizeStock = new Sequelize('stock', 'sa', 'sa@123#', {host: 'localhost',dialect: 'mssql'}); 


const locConfigStock = { user: 'sa', password: 'sa@123#', server: 'localhost', database: 'stock', requestTimeout: 3600000 };
const locConfigdafpSeeds = { user: 'sa', password: 'sa@123#', server: 'localhost', database: 'dafpseed', requestTimeout: 3600000 };
const locConfigAuth = { user: 'sa', password: 'sa@123#', server: 'localhost', database: 'AuthenticationDB', requestTimeout: 3600000 };


// const locConfigStock = { user: 'sa', password: '!@sToCk5$5ST', server: '10.172.0.101', database: 'stock', requestTimeout: 3600000 };
// const locConfigdafpSeeds = { user: 'sa', password: 'Q@W#E$R%T^YU*I(', server: '10.172.0.101', database: 'dafpseed', requestTimeout: 3600000 };
// const locConfigAuth = { user: 'sa', password: 'Auth@Db#123', server: '10.172.0.101', database: 'AuthenticationDB', requestTimeout: 3600000 };



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

exports.sqlstock = sqlstock;
exports.sequelizeSeed = sequelizeSeed;
exports.sequelizeStock = sequelizeStock;
exports.locConfigStock = locConfigStock;
exports.locConfigAuth = locConfigAuth;



