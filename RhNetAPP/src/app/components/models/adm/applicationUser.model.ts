import { UserRole } from 'src/app/components/models/adm/userRole.model';
import { Client } from 'src/app/components/models/adm/client.model';
import { UserPermission } from 'src/app/components/models/adm/userPermission.model';

export interface ApplicationUser {
	userId: string,
	cpf: string,
	userName: string,
	email: string,	
	applicationRoles: UserRole[],
	clients: Client[],
	permissions: UserPermission[]
}