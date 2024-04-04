# Generated by Django 5.0.3 on 2024-03-20 11:40

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ("cypress", "0004_testsuite_cypress_code"),
    ]

    operations = [
        migrations.AddField(
            model_name="testsuite",
            name="request_json",
            field=models.JSONField(blank=True, null=True),
        ),
        migrations.AlterField(
            model_name="testsuite",
            name="scenarios_file",
            field=models.FileField(
                blank=True, null=True, upload_to="uploads/scenarios_file/"
            ),
        ),
    ]
