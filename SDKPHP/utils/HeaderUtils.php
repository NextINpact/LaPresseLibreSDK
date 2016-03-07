<?php

/**
 * @author  Bastien Caubet <bastien@nextinpact.com>, Luc Raymond <luc@nextinpact.com>
 * Date: 19/01/2016
 * Time: 09:01
 */
class HeaderUtils
{
    /**
     * Vérifie le contexte de la requête
     * Si le header X_CTX est présent, la requête sert à verifier l'état du web-service
     * @param $server
     * @return bool
     */
    public static function IsTestingContext($server) {
        return array_key_exists('HTTP_X_CTX', $server);
    }


    /**
     * Vérification des informations contenus dans le header
     * Permet de valider la transaction après comparaison du hachage SHA1 entre les valeurs contenues dans les headers X-PART et X-TS plus le code secret et la valeur du header X-LPL
     * @param $server
     * @return bool
     */
    public static function CheckHeaders($server) {
        if(array_key_exists('HTTP_X_TS', $server) && array_key_exists('HTTP_X_PART', $server) && array_key_exists('HTTP_X_LPL', $server)) {
            return sha1($server['HTTP_X_PART'] . "+" . $server['HTTP_X_TS'] . "+" . Config::CODE_SECRET) == $server['HTTP_X_LPL'];
        }
        return FALSE;
    }
}