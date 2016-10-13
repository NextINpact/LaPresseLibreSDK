import hashlib
import time
from django.http import HttpResponse
from sdk_lpl.config import Config


def is_testing_ctx(request):
    """
    Vérifie le contexte de la requête
    Si le header x-ctx est présent, la requête sert à verifier l'état du web-service
    :param request:
    :return:
    """
    return "HTTP_X_CTX" in request.META


class LplMiddleware(object):
    """
    Vérification des informations contenus dans le header
    Permet de valider la transaction après comparaison du hachage SHA1
    entre les valeurs contenues dans les headers X-PART et X-TS plus le code secret et la valeur du header X-LPL
    """

    def process_request(self, request):
        if "HTTP_X_LPL" in request.META and "HTTP_X_TS" in request.META and "HTTP_X_PART" in request.META:
            request_hash = request.META["HTTP_X_LPL"]
            timestamp = request.META["HTTP_X_TS"]
            part_id = request.META["HTTP_X_PART"]

            expected_hash = hashlib.sha1(
                "{}+{}+{}".format(part_id, timestamp, Config.code_secret).encode('utf-8')).hexdigest()

            if expected_hash != request_hash:
                return HttpResponse('Unauthorized', status=401)
            else:
                pass

        else:
            return HttpResponse('Unauthorized', status=401)

    def process_response(self, request, response):
        timestamp = int(time.time())

        response['X-LPL'] = hashlib.sha1(
            "{}+{}+{}".format(Config.partenaire_id, timestamp, Config.code_secret).encode('utf-8')).hexdigest()
        response['X-TS'] = timestamp
        response['X-PART'] = Config.partenaire_id

        return response
