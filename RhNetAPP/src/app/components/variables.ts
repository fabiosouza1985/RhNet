import { Injectable } from '@angular/core';
import {UserService} from 'src/app/components/services/adm/user.service'
import {Profile} from 'src/app/components/models/adm/profile.model';
import { MenuService } from 'src/app/components/services/adm/menu.service';
import { MenuItem } from 'src/app/components/models/adm/menuItem.model';

@Injectable({
    providedIn: 'root'
  })
export class Variables {
    
    
    
    public Username: String = "";
    public Profiles: Profile[] = [];
    public CurrentProfile = "";
    public MenuItems: MenuItem[] = [];

    constructor(private service: UserService, private menuService: MenuService){
        if(localStorage.getItem("username") !== null && localStorage.getItem("username") !== undefined){
            this.Username = localStorage.getItem("username");

            this.service.getRoles().subscribe(results => {     
                this.Profiles = results;      
                if(localStorage.getItem("currentProfile") === null || localStorage.getItem("username") === undefined){
                    localStorage.setItem("currentProfile", this.Profiles[0].name);                    
                }       
                this.CurrentProfile = localStorage.getItem("currentProfile");
                this.GetMenus();
            },
              (err) => {     
                
                console.log(err);   
            });
        }

        
    }

    public GetMenus(): void {
        if (this.CurrentProfile == "") {
            return;
        }

        this.menuService.getMenus(this.CurrentProfile).subscribe(results => {
            var _menus = results;

            for (var i = 0; i < _menus.length; i++) {
               
               var _menu_headers = _menus[i].header.split("\\");
                if (_menu_headers.length === 1) {
                    this.MenuItems.push({
                        header: _menus[i].header,
                        path: _menus[i].path
                    });
                } else {
                    var index = this.MenuItems.map(function (e) { return e.header }).indexOf(_menu_headers[0]);

                    if (index < 0) {
                        this.MenuItems.push({
                            header: _menu_headers[0]
                        });
                        index = this.MenuItems.map(function (e) { return e.header }).indexOf(_menu_headers[0]);
                    }

                    this.MenuItems[index].children = [{
                        header: _menu_headers[1],
                        path: _menus[i].path
                    }]
                }                
            }
        },
            (err) => {

                console.log(err);
            });
    }
}