var config = require("../config");
var sha1 = require("sha1");


exports.IsTestingContext = function(req) {
    return req.get("x-ctx") !== undefined; 
};

exports.CheckRequestHeaders = function(req, res, next) {
    if (req.get("x-lpl") !== undefined && req.get("x-ts") !== undefined && req.get("x-part") !== undefined) {
        var requestHash = req.get("x-lpl");
        var timeStamp = req.get("x-ts");
        var partID = req.get("x-part");

        var expectedHash = sha1(`${partID}+${timeStamp}+${config.values.CODE_SECRET}`);

        if (expectedHash != requestHash)
            res.status(401).end();
        else
            next();
    }

    res.status(401).end("Unauthorized");
};


exports.AddResponseHeaders = function(res) {
    var timeStamp = Date.now() / 1000 | 0;
        
    res.set({
        'X-LPL': sha1(`${config.values.PARTENAIRE_ID}+${timeStamp}+${config.values.CODE_SECRET}`),
        'X-PART': config.values.PARTENAIRE_ID,
        'X-TS': timeStamp,
    });
};


exports.GetJsonFromRequest = function(crypt, reqValue) {
    var json = crypt.rijndael256Decrypt(config.values.AES_KEY, config.values.IV, reqValue);
    json = json.replace(/[\x00-\x1F\x80-\xFF]+/, "");
    return JSON.parse(json);
};