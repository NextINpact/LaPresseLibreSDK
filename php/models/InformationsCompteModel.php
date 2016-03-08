<?php

/**
* Modèle pour la création d'un lien d'inscription partenaire
* Représente le modèle de données qui compose le paramètre <user> dans l'url d'inscription partenaire
* @author  Bastien Caubet <bastien@nextinpact.com>, Luc Raymond <luc@nextinpact.com>
*/
class InformationsCompteModel
{
    public $Email;
    public $Pseudo;

    public function __construct($email, $userName)
    {
        $this->Email = $email;
        $this->Pseudo = $userName;
    }
}