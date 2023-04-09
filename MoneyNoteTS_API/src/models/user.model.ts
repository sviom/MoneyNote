import { ICommon } from './common.model';

class User implements ICommon {
    public guid: string;

    constructor() {
        this.guid = '';
    }
}

export default User;
