<?php
/**
* Modèle de réponse
* Représente le modèle de données envoyé en réponse d'une requête de création ou de mise à jour de compte partenaire
* @author  Bastien Caubet <bastien@nextinpact.com>, Luc Raymond <luc@nextinpact.com>
*/
class ValidationReponseModel
{
    public $IsValid;
    public $CodeUtilisateur;
    public $PartenaireID;
    public $CodeEtat;

    public function CreateDummyModel() {
        $this->PartenaireID = 0;
        $this->CodeUtilisateur = "dummy";
        $this->IsValid = TRUE;
        $this->CodeEtat = Etat::Success;
    }
}


abstract class Etat {
    const Success = 1;
    const EmailExist = 2;
    const UsernameExist = 3;
    const Fail = 4;
}