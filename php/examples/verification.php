<?php
error_reporting(-1); // reports all errors
ini_set("display_errors", "1"); // shows all errors

require_once "../api/VerificationService.php";
require_once "../models/UserInfoModel.php";
require_once "../utils/HeaderUtils.php";


/**
 * Implémentation basique du web-service de vérification
 */
try {
    $service = new VerificationService($_SERVER);
    $model = new UserInfoModel();

    // Contexte de TEST pour vérifier le bon fonctionnement du web-service
    // Le modèle retourné doit contenir des valeurs non pertinentes
    if(HeaderUtils::IsTestingContext($_SERVER)) {
        $model->CreateDummyModel();
    } else {
        $verificationModel = $service->getResult();

        // Ajoutez ici votre logique de vérification des données en base à partir de l'objet $verificationModel
        // Exemple de composition du modèle à partir des données en base
        $model->Mail = "jean.dupont@gmail.com";
        $model->CodeUtilisateur = "e5016836-dfbe-49e1-82d7-b8ac300da6aa";
        $model->TypeAbonnement = "Mensuel";
        $model->PartenaireID = 2;
        $model->DateExpiration = date_format(new DateTime(), "Y-m-d\TH:i:sO");
        $model->DateSouscription = date_format(new DateTime(), "Y-m-d\TH:i:sO");
        $model->AccountExist = TRUE;
    }

    echo $service->createResponse($model, 200);
} catch(Exception $e) {
    header('HTTP/1.1 401 Unauthorized', true, 401);
    echo json_encode(Array('error' => $e->getMessage()));
}

