<?php
/**
* Modèle pour la vérification de compte
* Représente le modèle de données reçu lors de l'envoi d'une requete LPL de vérification de compte partenaire
* @author  Bastien Caubet <bastien@nextinpact.com>, Luc Raymond <luc@nextinpact.com>
*/ 
class VerificationModel
{
    public $Password;
    public $Mail;
    public $CodeUtilisateur;

    /**
     * VerificationModel constructor.
     * @param $json
     */
    public function __construct($json) {
        $this->Password = $json["Password"];
        $this->Mail = $json["Mail"];
        $this->CodeUtilisateur = $json["CodeUtilisateur"];
    }
}
