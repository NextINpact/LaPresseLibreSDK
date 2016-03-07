var crypto = require("crypto");

module.exports = AESCrypt;

function AESCrypt() { }

AESCrypt.prototype.rijndael256Encrypt = function (aesKey, iv, data) {
    var encipher = crypto.createCipheriv("aes-256-cbc", aesKey, iv);

    var encryptdata = encipher.update(data, "utf8", "base64");

    encryptdata += encipher.final("base64");

    return encryptdata;
}


AESCrypt.prototype.rijndael256Decrypt = function (aesKey, iv, data) {
    var decipher = crypto.createDecipheriv("aes-256-cbc", aesKey, iv);
    
    decipher.setAutoPadding(false);

    var decoded = decipher.update(data, "base64", "utf8");

    decoded += decipher.final("utf8");

    return decoded;
}