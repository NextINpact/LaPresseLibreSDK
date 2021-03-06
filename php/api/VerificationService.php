<?php

/** 
* Classe de vérification de compte partenaire 
* @author  Bastien Caubet <bastien@nextinpact.com>, Luc Raymond <luc@nextinpact.com>
* 
* The MIT License (MIT) Copyright (c) 2016 INpact Mediagroup
*
* Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
*
* The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
*
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

require_once "LPLAbstractService.php";    
require_once "../models/VerificationModel.php";

class VerificationService extends LPLAbstractService
{
    /**
    * Constructeur
    * Appel du constructeur parent LPLAbstractService
    * 
    * @access   public
    * @param    $server : $_SERVER
    */
    public function __construct($server) {
        parent::__construct($server);
    }

    /**
     * Obtient les informations déchiffrées envoyé par LPL.
     * Déchiffre le json et retourne l'objet correspondant
     * @access   public
     * @return VerificationModel
     * @throws Exception
     * @internal param $request
     */
    public function getResult() {
        if($this->method == 'GET') {
            $json = parent::decryptRequest($this->request["crd"]);
            return new VerificationModel($json);
        }

        throw new Exception("Invalid Request");
    }

    /**
    * Création de la réponse HTTP
    * Appel à la méthode héritée de la classe parent LPLAbstractService
    * @access   public
    * @param    $data
    * @param    $status
    * @return   Array
    */
    public function createResponse($data, $status = 200) {
        return parent::createResponse($data, $status);
    }
}