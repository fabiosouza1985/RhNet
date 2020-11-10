import { Component, OnInit } from '@angular/core';
import {AuthService} from 'src/app/components/services/auth.service';
@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  autenticado = false;
  constructor(service : AuthService) { 
    this.autenticado = service.isAuthenticated();
  }

  ngOnInit(): void {
  }

}
