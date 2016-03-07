<?php
error_reporting(-1); // reports all errors
ini_set("display_errors", "1"); // shows all errors

require_once "../utils/InscriptionPartenaire.php";

/**
 * Exemple d'inscription à La Presse Libre depuis un partenaire
 * Date: 18/01/2016
 * Time: 16:39
 */

echo "<div>";
echo "<a href='" . InscriptionPartenaire::GenerateUrl("toto@gmail.com", "toto") . "' title='Inscription LPL'>Inscrivez vous à La Presse Libre</a>";
echo "</div>";
