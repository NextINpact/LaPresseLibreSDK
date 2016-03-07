<?php

/**
 * Utilitaire pour l'inscription partenaire
 * @author  Bastien Caubet <bastien@nextinpact.com>, Luc Raymond <luc@nextinpact.com>
 * Date: 18/01/2016
 * Time: 16:24
 */

require_once "../models/InformationsCompteModel.php";
require_once "../Config.php";

class InscriptionPartenaire
{
    /**
     * Génération de l'url de l'inscription partenaire
     * Inclure cette url dans une balise <a> pour rediriger l'utilisateur connecté vers une page d'inscription de la plateforme LPL avec les champs mail et pseudo déjà remplis
     * @param $email
     * @param $userName
     * @return string
     */
    public static function GenerateUrl($email, $userName) {
        $model = new InformationsCompteModel($email, $userName);

        $json = json_encode($model);

        $url = "http://www.lapresselibre.fr/inscription-partenaire?"
            . "user=" . rawurlencode(base64_encode(mcrypt_encrypt(MCRYPT_RIJNDAEL_256, Config::AES_KEY, $json, MCRYPT_MODE_CBC, Config::IV)))
            . "&partId=" . Config::PARTENAIRE_ID;

        return $url;
    }
}