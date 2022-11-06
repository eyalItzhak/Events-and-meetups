//all the rquest that go to the server
import axios, { AxiosResponse } from "axios";
import { Activity } from "../models/activity";


//############################################
//feach data from real server simolation
const sleep = (delay: number) =>{
    return new Promise((resolve)=>{
        setTimeout(resolve , delay)
    })
}

axios.interceptors.response.use(async response => {
   try {
        await sleep(1000);
        return response;
    } catch (error) {
        console.log(error);
        return await Promise.reject(error);
    }
})
//############################################


axios.defaults.baseURL = "http://localhost:5000/api" //baseURL= Prefix of the url



const responseBody = <T>(response: AxiosResponse<T>) => response.data; //get the data and we dont need to use response.data everwere...
//<T> is for genric type of var
const request = {  //url=Suffix of the url || here  axios.defaults.baseURL use  http://localhost:5000/api before {url}
    get: <T>(url: string) => axios.get<T>(url).then(responseBody),
    post: <T>(url: string, body: {}) => axios.post<T>(url, body).then(responseBody),
    put: <T>(url: string, body: {}) => axios.put<T>(url, body).then(responseBody),
    del: <T>(url: string) => axios.delete<T>(url).then(responseBody),
}

const Activities = {
    list: () => request.get<Activity[]>("/activities"), //here <T> become Activity[]
    details : (id : string) => request.get<Activity>(`/activities/${id}`),
    create : (activity : Activity) => request.post<void>('/activities',activity),
    update : (activity : Activity) => request.put<void>(`/activities/${activity.id}`,activity),
    delete : (id:string) => request.del<void>(`/activities/${id}`)
}

const agent = {
    Activities
}

export default agent;