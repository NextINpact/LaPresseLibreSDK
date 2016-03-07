<?php
error_reporting(-1); // reports all errors
ini_set("display_errors", "1"); // shows all errors

require_once "../api/MajCompteService.php";
require_once "../models/ValidationReponseModel.php";

/**
 * Implémentation basique du web-service de mise à jour
 * Le fichier Web.config indique le point de terminaison suivant : /ws/majCompte
 */
try {
    $service = new MajCompteService($_SERVER);
    $majModel = $service->getResult();

    // Traitements en base de données à partir de l'objet $majModel

    $model = new ValidationReponseModel();
    $model->CodeUtilisateur = "e5016836-dfbe-49e1-82d7-b8ac300da6aa";
    $model->IsValid = TRUE;
    $model->PartenaireID = 2;
    $model->CodeEtat = Etat::Success;

    echo $service->createResponse($model, 200);
} catch(Exception $e) {
    header('HTTP/1.1 401 Unauthorized', true, 401);
    echo json_encode(Array('error' => $e->getMessage()));
}