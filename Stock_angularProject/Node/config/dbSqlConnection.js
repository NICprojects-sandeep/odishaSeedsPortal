const Sequelize = require('sequelize');
// const sequelizeSeedLive = new Sequelize('dafpseed', 'seed', 'Q@W#E$R%T^YU*I(', {host: '164.100.140.101',dialect: 'mssql'});
const sequelizeSeed = new Sequelize('dafpseed', 'sa', 'sa@123#', { host: 'localhost', dialect: 'mssql' });
const sequelizeStock = new Sequelize('stock', 'sa', 'sa@123#', {host: '10.172.31.116',dialect: 'mssql'}); 


const locConfigStock = { user: 'sa', password: 'sa@123#', server: '10.172.31.116', database: 'stock', requestTimeout: 3600000 };


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

exports.sequelizeSeed = sequelizeSeed;
exports.sequelizeStock = sequelizeStock;
exports.locConfigStock = locConfigStock;


