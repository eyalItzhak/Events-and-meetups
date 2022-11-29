import { makeAutoObservable, reaction } from "mobx";
import { ServerError } from "../models/serverError";

export default class CommonStore {
    error: ServerError | null = null;
    token: string | null = window.localStorage.getItem('jwt'); //if token not found in local storage we get null
    appLoaded = false;

    constructor() {
        makeAutoObservable(this);

        reaction(
            () => this.token, //what we react to => run only when there is change on this parameter
            token => {
                if (token) { //what we do if token found
                    window.localStorage.setItem('jwt', token)
                } else {
                    window.localStorage.removeItem('jwt')
                }
            }

        )
    }

    setServerError = (error: ServerError) => {
        this.error = error;
    }

    setToken = (token: string | null) => {
        this.token = token; //token change so reaction func run...
    }

    setAppLoaded = () => {
        this.appLoaded = true;
    }
}