import { Component, OnInit } from '@angular/core';
import { Profile } from 'src/app/components/models/adm/profile.model';
import { UserService } from 'src/app/components/services/adm/user.service';
import { MenuService } from 'src/app/components/services/adm/menu.service';
import {MenuItem} from 'src/app/components/models/adm/menuItem.model';
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
  
  navItems: MenuItem[] = [];

    menus: Menu[];

    menu: Menu = {id:0,  header:'', permission_Name: '', role_Name: ''};
    displayedColumns: string[] = ["role_Name", "header", "path", "permission_Name", "actions"];
    constructor(private service: UserService, private menuService: MenuService) { }
    
  ngOnInit(): void {

      this.menuService.getAllMenus().subscribe(results_1 => {
          
          this.menus = results_1;

          let newMenu = {} as Menu;
          newMenu.id = 0;
          newMenu.header = '';
          newMenu.path = '';
          newMenu.role_Name = '';
          newMenu.permission_Name = '';

          this.menus.splice(0,0,newMenu);
          this.ds = new MatTableDataSource(this.menus);
        },
        (err) => {           
         console.log(err)})
      
    this.service.getRoles().subscribe(results => {
      this.profiles = results;
             

        
      this.navItems =  [
        {header:"Menu 01", path: '',
          children: [
            {header: 'Menu 01.1', path: 'login', children: []},
            {header: 'Menu 01.2', path: 'home', children: []}
          ]
        },
        {header:"Menu 02",path: '',
          children: [
            {header: 'Menu 02.1', path: 'login', children: []},
            {header: 'Menu 02.2', path: 'home', children: []},
            {header: 'Menu 02.3', path: '', children: [
              {header: 'Menu 02.3.1', path: 'home', children: []}
            ]}
    
          ]
        },
        {header: "Menu 03", path: '/login', children: []}
      ];
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
