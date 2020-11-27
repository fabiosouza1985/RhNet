export interface Property {
    name: string,
    description: string,
    order: number,
    required: boolean,
    autoGenerateField: boolean,
    autoGenerateFilter: boolean,
    minimum: number,
    maximum: number,
    readOnly: boolean

}
