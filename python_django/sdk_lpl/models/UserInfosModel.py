class UserInfoModel(object):
    PartenaireID = 0
    Mail = ""
    CodeUtilisateur = ""
    TypeAbonnement = ""
    DateExpiration = ""
    DateSouscription = ""
    AccountExist = False

    def __init__(self, **kwargs):
        self.__dict__.update(kwargs)

    def create_dummy_model(self):
        self.Mail = "dummy@gmail.com"
        self.CodeUtilisateur = "dummy1234"
        self.AccountExist = True
        self.PartenaireID = 0
