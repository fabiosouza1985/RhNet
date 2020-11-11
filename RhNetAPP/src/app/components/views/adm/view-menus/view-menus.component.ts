import { Component, OnInit } from '@angular/core';
import { Profile } from 'src/app/components/models/adm/profile.model';
import {UserService} from 'src/app/components/services/adm/user.service';
import {MenuItem} from 'src/app/components/models/adm/menuItem.model';
import {Menu} from 'src/app/components/models/adm/menu.model';

@Component({
  selector: 'app-view-menus',
  templateUrl: './view-menus.component.html',
  styleUrls: ['./view-menus.component.css']
})
export class ViewMenusComponent implements OnInit {

  profiles: Profile[] = [];
  
  navItems: MenuItem[] = [];

  menus: Menu[];
  displayedColumns: string[] = ["header"];
  constructor(private service: UserService) { }

  ngOnInit(): void {
    
    this.service.getAllMenus().subscribe(results_1 => {
      this.menus = results_1;
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

  getMenus(profile) : Menu[]{
    var _menus = [];
    
    for(var i =0; i < this.menus.length; i++){
      if (this.menus[i].role_name == profile){
        _menus.push(this.menus[i]);
      }
    }

      return _menus;
  }
}
