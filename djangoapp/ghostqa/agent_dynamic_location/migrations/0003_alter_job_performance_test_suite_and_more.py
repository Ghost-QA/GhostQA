# Generated by Django 5.0.4 on 2024-04-22 19:06

import django.db.models.deletion
from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ("agent_dynamic_location", "0002_alter_job_field_type"),
        ("cypress", "0005_testsuite_request_json_and_more"),
        ("performace_test", "0004_testcontainersruns_raw_data"),
    ]

    operations = [
        migrations.AlterField(
            model_name="job",
            name="performance_test_suite",
            field=models.ForeignKey(
                on_delete=django.db.models.deletion.CASCADE,
                to="performace_test.performacetestsuite",
            ),
        ),
        migrations.AlterField(
            model_name="job",
            name="test_suite",
            field=models.ForeignKey(
                on_delete=django.db.models.deletion.CASCADE, to="cypress.testsuite"
            ),
        ),
    ]
