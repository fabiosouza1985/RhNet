import { Component, OnInit } from '@angular/core';
import {Variables} from 'src/app/components/variables';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private variable : Variables, private router: Router) { }

  ngOnInit(): void {
    if(this.variable.Username == '' || this.variable.Username == null){
      this.router.navigate(['/login']);
    }
  }

}
