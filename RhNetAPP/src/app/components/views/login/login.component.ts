import { Component, OnInit } from '@angular/core';
import {AuthService} from 'src/app/components/services/adm/auth.service';
import { Router } from '@angular/router';
import {Variables} from 'src/app/components/variables';
import { Client } from 'src/app/components/models/adm/client.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

    clients: Client[] = [];
    selectedClient: Client = null;
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
        this.service.login(this.user_login.userName, this.user_login.password, this.selectedClient).subscribe(results => {     

            this.variable.Username = results.username;
            this.variable.Profiles = JSON.parse(results.profiles);
            this.variable.CurrentProfile = this.variable.Profiles[0].name;
            this.variable.SelectedClient = JSON.parse(results.currentClient);
            
            localStorage.setItem('token', results.access_token);
            localStorage.setItem('username', results.username);
            localStorage.setItem('email', results.email);
            localStorage.setItem('currentProfile', this.variable.Profiles[0].name);
            localStorage.setItem("currentClient", this.variable.SelectedClient.cnpj);
           
         
            this.variable.setTitle();

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

            if (err.status === 409) {
                
                alert("Selecione um cliente e logue novamente");
                this.clients = err.error;
            } else if (err.status === 0) {
                alert('Não foi possível acessar o servidor');
            } else {
                var erro = '';
                if (err.error.modelState !== null) {
                    for (var i = 0; i < err.error.modelState.error_description.length; i++) {
                        erro += err.error.modelState.error_description[i] + '\n '
                    }
                    alert(erro);
                } else {
                    alert(err.error);
                }

            };
               
      
  });
  }
}
