import { Component, OnInit } from '@angular/core';
import {UserService} from 'src/app/components/services/adm/user.service';
import {Variables} from 'src/app/components/variables';
import { Router } from '@angular/router';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {


  constructor(private service: UserService, public variable : Variables, private router: Router) { }

    ngOnInit(): void {
     
        }
   

  setPerfil(perfil) : void{
    this.variable.CurrentProfile = perfil;
      localStorage.setItem("currentProfile", perfil);
      this.variable.GetMenus();
      this.variable.GetQuickAccess();
    this.router.navigate(['/home']);
  }
}
