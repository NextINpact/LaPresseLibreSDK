class CreationCompteModel(object):
    def __init__(self, code_utilisateur, pseudo, mail, password, type_abo, date_souscription, date_expiration, tarif, statut):
        self.CodeUtilisateur = code_utilisateur
        self.Pseudo = pseudo
        self.Mail = mail
        self.Password = password
        self.TypeAbonnement = type_abo
        self.DateSouscription = date_souscription
        self.DateExpiration = date_expiration
        self.Tarif = tarif
        self.Statut = statut
