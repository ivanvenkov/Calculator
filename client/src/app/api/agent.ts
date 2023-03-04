import axios, { AxiosError, AxiosResponse } from "axios";

axios.defaults.baseURL = "http://localhost:5000/api/calculator";
axios.defaults.withCredentials = true;
axios.defaults.headers.post["Content-Type"] = "application/json;charset=utf-8";

const responseBody = (response: AxiosResponse) => response.data;

axios.interceptors.response.use(
  async (response) => {
    return response;
  },
  (error: AxiosError) => {
    console.log("intercepted by axios interceptor");
    const { data, status } = error.response as AxiosResponse;

    switch (status) {
      case 400:
        if (data.errorMsg) {          
          const modelStateErrors: string = data.errorMsg;
          throw modelStateErrors;
        }else if(data.title){
          const modelStateErrors: string = data.title;
          throw modelStateErrors;
        }
        break;
      case 500:      
        throw error.response;
        break;
      default:
        break;
    }

    return Promise.reject(error.response);
  }
);

const requests = {
  get: (url: string, params?: URLSearchParams) =>
    axios.get(url, { params }).then(responseBody),
  post: (url: string, body: {}) => axios.post(url, body).then(responseBody),
};

const Calculator = {
  calculate: (values: any) => requests.post("calculate", values),
};

const agent = {
  Calculator
};
export default agent;
