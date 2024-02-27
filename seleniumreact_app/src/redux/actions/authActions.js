import axios from "axios";
import { toast } from "react-toastify";
export const LOG_IN = "LOG_IN";
export const LOG_OUT = "LOG_OUT";
const BASE_URL = process.env.REACT_APP_BASE_URL;

export const login = (data, setLoading) => {
  return async (dispatch) => {
    try {
      setLoading(true);

      const res = await axios.post(`${BASE_URL}/Login`, data);
      const response = res.data;
      if (response?.result === 'Success') {
        toast.info('Successfully logged in', {
          style: {
            background: 'rgb(101, 77, 247)',
            color: 'rgb(255, 255, 255)',
          },
        });
        sessionStorage.setItem(
          "userData",
          JSON.stringify({ ...response.data, token: response.token })
        );
        sessionStorage.setItem('email',data.email.toString())
        dispatch({
          type: LOG_IN,
          payload: { ...response.data, token: response.token },
        });
      }else{
        toast.error(response.message)
      }
    } catch (err) {
      console.error(err);
      toast.error("NETWORK ERROR");
    } finally {
      setLoading(false);
    }
  };
};

export const logout = () => {
  return (dispatch) => {
    dispatch({ type: LOG_OUT });
  };
};


export const InviteUser = (email)=>{
  
  return async (dispatch)=>{
    try {
      const res = await axios.post(
        `${BASE_URL}/AddInBuildTestSuite/InviteUser?toEmail=${email}`,email);
      console.log('response ' ,res)
      if (res.status === 204) {
        toast.info('Successfully invited', {
          style: {
            background: 'rgb(101, 77, 247)', 
            color: 'rgb(255, 255, 255)', 
          },
        });
    } 
    }catch (error) {
      console.log("error inviting ",error);
      toast('Invitation fail')
    }
  }
}

export const AcceptInvitation = (email)=>{
  
  return async (dispatch)=>{
    try {
      const res = await axios.post(
        `${BASE_URL}/AddInBuildTestSuite/AcceptInvitation?toEmail=${email}`,email);
      console.log('response ' ,res)
      if (res.status === 204) {
        toast.info('Successfully accept', {
          style: {
            background: 'rgb(101, 77, 247)', 
            color: 'rgb(255, 255, 255)', 
          },
        });
    } 
    }catch (error) {
      console.log("error inviting ",error);
      toast('accept fail')
    }
  }
}
