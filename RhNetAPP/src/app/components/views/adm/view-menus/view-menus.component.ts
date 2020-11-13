import { Component, OnInit } from '@angular/core';
import { Profile } from 'src/app/components/models/adm/profile.model';
import { UserService } from 'src/app/components/services/adm/user.service';
import { MenuService } from 'src/app/components/services/adm/menu.service';
import {Menu} from 'src/app/components/models/adm/menu.model';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-view-menus',
  templateUrl: './view-menus.component.html',
  styleUrls: ['./view-menus.component.css']
})
export class ViewMenusComponent implements OnInit {
    ds = new MatTableDataSource();
    profiles: Profile[] = [];
  

    menus: Menu[];

    displayedColumns: string[] = ["role_Name", "header", "path", "permission_Name", "actions"];
    constructor(private service: UserService, private menuService: MenuService) { }
    
  ngOnInit(): void {

    this.service.getAllRoles().subscribe(results => {
      this.profiles = results;

        this.menuService.getAllMenus().subscribe(results_1 => {

            this.menus = results_1;

            let newMenu = {} as Menu;
            newMenu.id = 0;
            newMenu.header = '';
            newMenu.path = '';
            newMenu.role_Name = '';
            newMenu.permission_Name = '';

            this.menus.splice(0, 0, newMenu);
            this.ds = new MatTableDataSource(this.menus);
        },
            (err) => {
                console.log(err)
            })
    },
    (err) => {           
      console.log(err)})
  } 

    addMenu(menu: Menu): void {
        
        let newMenu = {} as Menu;
        newMenu.id = 0;
        newMenu.header = menu.header;
        newMenu.path = menu.path;
        newMenu.role_Name = menu.role_Name;
        newMenu.permission_Name = menu.permission_Name;

        this.menuService.addMenu(newMenu).subscribe(results => {
            menu.header = '';
            menu.path = '';
            menu.role_Name = '';
            menu.permission_Name = '';

            this.menus.push(results);
            this.ds.data = this.menus;
        },
            (err) => {
                console.log(err)
            })    

       
    }

    updateMenu(menu: Menu): void {
        this.menuService.updateMenu(menu).subscribe(results => {
            alert('Menu atualizado');
        },
            (err) => {
                console.log(err)
            })    
    }

    removeMenu(menu: Menu): void {
        this.menuService.removeMenu(menu).subscribe(results => {
            this.menus.splice(this.menus.indexOf(menu), 1);
            this.ds.data = this.menus;
        },
            (err) => {
                console.log(err)
            })   
       
    }
}
