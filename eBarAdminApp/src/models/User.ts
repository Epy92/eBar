export class User {
    email: string;
    name:string;
    username:string;
    sessionKey: string;
  
    constructor(email: string, sessionKey: string, name:string, username:string) {
      this.email = email;
      this.sessionKey = sessionKey;
      this.name = name;
      this.username = username;
    }
  }