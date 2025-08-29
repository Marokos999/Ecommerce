import { Component, inject, OnInit, signal } from '@angular/core';
import { HeaderComponent } from './layout/header/header.component';
import { ShopComponent } from "./fearures/shop/shop.component";

@Component({
  selector: 'app-root',
  imports: [HeaderComponent, ShopComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
title = 'skinet';
}
