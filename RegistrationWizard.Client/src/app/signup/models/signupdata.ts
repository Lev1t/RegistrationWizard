import { Credentials } from "./credentials";
import { Residence } from "./residence";

export class SignupData {
    public login: string;
    public password: string;
    public confirmPassword: string;
    public isAgreeToWorkForFood: boolean;
    public countryId: number;
    public provinceId: number;

    constructor(credentials: Credentials, residence: Residence) {
        this.login = credentials.login;
        this.password = credentials.password;
        this.confirmPassword = credentials.confirmPassword;
        this.isAgreeToWorkForFood = credentials.isAgreesToWorkForFood;
        this.countryId = residence.countryId;
        this.provinceId = residence.provinceId;
    }
}
