/**
* Modèle pour la vérification de compte
* Représente le modèle de données reçu lors de l'envoi d'une requete LPL de vérification de compte partenaire
* @author  Bastien Caubet <bastien@nextinpact.com>, Luc Raymond <luc@nextinpact.com>
*/ 
module.exports = VerificationModel;

function VerificationModel(obj) {
    this.Password = obj.Password;
    this.Mail = obj.Mail;
    this.CodeUtilisateur = obj.CodeUtilisateur;
}