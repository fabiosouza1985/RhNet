import {Profile} from 'src/app/components/models/adm/profile.model';

export interface User {
	username: string, 
	email: string,
	token: string,
	profiles: Profile[]
}