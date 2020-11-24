import {Profile} from 'src/app/components/models/adm/profile.model';
import { Client } from 'src/app/components/models/adm/client.model';
export interface User {
	username: string, 
	email: string,
	token: string,
	profiles: Profile[],
	currentClient: Client
}