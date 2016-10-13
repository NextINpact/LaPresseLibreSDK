import base64
from Crypto.Cipher import AES


class Crypt(object):
    """
    Classe pour le chiffrement AES256
    """

    def __init__(self, key, iv):
        self._key = key
        self._iv = iv
        self._mode = AES.MODE_CBC
        self._block_size = AES.block_size

    def pad(self, s):
        return s + b"\0" * (self._block_size - len(s) % self._block_size)

    def aes_encrypt(self, plain_text):
        """
        Chiffrement AES256
        Utilise AES256 pour chiffrer une chaine de caractères et encode en base64
        :param plain_text:
        :return:
        """
        encryptor = AES.new(self._key, self._mode, self._iv)
        padded_text = self.pad(plain_text.encode('utf-8'))
        return base64.b64encode(encryptor.encrypt(padded_text))

    def aes_decrypt(self, cipher_text):
        """
        Déchiffrement AES256
        Décode en base64 la chaine de caractères et utilise AES256 pour la déchiffrer
        :param cipher_text:
        :return:
        """
        decryptor = AES.new(self._key, self._mode, self._iv)
        plain_text = decryptor.decrypt(base64.b64decode(cipher_text))
        return plain_text.rstrip(b"\0").decode('utf-8')
