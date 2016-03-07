<?php
/**
* Modèle de réponse pour la vérification de compte
* Représente le modèle de données envoyé en réponse d'une requête de vérification de compte partenaire
* @author  Bastien Caubet <bastien@nextinpact.com>, Luc Raymond <luc@nextinpact.com>
*/
class UserInfoModel
{
    public $PartenaireID;
    public $Mail;
    public $CodeUtilisateur;
    public $TypeAbonnement;
    public $DateExpiration;
    public $DateSouscription;
    public $AccountExist;


    public function CreateDummyModel() {
        $this->Mail = "dummy@gmail.com";
        $this->CodeUtilisateur = "dummy";
        $this->AccountExist = TRUE;
    }
}
