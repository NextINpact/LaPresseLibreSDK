<?php

require_once "LPLAbstractService.php";    
require_once "../models/CreationCompteModel.php";  
  
/** 
* Classe de création de compte partenaire 
* @author  Bastien Caubet <bastien@nextinpact.com>, Luc Raymond <luc@nextinpact.com>
*/
class CreationCompteService extends LPLAbstractService
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
     *
     * @access   public
     * @return CreationCompteModel
     * @throws Exception
     */
    public function getResult() {
        if($this->method == 'POST') {
            $json = parent::decryptRequest($this->file);
            return new CreationCompteModel($json);
        }

        throw new Exception("Invalid Request");
    }

    /**
    * Création de la réponse HTTP
    * Appel à la méthode héritée de la classe parent LPLAbstractService
    * 
    * @access   public
    * @param    $data
    * @param    $status
    * @return   Array
    */
    public function createResponse($data, $status){
        return parent::createResponse($data, $status);
    }
}