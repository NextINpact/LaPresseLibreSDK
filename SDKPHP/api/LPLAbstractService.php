<?php

require_once "../Config.php";
require_once "../utils/HeaderUtils.php";

/**
* Classe abstraite 
* Réception du message, vérification des headers, déchiffrage du message, création du message de réponse
* @author  Bastien Caubet <bastien@nextinpact.com>, Luc Raymond <luc@nextinpact.com>
*/
abstract class LPLAbstractService 
{
    /**
    * Méthode HTTP de la requête
    * @var string
    * @access protected
    */
    protected $method;

    /**
    * Données stockées dans la requête
    * @var array
    * @access protected
    */
    protected $request;

    /**
    * Données stockées dans une requête PUT
    * @var array
    * @access protected
    */
    public $file;

    /**
     * Constructeur
     * Récupère la requête serveur, vérifie si les headers LPL sont présents et valides
     * Vérifie le type de méthode associée à la requête
     * Récupère les informations en fonction de la méthode
     * @access   public
     * @param    $server : $_SERVER
     * @throws Exception
     */
    public function __construct($server) {
        header("Access-Control-Allow-Orgin: *");
        header("Access-Control-Allow-Methods: *");
        header("Content-Type: application/json");

        if(HeaderUtils::CheckHeaders($server)) {
            $this->method = $server['REQUEST_METHOD'];
            if ($this->method == 'POST' && array_key_exists('HTTP_X_HTTP_METHOD', $server)) {
                if ($server['HTTP_X_HTTP_METHOD'] == 'DELETE') {
                    $this->method = 'DELETE';
                } else if ($server['HTTP_X_HTTP_METHOD'] == 'PUT') {
                    $this->method = 'PUT';
                } else {
                    throw new Exception("Unexpected Header");
                }
            }

            switch($this->method) {
                case 'DELETE':
                case 'POST':
                    $this->request = $this->_cleanInputs($_GET);
                    $this->file = file_get_contents("php://input");
                    break;
                case 'GET':
                    $this->request = $this->_cleanInputs($_GET);
                    break;
                case 'PUT':
                    $this->request = $this->_cleanInputs($_GET);
                    $this->file = file_get_contents("php://input");
                    break;
                default:
                    $this->createResponse('Invalid Method', 405);
                    break;
            }
        } else {
            throw new Exception("Unauthorized");
        }
    }


    /**
    * Récupération des informations de la requête
    * Création d'un array contenant les informations de la requête
    * @access   private
    * @param    $data
    * @return   Array
    */  
    private function _cleanInputs($data) {
        $clean_input = Array();
        if (is_array($data)) {
            foreach ($data as $k => $v) {
                $clean_input[$k] = $this->_cleanInputs($v);
            }
        } else {
            $clean_input = trim(strip_tags($data));
        }
        return $clean_input;
    }

    /**
    * Déchiffre le paramètre URL ou le body de la requête LPL
    * Decode en base64, déchiffre l'AES et décode en json la chaîne de caractère envoyée
    * @access   protected
    * @param    $param : string
    * @return   Array
    */
    protected function decryptRequest($param) {
        $raw = base64_decode($param);
        $rawJson = rtrim(mcrypt_decrypt(MCRYPT_RIJNDAEL_128, Config::AES_KEY, $raw, MCRYPT_MODE_CBC, Config::IV), "\0\4");
        return json_decode($rawJson, TRUE);
    }

    /**
    * Création de la réponse HTTP
    * Ajout des headers, chiffrage AES, encodage Json
    * @access   protected
    * @param    $data
    * @param    $status
    * @return   string
    */
    protected function createResponse($data, $status = 200) {
        header("HTTP/1.1 " . $status . " " . $this->_requestStatus($status));
        
        if($status == 200) {
            header("X-LPL: " . sha1(Config::PARTENAIRE_ID . "+" . time() . "+" . Config::CODE_SECRET));
            header("X-PART: " . Config::PARTENAIRE_ID);
            header("X-TS: " . time());
            $response = base64_encode(mcrypt_encrypt(MCRYPT_RIJNDAEL_128, Config::AES_KEY, json_encode($data), MCRYPT_MODE_CBC, Config::IV));
        } else {
            $response = $data;
        }

        return $response;
    }

    /**
     * Retourne le code de statut de la requête
     * @access   private
     * @param $code
     * @return string
     * @internal param $data
     */
    private function _requestStatus($code) {
        $status = array(
            200 => 'OK',
            401 => 'Not Authorized',
            404 => 'Not Found',   
            405 => 'Method Not Allowed',
            500 => 'Internal Server Error',
        ); 
        return ($status[$code]) ? $status[$code] : $status[500];
    }
}