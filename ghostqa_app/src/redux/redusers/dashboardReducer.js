import { GET_JIRA_INTEGRATION_LIST } from "../actions/dashboardAction";
  
  const initialState = {
    jiraIntegrationList: [],
  };
  
  const dashboardReducer = (state = initialState, action) => {
    switch (action.type) {
      case GET_JIRA_INTEGRATION_LIST: {
        return {
          ...state,
          jiraIntegrationList: action.payload,
        };
      }
      default:
        return state;
    }
  };
  export default dashboardReducer;