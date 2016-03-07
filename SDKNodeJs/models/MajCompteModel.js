module.exports = MajCompteModel;

function MajCompteModel(obj) {
    this.CodeUtilisateur = obj.CodeUtilisateur;
    this.TypeAbonnement = obj.TypeAbonnement;
    this.DateSouscription = obj.DateSouscription;
    this.DateExpiration = obj.DateExpiration;
    this.Tarif = obj.Tarif;
    this.Statut = obj.Statut;
}