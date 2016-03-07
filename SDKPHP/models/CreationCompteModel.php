<?php
/**
* Modèle pour la création de compte
* Représente le modèle de données reçu lors de l'envoi d'une requete LPL de création de compte partenaire
* @author  Bastien Caubet <bastien@nextinpact.com>, Luc Raymond <luc@nextinpact.com>
*/
class CreationCompteModel{

    public $CodeUtilisateur;
    public $Pseudo;
    public $Mail;
    public $Password;
    public $TypeAbonnement;
    public $DateSouscription;
    public $DateExpiration;
    public $Tarif;
    public $Statut;

    public function __construct($json){
        $this->CodeUtilisateur = $json["CodeUtilisateur"];
        $this->Pseudo = $json["Pseudo"];
        $this->Mail = $json["Mail"];
        $this->Password = $json["Password"];
        $this->TypeAbonnement = $json["TypeAbonnement"];
        $this->DateSouscription = $json["DateSouscription"];
        $this->DateExpiration = $json["DateExpiration"];
        $this->Tarif = $json["Tarif"];
        $this->Statut = $json["Statut"];
    } 
}