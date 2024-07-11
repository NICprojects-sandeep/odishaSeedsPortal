// middleware for doing role-based permissions
exports.permission = function (...allowed) {
    const isAllowed = role => allowed.indexOf(role) > -1;
    // return a middleware
    return (req, res, next) => {
        console.log(req.session.username , (req.session.role));
        if (req.session.username && isAllowed(req.session.role)) {
            next(); // role is allowed, so continue on the next middleware
        }
        else {
            req.session.destroy();
            res.redirect('https://odishaseedsportal.nic.in/users/#/');
            // res.status(403).json({message: "Forbidden"}); // user is forbidden
        }
    }
};