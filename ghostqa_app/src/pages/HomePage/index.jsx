import React, { useEffect } from "react";
import {
  Grid,
  Card,
  CardContent,
  Typography,
  Container,
} from "@material-ui/core";
import { makeStyles } from "@material-ui/core/styles";
import ReactApexChart from "react-apexcharts";
import OverallAutomation from "./charts/OverallAutomation";
import { useDispatch, useSelector } from "react-redux";
import { getJiraDeatils } from "../../redux/actions/dashboardAction";

const useStyles = makeStyles((theme) => ({
  root: {
    padding: theme.spacing(3),
    minHeight: "100vh",
    display: "flex",
    flexDirection: "column",
    justifyContent: "center",
    alignItems: "center",
  },
  topRow: {
    width: "100%",
    marginBottom: theme.spacing(2),
    display: "flex",
    justifyContent: "center",
  },
  contentRow: {
    flexGrow: 1,
    width: "100%",
    overflow: "auto",
    display: "flex",
    justifyContent: "center",
    alignItems: "center",
  },
  column: {
    padding: theme.spacing(2),
    display: "flex",
    justifyContent: "center",
    alignItems: "center",
  },
  card: {
    width: "100%",
    display: "flex",
    flexDirection: "column",
    justifyContent: "center",
    alignItems: "center",
    boxShadow: theme.shadows[2],
    borderRadius: theme.shape.borderRadius,
  },
  heading: {
    marginTop: theme.spacing(2),
    textAlign: "center",
    fontWeight: "bold",
  },
  chartContainer: {
    display: "flex",
    justifyContent: "center",
    alignItems: "center",
    width: "100%",
    height: "100%",
  },
  overallAutomationCard: {
    width: "100%",
    padding: theme.spacing(3),
    margin: theme.spacing(2),
    border: `1px solid ${theme.palette.divider}`,
    boxShadow: theme.shadows[3],
  },
  overallAutomationHeading: {
    textAlign: "center",
    fontWeight: "bold",
  },
  projectHeading: {
    textAlign: "center",
    fontWeight: "bold",
    padding: theme.spacing(2),
  },
  chartSize: {
    width: "400px",
    height: "250px",
  },
  cardContent: {
    display: "flex",
    flexDirection: "column",
    alignItems: "center",
    justifyContent: "center",
    width: "100%",
    padding: 0, // Remove padding
  },
}));

const Dashboard = () => {
  const classes = useStyles();
  const dispatch = useDispatch();
  const { userId } = useSelector((store) => store.auth);
  const { jiraIntegrationList } = useSelector((store) => store.dashboard);

  useEffect(() => {
    if (userId) {
      dispatch(getJiraDeatils(userId));
    }
  }, [dispatch, userId]);

  return (
    <Container className={classes.root}>
      <Grid container className={classes.topRow} spacing={2}>
        <Grid item xs={12} md={6} className={classes.column}>
          <Card className={`${classes.card} ${classes.overallAutomationCard}`}>
            <CardContent>
              <Typography
                variant="h4"
                component="h2"
                className={classes.overallAutomationHeading}
              >
                Overall Automation
              </Typography>
              {jiraIntegrationList.summary && (
                <OverallAutomation data={jiraIntegrationList.summary} />
              )}
            </CardContent>
          </Card>
        </Grid>
      </Grid>
      <Grid container className={classes.contentRow} spacing={2}>
        {jiraIntegrationList?.jira_projectsDetails
          ?.filter((project) => project.testCases.length > 0)
          .map((project) => (
            <Grid item key={project.id} xs={12} md={6} className={classes.column}>
              <Card className={classes.card}>
                <CardContent className={classes.cardContent}>
                  <Typography
                    variant="h5"
                    component="h2"
                    className={classes.projectHeading}
                  >
                    {project.name}
                  </Typography>
                  <div className={classes.chartContainer}>
                    <ReactApexChart
                      options={{
                        chart: {
                          type: "pie",
                          width: "100%",
                        },
                        labels: ["Automated", "Not Automated"],
                        responsive: [
                          {
                            breakpoint: 480,
                            options: {
                              chart: {
                                width: 400,
                              },
                              legend: {
                                position: "bottom",
                              },
                            },
                          },
                        ],
                      }}
                      series={[
                       (project.perAutomatedTestcases),
                       (project.perNotAutomatedTestcases),
                      ]}
                      type="pie"
                      className={classes.chartSize} 
                    />
                  </div>
                </CardContent>
              </Card>
            </Grid>
          ))}
      </Grid>
    </Container>
  );
};

export default Dashboard;
