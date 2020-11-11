import { Component, OnInit } from '@angular/core';
import {UserService} from 'src/app/components/services/adm/user.service';
import {Profile} from 'src/app/components/models/adm/profile.model';
import {Variables} from 'src/app/components/variables';
import { Router } from '@angular/router';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  profiles: Profile[] = [];

  constructor(private service: UserService, private variable : Variables, private router: Router) { }

  ngOnInit(): void {
    this.service.getRoles().subscribe(results => {
      this.profiles = results;
      
  },
    (err) => {           
      console.log(err)})
  }

  setPerfil(perfil) : void{
    this.variable.CurrentProfile = perfil;
    localStorage.setItem("currentProfile", perfil);
    this.router.navigate(['/home']);
  }
}
