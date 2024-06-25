import axios from "axios";
import { toast } from "react-toastify";
import { header } from "../../utils/authheader";
import { getBaseUrl } from "../../utils/configService";
export const GET_JIRA_INTEGRATION_LIST = "GET_JIRA_INTEGRATION_LIST";
export const GET_RECENTS_RUNS = "GET_RECENTS_RUNS";


export const getJiraDeatils = (userId) => {
    return async (dispatch) => {
      try {
        const BASE_URL = await getBaseUrl();
        const response = await axios.get(
          `${BASE_URL}/JiraIntegration/GetProjectDetailswithTestCase?userId=${userId}`,
          header()
        );
        console.log("GET_JIRA_INTEGRATION_LIST : ", response);
        dispatch({
          type: GET_JIRA_INTEGRATION_LIST,
          payload: response.data,
        });
      } catch (error) {
        // toast.error("NETWORK ERROR");
        console.log("error",error)
      }
    };
  };

  export const getRecentsRunList = () => {
    return async (dispatch) => {
      try {
        const BASE_URL = await getBaseUrl();
        const response = await axios.get(
          `${BASE_URL}/JiraIntegration/GetRecentSuiteRunData`,
          header()
        );
        console.log("GET_RECENTS_RUNS : ", response);
        dispatch({
          type: GET_RECENTS_RUNS,
          payload: response.data,
        });
      } catch (error) {
        toast.error("NETWORK ERROR");
      }
    };
  };