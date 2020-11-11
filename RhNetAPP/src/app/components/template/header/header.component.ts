import { Component, OnInit } from '@angular/core';
import {Variables} from 'src/app/components/variables';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor(public variable: Variables) { 

  }

  ngOnInit(): void {
    
  }

}
