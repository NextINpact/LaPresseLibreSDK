import json
from datetime import datetime

from django.http import HttpResponse
from django.utils.decorators import decorator_from_middleware

from sdk_lpl.config import Config
from sdk_lpl.models.CreationCompteModel import CreationCompteModel
from sdk_lpl.models.MajCompteModel import MajCompteModel
from sdk_lpl.models.UserInfosModel import UserInfoModel
from sdk_lpl.models.ValidationReponseModel import ValidationResponseModel, Codes
from sdk_lpl.models.VerificationModel import VerificationModel
from sdk_lpl.utils.crypt import Crypt
from sdk_lpl.utils.middlewares import is_testing_ctx, LplMiddleware


@decorator_from_middleware(LplMiddleware)
def verification(request):
    """
    Web-service de vérification
    :param request:
    :return:
    """

    crypt = Crypt(Config.aes_key, Config.iv)
    model = UserInfoModel()

    if is_testing_ctx(request):
        model.create_dummy_model()
    else:
        json_val = crypt.aes_decrypt(request.GET.get('crd', ''))
        j = json.loads(json_val)
        v_model = VerificationModel(j['Password'], j['Mail'], j['CodeUtilisateur'])

        """
        TODO : à modifier
        Ajoutez ici votre logique de verification des donnees en base à partir de l'objet c_model
        Exemple de composition du modele à partir des donnees en base
        """
        model.PartenaireID = Config.partenaire_id
        model.Mail = "testabo@gmail.com"
        model.CodeUtilisateur = "123123-1231-123-12311"
        model.AccountExist = True
        model.TypeAbonnement = "mensuel"
        model.DateExpiration = datetime.now().isoformat()
        model.DateSouscription = datetime.now().isoformat()

    response = crypt.aes_encrypt(json.dumps(model.__dict__))
    return HttpResponse(response, content_type="application/json")


@decorator_from_middleware(LplMiddleware)
def propagation(request):
    """
    Web-service de création de compte
    :param request:
    :return:
    """

    crypt = Crypt(Config.aes_key, Config.iv)
    model = ValidationResponseModel()

    if is_testing_ctx(request):
        model.create_dummy_model()
    else:
        json_val = crypt.aes_decrypt(request.body)
        j = json.loads(json_val)
        c_model = CreationCompteModel(j['CodeUtilisateur'], j['Pseudo'], j['Mail'], j['Password'], j['TypeAbonnement'], j['DateSouscription'], j['DateExpiration'], j['Tarif'], j['Statut'])


        """
        TODO : à modifier
        Ajoutez ici votre logique de verification des donnees en base à partir de l'objet c_model
        Exemple de composition du modele à partir des donnees en base
        """
        model.PartenaireID = Config.partenaire_id
        model.CodeUtilisateur = c_model.CodeUtilisateur
        model.IsValid = True
        model.CodeEtat = Codes.Success

    response = crypt.aes_encrypt(json.dumps(model.__dict__))
    return HttpResponse(response, content_type="application/json")


@decorator_from_middleware(LplMiddleware)
def update(request):
    """
    Web-service de mise à jour de compte
    :param request:
    :return:
    """
    crypt = Crypt(Config.aes_key, Config.iv)
    model = ValidationResponseModel()

    json_val = crypt.aes_decrypt(request.body)
    j = json.loads(json_val)
    maj_model = MajCompteModel(j['CodeUtilisateur'], j['TypeAbonnement'], j['DateSouscription'], j['DateExpiration'], j['Tarif'], j['Statut'])

    """
    TODO : à modifier
    Ajoutez ici votre logique de verification des donnees en base à partir de l'objet maj_model
    Exemple de composition du modele à partir des donnees en base
    """
    model.PartenaireID = Config.partenaire_id
    model.CodeUtilisateur = maj_model.CodeUtilisateur
    model.IsValid = True
    model.CodeEtat = Codes.Success

    response = crypt.aes_encrypt(json.dumps(model.__dict__))
    return HttpResponse(response, content_type="application/json")