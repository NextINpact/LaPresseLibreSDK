/**
* Modèle de réponse pour la vérification de compte
* Représente le modèle de données envoyé en réponse d'une requête de vérification de compte partenaire
* @author  Bastien Caubet <bastien@nextinpact.com>, Luc Raymond <luc@nextinpact.com>
*/
module.exports = UserInfosModel;

function UserInfosModel() {
    this.PartenaireID;
    this.Mail;
    this.CodeUtilisateur;
    this.TypeAbonnement;
    this.DateExpiration ;
    this.DateSouscription;
    this.AccountExist;
}

UserInfosModel.prototype.CreateDummyModel = function () {
    this.Mail = "dummy@gmail.com";
    this.CodeUtilisateur = "dummy1234";
    this.AccountExist = true;
}