import { Component, OnInit } from '@angular/core';
import {UserService} from 'src/app/components/services/user.service';
import {Profile} from 'src/app/components/models/adm/profile.model';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  profiles: Profile[] = [];

  constructor(private service: UserService) { }

  ngOnInit(): void {
    this.service.getRoles().subscribe(results => {
      this.profiles = results;
  },
    (err) => {           
      console.log(err)})
  }

}
