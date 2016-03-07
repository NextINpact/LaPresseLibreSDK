module.exports = VerificationModel;

function VerificationModel(obj) {
    this.Password = obj.Password;
    this.Mail = obj.Mail;
    this.CodeUtilisateur = obj.CodeUtilisateur;
}