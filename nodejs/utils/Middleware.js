/**
 * @author Bastien Caubet <bastien@nextinpact.com>, Luc Raymond <luc@nextinpact.com>
 * 
 * The MIT License (MIT) Copyright (c) 2016 INpact Mediagroup
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
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