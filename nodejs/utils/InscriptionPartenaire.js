/**
 * Utilitaire pour l'inscription partenaire
 * @author Bastien Caubet <bastien@nextinpact.com>, Luc Raymond <luc@nextinpact.com>
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
exports.GenerateUrl = function(mail, userName) {
    var model = new InfoModel(mail, userName);
    
    var json = JSON.stringify(model);
    
    var crypt = new AESCrypt();
    
    return "http://www.lapresselibre.fr/inscription-partenaire?" + 
              `user=${encodeURIComponent(crypt.rijndael256Encrypt(config.values.AES_KEY, config.values.IV, json))}` +
              `&partId=${config.values.PARTENAIRE_ID}`;
};