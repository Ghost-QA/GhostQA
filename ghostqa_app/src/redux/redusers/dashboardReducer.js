import { GET_JIRA_INTEGRATION_LIST, GET_RECENTS_RUNS } from "../actions/dashboardAction";
  
  const initialState = {
    jiraIntegrationList: [],
    recentsRunsList:[]
  };
  
  const dashboardReducer = (state = initialState, action) => {
    switch (action.type) {
      case GET_JIRA_INTEGRATION_LIST: {
        return {
          ...state,
          jiraIntegrationList: action.payload,
        };
      }
      case GET_RECENTS_RUNS: {
        return {
          ...state,
          recentsRunsList: action.payload
        }
      }
      default:
        return state;
    }
  };
  export default dashboardReducer;