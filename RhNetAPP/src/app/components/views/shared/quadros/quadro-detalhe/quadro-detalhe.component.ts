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

    atos_normativos: Ato_Normativo[] = [];
    ato_normativo_selecionado: Ato_Normativo;
    quadro: Quadro;
    ato_NormativoColumns: string[]=["numero", "descricao", "vigencia", "publicacao", "actions"];
    quadro_atos_normativos: [{id:1, numero: "123"}];

    constructor(private variable: Variables, private service: SharedService,
        private activatedRoute: ActivatedRoute) { }

    ngOnInit(): void {

        this.activatedRoute.params.subscribe(params => {
           
            let id = params['quadroId'];

            this.service.get('ato_normativo').subscribe(atos_normativos => {
                this.atos_normativos = atos_normativos;

                this.service.getById('Quadro', id).subscribe(results => {
                    this.quadro = results;
                    let ato_normativo: Ato_Normativo;

                    this.quadro.atos_Normativos.unshift(ato_normativo);
                },
                    (err) => {
                        console.log(err)
                    })
            },


                (err) => {
                    console.log(err)
                });
            
           

        });
    }

    addAtoNormativo(ato_normativo): void {

        this.variable.IsLoading = true;
        this.variable.IsEnabled = false;

        this.service.addWithUrl('quadro/addAtoNormativo', {quadro_Id: this.quadro.id  , ato_Normativo_Id: ato_normativo.id}).subscribe(results => {

            this.quadro.atos_Normativos.push(ato_normativo);

            
            this.variable.IsLoading = false;
            this.variable.IsEnabled = true;
            this.variable.showMessage('Ato Normativo adicionado com sucesso.');
        },
            (err) => {
                this.variable.IsLoading = false;
                this.variable.IsEnabled = true;
                if (err.error.errors !== undefined) {
                    let properties = Object.getOwnPropertyNames(err.error.errors);

                    var erros = '';
                    for (var e = 0; e < properties.length; e++) {
                        if (properties[e] !== 'length') {
                            erros += '- ' + err.error.errors[properties[e]] + '\n';
                        };
                    }
                    alert(erros);
                } else {
                    alert(err.error.message);
                }
                console.log(err)
            })
    }

    removeAtoNormativo(ato_normativo): void {

        this.variable.IsLoading = true;
        this.variable.IsEnabled = false;

        this.service.addWithUrl('quadro/removeAtoNormativo', { quadro_Id: this.quadro.id, ato_Normativo_Id: ato_normativo.id }).subscribe(results => {

            this.quadro.atos_Normativos.splice(ato_normativo, 1);


            this.variable.IsLoading = false;
            this.variable.IsEnabled = true;
            this.variable.showMessage('Ato Normativo removico com sucesso.');
        },
            (err) => {
                this.variable.IsLoading = false;
                this.variable.IsEnabled = true;
                if (err.error.errors !== undefined) {
                    let properties = Object.getOwnPropertyNames(err.error.errors);

                    var erros = '';
                    for (var e = 0; e < properties.length; e++) {
                        if (properties[e] !== 'length') {
                            erros += '- ' + err.error.errors[properties[e]] + '\n';
                        };
                    }
                    alert(erros);
                } else {
                    alert(err.error.message);
                }
                console.log(err)
            })
    }

}
