var infoModel = require("../models/InformationCompteModel");
var config = require("../config");
var AESCrypt = require("../utils/AesCrypt");

exports.GenerateUrl = function(mail, userName) {
    var model = new infoModel(mail, userName);
    
    var json = JSON.stringify(model);
    
    var crypt = new AESCrypt();
    
    return "http://www.lapresselibre.fr/inscription-partenaire?" + 
              `user=${encodeURIComponent(crypt.rijndael256Encrypt(config.values.AES_KEY, config.values.IV, JSON.stringify(model)))}` +
              `&partId=${config.values.PARTENAIRE_ID}`;
};