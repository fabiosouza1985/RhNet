import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpResponse
} from '@angular/common/http';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs';
import {tap} from 'rxjs/internal/operators';
import { Router } from '@angular/router';


@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  constructor(public auth: AuthService, private router: Router) {}
  
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
      
    request = request.clone({
      setHeaders: {
        Authorization: `Bearer ${this.auth.getToken()}`
      }
    });

    return next.handle(request).pipe(
        tap(
          event => this.handleResponse(request, event),
          error => this.handleError(request, error)
        )
      );
      
    
  }

  handleResponse(req: HttpRequest<any>, event) {
    //console.log('Handling response for ', req.url, event);
      if (event instanceof HttpResponse) {
          
              this.auth.refresh_token();
         
      // console.log('Request for ', req.url,
      //     ' Response Status ', event.status);
    }
  }

  handleError(req: HttpRequest<any>, event) {
     
    if(this.auth.getToken() === undefined || this.auth.getToken() === null || this.auth.isAuthenticated() === false){
      localStorage.removeItem('token');
      localStorage.removeItem('username');
      localStorage.removeItem('email');
      localStorage.removeItem('currentProfile');
      this.router.navigate(['/login']);    
    }  

      if (event.status === 403) {
          alert("Acesso negado");
      };
  }
  
}