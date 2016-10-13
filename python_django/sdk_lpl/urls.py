from django.conf.urls import url

from . import views

urlpatterns = [
    url(r'^verif', views.verification, name='verif'),
    url(r'^propagation', views.propagation, name='propagation'),
    url(r'^update', views.update, name='update'),
]
