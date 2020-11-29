export interface Ato_Normativo {
    id: number,
    descricao: string,
    numero: number,
    ano: number,
    vigencia_Data: Date,
    vigencia_Publicacao?: Date,
    tipo_de_Ato_Normativo_Id: number,
    tipo_de_Ato_Normativo_Descricao: string,
}