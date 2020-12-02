import { Component, Input, OnInit } from '@angular/core';
import { FavoriteService } from 'src/app/components/services/adm/favorite.service';
import { Variables } from 'src/app/components/variables';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header-page',
  templateUrl: './header-page.component.html',
  styleUrls: ['./header-page.component.css']
})
export class HeaderPageComponent implements OnInit {
    @Input() title: string;
    
    constructor(public variable: Variables, private router: Router, private favoriteService: FavoriteService) { }

    ngOnInit(): void {

        var currentProfile = '';
        if (this.variable.CurrentProfile.length > 0) {
            currentProfile = this.variable.CurrentProfile
        } else {
            currentProfile = localStorage.getItem('currentProfile');
        }
        this.favoriteService.isFavorite(this.router.url, currentProfile).subscribe(results => {
            this.variable.IsFavorite = results;
        },
            (err) => {
                console.log(err)
            })
    }

    addRemoveFavorite(): void {
        this.variable.addRemoveFavorite(!this.variable.IsFavorite, this.router.url);
      
    }

}
