/**
 * Classe pour le chiffrement AES
 * @author Bastien Caubet <bastien@nextinpact.com>, Luc Raymond <luc@nextinpact.com>
 * 
 * The MIT License (MIT) Copyright (c) 2016 INpact Mediagroup
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
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
AESCrypt.prototype.rijndael128Encrypt = function (aesKey, iv, data) {
    var encipher = crypto.createCipheriv("aes-128-cbc", aesKey, iv);

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
AESCrypt.prototype.rijndael128Decrypt = function (aesKey, iv, data) {
    var decipher = crypto.createDecipheriv("aes-128-cbc", aesKey, iv);
    
    decipher.setAutoPadding(false);

    var decoded = decipher.update(data, "base64", "utf8");

    decoded += decipher.final("utf8");

    return decoded;
}
