import { Component, OnInit } from '@angular/core';
import { Profile } from 'src/app/components/models/adm/profile.model';
import { UserService } from 'src/app/components/services/adm/user.service';
import { QuickAccessService } from 'src/app/components/services/adm/quick-access.service';
import { QuickAccess } from 'src/app/components/models/adm/quickAccess.model';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-quick-access',
  templateUrl: './quick-access.component.html',
  styleUrls: ['./quick-access.component.css']
})
export class QuickAccessComponent implements OnInit {

    ds = new MatTableDataSource();
    profiles: Profile[] = [];


    quickAccess: QuickAccess[];
        
    displayedColumns: string[] = ["role_Name", "header", "path", "permission_Name", "actions"];
    constructor(private service: UserService, private quickAccessService: QuickAccessService) { }

    ngOnInit(): void {

        this.service.getAllRoles().subscribe(results => {
            this.profiles = results;

            this.quickAccessService.getAllQuickAccess().subscribe(results_1 => {

                this.quickAccess = results_1;

                let newQuickAccess = {} as QuickAccess;
                newQuickAccess.id = 0;
                newQuickAccess.header = '';
                newQuickAccess.path = '';
                newQuickAccess.role_Name = '';
                newQuickAccess.permission_Name = '';

                this.quickAccess.splice(0, 0, newQuickAccess);
                this.ds = new MatTableDataSource(this.quickAccess);
            },
                (err) => {
                    console.log(err)
                })
        },
            (err) => {
                console.log(err)
            })
    }

    addQuickAccess(quickAccess: QuickAccess): void {

        let newQuickAccess = {} as QuickAccess;
        newQuickAccess.id = 0;
        newQuickAccess.header = quickAccess.header;
        newQuickAccess.path = quickAccess.path;
        newQuickAccess.role_Name = quickAccess.role_Name;
        newQuickAccess.permission_Name = quickAccess.permission_Name;

        this.quickAccessService.addQuickAccess(newQuickAccess).subscribe(results => {
            quickAccess.header = '';
            quickAccess.path = '';
            quickAccess.role_Name = '';
            quickAccess.permission_Name = '';

            this.quickAccess.push(results);
            this.ds.data = this.quickAccess;
        },
            (err) => {
                console.log(err)
            })


    }

    updateQuickAccess(quickAccess: QuickAccess): void {
        this.quickAccessService.updateQuickAccess(quickAccess).subscribe(results => {
            alert('QuickAccess atualizado');
        },
            (err) => {
                console.log(err)
            })
    }

    removeQuickAccess(quickAccess: QuickAccess): void {
        this.quickAccessService.removeQuickAccess(quickAccess).subscribe(results => {
            this.quickAccess.splice(this.quickAccess.indexOf(quickAccess), 1);
            this.ds.data = this.quickAccess;
        },
            (err) => {
                console.log(err)
            })

    }

}
