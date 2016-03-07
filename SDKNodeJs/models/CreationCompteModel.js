module.exports = CreationCompteModel;

function CreationCompteModel(obj) {
    this.CodeUtilisateur = obj.CodeUtilisateur;
    this.Pseudo = obj.Pseudo;
    this.Mail = obj.Mail;
    this.Password = obj.Password;
    this.TypeAbonnement = obj.TypeAbonnement;
    this.DateSouscription = obj.DateSouscription;
    this.DateExpiration = obj.DateExpiration;
    this.Tarif = obj.Tarif;
    this.Statut = obj.Statut;
}