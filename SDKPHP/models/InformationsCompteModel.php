<?php

/**
 * Created by PhpStorm.
 * User: Luc
 * Date: 18/01/2016
 * Time: 16:28
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