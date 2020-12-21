import { Ato_Normativo } from 'src/app/components/models/shared/ato_normativo.model';
import { Subquadro } from 'src/app/components/models/shared/subquadro.model';

export interface Quadro {
    id: number,
    descricao: string,
    sigla: string,
    atos_Normativos: Ato_Normativo[],
    subquadros: Subquadro[]
}