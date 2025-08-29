import { Component, inject, OnInit, signal } from '@angular/core';
import { HeaderComponent } from './layout/header/header.component';
import { Product } from './shared/models/products';
import { ShopService } from './core/services/shop.service';

@Component({
  selector: 'app-root',
  imports: [ HeaderComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  private shopService = inject(ShopService);
  protected  title = 'skinet';
  products: Product[] = [];

ngOnInit(): void {
    this.shopService.getProducts().subscribe({
      next: response => this.products = response.data,
      error: error => console.log(error)
    });
}
}
