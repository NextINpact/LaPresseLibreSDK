<?php

/**
* Utilitaire pour l'inscription partenaire
* @author  Bastien Caubet <bastien@nextinpact.com>, Luc Raymond <luc@nextinpact.com>
* Date: 18/01/2016
* Time: 16:24
* 
* The MIT License (MIT) Copyright (c) 2016 INpact Mediagroup
*
* Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
*
* The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
*
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
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