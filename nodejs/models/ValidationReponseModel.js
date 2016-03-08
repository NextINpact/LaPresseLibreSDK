/**
* Modèle de réponse
* Représente le modèle de données envoyé en réponse d'une requête de création ou de mise à jour de compte partenaire
* @author  Bastien Caubet <bastien@nextinpact.com>, Luc Raymond <luc@nextinpact.com>
*/
module.exports.ValidationModel = ValidationReponseModel;

function ValidationReponseModel() {
    this.PartenaireID;
    this.CodeUtilisateur;
    this.IsValid;
    this.CodeEtat;
}

ValidationReponseModel.prototype.CreateDummyModel = function() {
    this.PartenaireID = 0;
    this.CodeUtilisateur = "dummy";
    this.IsValid = true;
    this.CodeEtat = Etat.Success;
}


var Etat = {
    Success : 1,
    EmailExist: 2,
    UsernameExist: 3,
    Fail: 4  
};

module.exports.Etat = Etat;