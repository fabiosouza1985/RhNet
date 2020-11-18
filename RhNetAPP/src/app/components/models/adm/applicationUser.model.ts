import { Profile } from 'src/app/components/models/adm/profile.model';
import { Client } from 'src/app/components/models/adm/client.model';
import { Permission } from 'src/app/components/models/adm/permission.model';

export interface ApplicationUser {
	userId: string,
	cpf: string,
	userName: string,
	email: string,	
	applicationRoles: Profile[],
	clients: Client[],
	permissions: Permission[]
}