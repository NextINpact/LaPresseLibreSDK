<?php
/**
* Modèle pour la mise à jour de compte
* Représente le modèle de données reçu lors de l'envoi d'une requete LPL de mise à jour de compte partenaire
* @author  Bastien Caubet <bastien@nextinpact.com>, Luc Raymond <luc@nextinpact.com>
*/
class MajCompteModel
{
    public $CodeUtilisateur;
    public $TypeAbonnement;
    public $DateSouscription;
    public $DateExpiration;
    public $Tarif;
    public $Statut;

    public function __construct($json) {
        $this->CodeUtilisateur = $json["CodeUtilisateur"];
        $this->TypeAbonnement = $json["TypeAbonnement"];
        $this->DateSouscription = $json["DateSouscription"];
        $this->DateExpiration = $json["DateExpiration"];
        $this->Tarif = $json["Tarif"];
        $this->Statut = $json["Statut"];
    }
}
