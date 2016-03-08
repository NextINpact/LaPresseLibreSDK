var express = require("express");
var bodyParser = require('body-parser');
var errorHandler = require('errorhandler');

var config = require("./config");
var AESCrypt = require("./utils/AesCrypt");
var LPLMiddleware = require("./utils/Middleware");
var InscriptionPartenaire = require("./utils/InscriptionPartenaire");


var VerifModel = require("./models/VerificationModel");
var CreaModel = require("./models/CreationCompteModel");
var UserInfosModel = require("./models/UserInfosModel");
var ValidResModel = require("./models/ValidationReponseModel").ValidationModel;
var Etat = require("./models/ValidationReponseModel").Etat;
var MajModel = require("./models/MajCompteModel");



var app = express();
app.set('port', process.env.PORT || 8888);
app.use(bodyParser.text());

if(app.get("env") == "development") {
    app.use(errorHandler());
    
    app.use(function (req, res, next) {
        next();
    });
}


/**
 * Exemple de la génération d'une url pour l'inscription partenaire
 */
app.get("/inscription-partenaire", function (req, res) {
    res.setHeader("Content-Type", "text/plain");
    res.end(InscriptionPartenaire.GenerateUrl("toto@gmail.com", "tata"));
});


/**
 * Web-service de vérification
 */
app.get("/ws/verification", LPLMiddleware.CheckRequestHeaders, function (req, res) {
    var model = new UserInfosModel();
    var crypt = new AESCrypt();
            
    if(LPLMiddleware.IsTestingContext(req)) {
        // Ne pas modifier
        model.CreateDummyModel();
    } else {
        var json = LPLMiddleware.GetJsonFromRequest(crypt, req.query.crd);

        var vModel = new VerifModel(json);

        // TODO : à modifier
        // Ajoutez ici votre logique de verification des donnees en base à partir de l'objet vModel
        // Exemple de composition du modele à partir des donnees en base
        model.Mail = "testabo@gmail.com";
        model.CodeUtilisateur = "123123-1231-123-12311";
        model.AccountExist = true;
        model.PartenaireID = config.values.PARTENAIRE_ID;
        model.TypeAbonnement = "Mensuel";
        model.DateExpiration = new Date();
        model.DateSouscription = new Date();
    }

    var response = crypt.rijndael256Encrypt(config.values.AES_KEY, config.values.IV, JSON.stringify(model));
    
    LPLMiddleware.AddResponseHeaders(res);

    res.status(200).end(response);
});


/**
 * Web-service de création de compte
 */
app.post("/ws/creationCompte", LPLMiddleware.CheckRequestHeaders, function(req, res) {
    var crypt = new AESCrypt();
    var model = new ValidResModel();
    
    if(LPLMiddleware.IsTestingContext(req)) {
        // Ne pas modifier
        model.CreateDummyModel();
    } else {
        var json = LPLMiddleware.GetJsonFromRequest(crypt, req.body);

        var cModel = new CreaModel(json);
        
        // TODO : à modifier
        // Ajoutez ici votre logique de verification des donnees en base a partir de l'objet cModel
        // Exemple de composition du modele a partir des donnees en base
        model.PartenaireID = config.values.PARTENAIRE_ID;
        model.CodeUtilisateur = cModel.CodeUtilisateur;
        model.IsValid = true;
        model.CodeEtat = Etat.Success;
    }

    var response = crypt.rijndael256Encrypt(config.values.AES_KEY, config.values.IV, JSON.stringify(model));
    
    LPLMiddleware.AddResponseHeaders(res);

    res.status(200).end(response);
});


app.put("/ws/majCompte", LPLMiddleware.CheckRequestHeaders, function(req, res) {
    var crypt = new AESCrypt();
    var model = new ValidResModel();
    
    var json = LPLMiddleware.GetJsonFromRequest(crypt, req.body);

    var majModel = new MajModel(json);

    // TODO : à modifier
    // Ajoutez ici votre logique de verification des donnees en base a partir de l'objet majModel
    // Exemple de composition du modele a partir des donnees en base
    model.PartenaireID = config.values.PARTENAIRE_ID;
    model.CodeUtilisateur = majModel.CodeUtilisateur;
    model.IsValid = true;
    model.CodeEtat = Etat.Success;
    
    var response = crypt.rijndael256Encrypt(config.values.AES_KEY, config.values.IV, JSON.stringify(model));
    
    LPLMiddleware.AddResponseHeaders(res);

    res.status(200).end(response);
});


app.listen(app.get('port'), function(){
    console.log('Express server listening on port ' + app.get('port'));
});