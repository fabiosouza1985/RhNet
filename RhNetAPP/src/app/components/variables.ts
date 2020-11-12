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
            console.log(results);
        },
            (err) => {

                console.log(err);
            });
    }
}