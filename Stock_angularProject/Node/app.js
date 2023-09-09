// var createError = require('http-errors');
// var express = require('express');
// var path = require('path');
// var cookieParser = require('cookie-parser');
// var logger = require('morgan');
// var cors = require('cors');
// var jsonpars = require('body-parser');
// const session = require('express-session');
// const crypto = require('crypto');
// var homeRouter = require('./routes/osp/home');
// var stockRouter = require('./routes/stock/farmersale');
// const authRouter = require('./routes/auth');
// const ejs = require('ejs');
// var app = express();
// const sessionOptions = {
//   secret: crypto.randomBytes(64).toString('hex'),
//   resave: false,
//   saveUninitialized: false,
//   cookie: {
//     path: '/',
//     httpOnly: true,
//     secure: 'auto',
//     sameSite: true

//   },
//   genid() {
//     return uuidv4();
//   }
// };

// app.use(session(sessionOptions));
// app.use(logger('dev'));
// app.use(express.json());
// app.use(express.urlencoded({ extended: false }));
// app.use(cookieParser());
// app.use(express.static(path.join(__dirname, 'public')));
// app.use('/osp', express.static(path.join(__dirname, 'HomePageApp')));
// app.use('/users', express.static(path.join(__dirname, 'clientApp')));
// app.use('/assets', express.static('assets'));
// app.use(cors());

// app.use('/osp/home', homeRouter);
// app.use('/stock/home', stockRouter);
// app.use('/auth', authRouter);
// const publicRouter = require('./routes/stock/public');
// app.use('/publicR', publicRouter);

// app.use(function (req, res, next) {
//   next(createError(404));
// });
// const corsOptions = {
//   origin: ['http://localhost:3600', 'http://localhost:4200', 'http://localhost:4000'],
//   methods: 'GET, POST',
//   credentials: true
// };
// app.set('views', path.join(__dirname, 'views'));
// app.set('view engine', 'ejs');
// app.use(cors(corsOptions));
// app.use((err, req, res, next) => {
//   res.locals.message = err.message;
//   res.locals.error = req.app.get('env') === 'development' ? err : {
//   };
//   res.status(err.status || 500);
//   res.json({
//     message: err.message,
//     error: err
//   });
// });
// module.exports = app;
// app.listen(3000);


const createError = require('http-errors');
const express = require('express');
const path = require('path');
const fse = require('fs-extra');
const cookieParser = require('cookie-parser');
const logger = require('morgan');
const session = require('express-session');
const crypto = require('crypto');
const { v4: uuidv4 } = require('uuid');
const xFrameOptions = require('x-frame-options');
const xXssProtection = require("x-xss-protection");
const sts = require('strict-transport-security');
const csp = require('content-security-policy');
const device = require('express-device');
const cors = require('cors');
const bodyParser = require('body-parser');
const debug = require('debug')('server:server');
const http = require('http');
const favicon = require('serve-favicon');
var ejs = require('ejs');
let helmet = require("helmet");
const cache = require('cache-headers');
const format = require('biguint-format');
const {verifyAccessToken} = require('./helpers/jwt.helper')
const overrideConfig = {
  "maxAge": 2000,
  "setPrivate": true,
  "staleError": "number",
  "staleRevalidate": "number",
  "lastModified": "string",
};
var csrf = require('csurf');
var csrfProtection = csrf();
var parseForm = bodyParser.urlencoded({ extended: false });
sha512 = require('js-sha512');
const localSTS = sts.getSTS({ 'max-age': { 'days': 10 }, 'includeSubDomains': true });
const cspPolicy = {
  // 'report-uri': '/reporting',
  // 'default-src': csp.SRC_NONE,
  // 'script-src': [ csp.SRC_SELF, csp.SRC_DATA ]
};

const localCSP = csp.getCSP(cspPolicy);
var homeRouter = require('./routes/osp/home');
var stockRouter = require('./routes/stock/farmersale');
const authRouter = require('./routes/auth');
const publicRouter = require('./routes/stock/public');
const aaoRouter = require('./routes/stock/aao');
const dealerRouter = require('./routes/stock/dealer');


