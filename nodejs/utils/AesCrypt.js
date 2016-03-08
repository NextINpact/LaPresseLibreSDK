/**
 * Classe pour le chiffrement AES
 * @author Bastien Caubet <bastien@nextinpact.com>, Luc Raymond <luc@nextinpact.com>
 */


var crypto = require("crypto");

module.exports = AESCrypt;

function AESCrypt() { }

/**
 * Chiffrement AES256
 * Utilise AES256 pour chiffrer une chaine de caractères et encode en base64
 * @param aesKey
 * @param iv
 * @param data
 * @return string
 */
AESCrypt.prototype.rijndael256Encrypt = function (aesKey, iv, data) {
    var encipher = crypto.createCipheriv("aes-256-cbc", aesKey, iv);

    var encryptdata = encipher.update(data, "utf8", "base64");

    encryptdata += encipher.final("base64");

    return encryptdata;
}


/**
 * Déchiffrement AES256
 * Décode en base64 la chaine de caractères et utilise AES256 pour la déchiffrer
 * @param aesKey
 * @param iv
 * @param data
 * @return string
 */
AESCrypt.prototype.rijndael256Decrypt = function (aesKey, iv, data) {
    var decipher = crypto.createDecipheriv("aes-256-cbc", aesKey, iv);
    
    decipher.setAutoPadding(false);

    var decoded = decipher.update(data, "base64", "utf8");

    decoded += decipher.final("utf8");

    return decoded;
}