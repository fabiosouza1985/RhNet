import { Component, OnInit } from '@angular/core';
import { Variables } from 'src/app/components/variables';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  
    constructor(public variable: Variables) { 
    
  }

  ngOnInit(): void {
  }

}
