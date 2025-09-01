import { Component, inject, OnInit } from '@angular/core';
import { ShopService } from '../../core/services/shop.service';
import { Product } from '../../shared/models/products';
import { ProductItemComponent } from "./product-item/product-item.component";
import { MatDialog } from '@angular/material/dialog';
import { FilterDialogComponent } from './filter-dialog/filter-dialog.component';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { MatMenu } from '@angular/material/menu';
import { MatListOption, MatSelectionList, MatSelectionListChange } from '@angular/material/list';
import { MatMenuTrigger } from '@angular/material/menu';
import { ShopParams } from '../../shared/models/shopParams';

@Component({
  selector: 'app-shop',
  imports: [
    ProductItemComponent,
    MatButton,
    MatIcon,
    MatMenu,
    MatSelectionList,
    MatListOption,
    MatMenuTrigger
],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss'
})
export class ShopComponent implements OnInit {
  private shopService = inject(ShopService);
  private dialogService = inject(MatDialog);
  products: Product[] = [];
  sortOptions = [
    {name: 'Alphabetical', value: 'name'},
    {name: 'Price: Low-High', value: 'priceAsc'},
    {name: 'Price: High-Low', value: 'priceDesc'},
  ];

    shopParams = new ShopParams();


  ngOnInit(): void {
    this.initializeShop();
    };

    initializeShop(){
      this.shopService.getBrands();
      this.shopService.getTypes();
      this.getProducts();
  }

  getProducts(){
    this.shopService.getProducts(this.shopParams)
    .subscribe({
      next: response => this.products = response.data,
      error: error => console.log(error)
    })
  }

  onSortChange(event: MatSelectionListChange){
    const selectOption = event.options[0];
    if(selectOption.selected){
      this.shopParams.sort = selectOption.value;
      this.getProducts();
    }
  }
  

 openFilterDialog(){
  const dialogRef = this.dialogService.open(FilterDialogComponent, {
    minWidth: '500px',
    minHeight: '500px',
    data: {
      selectedBrands: this.shopParams.brands,
      selectedTypes: this.shopParams.types
    }
  });

  dialogRef.afterClosed().subscribe({
    next: result => {
      if(result){
        this.shopParams.brands = result.selectedBrands;
        this.shopParams.types = result.selectedTypes;
        this.getProducts();
      }
    }
  });

 }







 
}