const app = express();

const sessionOptions = {
  secret: crypto.randomBytes(64).toString('hex'),
  resave: false,
  saveUninitialized: false,
  // saveUninitialized: true,
  cookie: {
    path: '/',
    httpOnly: true,
    secure: 'auto',
    sameSite: true
    // ,maxAge: 1800000
    // ,maxAge: 60000

  },
  genid() {
    return uuidv4();
  }
};

app.use(session(sessionOptions));

const corsOptions = {
  origin: ['http://localhost:3600', 'http://localhost:4200', 'http://localhost:4300','http://10.172.31.116:4300','http://10.172.31.116:3000','http://10.172.31.116/:3000/osp/home/dealerwisedata'],
  methods: 'GET, POST',
  credentials: true
};

app.use(cors(corsOptions));
app.use(xFrameOptions());
app.use(xXssProtection());
app.use(localSTS);
// app.use(localCSP);
app.use(device.capture());

app.use(express.json({
  limit: '100mb'
}));
app.use(express.urlencoded({
  limit: '100mb', extended: true
}));
app.use(bodyParser.json({ limit: '100mb' }));
app.use(bodyParser.urlencoded({ limit: '100mb', extended: true }));
app.disable('x-powered-by');

// static paths
// app.use('/public', express.static('public'));
// app.use(express.static(path.join(__dirname ,'controllers')));
// app.use('/osp', express.static( 'HomePageApp'));
app.use(  express.static(path.join(__dirname, 'HomePageApp')));
app.use('/users', express.static(path.join(__dirname, 'clientApp')));
app.set('views', path.join(__dirname, 'views'));
app.set('view engine', 'ejs');

app.use(logger('dev'));
app.use(express.json());
app.use(express.urlencoded({
  extended: false
}));
app.use(cookieParser(crypto.randomBytes(64).toString('hex')));
app.use(express.static(path.join(__dirname, 'public')));
app.use(favicon(path.join(__dirname, 'favicon.ico')));

app.use('/osp/home', homeRouter);
app.use('/stock/home', stockRouter);
app.use('/auth', authRouter);
app.use('/publicR', publicRouter);
app.use('/aao', aaoRouter);
app.use('/dealer',dealerRouter);


app.get('*', function (req, res) {
  
  res.render('errorpage');
});
app.use(helmet.hidePoweredBy());
app.use((req, res, next) => {
  next(createError(404));
});
app.use(function(req, res, next) {
  res.setHeader("Content-Security-Policy", "script-src 'self' https://apis.google.com");
  return next();
});
app.use((err, req, res, next) => {
  // set locals, only providing error in development
  res.locals.message = err.message;
  res.locals.error = req.app.get('env') === 'development' ? err : {
  };
  // render the error page
  res.status(err.status || 500);
  // res.render('error');
  res.json({
    message: err.message,
    error: err
  });
});
const server = http.createServer(app);
function normalizePort(val) {
  const port = parseInt(val, 10);
  if (Number.isNaN(port)) {
    // named pipe
    return val;
  }
  if (port >= 0) {
    // port number
    return port;
  }
  return false;
}
const port = normalizePort(process.env.PORT || '3000');
app.set('port', port);
function onError(error) {
  if (error.syscall !== 'listen') {
    throw error;
  }
  const bind = typeof port === 'string'
    ? `Pipe ${port}`
    : `Port ${port}`;
  // handle specific listen errors with friendly messages
  switch (error.code) {
    case 'EACCES':
      console.error(`${bind} requires elevated privileges`);
      process.exit(1);
      break;
    case 'EADDRINUSE':
      console.error(`${bind} is already in use`);
      process.exit(1);
      break;
    default:
      throw error;
  }
}
function onListening() {
  const addr = server.address();
  const bind = typeof addr === 'string'
    ? `pipe ${addr}`
    : `port ${addr.port}`;
  debug(`Listening on ${bind}`);
}
server.listen(port,
  console.log('server running on ' + port)
);
server.on('error', onError);
server.on('listening', onListening);
process.env.ACCESS_TOKEN_SECRET = 'Hello world'

module.exports = app;