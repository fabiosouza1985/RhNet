import { Component, OnInit } from '@angular/core';
import {AuthService} from 'src/app/components/services/adm/auth.service';
import { Router } from '@angular/router';
import {Variables} from 'src/app/components/variables';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  user_login = {userName: '', password: ''}
  constructor(private service: AuthService, private router: Router, public variable: Variables) { }

  ngOnInit(): void {
    var token = localStorage.getItem('token')

    if(token !== null && token !== undefined && this.service.isAuthenticated()){      
      this.router.navigate(['/home'])
    }

    if(this.service.isAuthenticated() == false){
      localStorage.clear();
    }
  }

    login(): void{
        this.variable.IsLoading = true;
        this.variable.IsEnabled = false;
    this.service.login(this.user_login.userName, this.user_login.password).subscribe(results => {     
      localStorage.setItem('token', results.token);
      localStorage.setItem('username', results.username);
      localStorage.setItem('email', results.email);
      localStorage.setItem('currentProfile', results.profiles[0].name);

      this.variable.Username = results.username;
      this.variable.Profiles = results.profiles;
      this.variable.CurrentProfile = results.profiles[0].name;

      this.router.navigate(['/home']);
        this.variable.GetMenus();
        this.variable.GetQuickAccess();
        this.variable.GetFavorites();
        this.variable.IsLoading = false;
        this.variable.IsEnabled = true;
  },
        (err) => {     
            this.variable.IsLoading = false;
            this.variable.IsEnabled = true;
            if (err.status === 0) {
                alert('Não foi possível acessar o servidor');
            } else {
                alert(err.error);   
            };   
      
  });
  }
}
