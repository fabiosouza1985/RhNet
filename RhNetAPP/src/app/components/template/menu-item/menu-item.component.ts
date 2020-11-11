import {Component, Input, OnInit, ViewChild} from '@angular/core';
import {Router} from '@angular/router';
import {MenuItem} from 'src/app/components/models/adm/menuItem.model';

@Component({
  selector: 'app-menu-item',
  templateUrl: './menu-item.component.html',
  styleUrls: ['./menu-item.component.css']
})
export class MenuItemComponent implements OnInit {
  @Input() items: MenuItem[];
  @ViewChild('childMenu', {static: true}) public childMenu: any;

  constructor(public router: Router) {
  }

  ngOnInit() {
  }

}
