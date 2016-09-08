<?php

/**
* Class Config
* Pour connaitre vos identifiants, veuillez vous reporter à la documentation du SDK, à la section Configuration
* https://github.com/NextINpact/LaPresseLibreSDK/wiki/Int%C3%A9gration-et-configuration-du-SDK
* 
* The MIT License (MIT) Copyright (c) 2016 INpact Mediagroup
*
* Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
*
* The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
*
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
class Config {
    /**
    * Clé secrete pour le chiffrage AES Rijndael 256
    * À modifier
    * @var string
    */
    const AES_KEY = "UKKzV7sxiGx3uc0auKrUO2kJTT2KSCeg";

    /**
    * Vecteur d'initialisation pour le chiffrage AES Rijndael 256
    * À modifier
    * @var string
    */
    const IV = "7405589013321961";

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
    const CODE_SECRET = "mGoMuzoX8u";
}