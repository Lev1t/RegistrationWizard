export class Credentials {
    public login: string;
    public password: string;
    public confirmPassword: string;
    public isAgreesToWorkForFood: boolean;

    constructor(login: string, password: string, confirmPassowrd: string, isAgreesToWorkForFood: boolean) {
        this.login = login;
        this.password = password;
        this.confirmPassword = confirmPassowrd;
        this.isAgreesToWorkForFood = isAgreesToWorkForFood;
    }
}
