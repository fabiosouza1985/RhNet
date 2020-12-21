import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/components/services/shared/shared.service';
import { Variables } from 'src/app/components/variables';
import { Router, ActivatedRoute } from '@angular/router';
import { Quadro } from 'src/app/components/models/shared/quadro.model';
import { Ato_Normativo } from '../../../../models/shared/ato_normativo.model';

@Component({
  selector: 'app-quadro-detalhe',
  templateUrl: './quadro-detalhe.component.html',
  styleUrls: ['./quadro-detalhe.component.css']
})
export class QuadroDetalheComponent implements OnInit {

    quadro: Quadro;
    ato_NormativoColumns: string[]=["numero", "descricao", "vigencia", "publicacao", "actions"];
    quadro_atos_normativos: [{id:1, numero: "123"}];

    constructor(private variable: Variables, private service: SharedService,
        private activatedRoute: ActivatedRoute) { }

    ngOnInit(): void {

        this.activatedRoute.params.subscribe(params => {
           
            let id = params['quadroId'];

            this.service.getById('Quadro', id).subscribe(results => {                
                this.quadro = results;
                let ato_normativo: Ato_Normativo = {
                    id: 0,
                    descricao: '',
                    ano: 0,
                    numero: 0,
                    tipo_de_Ato_Normativo_Descricao: '',
                    tipo_de_Ato_Normativo_Id: 0,
                    vigencia_Data: new Date(),
                    vigencia_Publicacao: new Date()
                };
                this.quadro.atos_Normativos.push(ato_normativo);
            },
                (err) => {
                    console.log(err)
                })

        });
  }

}
