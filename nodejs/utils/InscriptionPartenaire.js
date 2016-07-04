/**
 * Utilitaire pour l'inscription partenaire
 * @author Bastien Caubet <bastien@nextinpact.com>, Luc Raymond <luc@nextinpact.com>
 * 
 * The MIT License (MIT) Copyright (c) 2016 INpact Mediagroup
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

var InfoModel = require("../models/InformationCompteModel");
var config = require("../config");
var AESCrypt = require("../utils/AesCrypt");

/**
 * Génération de l'url de l'inscription partenaire
 * Inclure cette url dans une balise <a> pour rediriger l'utilisateur connecté vers une page d'inscription de la plateforme LPL avec les champs mail et pseudo déjà remplis
 * @param mail
 * @param userName
 * @return string
 */
exports.GenerateUrl = function(mail, userName, guid) {
    var model = new InfoModel(mail, userName, guid);
    
    var json = JSON.stringify(model);
    
    var crypt = new AESCrypt();
    
    return "http://www.lapresselibre.fr/inscription-partenaire?" + 
              `user=${encodeURIComponent(crypt.rijndael128Encrypt(config.values.AES_KEY, config.values.IV, json))}` +
              `&partId=${config.values.PARTENAIRE_ID}`;
};
