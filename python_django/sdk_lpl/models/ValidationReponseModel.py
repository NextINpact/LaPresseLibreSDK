from enum import IntEnum


class Codes(IntEnum):
    Success = 1
    EmailExist = 2
    UsernameExist = 3
    Fail = 4


class ValidationResponseModel(object):
    PartenaireID = 0
    CodeUtilisateur = ""
    IsValid = False
    CodeEtat = Codes.Fail

    def __init__(self, **kwargs):
        self.__dict__.update(kwargs)

    def create_dummy_model(self):
        self.PartenaireID = 0
        self.CodeUtilisateur = "dummy1234"
        self.IsValid = True
        self.CodeEtat = Codes.Success
