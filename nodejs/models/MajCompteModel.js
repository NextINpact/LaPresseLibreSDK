/**
* Modèle pour la mise à jour de compte
* Représente le modèle de données reçu lors de l'envoi d'une requete LPL de mise à jour de compte partenaire
* @author  Bastien Caubet <bastien@nextinpact.com>, Luc Raymond <luc@nextinpact.com>
*/
module.exports = MajCompteModel;

function MajCompteModel(obj) {
    this.CodeUtilisateur = obj.CodeUtilisateur;
    this.TypeAbonnement = obj.TypeAbonnement;
    this.DateSouscription = obj.DateSouscription;
    this.DateExpiration = obj.DateExpiration;
    this.Tarif = obj.Tarif;
    this.Statut = obj.Statut;
}