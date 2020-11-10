import { Component, OnInit } from '@angular/core';
import {AuthService} from 'src/app/components/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  user_login = {userName: '', password: ''}
  constructor(private service: AuthService, private router: Router) { }

  ngOnInit(): void {
    var token = localStorage.getItem('token')

    if(token !== null && token !== undefined){      
      this.router.navigate(['/home'])
    }
  }

  login(): void{
    this.service.login(this.user_login.userName, this.user_login.password).subscribe(results => {
      localStorage.setItem('token', results.access_token);
      this.router.navigate(['/home']);
  },
    (err) => {     
      alert(err.error.modelState.error_description);   
  });
  }
}
