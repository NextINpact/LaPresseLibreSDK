<?php

/**
 * @author  Bastien Caubet <bastien@nextinpact.com>, Luc Raymond <luc@nextinpact.com>
 * Date: 19/01/2016
 * Time: 09:01
 * 
 * The MIT License (MIT) Copyright (c) 2016 INpact Mediagroup
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
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