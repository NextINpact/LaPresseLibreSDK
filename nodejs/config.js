/**
 * Classe Config
 * Pour connaitre vos identifiants, veuillez vous reporter à la documentation du SDK, à la section Configuration 
 * https://github.com/NextINpact/LaPresseLibreSDK/wiki/Int%C3%A9gration-et-configuration-du-SDK
 */
exports.values = {
    /**
    * Clé secrete pour le chiffrage AES Rijndael 256
    * À modifier
    * @var string
    */
    AES_KEY: "anjueolkdiwpoidaanjueolkdiwpoida",
    
    /**
    * Vecteur d'initialisation pour le chiffrage AES Rijndael 256
    * À modifier
    * @var string
    */
    IV: "4528711254935489",
    
    /**
    * Numéro partenaire LPL
    * À modifier
    * @var string
    */
    PARTENAIRE_ID: "2",
    
    /**
    * Code secret pour le hachage du header X-LPL
    * À modifier
    * @var string
    */
    CODE_SECRET: "af3zfA2qd",
};