import json
from urllib.parse import quote

from sdk_lpl.config import Config
from sdk_lpl.models.InfoCompteModel import InformationCompteModel
from sdk_lpl.utils.crypt import Crypt


def generate_url(mail, username, guid):
    """
    Génération de l'url de l'inscription partenaire
    Inclure cette url dans une balise <a> pour rediriger l'utilisateur connecté vers une page d'inscription de la plateforme LPL avec les champs mail et pseudo déjà remplis
    :param mail:
    :param username:
    :param guid:
    :return:
    """
    model = InformationCompteModel(mail, username, guid)
    json_val = json.dumps(model.__dict__)
    crypt = Crypt(Config.aes_key, Config.iv)
    return "http://www.lapresselibre.fr/inscription-partenaire?user={}&partId={}".format(
        quote(crypt.aes_encrypt(json_val)), Config.partenaire_id)
