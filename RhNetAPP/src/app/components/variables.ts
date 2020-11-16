import { Injectable } from '@angular/core';
import {UserService} from 'src/app/components/services/adm/user.service'
import {Profile} from 'src/app/components/models/adm/profile.model';
import { MenuService } from 'src/app/components/services/adm/menu.service';
import { MenuItem } from 'src/app/components/models/adm/menuItem.model';
import { Menu } from 'src/app/components/models/adm/menu.model';

@Injectable({
    providedIn: 'root'
  })
export class Variables {
    
    
    
    public Username: String = "";
    public Profiles: Profile[] = [];
    public CurrentProfile = "";
    public MenuItems: MenuItem[] = [];
    public QuickAccess: Menu[] = [];

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
                this.GetQuickAccess();
            },
              (err) => {     
                
                console.log(err);   
            });
        }

        
    }

    public GetMenus(): void {
        this.MenuItems = [];
        if (this.CurrentProfile == "") {
            return;
        }

        this.menuService.getMenus(this.CurrentProfile).subscribe(results => {
            var _menus = results;
            this.MenuItems.push({ header: '', children: [] });

            for (var i = 0; i < _menus.length; i++) {
                let _Menu = {} as MenuItem;
                

                var _menu_headers = _menus[i].header.split("\\");

                for (var x = 0; x < _menu_headers.length; x++) {
                    _Menu.header = _menu_headers[x];

                    var index = -1;
                    if (x === 0) {
                        index = this.MenuItems[0].children.map(function (e) { return e.header }).indexOf(_menu_headers[x]);
                        if (index < 0) {
                            this.MenuItems[0].children.push(_Menu);
                        }
                    } else {
                        if (_Menu.children !== undefined) {
                            index = _Menu.children.map(function (e) { return e.header }).indexOf(_menu_headers[x]);
                        }                        
                    }
                    

                    if (index < 0) {
                        
                        if (x + 1 === _menu_headers.length) {
                            _Menu.path = _menus[i].path;
                        } else {
                            if (_Menu.children === undefined) {
                                _Menu.children = [];
                            }


                            let _Menu_2 = {} as MenuItem;
                            _Menu.children.push(_Menu_2);
                            _Menu = _Menu_2;
                        }
                    } else {
                        if (x === 0) {

                            if (x + 1 === _menu_headers.length) {
                                _Menu.path = _menus[i].path;
                            } else {
                                if (_Menu.children === undefined) {
                                    _Menu.children = [];
                                }

                                let _Menu_2 = {} as MenuItem;
                                this.MenuItems[0].children[index].children.push(_Menu_2);
                                _Menu = _Menu_2;
                            }

                          
                        } else {
                            if (x + 1 === _menu_headers.length) {
                                _Menu.path = _menus[i].path;
                            } else {
                                if (_Menu.children === undefined) {
                                    _Menu.children = [];
                                }


                                let _Menu_2 = {} as MenuItem;
                                _Menu.children[index].children.push(_Menu_2);
                                _Menu = _Menu_2;
                            }
                        }
                    }
                    
                }
              
            }
        },
            (err) => {

                console.log(err);
            });
    }

    public GetQuickAccess(): void {
        this.QuickAccess = [];
        if (this.CurrentProfile == "") {
            return;
        }

        this.menuService.getQuickAccess(this.CurrentProfile).subscribe(results => {
            this.QuickAccess = results;
        },
            (err) => {

                console.log(err);
            });
    }

    public Logoff(): void {
        this.Username = '';
        this.CurrentProfile = '';
        this.MenuItems = [];
        this.Profiles = [];
        this.QuickAccess = [];
        localStorage.clear();
    }
}