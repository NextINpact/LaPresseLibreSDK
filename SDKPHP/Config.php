<?php

/**
 * Class Config
 * Pour connaitre vos identifiants, veuillez vous reporter à la documentation du SDK, à la section Configuration
 */
class Config {
    /**
    * Clé secrete pour le chiffrage AES Rijndael 256
    * À modifier
    * @var string
    */
    const AES_KEY = "anjueolkdiwpoidaanjueolkdiwpoida";

    /**
    * Vecteur d'initialisation pour le chiffrage AES Rijndael 256
    * À modifier
    * @var string
    */
    const IV = "4528711254935489";

    /**
    * Numéro partenaire LPL
    * À modifier
    * @var string
    */
    const PARTENAIRE_ID = "2";

    /**
    * Code secret pour le hachage du header X-LPL
    * À modifier
    * @var string
    */
    const CODE_SECRET = "af3zfA2qd";
}