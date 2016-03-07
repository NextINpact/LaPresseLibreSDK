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
    this.CodeUtilisateur = "dummy";
    this.AccountExist = true;
}