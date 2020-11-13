import { Component, OnInit } from '@angular/core';
import {Variables} from 'src/app/components/variables';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

    constructor(public variable: Variables, private router: Router) { 

  }

  ngOnInit(): void {
    
  }

    logoff(): void {
        this.variable.Logoff();
        this.router.navigate(['/login']);
    }
}
