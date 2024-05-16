from django.contrib import admin

# Register your models here.
from .models import Agent, Job, PrivateLocation, LoadDistribution, CustomToken

admin.site.register(Agent)
admin.site.register(Job)
admin.site.register(PrivateLocation)
admin.site.register(LoadDistribution)
admin.site.register(CustomToken)