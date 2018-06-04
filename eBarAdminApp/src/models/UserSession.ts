import { User } from "./User";

export class UserSession {
    static User:User;

    static getUser():User
    {
        return this.User;
    }

    static setUser(user:User)
    {
        this.User = user;
    }

    static deleteUserData()
    {
        this.User = null;
    }
  }