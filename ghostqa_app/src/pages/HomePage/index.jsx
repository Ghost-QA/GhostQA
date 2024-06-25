import React, { useEffect, useState } from "react";
import {
  Grid,
  Card,
  CardContent,
  Typography,
  CircularProgress,
} from "@material-ui/core";
import { makeStyles } from "@material-ui/core/styles";
import ReactApexChart from "react-apexcharts";
import OverallAutomation from "./charts/OverallAutomation";
import { useDispatch, useSelector } from "react-redux";
import {
  getJiraDeatils,
  getRecentsRunList,
} from "../../redux/actions/dashboardAction";
import RecentsTable from "./Recents";
import { useLocation } from "react-router-dom";
import { getPerformanceIntegrationList } from "../../redux/actions/settingAction";

const useStyles = makeStyles((theme) => ({
  root: {
    padding: theme.spacing(3),
    minHeight: "100vh",
    display: "flex",
  },
  leftSection: {
    flex: 1,
  },
  rightSection: {
    flex: 1,
  },
  topRow: {
    width: "100%",
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
    border: `1px solid ${theme.palette.divider}`,
    boxShadow: theme.shadows[3],
  },
  overallAutomationHeading: {
    textAlign: "center",
    fontWeight: "bold",
    fontSize: "16px",
    lineHeight: "21px",
    padding: "10px 22px",
  },
  overallHeading: {
    textAlign: "center",
    fontSize: "16px",
    lineHeight: "21px",
    padding: "10px 22px",
  },
  projectHeading: {
    textAlign: "center",
    fontWeight: "bold",
    fontSize: "16px",
    lineHeight: "21px",
    padding: "10px 22px",
  },
  chartSize: {
    width: "100%", // Adjusted to fit container width
    height: "200px",
  },
  cardContent: {
    display: "flex",
    flexDirection: "column",
    alignItems: "center",
    justifyContent: "center",
    width: "100%",
    padding: 0,
  },
  sectionHeader: {
    marginBottom: theme.spacing(2),
    textAlign: "center",
    fontWeight: "bold",
    fontSize: "16px",
    lineHeight: "21px",
    padding: "10px 22px",
  },
  loader: {
    display: "flex",
    justifyContent: "center",
    alignItems: "center",
    width: "100%",
    height: "100%",
    marginTop: "10px",
  },
}));

const Dashboard = () => {
  const classes = useStyles();
  const dispatch = useDispatch();
  const { userId } = useSelector((store) => store.auth);
  const location = useLocation();
  const { jiraIntegrationList, recentsRunsList } = useSelector(
    (store) => store.dashboard
  );
  const { performanceIntegration } = useSelector((state) => state.settings);
  const [inprogress, setInProgress] = useState(null);

  useEffect(() => {
    if (userId) {
      dispatch(getJiraDeatils(userId));
      dispatch(getRecentsRunList());
    }
  }, [dispatch, userId]);

  useEffect(() => {
    if (userId) {
      dispatch(getPerformanceIntegrationList(userId, setInProgress));
    }
  }, [userId, dispatch]);

  const isJiraIntegrated = performanceIntegration?.some(
    (integration) => integration.AppName === "Jira" && integration.IsIntegrated
  );

  return (
    <div className={classes.root}>
      <Grid container spacing={2}>
        <Grid item xs={12} md={6} className={classes.leftSection}>
          <Typography className={classes.sectionHeader}>
            Automation Coverage
          </Typography>
          <Grid container className={classes.topRow} spacing={2}>
            <Grid item xs={12} className={classes.column}>
              <Card
                className={`${classes.card} ${classes.overallAutomationCard}`}
              >
                <CardContent className={classes.cardContent}>
                  {inprogress !== null &&
                    inprogress !== undefined &&
                    !inprogress && (
                      <>
                        {isJiraIntegrated ? (
                          <>
                            <Typography
                              variant="h4"
                              component="h2"
                              className={classes.overallAutomationHeading}
                            >
                              Overall Automation
                            </Typography>
                            {jiraIntegrationList.summary && (
                              <OverallAutomation
                                data={jiraIntegrationList.summary}
                              />
                            )}
                          </>
                        ) : (
                          <Typography
                            variant="h4"
                            component="h2"
                            className={classes.overallHeading}
                          >
                            Please enable Test Management (JIRA/ADO etc.) under
                            Settings -&gt; Functional -&gt; Local Testing -&gt;
                            Integration
                          </Typography>
                        )}
                      </>
                    )}

                  {inprogress && (
                    <div className={classes.loader}>
                      <CircularProgress />
                    </div>
                  )}
                </CardContent>
              </Card>
            </Grid>
          </Grid>
          <Grid container className={classes.contentRow} spacing={2}>
            {!inprogress &&
              isJiraIntegrated &&
              jiraIntegrationList?.jira_projectsDetails
                ?.filter((project) => project.testCases.length > 0)
                .map((project) => (
                  <Grid
                    item
                    key={project.id}
                    xs={12}
                    md={6}
                    className={classes.column}
                  >
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
                                toolbar: {
                                  show: false,
                                },
                              },
                              labels: ["Automated", "Not Automated"],
                              dataLabels: {
                                enabled: true,
                                formatter: function (val, opts) {
                                  const total = opts.w.globals.series.reduce(
                                    (a, b) => a + b,
                                    0
                                  );
                                  const percentage =
                                    ((val / total) * 100).toFixed(1) + "%";
                                  return percentage;
                                },
                                offsetX: -10,
                                offsetY: -10,
                                style: {
                                  colors: ["#fff"],
                                  fontSize: "12px",
                                  fontFamily: "Helvetica, Arial, sans-serif",
                                  fontWeight: "600",
                                },
                                background: {
                                  enabled: true,
                                  foreColor: "#000",
                                  borderWidth: 1,
                                  borderColor: "#ccc",
                                  opacity: 0.85,
                                  padding: 4, // Adjust padding here
                                  dropShadow: {
                                    enabled: false,
                                  },
                                },
                              },
                              plotOptions: {
                                pie: {
                                  dataLabels: {
                                    offset: -20,
                                    minAngleToShowLabel: 10,
                                  },
                                },
                              },
                              responsive: [
                                {
                                  breakpoint: 480,
                                  options: {
                                    chart: {
                                      width: 450,
                                    },
                                    legend: {
                                      position: "bottom",
                                    },
                                  },
                                },
                              ],
                              tooltip: {
                                y: {
                                  formatter: function (val) {
                                    return val.toFixed(1) + "%";
                                  },
                                },
                              },
                            }}
                            series={[
                              project.perAutomatedTestcases,
                              project.perNotAutomatedTestcases,
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
        </Grid>
        <Grid item xs={12} md={6} className={classes.rightSection}>
          <Typography className={classes.sectionHeader}>Recent Runs</Typography>
          <Card className={classes.card}>
            <CardContent className={classes.cardContent}>
              <RecentsTable data={recentsRunsList} />
            </CardContent>
          </Card>
        </Grid>
      </Grid>
    </div>
  );
};

export default Dashboard;
