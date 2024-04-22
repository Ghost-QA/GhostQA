# Generated by Django 5.0.4 on 2024-04-22 17:18

import django.db.models.deletion
import uuid
from django.db import migrations, models


class Migration(migrations.Migration):

    initial = True

    dependencies = [
        ("cypress", "0005_testsuite_request_json_and_more"),
        ("performace_test", "0004_testcontainersruns_raw_data"),
    ]

    operations = [
        migrations.CreateModel(
            name="AgentDetails",
            fields=[
                (
                    "id",
                    models.BigAutoField(
                        auto_created=True,
                        primary_key=True,
                        serialize=False,
                        verbose_name="ID",
                    ),
                ),
                ("name", models.CharField(max_length=100)),
                ("ip_address", models.GenericIPAddressField()),
                ("port", models.IntegerField()),
                ("max_concurrent_jobs", models.IntegerField()),
                (
                    "status",
                    models.CharField(
                        choices=[("active", "Active"), ("inactive", "Inactive")],
                        default="inactive",
                        max_length=20,
                    ),
                ),
                (
                    "agent_status",
                    models.CharField(
                        choices=[("available", "Available"), ("Occupied", "Occupied")],
                        default="available",
                        max_length=20,
                    ),
                ),
                ("created_at", models.DateTimeField(auto_now_add=True)),
                ("updated_at", models.DateTimeField(auto_now=True)),
            ],
        ),
        migrations.CreateModel(
            name="Job",
            fields=[
                (
                    "id",
                    models.BigAutoField(
                        auto_created=True,
                        primary_key=True,
                        serialize=False,
                        verbose_name="ID",
                    ),
                ),
                (
                    "job_id",
                    models.CharField(
                        default=uuid.uuid4, editable=False, max_length=36, unique=True
                    ),
                ),
                (
                    "field_type",
                    models.CharField(
                        choices=[("jmeter", "JMX"), ("testlab", "Cypress")],
                        default=None,
                        max_length=20,
                    ),
                ),
                (
                    "job_status",
                    models.CharField(
                        blank=True,
                        choices=[
                            ("queued", "Queued"),
                            ("pending", "Pending"),
                            ("completed", "Completed"),
                        ],
                        default="queued",
                        max_length=10,
                    ),
                ),
                ("created_at", models.DateTimeField(auto_now_add=True)),
                ("updated_at", models.DateTimeField(auto_now=True)),
                (
                    "agent",
                    models.ForeignKey(
                        on_delete=django.db.models.deletion.CASCADE,
                        related_name="agent_details",
                        to="agent_dynamic_location.agentdetails",
                    ),
                ),
                (
                    "performance_test_suite",
                    models.ForeignKey(
                        blank=True,
                        null=True,
                        on_delete=django.db.models.deletion.CASCADE,
                        to="performace_test.performacetestsuite",
                    ),
                ),
                (
                    "test_suite",
                    models.ForeignKey(
                        blank=True,
                        null=True,
                        on_delete=django.db.models.deletion.CASCADE,
                        to="cypress.testsuite",
                    ),
                ),
            ],
        ),
    ]
