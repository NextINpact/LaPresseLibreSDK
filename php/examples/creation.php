<?php
error_reporting(-1); // reports all errors
ini_set("display_errors", "1"); // shows all errors

require_once "../api/CreationCompteService.php";
require_once "../models/ValidationReponseModel.php";
require_once "../utils/HeaderUtils.php";

/**
 * Implémentation basique du web-service de création de compte
 * Le fichier Web.config indique le point de terminaison suivant : /ws/creationCompte
 */
try {
    $service = new CreationCompteService($_SERVER);
    $model = new ValidationReponseModel();

    if(HeaderUtils::IsTestingContext($_SERVER)) {
        $model->CreateDummyModel();
    } else {
        $createModel = $service->getResult();
        // Ajoutez ici votre logique de vérification des données en base à partir de l'objet $createModel

        // Exemple de composition du modèle à partir des données en base
        $model->CodeUtilisateur = "e5016836-dfbe-49e1-82d7-b8ac300da6aa";
        $model->IsValid = TRUE;
        $model->PartenaireID = 2;
        $model->CodeEtat = Etat::Success;
    }

    echo $service->createResponse($model, 200);
} catch(Exception $e) {
    header('HTTP/1.1 401 Unauthorized', true, 401);
    echo json_encode(Array('error' => $e->getMessage()));
}