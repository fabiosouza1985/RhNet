import { Injectable } from '@angular/core';
import {UserService} from 'src/app/components/services/adm/user.service'
import {Profile} from 'src/app/components/models/adm/profile.model';
import { MenuService } from 'src/app/components/services/adm/menu.service';
import { FavoriteService } from 'src/app/components/services/adm/favorite.service';
import { MenuItem } from 'src/app/components/models/adm/menuItem.model';
import { Favorite } from 'src/app/components/models/adm/favorite.model';
import { Menu } from 'src/app/components/models/adm/menu.model';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
    providedIn: 'root'
  })
export class Variables {
    
    
    public IsLoading: boolean = false;
    public IsEnabled: boolean = true;
    public Username: String = "";
    public Profiles: Profile[] = [];
    public CurrentProfile = "";
    public MenuItems: MenuItem[] = [];
    public QuickAccess: Menu[] = [];
    public Favorites: Favorite[] = [];

    constructor(private service: UserService, private menuService: MenuService, private favoriteService : FavoriteService, private _snackBar: MatSnackBar){
        if(localStorage.getItem("username") !== null && localStorage.getItem("username") !== undefined){
            this.IsLoading = true;
            this.Username = localStorage.getItem("username");

            this.service.getRoles().subscribe(results => {     
                this.Profiles = results;      
                if(localStorage.getItem("currentProfile") === null || localStorage.getItem("username") === undefined){
                    localStorage.setItem("currentProfile", this.Profiles[0].name);                    
                }       
                this.CurrentProfile = localStorage.getItem("currentProfile");
                this.GetMenus();
                this.GetQuickAccess();
                this.GetFavorites();
                this.IsLoading = false;
            },
              (err) => {     
                  this.IsLoading = false;
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

    public GetFavorites(): void {
        this.Favorites = [];
        if (this.CurrentProfile == "") {
            return;
        }

        this.favoriteService.getFavorites(this.CurrentProfile).subscribe(results => {
            this.Favorites = results;
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
        this.Favorites = [];
        localStorage.clear();
    }

    public showMessage(message: string) {
        this._snackBar.open(message, 'X', {
            duration: 2000,
        });
    }

    public addRemoveFavorite(add: boolean, path: string): void{
        this.IsLoading = true;
        if (add) {
            this.favoriteService.addFavorite(path).subscribe(results => {

                this._snackBar.open('Adicionado ao favorito', 'X', {
                    duration: 2000,
                });
                this.Favorites.push(results);
                this.IsLoading = false;
               
            },
                (err) => {
                    console.log(err);
                    this.IsLoading = false;
                   
                })
            
        } else {
            this.favoriteService.removeFavorite(path).subscribe(results => {
                this._snackBar.open('Removido do favorito', 'X', {
                    duration: 2000,
                });      
                var index = this.Favorites.map(function (e) { return e.path }).indexOf(path);
                if (index >= 0) {
                    this.Favorites.splice(index, 1);
                }
                this.IsLoading = false;
                
            },
                (err) => {
                    console.log(err);
                    this.IsLoading = false;
                   
                })
           
        }
    }
}