import { Injectable } from '@angular/core';
import {UserService} from 'src/app/components/services/adm/user.service'
import {Profile} from 'src/app/components/models/adm/profile.model';

@Injectable({
    providedIn: 'root'
  })
export class Variables {
    
    
    
    public Username: String = "";
    public Profiles: Profile[] = [];
    public CurrentProfile = "";

    constructor(private service: UserService){
        if(localStorage.getItem("username") !== null && localStorage.getItem("username") !== undefined){
            this.Username = localStorage.getItem("username");

            this.service.getRoles().subscribe(results => {     
                this.Profiles = results;      
                if(localStorage.getItem("currentProfile") === null || localStorage.getItem("username") === undefined){
                    localStorage.setItem("currentProfile", this.Profiles[0].name);                    
                }       
                this.CurrentProfile = localStorage.getItem("currentProfile");
            },
              (err) => {     
                
                console.log(err);   
            });
        }
    }
}