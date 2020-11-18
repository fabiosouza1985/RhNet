import { ClientSituation } from "src/app/components/enums/clientSituation.enum";

export interface Client {
    id: number,
    description: string;
    cnpj: string;
    situation: ClientSituation;
}