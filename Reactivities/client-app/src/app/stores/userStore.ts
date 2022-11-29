import { makeAutoObservable, runInAction } from "mobx";
import { history } from "../..";
import agent from "../api/agent";
import { User, UserFormValues } from "../models/user";
import { store } from "./store";

export default class UserStore {
    user: User | null = null;

    constructor() {
        makeAutoObservable(this)
    }

    get isLoggedIn() {
        return !!this.user
    }

    login = async (creds: UserFormValues) => {
        try {
            const user = await agent.Account.login(creds);
            store.commonStore.setToken(user.token); //update the token on the browser
            runInAction(() => this.user = user); //runInAction used becuse any modification to the state must happen inside of an action.
            history.push('/activities')
            store.modalStore.closeModal();
        } catch (error) {
            throw error;
        }
    }

    logout = () => {
        store.commonStore.setToken(null); //remove token from the store
        window.localStorage.removeItem('jwt'); //remove token from browser
        this.user = null
        history.push('/')
    }

    getUser = async () => {
        try {
            const user = await agent.Account.current();
            runInAction(() => this.user = user);
        } catch (error) {
            console.log(error);
        }
    }

    register = async(creds :UserFormValues)=>{
        try {
            const user = await agent.Account.register(creds);
            store.commonStore.setToken(user.token); 
            runInAction(() => this.user = user);
            history.push('/activities')
            store.modalStore.closeModal();
        } catch (error) {
            throw error;
        }
    }

}