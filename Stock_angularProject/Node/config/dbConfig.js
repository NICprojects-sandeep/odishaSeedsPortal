const { Pool } = require('pg');
const atob = require('atob');
const { dbCredentials } = require('./dbCredentials');

const dbc = dbCredentials();

const pool = new Pool({
  user: atob(dbc.encodedUser),
  host: atob(dbc.encodedHost),
  database: atob(dbc.enCodedDatabase),
  password: atob(dbc.encodedPassword),
  port: atob(dbc.encodedPort),
  pool:{
    max:500,
    min:0,
    acquire:90000,
    idle:10000
  },
  connectionTimeout:3600000
});

module.exports = pool;