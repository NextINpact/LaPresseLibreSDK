/**
 * @author Bastien Caubet <bastien@nextinpact.com>, Luc Raymond <luc@nextinpact.com>
 */

var config = require("../config");
var sha1 = require("sha1");

/**
 * Vérifie le contexte de la requête
 * Si le header x-ctx est présent, la requête sert à verifier l'état du web-service
 * @param req
 * @return bool
 */
exports.IsTestingContext = function(req) {
    return req.get("x-ctx") !== undefined; 
};



/**
 * Vérification des informations contenus dans le header
 * Permet de valider la transaction après comparaison du hachage SHA1 entre les valeurs contenues dans les headers X-PART et X-TS plus le code secret et la valeur du header X-LPL
 * @param req
 * @param res
 * @param next
 * @return bool
 */
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


/**
 * Ajout des headers LPL à la réponse
 * @param res
 * @return 
 */
exports.AddResponseHeaders = function(res) {
    var timeStamp = Date.now() / 1000 | 0;
        
    res.set({
        'X-LPL': sha1(`${config.values.PARTENAIRE_ID}+${timeStamp}+${config.values.CODE_SECRET}`),
        'X-PART': config.values.PARTENAIRE_ID,
        'X-TS': timeStamp,
    });
};


/**
 * Récupère le json contenu dans la requête
 * Déchiffre la valeur reçu avec AES256 et enlèves les caractères unicodes non supportés
 * @param crypt
 * @param reqValue
 * @return string
 */
exports.GetJsonFromRequest = function(crypt, reqValue) {
    var json = crypt.rijndael256Decrypt(config.values.AES_KEY, config.values.IV, reqValue);
    json = json.replace(/[\x00-\x1F\x80-\xFF]+/, "");
    return JSON.parse(json);
};