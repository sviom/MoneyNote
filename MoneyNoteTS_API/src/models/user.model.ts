import { ICommon } from './common.model';

class User implements ICommon {
    public guid: string;

    public name: string;

    public email: string;

    public password: string;

    constructor() {
        this.guid = '';
    }
}

export default User;
